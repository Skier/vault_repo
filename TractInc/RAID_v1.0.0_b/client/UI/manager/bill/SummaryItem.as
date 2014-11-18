package UI.manager.bill
{
	import flash.events.EventDispatcher;
	import App.Domain.Bill;
	import mx.collections.ArrayCollection;
	import weborb.data.DynamicLoadEvent;
	import App.Domain.AssignmentBillItemsGroup;
	import mx.rpc.events.FaultEvent;
	import App.Domain.ActiveRecords;
	import App.Domain.AssetAssignment;
	import App.Domain.BillItem;
	import mx.binding.utils.ChangeWatcher;
	import App.Domain.BillItemStatus;
	import App.Domain.BillStatus;
	import common.StatusesRegistry;
	import mx.events.PropertyChangeEvent;
	import mx.events.PropertyChangeEventKind;
	import mx.rpc.Responder;
	import mx.controls.Alert;
	import util.DateUtil;
	import App.Domain.Afe;
	import mx.events.CollectionEvent;
	import App.Domain.SubAfe;
	
	
	[Bindable]
	public class SummaryItem extends EventDispatcher
	{
		public var bill:Bill;

		public var assignments:ArrayCollection;
		
		public var windowTitle:String;
		
		public var loaded:Boolean = false;
		public var saved:Boolean = true;
		public var canSubmit:Boolean = false;
		
		public var sr:StatusesRegistry = StatusesRegistry.getInstance();
		
		private var _assetAssignments:Object = new Object();
		private var _afes:Object = new Object;
		private var _subAfes:Object = new Object;
		
		public function SummaryItem(bill:Bill) {
			this.bill = bill;
			assignments = new ArrayCollection();
		}
		
		public function load():void 
		{
			loaded = false;

			loadAfes();
		}
		
		private function loadAfes():void 
		{
			var sql:String = "";
			sql += " select distinct Afe.* from [Afe] ";
			sql += "   inner join [AssetAssignment] aa on Afe.AFE = aa.AFE ";
			sql += " where aa.AssetId = ";
			sql += bill.RelatedAsset.AssetId.toString();
			
			var afes:ArrayCollection = ActiveRecords.Afe.findBySql(sql);
			afes.addEventListener("loaded",
					function(event:DynamicLoadEvent):void {
						for each (var afe:Afe in event.data as ArrayCollection) {
							_afes[afe.AFE] = afe;
						}
						loadSubAfes();
					});

		}

		private function loadSubAfes():void 
		{
			var sql:String = "";
			sql += " select distinct SubAfe.* from [SubAfe] ";
			sql += "   inner join [AssetAssignment] aa on SubAfe.SubAFE = aa.SubAFE ";
			sql += " where aa.AssetId = ";
			sql += bill.RelatedAsset.AssetId.toString();
			
			var subAfes:ArrayCollection = ActiveRecords.SubAfe.findBySql(sql);
			subAfes.addEventListener("loaded",
					function(event:DynamicLoadEvent):void {
						for each (var subAfe:SubAfe in event.data as ArrayCollection) {
							_subAfes[subAfe.SubAFE] = subAfe;
						}
						loadAssetAssignments();
					});

		}

		public function loadAssetAssignments():void 
		{
			var sql:String = "";
			sql += " select * from [AssetAssignment] ";
			sql += " where AssetId = ";
			sql += bill.RelatedAsset.AssetId.toString();

			var tempList:ArrayCollection = ActiveRecords.AssetAssignment.findBySql(sql);
			tempList.addEventListener("loaded", 
					function(event:DynamicLoadEvent):void {
						for each (var aa:AssetAssignment in tempList) {
							aa.RelatedAfe = _afes[aa.AFE] as Afe;
							aa.RelatedSubAfe = _subAfes[aa.SubAFE] as SubAfe;
							_assetAssignments[aa.AssetAssignmentId] = aa;
						}
						loadBillItems();
					});

		}
		
		private function loadBillItems():void 
		{	
			if (bill.RelatedBillItem.IsLoaded) {
				processBillItems();
			} else {
				bill.RelatedBillItem.addEventListener("loaded", onBillItemsLoad);
			}
		}
		
		private function onBillItemsLoad(event:DynamicLoadEvent):void 
		{
			bill.RelatedBillItem.removeEventListener("loaded", onBillItemsLoad);
			processBillItems();
		}

		private function processBillItems(e:* = null):void 
		{
			StatusesRegistry.getInstance().removeEventListener(StatusesRegistry.STATUSES_LOADED_EVENT, processBillItems);

			assignments.removeAll();
			
			if (!isBillCorrect) {
				dispatchEvent(new Event("bill_is_broken"));
			}
			
			for each (var billItem:BillItem in bill.RelatedBillItem) {
				
				billItem.loadNotes();
				
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
			
			loaded = true;
			dispatchEvent(new Event("summary_item_loaded"));

            windowTitle  = " Bill Detail Review ";
            windowTitle += "  ( Bill Start Date: ";
            windowTitle += DateUtil.format(new Date(Date.parse(bill.StartDate)));
            windowTitle += "    Asset: ";
            windowTitle += bill.RelatedAsset.FirstName;
            windowTitle += " ";
            windowTitle += bill.RelatedAsset.LastName;
            windowTitle += " )";
		}
		
		private function get isBillCorrect():Boolean 
		{
			for each (var billItem:BillItem in bill.RelatedBillItem) {
				
				if (bill.Status == BillStatus.BILL_STATUS_SUBMITTED 
					&& !(billItem.Status == BillItemStatus.BILL_ITEM_STATUS_SUBMITTED)) {
					return false;
				}
				
				if (bill.Status == BillStatus.BILL_STATUS_CORRECTED 
					&& !(billItem.Status == BillItemStatus.BILL_ITEM_STATUS_APPROVED 
						|| billItem.Status == BillItemStatus.BILL_ITEM_STATUS_CORRECTED
						|| billItem.Status == BillItemStatus.BILL_ITEM_STATUS_SUBMITTED
						|| billItem.Status == BillItemStatus.BILL_ITEM_STATUS_CONFIRMED
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
			for each ( var billItem:BillItem in bill.RelatedBillItem) {
				if ( billItem.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_CONFIRMED
					&& billItem.StatusTemp.Status != BillItemStatus.BILL_ITEM_STATUS_DECLINED ) {
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

        	for each (var billItem:BillItem in bill.RelatedBillItem) {
        		if ( billItem.Status == BillItemStatus.BILL_ITEM_STATUS_SUBMITTED
        			|| billItem.Status == BillItemStatus.BILL_ITEM_STATUS_CORRECTED ) {
       				canSubmit = false;
        		} 
        		if ( billItem.Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED ) {
        			isRejected = true;
        		}
        		if ( billItem.Status != BillItemStatus.BILL_ITEM_STATUS_APPROVED ) {
        			isApproved = false;
        		}

        	}
            
            if (isRejected) {
            	bill.RelatedBillStatus = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_REJECTED);
            } else if (isApproved) {
            	bill.RelatedBillStatus = StatusesRegistry.getInstance().getBillStatusByName(BillStatus.BILL_STATUS_APPROVED);
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
