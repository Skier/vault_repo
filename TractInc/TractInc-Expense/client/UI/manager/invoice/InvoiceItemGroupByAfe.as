package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	import App.Entity.AFEDataObject;
	import App.Entity.InvoiceItemDataObject;
	import App.Entity.AssetAssignmentDataObject;
	
	[Bindable]
	public class InvoiceItemGroupByAfe extends InvoiceItemGroup
	{
		public var afe:String;
		
		public var subAfeGroups:ArrayCollection = new ArrayCollection();
		
		public function InvoiceItemGroupByAfe(controller:InvoiceManagerController) {
			super(controller);
		}

		public override function addItem(item:InvoiceItemDataObject):void 
		{
			super.addItem(item);
			
			var assignment:AssetAssignmentDataObject = AssetAssignmentDataObject(assignmentsHash[item.AssetAssignmentId]);
			
			var group:InvoiceItemGroupBySubAfe = getSubAfeGroupBySubAfe(assignment.SubAFE);
			
			if (group == null) {
				group = new InvoiceItemGroupBySubAfe(controller);
				group.parentInvoice = this.parentInvoice;
				group.subAfe = assignment.SubAFE;
				subAfeGroups.addItem(group);
			}
			
			group.addItem(item);
			
		}
		
		private function getSubAfeGroupBySubAfe(subAfe:String):InvoiceItemGroupBySubAfe {
			for each (var item:InvoiceItemGroupBySubAfe in subAfeGroups) {
				if (item.subAfe == subAfe) {
					return item;
				}
			}
			return null;
		}
		
	}
}