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
public class ItemSvc : TraceableSvc
{
    public static List<Item> GetAll(SqlTransaction tran) { 
        return Select(tran, 0, null);
    }

    public static Item FindById(SqlTransaction tran, int id) { 
        Logger.ASSERT( 0 != id );
        List<Item> result = Select(tran, id, null);
        Logger.ASSERT( 1 >= result.Count ); 
        if ( 0 == result.Count ) {
            string message = string.Format("No Item with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static Item FindBySku(SqlTransaction tran, string sku) { 
        Logger.ASSERT( null != sku );
        Logger.ASSERT( !"".Equals(sku) );
        List<Item> result = Select(tran, 0, sku);
        Logger.ASSERT( 1 >= result.Count ); 
        if ( 0 == result.Count ) {
            return null;
        }
        return result[0];
    }

    public static Item FromReader(SqlDataReader rdr) {
        Item item = new Item();
        item.itemId = rdr.GetInt32( rdr.GetOrdinal("ItemId"));
        item.sku = rdr.GetString( rdr.GetOrdinal("SKU"));
        item.description = rdr.GetString(rdr.GetOrdinal("Description"));
        item.width = rdr.GetDouble(rdr.GetOrdinal("Width"));
        item.length = rdr.GetDouble(rdr.GetOrdinal("Length"));
        item.height = rdr.GetDouble(rdr.GetOrdinal("Height"));
        item.weight = rdr.GetDouble(rdr.GetOrdinal("Weight"));
        item.qtyIncrement = rdr.GetInt32(rdr.GetOrdinal("QtyIncrement"));
        item.isActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
        TraceableSvc.FromReader(rdr, item);
        return item;
    }

    public static Item Insert(SqlTransaction tran, Item item) {
        string sql = 
            " insert into Item ( SKU, Description, Width, Length, Height, Weight, QtyIncrement, IsActive, "
           +"        DateCreated, CreatedByUser, LastUpdateDate ) "
           +" values(@SKU, @Description, @Width, @Length, @Height, @Weight, @QtyIncrement, @IsActive, "
           +"        @DateCreated, @CreatedByUser, @LastUpdateDate ) ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@SKU", item.sku ));
        parms.Add(new SqlParameter("@Description", item.description ));
        parms.Add(new SqlParameter("@Width",  item.width ));
        parms.Add(new SqlParameter("@Length", item.length ));
        parms.Add(new SqlParameter("@Height", item.height ));
        parms.Add(new SqlParameter("@Weight", item.weight ));
        parms.Add(new SqlParameter("@QtyIncrement", item.qtyIncrement ));
        parms.Add(new SqlParameter("@IsActive", item.isActive ));
        parms.Add(new SqlParameter("@DateCreated", item.dateCreated ));
        parms.Add(new SqlParameter("@CreatedByUser", item.createdByUser ));
        parms.Add(new SqlParameter("@LastUpdateDate", item.lastUpdateDate ));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        item.itemId = SQLHelper.GetIdentity(tran);
        return item;

    }

    public static Item Update(SqlTransaction tran, Item item) {
        string sql = 
            " update Item  "
           +"    set Description = @Description,  "
           +"        Width = @Width, "
           +"        Length = @Length, " 
           +"        Height = @Height, "
           +"        Weight = @Weight, "
           +"        QtyIncrement = @QtyIncrement, "
           +"        IsActive = @IsActive, "
           +"        LastUpdateDate = @LastUpdateDate "
           +" where ItemId = @ItemId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ItemId", item.itemId));
        parms.Add(new SqlParameter("@Description", item.description ));
        parms.Add(new SqlParameter("@Width",  item.width ));
        parms.Add(new SqlParameter("@Length", item.length ));
        parms.Add(new SqlParameter("@Height", item.height ));
        parms.Add(new SqlParameter("@Weight", item.weight ));
        parms.Add(new SqlParameter("@QtyIncrement", item.qtyIncrement ));
        parms.Add(new SqlParameter("@IsActive", item.isActive ));
        parms.Add(new SqlParameter("@LastUpdateDate", item.lastUpdateDate ));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        return item;
    }


    private static List<Item> Select(SqlTransaction tran, int id, string sku) {
        string sql = 
            "select ItemId, SKU, Description, Width, Length, Height, Weight, QtyIncrement, IsActive, DateCreated, CreatedByUser, LastUpdateDate "
           +"  from Item "
           +" where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();

        if ( 0 != id ) {
            sql += " and ItemId = @ItemId ";
            parms.Add( new SqlParameter("@ItemId", id));
        }

        if ( null != sku ) {
            sql += "  and SKU = @SKU";
            parms.Add(new SqlParameter("@SKU", sku));
        }

        List<Item> result = new List<Item>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
            }
        }
        return result;
    }

};
};