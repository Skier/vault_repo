
      package App.Domain
      {
        import App.Domain.Codegen.*;
        import weborb.data.DynamicLoadEvent;
        import weborb.data.ActiveCollection;
        import mx.rpc.Responder;
        import mx.rpc.events.FaultEvent;
        import mx.controls.Alert;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.SubAfe")]
        public dynamic class SubAfe extends _SubAfe
        {
        
		public function canDeleteSubAfe(responder:Function):void {
			var items:ActiveCollection = ActiveRecords.BillItem.findBySql(
				"select bi.[BillItemId], bi.[BillItemTypeId], bi.[BillId], bi.[AssetAssignmentId], bi.[BillingDate], bi.[Qty], bi.[BillRate], bi.[Status], bi.[Notes]" +
				"  from BillItem bi " +
				" 		inner join AssetAssignment aa" +
				"				on bi.AssetAssignmentId = aa.AssetAssignmentId" +
				" where bi.[Status] <> 'CONFIRMED'" + 
				"   and aa.[SubAFE] = '" + SubAFE + "'");
			items.addEventListener("loaded",
				function(evt:DynamicLoadEvent):void {
					responder(0 == ActiveCollection(evt.data).length);
				}
			);
		}
		
    	public function deleteSubAfe():void {
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
    				Alert.show("Cannot store Project. Please contact administrator.", "Error");
    			}
    		));
    	}
    	
        }
      }
    