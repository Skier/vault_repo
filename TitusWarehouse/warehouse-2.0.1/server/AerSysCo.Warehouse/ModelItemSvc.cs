using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{
public class ModelItemSvc : TraceableSvc
{
    public enum StatusFilter {ACTIVE_ONLY, PASSIVE_ONLY, ALL};

    public static ModelItem FindById(SqlTransaction tran, int id, StatusFilter status) {
        Logger.ASSERT( 0 != id );
        List<ModelItem> result = Select(tran, id, 0, 0, 0, status );
        Logger.ASSERT(1 >= result.Count);
        if ( 0 == result.Count ) {
            string message = string.Format("No ModelItem with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        result[0].item = ItemSvc.FindById(tran, result[0].itemId);
        return result[0];
    }

    public static ModelItem FindById(SqlTransaction tran, int id) {
        return FindById(tran, id, StatusFilter.ACTIVE_ONLY );
    }

    public static ModelItem FindByModelAndItem(SqlTransaction tran, int modelId, int itemId) {
        Logger.ASSERT( 0 != modelId );
        Logger.ASSERT( 0 != itemId );
        List<ModelItem> result = Select(tran, 0, 0, itemId, modelId, StatusFilter.ACTIVE_ONLY);
        Logger.ASSERT(1 >= result.Count);
        if ( 0 == result.Count ) {
            return null;
        }
        return result[0];
    }

    public static ModelItem FindByItemAndBrandSoft(SqlTransaction tran, int brandId, int itemId) {
        return  RawFindByItemAndBrand(tran, brandId, itemId);
    }

    public static List<ModelItem> FindAllByItemAndBrand(SqlTransaction tran, int brandId, int itemId) {
        Logger.ASSERT( 0 != brandId );
        Logger.ASSERT( 0 != itemId );
        List<ModelItem> result = Select(tran, 0, brandId, itemId, 0, StatusFilter.ALL );
        return result;
    }

    public static ModelItem FindByItemAndBrand(SqlTransaction tran, int brandId, int itemId) {
        ModelItem result = RawFindByItemAndBrand(tran, brandId, itemId);
        if ( null == result ){
            string message = string.Format("No ModelItem with brandId {0} and ItemId {1} ", brandId, itemId );
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        result.item = ItemSvc.FindById(tran, itemId);
        return result;
    }

    public static ModelItem FindByItemAndBrand(SqlTransaction tran, int brandId, Item item) {
        ModelItem result = RawFindByItemAndBrand(tran, brandId, item.itemId);
        if ( null == result ){
            string message = string.Format("No ModelItem with brandId {0} and ItemId {1} ", brandId, item.itemId );
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        result.item = item;
        return result;
    }

    public static List<ModelItem> GetByModelId(SqlTransaction tran, int modelId) {
        List<ModelItem> result = Select(tran,0, 0, 0, modelId, StatusFilter.ACTIVE_ONLY);
        return result;
    }

    public static ModelItem FromReader(SqlDataReader rdr) {
        ModelItem mi = new ModelItem();
        mi.modelItemId = rdr.GetInt32(rdr.GetOrdinal("ModelItemId"));
        mi.modelId = rdr.GetInt32(rdr.GetOrdinal("ModelId"));
        mi.itemId = rdr.GetInt32(rdr.GetOrdinal("ItemId"));
        mi.configuration = rdr.GetString(rdr.GetOrdinal("Configuration"));
        mi.price = rdr.GetDecimal(rdr.GetOrdinal("Price"));
        mi.isActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
        mi.imageURL = rdr.GetString(rdr.GetOrdinal("ImageURL"));
        mi.xmlBullerDescription = rdr.GetString(rdr.GetOrdinal("XMLBullerDescription"));
        TraceableSvc.FromReader(rdr, mi);
        return mi;
    }

    public static ModelItem Insert(SqlTransaction tran, ModelItem mi) {
        string sql = 
            " insert into ModelItem ( ModelId, ItemId, Configuration, Price, IsActive,  ImageURL, "
           +"        XMLBullerDescription, DateCreated, LastUpdateDate, CreatedByUser, MacPac_Inventory_id ) "
           +" values ( @ModelId, @ItemId, @Configuration, @Price, @IsActive,  @ImageURL, "
           +"        @XMLBullerDescription, @DateCreated, @LastUpdateDate, @CreatedByUser, @MacPac_Inventory_id ) ";

        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ModelId", mi.modelId ));
        parms.Add(new SqlParameter("@ItemId", mi.itemId ));
        parms.Add(new SqlParameter("@Configuration", mi.configuration ));
        parms.Add(new SqlParameter("@Price", mi.price ));
        parms.Add(new SqlParameter("@IsActive", mi.isActive ));
        parms.Add(new SqlParameter("@ImageURL", mi.imageURL ));
        parms.Add(new SqlParameter("@XMLBullerDescription", mi.xmlBullerDescription ));
        parms.Add(new SqlParameter("@DateCreated", mi.dateCreated ));
        parms.Add(new SqlParameter("@LastUpdateDate", mi.lastUpdateDate ));
        parms.Add(new SqlParameter("@CreatedByUser", mi.createdByUser ));
        if ( 0 == mi.MACPACInventoryId ) {
            parms.Add(new SqlParameter("@MacPac_Inventory_id", DBNull.Value));
        } else {
            parms.Add(new SqlParameter("@MacPac_Inventory_id", mi.MACPACInventoryId));
        }

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        mi.modelItemId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("ModelItem created: created " + mi.configuration);
        return mi;
    }

    public static ModelItem Update(SqlTransaction tran, ModelItem mi) {
        string sql = " update ModelItem  "
                    +"    set Price = @Price, "
                    +"        IsActive = @IsActive, "
                    +"        LastUpdateDate = @LastUpdateDate, "
                    +"        MacPac_Inventory_id = @MacPac_Inventory_id, "
                    +"        Configuration  = @Configuration, "
                    +"        ImageURL  = @ImageURL, " 
                    +"        XMLBullerDescription = @XMLBullerDescription " 
                    +"  where ModelItemId = @ModelItemId ";

        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ModelItemId", mi.modelItemId ));
        parms.Add(new SqlParameter("@Configuration", mi.configuration ));
        parms.Add(new SqlParameter("@Price", mi.price ));
        parms.Add(new SqlParameter("@IsActive", mi.isActive ));
        parms.Add(new SqlParameter("@ImageURL", mi.imageURL ));
        parms.Add(new SqlParameter("@XMLBullerDescription", mi.xmlBullerDescription ));
        parms.Add(new SqlParameter("@LastUpdateDate", mi.lastUpdateDate ));
        if ( 0 == mi.MACPACInventoryId ) {
            parms.Add(new SqlParameter("@MacPac_Inventory_id", DBNull.Value));
        } else {
            parms.Add(new SqlParameter("@MacPac_Inventory_id", mi.MACPACInventoryId));
        }

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("ModelItem created: updated " + mi.configuration);
        return mi;
    }

    private static ModelItem RawFindByItemAndBrand(SqlTransaction tran, int brandId, int itemId) {
        Logger.ASSERT( 0 != brandId );
        Logger.ASSERT( 0 != itemId );
        List<ModelItem> result = Select(tran, 0, brandId, itemId, 0, StatusFilter.ACTIVE_ONLY);
        Logger.ASSERT(1 >= result.Count);
        if ( 0 == result.Count ) {
             return null;
        }
        return result[0];
    }

    private static List<ModelItem> Select(SqlTransaction tran, int id, int brandId, int itemId, int modelId, StatusFilter status) {
        string sql = " select ModelItemId, ModelId, ItemId, Configuration, Price, "
                   + "        IsActive, ImageURL, XMLBullerDescription, DateCreated, LastUpdateDate, CreatedByUser " 
                   + "   from ModelItem  "
                   + "  where 1= 1 " ;
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != id ) {
            sql += " and  ModelItemId = @ModelItemId ";
            parms.Add(new SqlParameter("@ModelItemId", id));
        }
        if ( 0 != itemId ) {
            sql += " and  ItemId = @ItemId ";
            parms.Add(new SqlParameter("@ItemId", itemId));
        }
        if ( 0 != modelId ) {
            sql += " and  ModelId = @ModelId ";
            parms.Add(new SqlParameter("@ModelId", modelId));
        }
        if ( 0 != brandId ) {
            sql += " and  exists ( select 1 from Model where ModelItem.ModelId = Model.ModelId and BrandId = @BrandId ) ";
            parms.Add(new SqlParameter("@BrandId", brandId));
        }
        if ( StatusFilter.ALL != status ) {
            sql += " and  IsActive = @IsActive ";
            parms.Add(new SqlParameter("@IsActive", status == StatusFilter.ACTIVE_ONLY ? 1 : 0));
        }
        List<ModelItem> result = new List<ModelItem>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
            }
        }
        return result;

    }

};
};