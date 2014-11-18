package UI.DashBoard.Customer
{
	import mx.core.Container;
	import UI.DashBoard.MainController;
	import mx.collections.ArrayCollection;
	import Domain.*;
	import Service.ServerSyncService;
	import mx.controls.Alert;
	import adobe.utils.CustomActions;
	
	public class CustomerController
	{
		private var _mainController:MainController;
		private var _mainCustomerView:MainCustomerView;
		private var _customerDashView:CustomerDashView;
		private var _findCustomerView:FindCustomerView;
		private var _editCustomerView:CustomerInfoView;
		private var _currentCustomer:Customer;
		
		[Bindable]
		public var selectedChild:Container;
      
        
		
		public function CustomerController(mainCustomerView:MainCustomerView, 
			customerDashView:CustomerDashView,
			findCustomerView:FindCustomerView,
			editCustomerView:CustomerInfoView,
			mainController:MainController):void {
				
			this._customerDashView = customerDashView;
			this._mainCustomerView = mainCustomerView;
			this._findCustomerView = findCustomerView;
			this._editCustomerView = editCustomerView;
			this._mainController = mainController;
			
			selectedChild = _customerDashView;
			
		}
		
		public function showDash():void{
			selectedChild = _customerDashView;
		}
		
		public function showEditCustomer(customer:Customer):void{
			
			var svc:ServerSyncService = new ServerSyncService();
			this._currentCustomer = customer;
			svc.FindJobsAndTicketsByCustomer(customer.ID, onJobsAndTicketsReceivedOK, 
				onJobsAndTicketsReceivedFailed);
		}
		
		public function get currentCustomer():Customer{
			return _currentCustomer;
		}
		
		private function onJobsAndTicketsReceivedOK(arr:ArrayCollection):void{
			
			this._currentCustomer.JobPackages = arr;	
			_editCustomerView.currentCustomer = this._currentCustomer;
			selectedChild = _editCustomerView;
				
		}
		
		private function onJobsAndTicketsReceivedFailed(msg:String):void{
			Alert.show(msg);
		}
		 
		public function showNewCustomer():void{
			_editCustomerView.currentCustomer = new Customer();
			selectedChild = _editCustomerView;
		}
		
	}
	
	
}