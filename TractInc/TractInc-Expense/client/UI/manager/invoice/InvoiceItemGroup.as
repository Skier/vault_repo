package UI.manager.invoice
{
	import flash.events.EventDispatcher;
	import mx.binding.utils.ChangeWatcher;
	import mx.collections.ArrayCollection;
	import mx.events.PropertyChangeEventKind;
	import App.Entity.InvoiceItemDataObject;
	import App.Entity.InvoiceDataObject;

	[Bindable]
	public class InvoiceItemGroup extends EventDispatcher
	{
		public var parentInvoice:InvoiceDataObject;

		public var totalDays:Number = 0;
		public var dailyAmount:Number = 0;
		public var otherAmount:Number = 0;
		public var totalAmount:Number = 0;
		
		public var assignmentsHash:Array;
		
		public var assetsHash:Array;
		
        public var controller:InvoiceManagerController;

		public function InvoiceItemGroup(controller:InvoiceManagerController) {
			this.controller = controller;
			
			assignmentsHash = controller.model.assignmentsHash;
			assetsHash = controller.parentController.model.assetsHash;
		}

		private var _items:ArrayCollection = new ArrayCollection();
		public function get items():ArrayCollection 
		{ 
			return _items; 
		}
		
		private var _deletedItems:ArrayCollection = new ArrayCollection();
		public function get deletedItems():ArrayCollection 
		{ 
			return _deletedItems; 
		}
		
		private var _isSelected:Boolean;
		public function get IsSelected():Boolean 
		{
			return _isSelected;
		}
		public function set IsSelected(selected:Boolean):void 
		{
			_isSelected = selected;
		}

		private var _partiallySelected:Boolean = false;
		public function get partiallySelected():Boolean 
		{
			return _partiallySelected;
		}

		public function set partiallySelected(selected:Boolean):void 
		{
			_partiallySelected = selected;
		}

		public function setSelected(selected:Boolean):void 
		{
			IsSelected = selected;
			
			for each (var item:InvoiceItemDataObject in items) {
				item.IsSelected = selected;
			}
		}
		
		public function addItem(item:InvoiceItemDataObject):void 
		{
			if (item == null) {
				return;
			}
			
			ChangeWatcher.watch(item, ["Qty"], onItemChanged);
			ChangeWatcher.watch(item, ["InvoiceRate"], onItemChanged);
			ChangeWatcher.watch(item, ["IsSelected"], onItemChanged);
			
			items.addItem(item);
			
			onItemChanged();
			onItemSelected();
		}
		
		public function removeItem(item:InvoiceItemDataObject):void 
		{
			if (item == null) {
				return;
			}
			
			var index:int = items.getItemIndex(item);
			if (index != -1) {
				items.removeItemAt(index);
				if (item.InvoiceItemId > 0 && !existsInDeleted(item)) {
					deletedItems.addItem(item);
				}
			}
			
			onItemChanged();
		}
		
		private function existsInDeleted(item:InvoiceItemDataObject):Boolean {
			for each (var i:InvoiceItemDataObject in deletedItems) {
				if (i.InvoiceItemId == item.InvoiceItemId) {
					return true;
				}
			}
			return false;
		}
		
		private function onItemChanged(event:* = null):void 
		{
			totalDays = 0;
			dailyAmount = 0;
			otherAmount = 0;
			totalAmount = 0;
			
			for each (var item:InvoiceItemDataObject in items) {

				if (item.IsSelected) {
					var amount:Number = item.Qty * item.InvoiceRate;
	
					if (item.InvoiceItemTypeId == 1) {
						totalDays += item.Qty;
						amount = item.Qty * item.InvoiceRate;
						dailyAmount += amount;
					} else {
						otherAmount += amount;
					}
	
					totalAmount += amount;
				}

			}
			
			onItemSelected();
		}

		private function onItemSelected(event:* = null):void 
		{
			var allSelected:Boolean = true;
			var nothingSelected:Boolean = true;
			
			for each (var item:InvoiceItemDataObject in items) {
				if (item.IsSelected) {
					nothingSelected = false;
					partiallySelected = true;
				} else {
					allSelected = false;
				}
			}
			
			if (allSelected) {
				IsSelected = true;
			} else if (nothingSelected) {
				IsSelected = false;
			} else {
				partiallySelected = true;
			}
			
		}

	}
}
