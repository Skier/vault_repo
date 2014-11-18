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
public class CustomerPriceSvc
{

    public static CustomerPrice FindByKey(SqlTransaction tran, int customerId, int modelItemId) {
        Logger.ASSERT( 0 != customerId );
        Logger.ASSERT( 0 != modelItemId );
        List<CustomerPrice> result = Select(tran, customerId, modelItemId);
        if ( 0 == result.Count ) {
            return null;
        }
        return result[0];
    }

    public static List<CatalogItem> FillCustomerPrices(SqlTransaction tran, int customerId, List<CatalogItem> items) {
        Logger.ASSERT( 0 != customerId );
        foreach(CatalogItem item in items) {
            item.customerId = customerId;
            item.modelItem = FillCustomerPrice(tran, customerId, item.modelItem);
        }
        return items;
    }

    public static ModelItem FillCustomerPrice(SqlTransaction tran, int customerId, ModelItem modelItem) {
        Logger.ASSERT( 0 != customerId );
        CustomerPrice price = CustomerPriceSvc.FindByKey(tran, customerId, modelItem.modelItemId);
        if ( null != price ) {
            modelItem.price = Decimal.Round(Decimal.Multiply(modelItem.price, new Decimal(price.multiplier)), 3);
        }
        return modelItem;
    }

    public static void DeleteAll(SqlTransaction tran) {
        string sql = " delete from CustomerPrice ";

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, new SqlParameter[0]{});

    }
    public static void DeleteByCustomerId(SqlTransaction tran, int customerId) {
        string sql = " delete from CustomerPrice  where CustomerId = @CustomerId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add( new SqlParameter("@CustomerId", customerId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
    }

    public static CustomerPrice Insert(SqlTransaction tran, CustomerPrice price) {
        string sql = 
            " insert into CustomerPrice (CustomerId, ModelItemId, Multiplier, MarketingProgram) "
           +"  values (@CustomerId, @ModelItemId, @Multiplier, @MarketingProgram) ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add( new SqlParameter("@CustomerId", price.customerId));
        parms.Add( new SqlParameter("@ModelItemId", price.modelItemId));
        parms.Add( new SqlParameter("@Multiplier", price.multiplier));
        parms.Add( new SqlParameter("@MarketingProgram", price.marketingProgram));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        return price;
    }

    public static CustomerPrice Update(SqlTransaction tran, CustomerPrice price) {
        string sql = " update CustomerPrice "
                    +"    set Multiplier = @Multiplier "
                    +"  where CustomerId = @CustomerId and ModelItemId = @ModelItemId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add( new SqlParameter("@CustomerId", price.customerId));
        parms.Add( new SqlParameter("@ModelItemId", price.modelItemId));
        parms.Add( new SqlParameter("@Multiplier", price.multiplier));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        return price;
    }

    private static List<CustomerPrice> Select(SqlTransaction tran, int customerId, int modelItemId) {
        string sql = " select CustomerId, ModelItemId, Multiplier, MarketingProgram  " 
                   + "   from CustomerPrice "
                   + "  where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != customerId ) {
            sql += " and CustomerId = @CustomerId ";
            parms.Add(new SqlParameter("@CustomerId", customerId));
        }
        if ( 0 != modelItemId ) {
            sql += " and ModelItemId = @ModelItemId";
            parms.Add(new SqlParameter("@ModelItemId", modelItemId));
        }
        List<CustomerPrice> result = new List<CustomerPrice>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
             while( rdr.Read() ) {
                 result.Add(FromReader(rdr));
             }
        }
        return result;

    }

    private static CustomerPrice FromReader(SqlDataReader rdr) {
        CustomerPrice result = new CustomerPrice();
        result.customerId = rdr.GetInt32(rdr.GetOrdinal("CustomerId"));
        result.modelItemId = rdr.GetInt32(rdr.GetOrdinal("ModelItemId"));
        result.multiplier = rdr.GetDouble(rdr.GetOrdinal("Multiplier"));
        result.marketingProgram = rdr.GetString(rdr.GetOrdinal("MarketingProgram"));
        return result;
    }
}
}
