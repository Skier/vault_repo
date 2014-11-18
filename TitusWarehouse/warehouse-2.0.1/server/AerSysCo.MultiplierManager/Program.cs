using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.Warehouse;
using AerSysCo.CustomerCenter;
using log4net;

namespace AerSysCo.MultiplierManager
{
class Program
{


static void Main(string[] args)
{
    log4net.Config.XmlConfigurator.Configure();

    Program Iam = new Program();
    try {
        List<Brand> brands = (List<Brand>)TransactionHelper.QueryTransaction(Iam, "GetBrands");

        List<Customer> customers = (List<Customer>)TransactionHelper.QueryTransaction(Iam, "GetCustomers");

        if ( args.Length != 1 ) {
            ShowUsage(brands);
            return;
        }

        String brandCode = null;
        Brand brand = null;
        if ( "ALL".Equals(args[0].ToUpper()) ) {
            brandCode = "ALL";
        } else {
            brand = CheckParam(args[0], brands);
            if ( null == brand ) {
                Console.WriteLine("Unknown brand '"+args[0]+"'");
                ShowUsage(brands);
                return;
            }
            brandCode = args[0].ToUpper();
        }

        foreach(Customer cust in customers ) {
            try {
                if ( !("ALL".Equals(brandCode) || (null != brand && brand.brandId == cust.brandId ) ) ) {
                    continue;
                }

                TransactionHelper.Transaction(Iam, "SyncCustomer", cust);
            } catch ( Exception ex ) {
                Logger.GetAppLogger().Error("Fail to update multiplier for customer "+cust.MACPACCustonerNumber+" due to error "+ex.Message, ex );
            }
        }
        Logger.GetSysLogger().Info("Multipliers synchronization done");
    } catch (Exception ex) {
        Logger.GetSysLogger().Fatal("Multipliers Synchronization fail", ex);
    }
}


private List<Customer> GetCustomers(SqlTransaction tran) {
    return CustomerSvc.GetAll(tran);
}

private void SyncCustomer(SqlTransaction tran, Customer cust) {
    CustomerPriceSvc.DeleteByCustomerId(tran, cust.customerId);
    Brand brand = BrandSvc.FindById(tran, cust.brandId);
    List<MACPACMultiplier> mults = MACPACInventorySvc.GetMultipliers(tran, brand.code, CustomerSvc.GetShortAccountNo(cust.MACPACCustonerNumber));
    foreach(MACPACMultiplier mult in mults) {
        Item item = ItemSvc.FindBySku(tran, mult.sku);
        ModelItem mi = ModelItemSvc.FindByItemAndBrandSoft(tran, brand.brandId, item.itemId);
        if ( null != mi ) {
            CustomerPrice cp = new CustomerPrice();
            cp.customerId = cust.customerId;
            cp.modelItemId = mi.modelItemId;
            cp.multiplier = Decimal.ToDouble(mult.multiplier);
            cp.marketingProgram = mult.marketingProgram;
            try {
                CustomerPriceSvc.Insert(tran, cp);
                Logger.GetAppLogger().Debug("Multiplier for '"+cust.MACPACCustonerNumber+ "' for '"+mult.sku+"' was updated" );
            } catch ( Exception ex ) {
                Logger.GetAppLogger().Error("Fail to update multiplier for "+cust.MACPACCustonerNumber+ " for "+mult.sku+" due to error "+ex.Message, ex );
            } 
        }
    }
}


private static void ShowUsage(List<Brand> brands) {
    Console.Write("Use: ALL");
    foreach ( Brand brand in brands ) {
        Console.Write("|"+brand.code);
    }
    Console.WriteLine("");
}

private static Brand CheckParam(String param, List<Brand> brands) {
    foreach (Brand brand in brands) {
        if ( param.ToUpper().Equals(brand.code.ToUpper()) ) {
            return brand;
        }
    }
    return null;
}

private List<Brand> GetBrands(SqlTransaction tran) {
    return BrandSvc.GetAll(tran);
}


}
}
