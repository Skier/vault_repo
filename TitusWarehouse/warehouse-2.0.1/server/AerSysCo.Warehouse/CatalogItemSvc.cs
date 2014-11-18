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
public class CatalogItemSvc : TraceableSvc
{

    public static List<CatalogItem> GetByCategory(SqlTransaction tran, int categoryId) {
         List<CatalogItem> result = new List<CatalogItem>();

         List<Model> models = ModelSvc.GetByCategoryOnly(tran, categoryId);
         foreach (Model model in models) {
             List<CatalogItem> items = GetByModel(tran, model.modelId);
             result.AddRange(items);
         }

         List<Category> categories = CategorySvc.GetChildren(tran, categoryId, 0);
         foreach ( Category category in categories) {
             List<CatalogItem> items = GetByCategory(tran, category.CategoryId);
             result.AddRange(items);
         }
         return result;
    }

    public static List<CatalogItem> GetByModel(SqlTransaction tran, int modelId) {
        Logger.ASSERT( 0 != modelId );
        return Select(tran, modelId, 0, null);
    }

    public static List<CatalogItem> Search(SqlTransaction tran, int brandId, string search) {
        Logger.ASSERT( 0 != brandId );
        return Select(tran, 0, brandId, search);
    }

    public static int CompareBySku(CatalogItem item1, CatalogItem item2) {
        return item1.item.sku.CompareTo(item2.item.sku);
    }

    private static List<CatalogItem> Select(SqlTransaction tran, int modelId, int brandId, string search) {
        string sql = 
            "select mi.ModelItemId, mi.ModelId, mi.ItemId, mi.Configuration, mi.Price, mi.IsActive, mi.ImageURL, "
           +"       mi.XMLBullerDescription, mi.DateCreated, mi.LastUpdateDate, mi.CreatedByUser, "
           +"       i.SKU, i.Description, i.Width, i.Length, i.Height, i.Weight, i.QtyIncrement, "    
           +"       inv.InventoryId, inv.WarehouseId, inv.Qty, m.ModelName, "
           +"       ( select coalesce(sum(Qty), 0) "
           +"           from OrderDetail od "
           +"                inner join TheOrder o on od.OrderId = o.OrderId  "
           +"          where od.ItemId = i.ItemId "
           +"            and inv.WarehouseId = o.WarehouseId "
           +"            and o.OrderStatusId != 2 and o.OrderStatusId != -1 ) QtyAllocated "
           +"  from ModelItem mi"
           +"       inner join Item i on mi.ItemId = i.ItemId "
           +"       inner join Inventory inv on inv.ItemId = i.ItemId "
           +"       inner join Model m on mi.ModelId = m.ModelId "
           +"       inner join Warehouse w on inv.WarehouseId = w.WarehouseId and w.WarehouseCode not like '*%' "
           +" where i.IsActive = 1 and mi.IsActive = 1 and m.IsActive = 1 "; 
        string  orderBy = " order by mi.ModelItemId, i.ItemId, inv.InventoryId ";

        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != modelId ) {
            sql += " and mi.ModelId = @modelId ";
            SqlParameter param = new SqlParameter("@modelId", modelId);
            parms.Add(param);
        }
        if ( 0 != brandId ) {
            sql += " and exists ( select 1 from Model where Model.ModelId = mi.ModelId and Model.BrandId = @brandId ) ";
            SqlParameter param = new SqlParameter("@brandId", brandId);
            parms.Add(param);
        }
        if ( null != search ) {
            string[] keys = search.Split(new char[] {' '});
            string descriptionLikeString = MakeLikeString("i.description", keys);
            string configurationLikeString = MakeLikeString("mi.configuration", keys);
            string modelNameLikeString = MakeLikeString("Model.ModelName", keys);

            sql += " and ( i.SKU = @SKU or i.description like @search  or mi.configuration like @search or "
                 + descriptionLikeString+" or "+configurationLikeString
                 + " or exists ( select 1 from  Model "
                                      + " where Model.ModelId = mi.ModelId "
                                      + "    and ( Model.ModelName like @search "
                                      + "         or "+modelNameLikeString+" ))) " ;

#if FALSE
            sql += " and ( i.SKU = @SKU or i.description like @search or mi.configuration like @search  "
                 + " or exists ( select 1 from  Model "
                                      + " where Model.ModelId = mi.ModelId "
                                      + "   and Model.ModelName like @search )) " ;
#endif

            SqlParameter param = new SqlParameter("@SKU", search);
            parms.Add(param);
            param = new SqlParameter("@search", "%"+search+"%");
            parms.Add(param);
        }

        sql += orderBy;
        List<CatalogItem> result = new List<CatalogItem>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            int modelItemId = 0;
            CatalogItem ci = null;
            while( rdr.Read() ) {
                if ( modelItemId != rdr.GetInt32(rdr.GetOrdinal("ModelItemId")) ) {
                    modelItemId = rdr.GetInt32(rdr.GetOrdinal("ModelItemId"));
                    ci = new CatalogItem();
                    ci.modelItem  = ModelItemSvc.FromReader(rdr);
                    ci.item = ItemSvc.FromReader(rdr);
                    ci.modelName = rdr.GetString(rdr.GetOrdinal("ModelName"));
                    result.Add(ci);
                }
                Inventory inv = InventorySvc.FromReader(rdr);
                inv.qtyAllocated = rdr.GetInt32(rdr.GetOrdinal("QtyAllocated"));
                ci.inventories.Add(inv);
            }
        }
          
        return result;

    } 

    private static string MakeLikeString(string field, string[] keys) {
       StringBuilder result = new StringBuilder();
       result.Append(" ( 1=1 ");
       foreach( string key in keys) {
           result.AppendFormat(" and {0} like '%{1}%' ", field, key.Trim());
       }
       result.Append(" ) ");
       return result.ToString();
    }
};
};
