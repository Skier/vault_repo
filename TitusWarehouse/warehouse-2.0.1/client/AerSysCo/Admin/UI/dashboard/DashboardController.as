package AerSysCo.Admin.UI.dashboard
{
	import flash.events.Event;
	import AerSysCo.UI.Models.CustomerUI;
	import AerSysCo.Service.WarehouseStorage;
	import mx.rpc.Responder;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import AerSysCo.Server.Customer;
	import AerSysCo.Server.CustomerCollection;
	import mx.controls.Alert;
	import AerSysCo.UI.Models.ASCUserUI;
	import AerSysCo.UI.Models.BrandUI;
	import mx.core.Application;
	import flash.display.DisplayObject;
	import flash.events.MouseEvent;
	import AerSysCo.Server.CustomerListResult;
	import AerSysCo.UI.Models.RequestResultUI;
	import AerSysCo.Server.CustomerResult;
	
	[Bindable]
	public class DashboardController
	{
		public var view:DashboardView;
		public var model:DashboardModel;
		
		private var popupDetail:CustomerDetailPopup;
		
		public function DashboardController() 
		{
			model = new DashboardModel();
		}
		
		public function init(user:ASCUserUI, view:DashboardView):void 
		{
			model = new DashboardModel();
			model.currentUser = user;
			this.view = view;
			
			loadCustomers();
		}
		
		public function loadCustomers():void 
		{
			model.loading = true;
			
			WarehouseStorage.getInstance().getCustomers(model.currentUser.toASCUser(),
				new Responder(
					function (event:ResultEvent):void 
					{
						model.loading = false;
						model.customers.removeAll();
						
						var ccResult:CustomerListResult = event.result as CustomerListResult;
						var cc:CustomerCollection;
						if (ccResult.result.status == RequestResultUI.SUCCESS) 
						{
							cc = ccResult.customers;
						} else 
						{
							Alert.show(ccResult.result.message, "Load Customers fault");
							return;
						}
						
						
						for each (var cust:Customer in cc.toArray()) 
						{
							var customer:CustomerUI = new CustomerUI();
							customer.populateFromCustomer(cust);
							model.customers.addItem(customer);
						} 

						updateBrandList();
					},
					function (event:FaultEvent):void 
					{
						model.loading = false;
						Alert.show("Load Customers fault: " + event.fault.faultString, "Load fault");
					}
				)
			);
		} 
		
		public function saveCustomer(customer:CustomerUI):void
		{
			customer.enabled = false;
			
			WarehouseStorage.getInstance().saveCustomer(model.currentUser.toASCUser(), customer.toCustomer(),
				new Responder(
					function (event:ResultEvent):void 
					{
						customer.enabled = true;

						var cResult:CustomerResult = event.result as CustomerResult;
						var c:Customer;
						if (cResult.result.status == RequestResultUI.SUCCESS) 
						{
							c = cResult.customer;
						} else 
						{
							customer.getMemento();
							Alert.show(cResult.result.message, "Save Customer fault");
							return;
						}
						
						customer.populateFromCustomer(c);
					},
					function (event:FaultEvent):void 
					{
						customer.enabled = true;
						customer.getMemento();
						Alert.show("Save Customer fault: " + event.fault.faultString, "Save fault");
					}
				)
			);
		}
		
		public function setFilter(brand:BrandUI, str:String):void 
		{
			model.currentBrand = brand;
			model.searchString = str;
			model.customersFiltered.filterFunction = filterCustomersByName;
			model.customersFiltered.refresh();
		}
		
		public function openCustomer(customer:CustomerUI):void 
		{
			if (!customer.enabled)
				return;
			
			popupDetail = CustomerDetailPopup.open(Application.application as DisplayObject, customer, true);
			popupDetail.btnCancelOrder.addEventListener(MouseEvent.CLICK, 
				function (event:MouseEvent):void 
				{
					popupDetail.close();
				});
			popupDetail.btnSubmitOrder.addEventListener(MouseEvent.CLICK, 
				function (event:MouseEvent):void 
				{
					customer.setMemento();
					customer.maxOrderTotal = Number(popupDetail.txtLimit.text);
					customer.creditStatus = !popupDetail.cbCreditHold.selected;

					saveCustomer(customer);
					
					popupDetail.close();
				});
		}
		
		public function logout():void 
		{
			view.dispatchEvent(new Event("loginRequest"));
		}
		
		private function updateBrandList():void 
		{
			model.brandList.removeAll();

			var brand:BrandUI = new BrandUI();
			brand.brandId = 0;
			brand.brandName = "All Brands";

			model.brandList.addItem(brand);

			for each (var c:CustomerUI in model.customers) 
			{
				if (!brandExists(c.brandId)) 
				{
					var b:BrandUI = new BrandUI();
					b.brandId = c.brandId;
					b.brandName = c.brandName;
					
					model.brandList.addItem(b);
				}
			}
		}
		
		private function brandExists(brandId:int):Boolean 
		{
			for each (var b:BrandUI in model.brandList) 
			{
				if (b.brandId == brandId) 
				{
					return true;
				}
			}
			
			return false;
		}
		
		private function filterCustomersByName(item:CustomerUI):Boolean 
		{
			var result:Boolean = false;
			
			if (model.searchString != null
				&& model.searchString.length == 0) 
			{
				result = true;
			} else if (model.searchString != null
				&& model.searchString.length > 0
				&& item.salesRepCompanyName.toUpperCase().indexOf(model.searchString.toUpperCase()) >= 0) 
			{
				result = true;
			} else if (model.searchString != null
				&& model.searchString.length > 0
				&& item.customerNumber.toUpperCase().indexOf(model.searchString.toUpperCase()) >= 0) 
			{
				result = true;
			} else if (model.searchString == null) 
			{
				result = true;
			} else
			{
				result = false;
			}
			
			if (result && (model.currentBrand.brandId == 0 || model.currentBrand.brandId == item.brandId)) 
			{
				result = true;
			} else 
			{
				result = false;
			}
			
			return result;
		}
	}
}