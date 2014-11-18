package AerSysCo.UI.Ordering.CheckOut
{
    import AerSysCo.UI.Models.CustomerUI;
    import AerSysCo.UI.Models.ShippingAddressUI;
    import AerSysCo.Service.WarehouseStorage;
    import AerSysCo.Server.Context;
    import AerSysCo.UI.MainController;
    import AerSysCo.Server.ShoppingCart;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import AerSysCo.Server.ShippingAddress;
    import AerSysCo.UI.Models.ShoppingCartUI;
    import AerSysCo.Server.ShippingOptionCollection;
    import AerSysCo.Server.ShippingAddressCollection;
    import AerSysCo.Server.ShippingOption;
    import AerSysCo.Server.ShipmentShippingOptions;
    import AerSysCo.Server.ShipmentShippingOptionsCollection;
    import AerSysCo.UI.Models.ShoppingCartShipmentUI;
    import AerSysCo.UI.Models.ShippingOptionUI;
    import mx.core.Application;
    import flash.display.DisplayObject;
    import mx.collections.ArrayCollection;
    import AerSysCo.Server.ShoppingCartShipmentCollection;
    import AerSysCo.Server.CheckInResult;
    import flash.net.URLRequest;
    import flash.net.navigateToURL;
    import mx.managers.PopUpManager;
    import AerSysCo.Server.ShoppingCartShipmentListResult;
    import AerSysCo.UI.Models.RequestResultUI;
    import AerSysCo.Server.ShoppingCartResult;
    import AerSysCo.Server.ShippingAddressResult;
    import AerSysCo.Server.ShipmentShippingOptionsResult;
    import AerSysCo.Server.PONumberCheckResult;
    
    public class CheckOutController
    {
        [Bindable] public var model:CheckOutModel;
        public var view:CheckOutView;
        
        private var waitScreen:WaitScreen;
        
        public function init(customer:CustomerUI, view:CheckOutView):void 
        {
            model = new CheckOutModel();
            model.currentCustomer = customer;
            
            this.view = view;

            if (view.viewShipmentAddress) 
            {
                view.vsCheckOut.selectedChild = view.viewShipmentAddress;
                view.viewShipmentAddress.init();
            }
        }
        
        public function proceedAddressScreen(address:ShippingAddressUI):void 
        {
            if (address == null) 
                return;
            
            if (address.addressId > 0) 
            {
                model.currentShippingAddress = address;
                saveShoppingCart(model.currentCustomer.shoppingCart);
            } else 
            {
                saveShippingAddress(address);
            }
        }
        
        public function proceedShippingOptions():void 
        {
            for each (var shipment:ShoppingCartShipmentUI in model.currentCustomer.shoppingCart.shipmentsFiltered) 
            {
                checkShipment(shipment);
            }
        }
        
        private function checkShipment(shipment:ShoppingCartShipmentUI):void 
        {
            var context:Context = MainController.getInstance().model.context;
            if (context == null)
            {
                trace("CheckOutController:checkShipment - context is null");
                return;
            }
            
            WarehouseStorage.getInstance().checkUniquePONumber(context, model.currentCustomer.customerId, shipment.poNumber,
                new Responder(
                    function (event:ResultEvent):void 
                    {
                        var checkPoNumberResult:PONumberCheckResult = event.result as PONumberCheckResult;
                        
                        if (checkPoNumberResult.result.status == RequestResultUI.SUCCESS) 
                        {
                            if (checkPoNumberResult.isUnique) 
                            {
                                shipment.isPONumberValid = true;
                                tryToSaveShippingOptions();
                            } else 
                            {
                                Alert.show("PO Number [" + shipment.poNumber + "] is not unique. Please check it.", "PO Number must be unique");
                            }
                        } else if (checkPoNumberResult.result.status == RequestResultUI.ERROR)
                        {
                            Alert.show("Check PON. Error: " + checkPoNumberResult.result.message, "PO Number is not valid.");
                        } else 
                        {
                            Alert.show("Server internal error. Please contact site Administrator.", "Internal Server Error.");
                        }
                    },
                    function (event:FaultEvent):void 
                    {
                        Alert.show("Check Unique PO Number Fault: " + event.fault.message);
                    }
                )
            );
        }
        
        private function tryToSaveShippingOptions():void 
        {
            var shipments:ShoppingCartShipmentCollection = new ShoppingCartShipmentCollection();
            for each (var shipment:ShoppingCartShipmentUI in model.currentCustomer.shoppingCart.shipmentsFiltered) 
            {
                if (shipment.isPONumberValid) 
                {
                    shipments.addItem(shipment.toShoppingCartShipment());
                } else 
                {
                    return;
                }
            }

            saveShipments(shipments);
        }
        
        public function submitOrders():void 
        {
            if (model.currentCustomer.creditStatus == false) 
            {
                Alert.show("Cannot place the order. Credit Hold", "Credit Hold");
                return;
            }
            
            var context:Context = MainController.getInstance().model.context;
            if (context == null)
            {
                trace("CheckOutController:saveShippingAddress - context is null");
                return;
            }
            
            waitScreen = WaitScreen.open(Application.application as DisplayObject);
            waitScreen.lblMessage.text = "Orders processing. Please Wait....";

            WarehouseStorage.getInstance().checkInShoppingCart(context, 
                model.currentCustomer.shoppingCart.shoppingCartId, model.currentCustomer.shoppingCart.version,
                new Responder(
                    function (event:ResultEvent):void 
                    {
                        var checkInResult:CheckInResult = event.result as CheckInResult;
                        
                        if (checkInResult.result.status == RequestResultUI.SUCCESS) 
                        {
                            openPdf(checkInResult.acknowURL);
                            view.dispatchEvent(new Event("createFinalOrders"));
                        } else 
                        {
                            if (checkInResult.result.status == RequestResultUI.ERROR) 
                            {
                                Alert.show("Check in SC Error: " + checkInResult.result.message);
                            } else if (checkInResult.result.status == RequestResultUI.VERSION) 
                            {
                                model.currentCustomer.shoppingCart.dispatchVersionConflict();
                            } else 
                            {
                                Alert.show("Server Internal Error. Please contact Site Administrator.");
                            }
                        }
                        waitScreen.close();
                    },
                    function (event:FaultEvent):void 
                    {
                        Alert.show("Check in SC Fault: " + event.fault.message);
                        waitScreen.close();
                    }
                )
            );
        }
        
        public function showTerms():void 
        {
            RulesPopup.open(Application.application as DisplayObject);
        }
        
        private function openPdf(url:String):void 
        {
            var urlRequest:URLRequest = new URLRequest(url);
            navigateToURL(urlRequest, "_blank");
        }
        
        private function saveShipments(shipments:ShoppingCartShipmentCollection):void
        {
            var context:Context = MainController.getInstance().model.context;
            if (context == null)
            {
                trace("CheckOutController:saveShipments - context is null");
                return;
            }
            
            view.viewShippingOptions.enabled = false;
            
            WarehouseStorage.getInstance().saveShoppingCartShipments(context, shipments, model.currentCustomer.shoppingCart.version,
                new Responder(
                    function (event:ResultEvent):void 
                    {
                        var scsResult:ShoppingCartShipmentListResult = event.result as ShoppingCartShipmentListResult;
                        if (scsResult.result.status != RequestResultUI.SUCCESS) 
                        {
                            if (scsResult.result.status == RequestResultUI.ERROR) 
                            {
                                Alert.show("Save SC Shipments Error:" + scsResult.result.message);
                            } else if (scsResult.result.status == RequestResultUI.VERSION) 
                            {
                                model.currentCustomer.shoppingCart.dispatchVersionConflict();
                            } else 
                            {
                                Alert.show("Server Internal Error. Please contact Site Administrator.");
                            }
                            view.viewShippingOptions.enabled = true;
                            return;
                        }
                        
                        model.currentCustomer.shoppingCart.version = scsResult.version;
                        model.currentCustomer.shoppingCart.updateTotals();
                        
                        WarehouseStorage.getInstance().saveShoppingCart(context, model.currentCustomer.shoppingCart.toShoppingCart(), 
                            new Responder(
                                function (event:ResultEvent):void 
                                {

                                   var scResult:ShoppingCartResult = event.result as ShoppingCartResult;
                                    if (scResult.result.status != RequestResultUI.SUCCESS) 
                                    {
                                        if (scResult.result.status == RequestResultUI.ERROR) 
                                        {
                                            Alert.show("Save SC Error:" + scResult.result.message);
                                        } else if (scResult.result.status == RequestResultUI.VERSION) 
                                        {
                                            model.currentCustomer.shoppingCart.dispatchVersionConflict();
                                        } else 
                                        {
                                            Alert.show("Server Internal Error. Please contact Site Administrator.");
                                        }

                                        return;
                                    }
                                    
                                    model.currentCustomer.shoppingCart.version = scResult.cart.version;
                                    
                                    view.viewShippingOptions.enabled = true;
                                    view.vsCheckOut.selectedChild = view.viewFinalCheckout;
                                    view.viewFinalCheckout.init();
                                },
                                function (event:FaultEvent):void 
                                {
                                    Alert.show("Save SC Fault: " + event.fault.message);
                                    view.viewShippingOptions.enabled = true;
                                }
                            )
                        );

                    },
                    function (event:FaultEvent):void 
                    {
                        Alert.show("Save SC Shipment Fault: " + event.fault.message);
                        view.viewShippingOptions.enabled = true;
                    }
                )
            );
        
        }
        
        private function saveShippingAddress(address:ShippingAddressUI):void 
        {
            var context:Context = MainController.getInstance().model.context;
            if (context == null)
            {
                trace("CheckOutController:saveShippingAddress - context is null");
                return;
            }
            
            view.viewShipmentAddress.enabled = false;
            
            WarehouseStorage.getInstance().saveShippingAddress(context, address.toShippingAddress(),
                new Responder(
                    function (event:ResultEvent):void 
                    {
                        view.viewShipmentAddress.enabled = true;

                        var saResult:ShippingAddressResult = event.result as ShippingAddressResult;
                        var sa:ShippingAddress;
                        if (saResult.result.status == RequestResultUI.SUCCESS) 
                        {
                            sa = saResult.address;
                        } else 
                        {
                            Alert.show("Save Shipping Address Error: " + saResult.result.message);
                            return;
                        }
                        
                        model.currentShippingAddress = new ShippingAddressUI;
                        model.currentShippingAddress.populateFromShippingAddress(sa);
                        if (address.addressId == 0) 
                        {
                            model.currentCustomer.shipmentAddresses.addItemAt(model.currentShippingAddress, 0);
                        }
                        
                        saveShoppingCart(model.currentCustomer.shoppingCart);
                    },
                    function (event:FaultEvent):void 
                    {
                        Alert.show("Save Shipping Address Fault: " + event.fault.message);
                        view.viewShipmentAddress.enabled = true;
                    }
                )
            );
        }
        
        private function saveShoppingCart(scUI:ShoppingCartUI):void 
        {
            var context:Context = MainController.getInstance().model.context;
            if (context == null)
            {
                trace("CheckOutController:saveShoppingCart - context is null");
                return;
            }
            
            view.viewShipmentAddress.enabled = false;

            var sc:ShoppingCart = scUI.toShoppingCart();
            sc.shippingAddressId = model.currentShippingAddress.addressId;
            sc.markOrder = view.viewShipmentAddress.txtMark.text;
            sc.jobsiteContactPh = view.viewShipmentAddress.txtJobsite.text;
            sc.deliveryRequest = view.viewShipmentAddress.txtDelivery.text;
            
                
            WarehouseStorage.getInstance().saveShoppingCart(context, sc,
                new Responder(
                    function (event:ResultEvent):void 
                    {
                        view.viewShipmentAddress.enabled = true;

                        var scResult:ShoppingCartResult = event.result as ShoppingCartResult;
                        var sCart:ShoppingCart;
                        if (scResult.result.status == RequestResultUI.SUCCESS) 
                        {
                            sCart = scResult.cart;
                            scUI.shippingAddress = model.currentShippingAddress;
                            scUI.shippingAddressId = model.currentShippingAddress.addressId;
                            scUI.markOrder = sCart.markOrder;
                            scUI.jobsiteContactPh = sCart.jobsiteContactPh;
                            scUI.deliveryRequest = sCart.deliveryRequest;
                            scUI.version = sCart.version;
                        } else 
                        {
                            if (scResult.result.status == RequestResultUI.ERROR) 
                            {
                                Alert.show("Save SC Error: " + scResult.result.message);
                            } else if (scResult.result.status == RequestResultUI.VERSION) 
                            {
                                model.currentCustomer.shoppingCart.dispatchVersionConflict();
                            } else 
                            {
                                Alert.show("Server Internal Error. Please contact Site Administrator.");
                            }

                            return;
                        }
                                    
                        view.vsCheckOut.selectedChild = view.viewShippingOptions;
                        loadShippingOptions();
                    }, 
                    function (event:FaultEvent):void 
                    {
                        Alert.show("Save SC Fault: " + event.fault.message);
                        view.viewShipmentAddress.enabled = true;
                    }
                )
            );
        }
        
        private function loadShippingOptions():void 
        {
            var context:Context = MainController.getInstance().model.context;
            if (!context) 
                throw new Error("ShippingOptionsView:loadShippingOptions - Context is null !");
            
            waitScreen = WaitScreen.open(Application.application as DisplayObject);
            
            WarehouseStorage.getInstance().getShippingOptions(context, model.currentCustomer.shoppingCart.shoppingCartId,
                new Responder(
                    function (event:ResultEvent):void 
                    {
                        var ssocResult:ShipmentShippingOptionsResult = event.result as ShipmentShippingOptionsResult;
                        var ssoc:ShipmentShippingOptionsCollection;
                        if (ssocResult.result.status == RequestResultUI.SUCCESS) 
                        {
                            ssoc = ssocResult.options;
                        } else 
                        {
                            Alert.show("Get Shipping Options Error: " + ssocResult.result.message);
                            view.vsCheckOut.selectedChild = view.viewShipmentAddress;
                            waitScreen.close();
                            return;
                        }
                        
                        for each (var sso:ShipmentShippingOptions in ssoc.toArray()) 
                        {
                            var shUI:ShoppingCartShipmentUI = model.currentCustomer.shoppingCart.getShipment(sso.shoppingCartShipmentId);
                            shUI.shipmentOptions.removeAll();
                            for each (var so:ShippingOption in sso.options.toArray()) 
                            {
                                var soUI:ShippingOptionUI = new ShippingOptionUI();
                                soUI.populateFromShippingOption(so);
                                shUI.shipmentOptions.addItem(soUI);
                            }
                            shUI.liftGateCost = sso.liftGatePrice;
                        }
                        view.vsCheckOut.selectedChild = view.viewShippingOptions;
                        waitScreen.close();
                    },
                    function (event:FaultEvent):void 
                    {
                        Alert.show("Get Shipping Options Fault: " + event.fault.message);
                        view.vsCheckOut.selectedChild = view.viewShipmentAddress;
                        waitScreen.close();
                    }
                )
            );
        }
        
        private function switchFlowState(flowState:int):void 
        {
            switch (flowState) 
            {
                case CheckOutModel.FLOW_STATE_SHIPMENT_ADDRESS:
                    if (model.currentFlowState != CheckOutModel.FLOW_STATE_SHIPMENT_ADDRESS) 
                    {
                        model.currentFlowState = CheckOutModel.FLOW_STATE_SHIPMENT_ADDRESS
                    }
                    break;
                     
                case CheckOutModel.FLOW_STATE_SHIPPING_OPTIONS:
                    if (model.currentFlowState != CheckOutModel.FLOW_STATE_SHIPPING_OPTIONS) 
                    {
                        model.currentFlowState = CheckOutModel.FLOW_STATE_SHIPPING_OPTIONS
                    }
                    break;
                     
                case CheckOutModel.FLOW_STATE_SHIPMENT_ADDRESS:
                    if (model.currentFlowState != CheckOutModel.FLOW_STATE_SHIPMENT_ADDRESS) 
                    {
                        model.currentFlowState = CheckOutModel.FLOW_STATE_SHIPMENT_ADDRESS
                    }
                    break;
            }
        }
            
    }
}