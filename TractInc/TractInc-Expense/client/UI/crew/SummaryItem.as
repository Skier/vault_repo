package UI.crew
{
	import flash.events.EventDispatcher;
	import mx.collections.ArrayCollection;
	import App.Entity.AssignmentBillItemsGroup;
	import mx.rpc.events.FaultEvent;
	import mx.binding.utils.ChangeWatcher;
	import common.StatusesRegistry;
	import mx.events.PropertyChangeEvent;
	import mx.events.PropertyChangeEventKind;
	import mx.rpc.Responder;
	import mx.controls.Alert;
	import util.DateUtil;
	import App.Entity.BillItemDataObject;
	import App.Entity.BillDataObject;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import App.Entity.AssetAssignmentDataObject;
	import App.Entity.AFEDataObject;
	
	
	[Bindable]
	public class SummaryItem extends EventDispatcher
	{
		public var bill:BillDataObject;

		public var assignments:ArrayCollection;
		
		public var windowTitle:String;
		
		public var loaded:Boolean = false;
		public var saved:Boolean = true;
		public var canSubmit:Boolean = false;
		
		public var sr:StatusesRegistry = StatusesRegistry.instance;
		
		private var _assetAssignments:Array = new Array();
		private var _afes:Array = new Array();
		private var _subAfes:Array = new Array();
		
		public function SummaryItem(bill:BillDataObject, afes:Array, subAfes:Array, assignments:Array) {
			this.bill = bill;
			
			for each (var assignment:AssetAssignmentDataObject in assignments) {
				_assetAssignments[assignment.AssetAssignmentId] = assignment;
			}
			
			_afes = afes;
			_subAfes = subAfes;
			
			load();
		}
		
		private function load():void 
		{
			loaded = false;
			
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetBill.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				assignments = new ArrayCollection();
       				
       				var filledBill:BillDataObject = BillDataObject(result.result);
       				bill.assign(filledBill);
       				bill.AssetInfo = filledBill.AssetInfo;
       				bill.BillItems = filledBill.BillItems;
       				bill.Notes = filledBill.Notes;
       				
					for each (var billItem:BillItemDataObject in bill.BillItems) {
						var group:AssignmentBillItemsGroup = getGroupByAssignmentId(billItem.AssetAssignmentId);
						
						if (group == null) {
							group = new AssignmentBillItemsGroup();
							group.assetAssignment = _assetAssignments[billItem.AssetAssignmentId];
							assignments.addItem(group);
						}
						
						billItem.toTempFields();

						group.addBillItem(billItem);
						
						ChangeWatcher.watch(billItem, ["StatusTemp"], onBillItemStatusChanged);
						
					}
			
					dispatchEvent(new Event("summary_item_loaded"));

            		windowTitle  = " Bill Detail Review ";
            		windowTitle += "  ( Bill Start Date: ";
            		windowTitle += DateUtil.format(new Date(Date.parse(bill.StartDate)));
            		windowTitle += "    Asset: ";
            		windowTitle += bill.AssetInfo.FirstName;
            		windowTitle += " ";
            		windowTitle += bill.AssetInfo.LastName;
            		windowTitle += " )";
            
            		loaded = true;
       			}
       		);
       		userService.GetBill.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetBill(bill.BillId);
		}
		
		private function get isBillCorrect():Boolean 
		{
			return true; // need to change this method.
			
			for each (var billItem:BillItemDataObject in bill.BillItems) {
				
				if (bill.Status == BillDataObject.BILL_STATUS_SUBMITTED 
					&& !(billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED)) {
					return false;
				}
				
				if (bill.Status == BillDataObject.BILL_STATUS_CORRECTED 
					&& !(billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_APPROVED 
						|| billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_CORRECTED
						|| billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED
						|| billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED
						|| billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_VERIFIED
						) ) {
					return false;
				}
			}
			
			return true;
		}

		private function onBillItemStatusChanged(e:*):void 
		{
			checkSubmit();
		}
		
		private function checkSubmit():void 
		{
			for each ( var billItem:BillItemDataObject in bill.BillItems) {
				if ( billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_APPROVED
					&& billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_REJECTED 
					&& billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED ) {
   					canSubmit = false;
   					return;
    			}
    		}
			canSubmit = true;
		}
		
		private function getGroupByAssignmentId(id:int):AssignmentBillItemsGroup 
		{
			for each (var group:AssignmentBillItemsGroup in assignments) {
				if (group.assetAssignment.AssetAssignmentId == id) {
					return group;
				}
			}
			
			return null;
		}
		
        private function onFault(event:FaultEvent):void 
        {
        	Alert.show(event.fault.message);
        }
        
	}
	
}
