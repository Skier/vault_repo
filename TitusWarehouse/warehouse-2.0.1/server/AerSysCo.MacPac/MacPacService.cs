using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AerSysCo.MacPac.WebReference;
using AerSysCo.Common;



namespace AerSysCo.MacPac
{
public enum OrderStatus {
    UNKNOWN, PENDING, CLOSED, OPEN
};

public enum CreditHold {
    CREDIT_CLOSED, CREDIT_OPEN, CREDIT_UNKNOWN
};

public class MacPacOrder {
    public OrderStatus status = OrderStatus.UNKNOWN;
    public DateTime shippingDate;
    public string orderNumber;
    public string releaseNumber;
    public string trackingNumber="";
};


public class MacPacService
{

public static CreditHold GetCreditStatus(string customerid) {
    Service s = new Service();
    s.SpHeaderValue = new SpHeader();
    s.SpHeaderValue.token = "*#*$2831@*3092";
    DataTable table = s.GetCustStatus(customerid);
    if ( 1 != table.Rows.Count ) {
        return CreditHold.CREDIT_UNKNOWN;
    }
    DataRow row = table.Rows[0];
    if ( "N" == row[table.Columns["CreditHold"]].ToString() ) {
        return CreditHold.CREDIT_OPEN;
    } else {
        return CreditHold.CREDIT_CLOSED;
    }

}

public static MacPacOrder GetOrderStatus(string customerid, string ponumber, DateTime orderDate ) {
    Logger.GetAppLogger().Debug("Get order status '"+customerid+"' '"+ponumber+"'");

    Service s = new Service();
    s.SpHeaderValue = new SpHeader();
    s.SpHeaderValue.token = "*#*$2831@*3092";
    DataTable table = s.GetOrderHeader(customerid, ponumber, orderDate.ToShortDateString(), DateTime.Now.ToShortDateString());

    MacPacOrder result = new MacPacOrder();
    if ( 1 != table.Rows.Count ) {
        result.status = OrderStatus.UNKNOWN;
        return result;
    }
    DataRow row = table.Rows[0];
    result.orderNumber = row[table.Columns["orderno"]].ToString();
    result.releaseNumber = "0";
    if ( "C" == row[table.Columns["status"]].ToString() ) {
        result.status = OrderStatus.CLOSED;
    } else if ( "O" == row[table.Columns["status"]].ToString() ) {
        result.status = OrderStatus.OPEN;
    } else {
        result.status = OrderStatus.PENDING;
    }
    result.shippingDate = DateTime.Parse(row[table.Columns["promisedshipdt"]].ToString());

    return result;
}

}
}
