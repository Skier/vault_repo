package UI.manager.admin.assignment
{
	
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	import mx.events.CloseEvent;
	import mx.events.CollectionEventKind;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.managers.PopUpManager;
	import mx.rpc.Responder;
	import mx.core.IFlexDisplayObject;
	import flash.events.MouseEvent;
	import flash.display.DisplayObject;
	import common.TypesRegistry;
	import UI.manager.admin.AdminController;
	import App.Entity.AssetAssignmentDataObject;
	import App.Entity.ClientDataObject;
	import App.Entity.AssetDataObject;
	import App.Entity.BillDataObject;
	import App.Entity.DefaultInvoiceRateDataObject;
	import App.Entity.DefaultBillRateDataObject;
	import App.Entity.BillItemTypeDataObject;
	import App.Entity.RateByAssignmentDataObject;
	import App.Service.CrewChiefService;
	import App.Entity.BillStatusDataObject;
	import mx.rpc.events.ResultEvent;
	import App.Entity.InvoiceItemTypeDataObject;
	import App.Entity.AFEDataObject;
	import App.Entity.ProjectDataObject;
	import mx.collections.ListCollectionView;
	
	public class AssignmentsController
	{
		
		public var view:AssignmentsView;
		
		[Bindable]
		public var model:AssignmentsModel;

		public var parentController:AdminController;
		
		public function AssignmentsController(view:AssignmentsView, parentController:AdminController)
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new AssignmentsModel();
		}
		
		public function open():void {
			view.dgAssetProjects.dataProvider = null;
			view.dgCrewAssets.dataProvider = null;
			view.dgCrewProjects.dataProvider = null;
			
			model.data = parentController.model.data;
			model.assets = new ListCollectionView(parentController.parentController.model.assets);
			model.assets.filterFunction = function(obj:Object):Boolean {
				return !AssetDataObject(obj).Deleted && (AssetDataObject(obj).ChiefAssetId == AssetDataObject(obj).AssetId);
			};
			model.assets.refresh();
			view.dgCrewChiefs.dataProvider = model.assets;
			
			model.assetsByChief = new Array();
			
			for each (var asset:AssetDataObject in parentController.model.data.Assets) {
				var chiefAssets:ArrayCollection = ArrayCollection(model.assetsByChief[asset.ChiefAssetId]);
				if (null == chiefAssets) {
					chiefAssets = new ArrayCollection();
					chiefAssets.filterFunction = function(obj:Object):Boolean {
						return !AssetDataObject(obj).Deleted;
					}
					chiefAssets.refresh();
					
					model.assetsByChief[asset.ChiefAssetId] = chiefAssets;
				}
				
				chiefAssets.addItem(asset);
			}
			
			model.clients = new ArrayCollection(parentController.model.data.Clients);
			model.clients.filterFunction = function(obj:Object):Boolean {
				var client:ClientDataObject = ClientDataObject(obj);
				return !client.Deleted && client.Active;
			}
			model.clients.refresh();
			
			model.afesByClient = new Array();
			for each (var afe:AFEDataObject in parentController.parentController.appController.Model.afesHash) {
				if ("ISSUED" != afe.AFEStatus) {
					continue;
				}
				
				var clientAfes:ArrayCollection = ArrayCollection(model.afesByClient[afe.ClientId]);
				if (null == clientAfes) {
					clientAfes = new ArrayCollection();
					clientAfes.filterFunction = function(obj:Object):Boolean {
						return !AFEDataObject(parentController.parentController.appController.Model.afesHash[String(obj)]).Deleted;
					}
					clientAfes.refresh();
					
					model.afesByClient[afe.ClientId] = clientAfes;
				}
				
				clientAfes.addItem(afe.AFE);
			}
			
			model.projectsByAfe = new Array();
			for each (var project:ProjectDataObject in parentController.parentController.appController.Model.projectsHash) {
				if ("ISSUED" != project.SubAFEStatus) {
					continue;
				}
				
				var afeProjects:ArrayCollection = model.projectsByAfe[project.AFE] as ArrayCollection;
				if (null == afeProjects) {
					afeProjects = new ArrayCollection();
					afeProjects.filterFunction = function(obj:Object):Boolean {
						if (null == parentController.parentController.appController.Model.projectsHash[String(obj)]) {
							return false;
						}
						
						return !ProjectDataObject(parentController.parentController.appController.Model.projectsHash[String(obj)]).Deleted;
					}
					afeProjects.refresh();
					
					model.projectsByAfe[project.AFE] = afeProjects;
				}
				
				afeProjects.addItem(project.SubAFE);
			}
			
			model.projectsByAsset = new Array();
			for each (var chiefAssignment:AssetAssignmentDataObject in model.data.Assignments) {
				if (("ISSUED" != chiefAssignment.AFEStatus)
						|| ("ISSUED" != chiefAssignment.ProjectStatus)) {
					continue;
				}
				
				var assetProjects:ArrayCollection = model.projectsByAsset[chiefAssignment.AssetId] as ArrayCollection;
				if (null == assetProjects) {
					assetProjects = new ArrayCollection();
					model.projectsByAsset[chiefAssignment.AssetId] = assetProjects;
				}
				assetProjects.addItem(chiefAssignment);
			}
			
			model.isLoading = false;
		}
		
		public function getAssignmentById(id:int):AssetAssignmentDataObject
		{
			var assignments:ArrayCollection = view.dgAssetProjects.dataProvider as ArrayCollection;
			
			for each (var assignment:AssetAssignmentDataObject in assignments) {
				if (assignment.AssetAssignmentId == id) {
					return assignment;
				}
			}

			return null;
		}
		
		public function onClickNewAssetAssignment():void 
		{
			var assignment:AssetAssignmentDataObject = new AssetAssignmentDataObject();
			
			assignment.ratesHash = new Array();
			
			for each (var billItemType:BillItemTypeDataObject in TypesRegistry.instance.countableBillItemTypes) {
				var rate:RateByAssignmentDataObject = new RateByAssignmentDataObject();
				rate.BillItemTypeId = billItemType.BillItemTypeId;
				rate.BillRate = 0;
				rate.InvoiceRate = 0;
				rate.Deleted = false;
				
				assignment.ratesHash[rate.BillItemTypeId] = rate;
			}
			
			assignment.ProjectStatus = "ISSUED";

			var collection:ArrayCollection = view.dgAssetProjects.dataProvider as ArrayCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAssignmentsCollectionChanged);
			}
			
			model.currentAsset = AssetDataObject(view.dgCrewChiefs.selectedItem);
			assignment.AssetId = model.currentAsset.AssetId;
			
			openAssetAssignment(assignment);
		}
		
		public function onClickEditAssetAssignment():void 
		{
			var assignment:AssetAssignmentDataObject = view.dgAssetProjects.selectedItem as AssetAssignmentDataObject;
			model.currentAsset = AssetDataObject(view.dgCrewAssets.selectedItem);
			if (assignment == null) {
				return;
			} else {
				openAssetAssignment(assignment);
			}
		}
		
		public function onClickDeleteAssetAssignment():void 
		{
			var assetAssignment:AssetAssignmentDataObject = view.dgCrewProjects.selectedItem as AssetAssignmentDataObject;
			
			CrewChiefService.getInstance().canDeleteAssignment(assetAssignment.AssetAssignmentId, new Responder(
				function(result:ResultEvent):void {
					if (Boolean(result.result)) {
            			Alert.show("Are you really want to delete selected Asset Assignment and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	CrewChiefService.getInstance().deleteAssignment(assetAssignment.AssetAssignmentId, new Responder(
			                    		function(result:ResultEvent):void {
			                    			var chief:Object = view.dgCrewChiefs.selectedItem;
			                    			open();
			                    			view.dgCrewChiefs.selectedItem = chief;
			                    			view.onCrewChiefChange();
			                    		},
			                    		function(fault:FaultEvent):void {
			                    			Alert.show("Cannot delete assignment. Please contact administrator", "System Error");
			                    		}
			                    	));
			                    	var assignments:ListCollectionView = ListCollectionView(view.dgCrewProjects.dataProvider);
			                    	assignments.removeItemAt(assignments.getItemIndex(assetAssignment));
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Asset Assignment because there are not confirmed items", "Delete");
     				}
    			},
    			function(fault:FaultEvent):void {
					Alert.show("Please contact administrator.", "System Error");
    			}
    		));
		}

		public function getClientById(id:int):ClientDataObject
		{
			return parentController.getClientById(id);
		}
		
		private function openAssetAssignment(assignment:AssetAssignmentDataObject):void 
		{
			var popup:AssetAssignmentDetail = AssetAssignmentDetail.Open(assignment, view.parentDocument as DisplayObject, model);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onAssetAssignmentSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onAssetAssignmentSubmit(event:MouseEvent):void 
		{
			var popup:AssetAssignmentDetail = event.currentTarget.parentDocument as AssetAssignmentDetail;
			
			var assetAssignment:AssetAssignmentDataObject = popup.assetAssignment;
		
			popup.enabled = false;
			
			if (0 == assetAssignment.AssetAssignmentId) {
				var afe:String = popup.cbAfes.selectedItem as String;
				var subAfe:String = popup.cbSubAfes.selectedItem as String;
	
				var aa:AssetAssignmentDataObject = getAssetAssignment(afe, subAfe);
				
				if (aa != null) {
					Alert.show("Asset assignment already exists");
					return;
				}
			
				assetAssignment.AFE = afe;
				assetAssignment.SubAFE = subAfe;
				assetAssignment.AssetId = AssetDataObject(view.dgCrewChiefs.selectedItem).AssetId;
				assetAssignment.ClientId = ClientDataObject(popup.cbClients.selectedItem).ClientId;
				assetAssignment.ClientName = ClientDataObject(popup.cbClients.selectedItem).ClientName;
				
				var clientInfo:ClientDataObject = null;
				
				for each (clientInfo in model.clients) {
					if (assetAssignment.ClientId == clientInfo.ClientId) {
						break;
					}
				}
				
				if (null == clientInfo) {
					popup.enabled = true;
					Alert.show("Please contact administrator", "System Error");
					return;
				}
				
				var defaultBillRates:ArrayCollection = new ArrayCollection(AssetDataObject(view.dgCrewChiefs.selectedItem).DefaultRates);
				var defaultInvoiceRates:ArrayCollection = new ArrayCollection(clientInfo.DefaultRates);
				
				if ((0 == defaultBillRates.length)
						|| (0 == defaultInvoiceRates.length)) {
					Alert.show("Cannot create assignment. There is no default rates.", "Application Error");
					view.enabled = true;
					return;
				}
				
				var defaultBillRatesHash:Array = new Array();
				var defaultInvoiceRatesHash:Array = new Array();
				
				for each (var defaultBillRate:DefaultBillRateDataObject in defaultBillRates) {
					defaultBillRatesHash[defaultBillRate.BillItemTypeId] = defaultBillRate;
				}
					
				for each (var defaultInvoiceRate:DefaultInvoiceRateDataObject in defaultInvoiceRates) {
					defaultInvoiceRatesHash[defaultInvoiceRate.InvoiceItemTypeId] = defaultInvoiceRate;
				}
					
				assetAssignment.Rates = new Array();
				
				for each (var billItemTypeInfo:BillItemTypeDataObject in TypesRegistry.instance.getAllBillItemTypes) {
					if (billItemTypeInfo.IsPresetRate) {
						var rateByAssignment:RateByAssignmentDataObject = new RateByAssignmentDataObject();
						rateByAssignment.BillItemTypeId = billItemTypeInfo.BillItemTypeId;
						
						if (null != defaultBillRatesHash[billItemTypeInfo.BillItemTypeId]) {
							rateByAssignment.BillRate = DefaultBillRateDataObject(defaultBillRatesHash[billItemTypeInfo.BillItemTypeId]).BillRate;
						}
						
						if (null != defaultInvoiceRatesHash[billItemTypeInfo.InvoiceItemTypeId]) {
							rateByAssignment.InvoiceRate = DefaultInvoiceRateDataObject(defaultInvoiceRatesHash[billItemTypeInfo.InvoiceItemTypeId]).InvoiceRate;
						}
						
						rateByAssignment.ShouldNotExceedRate = false;
						assetAssignment.Rates.push(rateByAssignment);
					}
				}
			} else {
				for each (var assignmentRate:RateByAssignmentDataObject in assetAssignment.Rates) {
					assignmentRate.fromTempFields();
				}
			}
			
			CrewChiefService.getInstance().storeAssignment(assetAssignment, new Responder(
				function(result:ResultEvent):void {
					PopUpManager.removePopUp(popup);
					if (null == model.projectsByAsset[assetAssignment.AssetId]) {
						model.projectsByAsset[assetAssignment.AssetId] = new ArrayCollection();
					}
					
					var assignments:ListCollectionView = ListCollectionView(model.projectsByAsset[assetAssignment.AssetId]);
					if (!assignments.contains(assetAssignment)) {
						assignments.addItem(assetAssignment);
						ListCollectionView(view.dgCrewProjects.dataProvider).addItem(assetAssignment);
					}
				},
				function(fault:FaultEvent):void {
					popup.enabled = true;
					Alert.show("Please contact administrator.", "System Error");
				}
			));
		}
		
		private function getAssetAssignment(afe:String, subAfe:String):AssetAssignmentDataObject
		{
			var collection:ArrayCollection = view.dgCrewProjects.dataProvider as ArrayCollection;

			for each (var aa:AssetAssignmentDataObject in collection) {
				if (aa.AFE == afe && aa.SubAFE == subAfe) {
					return aa;
				}
			}

			return null;
		}

 		private function onAssignmentsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastAddedIndex:int = ArrayCollection(view.dgAssetProjects.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAssetProjects.selectedIndex = lastAddedIndex;
				view.dgAssetProjects.scrollToIndex(lastAddedIndex);
			}
		}
		
		private function onClickCancel(event:MouseEvent):void 
		{
			var popup:IFlexDisplayObject = event.currentTarget.parentDocument as IFlexDisplayObject;
			PopUpManager.removePopUp(popup);
		}
		
		private function get chiefs():ArrayCollection 
		{
			return view.dgAssetProjects.dataProvider as ArrayCollection;
		}
		
		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
		
	}
	
}
