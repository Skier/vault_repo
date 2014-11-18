
package App.Domain
{
	
    import App.Domain.Codegen.*;
    import weborb.data.DynamicLoadEvent;
    import weborb.data.ActiveCollection;
        
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Domain.AssetAssignment")]
    public dynamic class AssetAssignment extends _AssetAssignment
    {
        
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
				"select bi.[BillItemId], bi.[BillItemTypeId], bi.[BillId], bi.[AssetAssignmentId], bi.[BillingDate], bi.[Qty], bi.[BillRate], bi.[Status], bi.[Notes]" +
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
    		/* if (!RelatedRateByAssignment.IsLoaded) {
    			RelatedRateByAssignment.addEventListener("loaded", onRelatedRatesLoaded);
    		} else {
    			onRelatedRatesLoaded(null);
    		} */
    	}
/*
    	private function onRelatedRatesLoaded(evt:DynamicLoadEvent):void {
    		if (null != evt) {
    			RelatedRateByAssignment.removeEventListener("loaded", onRelatedRatesLoaded);
    		}
    		
    		for each (var rate:RateByAssignment in this.RelatedRateByAssignment) {
    			rate.deleteRate(false);
    		}
    		save();
    	}
*/
	}
}
    