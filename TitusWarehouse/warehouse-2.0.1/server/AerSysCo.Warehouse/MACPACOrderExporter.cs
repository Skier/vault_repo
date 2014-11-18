using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Warehouse
{
public class MACPACOrderExporter
{

private XmlDocument doc = null;
private XmlElement root = null;
private Brand brand = null;

public XmlDocument ExportOrder(SqlTransaction tran, Order order) {
    Logger.ASSERT(null != order);

    brand = BrandSvc.FindById(tran, order.brandId);

    order.MACPACFileName = "WEB"+brand.code+order.orderId.ToString()+".xml";

    this.doc = new XmlDocument();
    this.root = doc.CreateElement("cXML");
    doc.AppendChild(root);
    root.SetAttribute("version","1.0");
    root.SetAttribute("payloadID","XML PO for MacPac");


    MakeHeader(order);
    MakeReport(order);
    return this.doc;
}

private XmlElement MakeReport(Order order) {
    XmlElement report = this.doc.CreateElement("Report");
    this.root.AppendChild(report);
    XmlElement tts = this.doc.CreateElement("TTS");
    report.AppendChild(tts);

    tts.SetAttribute("file", order.MACPACFileName);

    XmlElement address = this.doc.CreateElement("Address");
    tts.AppendChild(address);

    XmlElement soldTo = this.doc.CreateElement("SoldTo");
    address.AppendChild(soldTo);
    {
        XmlElement name = this.doc.CreateElement("Name1");
        name.AppendChild( doc.CreateCDataSection( null != order.soldName ? order.soldName :"" ));
        soldTo.AppendChild(name);

        XmlElement street = this.doc.CreateElement("Street1");
        street.AppendChild( doc.CreateCDataSection( null != order.soldAddress1? order.soldAddress1 :"")); 
        soldTo.AppendChild(street);

        if ( null != order.soldAddress2 && !"".Equals(order.soldAddress2) ) {
            street = this.doc.CreateElement("Street2");
            street.AppendChild( doc.CreateCDataSection( null != order.soldAddress2? order.soldAddress2 :"")); 
            soldTo.AppendChild(street);
        }

        XmlElement city = this.doc.CreateElement("City");
        city.AppendChild( doc.CreateCDataSection(order.soldCity));
        soldTo.AppendChild(city);

        XmlElement state = this.doc.CreateElement("State");
        state.AppendChild( doc.CreateCDataSection(null != order.soldState ? order.soldState : ""));
        soldTo.AppendChild(state);

        XmlElement zip = this.doc.CreateElement("Zip");
        zip.AppendChild( doc.CreateCDataSection( null != order.soldZip ?  order.soldZip : "" ));
        soldTo.AppendChild(zip);
    }


    XmlElement shipTo = this.doc.CreateElement("ShipTo");
    address.AppendChild(shipTo);
    {
        XmlElement name = this.doc.CreateElement("Name1");
        name.AppendChild( doc.CreateCDataSection( null != order.shipName ? order.shipName :"" ));
        shipTo.AppendChild(name);

        XmlElement street = this.doc.CreateElement("Street1");
        street.AppendChild( doc.CreateCDataSection( null != order.shipAddress1? order.shipAddress1 :"" )); 
        shipTo.AppendChild(street);

        if ( null != order.shipAddress2 && !"".Equals(order.shipAddress2) ) {
            street = this.doc.CreateElement("Street2");
            street.AppendChild( doc.CreateCDataSection( null != order.shipAddress2? order.shipAddress2 :"" )); 
            shipTo.AppendChild(street);
        }

        XmlElement city = this.doc.CreateElement("City");
        city.AppendChild( doc.CreateCDataSection( order.shipCity ));
        shipTo.AppendChild(city);

        XmlElement state = this.doc.CreateElement("State");
        state.AppendChild( doc.CreateCDataSection( null != order.shipState ? order.shipState : "" ));
        shipTo.AppendChild(state);

        XmlElement zip = this.doc.CreateElement("Zip");
        zip.AppendChild( doc.CreateCDataSection( null != order.shipZip ?  order.shipZip : "" ));
        shipTo.AppendChild(zip);

        XmlElement att = this.doc.CreateElement("Attention");
        shipTo.AppendChild(att);

        XmlElement country = this.doc.CreateElement("Country");
        country.AppendChild( doc.CreateCDataSection("US".Equals(order.shipCountry) ? "USA" : order.shipCountry ));
        shipTo.AppendChild(country);
    }



    MakeBrand(tts, order);
    MakeMarketingProgramm(tts, order);
    MakeOrderInfo(tts, order);
    MakeShipping(tts, order);
    MakeSpecialInfo(tts, order);
    MakeQuantityInfo(tts, order);

    MakeLineItems(tts, order);
    MakePricingTotal(tts, order);
    return report;
}                                   


private XmlElement MakePricingTotal(XmlElement parent, Order order) {

    XmlElement pt = this.doc.CreateElement("PricingTotals");
    parent.AppendChild(pt);

    XmlElement boc = this.doc.CreateElement("BaseOrderCost");
    pt.AppendChild(boc);
    boc.AppendChild( doc.CreateCDataSection( order.total.ToString() )); // ><![CDATA[5450.00]]></BaseOrderCost>         <!-- TheOrder. -->

    XmlElement sc = this.doc.CreateElement("SetupCharge");
    pt.AppendChild(sc); 
    sc.AppendChild( doc.CreateCDataSection( ".00" ));

    XmlElement fr = this.doc.CreateElement("Freight");
    pt.AppendChild(fr); // ><![CDATA[.00]]></Freight>                         <!-- TheOrder. -->
    fr.AppendChild(doc.CreateCDataSection(order.shippingTotal.ToString()));

    XmlElement toc = this.doc.CreateElement("TotalOrderCost"); 
    pt.AppendChild(toc); // ><![CDATA[5450.00]]></TotalOrderCost>       <!-- TheOrder. -->
    toc.AppendChild(doc.CreateCDataSection(order.grandTotal.ToString()));

    XmlElement nmf = this.doc.CreateElement("NetMinusFreight"); 
    pt.AppendChild(nmf);  // ><![CDATA[5450.00]]></NetMinusFreight>     <!-- TheOrder. -->
    nmf.AppendChild(doc.CreateCDataSection((order.grandTotal-order.shippingTotal).ToString()));

    return pt;
}

private XmlElement MakeLineItems(XmlElement parent, Order order) {
    XmlElement li = this.doc.CreateElement("LineItems");
    parent.AppendChild(li);

    int shipmentLine = 1;
    foreach (OrderDetail detail in order.details ) {
        MakeDatailItem(li, order, detail);
        shipmentLine++;
    }
    MakeShipmentItem(li, order, shipmentLine);
    return li;
}

private int MakeShipmentItem(XmlElement parent, Order order, int startLine) {
    XmlElement grp = this.doc.CreateElement("Group");
    parent.AppendChild(grp);
    XmlElement mc = this.doc.CreateElement("ModelConfig");
    grp.AppendChild(mc);

    XmlElement line = this.doc.CreateElement("Line");
    mc.AppendChild(line);
    line.AppendChild( doc.CreateCDataSection( startLine.ToString()));
    startLine++;

    XmlElement plantCode = this.doc.CreateElement("PlantCode");
    mc.AppendChild(plantCode);
    plantCode.AppendChild( doc.CreateCDataSection(order.warehouse.warehouseCode));

    XmlElement qty = this.doc.CreateElement("Qty");
    mc.AppendChild(qty);
    qty.AppendChild( doc.CreateCDataSection("1"));

    XmlElement model = this.doc.CreateElement("Model");
    mc.AppendChild(model);
    model.AppendChild( doc.CreateCDataSection( "\\FRT" ));

    XmlElement comment = this.doc.CreateElement("Comment");
    mc.AppendChild(comment);

    XmlElement tag = this.doc.CreateElement("Tag");
    mc.AppendChild(tag);

    XmlElement mult= this.doc.CreateElement("Multiplier");
    mc.AppendChild(mult);
    mult.AppendChild( doc.CreateCDataSection("1"));

    XmlElement price= this.doc.CreateElement("IndividualPrice");
    mc.AppendChild(price);
    price.AppendChild( doc.CreateCDataSection(order.shippingTotal.ToString()));

    XmlElement cost = this.doc.CreateElement("TotalCost");
    mc.AppendChild(cost);
    cost.AppendChild( doc.CreateCDataSection( order.shippingTotal.ToString()));

    return startLine;
}

private XmlElement MakeDatailItem(XmlElement parent, Order order, OrderDetail detail) {
    XmlElement grp = this.doc.CreateElement("Group");
    parent.AppendChild(grp);
    XmlElement mc = this.doc.CreateElement("ModelConfig");
    grp.AppendChild(mc);

    XmlElement ln = this.doc.CreateElement("Line");
    mc.AppendChild(ln);
    ln.AppendChild( doc.CreateCDataSection( detail.lineNumber.ToString()));

    XmlElement plantCode = this.doc.CreateElement("PlantCode");
    mc.AppendChild(plantCode);
    plantCode.AppendChild( doc.CreateCDataSection(order.warehouse.warehouseCode));

    XmlElement qty = this.doc.CreateElement("Qty");
    mc.AppendChild(qty);
    qty.AppendChild( doc.CreateCDataSection(detail.qty.ToString()));

    XmlElement model = this.doc.CreateElement("Model");
    mc.AppendChild(model);
    model.AppendChild( doc.CreateCDataSection(detail.sku));

    XmlElement comment = this.doc.CreateElement("Comment");
    mc.AppendChild(comment);

    XmlElement tag = this.doc.CreateElement("Tag");
    mc.AppendChild(tag);

    XmlElement mult= this.doc.CreateElement("Multiplier");
    mc.AppendChild(mult);
    mult.AppendChild( doc.CreateCDataSection(detail.multiplier.ToString()));

    XmlElement price= this.doc.CreateElement("IndividualPrice");
    mc.AppendChild(price);
    price.AppendChild( doc.CreateCDataSection(detail.price.ToString()));

    XmlElement cost = this.doc.CreateElement("TotalCost");
    mc.AppendChild(cost);
    cost.AppendChild( doc.CreateCDataSection(detail.cost.ToString()));

    return grp;
}


private XmlElement MakeQuantityInfo(XmlElement parent, Order order) {
    XmlElement qi = this.doc.CreateElement("QuantityInfo");
    parent.AppendChild(qi);
    XmlElement jn = this.doc.CreateElement("JobNumber"); // ><![CDATA[26276]]></JobNumber>                         
    qi.AppendChild(jn);

    XmlElement mc = this.doc.CreateElement("ModelCount"); //>1</ModelCount>                                       
    qi.AppendChild(mc);
    mc.AppendChild( doc.CreateCDataSection(order.details.Count.ToString()));

    XmlElement vn = this.doc.CreateElement("VersionNumber"); //></VersionNumber>
    qi.AppendChild(vn);

    XmlElement lc = this.doc.CreateElement("LineCount"); //>3</LineCount>                                         
    qi.AppendChild(lc);
    lc.AppendChild( doc.CreateCDataSection((order.details.Count + 1).ToString()));

    XmlElement bl = this.doc.CreateElement("BrandLogo"); // >DarkBlueLogo.gif</BrandLogo>
    qi.AppendChild(bl);

    XmlElement jid = this.doc.CreateElement("JobInitiatedDate"); // >1/11/2008</JobInitiatedDate>                 
    qi.AppendChild(jid);
    jid.AppendChild( doc.CreateCDataSection( order.orderDate.ToShortDateString()));

    XmlElement email = this.doc.CreateElement("email"); // >aliu@norman-wright.com</email>                        
    qi.AppendChild(email);
    email.AppendChild( doc.CreateCDataSection( order.email));

    return qi;
}

private XmlElement MakeSpecialInfo(XmlElement parent, Order oreder) {
    XmlElement si = this.doc.CreateElement("SpecialInfo");
    parent.AppendChild(si);
    XmlElement SDAN = this.doc.CreateElement("SDANo");
    si.AppendChild(SDAN);

    return si;
}

private XmlElement MakeShipping(XmlElement parent, Order order) {
    XmlElement shipping = this.doc.CreateElement("Shipping");
    parent.AppendChild(shipping);
    XmlElement method = this.doc.CreateElement("ShippingMethod");
    shipping.AppendChild(method);

    XmlElement shipVia = this.doc.CreateElement("ShipVia");
    method.AppendChild(shipVia);
    shipVia.AppendChild( doc.CreateCDataSection(order.shippingType.shippingCode));
    //>LTL</ShipVia>                                         <!-- ShippingType.ShippingCode -->

    XmlElement cbd = this.doc.CreateElement("CallBeforeDelivery");
    method.AppendChild(cbd);
    cbd.AppendChild( doc.CreateCDataSection( null != order.jobsiteContactPh ? "CALL "+order.jobsiteContactPh: ""));
    //><![CDATA[]]></CallBeforeDelivery>          <!--  -->

    XmlElement terms = this.doc.CreateElement("Terms");
    method.AppendChild(terms);
    //><![CDATA[F.O.B. Factory]]></Terms>                      <!--  -->

    XmlElement markOrder = this.doc.CreateElement("MarkOrder");
    method.AppendChild(markOrder);
    markOrder.AppendChild( doc.CreateCDataSection( null != order.markOrder ? order.markOrder : ""));
    //><![CDATA[MMC]]></MarkOrder>                         <!--  -->

    XmlElement instructions = this.doc.CreateElement("ShippingInstructions");
    method.AppendChild(instructions);
    
    instructions.AppendChild( doc.CreateCDataSection( null != order.deliveryRequest ? order.deliveryRequest : ""));
    //><![CDATA[]]></ShippingInstructions>      <!--  -->

    XmlElement charges = this.doc.CreateElement("ShippingCharges");
    shipping.AppendChild(charges);

    XmlElement commentsToFactory = this.doc.CreateElement("CommentsToFactory");
    charges.AppendChild(commentsToFactory);
    //><![CDATA[]]></CommentsToFactory>            <!--  -->

    XmlElement serviceRequest = this.doc.CreateElement("CustomerServiceRequest");
    charges.AppendChild(serviceRequest);
    //><![CDATA[]]></CustomerServiceRequest>  <!--  -->
    return shipping;
}

private XmlElement MakeOrderInfo(XmlElement parent, Order order) {
    XmlElement oi = this.doc.CreateElement("OrderInfo");
    parent.AppendChild(oi);
    XmlElement od = this.doc.CreateElement("OrderDate");
    oi.AppendChild(od);
    od.AppendChild( doc.CreateCDataSection( order.orderDate.ToShortDateString()));

    XmlElement repPon = this.doc.CreateElement("RepPONo");
    oi.AppendChild(repPon);
    repPon.AppendChild( doc.CreateCDataSection( order.PONumber));  
    XmlElement custPon = this.doc.CreateElement("CustomerPONo");
    oi.AppendChild(custPon);
    XmlElement custAcctNo = this.doc.CreateElement("CustAccountNo");
    oi.AppendChild(custAcctNo);
    custAcctNo.AppendChild( doc.CreateCDataSection( CustomerSvc.GetShortAccountNo(order.customer.MACPACCustonerNumber)));
    // ><![CDATA[]]></CustAccountNo>                 <!--  -->
    XmlElement jobName = this.doc.CreateElement("JobName");
    oi.AppendChild(jobName);
    jobName.AppendChild( doc.CreateCDataSection(order.orderId.ToString()));
    XmlElement sp = this.doc.CreateElement("SalesPerson");
    oi.AppendChild(sp);
    sp.AppendChild( doc.CreateCDataSection( null != order.salesPerson ? order.salesPerson : ""));
    return oi;
}

private XmlElement MakeMarketingProgramm(XmlElement report, Order order) {
    XmlElement mp = this.doc.CreateElement("MarketingProgram");
    report.AppendChild(mp);
    XmlElement pc = this.doc.CreateElement("ProgramCode");
    mp.AppendChild(pc);
    XmlElement p = this.doc.CreateElement("Program");
    mp.AppendChild(p);
    if ( null == order.marketingProgram ) {
        pc.AppendChild( doc.CreateCDataSection(""));
    } else {
        pc.AppendChild( doc.CreateCDataSection(order.marketingProgram));
    }
    p.AppendChild( doc.CreateCDataSection(""));
    return mp;
}

private XmlElement MakeBrand(XmlElement report, Order order) {
    XmlElement brand = this.doc.CreateElement("Brand");

    XmlElement acctInfo = this.doc.CreateElement("AccountInfo");
    brand.AppendChild(acctInfo);
    XmlElement repAcctNo = this.doc.CreateElement("RepAccountNo");
    acctInfo.AppendChild(repAcctNo);
    repAcctNo.AppendChild( doc.CreateCDataSection( CustomerSvc.GetShortAccountNo(order.customer.MACPACCustonerNumber) ));
    XmlElement phone = this.doc.CreateElement("Phone");
    acctInfo.AppendChild(phone);
    phone.AppendChild( doc.CreateCDataSection(null != order.phone ? order.phone : ""));
    XmlElement fax = this.doc.CreateElement("Fax");
    acctInfo.AppendChild(fax);
    fax.AppendChild( doc.CreateCDataSection(null != order.fax ? order.fax : ""));

    XmlElement wh = this.doc.CreateElement("SellingWareHouse");
    wh.AppendChild( doc.CreateCDataSection(this.brand.code));
    brand.AppendChild(wh);

    XmlElement os = this.doc.CreateElement("OrderSource");
    os.AppendChild( doc.CreateCDataSection("WHS"));
    brand.AppendChild(os);
    report.AppendChild(brand);
    return brand;
}

private XmlElement MakeHeader(Order order) {
    XmlElement header = this.doc.CreateElement("Header");
    this.root.AppendChild(header);
    string domain = ConfigurationManager.AppSettings[this.brand.brandName.ToLower()+"_domain"];

    // From credential 
    {
        XmlElement From = this.doc.CreateElement("From");
        header.AppendChild(From);
        XmlElement cred = this.doc.CreateElement("Credential");
        From.AppendChild(cred);

        cred.SetAttribute("domain", (null==domain ? this.brand.brandName : domain));

        XmlElement ident = this.doc.CreateElement("Identity");
        cred.AppendChild(ident);
    }
    // To credential 
    {
        XmlElement To = this.doc.CreateElement("To");
        header.AppendChild(To);
        XmlElement cred = this.doc.CreateElement("Credential");
        To.AppendChild(cred);
        cred.SetAttribute("domain", "Ruskin Corp");
        XmlElement ident = this.doc.CreateElement("Identity");
        cred.AppendChild(ident);
    }
    // Sender credential 
    {
        XmlElement Sender = this.doc.CreateElement("Sender");
        header.AppendChild(Sender);
        XmlElement cred = this.doc.CreateElement("Credential");
        Sender.AppendChild(cred);


        cred.SetAttribute("domain", (null==domain ? this.brand.brandName : domain));

        XmlElement ident = this.doc.CreateElement("Identity");
        cred.AppendChild(ident);
    }
    return header;
}

}
}
