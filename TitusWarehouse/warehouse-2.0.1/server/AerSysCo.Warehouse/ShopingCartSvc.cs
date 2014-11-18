using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Configuration;
using Fop.Net;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Warehouse
{
public class ShoppingCartSvc
{
    enum CartStatus { ANY, ACTIVE, PASSIVE };

    public static Entity.ShoppingCart FindById(SqlTransaction tran, int id) {
        Logger.ASSERT(0 != id);
        List<Entity.ShoppingCart> result = Select(tran, id, 0, 0, null, CartStatus.ANY);
        if ( 0 == result.Count ) {
            string message = string.Format("No Shoping Cart with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);

        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} ShopingCart with id {1} ", result.Count, id);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return FullFill(tran, result[0]);
    }

    public static Entity.ShoppingCart FindByCustomer(SqlTransaction tran, int customerId, int brandId, string userid) {
        Logger.ASSERT(0 != customerId);
        Logger.ASSERT(0 != brandId);
        Logger.ASSERT( null != userid );
        List<Entity.ShoppingCart> result = Select(tran, 0, customerId, brandId, userid, CartStatus.ACTIVE);
        if ( 0 == result.Count ) {
            return null;
        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} ShopingCart with customerId {1} BrandId {2}", result.Count, customerId, brandId);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return FullFill(tran, result[0]);
    }

    public static Entity.ShoppingCart FullFill(SqlTransaction tran, Entity.ShoppingCart cart) {
        cart.shipments = ShoppingCartShipmentSvc.GetByShopingCartId(tran, cart.shoppingCartId);
        foreach (ShoppingCartShipment shipment in cart.shipments) {
            ShoppingCartShipmentSvc.FullFill(tran, shipment);
        }
        if ( 0 != cart.shippingAddressId ) {
            cart.shippingAddress = ShippingAddressSvc.FindById(tran, cart.shippingAddressId);
        } else {
            cart.shippingAddress = null;
        }
        cart.customer = CustomerSvc.FindById(tran, cart.customerId);
        return cart;
    }

    public static Entity.ShoppingCart Insert(SqlTransaction tran, Entity.ShoppingCart cart) {
        string sql = 
            " insert into ShoppingCart (IsActive, CustomerId, OrderDate, RepAccountNo, ShippingAddressId, "
          + "        BrandId, IPAddress, Total, ShippingTotalAllWarehouses, GrandTotal, "
          + "        Phone, Fax, Email, SalesPerson, JobsiteContactPh, MarkOrder, DeliveryRequest, "
          + "        DateCreated, CreatedByUser, LastUpdateDate, AcknFileName ) "
          + " values (@IsActive, @CustomerId, @OrderDate, @RepAccountNo, @ShippingAddressId, "
          + "        @BrandId, @IPAddress, @Total, @ShippingTotalAllWarehouses, @GrandTotal, "
          + "        @Phone, @Fax, @Email, @SalesPerson, @JobsiteContactPh, @MarkOrder, @DeliveryRequest, "
          + "        @DateCreated, @CreatedByUser, @LastUpdateDate, @AcknFileName) ";

         List<SqlParameter> parms = new List<SqlParameter>();

         parms.Add(new SqlParameter("@IsActive", true));
         parms.Add(new SqlParameter("@CustomerId", cart.customerId)); 
         parms.Add(new SqlParameter("@OrderDate", DateTime.MinValue == cart.orderDate ? DateTime.Now : cart.orderDate ));
         parms.Add(new SqlParameter("@RepAccountNo", cart.repAccountNo ));
         if ( 0 == cart.shippingAddressId ) { 
             parms.Add(new SqlParameter("@ShippingAddressId", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@ShippingAddressId", cart.shippingAddressId)); 
         }
         parms.Add(new SqlParameter("@BrandId", cart.brandId));
         parms.Add(new SqlParameter("@IPAddress" , cart.ipAddress));
         parms.Add(new SqlParameter("@Total", cart.total));
         parms.Add(new SqlParameter("@ShippingTotalAllWarehouses", cart.shippingTotalAllWarehouses));
         parms.Add(new SqlParameter("@GrandTotal", cart.grandTotal));
         if ( null == cart.phone ) {
             parms.Add(new SqlParameter("@Phone", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@Phone", cart.phone)); 
         }
         if ( null == cart.fax ) {
             parms.Add(new SqlParameter("@Fax", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@Fax", cart.fax)); 
         }
         if ( null == cart.email ) {
             parms.Add(new SqlParameter("@Email", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@Email", cart.email)); 
         }
         if ( null == cart.salesPerson ) {
             parms.Add(new SqlParameter("@SalesPerson", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@SalesPerson", cart.salesPerson)); 
         }
         if ( null == cart.jobsiteContactPh ) {
             parms.Add(new SqlParameter("@JobsiteContactPh", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@JobsiteContactPh", cart.jobsiteContactPh)); 
         }
         if ( null == cart.markOrder ) {
             parms.Add(new SqlParameter("@MarkOrder", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@MarkOrder", cart.markOrder)); 
         }
         if ( null == cart.deliveryRequest ) {
             parms.Add(new SqlParameter("@DeliveryRequest", DBNull.Value));
         } else {
             parms.Add(new SqlParameter("@DeliveryRequest", cart.deliveryRequest));
         }
         parms.Add(new SqlParameter("@DateCreated", DateTime.Now));
         parms.Add(new SqlParameter("@CreatedByUser", cart.createdByUser));
         parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now));
         parms.Add(new SqlParameter("@AcknFileName", Guid.NewGuid().ToString()));

         SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
         cart.shoppingCartId = SQLHelper.GetIdentity(tran);

         return FindById(tran, cart.shoppingCartId);
    }

    public static int UpdateVersion(SqlTransaction tran, int cartId, int version) {
        string sql = 
            " update ShoppingCart "
            +"    set version = version + 1 "
            +"  where ShoppingCartId = @Id "
            +"    and IsActive = 1 "
            +"    and version = @version" ;

        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@Id", cartId));
        parms.Add(new SqlParameter("@version", version));
        int count = SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        if ( 0 == count ) {
            throw new VersionNotFoundException();
        }
        parms.Clear();
        sql = " select version from ShoppingCart where ShoppingCartId = @Id ";
        parms.Add(new SqlParameter("@Id", cartId));
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            if ( rdr.Read() ) {
                return rdr.GetInt32(rdr.GetOrdinal("version"));
            } else {
                throw new VersionNotFoundException();
            }
        }
    }

    public static Entity.ShoppingCart Update(SqlTransaction tran, Entity.ShoppingCart cart) {
        string sql = 
            "update ShoppingCart "
          + "   set  IsActive = @IsActive , "
          + "        OrderDate = @OrderDate, "
          + "        RepAccountNo = @RepAccountNo, "
          + "        ShippingAddressId = @ShippingAddressId , "
          + "        IPAddress = @IPAddress, "
          + "        Total = @Total, " 
          + "        ShippingTotalAllWarehouses = @ShippingTotalAllWarehouses, "
          + "        GrandTotal = @GrandTotal, "
          + "        Phone = @Phone, "
          + "        Fax = @Fax, "
          + "        Email = @Email, "
          + "        SalesPerson = @SalesPerson, "
          + "        JobsiteContactPh = @JobsiteContactPh, "
          + "        MarkOrder = @MarkOrder," 
          + "        DeliveryRequest = @DeliveryRequest,  "
          + "        LastUpdateDate = @LastUpdateDate, "
          + "        version = version + 1 "
          + "  where ShoppingCartId = @Id "
          + "    and version = @version" ;

         List<SqlParameter> parms = new List<SqlParameter>();

         parms.Add(new SqlParameter("@IsActive", cart.isActive));
         parms.Add(new SqlParameter("@OrderDate", DateTime.MinValue == cart.orderDate ? DateTime.Now : cart.orderDate ));
         parms.Add(new SqlParameter("@RepAccountNo", cart.repAccountNo ));
         if ( 0 == cart.shippingAddressId ) { 
             parms.Add(new SqlParameter("@ShippingAddressId", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@ShippingAddressId", cart.shippingAddressId)); 
         }
         parms.Add(new SqlParameter("@IPAddress" , cart.ipAddress));
         parms.Add(new SqlParameter("@Total", cart.total));
         parms.Add(new SqlParameter("@ShippingTotalAllWarehouses", cart.shippingTotalAllWarehouses));
         parms.Add(new SqlParameter("@GrandTotal", cart.grandTotal));
         if ( null == cart.phone ) {
             parms.Add(new SqlParameter("@Phone", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@Phone", cart.phone)); 
         }
         if ( null == cart.fax ) {
             parms.Add(new SqlParameter("@Fax", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@Fax", cart.fax)); 
         }
         if ( null == cart.email ) {
             parms.Add(new SqlParameter("@Email", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@Email", cart.email)); 
         }
         if ( null == cart.salesPerson ) {
             parms.Add(new SqlParameter("@SalesPerson", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@SalesPerson", cart.salesPerson)); 
         }
         if ( null == cart.jobsiteContactPh ) {
             parms.Add(new SqlParameter("@JobsiteContactPh", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@JobsiteContactPh", cart.jobsiteContactPh)); 
         }
         if ( null == cart.markOrder ) {
             parms.Add(new SqlParameter("@MarkOrder", DBNull.Value)); 
         } else {
             parms.Add(new SqlParameter("@MarkOrder", cart.markOrder)); 
         }
         if ( null == cart.deliveryRequest ) {
             parms.Add(new SqlParameter("@DeliveryRequest", DBNull.Value));
         } else {
             parms.Add(new SqlParameter("@DeliveryRequest", cart.deliveryRequest));
         }
         parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now));
         parms.Add(new SqlParameter("@Id", cart.shoppingCartId));
         parms.Add(new SqlParameter("@version", cart.version));


         int count = SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
         if ( 0 == count ) {
             throw new VersionNotFoundException();
         }

         return FindById(tran, cart.shoppingCartId);

    }

    public static string GetAcknowFile(SqlTransaction tran, int id) {
        Entity.ShoppingCart cart = ShoppingCartSvc.FindById(tran, id);
        Brand b = BrandSvc.FindById(tran, cart.brandId);
        return  ConfigurationManager.AppSettings[b.brandName.ToLower()+"_urlPrefix"]+"/"+cart.acknFileName+".pdf";
    }

    public static void MakeAcknow(SqlTransaction tran, int id) {
        Entity.ShoppingCart cart = ShoppingCartSvc.FindById(tran, id);
        Brand brand = BrandSvc.FindById(tran, cart.brandId);

        XmlDocument doc = new XmlDocument();
        XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", Encoding.UTF8.WebName, "yes");
        XmlElement root = doc.CreateElement("acknowledgement");
        doc.AppendChild(root);

        XmlElement brnd = doc.CreateElement("brand"); //>TTS</brand>
        root.AppendChild(brnd); 
        brnd.AppendChild( doc.CreateCDataSection(brand.brandName));

        XmlElement dat = doc.CreateElement("date"); // >02/03/2008 14:12:25 CST</date>
        root.AppendChild(dat); 
        dat.AppendChild( doc.CreateCDataSection( cart.orderDate.ToString()));

        XmlElement customer = doc.CreateElement("customer"); // >1234342</customer>
        root.AppendChild(customer); 
        customer.AppendChild( doc.CreateCDataSection( CustomerSvc.GetShortAccountNo(cart.customer.MACPACCustonerNumber)));

        XmlElement orderedBy = doc.CreateElement("ordered-by"); // >Jane Doe</ordered-by>
        root.AppendChild(orderedBy);
        orderedBy.AppendChild( doc.CreateCDataSection( cart.salesPerson ));

        XmlElement grandTotal = doc.CreateElement("grand-total"); // >12478.23</grand-total>
        root.AppendChild(grandTotal);
        grandTotal.AppendChild( doc.CreateCDataSection( cart.grandTotal.ToString("C")));

        XmlElement soldTo = doc.CreateElement("sold-to"); 
        root.AppendChild(soldTo); {

            XmlElement name = doc.CreateElement("name" ); // >Archeton</name>
            soldTo.AppendChild(name);
            name.AppendChild( doc.CreateCDataSection(cart.customer.address.name));

            XmlElement address1 = doc.CreateElement("address1"); // 123 Main St</address1>
            soldTo.AppendChild(address1);
            address1.AppendChild( doc.CreateCDataSection(cart.customer.address.address1));

            XmlElement address2 = doc.CreateElement("address2");// <address2></address2>
            soldTo.AppendChild(address2);
            address2.AppendChild( doc.CreateCDataSection(cart.customer.address.address2));

            XmlElement city = doc.CreateElement("city"); // Dallas</city>
            soldTo.AppendChild(city);
            city.AppendChild( doc.CreateCDataSection(cart.customer.address.city));

            XmlElement state = doc.CreateElement("state"); // >TX</state>
            soldTo.AppendChild(state);
            state.AppendChild( doc.CreateCDataSection( cart.customer.address.state ));

            XmlElement zip = doc.CreateElement("zip"); // >75218</zip>
            soldTo.AppendChild(zip);
            zip.AppendChild( doc.CreateCDataSection(cart.customer.address.zip));
        }
        XmlElement shipTo = doc.CreateElement("ship-to"); 
        root.AppendChild(shipTo); {
            XmlElement name = doc.CreateElement("name" ); // >Archeton</name>
            shipTo.AppendChild(name);
            name.AppendChild( doc.CreateCDataSection(cart.shippingAddress.name));

            XmlElement address1 = doc.CreateElement("address1"); // 123 Main St</address1>
            shipTo.AppendChild(address1);
            address1.AppendChild( doc.CreateCDataSection( cart.shippingAddress.address1 ));

            XmlElement address2 = doc.CreateElement("address2");// <address2></address2>
            shipTo.AppendChild(address2);
            address2.AppendChild( doc.CreateCDataSection(cart.shippingAddress.address2));

            XmlElement city = doc.CreateElement("city"); // Dallas</city>
            shipTo.AppendChild(city);
            city.AppendChild( doc.CreateCDataSection(cart.shippingAddress.city));

            XmlElement state = doc.CreateElement("state"); // >TX</state>
            shipTo.AppendChild(state);
            state.AppendChild( doc.CreateCDataSection(cart.shippingAddress.state));

            XmlElement zip = doc.CreateElement("zip"); // >75218</zip>
            shipTo.AppendChild(zip);
            zip.AppendChild( doc.CreateCDataSection(cart.shippingAddress.zip));

            XmlElement country = doc.CreateElement("country"); 
            shipTo.AppendChild(country);
            country.AppendChild( doc.CreateCDataSection("US".Equals(cart.shippingAddress.country.ToUpper()) ? "USA" : cart.shippingAddress.country.ToUpper() ));
        }
        if (   null != cart.markOrder 
            || null != cart.jobsiteContactPh 
            || null != cart.deliveryRequest ) 
        {
            XmlElement shipping = doc.CreateElement("shipping");
            root.AppendChild(shipping);
            if ( null != cart.markOrder && !"".Equals(cart.markOrder) ) {
                XmlElement markOrder = doc.CreateElement("mark-order"); // ></mark-order>
                shipping.AppendChild(markOrder);
                markOrder.AppendChild( doc.CreateCDataSection(cart.markOrder));
            }
            if ( null != cart.jobsiteContactPh && !"".Equals(cart.jobsiteContactPh) ) {
                XmlElement jobSiteContact = doc.CreateElement("jobsite-contact"); // ></jobsite-contact>
                shipping.AppendChild(jobSiteContact);
                jobSiteContact.AppendChild( doc.CreateCDataSection(cart.jobsiteContactPh));
            }
            if ( null != cart.deliveryRequest && !"".Equals(cart.deliveryRequest) ) {
                XmlElement deliveryRequest = doc.CreateElement("delivery-request"); // ></delivery-request>
                shipping.AppendChild(deliveryRequest);
                deliveryRequest.AppendChild( doc.CreateCDataSection(cart.deliveryRequest));
            }
        }

        XmlElement os = doc.CreateElement("orders");
        root.AppendChild(os);

        List<Order> orders = OrderSvc.GetByShoppingCartId(tran, cart.shoppingCartId);
        foreach ( Order order in orders ) {
            ShoppingCartShipment cartShip = null;
            foreach (ShoppingCartShipment csh in cart.shipments) {
                if ( csh.shoppingCartShipmentId == order.shopingCartShipmentId ) {
                    cartShip = csh;
                    break; 
                }
            }
            Logger.ASSERT( null != cartShip );

            XmlElement o = doc.CreateElement("order");
            os.AppendChild(o);
            XmlElement oid = doc.CreateElement("id"); o.AppendChild(oid);// >12312323</id>
            oid.AppendChild( doc.CreateCDataSection(order.orderId.ToString()));

            XmlElement ponumber = doc.CreateElement("ponumber"); o.AppendChild(ponumber); //>123456789012345678901234</ponumber>
            ponumber.AppendChild( doc.CreateCDataSection(order.PONumber));

            XmlElement warehouse = doc.CreateElement("warehouse"); o.AppendChild(warehouse); //>ASC/TRB</warehouse>
            warehouse.AppendChild( doc.CreateCDataSection(order.warehouse.warehouseName));

            XmlElement shipVia = doc.CreateElement("ship-via"); o.AppendChild(shipVia); // >FedEx 2 dais</ship-via>
            shipVia.AppendChild( doc.CreateCDataSection(order.shippingType.shippingType));

            XmlElement shipTotal = doc.CreateElement("ship-total"); o.AppendChild(shipTotal); //>1245.23</ship-total>
            if ( cartShip.needLiftGate ) { 
                shipTotal.AppendChild( doc.CreateCDataSection((order.shippingTotal - cartShip.liftGatePrice).ToString("C")));

                XmlElement lgCost = doc.CreateElement("liftgate-cost"); o.AppendChild(lgCost); 
                lgCost.AppendChild( doc.CreateCDataSection(cartShip.liftGatePrice.ToString("C")));
            } else {
                shipTotal.AppendChild( doc.CreateCDataSection(order.shippingTotal.ToString("C")));
            }

            XmlElement totalCost = doc.CreateElement("total-cost"); o.AppendChild(totalCost); // >total-cost</total-cost>
            totalCost.AppendChild( doc.CreateCDataSection(order.total.ToString("C")));

            XmlElement gt = doc.CreateElement("grand-total"); o.AppendChild(gt); // >grand-total</grand-total>
            gt.AppendChild( doc.CreateCDataSection(order.grandTotal.ToString("C")));

            XmlElement lines = doc.CreateElement("lines"); o.AppendChild(lines);
            lines.AppendChild( doc.CreateCDataSection((order.details.Count + 1).ToString()));

            foreach( OrderDetail detail in order.details ) {
                XmlElement line = doc.CreateElement("line"); lines.AppendChild(line);
                XmlElement no = doc.CreateElement("no"); line.AppendChild(no); // >001</no>
                no.AppendChild( doc.CreateCDataSection(detail.lineNumber.ToString()));

                XmlElement sku = doc.CreateElement("sku"); line.AppendChild(sku); //>3001</sku>
                sku.AppendChild( doc.CreateCDataSection(detail.sku.ToString()));

                ModelItem mi = ModelItemSvc.FindByItemAndBrand(tran, order.brandId, detail.itemId);
                Model mo = ModelSvc.FindById(tran, mi.modelId);
                XmlElement model = doc.CreateElement("model"); line.AppendChild(model); // >350RLF1</model>
                model.AppendChild( doc.CreateCDataSection(mo.modelName));

                XmlElement desc = doc.CreateElement("description"); line.AppendChild(desc); //>AL SUP GRIL, 24X12 SRF MT</description>
                Item i= ItemSvc.FindById(tran, mi.itemId);
                desc.AppendChild( doc.CreateCDataSection(i.description));

                XmlElement qty = doc.CreateElement("qty"); line.AppendChild(qty); //>25</qty>
                qty.AppendChild( doc.CreateCDataSection(detail.qty.ToString()));

                XmlElement price = doc.CreateElement("price"); line.AppendChild(price); //>124.14</price>
                price.AppendChild( doc.CreateCDataSection( (Decimal.ToDouble(detail.price) * detail.multiplier).ToString("C")));

                XmlElement cost = doc.CreateElement("cost"); line.AppendChild(cost); //>3103.05</cost>
                cost.AppendChild( doc.CreateCDataSection(detail.cost.ToString("C")));
            }
        }



        string xmlfilename = ConfigurationManager.AppSettings["acknowledgement"]+"\\"+cart.acknFileName+".xml";
        string pdffilename = ConfigurationManager.AppSettings["acknowledgement"]+"\\"+cart.acknFileName+".pdf";
        string xslfilename = ConfigurationManager.AppSettings["acknowledgement"]+"\\"+brand.brandName+"-acknowledgement.xsl";

        using ( XmlWriter writer = new XmlTextWriter(xmlfilename, Encoding.UTF8 ) ) {
            doc.WriteTo(writer);
        }

        NFop.Create_PDF_from_XML_XSL(xmlfilename, xslfilename, pdffilename);
    }

    public static void MarkInactive(SqlTransaction tran, int shoppingCartId, int version ) {
        Logger.ASSERT( 0 != shoppingCartId);
        string sql = " update ShoppingCart "
                    +"    set IsActive = 0 "
                    +"  where ShoppingCartId = @ShoppingCartId "
                    +"    and version = @version ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ShoppingCartId", shoppingCartId));
        parms.Add(new SqlParameter("@version", version));
        int count = SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
         if ( 0 == count ) {
             throw new VersionNotFoundException();
         }
    }

    public static string GetMarketingProgram(SqlTransaction tran, int shoppingCartShippmentId, int customerid) {
        string sql = 
           " select top 1 MarketingProgram "
          +"   from CustomerPrice cp " 
          +"        inner join ShoppingCartDetail scd on scd.ModelItemId = cp.ModelItemId "
          +"  where scd.ShipmentId = @ShipmentId "
          +"    and cp.CustomerId = @CustomerId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@ShipmentId", shoppingCartShippmentId));
        parms.Add(new SqlParameter("@CustomerId", customerid));
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            if ( rdr.Read() ) {
                return rdr.GetString(rdr.GetOrdinal("MarketingProgram"));
            } else {
                return null;
            }
        }
    }

    public static List<string> ValidateBeforeCheckIn(SqlTransaction tran, int cartId) {
        List<string> result = new List<string>();
        ShoppingCart cart = ShoppingCartSvc.FindById(tran, cartId);
        cart = ShoppingCartSvc.FullFill(tran, cart);

        // validate PO numbers 
        OrderFilter filter = new OrderFilter();
        filter.customerId = cart.customerId;
        Hashtable ponumbers = new Hashtable();
        foreach ( ShoppingCartShipment shipment in cart.shipments ) {
            if ( 0 != shipment.details.Count ) {
                if ( null == shipment.PoNumber || "".Equals(shipment.PoNumber) ) {
                    result.Add(" PO Number is empty");
                }
                foreach ( ShoppingCartDetail detail in shipment.details  ) {
                    if ( !detail.modelItem.isActive || !detail.modelItem.item.isActive ) {
                        result.Add("Item "+detail.modelItem.item.sku+" is not allowed any more.");
                    }
                }
                filter.poNumberStrong = shipment.PoNumber;
                List<Order> orders = OrderSvc.GetOrders(tran, filter);
                if ( 0 != orders.Count ) {
                    result.Add(" PO Number '"+shipment.PoNumber+"' is not unique");
                }
                if ( ponumbers.ContainsKey(shipment.PoNumber) ) {
                    result.Add(" PO Number '"+shipment.PoNumber+"' is not unique");
                } else {
                    ponumbers.Add(shipment.PoNumber, shipment);
                }
            }
        }
        
        // validate inventory
        decimal nettoTotal = cart.grandTotal;
        foreach ( ShoppingCartShipment shipment in cart.shipments ) {
            Entity.Warehouse w = WarehouseSvc.FindById(tran, shipment.warehouseId);
            foreach ( ShoppingCartDetail detail in shipment.details ) {
                Inventory inv = InventorySvc.FindByItemWarehouse(tran, detail.modelItem.itemId, shipment.warehouseId);
                int ordered = OrderSvc.GetOrderedQty(tran, detail.modelItem.itemId, shipment.warehouseId);
                if ( inv.qty - ordered - detail.qtyOrdered < 0 ) {
                   result.Add("No enought inventory for Item '"+detail.modelItem.item.sku+"' in warehouse '"+w.name+"'");
                }
            }
            nettoTotal -= shipment.liftGatePrice; 
            nettoTotal -= shipment.shippingTotal;
        }

        // validate customer status 
        if ( !cart.customer.creditStatus ) {
            result.Add("Credit status hold.");
        }

        // validate overrun 
        if (  nettoTotal > cart.customer.dayBalance ) {
            result.Add("You overlimit today.");
        }
        return result;
    }

    public static int GetCustomerId(SqlTransaction tran, int id) {
        List<ShoppingCart> carts = Select(tran, id, 0, 0, null, CartStatus.ANY);
        Logger.ASSERT( 1 == carts.Count);
        return carts[0].customerId;
    }

    private static List<Entity.ShoppingCart> Select(SqlTransaction tran, 
                  int id, int customerId, int brandId, string userid, CartStatus status) 
    {
        string sql = 
            " select ShoppingCartId, IsActive, CustomerId, OrderDate, RepAccountNo, ShippingAddressId, "
          + "        BrandId, IPAddress, Total, ShippingTotalAllWarehouses, GrandTotal, "
          + "        Phone, Fax, Email, SalesPerson, JobsiteContactPh, MarkOrder, DeliveryRequest, "
          + "        DateCreated, CreatedByUser, LastUpdateDate, AcknFileName, version "
          + "   from ShoppingCart " 
          + "  where 1=1 ";

        List<SqlParameter> parms = new List<SqlParameter>();

        if ( CartStatus.ANY != status ) {
            sql += " and IsActive = @IsAny ";
            parms.Add( new SqlParameter("@isAny", CartStatus.ACTIVE == status ? true: false)); 
        }

        if ( 0 != id ) {
            sql += " and  ShoppingCartId = @Id ";
            parms.Add(new SqlParameter("@Id", id));
        }

        if ( null != userid ) {
            sql += " and  RepAccountNo = @RepAccountNo ";
            parms.Add(new SqlParameter("@RepAccountNo", userid));
        }

        if ( 0 != customerId ) {
            sql += " and  CustomerId = @CustomerId ";
            parms.Add(new SqlParameter("@CustomerId", customerId));
        }

        if ( 0 != brandId ) {
            sql += " and  BrandId = @BrandId ";
            parms.Add(new SqlParameter("@BrandId", brandId));
        }

        List<Entity.ShoppingCart> result = new List<Entity.ShoppingCart>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            while( rdr.Read() ) {
                result.Add(FromReader(rdr));
            }
        }
        return result;
    }

    private static Entity.ShoppingCart FromReader(SqlDataReader rdr) {
        Entity.ShoppingCart result = new Entity.ShoppingCart();
        result.shoppingCartId = rdr.GetInt32(rdr.GetOrdinal("ShoppingCartId"));
        result.isActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
        result.customerId = rdr.GetInt32(rdr.GetOrdinal("CustomerId"));
        result.orderDate = rdr.GetDateTime(rdr.GetOrdinal("OrderDate"));
        result.repAccountNo = rdr.GetString(rdr.GetOrdinal("RepAccountNo"));
        result.shippingAddressId = rdr.IsDBNull(rdr.GetOrdinal("ShippingAddressId"))? 0 : rdr.GetInt32(rdr.GetOrdinal("ShippingAddressId"));
        result.brandId = rdr.GetInt32(rdr.GetOrdinal("BrandId"));
        result.ipAddress = rdr.GetString(rdr.GetOrdinal("IPAddress"));
        result.total = rdr.GetDecimal(rdr.GetOrdinal("Total"));
        result.shippingTotalAllWarehouses = rdr.GetDecimal(rdr.GetOrdinal("ShippingTotalAllWarehouses"));
        result.grandTotal = rdr.GetDecimal(rdr.GetOrdinal("GrandTotal"));
        result.phone = rdr.IsDBNull(rdr.GetOrdinal("Phone")) ? null : rdr.GetString(rdr.GetOrdinal("Phone"));
        result.fax = rdr.IsDBNull(rdr.GetOrdinal("Fax")) ? null : rdr.GetString(rdr.GetOrdinal("Fax"));
        result.salesPerson = rdr.IsDBNull(rdr.GetOrdinal("SalesPerson")) ? null : rdr.GetString(rdr.GetOrdinal("SalesPerson"));
        result.email = rdr.IsDBNull(rdr.GetOrdinal("Email")) ? null : rdr.GetString(rdr.GetOrdinal("Email"));
        result.jobsiteContactPh = rdr.IsDBNull(rdr.GetOrdinal("JobsiteContactPh")) ? null : rdr.GetString(rdr.GetOrdinal("JobsiteContactPh"));
        result.markOrder = rdr.IsDBNull(rdr.GetOrdinal("MarkOrder")) ? null : rdr.GetString(rdr.GetOrdinal("MarkOrder"));
        result.deliveryRequest = rdr.IsDBNull(rdr.GetOrdinal("DeliveryRequest")) ? null : rdr.GetString(rdr.GetOrdinal("DeliveryRequest"));
        result.acknFileName = rdr.GetString(rdr.GetOrdinal("AcknFileName"));
        result.version = rdr.GetInt32(rdr.GetOrdinal("version"));
        TraceableSvc.FromReader(rdr, result);
        return result;
    }
}
}
