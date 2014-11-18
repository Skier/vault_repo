package App.Domain
{
	import App.Domain.Codegen.*;
	import weborb.data.ActiveCollection;
	import mx.rpc.Responder;
	import weborb.data.DynamicLoadEvent;      
	import mx.controls.Alert;
	import mx.rpc.events.FaultEvent;

    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Domain.Asset")]
    public dynamic class Asset extends _Asset
    {
   		public function get Name():String 
   		{
			if (FirstName != null && FirstName.length > 0 && LastName != null && LastName.length > 0){
        		return FirstName + " " + LastName;
			} else if (BusinessName != null && BusinessName.length > 0) {
        		return BusinessName;
			} else {
				return "_undefined_";
			}
     	}
     	
     	public function set Name(name:String):void 
     	{
     		return;
     	}

		public function canDeleteAsset(responder:Function):void {
			var items:ActiveCollection = ActiveRecords.BillItem.findBySql(
				"select bi.[BillItemId], bi.[BillItemTypeId], bi.[BillId], bi.[AssetAssignmentId], bi.[BillingDate], bi.[Qty], bi.[BillRate], bi.[Status], bi.[Notes], bi.[BillItemCompositionId]" +
				"  from BillItem bi " +
				" 		inner join Bill b" +
				"				on bi.BillId = b.BillId" +
				" where bi.Status <> 'CONFIRMED'" + 
				"   and b.AssetId = " + AssetId.toString());
			items.addEventListener("loaded",
				function(evt:DynamicLoadEvent):void {
					responder(0 == ActiveCollection(evt.data).length);
				}
			);
		}
		
    	public function deleteAsset():void {
    		Deleted = true;
    		
    		if (!RelatedAssetAssignment.IsLoaded) {
    			RelatedAssetAssignment.addEventListener("loaded", onRelatedAssignmentsLoaded);
    		} else {
    			onRelatedAssignmentsLoaded(null);
    		}
    	}
    	
    	private function onRelatedAssignmentsLoaded(evt:DynamicLoadEvent):void {
    		if (null != evt) {
    			RelatedAssetAssignment.removeEventListener("loaded", onRelatedAssignmentsLoaded);
    		}
    		
    		save(false, new Responder(
    			function(result:Object):void {
		    		for each (var assignment:AssetAssignment in RelatedAssetAssignment) {
    					assignment.deleteAssignment();
		    		}
    			},
    			function(evt:FaultEvent):void {
    				Alert.show("Cannot store Asset. Please contact administrator.", "Error");
    			}
    		));
    	}
    	
  	}
}
