using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Warehouse
{

public class OrderSvc 
{
    public static Order FindById(SqlTransaction tran, int id) {
        Logger.ASSERT(0 != id);
        List<Order> result = Select(tran, id, 0, null);
        if ( 0 == result.Count ) {
            string message = string.Format("No Order with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }

        FullFill(tran, result[0]);
        return result[0];
    }

    public static List<Order> GetByShoppingCartId(SqlTransaction tran, int cartId) {
        Logger.ASSERT(0 != cartId);
        List<Order> result = Select(tran, 0, cartId, null);
        foreach(Order o in result) {
            FullFill(tran, o);
        }
        return result;
    }

    public static List<Order> GetOrders(SqlTransaction tran, OrderFilter filter)
    {
        Logger.ASSERT(null != filter);
        List<Order> result = Select(tran, 0, 0, filter);
        foreach (Order o in result)
        {
            FullFill(tran, o);
        }
        return result;
    }

    public static Order CreateOrder(SqlTransaction tran, Entity.ShoppingCart cart, ShoppingCartShipment shipment)
    {
        string sql = 
            " insert into TheOrder (ShippingTypeId, OrderStatusId, WarehouseId, CustomerId,"
           +"        BrandId, PONumber, OrderDate, ShippingDate, MACPACOrderNumber, ReleaseNumber, "
           +"        TrackingNumber, RepAccountNo, Total, ShippingTotal, GrandTotal, JobsiteContactPh, "
           +"        Phone, Fax, Email, SalesPerson, MarkOrder, DeliveryRequest, MACPACXML, MACPACFileName, "
           +"        ShoppingCartShipmentId, DateCreated, CreatedByUser, LastUpdateDate, SoldName, "
           +"        SoldAddress1, SoldAddress2, SoldCity, SoldState, SoldZip, SoldCountry, ShipName, "
           +"        ShipAddress1, ShipAddress2, ShipCity, ShipState, ShipZip, ShipCountry, MarketingProgram )  "
          + " values (@ShippingTypeId, @OrderStatusId, @WarehouseId, @CustomerId,"
           +"        @BrandId, @PONumber, @OrderDate, @ShippingDate, @MACPACOrderNumber, @ReleaseNumber, "
           +"        @TrackingNumber, @RepAccountNo, @Total, @ShippingTotal, @GrandTotal, @JobsiteContactPh, "
           +"        @Phone, @Fax, @Email, @SalesPerson, @MarkOrder, @DeliveryRequest, @MACPACXML, @MACPACFileName, "
           +"        @ShopingCartShipmentId, @DateCreated, @CreatedByUser, @LastUpdateDate, @SoldName, "
           +"        @SoldAddress1, @SoldAddress2, @SoldCity, @SoldState, @SoldZip, @SoldCountry, @ShipName, "
           +"        @ShipAddress1, @ShipAddress2, @ShipCity, @ShipState, @ShipZip, @ShipCountry, @MarketingProgram ) ";

        List<SqlParameter> parms = new List<SqlParameter>();

        parms.Add(new SqlParameter("@ShippingTypeId", shipment.shippingTypeId));
        parms.Add(new SqlParameter("@OrderStatusId", OrderStatus.SUBMITTED));
        parms.Add(new SqlParameter("@WarehouseId",   shipment.warehouseId));
        parms.Add(new SqlParameter("@CustomerId", cart.customerId));
        parms.Add(new SqlParameter("@BrandId", cart.brandId));
        parms.Add(new SqlParameter("@PONumber", shipment.PoNumber));
        parms.Add(new SqlParameter("@OrderDate", DateTime.Now));
        parms.Add(new SqlParameter("@ShippingDate", DBNull.Value));
        parms.Add(new SqlParameter("@MACPACOrderNumber", DBNull.Value));
        parms.Add(new SqlParameter("@ReleaseNumber", DBNull.Value));
        parms.Add(new SqlParameter("@TrackingNumber", DBNull.Value));
        parms.Add(new SqlParameter("@RepAccountNo", cart.repAccountNo));


        parms.Add(new SqlParameter("@JobsiteContactPh", null == cart.jobsiteContactPh ? "" : cart.jobsiteContactPh));
        parms.Add(new SqlParameter("@Phone", null == cart.phone ? "": cart.phone ));
        parms.Add(new SqlParameter("@Fax", null == cart.fax ? "" : cart.fax ));
        parms.Add(new SqlParameter("@Email", null == cart.email ? "" : cart.email ));
        parms.Add(new SqlParameter("@SalesPerson", cart.salesPerson ));
        parms.Add(new SqlParameter("@MarkOrder", null == cart.markOrder ? "" : cart.markOrder ));
        string deliveryRequest = "";
        if ( shipment.needLiftGate ) {
            deliveryRequest += "Liftgate req`d.";
        }
        deliveryRequest += null == cart.deliveryRequest ? "" : cart.deliveryRequest;
        parms.Add(new SqlParameter("@DeliveryRequest", deliveryRequest ));
        parms.Add(new SqlParameter("@MACPACXML", ""));
        parms.Add(new SqlParameter("@MACPACFileName", ""));
        parms.Add(new SqlParameter("@ShopingCartShipmentId", shipment.shoppingCartShipmentId ));
        parms.Add(new SqlParameter("@DateCreated", DateTime.Now ));
        parms.Add(new SqlParameter("@CreatedByUser", cart.createdByUser ));
        parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now ));

        Customer cust = CustomerSvc.FindById(tran, cart.customerId);

        parms.Add(new SqlParameter("@SoldName", cust.address.name ));
        parms.Add(new SqlParameter("@SoldAddress1", null == cust.address.address1 ? "" : cust.address.address1));
        parms.Add(new SqlParameter("@SoldAddress2", null == cust.address.address2 ? "" : cust.address.address2));
        parms.Add(new SqlParameter("@SoldCity", cust.address.city ));
        parms.Add(new SqlParameter("@SoldState", null == cust.address.state ? "" : cust.address.state ));
        parms.Add(new SqlParameter("@SoldZip", null == cust.address.zip ? "" : cust.address.zip ));
        parms.Add(new SqlParameter("@SoldCountry", null == cust.address.country ? "USA" : cust.address.country ));

        ShippingAddress shipAddr = ShippingAddressSvc.FindById(tran, cart.shippingAddressId);

        parms.Add(new SqlParameter("@ShipName", shipAddr.name ));
        parms.Add(new SqlParameter("@ShipAddress1", null == shipAddr.address1 ? "" : shipAddr.address1 ));
        parms.Add(new SqlParameter("@ShipAddress2",  null == shipAddr.address2 ? "" : shipAddr.address2 ));
        parms.Add(new SqlParameter("@ShipCity", null == shipAddr.city ? "" : shipAddr.city ));
        parms.Add(new SqlParameter("@ShipState", null == shipAddr.state ? "" : shipAddr.state ));
        parms.Add(new SqlParameter("@ShipZip", null == shipAddr.zip ? "" : shipAddr.zip ));
        parms.Add(new SqlParameter("@ShipCountry", null == shipAddr.country ? "USA" : shipAddr.country ));
        String mp = ShoppingCartSvc.GetMarketingProgram(tran, shipment.shoppingCartShipmentId, cart.customerId);
        if ( null == mp ) {
            parms.Add(new SqlParameter("@MarketingProgram", DBNull.Value));
        } else {
            parms.Add(new SqlParameter("@MarketingProgram", mp));
        }

        int lineNumber = 1;
        Decimal total = 0m;
        List<OrderDetail> ods = new List<OrderDetail>();
        foreach( ShoppingCartDetail detail in shipment.details ) {
            OrderDetail od = new OrderDetail();
            od.itemId = detail.modelItem.itemId;
            od.qty = detail.qtyOrdered;
            od.sku = detail.modelItem.item.sku;
            CustomerPrice price = CustomerPriceSvc.FindByKey(tran, cart.customerId, detail.modelItemId);
            od.multiplier = null == price ? 1 : price.multiplier;
            ModelItem mi = ModelItemSvc.FindById(tran, detail.modelItemId);
            od.price = mi.price;
            od.cost = od.qty * Decimal.Round(Decimal.Multiply(od.price, new Decimal(od.multiplier)), 2);
            od.shopingCartDetailId = detail.shoppingCartDetailId; 
            od.lineNumber = lineNumber++;
            total += od.cost;
            ods.Add(od);
        }

        parms.Add(new SqlParameter("@Total", total));
        parms.Add(new SqlParameter("@ShippingTotal", shipment.shippingTotal));
        parms.Add(new SqlParameter("@GrandTotal", total + shipment.shippingTotal));

        int orderId = 0;
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        orderId = SQLHelper.GetIdentity(tran);
        foreach( OrderDetail od in ods) {
            od.orderId = orderId;
            OrderDatailSvc.Insert(tran, od);
        }

        return FindById(tran, orderId);
    } 

    public static string MakeMACPACXML(SqlTransaction tran, Order order) {
        MACPACOrderExporter exporter = new MACPACOrderExporter();
        XmlDocument doc = exporter.ExportOrder(tran, order);
        string filename = ConfigurationManager.AppSettings["macpacdir"]+"\\"+order.MACPACFileName;
        using ( XmlWriter writer = new XmlTextWriter(filename, Encoding.Default ) ) {
            doc.WriteTo(writer);
        }
        string copyFileName = ConfigurationManager.AppSettings["macpaccopydir"]+"\\"+order.MACPACFileName;
        using ( XmlWriter writer = new XmlTextWriter(copyFileName, Encoding.Default ) ) {
            doc.WriteTo(writer);
        }

        order.MACPACXML = doc.OuterXml;
        OrderSvc.StoreMACPACXML(tran, order.orderId, order.MACPACXML, order.MACPACFileName);
        return filename;
    }

    public static int GetOrderedQty(SqlTransaction tran, int itemId, int warehuseId) {
        string sql = " select coalesce(sum(od.Qty), 0) qtyAllocated"
                    +"   from OrderDetail od "
                    +"        inner join TheOrder o on od.OrderId = o.OrderId and o.OrderStatusId not in (2, -1) "
                    +"  where od.ItemId = @ItemId "
                    +"    and o.WarehouseId = @WarehouseId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ItemId", itemId));
        parms.Add(new SqlParameter("@WarehouseId", warehuseId));

        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             if( rdr.Read() ) {
                 return rdr.GetInt32(rdr.GetOrdinal("qtyAllocated"));
             } else {
                 return 0;
             }
        }

    }

    public static void MakOrderClosed(SqlTransaction tran, int orderId, string macpacorderid, 
                                      DateTime shippingDate, string release, string tracenumber ) 
    {
        string sql = 
            " update TheOrder    "
           +"    set ShippingDate = @ShippingDate , " 
           +"        MACPACOrderNumber = @MACPACOrderNumber ,"
           +"        ReleaseNumber = @ReleaseNumber , "
           +"        TrackingNumber = @TrackingNumber ,  "
           +"        OrderStatusId = @OrderStatus "
           +"  where OrderId = @OrderId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add( new SqlParameter("@ShippingDate", shippingDate));
        parms.Add( new SqlParameter("@MACPACOrderNumber", macpacorderid));
        parms.Add( new SqlParameter("@ReleaseNumber", release));
        parms.Add( new SqlParameter("@TrackingNumber", tracenumber));
        parms.Add( new SqlParameter("@OrderStatus", OrderStatus.CONFIRMED));
        parms.Add( new SqlParameter("@OrderId", orderId));
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        parms.Clear();
        sql = " insert into OrderStatusLog (OrderId, NewStatus, OldStatus, ChangeDate, CreatedByUser, "
             +"              LastUpdateDate, DateCreated ) "
             +" values ( @OrderId, 2, 1, getdate(), 'OrderManager', "
             +"              getdate(), getdate() )";
        parms.Add( new SqlParameter("@OrderId", orderId));
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
    }

    private static void StoreMACPACXML(SqlTransaction tran, int orderId, string xml, string filename) {
        Logger.ASSERT(0 != orderId);
        string sql = " update TheOrder "
                    +"    set MACPACXML = @MACPACXML, "
                    +"        MACPACFileName = @MACPACFileName "
                    +"  where OrderId = @OrderId";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@MACPACXML", xml)); 
        parms.Add( new SqlParameter("@OrderId", orderId));
        parms.Add( new SqlParameter("@MACPACFileName", filename));
        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
    }

    private static void FullFill(SqlTransaction tran, Order order) {
        order.details = OrderDatailSvc.GetByOrderId(tran, order.orderId);
        order.warehouse = WarehouseSvc.FindById(tran, order.warehouseId);
        order.shippingType = ShippingTypeSvc.FindById(tran, order.shippingTypeId);
        order.customer = CustomerSvc.FindById(tran, order.customerId);
    }

    private static List<Order> Select(SqlTransaction tran, int id, int cartId, OrderFilter filter) {
        string sql = 
            " select OrderId, ShippingTypeId, OrderStatusId, WarehouseId, CustomerId, BrandId, "
            + "       PONumber, OrderDate, ShippingDate, MACPACOrderNumber, ReleaseNumber, TrackingNumber, "
            + "       RepAccountNo, Total, ShippingTotal, GrandTotal, JobsiteContactPh, Phone, Fax, Email, "
            + "       SalesPerson, MarkOrder, DeliveryRequest, MACPACXML, MACPACFileName, ShoppingCartShipmentId, "
            + "       DateCreated, CreatedByUser, LastUpdateDate, SoldName, SoldAddress1, SoldAddress2, SoldCity, "
            + "       SoldState, SoldZip, SoldCountry, ShipName, ShipAddress1, ShipAddress2, ShipCity, ShipState, "
            + "       ShipZip, ShipCountry, MarketingProgram, "
            + "       (select OrderStatus.Status from OrderStatus where OrderStatus.OrderStatusId = TheOrder.OrderStatusId) as OrderStatusStr "
            + "  from TheOrder " 
            + " where 1=1 ";

        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != id ) {
            sql += " and OrderId = @OrderId ";
            parms.Add(new SqlParameter("@OrderId", id));
        }
        if ( 0 != cartId ) {
            sql += " and   ShoppingCartShipmentId in ( select ShoppingCartShipmentId "
                                                    +"  from ShoppingCartShipment "
                                                    +" where ShoppingCartId = @CartId  )";
            parms.Add(new SqlParameter("@CartId", cartId));
        }

        int count = Int32.MaxValue;
        if (null != filter)
        {
            if ( null != filter.createdBy && !"".Equals(filter.createdBy) ) {
                sql += " and  CreatedByUser = @CreatedBy ";
                parms.Add(new SqlParameter("@CreatedBy", filter.createdBy));
            }
            if ( 0 != filter.statusId ) {
                sql += " and  OrderStatusId = @OrderStatusId ";
                parms.Add(new SqlParameter("@OrderStatusId", filter.statusId));
            }
            if ( 0 != filter.customerId ) {
                sql += " and  CustomerId = @CustomerId ";
                parms.Add(new SqlParameter("@CustomerId", filter.customerId));
            }
            if ( null != filter.poNumber && "" != filter.poNumber ) {
                sql += " and  PONumber LIKE @PONumber ";
                parms.Add(new SqlParameter("@PONumber", "%" + filter.poNumber + "%"));
            }
            if ( null != filter.poNumberStrong && !"".Equals(filter.poNumberStrong) ) {
                sql += " and  PONumber = @PONumberStrong ";
                parms.Add(new SqlParameter("@PONumberStrong", filter.poNumberStrong));
            }
            if ( null != filter.confirmNumber && "" != filter.confirmNumber ) {
                sql += " and  OrderId  = @OrderId ";
                parms.Add(new SqlParameter("@OrderId", filter.confirmNumber));
            }
            if ( DateTime.MinValue != filter.fromDate ) {
                sql += " and  OrderDate >= @FromDate ";
                parms.Add(new SqlParameter("@FromDate", filter.fromDate));
            }
            if ( DateTime.MinValue != filter.toDate ) {
                sql += " and  OrderDate <= @ToDate ";
                parms.Add(new SqlParameter("@ToDate", filter.toDate));
            }
            if ( 0 != filter.quantity ) {
                count = filter.quantity;
            }
        }
        
        sql += " order by OrderDate desc ";

        List<Order> result = new List<Order>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() && count > result.Count) {
                 result.Add(FromReader(rdr));
             }
        }
        return result;
    }

    private static Order FromReader(SqlDataReader rdr) {
        Order order = new Order();
        order.orderId = rdr.GetInt32(rdr.GetOrdinal("OrderId")); 
        order.shippingTypeId = rdr.GetInt32(rdr.GetOrdinal("ShippingTypeId")); 
        order.orderStatusId = rdr.GetInt32(rdr.GetOrdinal("OrderStatusId")); 
        order.warehouseId = rdr.GetInt32(rdr.GetOrdinal("WarehouseId")); 
        order.customerId = rdr.GetInt32(rdr.GetOrdinal("CustomerId")); 
        order.brandId = rdr.GetInt32(rdr.GetOrdinal("BrandId")); 
        order.PONumber = rdr.GetString(rdr.GetOrdinal("PONumber")); 
        order.orderDate = rdr.GetDateTime(rdr.GetOrdinal("OrderDate")); 
        order.shippingDate = rdr.IsDBNull(rdr.GetOrdinal("ShippingDate")) ?  DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("ShippingDate"));  //

        order.orderDateStr = order.orderDate.ToString();
        if ( !rdr.IsDBNull(rdr.GetOrdinal("ShippingDate")) ) {
            order.shippingDateStr =  order.shippingDate.ToString();
        }

        order.MACPACOrderNumber = rdr.IsDBNull(rdr.GetOrdinal("MACPACOrderNumber")) ? null : rdr.GetString(rdr.GetOrdinal("MACPACOrderNumber")); //
        order.releaseNumber = rdr.IsDBNull(rdr.GetOrdinal("ReleaseNumber"))? null : rdr.GetString(rdr.GetOrdinal("ReleaseNumber"));  // 
        order.trackingNumber = rdr.IsDBNull(rdr.GetOrdinal("TrackingNumber"))? null : rdr.GetString(rdr.GetOrdinal("TrackingNumber"));  //
        order.repAccountNo = rdr.GetString(rdr.GetOrdinal("RepAccountNo")); 
        order.total = rdr.GetDecimal(rdr.GetOrdinal("Total")); 
        order.shippingTotal = rdr.GetDecimal(rdr.GetOrdinal("ShippingTotal")); 
        order.grandTotal = rdr.GetDecimal(rdr.GetOrdinal("GrandTotal")); 
        order.jobsiteContactPh = rdr.GetString(rdr.GetOrdinal("JobsiteContactPh")); 
        order.phone = rdr.GetString(rdr.GetOrdinal("Phone")); 
        order.fax = rdr.GetString(rdr.GetOrdinal("Fax")); 
        order.email = rdr.GetString(rdr.GetOrdinal("Email")); 
        order.salesPerson = rdr.GetString(rdr.GetOrdinal("SalesPerson")); 
        order.markOrder = rdr.GetString(rdr.GetOrdinal("MarkOrder")); 
        order.deliveryRequest = rdr.GetString(rdr.GetOrdinal("DeliveryRequest")); 
        order.MACPACXML = rdr.GetString(rdr.GetOrdinal("MACPACXML")); 
        order.MACPACFileName = rdr.GetString(rdr.GetOrdinal("MACPACFileName")); 
        order.shopingCartShipmentId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartShipmentId")); 
        order.soldName = rdr.GetString(rdr.GetOrdinal("SoldName")); 
        order.soldAddress1 = rdr.GetString(rdr.GetOrdinal("SoldAddress1")); 
        order.soldAddress2 = rdr.GetString(rdr.GetOrdinal("SoldAddress2")); 
        order.soldCity = rdr.GetString(rdr.GetOrdinal("SoldCity")); 
        order.soldState = rdr.GetString(rdr.GetOrdinal("SoldState")); 
        order.soldZip = rdr.GetString(rdr.GetOrdinal("SoldZip")); 
        order.soldCountry = rdr.GetString(rdr.GetOrdinal("SoldCountry")); 
        order.shipName = rdr.GetString(rdr.GetOrdinal("ShipName")); 
        order.shipAddress1 = rdr.GetString(rdr.GetOrdinal("ShipAddress1")); 
        order.shipAddress2 = rdr.GetString(rdr.GetOrdinal("ShipAddress2")); 
        order.shipCity = rdr.GetString(rdr.GetOrdinal("ShipCity")); 
        order.shipState = rdr.GetString(rdr.GetOrdinal("ShipState")); 
        order.shipZip = rdr.GetString(rdr.GetOrdinal("ShipZip")); 
        order.shipCountry = rdr.GetString(rdr.GetOrdinal("ShipCountry")); 
        order.marketingProgram = rdr.IsDBNull(rdr.GetOrdinal("MarketingProgram")) ? null : rdr.GetString(rdr.GetOrdinal("MarketingProgram"));
        order.orderStatusStr = rdr.IsDBNull(rdr.GetOrdinal("OrderStatusStr")) ? null : rdr.GetString(rdr.GetOrdinal("OrderStatusStr"));
        TraceableSvc.FromReader(rdr, order);
        return order;
    }

}

}
