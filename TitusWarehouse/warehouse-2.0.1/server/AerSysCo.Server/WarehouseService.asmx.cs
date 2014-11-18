using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using log4net;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.Warehouse;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Server
{

public class WarehouseService : WebService
{
    static WarehouseService() {
        log4net.Config.XmlConfigurator.Configure();
    }

    public WarehouseService() {
        InitializeComponent();
    }

    #region Component Designer generated code

    //Required by the Web Services Designer 
    private IContainer components = null;
        
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing ) {
        if(disposing && components != null) {
            components.Dispose();
        }
        base.Dispose(disposing);        
    }

    #endregion    

    [WebMethod]
    public SalesRepResult GetSalesRep(Context context) {
         SalesRepResult result = new SalesRepResult();
         try {
             TransactionHelper.Transaction(this, "GetSalesRepTx", context, result);
         } catch ( Exception ex) {
             result.result.status = RequestResult.FAIL;
             Logger.GetAppLogger().Error(ex.Message, ex);
         }
         return result;
    }

    public void GetSalesRepTx(SqlTransaction tran, Context context, SalesRepResult result) {
        try {
            result.salesRep = CustomerSvc.GetSalesRep(tran, context.BrendId, context.Username);
            result.salesRep.customerList.Sort(CustomerSvc.CompareByName);
            Logger.GetAppLogger().Debug("GetCatalogPackage: SalesRep  " + result.salesRep.customerList.Count);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        } catch (SalesRepNotFoundException) {
            result.salesRep = new SalesRep();
            result.salesRep.SalesRepId = -1;
            result.result.status = RequestResult.ERROR;
            result.result.message = "User not found";
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public CatalogPackageResult GetCatalogPackage(Context context) {
        CatalogPackageResult result = new CatalogPackageResult();
        try {
            TransactionHelper.Transaction(this, "GetCatalogPackageTx", context, result);
        } catch (Exception ex) {
             result.result.status = RequestResult.FAIL;
             Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetCatalogPackageTx(SqlTransaction tran, Context context, CatalogPackageResult result) {
        result.pack = new CatalogPackage();
        try {
            result.pack.warehouseList = WarehouseSvc.GetAllActive(tran);
            Logger.GetAppLogger().Debug("GetCatalogPackage: warehouses " + result.pack.warehouseList.Count);
            result.pack.categoryList = CategorySvc.GetByBrand(tran, context.BrendId);
            Logger.GetAppLogger().Debug("GetCatalogPackage: categories " + result.pack.categoryList.Count);
            result.pack.modelList = ModelSvc.GetByBrandId(tran, context.BrendId);
            Logger.GetAppLogger().Debug("GetCatalogPackage: models " + result.pack.modelList.Count);
            Brand brand = BrandSvc.FindById(tran, context.BrendId);
            result.pack.defaultCategory = ConfigurationManager.AppSettings[brand.brandName.ToLower()+"_default_category"];
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public CatalogItemResult GetItemsByCategory(Context context, int customerId, int categoryId) {
        CatalogItemResult result = new CatalogItemResult();
        try {
            TransactionHelper.Transaction(this, "GetItemsByCategoryTx", context, customerId, categoryId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetItemsByCategoryTx(SqlTransaction tran, Context context, int customerId, 
                                                  int categoryId, CatalogItemResult result) 
    {
        try {
            result.items = CatalogItemSvc.GetByCategory(tran, categoryId);
            result.items = CustomerPriceSvc.FillCustomerPrices(tran, customerId, result.items);
            result.items.Sort(CatalogItemSvc.CompareBySku);
            Logger.GetAppLogger().Debug("GetItemsByCategory: results " + result.items.Count);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }


    [WebMethod]
    public CatalogItemResult GetItemsByModel(Context context, int customerId, int modelId) {
        CatalogItemResult result = new CatalogItemResult();
        try {
            TransactionHelper.Transaction(this, "GetItemsByModelTx", context, customerId, modelId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetItemsByModelTx(SqlTransaction tran, Context context, int customerId, 
                                               int modelId, CatalogItemResult result) 
    {
        try {
            result.items = CatalogItemSvc.GetByModel(tran, modelId);
            Logger.GetAppLogger().Debug("GetItemsByModel: results " + result.items.Count);
            result.items = CustomerPriceSvc.FillCustomerPrices(tran, customerId, result.items);
            result.items.Sort(CatalogItemSvc.CompareBySku);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public CatalogItemResult SearchItems(Context context, int customerId, string search) {
        CatalogItemResult result = new CatalogItemResult();
        try {
            TransactionHelper.Transaction(this, "SearchItemsTx", context, customerId, search, result); 
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void SearchItemsTx(SqlTransaction tran, Context context, int customerId, 
                                           string search, CatalogItemResult result) 
    {
        try {
            result.items = CatalogItemSvc.Search(tran, context.BrendId, search);
            Logger.GetAppLogger().Debug("Search: results " + result.items.Count);
            result.items = CustomerPriceSvc.FillCustomerPrices(tran, customerId, result.items);
            result.items.Sort(CatalogItemSvc.CompareBySku);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public ShoppingCartResult GetShoppingCart(Context context, int customerId) {
        ShoppingCartResult result = new ShoppingCartResult();
        try {
            TransactionHelper.Transaction(this, "GetShoppingCartTx", context, customerId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetShoppingCartTx(SqlTransaction tran, Context context, int customerId, ShoppingCartResult result) {
        try { 
            result.cart = ShoppingCartSvc.FindByCustomer(tran, customerId, context.BrendId, context.Username);
            if ( null == result.cart ) {
                Brand b = BrandSvc.FindById(tran, context.BrendId);
                CustomerCenterService cc = new CustomerCenterService(b.brandName, b.code, b.imageURLprefix);
                SalesRep rep = cc.GetUserShort(context.Username);
                ShoppingCart cart = new ShoppingCart();
                cart.customerId = customerId;
                cart.brandId = context.BrendId;
                cart.ipAddress = "127.0.0.1"; // to do: fix me
                cart.total = 0.00M;
                cart.shippingTotalAllWarehouses = 0.00M;
                cart.grandTotal = 0.00M;
                cart.dateCreated = DateTime.Now;
                cart.createdByUser = context.Username;
                cart.lastUpdateDate = DateTime.Now;
                cart.repAccountNo = context.Username; //FIXME 
                cart.salesPerson = rep.LastName;

                result.cart = ShoppingCartSvc.Insert(tran, cart);

                List<Entity.Warehouse> warehouses = WarehouseSvc.GetAllActive(tran);
                foreach ( Entity.Warehouse w in warehouses ) {
                    ShoppingCartShipment s = new ShoppingCartShipment();
                    s.shoppingCartId = result.cart.shoppingCartId;
                    s.shippingTotal = 0.0m;
                    s.warehouseId = w.warehouseId;
                    s.dateCreated = DateTime.Now;
                    s.createdByUser = context.Username;
                    s.lastUpdateDate = DateTime.Now;
                    s.needLiftGate = false;
                    s.liftGatePrice = 0m;
                    s = ShoppingCartShipmentSvc.Insert(tran, s);
                    result.cart.shipments.Add(s);
                }
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public ShoppingCartDetailResult SaveShoppingCartDetail(Context context, ShoppingCartDetail detail, int version) {
        ShoppingCartDetailResult result = new ShoppingCartDetailResult();
        try {
            TransactionHelper.Transaction(this, "SaveShoppingCartDetailTx", context, detail, version, result);
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void SaveShoppingCartDetailTx(SqlTransaction tran, Context context, ShoppingCartDetail detail, int version, ShoppingCartDetailResult result) {
        result.version = ShoppingCartSvc.UpdateVersion(tran, detail.shoppingCartId, version);
        try {
            detail.lastUpdateDate = DateTime.Now;
            if ( 0 == detail.shoppingCartDetailId ) {
                detail.createdByUser = context.Username;
                detail.dateCreated = DateTime.Now;
                Logger.GetAppLogger().Debug("Insert ShoppingCartDetail: called.");
                result.detail = ShoppingCartDetailSvc.InsertShoppingCartDetail(tran, detail);
            } else {
                Logger.GetAppLogger().Debug("Update ShoppingCartDetail: called.");
                result.detail = ShoppingCartDetailSvc.UpdateShoppingCartDetail(tran, detail);
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public ShoppingCartDetailListResult SaveShoppingCartDetails(Context context, List<ShoppingCartDetail> details, int version) {
        ShoppingCartDetailListResult result = new ShoppingCartDetailListResult();
        try {
            TransactionHelper.Transaction(this, "SaveShoppingCartDetailsTx", context, details, version, result);
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void SaveShoppingCartDetailsTx(SqlTransaction tran, Context context, 
                                          List<ShoppingCartDetail> details, 
                                          int version,
                                          ShoppingCartDetailListResult result) 
    {
        try {
            result.version = version;
            foreach(ShoppingCartDetail detail in details) {
                result.version = ShoppingCartSvc.UpdateVersion(tran, detail.shoppingCartId, result.version);
                detail.lastUpdateDate = DateTime.Now;
                if ( 0 == detail.shoppingCartDetailId ) {
                    detail.createdByUser = context.Username;
                    detail.dateCreated = DateTime.Now;
                    Logger.GetAppLogger().Debug("Insert ShoppingCartDetails: called.");
                    result.details.Add(ShoppingCartDetailSvc.InsertShoppingCartDetail(tran, detail));
                } else {
                    Logger.GetAppLogger().Debug("Update ShoppingCartDetails: called.");
                    result.details.Add(ShoppingCartDetailSvc.UpdateShoppingCartDetail(tran, detail));
                }
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public VersionResult RemoveShoppingCartDetail(Context context, int shoppingCartDetailId, int version) {
        VersionResult result = new VersionResult();
        try {
            TransactionHelper.Transaction(this, "RemoveShoppingCartDetailTx", context, shoppingCartDetailId, version, result); 
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void RemoveShoppingCartDetailTx(SqlTransaction tran, Context context, 
                                           int shoppingCartDetailId, int version, 
                                           VersionResult result) 
    {
        try {
            ShoppingCartDetail detail = ShoppingCartDetailSvc.GetById(tran, shoppingCartDetailId);
            result.version = ShoppingCartSvc.UpdateVersion(tran, detail.shoppingCartId, version);
            ShoppingCartDetailSvc.RemoveShoppingCartDetail(tran, shoppingCartDetailId);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public VersionResult RemoveShoppingCartDetails(Context context, List<ShoppingCartDetail> details, int version) {
        VersionResult result = new VersionResult();
        try {
            TransactionHelper.Transaction(this, "RemoveShoppingCartDetailsTx", context, details, version, result);
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void RemoveShoppingCartDetailsTx(SqlTransaction tran, Context context, 
                                            List<ShoppingCartDetail> details, int version, 
                                            VersionResult result) 
    {
        try {
            result.version = version;
            foreach (ShoppingCartDetail detail in details) {
                result.version = ShoppingCartSvc.UpdateVersion(tran, detail.shoppingCartId, result.version);
                ShoppingCartDetailSvc.RemoveShoppingCartDetail(tran, detail.shoppingCartDetailId);
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public VersionResult RemoveAllShoppingCartDetails(Context context, int shoppingCartId, int version) {
        VersionResult result = new VersionResult();
        try {
            TransactionHelper.Transaction(this, "RemoveAllShoppingCartDetailsTx", context, shoppingCartId, version, result);
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void RemoveAllShoppingCartDetailsTx(SqlTransaction tran,Context context, int shoppingCartId, int version, VersionResult result) {
        result.version = ShoppingCartSvc.UpdateVersion(tran, shoppingCartId, version);
        List<ShoppingCartDetail> details = ShoppingCartDetailSvc.GetByShoppingCartId(tran, shoppingCartId);
        try {
            foreach (ShoppingCartDetail detail in details) {
                ShoppingCartDetailSvc.RemoveShoppingCartDetail(tran, detail.shoppingCartDetailId);
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]    
    public ShippingAddressResult SaveShippingAddress(Context context, ShippingAddress address) {
        ShippingAddressResult result = new ShippingAddressResult();
        try { 
            TransactionHelper.Transaction(this, "SaveShippingAddressTx", context, address, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void SaveShippingAddressTx(SqlTransaction tran, Context context, 
                                                       ShippingAddress address, ShippingAddressResult result) 
    {
        try {
            if ( 0 == address.addressId ) {
                result.address = ShippingAddressSvc.Insert(tran, address);
            } else {
                result.address = ShippingAddressSvc.Update(tran, address);
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]    
    public ShippingAddressListResult GetAddressesByCustomerId(Context context, int customerId) {
        ShippingAddressListResult result = new ShippingAddressListResult();
        try {
            TransactionHelper.Transaction(this, "GetAddressesByCustomerIdTx", context, customerId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;

    }
    public void GetAddressesByCustomerIdTx(SqlTransaction tran, Context context, int customerId, ShippingAddressListResult result) {
        try {
            result.addresses = ShippingAddressSvc.GetByCustomerId(tran, customerId);
            result.addresses.Sort(ShippingAddressSvc.CompareById);
            result.addresses = result.addresses.GetRange(0, Math.Min(20, result.addresses.Count));
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]    
    public ShoppingCartResult SaveShoppingCart(Context context, ShoppingCart cart) {
        ShoppingCartResult result = new ShoppingCartResult();
        try {
            TransactionHelper.Transaction(this, "SaveShoppingCartTx", context, cart, result);
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void SaveShoppingCartTx(SqlTransaction tran, Context context, ShoppingCart cart, 
                                                 ShoppingCartResult result) 
    {
        try {
            Brand b = BrandSvc.FindById(tran, context.BrendId);
            CustomerCenterService cc = new CustomerCenterService(b.brandName, b.code, b.imageURLprefix);
            SalesRep rep = cc.GetUserShort(context.Username);
            cart.salesPerson = rep.LastName;
            result.cart = ShoppingCartSvc.Update(tran, cart);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]    
    public ShipmentShippingOptionsResult GetShippingOptions(Context context, int cartId) {
        ShipmentShippingOptionsResult result = new ShipmentShippingOptionsResult();
        try { 
            TransactionHelper.Transaction(this, "GetShippingOptionsTx" , context, cartId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void GetShippingOptionsTx(SqlTransaction tran, Context context, int cartId, 
                                                              ShipmentShippingOptionsResult result) 
    {
        try {
            ShoppingCart cart = ShoppingCartSvc.FindById(tran, cartId);
            cart = ShoppingCartSvc.FullFill(tran, cart);
            foreach (ShoppingCartShipment ship in cart.shipments) {
                if ( ship.details.Count > 0 ) {
                    ShipmentShippingOptions opt = ShippingPriceSvc.CalculateShipment(tran, cart, ship);
                    opt.shoppingCartShipmentId = ship.shoppingCartShipmentId;
                    result.options.Add(opt);
                }
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]    
    public ShoppingCartShipmentResult SaveShoppingCartShipment(Context context, ShoppingCartShipment shipment, int version) {
        ShoppingCartShipmentResult result = new ShoppingCartShipmentResult();
        try {
            TransactionHelper.Transaction(this, "SaveShoppingCartShipmentTx", context, shipment, version, result);
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;

    }
    public void SaveShoppingCartShipmentTx(SqlTransaction tran, Context context, 
                                           ShoppingCartShipment shipment, 
                                           int version,
                                           ShoppingCartShipmentResult result) 
    {
        result.version = ShoppingCartSvc.UpdateVersion(tran, shipment.shoppingCartId, version);
        try {
            result.shipment = ShoppingCartShipmentSvc.Update(tran, shipment);
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]    
    public ShoppingCartShipmentListResult SaveShoppingCartShipments(Context context, List<ShoppingCartShipment> shipments, int version) {
        ShoppingCartShipmentListResult result = new ShoppingCartShipmentListResult();
        try {
            TransactionHelper.Transaction(this, "SaveShoppingCartShipmentsTx", context, shipments, version, result); 
        } catch (VersionNotFoundException) {
            result.result.status = RequestResult.VERSION;
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void SaveShoppingCartShipmentsTx(SqlTransaction tran, Context context, 
                                            List<ShoppingCartShipment> shipments,
                                            int version, 
                                            ShoppingCartShipmentListResult result) 
    {
        result.version = ShoppingCartSvc.UpdateVersion(tran, shipments[0].shoppingCartId, version);
        try {
            foreach(ShoppingCartShipment shipment in shipments) {
                result.shipments.Add(ShoppingCartShipmentSvc.Update(tran, shipment));
            }
            result.result.status = RequestResult.SUCCESSS;
        } catch (ApplicationException ex) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public CheckInResult CheckInShoppingCart(Context context, int cartId, int version) {
        CheckInResult result = new CheckInResult();
        try {
            TransactionHelper.Transaction(this, "CheckInShoppingCartTx", 
                context, cartId, version, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void CheckInShoppingCartTx(SqlTransaction tran, Context context, 
                                      int cartId, int version,
                                      CheckInResult result) 
    {   
        try {
            int newVersion = ShoppingCartSvc.UpdateVersion(tran, cartId, version);

            result.errors = ShoppingCartSvc.ValidateBeforeCheckIn(tran, cartId);
            if ( 0 != result.errors.Count ) {
                result.result.status = RequestResult.ERROR;
                result.result.message = result.errors[0];
                return;
            }
            ShoppingCart cart = ShoppingCartSvc.FindById(tran, cartId);

            Brand b = BrandSvc.FindById(tran, context.BrendId);
            CustomerCenterService cc = new CustomerCenterService(b.brandName, b.code, b.imageURLprefix);
            SalesRep rep = cc.GetUserShort(context.Username);
            cart.salesPerson = rep.LastName;
            cart.orderDate = DateTime.Now;
            ShoppingCartSvc.Update(tran, cart);

            cart = ShoppingCartSvc.FindById(tran, cartId);
            List<Order> orders = new List<Order>();
            foreach (ShoppingCartShipment shipment in cart.shipments) {
                if ( 0 < shipment.details.Count ) {
                    Order order = OrderSvc.CreateOrder(tran, cart, shipment);
                    OrderSvc.MakeMACPACXML(tran, order);
                    orders.Add(order);
                }
            }
            ShoppingCartSvc.MakeAcknow(tran, cartId);
            result.acknowURL = ShoppingCartSvc.GetAcknowFile(tran, cartId);
            ShoppingCartSvc.MarkInactive(tran, cartId, cart.version);

            ShoppingCartResult scresult = new ShoppingCartResult();
            GetShoppingCartTx(tran, context, cart.customerId, scresult);
            if ( !RequestResult.SUCCESSS.Equals(scresult.result.status) ) {
                result.result = scresult.result;
            } 
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public LoginResult Login(string username, string password) {
        LoginResult result = new LoginResult();
        try {
            TransactionHelper.Transaction(this, "LoginTx", username, password, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void LoginTx(SqlTransaction tran, string username, string password, LoginResult result) {
        try {
            ASCUser user = ASCUserSvc.FindByLoginAndPassword(tran, username, password);
            result.user = user;
            if (user != null) {
                result.result.status = RequestResult.SUCCESSS;
                result.user.brand = BrandSvc.FindById(tran, result.user.brandId);
                result.user.userType = UserTypeSvc.FindById(tran, result.user.userTypeId);
            } else {
                result.result.status = RequestResult.ERROR;
                result.result.message = "Username or Password is incorrect";
            }
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }
    
    [WebMethod]
    public CustomerListResult GetCustomers(ASCUser user) {
        CustomerListResult result = new CustomerListResult();
        try { 
            TransactionHelper.Transaction(this, "GetCustomersTx", user, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    
    public void GetCustomersTx(SqlTransaction tran, ASCUser user, CustomerListResult result) {
        try { 
            result.customers = CustomerSvc.GetAll(tran);
            result.customers.Sort(CustomerSvc.CompareByName);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public CustomerResult GetCustomer(int id) {
        CustomerResult result = new CustomerResult();
        try { 
            TransactionHelper.Transaction(this, "GetCustomerTx", id, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    
    public void GetCustomerTx(SqlTransaction tran, int id, CustomerResult result) {
        try { 
            result.customer = CustomerSvc.FindById(tran, id);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public CustomerResult SaveCustomer(ASCUser user, Customer customer) {
        CustomerResult result = new CustomerResult();
        try { 
            TransactionHelper.Transaction(this, "SaveCustomerTx", user, customer, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void SaveCustomerTx(SqlTransaction tran, ASCUser user, Customer customer, CustomerResult result) {
        try {
            result.customer = CustomerSvc.Update(tran, customer);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }
    
    [WebMethod]
    public OrderListResult GetOrders(Context context, OrderFilter filter){
        OrderListResult result = new OrderListResult();
        try {
            TransactionHelper.Transaction(this, "GetOrdersTx", context, filter, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetOrdersTx(SqlTransaction tran, Context context, OrderFilter filter, OrderListResult result) {
        try {
            result.orders = OrderSvc.GetOrders(tran, filter);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public OrderResult GetOrder(Context context, int orderId) {
        OrderResult result = new OrderResult();
        try {
            TransactionHelper.Transaction(this, "GetOrderTx", context, orderId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetOrderTx(SqlTransaction tran, Context context, int orderId, OrderResult result) {
        try {
            result.order = OrderSvc.FindById(tran, orderId);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public PONumberCheckResult CheckUniquePONumber(Context context, int customerId, string poNumber) {
        PONumberCheckResult result = new PONumberCheckResult();
        try {
            TransactionHelper.Transaction(this, "CheckUniquePONumberTx", context, customerId, poNumber, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void CheckUniquePONumberTx(SqlTransaction tran, Context context, int customerId, string poNumber, PONumberCheckResult result) {
        try {
            OrderFilter filter = new OrderFilter();
            filter.customerId = customerId;
            filter.poNumberStrong = poNumber;
            result.orders = result.orders = OrderSvc.GetOrders(tran, filter);
            result.isUnique = (0 == result.orders.Count);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public ZipCheckResult CheckZip(Context context, string zip, string country) {
        ZipCheckResult result = new ZipCheckResult();
        try {
            TransactionHelper.Transaction(this, "CheckZipTx", context, zip, country, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }
    public void CheckZipTx(SqlTransaction tran, Context context, string zip, string country, ZipCheckResult result) {
        try {
            string sql = "select city, state from zips where zip = @zip and country = @country";
            SqlParameter[] parms = new SqlParameter[2];
            if ( "CA" == country.ToUpper() ) {
                zip = zip.Replace(" ", "");
                if ( zip.Length > 3 ) {
                   zip = zip.Insert(3 ," ");
                }
            }
            parms[0] = new SqlParameter("@zip", zip.ToUpper());
            parms[1] = new SqlParameter("@country", country.ToUpper());
            Logger.GetAppLogger().DebugFormat("Country {0}, zip {1}", country, zip);
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms) ) {
                while( rdr.Read() ) {
                    Logger.GetAppLogger().DebugFormat("State {0}, city {1}", rdr.GetString(rdr.GetOrdinal("state")), rdr.GetString(rdr.GetOrdinal("city")) );
                    result.cities.Add(rdr.GetString(rdr.GetOrdinal("city")));
                    result.state = rdr.GetString(rdr.GetOrdinal("state"));
                    result.result.status = RequestResult.SUCCESSS;
                } 
                if ( 0 == result.cities.Count ) {
                    result.result.status = RequestResult.ERROR;
                    result.result.message = string.Format("Unknown zip code {0}", zip);
                }
            }
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }

    [WebMethod]
    public URLResult GetOrderPDF(Context context, int orderId) {
        URLResult result = new URLResult();
        try {
            TransactionHelper.Transaction(this, "GetOrderPDFTx", context, orderId, result);
        } catch (Exception ex) {
            result.result.status = RequestResult.FAIL;
            Logger.GetAppLogger().Error(ex.Message, ex);
        }
        return result;
    }

    public void GetOrderPDFTx(SqlTransaction tran, Context context, int orderId, URLResult result) {
        try {
            Order order = OrderSvc.FindById(tran, orderId);
            result.url = ShoppingCartSvc.GetAcknowFile(tran, ShoppingCartShipmentSvc.FindById(tran, order.shopingCartShipmentId).shoppingCartId);
            result.result.status = RequestResult.SUCCESSS;
        } catch ( ApplicationException ex ) {
            result.result.status = RequestResult.ERROR;
            result.result.message = ex.Message;
            Logger.GetAppLogger().Warn(ex.Message, ex);
            throw new RollbackOnlyException();
        }
    }



}

}
