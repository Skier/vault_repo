package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	import App.Domain.InvoiceItem;
	import App.Domain.Asset;
	
	[Bindable]
	public class InvoiceItemGroupByAsset extends InvoiceItemGroup
	{
		public var asset:Asset;
		
		public var typeGroups:ArrayCollection = new ArrayCollection();
		
		public override function addItem(invoiceItem:InvoiceItem):void 
		{
			super.addItem(invoiceItem);
			
			var group:InvoiceItemGroupByType = getTypeGroupByTypeId(invoiceItem.InvoiceItemTypeId);
			
			if (group == null) {
				group = new InvoiceItemGroupByType;
				group.parentInvoice = this.parentInvoice;
				group.invoiceItemType = invoiceItem.RelatedInvoiceItemType;
				typeGroups.addItem(group);
			}
			
			group.addItem(invoiceItem);
			
		}
		
		private function getTypeGroupByTypeId(id:int):InvoiceItemGroupByType {
			for each (var item:InvoiceItemGroupByType in typeGroups) {
				if (item.invoiceItemType.InvoiceItemTypeId == id) {
					return item;
				}
			}
			return null;
		}
		
	}
}