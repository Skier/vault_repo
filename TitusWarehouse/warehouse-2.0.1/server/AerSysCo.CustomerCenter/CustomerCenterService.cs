using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using AerSysCo.Common;
using ShoppingCart;
using log4net;


namespace AerSysCo.CustomerCenter
{
public class CustomerCenterService
{

    private string brand = null;
    private string brandCode = null;
    private string imageURLprefix = null;

    private ShoppingCart.SQL customerCenter = null;

    public CustomerCenterService(string brand, string brandCode, string imageURLprefix) {
        Logger.ASSERT(null != brand);
        Logger.ASSERT(null != brandCode);
        Logger.ASSERT(null != imageURLprefix);

        this.customerCenter = new ShoppingCart.SQL(brand);
        this.brand = brand.ToLower();
        this.brandCode = brandCode.ToUpper();
        this.imageURLprefix = imageURLprefix;
    }


    public SalesRep GetUserShort(string userid) {
        DataSet ds = this.customerCenter.Get_User_Name(userid);
        DataTable tbl = ds.Tables[0];
        if ( 0 == tbl.Rows.Count ) {
            throw new ApplicationException("User with id '"+userid+"' not found");
        }
        SalesRep result = new SalesRep();
        result.FirstName = tbl.Rows[0][tbl.Columns["userid"]].ToString();
        result.LastName = tbl.Rows[0][tbl.Columns["username"]].ToString();
        result.UserName = tbl.Rows[0][tbl.Columns["emailaddress"]].ToString();
        return result;
    }

    public SalesRep GetUser(string userid) {
        DataSet ds = this.customerCenter.Get_User_Name(userid);
        DataTable tbl = ds.Tables[0];
        if ( 0 == tbl.Rows.Count ) {
            throw new ApplicationException("User with id '"+userid+"' not found");
        }
        SalesRep result = new SalesRep();
        result.FirstName = tbl.Rows[0][tbl.Columns["userid"]].ToString();
        result.LastName = tbl.Rows[0][tbl.Columns["username"]].ToString();
        result.UserName = tbl.Rows[0][tbl.Columns["emailaddress"]].ToString();
        result = FullFill(result);
        return result;
    }

    public List<Customer> GetAllCustomers() {
        DataSet ds = this.customerCenter.Get_All_Accounts();
        DataTable tbl = ds.Tables[0];
        List<Customer> result = new List<Customer>();
        foreach(DataRow row in tbl.Rows) {
            Customer cust = new Customer();
            cust.MACPACCustonerNumber = row[tbl.Columns["customerid"]].ToString();
            cust.email = null == row[tbl.Columns["emailaddress"]] ? "" : row[tbl.Columns["emailaddress"]].ToString();
            cust.fax = null == row[tbl.Columns["faxnumber"]] ? "" : row[tbl.Columns["faxnumber"]].ToString();
            cust.phoneNumber = null == row[tbl.Columns["businessPhone1"]] ? "" : row[tbl.Columns["businessPhone1"]].ToString();
            cust.salesRepCompanyName = row[tbl.Columns["customername"]].ToString();
            cust.address = new Address();
            cust.address.name = row[tbl.Columns["customername"]].ToString();
            cust.address.address1 = null == row[tbl.Columns["address1"]] ? "" : row[tbl.Columns["address1"]].ToString();
            cust.address.address2 = null == row[tbl.Columns["address2"]] ? "" : row[tbl.Columns["address2"]].ToString();
            cust.address.city = null == row[tbl.Columns["city"]] ? "" : row[tbl.Columns["city"]].ToString();
            cust.address.state = null == row[tbl.Columns["stateprovince"]] ? "" : row[tbl.Columns["stateprovince"]].ToString();
            cust.address.zip = null == row[tbl.Columns["zipPostal"]] ? "" : row[tbl.Columns["zipPostal"]].ToString();
            cust.address.country = null == row[tbl.Columns["Country"]] ? "" : row[tbl.Columns["Country"]].ToString();
            result.Add(cust);
        }
        return result;
    } 


    private SalesRep FullFill(SalesRep rep) {
        DataSet ds = this.customerCenter.Get_Customer_Account(rep.FirstName);
        DataTable tbl = ds.Tables[0];
        rep.customerList = new List<Customer>();
        foreach(DataRow row in tbl.Rows) {

            Customer cust = new Customer();
            cust.MACPACCustonerNumber = row[tbl.Columns["customerid"]].ToString();
            cust.email = null == row[tbl.Columns["emailaddress"]] ? "" : row[tbl.Columns["emailaddress"]].ToString();
            cust.fax = null == row[tbl.Columns["faxnumber"]] ? "" : row[tbl.Columns["faxnumber"]].ToString();
            cust.phoneNumber = null == row[tbl.Columns["businessPhone1"]] ? "" : row[tbl.Columns["businessPhone1"]].ToString();
            cust.salesRepCompanyName = row[tbl.Columns["customername"]].ToString();
            cust.address = new Address();
            cust.address.name = row[tbl.Columns["customername"]].ToString();
            cust.address.address1 = null == row[tbl.Columns["address1"]] ? "" : row[tbl.Columns["address1"]].ToString();
            cust.address.address2 = null == row[tbl.Columns["address2"]] ? "" : row[tbl.Columns["address1"]].ToString();
            cust.address.city = null == row[tbl.Columns["city"]] ? "" : row[tbl.Columns["city"]].ToString();
            cust.address.state = null == row[tbl.Columns["stateprovince"]] ? "" : row[tbl.Columns["stateprovince"]].ToString();
            cust.address.zip = null == row[tbl.Columns["zipPostal"]] ? "" : row[tbl.Columns["zipPostal"]].ToString();
            cust.address.country = null == row[tbl.Columns["Country"]] ? "" : row[tbl.Columns["Country"]].ToString();

            rep.customerList.Add(cust);
        }
        return rep;
    }


    public Address GetCustomerAddress(string customerid) {
        DataSet ds = this.customerCenter.Get_Customer_Account_Detail(customerid);
        DataTable tbl = ds.Tables[0];
        if ( 0 == tbl.Rows.Count ) {
            throw new ApplicationException("Customer with id '"+customerid+"' not found");
        }
        DataRow row = tbl.Rows[0];
        Address address = new Address();
        address.name = row[tbl.Columns["customername"]].ToString();
        address.address1 = null == row[tbl.Columns["address1"]] ? "" : row[tbl.Columns["address1"]].ToString();
        address.address2 = null == row[tbl.Columns["address2"]] ? "" : row[tbl.Columns["address1"]].ToString();
        address.city = null == row[tbl.Columns["city"]] ? "" : row[tbl.Columns["city"]].ToString();
        address.state = null == row[tbl.Columns["stateprovince"]] ? "" : row[tbl.Columns["stateprovince"]].ToString();
        address.zip = null == row[tbl.Columns["zipPostal"]] ? "" : row[tbl.Columns["zipPostal"]].ToString();
        address.country = null == row[tbl.Columns["Country"]] ? "" : row[tbl.Columns["Country"]].ToString();
        return address;
    }


    public List<BrandModel> GetModels() {
        DataSet ds = this.customerCenter.getProductCategory(this.brandCode);
        DataTable tbl = ds.Tables[0];
        List<BrandModel> result = new List<BrandModel>();
        foreach(DataRow row in tbl.Rows) {
            BrandModel model = new BrandModel();
            model.brandName = this.brand;
            model.category1 = row[tbl.Columns["cat1"]].ToString();
            model.category2 = row[tbl.Columns["cat2"]].ToString();
            model.category3 = row[tbl.Columns["cat3"]].ToString();
            model.modelName = row[tbl.Columns["name"]].ToString();
            model.model = row[tbl.Columns["mfgcode"]].ToString();
            model.description = row[tbl.Columns["description"]].ToString();
            model.imageURL = this.imageURLprefix+row[tbl.Columns["image_id"]].ToString().ToLower();    
            result.Add(model);
        }
        return result;
        
    }

    public LatLng GetLatLng(string zip) {
        DataSet ds = this.customerCenter.Get_Lat_Long(zip);
        DataTable tbl = ds.Tables[0];
        if ( 0 == tbl.Rows.Count ) {
            throw new ApplicationException("Zip code '"+zip+"' not found");
        }
        DataRow row = tbl.Rows[0];
        LatLng result = new LatLng();
        result.lat = Double.Parse(row[tbl.Columns["Lat"]].ToString());
        result.lng = Double.Parse(row[tbl.Columns["Long"]].ToString());

        return result;
    }


    public int CalculateDistance(string zip1, string zip2) {
        LatLng point1 = GetLatLng(zip1);
        LatLng point2 = GetLatLng(zip2);
        return FindDistance2(point1.lat, point1.lng, point2.lat, point2.lng);
    }

    private double ArcCos(double X) {
        return Math.Atan(-X / Math.Sqrt(-X * X + 1)) + 2 * Math.Atan(1);
    }
    private double ArcCosY(double Y) {    
        if ( Y == 1 ) { 
            return 0;
        } else {
            return (2 * Math.Atan(1) - Math.Atan(Y / Math.Sqrt(1 - Y * Y)) ) * 57.295779513082320876798154814105;
        }
    }
    private int FindDistance2(double latA, double lonA, double latB, double lonB) {
           /* 'cos D = ( sin a )(sin b) + (cos a)(cos b)(cos P)
            '            D is the angular distance between points A and B
            '            a is the latitude of point A
            '            b is the latitude of point B
            '            P is the longitudinal difference between points A and B
            */ 
        double radX = 0.017453292519943295769236907684886;
            /*
            'a = 43
            'b = 35
            'aa = 80
            'bb = -135 '(135 E)
            */
            /*
            latA = csng(latA)
            latB = csng(latB)
            
            lonA = csng(lonA)
            lonB = csng(lonB)
            */
        double p = (lonA - lonB);

        if ( p > 180 ) {
            p = 360 - p;
        }

        double cosD = (Math.Sin(latA*radX)*Math.Sin(latB*radX)) + (Math.Cos(latA*radX)*Math.Cos(latB*radX)*Math.Cos(p*radX));
        
        double dist = 68.9722 *ArcCosY(cosD); //  '68.9722 miles per degree
        return Decimal.ToInt32(new Decimal(Math.Round(dist, 1)));
    }


    private void FromReader( SqlDataReader rdr, Address address ) {
        address.name = rdr.GetString(rdr.GetOrdinal("customername"));
        address.address1 = rdr.IsDBNull(rdr.GetOrdinal("address1")) ? "" : rdr.GetString(rdr.GetOrdinal("address1"));
        address.address2 = rdr.IsDBNull(rdr.GetOrdinal("address2")) ? "" : rdr.GetString(rdr.GetOrdinal("address2"));
        address.city = rdr.IsDBNull(rdr.GetOrdinal("city")) ? "" : rdr.GetString(rdr.GetOrdinal("city"));
        address.state = rdr.IsDBNull(rdr.GetOrdinal("stateprovince")) ? "" : rdr.GetString(rdr.GetOrdinal("stateprovince"));
        address.zip = rdr.IsDBNull(rdr.GetOrdinal("zipPostal")) ? "" : rdr.GetString(rdr.GetOrdinal("zipPostal"));
        address.country = rdr.IsDBNull(rdr.GetOrdinal("Country")) ? "" : rdr.GetString(rdr.GetOrdinal("Country"));
    }


}
}
