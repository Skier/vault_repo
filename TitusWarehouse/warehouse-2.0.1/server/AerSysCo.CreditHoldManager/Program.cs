using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.Warehouse;
using AerSysCo.CustomerCenter;
using AerSysCo.MacPac;
using log4net;

namespace AerSysCo.CreditHoldManager
{
class Program
{
static void Main(string[] args)
{
    log4net.Config.XmlConfigurator.Configure();

    Program Iam = new Program();
    try {
        List<Brand> brands = (List<Brand>)TransactionHelper.QueryTransaction(Iam, "GetAllBrands");
        if ( args.Length != 1 ) {
            ShowUsage(brands);
            return;
        }

        if ( !"ALL".Equals(args[0].ToUpper()) ) {
            Brand toDo = CheckParam(args[0], brands);
            if ( null == toDo ) {
                Console.WriteLine("Unknown brand '"+args[0]+"'");
                ShowUsage(brands);
                return;
            }
            brands.Clear();
            brands.Add(toDo);
        }

        List<Customer> customers = (List<Customer>)TransactionHelper.QueryTransaction(Iam, "GetCustomers", brands);
        foreach (Customer customer in customers) {
            try {
                switch (MacPacService.GetCreditStatus(customer.MACPACCustonerNumber)) {
                   case CreditHold.CREDIT_CLOSED:
                       customer.creditStatus = false;
                       break;
                   case CreditHold.CREDIT_OPEN:
                       customer.creditStatus = true;
                       break;
                   case CreditHold.CREDIT_UNKNOWN:
                       customer.creditStatus = false;
                       break;
                }
                TransactionHelper.Transaction(Iam, "UpdateCustomer", customer);
            } catch (Exception ex) {
                Logger.GetSysLogger().Error(
                    "Cannot update customer '"+customer.MACPACCustonerNumber+"' due to error "+ex.Message , 
                    ex);
            }
        }
    } catch (Exception ex) {
        Logger.GetSysLogger().Fatal("Credit Hold manager fail", ex);
    }
    Logger.GetSysLogger().Info("Credit Hold manager done");

}


private List<Customer> GetCustomers(SqlTransaction tran, List<Brand> brands) {
    List<Customer> customers = CustomerSvc.GetAll(tran);
    List<Customer> result = new List<Customer>();
    foreach (Customer cust in customers) {
        foreach(Brand brand in brands) {
            if ( cust.brandId == brand.brandId ) {
                result.Add(cust);
                break;
            }
        }
    }
    return result;
}

private void UpdateCustomer(SqlTransaction tran, Customer cust) {
    CustomerSvc.Update(tran, cust);
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


private List<Brand> GetAllBrands(SqlTransaction tran) {
    return BrandSvc.GetAll(tran);
}



}
}
