package AerSysCo.UI.Account.Orders
{
	import AerSysCo.UI.Models.OrderFilterUI;
	import mx.collections.ArrayCollection;
	import AerSysCo.UI.Models.CustomerUI;
	import AerSysCo.UI.Models.OrderUI;
	import mx.managers.CursorManager;
	
	[Bindable]
	public class OrdersHistoryModel
	{
		public static const STATE_ORDER_LIST:int = 0;
		public static const STATE_ORDER_DETAIL:int = 1;

        public var customer:CustomerUI;		
		public var orderFilter:OrderFilterUI;
		public var orderList:ArrayCollection;
		public var currentOrder:OrderUI;
		
		public var workFlowState:int = 0;
		private var _isBusy:Boolean = false;
		public function get isBusy():Boolean {return _isBusy}
		public function set isBusy(value:Boolean):void 
		{
			_isBusy = value;
			
			if (value) {
				CursorManager.setBusyCursor();
			} else {
				CursorManager.removeBusyCursor();
			}	
		}
		
		public function OrdersHistoryModel()
		{
			orderFilter = new OrderFilterUI();
			orderList = new ArrayCollection();
		}
	}
}