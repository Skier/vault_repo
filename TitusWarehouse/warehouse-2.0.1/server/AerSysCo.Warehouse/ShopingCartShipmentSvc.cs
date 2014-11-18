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

public class ShoppingCartShipmentSvc 
{

    public static List<ShoppingCartShipment> GetByShopingCartId(SqlTransaction tran, int shopingCartId) {
        Logger.ASSERT( 0 != shopingCartId );
        return Select(tran, 0, shopingCartId);
    } 

    public static ShoppingCartShipment FindById(SqlTransaction tran, int id) {
        Logger.ASSERT( 0 != id );
        List<ShoppingCartShipment> result = Select(tran, id, 0);
        Logger.ASSERT(1 >= result.Count);
        if ( 0 == result.Count ) {
            string message = string.Format("No ShopingCardShipment with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    } 

    public static ShoppingCartShipment FullFill(SqlTransaction tran, ShoppingCartShipment shipment) {
        shipment.details = ShoppingCartDetailSvc.GetByShipmentId(tran, shipment.shoppingCartShipmentId);
        return shipment;
    }

#if FALSE 
    public static decimal GetDetailTotal(SqlTransaction tran, Entity.ShoppingCart cart, ShoppingCartShipment shipment) {
        decimal result = 0m;
        foreach (ShoppingCartDetail detail in shipment.details ) {
            CustomerPrice price = CustomerPriceSvc.FindByKey(tran, cart.customerId, detail.modelItemId);
            result += detail.price * detail.qtyOrdered * (null == price ? 1m : new decimal(price.multiplier));
        }
        return result;
    }
#endif

    public static ShoppingCartShipment Update(SqlTransaction tran, ShoppingCartShipment shipment) {
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql = 
            "update ShoppingCartShipment "
           +"   set ShippingTypeId = @ShippingTypeId, "
           +"       ShippingTotal = @ShippingTotal, "
           +"       LastUpdateDate = @LastUpdateDate, "
           +"       PONumber = @PONumber, "
           +"       NeedLiftGate = @NeedLiftGate, "
           +"       LiftGatePrice = @LiftGatePrice " 
           +" where ShoppingCartShipmentId = @ShoppingCartShipmentId ";

        parms.Add(new SqlParameter("@ShoppingCartShipmentId", shipment.shoppingCartShipmentId )); 
        if ( 0 == shipment.shippingTypeId ) {
            parms.Add(new SqlParameter("@ShippingTypeId",  DBNull.Value)); 
        } else { 
            parms.Add(new SqlParameter("@ShippingTypeId",  shipment.shippingTypeId)); 
        } 
        parms.Add(new SqlParameter("@ShippingTotal", shipment.shippingTotal ));
        parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now));
        if ( null == shipment.PoNumber ) { 
            parms.Add(new SqlParameter("@PONumber", DBNull.Value)); 
        } else {
            parms.Add(new SqlParameter("@PONumber", shipment.PoNumber)); 
        }
        parms.Add(new SqlParameter("@NeedLiftGate", shipment.needLiftGate ));
        parms.Add(new SqlParameter("@LiftGatePrice", shipment.liftGatePrice));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("ShopingCartShipmentSvc: updated " + shipment.shoppingCartId+" "+shipment.warehouseId);

        return FindById(tran, shipment.shoppingCartShipmentId); 
    }

    public static ShoppingCartShipment Insert(SqlTransaction tran, ShoppingCartShipment shipment) {
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql = 
            "insert into ShoppingCartShipment (ShoppingCartId, WarehouseId, "
           +"         ShippingTypeId, ShippingTotal, DateCreated, CreatedByUser, LastUpdateDate, PONumber, "
           +"         NeedLiftGate, LiftGatePrice ) " 
           +" values (@ShopingCartId, @WarehouseId, "
           +"         @ShippingTypeId, @ShippingTotal, @DateCreated, @CreatedByUser, @LastUpdateDate, @PONumber, "
           +"         @NeedLiftGate, @LiftGatePrice) ";

        parms.Add(new SqlParameter("@ShopingCartId", shipment.shoppingCartId));
        parms.Add(new SqlParameter("@WarehouseId", shipment.warehouseId ));
        if ( 0 == shipment.shippingTypeId ) {
            parms.Add(new SqlParameter("@ShippingTypeId",  DBNull.Value)); 
        } else { 
            parms.Add(new SqlParameter("@ShippingTypeId",  shipment.shippingTypeId)); 
        } 
        parms.Add(new SqlParameter("@ShippingTotal", shipment.shippingTotal ));
        parms.Add(new SqlParameter("@DateCreated", DateTime.Now));
        parms.Add(new SqlParameter("@CreatedByUser", shipment.createdByUser ));
        parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now));
        if ( null == shipment.PoNumber ) { 
            parms.Add(new SqlParameter("@PONumber", DBNull.Value)); 
        } else {
            parms.Add(new SqlParameter("@PONumber", shipment.PoNumber)); 
        }
        parms.Add(new SqlParameter("@NeedLiftGate", shipment.needLiftGate ));
        parms.Add(new SqlParameter("@LiftGatePrice", shipment.liftGatePrice));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        shipment.shoppingCartShipmentId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("ShopingCartShipmentSvc: created " + shipment.shoppingCartId+" "+shipment.warehouseId);

        return FindById(tran, shipment.shoppingCartShipmentId); 
    }


    private static List<ShoppingCartShipment> Select(SqlTransaction tran, int id, int shopingCartId) {
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql = "select ShoppingCartShipmentId, ShoppingCartId, WarehouseId, ShippingTypeId, "       
                   +"        ShippingTotal, PONumber,  NeedLiftGate, LiftGatePrice, "
                   + "       CreatedByUser, LastUpdateDate, DateCreated "
                   + "  from ShoppingCartShipment "
                   + " where 1=1 ";

        if ( 0 != id ) {
            sql += " and ShoppingCartShipmentId = @Id ";
            parms.Add(new SqlParameter("@Id", id));
        }
 
        if ( 0 != shopingCartId ) {
            sql += " and ShoppingCartId = @ShopingCartId ";
            parms.Add(new SqlParameter("@ShopingCartId", shopingCartId));
        }

        List<ShoppingCartShipment> result = new List<ShoppingCartShipment>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
             }
         }
         return result;
    }

    private static ShoppingCartShipment FromReader(SqlDataReader rdr) {
        ShoppingCartShipment result = new ShoppingCartShipment();
        result.shoppingCartShipmentId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartShipmentId"));
        result.shoppingCartId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartId"));
        result.warehouseId = rdr.GetInt32(rdr.GetOrdinal("WarehouseId"));
        result.shippingTypeId = rdr.IsDBNull(rdr.GetOrdinal("ShippingTypeId"))? 0 : rdr.GetInt32(rdr.GetOrdinal("ShippingTypeId"));
        result.shippingTotal = rdr.GetDecimal(rdr.GetOrdinal("ShippingTotal")); 
        result.needLiftGate = rdr.GetBoolean(rdr.GetOrdinal("NeedLiftGate"));
        result.liftGatePrice = rdr.GetDecimal(rdr.GetOrdinal("LiftGatePrice"));
        result.PoNumber = rdr.IsDBNull(rdr.GetOrdinal("PONumber")) ? null : rdr.GetString(rdr.GetOrdinal("PONumber"));
        TraceableSvc.FromReader(rdr, result);
        return result;
    }
}

}