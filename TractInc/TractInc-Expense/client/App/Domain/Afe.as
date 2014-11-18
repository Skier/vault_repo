
      package App.Domain
      {
        import App.Domain.Codegen.*;
        import weborb.data.DynamicLoadEvent;
        import weborb.data.ActiveCollection;
        import mx.rpc.Responder;
        import mx.rpc.events.FaultEvent;
        import mx.controls.Alert;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.Afe")]
        public dynamic class Afe extends _Afe
        {
        
		public function canDeleteAfe(responder:Function):void {
			var items:ActiveCollection = ActiveRecords.BillItem.findBySql(
				"select bi.[BillItemId], bi.[BillItemTypeId], bi.[BillId], bi.[AssetAssignmentId], bi.[BillingDate], bi.[Qty], bi.[BillRate], bi.[Status], bi.[Notes], bi.[BillItemCompositionId]" +
				"  from BillItem bi " +
				" 		inner join AssetAssignment aa" +
				"				on bi.AssetAssignmentId = aa.AssetAssignmentId" +
				" where bi.[Status] <> 'CONFIRMED'" + 
				"   and aa.[AFE] = '" + AFE + "'");
			items.addEventListener("loaded",
				function(evt:DynamicLoadEvent):void {
					responder(0 == ActiveCollection(evt.data).length);
				}
			);
		}
		
    	public function deleteAfe():void {
    		Deleted = true;
    		
    		if (!RelatedSubAfe.IsLoaded) {
    			RelatedSubAfe.addEventListener("loaded", onRelatedSubAfesLoaded);
    		} else {
    			onRelatedSubAfesLoaded(null);
    		}
    	}
    	
    	private function onRelatedSubAfesLoaded(evt:DynamicLoadEvent):void {
    		if (null != evt) {
    			RelatedSubAfe.removeEventListener("loaded", onRelatedSubAfesLoaded);
    		}
    		
    		save(false, new Responder(
    			function(result:Object):void {
		    		for each (var subAfe:SubAfe in RelatedSubAfe) {
    					subAfe.deleteSubAfe();
		    		}
    			},
    			function(evt:FaultEvent):void {
    				Alert.show("Cannot store AFE. Please contact administrator.", "Error");
    			}
    		));
    	}
    	
        }
      }
    