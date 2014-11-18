using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AerSysCo.Common;
using AerSysCo.Warehouse;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;


namespace CustomerPriceManager
{
class Program
{
    static void Main(string[] args) {
        try {
            using (SqlConnection conn = SQLHelper.CreateConnection() ) {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction() ) {
                    List<Customer> customers = CustomerSvc.GetAll(tran);
                    foreach( Customer cust in customers ) {
                        CustomerPriceSvc.UpdateCustomerPrices(tran, cust);
                        Logger.GetAppLogger().Info(" Customer "+cust.MACPACCustonerNumber+ " price updated ");
                    }
                    tran.Commit();
                }
           }
        } catch (Exception ex) {
            Logger.GetAppLogger().Error("GetCatalogPackage: unexpected exception", ex);
            throw ex;
        }


    }
}
}
