using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using AerSysCo.Entity;
using AerSysCo.Warehouse;
using AerSysCo.Common;
using AerSysCo.CustomerCenter;
using log4net;

namespace AerSysCo.ECatalogManager
{
class Program
{

static void Main(string[] args)
{

    log4net.Config.XmlConfigurator.Configure();

    Program Iam = new Program();
    try { 
        List<Brand> brands = (List<Brand>)TransactionHelper.QueryTransaction(Iam, "GetBrands");
        if ( args.Length != 1 ) {
            ShowUsage(brands);
            return;
        }

        String brandCode = null;
        if ( "ALL".Equals(args[0].ToUpper()) ) {
            brandCode = "ALL";
        } else {
            if ( !CheckParam(args[0], brands) ) {
                Console.WriteLine("Unknown brand '"+args[0]+"'");
                ShowUsage(brands);
                return;
            }
            brandCode = args[0].ToUpper();
            Brand toDo = null;
            foreach(Brand b in brands) {
                if ( b.code.ToUpper().Equals(brandCode) ) {
                    toDo = b;
                    break;
                }
            }
            brands.Clear();
            brands.Add(toDo);
        }

        foreach (Brand brand in brands) {
            CustomerCenterService cs = new CustomerCenterService(brand.brandName, brand.code, brand.imageURLprefix);
            List<BrandModel> models = cs.GetModels();
            foreach(BrandModel model in models) {
                Model m = (Model)TransactionHelper.QueryTransaction(Iam, "GetModel", brand.brandId, model.model);
                if ( null == m ) {
                    continue;
                }
                List<ModelItem> items = (List<ModelItem>)TransactionHelper.QueryTransaction(Iam, "GetModelItems", m.modelId);
                foreach( ModelItem mi in items ) {
                    mi.imageURL = model.imageURL;
                    mi.xmlBullerDescription = model.description;
                    TransactionHelper.Transaction(Iam, "UpdateModelItem", mi);
                }
            }
        }
    } catch (Exception ex ) {
        Logger.GetSysLogger().Fatal("ECatalog manager fail", ex);
    }
    Logger.GetSysLogger().Info("ECatalog manager done");
}

private List<Brand> GetBrands(SqlTransaction tran) {
    return BrandSvc.GetAll(tran);
}

private Model GetModel(SqlTransaction tran, int brandId, string modelName) {
    return ModelSvc.FindByBrandAndName(tran, brandId, modelName);
}

private List<ModelItem> GetModelItems(SqlTransaction tran, int modelId) {
    return ModelItemSvc.GetByModelId(tran, modelId);
}

private void UpdateModelItem(SqlTransaction tran, ModelItem mi) {
    ModelItemSvc.Update(tran, mi);
}

private static void ShowUsage(List<Brand> brands) {
    Console.Write("Use: ALL");
    foreach ( Brand brand in brands ) {
        Console.Write("|"+brand.code);
    }
    Console.WriteLine("");
}

private static bool CheckParam(String param, List<Brand> brands) {
    foreach (Brand brand in brands) {
        if ( param.ToUpper().Equals(brand.code.ToUpper()) ) {
            return true;
        }
    }
    return false;
}

}
}
