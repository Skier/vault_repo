
      package App.Domain
      {
        import App.Domain.Codegen.*;
        import weborb.data.DynamicLoadEvent;
        import weborb.data.ActiveCollection;
        import mx.rpc.Responder;
        import mx.controls.Alert;
        import mx.rpc.events.FaultEvent;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.Client")]
        public dynamic class Client extends _Client
        {

		public function canDeleteClient(responder:Function):void {
			var items:ActiveCollection = ActiveRecords.BillItem.findBySql(
				"select bi.[BillItemId], bi.[BillItemTypeId], bi.[BillId], bi.[AssetAssignmentId], bi.[BillingDate], bi.[Qty], bi.[BillRate], bi.[Status], bi.[Notes]" +
				"  from BillItem bi " +
				" 		inner join AssetAssignment aa" +
				"				on bi.AssetAssignmentId = aa.AssetAssignmentId" +
				" 		inner join Afe a" +
				"				on aa.AFE = a.AFE" +
				" where bi.Status <> 'CONFIRMED'" + 
				"   and a.ClientId = " + ClientId.toString());
			items.addEventListener("loaded",
				function(evt:DynamicLoadEvent):void {
					responder(0 == ActiveCollection(evt.data).length);
				}
			);
		}
		
    	public function deleteClient():void {
    		Deleted = true;
    		
    		if (!RelatedAfe.IsLoaded) {
    			RelatedAfe.addEventListener("loaded", onRelatedAfesLoaded);
    		} else {
    			onRelatedAfesLoaded(null);
    		}
    	}
    	
    	private function onRelatedAfesLoaded(evt:DynamicLoadEvent):void {
    		if (null != evt) {
    			RelatedAfe.removeEventListener("loaded", onRelatedAfesLoaded);
    		}
    		
    		save(false, new Responder(
    			function(result:Object):void {
		    		for each (var afe:Afe in RelatedAfe) {
    					afe.deleteAfe();
		    		}
    			},
    			function(evt:FaultEvent):void {
    				Alert.show("Cannot store Client. Please contact administrator.", "Error");
    			}
    		));
    	}
    	
        }
      }
    