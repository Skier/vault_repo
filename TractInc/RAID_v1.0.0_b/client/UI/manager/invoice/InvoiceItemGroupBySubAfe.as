package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	import App.Domain.InvoiceItem;
	import App.Domain.Asset;
	import App.Domain.SubAfe;
	
	[Bindable]
	public class InvoiceItemGroupBySubAfe extends InvoiceItemGroup
	{
		public var subAfe:SubAfe;
		
		public var assetGroups:ArrayCollection = new ArrayCollection();
		
		public override function addItem(item:InvoiceItem):void 
		{
			super.addItem(item);
			
			var group:InvoiceItemGroupByAsset = getAssetGroupByAssetId(item.RelatedAssetAssignment.RelatedAsset.AssetId);
			
			if (group == null) {
				group = new InvoiceItemGroupByAsset;
				group.parentInvoice = this.parentInvoice;
				group.asset = item.RelatedAssetAssignment.RelatedAsset;
				assetGroups.addItem(group);
			}
			
			group.addItem(item);
			
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