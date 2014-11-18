package UI.manager.invoice
{
	
    import mx.collections.ArrayCollection;
    import common.StatusesRegistry;
    import flash.events.Event;
    import mx.events.CollectionEvent;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.Responder;
    import App.Entity.InvoiceDataObject;
    import App.Entity.InvoiceItemDataObject;
    import App.Entity.AssetAssignmentDataObject;
    import App.Entity.AssetDataObject;
    import App.Entity.ProjectDataObject;
    import App.Entity.AFEDataObject;
    import mx.rpc.events.ResultEvent;
    import mx.controls.Alert;
    import mx.rpc.remoting.RemoteObject;
    import App.Service.ManagerService;
    
    [Bindable]
    public class InvoiceStructure extends InvoiceItemGroup
    {
    	
        public var invoice:InvoiceDataObject;
        public var afeGroups:ArrayCollection = new ArrayCollection();
        public var additionalItems:ArrayCollection = new ArrayCollection();
        
        public var isLoaded:Boolean = false;

        private var _afes:ArrayCollection = new ArrayCollection();

        private var _subAfes:ArrayCollection = new ArrayCollection();

        private var _assets:ArrayCollection = new ArrayCollection();
        
        private var _assetAssignments:ArrayCollection = new ArrayCollection();
        
        private var _types:ArrayCollection = new ArrayCollection();
        
		public function InvoiceStructure(controller:InvoiceManagerController) {
			super(controller);
		}

        public function init(actualInvoice:InvoiceDataObject):void 
        {
            isLoaded = false;
            afeGroups = new ArrayCollection();
            
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetInvoiceData.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				invoice = result.result as InvoiceDataObject;
       				
					for each (var assignmentInfo:AssetAssignmentDataObject in invoice.Assignments) {
						assignmentsHash[assignmentInfo.AssetAssignmentId] = assignmentInfo;
					}
       				
       				parseItems();
       				
					isLoaded = true;
       			}
       		);
       		userService.GetInvoiceData.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetInvoiceData(actualInvoice.InvoiceId);
        }
        
        public function setStatus(status:String):void 
        {
            invoice.Status = status;

            for each (var invoiceItem:InvoiceItemDataObject in invoice.InvoiceItems) {
                invoiceItem.Status = status;
            }
        }
        
        public function save(responder:Responder):void 
        {
            if (deletedItems.length > 0) {
                for each (var invoiceItem:InvoiceItemDataObject in deletedItems) {
                	invoiceItem.Deleted = true;
                }
            }
            
            ManagerService.getInstance().storeInvoice(invoice, new Responder(
            	function(result:ResultEvent):void {
		            for each (var item:InvoiceItemDataObject in deletedItems) {
	                    deletedItems.removeItemAt(deletedItems.getItemIndex(item));
   					}
   					responder.result(result);
            	},
            	function(fault:FaultEvent):void {
            		responder.fault(fault);
            	}
            ));
        }
        
        public override function removeItem(item:InvoiceItemDataObject):void 
        {
            var index:int;
            
            if (additionalItems.contains(item)) {
                index = additionalItems.getItemIndex(item);
                if (index >= 0) {
                    additionalItems.removeItemAt(index);
                }
            }

            if (-1 < invoice.InvoiceItems.indexOf(item)) {
                index = invoice.InvoiceItems.indexOf(item);
                if (index >= 0) {
                    invoice.InvoiceItems.splice(index, 1);
                }
            }

            super.removeItem(item);
        }
        
        private function parseItems():void 
        {
            for each (var invoiceItem:InvoiceItemDataObject in invoice.InvoiceItems) {
                addItem(invoiceItem);
            }
            isLoaded = true;
            dispatchEvent(new Event("invoice_structure_loaded"));
        }

        public override function addItem(item:InvoiceItemDataObject):void 
        {
            var assignment:AssetAssignmentDataObject = AssetAssignmentDataObject(assignmentsHash[item.AssetAssignmentId]);
            if (null == assignment) {
            	return;
            }
            
            super.addItem(item);
            
            if (item.BillItemId == 0) {
                additionalItems.addItem(item);
            } else {
                var group:InvoiceItemGroupByAfe = getAfeGroupByAfe(assignment.AFE);
                if (group == null) {
                    group = new InvoiceItemGroupByAfe(controller);
                    group.parentInvoice = invoice;
                    group.afe = assignment.AFE;
                    afeGroups.addItem(group);
                }
                group.addItem(item);
            }
            
            if (null != invoice.InvoiceItems) {
            	if (-1 == invoice.InvoiceItems.indexOf(item)) {
                	invoice.InvoiceItems.push(item);
            	}
            }
            
            var afe:AFEDataObject = AFEDataObject(controller.parentController.appController.Model.afesHash[assignment.AFE]);
            if (!_afes.contains(afe)) {
            	_afes.addItem(afe);
            }
            
            var project:ProjectDataObject = ProjectDataObject(controller.parentController.appController.Model.projectsHash[assignment.SubAFE]);
            if (!_subAfes.contains(project)) {
            	_subAfes.addItem(project);
            }
            
            var asset:AssetDataObject = AssetDataObject(controller.parentController.model.assetsHash[assignment.AssetId]);
            if (!_assets.contains(asset)) {
            	_assets.addItem(asset);
            }
            
            if (!_assetAssignments.contains(assignment)) {
            	_assetAssignments.addItem(assignment);
            }
        }
        
        public function getItemTypes():ArrayCollection 
        {
            return _types;
        }
        
        public function getAfes():ArrayCollection 
        {
            return _afes;
        }
        
        public function getSubAfes():ArrayCollection 
        {
            return _subAfes;
        }
        
        public function getAssets():ArrayCollection 
        {
            return _assets;
        }
        
        public function getAssetAssignments():ArrayCollection 
        {
            return _assetAssignments;
        }
        
        private function getAfeGroupByAfe(id:String):InvoiceItemGroupByAfe 
        {
            for each (var item:InvoiceItemGroupByAfe in afeGroups) {
                if (item.afe == id) {
                    return item;
                }
            }
            return null;
        }

        private function getAfeById(AFE:String):AFEDataObject
        {
            for each (var afe:AFEDataObject in _afes) {
                if (afe.AFE == AFE) {
                    return afe;
                }
            }
            throw new Error("Afe not found - " + AFE);
            return null;
        }

        private function getSubAfeById(SubAFE:String):ProjectDataObject
        {
            for each (var subAfe:ProjectDataObject in _subAfes) {
                if (subAfe.SubAFE == SubAFE) {
                    return subAfe;
                }
            }
            throw new Error("Project not found - " + SubAFE);
            return null;
        }

        private function getAssetById(id:int):AssetDataObject
        {
            for each (var asset:AssetDataObject in _assets) {
                if (asset.AssetId == id) {
                    return asset;
                }
            }
            throw new Error("Asset not found - " + id.toString());
            return null;
        }
        
        private function getAssetAssignmentById(id:int):AssetAssignmentDataObject
        {
            for each (var assetAssignment:AssetAssignmentDataObject in _assetAssignments) {
                if (assetAssignment.AssetAssignmentId == id) {
                    return assetAssignment;
                }
            }
            throw new Error("AssetAssignment not found - " + id.toString());
            return null;
        }
        
        private function onFault(event:FaultEvent):void {
            dispatchEvent(event);
        }
        
    }
}