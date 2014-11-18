using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Warehouse
{

public class ShoppingCartDetailSvc 
{

    public static List<ShoppingCartDetail> GetByShoppingCartId(SqlTransaction tran, int shoppingCartId) {
        Logger.ASSERT( 0 != shoppingCartId );
        return FullFill(tran, Select(tran, 0, shoppingCartId, 0, 0));
    } 

    public static List<ShoppingCartDetail> GetByShipmentId(SqlTransaction tran, int shipmentId) {
        Logger.ASSERT( 0 != shipmentId );
        return FullFill(tran, Select(tran, 0, 0, shipmentId, 0));
    } 

    public static List<ShoppingCartDetail> GetActiveOnlyByModelItemId(SqlTransaction tran, int modeItemId) {
        Logger.ASSERT( 0 != modeItemId );
        return FullFill(tran, Select(tran, 0, 0, 0, modeItemId));
    } 

    public static ShoppingCartDetail GetById(SqlTransaction tran, int id) {
        Logger.ASSERT( 0 != id );
        List<ShoppingCartDetail> result =  Select(tran, id, 0, 0, 0);
        if ( 0 == result.Count ) {
            string message = string.Format("No Shoping Cart Detail with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);

        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} Shoping Cart Details with id {1} ", result.Count, id);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        ShoppingCart cart = ShoppingCartSvc.FindById(tran, result[0].shoppingCartId);

        return FullFill(tran, result[0]);
    } 

    public static ShoppingCartDetail InsertShoppingCartDetail(SqlTransaction tran, ShoppingCartDetail detail) {
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql = " insert into ShoppingCartDetail (ShoppingCartId, ShipmentId, ModelItemId, "
                    +"        QtyOrdered, QtyNeeded, Price, CreatedByUser, LastUpdateDate, DateCreated, SKU ) "
                    +" values (@ShoppingCartId, @ShoppingCartShipmentId, @ModelItemId, "
                    +"         @QtyOrdered, @QtyNeeded, @Price, @CreatedByUser, @LastUpdateDate, @DateCreated, @SKU ) ";

        parms.Add(new SqlParameter("@ShoppingCartId", detail.shoppingCartId));
        parms.Add(new SqlParameter("@ShoppingCartShipmentId", detail.shoppingCartShipmentId));
        parms.Add(new SqlParameter("@ModelItemId", detail.modelItemId));
        parms.Add(new SqlParameter("@QtyOrdered", detail.qtyOrdered));
        parms.Add(new SqlParameter("@QtyNeeded", detail.qtyNeeded));
        parms.Add(new SqlParameter("@Price", detail.price));
        parms.Add(new SqlParameter("@CreatedByUser", detail.createdByUser));
        parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now));
        parms.Add(new SqlParameter("@DateCreated", DateTime.Now));
        parms.Add(new SqlParameter("@SKU", detail.sku)); 

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        detail.shoppingCartDetailId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("ShoppingCartDetailSvc: created " + detail.sku);
        return  GetById(tran, detail.shoppingCartDetailId);
    }

    public static ShoppingCartDetail UpdateShoppingCartDetail(SqlTransaction tran, ShoppingCartDetail detail) {
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql = " update ShoppingCartDetail "
                   + "    set QtyOrdered     = @QtyOrdered, "    
                   + "        QtyNeeded      = @QtyNeeded, "     
                   + "        Price          = @Price, "         
                   + "        LastUpdateDate = @LastUpdateDate "
                   + "  where ShoppingCartDetailId  = @ShoppingCartDetailId ";

        parms.Add(new SqlParameter("@QtyOrdered", detail.qtyOrdered));    
        parms.Add(new SqlParameter("@QtyNeeded", detail.qtyNeeded));     
        parms.Add(new SqlParameter("@Price", detail.price));         
        parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now));
        parms.Add(new SqlParameter("@ShoppingCartDetailId", detail.shoppingCartDetailId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("ShoppingCartDetailSvc: Updated " + detail.sku);
        return GetById(tran, detail.shoppingCartDetailId);
    }

    public static void RemoveShoppingCartDetail(SqlTransaction tran, int shoppingCartDetailId) {
        Logger.ASSERT( 0 != shoppingCartDetailId );
        string sql = " delete from ShoppingCartDetail "
                   + "  where ShoppingCartDetailId = @ShoppingCartDetailId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ShoppingCartDetailId", shoppingCartDetailId));
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("ShoppingCartDetailSvc: Deleted  " + shoppingCartDetailId);
    }

    public static List<ShoppingCartDetail> FullFill(SqlTransaction tran, List<ShoppingCartDetail> details) { 
        foreach ( ShoppingCartDetail detail in details ) {
            FullFill(tran, detail);
        }
        return details;
    }

    public static ShoppingCartDetail FullFill(SqlTransaction tran, ShoppingCartDetail detail) {
        detail.modelItem = ModelItemSvc.FindById(tran, detail.modelItemId, ModelItemSvc.StatusFilter.ALL);
        int customerId = ShoppingCartSvc.GetCustomerId(tran, detail.shoppingCartId);
        CustomerPriceSvc.FillCustomerPrice(tran, customerId, detail.modelItem);

        ShoppingCartShipment shipment = ShoppingCartShipmentSvc.FindById(tran, detail.shoppingCartShipmentId);
        detail.inventory = InventorySvc.FindByItemWarehouse(tran, detail.modelItem.itemId, shipment.warehouseId); 
        detail.modelName = ModelSvc.FindById(tran, detail.modelItem.modelId).modelName;
        return detail;
    }

    private static List<ShoppingCartDetail> Select(SqlTransaction tran, int id, int shoppingCartId, int shipmentId, int modelItemId) {
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql = "select ShoppingCartDetailId, ShoppingCartId, ShipmentId, ModelItemId, "
                   + "       QtyOrdered, QtyNeeded, Price, SKU, "
                   + "       CreatedByUser, LastUpdateDate, DateCreated "
                   + "  from ShoppingCartDetail "
                   + " where 1=1 ";
        if ( 0 != shoppingCartId ) {
            sql += " and  ShoppingCartId = @ShoppingCartId ";
            parms.Add(new SqlParameter("@ShoppingCartId", shoppingCartId));
        }
        if ( 0 != id ) {
            sql += " and  ShoppingCartDetailId = @ShoppingCartDetailId ";
            parms.Add(new SqlParameter("@ShoppingCartDetailId", id));
        }
        if ( 0 != shipmentId ) {
            sql += " and  ShipmentId = @ShoppingCartShipmentId ";
            parms.Add(new SqlParameter("@ShoppingCartShipmentId", shipmentId));
        }
        if ( 0 != modelItemId ) {
            sql += " and  ModelItemId = @ModelItemId "
                  +" and  1 = ( select IsActive from ShoppingCart where ShoppingCart.ShoppingCartId = ShoppingCartDetail.ShoppingCartId ) ";
            parms.Add(new SqlParameter("@ModelItemId", modelItemId));
        }

        List<ShoppingCartDetail> result = new List<ShoppingCartDetail>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
            }
        }
        return result;
    }

    private static ShoppingCartDetail FromReader(SqlDataReader rdr) {
         ShoppingCartDetail d = new ShoppingCartDetail();
         d.shoppingCartDetailId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartDetailId"));
         d.shoppingCartId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartId"));
         d.shoppingCartShipmentId = rdr.GetInt32(rdr.GetOrdinal("ShipmentId"));
         d.modelItemId = rdr.GetInt32(rdr.GetOrdinal("ModelItemId"));
         d.qtyOrdered = rdr.GetInt32(rdr.GetOrdinal("QtyOrdered"));
         d.qtyNeeded = rdr.GetInt32(rdr.GetOrdinal("QtyNeeded"));
         d.price = rdr.GetDecimal(rdr.GetOrdinal("Price"));
         d.sku = rdr.GetString(rdr.GetOrdinal("SKU"));
         TraceableSvc.FromReader(rdr, d);
         return d;
    }
}

}