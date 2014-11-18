package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	import App.Entity.ProjectDataObject;
	import App.Entity.InvoiceItemDataObject;
	import App.Entity.AssetAssignmentDataObject;
	import App.Entity.AssetDataObject;
	
	[Bindable]
	public class InvoiceItemGroupBySubAfe extends InvoiceItemGroup
	{
		public var subAfe:String;
		
		public var assetGroups:ArrayCollection = new ArrayCollection();
		
		public function InvoiceItemGroupBySubAfe(controller:InvoiceManagerController) {
			super(controller);
		}

		private var _dailyItemsCount:int = 0;
		public function get dailyItemsCount():int {
			return _dailyItemsCount;
		}
		public function set dailyItemsCount(value:int):void {
			_dailyItemsCount = value;
		}
		
		public override function addItem(item:InvoiceItemDataObject):void 
		{
			super.addItem(item);
			
			var assignment:AssetAssignmentDataObject = AssetAssignmentDataObject(assignmentsHash[item.AssetAssignmentId]);
			
			var group:InvoiceItemGroupByAsset = getAssetGroupByAssetId(assignment.AssetId);
			
			if (group == null) {
				group = new InvoiceItemGroupByAsset(controller);
				group.parentInvoice = this.parentInvoice;
				group.asset = AssetDataObject(assetsHash[assignment.AssetId]);
				assetGroups.addItem(group);
			}
			
			group.addItem(item);
			
			if (1 == item.InvoiceItemTypeId) {
				dailyItemsCount++;
			}
			
		}
		
		private function getAssetGroupByAssetId(id:int):InvoiceItemGroupByAsset {
			for each (var item:InvoiceItemGroupByAsset in assetGroups) {
				if (item.asset.AssetId == id) {
					return item;
				}
			}
			return null;
		}
		
	}
}