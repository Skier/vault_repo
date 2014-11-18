package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	import App.Domain.InvoiceItem;
	import App.Domain.Asset;
	import App.Domain.Afe;
	
	[Bindable]
	public class InvoiceItemGroupByAfe extends InvoiceItemGroup
	{
		public var afe:Afe;
		
		public var subAfeGroups:ArrayCollection = new ArrayCollection();
		
		public override function addItem(item:InvoiceItem):void 
		{
			super.addItem(item);
			
			var group:InvoiceItemGroupBySubAfe = getSubAfeGroupBySubAfe(item.RelatedAssetAssignment.SubAFE);
			
			if (group == null) {
				group = new InvoiceItemGroupBySubAfe;
				group.parentInvoice = this.parentInvoice;
				group.subAfe = item.RelatedAssetAssignment.RelatedSubAfe;
				subAfeGroups.addItem(group);
			}
			
			group.addItem(item);
			
		}
		
		private function getSubAfeGroupBySubAfe(subAfe:String):InvoiceItemGroupBySubAfe {
			for each (var item:InvoiceItemGroupBySubAfe in subAfeGroups) {
				if (item.subAfe.SubAFE == subAfe) {
					return item;
				}
			}
			return null;
		}
		
	}
}