package AerSysCo.UI.Account.Orders
{
	import AerSysCo.UI.Models.CustomerUI;
	import AerSysCo.UI.Models.OrderFilterUI;
	import AerSysCo.Server.Context;
	import AerSysCo.UI.MainController;
	import AerSysCo.Service.WarehouseStorage;
	import mx.rpc.Responder;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import AerSysCo.Server.OrderCollection;
	import AerSysCo.Server.Order;
	import AerSysCo.UI.Models.OrderUI;
	import AerSysCo.Server.OrderListResult;
	import AerSysCo.UI.Models.RequestResultUI;
	import AerSysCo.Server.OrderResult;
	import AerSysCo.Service.ResponderAdapter;
    import flash.net.URLRequest;
    import flash.net.navigateToURL;
	import AerSysCo.Server.URLResult;
	
	[Bindable]
	public class OrdersHistoryController
	{
		public var model:OrdersHistoryModel;
		private var view:OrderHistoryView;
		
		public function init(customer:CustomerUI, view:OrderHistoryView):void 
		{
			this.view = view;
			model = new OrdersHistoryModel();
			model.customer = customer;
			initDefaultFilter(model.orderFilter, customer);
			loadOrders();
		}
		
		public function search():void 
		{
			var filter:OrderFilterUI = model.orderFilter;
			
			filter.quantity = int(view.cbQuantity.text);
			filter.customerId = model.customer.customerId;
			filter.poNumber = view.txtPONumber.text;
			filter.confirmNumber = view.txtConfirmNumber.text;

			if ( view.dateFrom.selectedDate != null ) {
				filter.fromDate = view.dateFrom.selectedDate;
				filter.fromDate.hours = 0;
				filter.fromDate.minutes = 0;
				filter.fromDate.seconds = 0;
				filter.fromDate.milliseconds = 0;
			} else {
				filter.fromDate = null;
			} 

			if (view.dateTo.selectedDate != null) 
			{
				filter.toDate = view.dateTo.selectedDate;
				filter.toDate.hours = 23;
				filter.toDate.minutes = 59;
				filter.toDate.seconds = 59;
				filter.toDate.milliseconds = 999;
			} else 
			{
				filter.toDate = null;
			}
			
			loadOrders();
		}
		
		public function openOrder(order:OrderUI):void 
		{
			model.currentOrder = order;
			model.workFlowState = OrdersHistoryModel.STATE_ORDER_DETAIL;
//			fillOrder(model.currentOrder);
		}

		public function printOrder(order:OrderUI):void 
		{
			WarehouseStorage.getInstance().getOrderPDF(
  			    MainController.getInstance().model.context, 
 			    order.orderId,                  
 			    new mx.rpc.Responder(
	                function (event:ResultEvent):void  {
	                	var urlResult:URLResult = event.result as URLResult;
                        var urlRequest:URLRequest = new URLRequest(urlResult.url);
                        navigateToURL(urlRequest, "_blank");
                    },
                    function (event:FaultEvent):void {
		                Alert.show("Can not get Order PDF: " + event.fault.message);
                    }
                )
            )    
		}
		


		
		public function closeOrder():void 
		{
			model.workFlowState = OrdersHistoryModel.STATE_ORDER_LIST;
		}
		
		private function initDefaultFilter(filter:OrderFilterUI, customer:CustomerUI):void 
		{
			filter.customerId = customer.customerId;
			filter.quantity = 5;
			filter.fromDate = null;
			filter.toDate = null;
		}
		
		private function loadOrders():void 
		{
			var context:Context = MainController.getInstance().model.context;
			if (!context) {
				throw new Error("OrderHistoryController:loadOrders - context is null");
		    }		
			
			model.isBusy = true;
			model.orderList.removeAll();
			WarehouseStorage.getInstance().getOrders(context, model.orderFilter.toOrderFilter(),
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;

						var dcResult:OrderListResult = event.result as OrderListResult;
						var result:OrderCollection;
						if (dcResult.result.status == RequestResultUI.SUCCESS)
						{
							result = dcResult.orders;
						} else 
						{
							Alert.show(dcResult.result.message, "Orders Load Error");
							return;
						}
						
						for each (var o:Order in result.toArray()) 
						{
							var order:OrderUI = new OrderUI();
							order.populateFromOrder(o);
							model.orderList.addItem(order);
						}
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show("Orders Loading Fault: " + event.fault.faultString, "Load error");
					}
				)
			);
		}
		
		private function fillOrder(order:OrderUI):void 
		{
			var context:Context = MainController.getInstance().model.context;
			if (!context)
				throw new Error("OrderHistoryController:loadOrders - context is null");
			
			order.isLoading = true;
			WarehouseStorage.getInstance().getOrder(context, order.orderId,
				new Responder(
					function (event:ResultEvent):void 
					{
						order.isLoading = false;

						var oResult:OrderResult = event.result as OrderResult;
						var o:Order;
						if (oResult.result.status == RequestResultUI.SUCCESS) 
						{
							o = oResult.order;
						} else 
						{
							Alert.show(oResult.result.message, "Load Order Error");
							return;
						}
						
						order.populateFromOrder(event.result as Order);
					},
					function (event:FaultEvent):void 
					{
						order.isLoading = false;
						Alert.show("Order Loading Fault: " + event.fault.faultString, "Load error");
					}
				)
			);
		}
	}
}