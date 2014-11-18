using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;


namespace AerSysCo.Warehouse
{
public class OrderDatailSvc
{
    public static List<OrderDetail> GetByOrderId(SqlTransaction tran, int orderId) {
        Logger.ASSERT(0 != orderId);
        List<OrderDetail> result = Select(tran, orderId);
        foreach (OrderDetail o in result)
        {
            o.item = ItemSvc.FindById(tran, o.itemId);
            o.shoppingCartDetail = ShoppingCartDetailSvc.GetById(tran, o.shopingCartDetailId);
        }
        return result;
    }

    public static OrderDetail Insert(SqlTransaction tran, OrderDetail detail) {
        string sql = 
            " insert into OrderDetail (OrderId, ItemId, Qty, Price, SKU, Multiplier, Cost, ShoppingCartDetailId, LineNumber ) "
           +" values (@OrderId, @ItemId, @Qty, @Price, @SKU, @Multiplier, @Cost, @ShopingCartDetailId, @LineNumber )";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@OrderId", detail.orderId));
        parms.Add(new SqlParameter("@ItemId", detail.itemId));
        parms.Add(new SqlParameter("@Qty", detail.qty));
        parms.Add(new SqlParameter("@Price", detail.price));
        parms.Add(new SqlParameter("@SKU", detail.sku));
        parms.Add(new SqlParameter("@Multiplier", detail.multiplier));
        parms.Add(new SqlParameter("@Cost", detail.cost));
        parms.Add(new SqlParameter("@ShopingCartDetailId", detail.shopingCartDetailId));
        parms.Add(new SqlParameter("@LineNumber", detail.lineNumber));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        detail.orderDetailId = SQLHelper.GetIdentity(tran);
        return detail;
    }

    private static List<OrderDetail> Select(SqlTransaction tran, int orderId) {
        string sql = " select OrderDetailId, OrderId, ItemId, Qty, Price, SKU, Multiplier, Cost, ShoppingCartDetailId, LineNumber  "
                   + "   from OrderDetail "
                   + "  where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != orderId ) {
            sql += "  and OrderId = @OrderId ";
            parms.Add(new SqlParameter("@OrderId", orderId));
        }
        List<OrderDetail> result = new List<OrderDetail>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
             }
        }
        return result;

    }

    private static OrderDetail FromReader(SqlDataReader rdr) {
        OrderDetail result = new OrderDetail();
        result.orderDetailId = rdr.GetInt32(rdr.GetOrdinal("OrderDetailId"));
        result.orderId = rdr.GetInt32(rdr.GetOrdinal("OrderId"));
        result.itemId = rdr.GetInt32(rdr.GetOrdinal("ItemId"));
        result.qty = rdr.GetInt32(rdr.GetOrdinal("Qty"));
        result.price = rdr.GetDecimal(rdr.GetOrdinal("Price"));
        result.sku = rdr.GetString(rdr.GetOrdinal("SKU"));
        result.multiplier = rdr.GetDouble(rdr.GetOrdinal("Multiplier"));
        result.cost = rdr.GetDecimal(rdr.GetOrdinal("Cost"));
        result.shopingCartDetailId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartDetailId"));
        result.lineNumber = rdr.GetInt32(rdr.GetOrdinal("LineNumber"));
        return result;
    }
}
}
