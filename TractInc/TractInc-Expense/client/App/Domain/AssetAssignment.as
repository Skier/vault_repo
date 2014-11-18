
package App.Domain
{
	
    import App.Domain.Codegen.*;
    import weborb.data.DynamicLoadEvent;
    import weborb.data.ActiveCollection;
        
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Domain.AssetAssignment")]
    public dynamic class AssetAssignment extends _AssetAssignment
    {
        
    	public function get clientName():String {
    		return RelatedAfe.RelatedClient.ClientName;
    	}

		public function get afeName():String {
			return RelatedAfe.AFEName;
		}
		
		public function get subAfeName():String {
			return RelatedSubAfe.SubAFE;
		}
		
    	public function isEditable():Boolean {
        	return RelatedAfe.RelatedClient.Active
				&& (AfeStatus.AFE_STATUS_ISSUED == RelatedAfe.AFEStatus
					|| AfeStatus.AFE_STATUS_UNLOCKED == RelatedAfe.AFEStatus)
				&& (SubAfeStatus.SUBAFE_STATUS_ISSUED == RelatedSubAfe.SubAFEStatus
					|| SubAfeStatus.SUBAFE_STATUS_UNLOCKED == RelatedSubAfe.SubAFEStatus)
				&& !Deleted;
        }
        	
		public function canDeleteAssignment(responder:Function):void {
			var items:ActiveCollection = ActiveRecords.BillItem.findBySql(
				"select bi.[BillItemId], bi.[BillItemTypeId], bi.[BillId], bi.[AssetAssignmentId], bi.[BillingDate], bi.[Qty], bi.[BillRate], bi.[Status], bi.[Notes], bi.[BillItemCompositionId]" +
				"  from BillItem bi " +
				" where bi.[Status] <> 'CONFIRMED'" + 
				"   and bi.[AssetAssignmentId] = " + AssetAssignmentId.toString());
			items.addEventListener("loaded",
				function(evt:DynamicLoadEvent):void {
					responder(0 == ActiveCollection(evt.data).length);
				}
			);
		}
		
    	public function deleteAssignment():void {
    		Deleted = true;
    		save();
    	}

	}
	
}
