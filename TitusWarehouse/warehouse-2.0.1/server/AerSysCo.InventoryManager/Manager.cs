using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using AerSysCo.Entity;
using AerSysCo.Warehouse;
using AerSysCo.Common;
using AerSysCo.CustomerCenter;
using AerSysCo.MacPac;


namespace AerSysCo.InventoryManager
{
class Manager
{

private Hashtable brandsMap = new Hashtable();

static void Main(string[] args) {
    log4net.Config.XmlConfigurator.Configure();
    Manager Iam = new Manager();

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
    }

    try {
        Logger.GetSysLogger().Info("Inventory and Price synchronization started");


        List<MACPACInventory> invs = (List<MACPACInventory>)TransactionHelper.QueryTransaction(Iam, "GetNewInventories");
        foreach ( MACPACInventory inv in invs ) {
            try { 
                if ( !"ALL".Equals(brandCode) && !brandCode.Equals(inv.brand.ToUpper()) ) {
                    continue;
                }
                Brand brand = (Brand)TransactionHelper.QueryTransaction(Iam, "GetBrand", inv.brand);
                if ( null == brand ) {
                    Logger.GetAppLogger().Warn("Brand "+inv.brand+" not found. Skip it.");
                    continue;
                }
                Logger.GetAppLogger().Info(string.Format("Started sync of {0} {1} {2} ", inv.part, inv.plant, inv.brand));

                Entity.Warehouse warehouse = (Entity.Warehouse)TransactionHelper.QueryTransaction(Iam, "GetWarehouse", inv.plant);
                Item item = (Item)TransactionHelper.QueryTransaction(Iam, "GetItem", inv.part); 
                if ( null == item ) {
                    item = Iam.MakeItem(inv);
                } else {
                    item = Iam.UpdateItem(item, inv);
                }
                Model model = (Model)TransactionHelper.QueryTransaction(Iam, "GetModel", brand.brandId, inv.model);
                if ( null == model ) {
                    model = Iam.MakeModel(inv, brand);
                }

                ModelItem modelItem = (ModelItem)TransactionHelper.QueryTransaction(Iam, "GetModelItem", model, item); 
                if ( null == modelItem ) {
                    modelItem = Iam.MakeModelItem(inv, model, item, brand);
                } else {
                    modelItem.price = inv.basePrice;
                    modelItem.MACPACInventoryId = inv.macPac_Inventory_id;
                    modelItem.lastUpdateDate = DateTime.Now;
                    modelItem.configuration = inv.configuration;
                    modelItem.isActive = "1".Equals(inv.partStatus);
                    TransactionHelper.Transaction(Iam, "UpdateModelItem", modelItem);
                }

                Inventory inventory = (Inventory)TransactionHelper.QueryTransaction(Iam, "GetInventory", item, warehouse);
                if ( null == inventory ) {
                    inventory = Iam.MakeInventory(inv, item, warehouse);
                } else {
                    inventory.qty = inv.onHand - inv.allocated;
                    inventory.lastUpdateDate = DateTime.Now;
                    inventory.createdByUser = "InventoryManager";
                    TransactionHelper.Transaction(Iam, "UpdateInventory", inventory);
                }

                TransactionHelper.Transaction(Iam, "MarkSuccess", inv);
                Logger.GetAppLogger().Info(string.Format("Finished sync of {0} {1} {2} ", inv.part, inv.plant, inv.brand));
            } catch ( Exception ex ) {
                TransactionHelper.Transaction(Iam, "MarkFail", inv, ex);
                Logger.GetAppLogger().Error(
                    string.Format("Cannot sync of {0} {1} {2} due to error {3}", 
                                  inv.part, inv.plant, inv.brand, ex.Message), 
                    ex);
            }
        }
        foreach( Brand brand in brands ) {
            if ( !"ALL".Equals(brandCode) && !brandCode.Equals(brand.code.ToUpper()) ) {
                continue;
            }
            TransactionHelper.Transaction(Iam, "RemoveEmptyCategoriesByBrand", brand.brandId);
        }
    } catch (Exception ex) {
        Logger.GetSysLogger().Fatal("Inventory Synchronization fail", ex);
    }
    Logger.GetSysLogger().Info(" Inventory and Price synchronization finished");
}

private static bool CheckParam(String param, List<Brand> brands) {
    foreach (Brand brand in brands) {
        if ( param.ToUpper().Equals(brand.code.ToUpper()) ) {
            return true;
        }
    }
    return false;
}

private static void ShowUsage(List<Brand> brands) {
    Console.Write("Use: ALL");
    foreach ( Brand brand in brands ) {
        Console.Write("|"+brand.code);
    }
    Console.WriteLine("");
}

private Manager() {
    List<Brand> brands = (List<Brand>)TransactionHelper.QueryTransaction(this, "GetBrands");
    foreach (Brand brand in brands) {
        CustomerCenterService cs = new CustomerCenterService(brand.brandName, brand.code, brand.imageURLprefix);
        List<BrandModel> models = cs.GetModels();
        foreach(BrandModel model in models) {
            string key = model.brandName+"-"+model.model;
            key = key.ToUpper();
            if ( !brandsMap.Contains(key) ) {
                brandsMap.Add(key, model);
            }
        }
    }
    
}

private List<Brand> GetBrands(SqlTransaction tran) {
    return BrandSvc.GetAll(tran);
}

private Brand GetBrand(SqlTransaction tran, string brandCode) {
    return BrandSvc.FindByCodeSoft(tran, brandCode);
}

private List<MACPACInventory> GetNewInventories(SqlTransaction tran) {
    return MACPACInventorySvc.GetNew(tran);
}

private Entity.Warehouse GetWarehouse(SqlTransaction tran, string warehouse) {
    return WarehouseSvc.FindByCode(tran, warehouse);
}

private Item GetItem(SqlTransaction tran, string sku) {
    return ItemSvc.FindBySku(tran, sku);
}

private Model GetModel(SqlTransaction tran, int brandId, string modelName ) {
    return ModelSvc.FindByBrandAndName(tran, brandId, modelName);
}

private ModelItem GetModelItem(SqlTransaction tran, Model model, Item item) {
    ModelItem result = ModelItemSvc.FindByModelAndItem(tran, model.modelId, item.itemId);
    if ( null == result ) {
         List<ModelItem> items = ModelItemSvc.FindAllByItemAndBrand(tran, model.brandId, item.itemId);
         foreach ( ModelItem mi in items ) {
             if ( mi.isActive ) {
                 mi.isActive = false;
                 ModelItemSvc.Update(tran, mi);
                 List<ShoppingCartDetail> details = ShoppingCartDetailSvc.GetActiveOnlyByModelItemId(tran, mi.modelItemId);
                 foreach ( ShoppingCartDetail detail in details ) {
                     ShoppingCartSvc.UpdateVersion(tran, detail.shoppingCartId, 0);
                     ShoppingCartDetailSvc.RemoveShoppingCartDetail(tran, detail.shoppingCartDetailId);
                 }
             }
             if ( mi.modelId == model.modelId ) {
                 mi.isActive = true;
                 ModelItemSvc.Update(tran, mi);
                 result = mi;
             }
         }
    }
    return result;
}

private void UpdateModelItem(SqlTransaction tran, ModelItem modelItem) {
    ModelItemSvc.Update(tran, modelItem);
}

private Inventory GetInventory(SqlTransaction tran, Item item, Entity.Warehouse warehouse) {
    return InventorySvc.FindByItemWarehouse(tran, item.itemId, warehouse.warehouseId);
}


private Item MakeItem(MACPACInventory inv) {
    Item item = new Item();
    item.sku = inv.part;
    item.description = inv.partDesc;
    item.qtyIncrement = Decimal.ToInt32(inv.qtypercontainer);
    item.length = Decimal.ToDouble(inv.depth);
    item.width = Decimal.ToDouble(inv.width);
    item.height = Decimal.ToDouble(inv.height);
    item.weight = Decimal.ToDouble(inv.partweight);
    item.isActive = true;
    item.dateCreated = DateTime.Now;
    item.createdByUser = "InventoryManager";
    item.lastUpdateDate = DateTime.Now;

    return (Item)TransactionHelper.QueryTransaction(this, "InsertItem", item);
}

private Item UpdateItem(Item item, MACPACInventory inv) {
    item.description = inv.partDesc;
    item.qtyIncrement = Decimal.ToInt32(inv.qtypercontainer);
    item.length = Decimal.ToDouble(inv.depth);
    item.width = Decimal.ToDouble(inv.width);
    item.height = Decimal.ToDouble(inv.height);
    item.weight = Decimal.ToDouble(inv.partweight);
    item.isActive = true;
    item.lastUpdateDate = DateTime.Now;

    return (Item)TransactionHelper.QueryTransaction(this, "ItemUpdate", item);
}


private Model MakeModel(MACPACInventory inv, Brand brand) {
    string key = brand.brandName+"-"+inv.model;
    key = key.ToUpper();
    if ( !brandsMap.Contains(key) ) {
        string msg = string.Format("Cannot create model for {0}, {1}. Definition in Ecatalog not found.", brand.brandName, inv.model);
        Logger.GetAppLogger().Error(msg);
        // FIXME: update inventory
        throw new ApplicationException(msg);
    }
    BrandModel bmodel = (BrandModel)brandsMap[key];
    Category targetCategory = null;
    Category cat1 = (Category)TransactionHelper.QueryTransaction(this, "GetCategory", 0, brand.brandId, bmodel.category1);
    if ( null == cat1 ) {
        cat1 = new Category();
        cat1.BrandId = brand.brandId;
        cat1.createdByUser = "InventoryManager";
        cat1.Name = bmodel.category1;
        cat1.ParentCategoryId = 0;
        cat1.lastUpdateDate = DateTime.Now;
        cat1.dateCreated = DateTime.Now;
        cat1 = (Category) TransactionHelper.QueryTransaction(this, "InsertCategory", cat1);
    }
    targetCategory = cat1;
    if ( 0 != bmodel.category2.Trim().Length ) {
        Category cat2 = (Category)TransactionHelper.QueryTransaction(this, "GetCategory", cat1.CategoryId, brand.brandId, bmodel.category2);
        if ( null == cat2 ) {
            cat2 = new Category();
            cat2.BrandId = brand.brandId;
            cat2.createdByUser = "InventoryManager";
            cat2.Name = bmodel.category2;
            cat2.ParentCategoryId = cat1.CategoryId;
            cat2.lastUpdateDate = DateTime.Now;
            cat2.dateCreated = DateTime.Now;
            cat2 = (Category) TransactionHelper.QueryTransaction(this, "InsertCategory", cat2);
        }
        targetCategory = cat2;
        if (  0 != bmodel.category3.Trim().Length ) {
            Category cat3 = (Category)TransactionHelper.QueryTransaction(this, "GetCategory", cat2.CategoryId, brand.brandId, bmodel.category3);
            if ( null == cat3 ) {
                cat3 = new Category();
                cat3.BrandId = brand.brandId;
                cat3.createdByUser = "InventoryManager";
                cat3.Name = bmodel.category3;
                cat3.ParentCategoryId = cat2.CategoryId;
                cat3.lastUpdateDate = DateTime.Now;
                cat3.dateCreated = DateTime.Now;
                cat3 = (Category) TransactionHelper.QueryTransaction(this, "InsertCategory", cat3);
            }
            targetCategory = cat3;
        }
    }

    Model result = new Model();
    result.brandId = brand.brandId;
    result.categoryId = targetCategory.CategoryId;
    result.isActive = true;
    result.modelName = inv.model;
    result.dateCreated = DateTime.Now;
    result.createdByUser = "InventoryManager";
    result.lastUpdateDate = DateTime.Now;

    result = (Model)TransactionHelper.QueryTransaction(this, "InsertModel", result);
    return result;
}

private Model InsertModel(SqlTransaction tran, Model model) {
    return ModelSvc.Insert(tran, model);
}

private Category InsertCategory(SqlTransaction tran, Category cat) {
    return CategorySvc.Insert(tran, cat);
}

private ModelItem MakeModelItem(MACPACInventory inv, Model model, Item item, Brand brand) {
    string key = brand.brandName+"-"+inv.model;
    key = key.ToUpper();
    if ( !brandsMap.Contains(key) ) {
        string msg = string.Format("Cannot create model for {0}, {1}. Definition in Ecatalog not found.", brand.brandName, inv.model);
        Logger.GetAppLogger().Error(msg);
        // FIXME: update inventory
        throw new ApplicationException(msg);
    }
    BrandModel bmodel = (BrandModel)brandsMap[key];

    ModelItem mi = new ModelItem();
    mi.modelId = model.modelId;
    mi.itemId = item.itemId;
    mi.configuration = inv.configuration;
    mi.price = inv.basePrice;
    mi.isActive = "1".Equals(inv.partStatus);
    mi.imageURL = bmodel.imageURL;
    mi.xmlBullerDescription = bmodel.description;
    mi.MACPACInventoryId = inv.macPac_Inventory_id;    

    mi = (ModelItem)TransactionHelper.QueryTransaction(this, "InsertModelItem", mi);
    return mi;
}

private ModelItem InsertModelItem(SqlTransaction tran, ModelItem mi) {
    return ModelItemSvc.Insert(tran, mi);
}

private Inventory MakeInventory(MACPACInventory inv, Item item, Entity.Warehouse warehouose) {
    Inventory inventory = new Inventory();
    inventory.itemId = item.itemId;
    inventory.MacPac_Inventory_id = inv.macPac_Inventory_id;
    inventory.warehouseId = warehouose.warehouseId;
    inventory.qty = inv.onHand - inv.allocated;
    inventory.lastUpdateDate = DateTime.Now;
    inventory.createdByUser = "InventoryManager";
    inventory.dateCreated = DateTime.Now;
    return (Inventory)TransactionHelper.QueryTransaction(this, "InsertInventory", inventory);
}

private Inventory InsertInventory(SqlTransaction tran, Inventory inventory) {
    return InventorySvc.Insert(tran, inventory);
}

private Inventory UpdateInventory(SqlTransaction tran, Inventory inventory) {
    return InventorySvc.Update(tran, inventory);
}

private Item InsertItem(SqlTransaction tran, Item item) {
    return ItemSvc.Insert(tran, item);
}

private Item ItemUpdate(SqlTransaction tran, Item item) {
    return ItemSvc.Update(tran, item);
}

private Category GetCategory(SqlTransaction tran, int parentId, int brandId, string name) {
    return CategorySvc.FindByName(tran, name, parentId, brandId);
}

private void MarkFail(SqlTransaction tran, MACPACInventory inv, Exception ex) {
    MACPACInventorySvc.MarkFail(tran, inv.macPac_Inventory_id, ex.Message);
}

private void MarkSuccess(SqlTransaction tran, MACPACInventory inv) {
    MACPACInventorySvc.MarkSuccess(tran, inv.macPac_Inventory_id);
}

private void RemoveEmptyCategoriesByBrand(SqlTransaction tran, int brandId) {
    for(;;) {
        List<Category> categories = CategorySvc.GetByBrand(tran, brandId);
        if ( 0 == RemoveEmptyCategories(tran, categories) ) {
            break;
        }
    }
}

private int RemoveEmptyCategories(SqlTransaction tran, List<Category> categories) {
    int result = 0;
    foreach( Category category in categories ) {
        if ( CategorySvc.IsEmpty(tran, category.CategoryId) ) { 
            CategorySvc.Delete(tran,  category.CategoryId);
            result++;
        }
    }
    return result;
}

}
}
