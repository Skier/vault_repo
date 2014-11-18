package UI.DashBoard.Customer
{
	import mx.core.Container;
	import UI.DashBoard.MainController;
	import mx.collections.ArrayCollection;
	import Service.CustomerService;
	import Domain.*;
	
	public class CustomerController
	{
		private var _mainController:MainController;
		private var _mainCustomerView:MainCustomerView;
		private var _customerDashView:CustomerDashView;
		private var _findCustomerView:FindCustomerView;
		private var _editCustomerView:CustomerInfoView;
		
		
		
		
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
			_editCustomerView.currentCustomer = customer;
			customer.jobs = CustomerService.findJobsByCustomer(customer);
			selectedChild = _editCustomerView;
			
		}
		 
		public function showNewCustomer():void{
			_editCustomerView.currentCustomer = new Customer();
			selectedChild = _editCustomerView;
		}
		
	}
	
	
}