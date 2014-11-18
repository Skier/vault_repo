using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.CustomerCenter;
using AerSysCo.Entity;


namespace AerSysCo.Warehouse
{

public class CustomerSvc {

    public static SalesRep GetSalesRep(SqlTransaction tran, int brandId, string login) {
        Brand brand = BrandSvc.FindById(tran, brandId);
        CustomerCenterService custService = new CustomerCenterService(brand.brandName, brand.code, brand.imageURLprefix);
        SalesRep result = custService.GetUser(login);
        List<Customer> customers = new List<Customer>();
        foreach(Customer cst in result.customerList) {
            Customer cust = FindByMACPACNumber(tran, cst.MACPACCustonerNumber);
            if ( null == cust ) {
                continue;
            }
            customers.Add(cust);
        }
        Logger.GetAppLogger().Debug("Customers count "+customers.Count);
        result.customerList = customers;
        return result;
    }

    public static List<Customer> GetAll(SqlTransaction tran) {
        return Select(tran, 0, null, 0, null);
    }

    public static Customer FindById(SqlTransaction tran, int id) {
        Logger.ASSERT(0 != id, "Id is null");
        List<Customer> result = Select(tran, id, null, 0, null);
        Logger.ASSERT( 1>= result.Count, "too many values for Id" );
        if ( 0 == result.Count ) {
            string message = string.Format("No customer with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static Customer SoftFindById(SqlTransaction tran, int id) {
        Logger.ASSERT(0 != id);
        List<Customer> result = Select(tran, id, null, 0, null);
        Logger.ASSERT( 1>= result.Count );
        if ( 0 == result.Count ) {
            return null;
        }
        return result[0];
    }

    public static Customer FindByMACPACNumber(SqlTransaction tran, string custId) {
        Logger.ASSERT(null != custId);
        List<Customer> result = Select(tran, 0, custId, 0, null);
        if ( 0 == result.Count ) {
           return null;
        } 
        Logger.ASSERT( 1 >= result.Count );
        return result[0];
    }

    public static Customer FindByShortMACPACNumber(SqlTransaction tran, int brandId, string shortCustId) {
        Logger.ASSERT(null != shortCustId);
        List<Customer> result = Select(tran, 0, null, brandId, shortCustId);
        if ( 0 == result.Count ) {
           return null;
        } 
        Logger.ASSERT( 1 >= result.Count );
        return result[0];
    }


    public static Customer Insert(SqlTransaction tran, Customer cust) {
        string sql = 
            " insert into Customer (DefaultWarehouseId, MACPACCustomerNumber, SalesRepCompanyName, CreditStatus, MaxOrderTotal, CreatedByUser, LastUpdateDate, DateCreated, "
           +"         BrandId, Address1, Address2, City, State, Zip, Country, Phone, Fax, Email )" 
           +" values (@DefaultWarehouseId, @MACPACCustomerNumber, @SalesRepCompanyName, @CreditStatus, @MaxOrderTotal, @CreatedByUser, @LastUpdateDate, @DateCreated, "
           +"         @BrandId, @Address1, @Address2, @City, @State, @Zip, @Country, @Phone, @Fax, @Email) ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@DefaultWarehouseId" , cust.defaultWarehouseId ));
        parms.Add(new SqlParameter("@MACPACCustomerNumber", cust.MACPACCustonerNumber ));
        parms.Add(new SqlParameter("@SalesRepCompanyName", cust.salesRepCompanyName));
        parms.Add(new SqlParameter("@CreditStatus", cust.creditStatus));
        parms.Add(new SqlParameter("@MaxOrderTotal", cust.maxOrderTotal ));
        parms.Add(new SqlParameter("@CreatedByUser", cust.createdByUser ));
        parms.Add(new SqlParameter("@LastUpdateDate", cust.lastUpdateDate));
        parms.Add(new SqlParameter("@DateCreated", cust.dateCreated));
        parms.Add(new SqlParameter("@BrandId", cust.brandId));
        parms.Add(new SqlParameter("@Address1", null == cust.address.address1 ? "" : cust.address.address1));
        parms.Add(new SqlParameter("@Address2", null == cust.address.address2 ? "" : cust.address.address2));
        parms.Add(new SqlParameter("@City", null == cust.address.city ? "" : cust.address.city));
        parms.Add(new SqlParameter("@State", null == cust.address.state ? "" : cust.address.state));
        parms.Add(new SqlParameter("@Zip", null == cust.address.zip ? "" : cust.address.zip));
        parms.Add(new SqlParameter("@Country", null == cust.address.country ? "USA" : cust.address.country));
        parms.Add(new SqlParameter("@Phone", null == cust.phoneNumber ? "" : cust.phoneNumber));
        parms.Add(new SqlParameter("@Fax", null == cust.fax ? "" : cust.fax));
        parms.Add(new SqlParameter("@Email", null == cust.email ? "" : cust.email));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        cust.customerId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("CustomerSvc: created " + cust.MACPACCustonerNumber);
        return cust;

    }
                                                    
    public static string GetShortAccountNo(string accountno) {
        string [] parts = accountno.Split('-');
        Logger.ASSERT(3 == parts.Length);
        return parts[2];
    }

    public static Brand GetCustomerBrand(SqlTransaction tran, Customer cust) {
        return BrandSvc.FindById(tran, 1); // Titus only now
    }

    public static int CompareByName( Customer cust1, Customer cust2) {
        return cust1.salesRepCompanyName.CompareTo(cust2.salesRepCompanyName);
    }

    public static Customer Update(SqlTransaction tran, Customer customer)
    {
        Logger.ASSERT( 0 != customer.customerId );
        List<SqlParameter> parms = new List<SqlParameter>();
        string sql =
            "update Customer "
           + "   set DefaultWarehouseId = @DefaultWarehouseId, "
           + "       MACPACCustomerNumber = @MACPACCustomerNumber, "
           + "       SalesRepCompanyName = @SalesRepCompanyName, "
           + "       CreditStatus = @CreditStatus, "
           + "       MaxOrderTotal = @MaxOrderTotal, "
           + "       LastUpdateDate = @LastUpdateDate, "
           + "       Address1 = @Address1, "
           + "       Address2 = @Address2, "
           + "       City = @City,  "
           + "       State = @State, "
           + "       Zip = @Zip,   "
           + "       Country = @Country, "
           + "       Phone = @Phone, "
           + "       Fax = @Fax, "
           + "       Email = @Email  "
           + " where CustomerId = @CustomerId ";

        parms.Add(new SqlParameter("@CustomerId", customer.customerId));
        parms.Add(new SqlParameter("@DefaultWarehouseId", customer.defaultWarehouseId));
        parms.Add(new SqlParameter("@MACPACCustomerNumber", customer.MACPACCustonerNumber));
        parms.Add(new SqlParameter("@SalesRepCompanyName", customer.salesRepCompanyName));
        parms.Add(new SqlParameter("@CreditStatus", customer.creditStatus));
        parms.Add(new SqlParameter("@MaxOrderTotal", customer.maxOrderTotal));
        parms.Add(new SqlParameter("@LastUpdateDate", customer.lastUpdateDate));
        parms.Add(new SqlParameter("@Address1", customer.address.address1));
        parms.Add(new SqlParameter("@Address2", null == customer.address.address2 ? "" : customer.address.address2));
        parms.Add(new SqlParameter("@City", null == customer.address.city ? "" : customer.address.city));
        parms.Add(new SqlParameter("@State", null == customer.address.state ? "" : customer.address.state));
        parms.Add(new SqlParameter("@Zip", null == customer.address.zip ? "" : customer.address.zip));
        parms.Add(new SqlParameter("@Country", null == customer.address.country ? "USA" : customer.address.country));
        parms.Add(new SqlParameter("@Phone", null == customer.phoneNumber ? "" : customer.phoneNumber));
        parms.Add(new SqlParameter("@Fax", null == customer.fax ? "" : customer.fax));
        parms.Add(new SqlParameter("@Email", null == customer.email ? "" : customer.email));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("CustomerSvc: updated " + customer.customerId);

        return FindById(tran, customer.customerId);
    }

    private static List<Customer> Select(SqlTransaction tran, int id, string macpacCustId, int brandId, string shortCustId)
    {
        string sql = "select CustomerId, MACPACCustomerNumber, SalesRepCompanyName, CreditStatus, "
                   + "       MaxOrderTotal, CreatedByUser, LastUpdateDate, DateCreated,  "
                   + "       DefaultWarehouseId, BrandId, Address1, Address2, City, State, "
                   + "       Zip, Country, Phone, Fax, Email, "
                   + "       (select sum(Total) from TheOrder "
                   + "         where TheOrder.CustomerId = Customer.CustomerId "
                   + "           and CONVERT(varchar, TheOrder.OrderDate, 101) = CONVERT(varchar, getDate(), 101)"
                   + "           and OrderStatusId != -1) as RemainBalance,  "
                   + "       (select BrandName from Brand where Brand.BrandId = Customer.BrandId ) as BrandName "
                   + "  from Customer "
                   + " where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != id ) {
            sql += " and CustomerId = @id ";
            parms.Add(new SqlParameter("@id", id));
        }
        if ( null != macpacCustId ) {
            sql += " and  MACPACCustomerNumber = @MACPACCustomerNumber ";
            parms.Add(new SqlParameter("@MACPACCustomerNumber", macpacCustId)); 
        }

        if ( 0 != brandId ) {
            sql += " and BrandId = @BrandId ";
            parms.Add(new SqlParameter("@BrandId", brandId));
        }
        if ( null != shortCustId ) {
            sql += " and  MACPACCustomerNumber like @ShortId ";
            parms.Add( new SqlParameter("@ShortId", "%-"+shortCustId));
        }

         List<Customer> result = new List<Customer>();
         using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 Customer c = new Customer();
                 c.customerId = rdr.GetInt32(rdr.GetOrdinal("CustomerId"));
                 c.MACPACCustonerNumber = rdr.GetString(rdr.GetOrdinal("MACPACCustomerNumber"));
                 c.salesRepCompanyName = rdr.GetString(rdr.GetOrdinal("SalesRepCompanyName"));
                 c.maxOrderTotal = rdr.GetDecimal(rdr.GetOrdinal("MaxOrderTotal"));
                 c.creditStatus = rdr.GetBoolean(rdr.GetOrdinal("CreditStatus"));
                 c.lastUpdateDate = rdr.GetDateTime(rdr.GetOrdinal("LastUpdateDate"));
                 c.createdByUser = rdr.GetString(rdr.GetOrdinal("CreatedByUser"));
                 c.dateCreated = rdr.GetDateTime(rdr.GetOrdinal("DateCreated"));
                 c.defaultWarehouseId = rdr.GetInt32(rdr.GetOrdinal("DefaultWarehouseId"));
                 c.brandId = rdr.GetInt32(rdr.GetOrdinal("BrandId"));
                 c.phoneNumber = rdr.GetString(rdr.GetOrdinal("Phone"));
                 c.fax = rdr.GetString(rdr.GetOrdinal("Fax"));
                 c.email = rdr.GetString(rdr.GetOrdinal("Email"));
                 c.address = new Address();
                 c.address.name = rdr.GetString(rdr.GetOrdinal("SalesRepCompanyName"));
                 c.address.address1 = rdr.GetString(rdr.GetOrdinal("Address1"));
                 c.address.address2 = rdr.GetString(rdr.GetOrdinal("Address2"));
                 c.address.city = rdr.GetString(rdr.GetOrdinal("City"));
                 c.address.state = rdr.GetString(rdr.GetOrdinal("State"));
                 c.address.country = rdr.GetString(rdr.GetOrdinal("Country"));
                 c.address.zip = rdr.GetString(rdr.GetOrdinal("Zip"));
                 if (rdr.IsDBNull(rdr.GetOrdinal("RemainBalance"))) {
                     c.dayBalance = rdr.GetDecimal(rdr.GetOrdinal("MaxOrderTotal"));                 
                 } else {
                     c.dayBalance = rdr.GetDecimal(rdr.GetOrdinal("MaxOrderTotal")) - rdr.GetDecimal(rdr.GetOrdinal("RemainBalance"));                 
                 }
                 c.brandName = rdr.GetString(rdr.GetOrdinal("BrandName"));
                 result.Add(c);
             }
         }
          
         return result;
    }

};

};
