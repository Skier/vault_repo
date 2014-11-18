package UI.manager.bill
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
	import mx.events.CollectionEvent;
	import App.Entity.BillStatusDataObject;
	import App.Entity.BillDataObject;
	import App.Entity.AFEDataObject;
	import App.Entity.BillItemDataObject;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import App.Entity.AssetAssignmentDataObject;
	
	
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
		private var _afes:Object = new Object;
		private var _subAfes:Object = new Object;
		
		public function SummaryItem(bill:BillDataObject, afes:Array, subAfes:Array, assignments:Array) {
			this.bill = bill;
			
			for each (var assignment:AssetAssignmentDataObject in assignments) {
				_assetAssignments[assignment.AssetAssignmentId] = assignment;
			}
			
			_afes = afes;
			_subAfes = subAfes;
		}
		
		public function load():void 
		{
			loaded = false;
			
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetBill.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var filledBill:BillDataObject = BillDataObject(result.result);
       				bill.assign(filledBill);
       				bill.AssetInfo = filledBill.AssetInfo;
       				bill.BillItems = filledBill.BillItems;
       				bill.Notes = filledBill.Notes;
       				
       				processBillItems();
       				
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
		
		private function processBillItems():void 
		{
			if (null != assignments) {
				assignments.removeAll();
			} else {
				assignments = new ArrayCollection();
			}
			
			if (!isBillCorrect) {
				dispatchEvent(new Event("bill_is_broken"));
			}
			
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
		}
		
		private function get isBillCorrect():Boolean 
		{
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
				if ( billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED
					&& billItem.StatusTemp != BillItemDataObject.BILL_ITEM_STATUS_DECLINED ) {
   					canSubmit = false;
   					return;
    			}
    		}
			canSubmit = true;
		}
		
		public function updateStatus():void 
		{
			if (assignments.length == 0) {
				return;
			}
			
           	var isApproved:Boolean = true;
           	var isRejected:Boolean = false;
            
           	canSubmit = true;

        	for each (var billItem:BillItemDataObject in bill.BillItems) {
        		if ( billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED
        			|| billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_CORRECTED ) {
       				canSubmit = false;
        		} 
        		if ( billItem.Status == BillItemDataObject.BILL_ITEM_STATUS_REJECTED ) {
        			isRejected = true;
        		}
        		if ( billItem.Status != BillItemDataObject.BILL_ITEM_STATUS_APPROVED ) {
        			isApproved = false;
        		}

        	}
            
            if (isRejected) {
            	bill.Status = BillStatusDataObject.BILL_STATUS_REJECTED;
            } else if (isApproved) {
            	bill.Status = BillStatusDataObject.BILL_STATUS_APPROVED;
            }
            
		}
		
		private function onGroupStatusChanged(event:*):void 
		{
			updateStatus();
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
