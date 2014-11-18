using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;
using AerSysCo.Warehouse;

namespace AerSysCo.CustomerManager
{
class Program
{
static void Main(string[] args)
{
    log4net.Config.XmlConfigurator.Configure();

    Program Iam = new Program();
    List<Brand> brands = null;
    List<Entity.Warehouse> warehouses = null;
    try { 
        warehouses = (List<Entity.Warehouse>)TransactionHelper.QueryTransaction(Iam, "GetWarehouses");
        if ( 1 > warehouses.Count ) {
            throw new ApplicationException("No warehouses in the database");
        }
        brands = (List<Brand>)TransactionHelper.QueryTransaction(Iam, "GetAllBrands");
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


    } catch (Exception ex) {
        Logger.GetSysLogger().Fatal("Cannot get a list of brands", ex);
        return;
    }

    foreach(Brand brand in brands) {
        Logger.GetAppLogger().Info("Brand "+brand.brandName+" sync started");
        try {
            CustomerCenterService ccs = new CustomerCenterService(brand.brandName, brand.code, brand.imageURLprefix);
            List<Customer> customers = ccs.GetAllCustomers();
            foreach(Customer customer in customers) {
                try {
                    customer.brandId = brand.brandId;
                    Customer our = (Customer)TransactionHelper.QueryTransaction(Iam, "GetCustByMacPacId", customer.MACPACCustonerNumber);
                    if ( null == our ) {
                        customer.maxOrderTotal = 0m;
                        customer.creditStatus = false;
                        customer.defaultWarehouseId = warehouses[0].warehouseId;
                        if ( 3 < customer.address.country.Length ) {
                            customer.address.country = customer.address.country.Substring(0,3);
                        }
                        our = (Customer)TransactionHelper.QueryTransaction(Iam, "InsertCustomer", customer);
                        Logger.GetAppLogger().Info("Customer "+customer.MACPACCustonerNumber+" was inserted");
                    } else {
                        our.phoneNumber = customer.phoneNumber;
                        our.fax = customer.fax;
                        our.email = customer.email;
                        our.salesRepCompanyName = customer.salesRepCompanyName;
                        our.address.address1 = customer.address.address1;
                        our.address.address2 = customer.address.address2;
                        our.address.city = customer.address.city;
                        if ( 3 < customer.address.country.Length ) {
                            our.address.country = customer.address.country.Substring(0,3);
                        }
                        our.address.zip = customer.address.zip;
                        our.address.state = customer.address.state;
                        our = (Customer)TransactionHelper.QueryTransaction(Iam, "UpdateCustomer", our);
                        Logger.GetAppLogger().Info("Customer "+customer.MACPACCustonerNumber+" was updated");
                    }
                } catch (Exception ex) {
                    Logger.GetSysLogger().Error("Customer "+customer.MACPACCustonerNumber+ " sync fail " ,ex);
                }
            }
        } catch (Exception ex) {
            Logger.GetSysLogger().Error("Brand "+brand.brandName+" sync fail ", ex);
        }
        Logger.GetAppLogger().Info("Brand "+brand.brandName+" sync finnished");
    }
}

private Customer GetCustByMacPacId(SqlTransaction tran, string macpacid) {
    return  CustomerSvc.FindByMACPACNumber(tran, macpacid);
}

private List<Brand> GetAllBrands(SqlTransaction tran) {
    return BrandSvc.GetAll(tran);
}

private Customer InsertCustomer(SqlTransaction tran, Customer cust) {
    return CustomerSvc.Insert(tran, cust);
}

private Customer UpdateCustomer(SqlTransaction tran, Customer cust) {
    return CustomerSvc.Update(tran, cust);
}

private List<Entity.Warehouse> GetWarehouses(SqlTransaction tran) {
    return WarehouseSvc.GetAll(tran);
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


}
}
