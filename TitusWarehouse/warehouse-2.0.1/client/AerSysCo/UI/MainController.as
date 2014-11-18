package AerSysCo.UI
{
	import flash.events.EventDispatcher;
	import AerSysCo.Server.Context;
	import mx.rpc.Responder;
	import AerSysCo.UI.Models.CustomerUI;
	import AerSysCo.Service.WarehouseStorage;
	import flash.net.SharedObject;
	import AerSysCo.UI.TopBar.SelectCustomerView;
	import flash.events.MouseEvent;
	import mx.core.Application;
	import flash.display.DisplayObject;
	import mx.rpc.events.ResultEvent;
	import AerSysCo.Server.SalesRep;
	import AerSysCo.UI.Models.UserUI;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import AerSysCo.Server.CatalogPackage;
	import mx.collections.ArrayCollection;
	import AerSysCo.UI.Models.WarehouseUI;
	import AerSysCo.UI.Models.CategoryUI;
	import AerSysCo.Server.WarehouseCollection;
	import AerSysCo.Server.Warehouse;
	import mx.events.ResizeEvent;
	import AerSysCo.UI.Models.ShoppingCartUI;
	import AerSysCo.Server.ShoppingCart;
	import AerSysCo.Server.Customer;
	import AerSysCo.Server.ShippingAddressCollection;
	import AerSysCo.Server.ShippingAddress;
	import AerSysCo.UI.Models.ShippingAddressUI;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import AerSysCo.Server.SalesRepResult;
	import AerSysCo.UI.Models.RequestResultUI;
	import AerSysCo.Server.ShoppingCartResult;
	import AerSysCo.Server.ShippingAddressListResult;
	import AerSysCo.UI.TopBar.UserControlBar;
	import flash.events.Event;
	import AerSysCo.Server.CustomerResult;

	[Bindable]
	public class MainController
	{

// singleton section begin
		private static var instance:MainController;
		
		public function MainController()
		{
			if (instance != null) 
			{
				throw new Error("singleton!");
			}
		}
		
		public static function getInstance():MainController 
		{
			if (instance == null) 
			{
				instance = new MainController();
			}
			
			return instance;
		}
// singleton section end

		public function init():void 
		{
			view.visible = false;
			initPopup = InitPopup.open(Application.application as DisplayObject, true);

			model = new MainModel();

			var context:Context = new Context();

            context.Username = Application.application.parameters["u"];
            context.Password = Application.application.parameters["p"];
            context.BrendId = int(Application.application.parameters["b"]);

            initPopup.log("Init application...");

			if ( Application.application.parameters["a"] != "true" ) 
			{
//            	view.dispatchEvent(new Event("logoutRequest"));
//            	return;
			}
            
            WarehouseStorage.getInstance().getSalesRep(context, 
            	new Responder(
	            	function (event:ResultEvent):void 
	            	{
			            var srResult:SalesRepResult = event.result as SalesRepResult;
			            var rep:SalesRep;
			            
			            if (srResult.result.status == RequestResultUI.SUCCESS) 
			            {
			            	rep = srResult.salesRep;
			            } else 
			            {
			            	view.dispatchEvent(new Event("logoutRequest"));
			            	return;
			            }

			            if ( -1 != rep.SalesRepId ) {
				        	var u:UserUI = new UserUI();
				        		u.login = rep.FirstName;
					        	u.firstName = "";
					        	u.lastName = rep.LastName;

							for each (var customer:Customer in rep.customerList.toArray()) 
							{
								var cust:CustomerUI = new CustomerUI();
								cust.populateFromCustomer(customer, true);
								u.customerList.addItem(cust);
							}
							model.user = u;
							model.context = context;
				        	
							view.visible = true;
				
			            } else {
			            	view.dispatchEvent(new Event("logoutRequest"));
			            }
			
			            initPopup.log(" Done.\n");

			        	if (initPopup) 
			            	initPopup.close();
				
			        	initCurrentCustomer();
	            	},
	            	function (event:FaultEvent):void 
	            	{
			            appFault(event.fault.faultString);
	            	}
	            )
	        );
		}
		
// properties begin
		
		public var model:MainModel;
		public var view:MainView;
		
		private var initPopup:InitPopup;

// properties end

		public function appFault(str:String):void 
		{
        	if (initPopup) 
            	initPopup.close();
			
			var faultPopup:FaultPopup = FaultPopup.open(view, true);
			faultPopup.log(str);

			faultPopup.btnReset.addEventListener(MouseEvent.CLICK,
				function (e:*):void 
				{
					faultPopup.close();
					init();
				});
		}

		public function catalogLoadFault():void 
		{
        	if (initPopup) 
            	initPopup.close();
			
			var faultPopup:FaultPopup = FaultPopup.open(view, true);
			faultPopup.log("Categories can not be loaded.");

			faultPopup.btnReset.addEventListener(MouseEvent.CLICK,
				function (e:*):void 
				{
					faultPopup.close();
					view.catalogView.init(model.currentCustomer);
				});
		}
		
        private function loadCustomer(customer:CustomerUI = null):void 
        {
        	if (customer == null)
        		customer = model.currentCustomer;

        	if (customer == null)
        		return;
        		
            WarehouseStorage.getInstance().getCustomer(customer.customerId, 
            	new Responder(
            		function (event:ResultEvent):void 
            		{
            			var cResult:CustomerResult = event.result as CustomerResult;

			            if (cResult.result.status == RequestResultUI.SUCCESS) 
			            {
			            	customer.populateFromCustomer(cResult.customer, false);
			            } else 
			            {
			            	if (cResult.result.status == RequestResultUI.ERROR) 
			            	{
				            	appFault(cResult.result.message);
			            	} else 
			            	{
			            		appFault("Customer reload Fault");
			            	}

			            	return;
			            }
            		},
            		function (event:FaultEvent):void 
            		{
			            appFault(event.fault.faultString);
            		}
            	)
            );
        }
        
        public function setCurrentCustomer(customer:CustomerUI = null):void 
        {
        	if (customer != null) 
        	{
	            model.currentCustomer = customer;

				var so:SharedObject = SharedObject.getLocal(MainModel.LOCAL_SHARED_OBJECT_NAME);
	            so.data.currentCustomerId = customer.customerId;
	            so.flush();
        	}
        	
        	loadCustomer(customer);

			loadShoppingCart();
			loadShippingAddresses();

	        initCatalogPanel();
        }
        
        private function loadShoppingCart(customer:CustomerUI = null):void 
        {
        	if (customer == null)
        		customer = model.currentCustomer;

        	if (customer == null)
        		return;
        		
            WarehouseStorage.getInstance().getShoppingCart(model.context, customer.customerId, 
            	new Responder(
            		function (event:ResultEvent):void 
            		{
            			var scResult:ShoppingCartResult = event.result as ShoppingCartResult;
            			var sc:ShoppingCart;

			            if (scResult.result.status == RequestResultUI.SUCCESS) 
			            {
			            	sc = scResult.cart
			            } else 
			            {
			            	appFault(scResult.result.message);
			            	return;
			            }
            			
			            if (model.currentCustomer.shoppingCart == null) 
			            {
			            	model.currentCustomer.shoppingCart = new ShoppingCartUI();
			            	model.currentCustomer.shoppingCart.addEventListener("versionConflict", scVersionConflictHandler);
			            } 

			            model.currentCustomer.shoppingCart.populateFromShoppingCart(sc, true);
			
//				        initCatalogPanel();
            		},
            		function (event:FaultEvent):void 
            		{
			            appFault(event.fault.faultString);
            		}
            	)
            );
        }
        
        private function scVersionConflictHandler(e:Event):void 
        {
        	Alert.show("Shopping Cart was changed by another User. Shopping Cart have to be reloaded");
        	view.resetCatalogView();
       		loadShoppingCart();
        }

        private function loadShippingAddresses(customer:CustomerUI = null):void 
        {
        	if (customer == null)
        		customer = model.currentCustomer;

        	if (customer == null)
        		return;
        		
            WarehouseStorage.getInstance().getAddressesByCustomerId(model.context, customer.customerId, 
            	new Responder(
            		function (event:ResultEvent):void 
            		{
            			var sacResult:ShippingAddressListResult = event.result as ShippingAddressListResult;
						var sac:ShippingAddressCollection;
						
			            if (sacResult.result.status == RequestResultUI.SUCCESS) 
			            {
			            	sac = sacResult.addresses
			            } else 
			            {
			            	appFault(sacResult.result.message);
			            	return;
			            }
						
            			customer.shipmentAddresses.removeAll();
            			for each (var sa:ShippingAddress in sac.toArray()) 
            			{
            				var saUI:ShippingAddressUI = new ShippingAddressUI();
            				saUI.populateFromShippingAddress(sa);
            				customer.shipmentAddresses.addItem(saUI);
            			}

            		},
            		function (event:FaultEvent):void 
            		{
			            appFault(event.fault.faultString);
            		}
            	)
            );
        }

        private function initCatalogPanel():void 
        {
        	view.catalogView.init(model.currentCustomer);
        	view.controlBar.setState(UserControlBar.STATE_HOME);
        }
        
		public function initCurrentCustomer():void 
		{
			var customerId:int = 0;
			var so:SharedObject = SharedObject.getLocal(MainModel.LOCAL_SHARED_OBJECT_NAME);
		
			if (so.data != null) 
			{
				customerId = so.data.currentCustomerId as int;
			}
			
			if (customerId == 0)
			{
				
	        	if (initPopup) 
	            	initPopup.close();
            
				selectCurrentCustomer();
			} else 
			{
				for each (var cust:CustomerUI in model.user.customerList) 
				{
					if (cust.customerId == customerId) 
					{
						setCurrentCustomer(cust);
						return;
					}
				}

				selectCurrentCustomer();
			}
		}
		
		public function selectCurrentCustomer():void 
		{
			var popup:SelectCustomerView = SelectCustomerView.open(Application.application as DisplayObject, model.user.customerList);

			popup.currentCustomer = model.currentCustomer;
			popup.customerList = model.user.customerList

			popup.btnOk.addEventListener(MouseEvent.CLICK, 
				function (e:MouseEvent):void 
				{
					var newCustomer:CustomerUI = popup.cbCustomers.selectedItem as CustomerUI;
					if (newCustomer != null) 
					{
						setCurrentCustomer(newCustomer);
						popup.close();
						view.vsMain.selectedIndex = 0;
					}
				});
		}
		
		public function openFaq():void 
		{
			openPdf("faq.pdf");
		}
		
		public function openTerms():void 
		{
			openPdf("terms.pdf");
		}
		
		private function openPdf(url:String):void 
		{
            var urlRequest:URLRequest = new URLRequest(url);
            navigateToURL(urlRequest, "_blank");
		}
		
// handlers begin		

        private function getSalesRepOnFaultHandler(event:FaultEvent):void 
        {
            appFault(event.fault.faultString);
        }

// handlers end		
		
	}
}