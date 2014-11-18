package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	import App.Entity.AssetDataObject;
	import App.Entity.InvoiceItemDataObject;
	
	[Bindable]
	public class InvoiceItemGroupByAsset extends InvoiceItemGroup
	{
		public var asset:AssetDataObject;
		
		public var typeGroups:ArrayCollection = new ArrayCollection();
		
		public function InvoiceItemGroupByAsset(controller:InvoiceManagerController) {
			super(controller);
		}

		public override function addItem(invoiceItem:InvoiceItemDataObject):void 
		{
			super.addItem(invoiceItem);
			
			var group:InvoiceItemGroupByType = getTypeGroupByTypeId(invoiceItem.InvoiceItemTypeId);
			
			if (group == null) {
				group = new InvoiceItemGroupByType(controller);
				group.parentInvoice = this.parentInvoice;
				group.invoiceItemTypeId = invoiceItem.InvoiceItemTypeId;
				typeGroups.addItem(group);
			}
			
			group.addItem(invoiceItem);
			
		}
		
		private function getTypeGroupByTypeId(id:int):InvoiceItemGroupByType {
			for each (var item:InvoiceItemGroupByType in typeGroups) {
				if (item.invoiceItemTypeId == id) {
					return item;
				}
			}
			return null;
		}
		
	}
}