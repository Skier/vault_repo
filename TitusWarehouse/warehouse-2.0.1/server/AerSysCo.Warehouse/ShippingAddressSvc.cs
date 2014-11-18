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

public class ShippingAddressSvc {

    public static ShippingAddress FindById(SqlTransaction tran, int id){
        Logger.ASSERT(0 != id);
        List<ShippingAddress> result = Select(tran, id, 0);
        if ( 0 == result.Count ) {
            string message = string.Format("No addresse with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);

        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} addresses with id {1} ", result.Count, id);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static List<ShippingAddress> GetByCustomerId(SqlTransaction tran, int customerId) {
        Logger.ASSERT(0 != customerId);
        return Select(tran, 0, customerId);
    }

    public static List<ShippingAddress> GetByCustomer(SqlTransaction tran, Customer customer) {
        return GetByCustomerId(tran, customer.customerId);
    }

    public static ShippingAddress Insert(SqlTransaction tran, ShippingAddress address)  {
         string sql = 
              " insert into CustomerShippingAddress (CustomerId, Name, Address1, Address2, City, State, Zip, Country, CreatedByUser, DateCreated, LastUpdateDate ) "
             +" values (@CustomerId, @Name, @Address1, @Address2, @City, @State, @Zip, @Country, @CreatedByUser, @DateCreated, @LastUpdateDate ) ";
         List<SqlParameter> parms = new List<SqlParameter>();
         parms.Add(new SqlParameter("@CustomerId", address.customerId));
         parms.Add(new SqlParameter("@Name", address.name));
         parms.Add(new SqlParameter("@Address1", address.address1 )); 
         parms.Add(new SqlParameter("@Address2", null != address.address2 ? address.address2 : "" ));
         parms.Add(new SqlParameter("@City", address.city ));
         parms.Add(new SqlParameter("@State", null != address.state ? address.state : ""));
         parms.Add(new SqlParameter("@Zip", address.zip ));
         parms.Add(new SqlParameter("@Country", null != address.country ? address.country : "USA"));
         parms.Add(new SqlParameter("@CreatedByUser", address.createdByUser ));
         parms.Add(new SqlParameter("@DateCreated", DateTime.Now ));
         parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now ));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        address.addressId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("ShippingAddress: created " + address.name );
        return  FindById(tran, address.addressId);
    }

    public static ShippingAddress Update(SqlTransaction tran, ShippingAddress address)  {
         string sql = 
              " update CustomerShippingAddress " 
             +"    set Name = @Name, "
             +"        Address1 = @Address1, "     
             +"        Address2 = @Address2, "       
             +"        City = @City, "           
             +"        State = @State, "          
             +"        Zip = @Zip, "            
             +"        Country = @Country, "        
             +"        LastUpdateDate = @LastUpdateDate " 
             +"  where AddressId = @AddressId";

         List<SqlParameter> parms = new List<SqlParameter>();
         parms.Add(new SqlParameter("@AddressId", address.addressId));
         parms.Add(new SqlParameter("@Name", address.name));
         parms.Add(new SqlParameter("@Address1", address.address1 )); 
         parms.Add(new SqlParameter("@Address2", null != address.address2 ? address.address2 : "" ));
         parms.Add(new SqlParameter("@City", address.city ));
         parms.Add(new SqlParameter("@State", null != address.state ? address.state : ""));
         parms.Add(new SqlParameter("@Zip", address.zip ));
         parms.Add(new SqlParameter("@Country", null != address.country ? address.country : "USA"));
         parms.Add(new SqlParameter("@LastUpdateDate", DateTime.Now ));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("ShippingAddress: updated " + address.name );
        return  FindById(tran, address.addressId);
    }

    public static int CompareById(ShippingAddress addr1, ShippingAddress addr2) {
        return addr2.addressId - addr1.addressId;
    }

    private static List<ShippingAddress> Select(SqlTransaction tran, int id, int customerId) {
        string sql = " select AddressId, CustomerId, Address1, Address2, City, State, Zip, Country, "
                   + "        CreatedByUser, DateCreated, LastUpdateDate, Name  " 
                   + "   from CustomerShippingAddress "
                   + "  where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != id ) {
            sql += " and AddressId = @id ";
            SqlParameter param = new SqlParameter("@id", id);
            parms.Add(param);
        }
        if ( 0 != customerId ) {
            sql += " and CustomerId = @customerid ";
            sql += " and exists ( select 'x' from ShoppingCart where ShoppingCart.ShippingAddressId = CustomerShippingAddress.AddressId ) ";
            SqlParameter param = new SqlParameter("@customerid", customerId);
            parms.Add(param);
        }
        List<ShippingAddress> result = new List<ShippingAddress>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            while( rdr.Read() ) {
                ShippingAddress addr = new ShippingAddress();
                addr.address1 = rdr.GetString(rdr.GetOrdinal("Address1"));
                addr.address2 = rdr.GetString(rdr.GetOrdinal("Address2"));
                addr.city = rdr.GetString(rdr.GetOrdinal("City"));
                addr.country = rdr.GetString(rdr.GetOrdinal("Country"));
                addr.zip = rdr.GetString(rdr.GetOrdinal("Zip"));
                addr.state = rdr.GetString(rdr.GetOrdinal("State"));
                addr.lastUpdateDate = rdr.GetDateTime(rdr.GetOrdinal("LastUpdateDate"));
                addr.createdByUser = rdr.GetString(rdr.GetOrdinal("CreatedByUser"));
                addr.dateCreated = rdr.GetDateTime(rdr.GetOrdinal("DateCreated"));
                addr.addressId = rdr.GetInt32(rdr.GetOrdinal("AddressId"));
                addr.customerId = rdr.GetInt32(rdr.GetOrdinal("CustomerId"));
                addr.name = rdr.GetString(rdr.GetOrdinal("Name"));
                result.Add(addr);
            }
        }
        return result;
    }
};

};