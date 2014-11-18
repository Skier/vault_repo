package UI.crew.asset
{
	
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	import mx.events.CloseEvent;
	import flash.events.MouseEvent;
	import mx.controls.Alert;
	import flash.display.DisplayObject;
	import mx.managers.PopUpManager;
	import mx.rpc.Responder;
	import mx.core.IFlexDisplayObject;
	import mx.rpc.events.FaultEvent;
	import mx.events.CollectionEventKind;
	import common.TypesRegistry;
	import common.PermissionsRegistry;
	import App.Entity.AssetDataObject;
	import App.Entity.AssetAssignmentDataObject;
	import App.Entity.ClientDataObject;
	import App.Entity.AFEDataObject;
	import App.Entity.ProjectDataObject;
	import App.Service.CrewChiefService;
	import mx.rpc.events.ResultEvent;
	import App.Entity.RateByAssignmentDataObject;
	import App.Entity.DefaultBillRateDataObject;
	import App.Entity.BillItemTypeDataObject;
	import App.Entity.DefaultInvoiceRateDataObject;
	import App.Entity.InvoiceItemTypeDataObject;
	import App.Entity.CrewChiefDataObject;
	import mx.collections.ListCollectionView;
	
	public class AssetsController
	{
		
		public var view:AssetsView;
		
		[Bindable]
		public var model:AssetsModel;

		public var parentController:InvoiceController;
		
		public function AssetsController(view:AssetsView, parentController:InvoiceController)
		{
			this.view = view;
			this.parentController = parentController;
		}
		
		public function open(data:CrewChiefDataObject):void {
			model = new AssetsModel();
			
			model.data = data;
			
			for each (var asset:AssetDataObject in model.data.Assets) {
				if (asset.AssetId == asset.ChiefAssetId) {
					continue;
				}
				
				model.assets.addItem(asset);
			}
			
			model.afesByClient = new Array();
			model.projectsByAfe = new Array();
			model.projectsByAsset = new Array();
			
			for each (var chiefAssignment:AssetAssignmentDataObject in model.data.Assignments) {
				if (("ISSUED" != chiefAssignment.AFEStatus)
						|| ("ISSUED" != chiefAssignment.ProjectStatus)) {
					continue;
				}
				
				var clientAfes:ArrayCollection = model.afesByClient[chiefAssignment.ClientId] as ArrayCollection;
				
				if (null == clientAfes) {
					clientAfes = new ArrayCollection();
					model.afesByClient[chiefAssignment.ClientId] = clientAfes;
				}
				
				if (!clientAfes.contains(chiefAssignment.AFE)) {
					clientAfes.addItem(chiefAssignment.AFE);
				}
				
				var afeProjects:ArrayCollection = model.projectsByAfe[chiefAssignment.AFE] as ArrayCollection;
				if (null == afeProjects) {
					afeProjects = new ArrayCollection();
					model.projectsByAfe[chiefAssignment.AFE] = afeProjects;
				}
				
				if (!afeProjects.contains(chiefAssignment.SubAFE)) {
					afeProjects.addItem(chiefAssignment.SubAFE);
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
		
		public function getAssetById(id:int):AssetDataObject 
		{
			var assets:ArrayCollection = view.dgAssets.dataProvider as ArrayCollection;
			
			for each (var asset:AssetDataObject in assets) {
				if (asset.AssetId == id) {
					return asset;
				}
			}

			return null;
		}
		
		public function onClickNewAssetAssignment():void 
		{
			var asset:AssetDataObject = view.dgAssets.selectedItem as AssetDataObject;
			if (asset == null) {
				return;
			} else {
				var assetAssignment:AssetAssignmentDataObject = new AssetAssignmentDataObject();
				assetAssignment.AssetId = asset.AssetId;

				var collection:ArrayCollection = view.dgAssetAssignments.dataProvider as ArrayCollection;
				if (collection) {
					collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAssetAssignmentsCollectionChanged);
				}
				
				var popup:AssetAssignmentDetail = AssetAssignmentDetail.Open(assetAssignment, view.parentDocument as DisplayObject, model);
				popup.btnSubmit.addEventListener(MouseEvent.CLICK, onAssetAssignmentSubmit);
				popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
			}
		}
		
		public function onClickDeleteAssetAssignment():void 
		{
			var assetAssignment:AssetAssignmentDataObject = view.dgAssetAssignments.selectedItem as AssetAssignmentDataObject;
			
			CrewChiefService.getInstance().canDeleteAssignment(assetAssignment.AssetAssignmentId, new Responder(
				function(result:ResultEvent):void {
					if (Boolean(result.result)) {
            			Alert.show("Are you really want to delete selected Asset Assignment and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	CrewChiefService.getInstance().deleteAssignment(assetAssignment.AssetAssignmentId, new Responder(
			                    		function(result:ResultEvent):void {
			                    		},
			                    		function(fault:FaultEvent):void {
			                    			Alert.show("Cannot delete assignment. Please contact administrator", "System Error");
			                    		}
			                    	));
			                    	assetAssignment.Deleted = true;
			                    	/* var assignments:ArrayCollection = ArrayCollection(view.dgAssetAssignments.dataProvider);
			                    	if (assignments.contains(assetAssignment)) {
			                    		assignments.removeItemAt(assignments.getItemIndex(assetAssignment));
			                    	} */
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

		private function onAssetsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastAddedIndex:int = ArrayCollection(view.dgAssets.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAssets.selectedIndex = lastAddedIndex;
				view.dgAssets.scrollToIndex(lastAddedIndex);
			}
		}
		
		public function onClickViewAssetAssignment(assetAssignment:AssetAssignmentDataObject):void 
		{
			var popupRO:AssetAssignmentDetailRO = AssetAssignmentDetailRO.Open(assetAssignment, view.parentDocument as DisplayObject, true);
			popupRO.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onAssetAssignmentSubmit(event:MouseEvent):void 
		{
			var popup:AssetAssignmentDetail = event.currentTarget.parentDocument as AssetAssignmentDetail;
			
			var assetAssignment:AssetAssignmentDataObject = new AssetAssignmentDataObject();
		
			var afe:String = popup.cbAfes.selectedItem as String;
			var subAfe:String = popup.cbSubAfes.selectedItem as String;

			var aa:AssetAssignmentDataObject = getAssetAssignment(afe, subAfe);
			
			if (aa != null) {
				Alert.show("Asset assignment already exists");
				return;
			}
			
			assetAssignment.AFE = afe;
			assetAssignment.SubAFE = subAfe;
			assetAssignment.AssetId = AssetDataObject(view.dgAssets.selectedItem).AssetId;
			assetAssignment.ClientId = ClientDataObject(popup.cbClients.selectedItem).ClientId;
			assetAssignment.ClientName = ClientDataObject(popup.cbClients.selectedItem).ClientName;
			
			popup.enabled = false;
			
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
			
			var defaultBillRates:ArrayCollection = new ArrayCollection(AssetDataObject(view.dgAssets.selectedItem).DefaultRates);
			if (0 == defaultBillRates.length) {
				Alert.show("Cannot create assignment. There is no default rates.", "Application Error");
				view.enabled = true;
				return;
			}
			
			var defaultInvoiceRates:ArrayCollection = new ArrayCollection(clientInfo.DefaultRates);
			if (0 == defaultInvoiceRates.length) {
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
			assetAssignment.ProjectStatus = "ISSUED";
			
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
			
			CrewChiefService.getInstance().storeAssignment(assetAssignment, new Responder(
				function(result:ResultEvent):void {
					PopUpManager.removePopUp(popup);
					
					ArrayCollection(view.dgAssetAssignments.dataProvider).addItem(assetAssignment);
					
					if (null == model.projectsByAsset[assetAssignment.AssetId]) {
						model.projectsByAsset[assetAssignment.AssetId] = new ArrayCollection();
					}
					
					var assignments:ListCollectionView = ListCollectionView(model.projectsByAsset[assetAssignment.AssetId]);
					if (!assignments.contains(assetAssignment)) {
						assignments.addItem(assetAssignment);
					}
				},
				function(fault:FaultEvent):void {
					popup.enabled = true;
					Alert.show("Please contact administrator.", "System Error");
				}
			));
		}
		
 		private function onAssetAssignmentsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ArrayCollection(view.dgAssetAssignments.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAssetAssignments.selectedIndex = lastGridIndex;
				view.dgAssetAssignments.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getAssetAssignment(afe:String, subAfe:String):AssetAssignmentDataObject
		{
			var collection:ArrayCollection = view.dgAssetAssignments.dataProvider as ArrayCollection;

			for each (var aa:AssetAssignmentDataObject in collection) {
				if (aa.AFE == afe && aa.SubAFE == subAfe) {
					return aa;
				}
			}

			return null;
		}

		private function onClickCancel(event:MouseEvent):void 
		{
			var popup:IFlexDisplayObject = event.currentTarget.parentDocument as IFlexDisplayObject;
			PopUpManager.removePopUp(popup);
		}
		
		private function get chiefs():ArrayCollection 
		{
			var result:ArrayCollection = new ArrayCollection();
			
			var assets:ArrayCollection = view.dgAssets.dataProvider as ArrayCollection;
			
			for each (var asset:AssetDataObject in assets) {
				if (asset.AssetId == asset.ChiefAssetId) {
					result.addItem(asset);
				}
			}
			
			return result;
		}
		
		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
		
	}
	
}
