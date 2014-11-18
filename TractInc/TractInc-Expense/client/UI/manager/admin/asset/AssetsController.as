package UI.manager.admin.asset
{
	
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	import mx.events.CloseEvent;
	import mx.events.CollectionEventKind;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.managers.PopUpManager;
	import mx.core.IFlexDisplayObject;
	import flash.events.MouseEvent;
	import flash.display.DisplayObject;
	import common.TypesRegistry;
	import UI.manager.admin.AdminController;
	import App.Entity.AssetDataObject;
	import App.Entity.ClientDataObject;
	import App.Entity.AssetTypeDataObject;
	import App.Entity.BillItemTypeDataObject;
	import App.Entity.DefaultBillRateDataObject;
	import App.Entity.BillDataObject;
	import App.Entity.UserDataObject;
	import App.Entity.UserAssetDataObject;
	import App.Entity.UserRoleDataObject;
	import App.Domain.AssetTypeDataMapper;
	import App.Entity.BillStatusDataObject;
	import App.Service.ManagerService;
	import mx.rpc.events.ResultEvent;
	import mx.collections.ListCollectionView;
	
	public class AssetsController
	{
		
		public var view:AssetsView;
		
		[Bindable]
		public var model:AssetsModel;

		[Bindable]
		public var parentController:AdminController;
		
		public function AssetsController(view:AssetsView, parentController:AdminController)
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new AssetsModel();
		}
		
		public function open():void {
			model.assets = new ArrayCollection();
			model.assets.filterFunction = function(obj:Object):Boolean {
				var asset:AssetDataObject = AssetDataObject(obj);
				if (AssetDataObject(obj).Deleted) {
					return false;
				}
				return (null == view.cbCrewFilter.selectedItem)
					|| (AssetDataObject(view.cbCrewFilter.selectedItem).AssetId == asset.ChiefAssetId);
			};
			model.assets.refresh();
			
			for each (var asset:AssetDataObject in parentController.model.data.Assets) {
				model.assets.addItem(asset);
			}
			
			model.crews = new ListCollectionView(model.assets);
			model.crews.filterFunction = function(obj:Object):Boolean {
				return AssetDataObject(obj).ChiefAssetId == AssetDataObject(obj).AssetId;
			}
			model.crews.refresh();
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
		
		public function onClickNewAsset():void 
		{
			var asset:AssetDataObject = new AssetDataObject();
			
			asset.DefaultRates = new Array();
			for each (var itemType:BillItemTypeDataObject in TypesRegistry.instance.countableBillItemTypes) {
				var rate:DefaultBillRateDataObject = new DefaultBillRateDataObject();
				rate.BillItemTypeId = itemType.BillItemTypeId;
				rate.BillRate = 0;
				
				asset.DefaultRates.push(rate);
			}
			
			asset.UserInfo = new UserDataObject();

			var collection:ArrayCollection = view.dgAssets.dataProvider as ArrayCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAssetsCollectionChanged);
			}
			
			openAsset(asset);
		}
		
		public function onClickEditAsset():void 
		{
			var asset:AssetDataObject = view.dgAssets.selectedItem as AssetDataObject;
			if (asset == null) {
				return;
			} else {
				openAsset(asset);
			}
		}
		
		public function onClickDeleteAsset():void 
		{
			var asset:AssetDataObject = view.dgAssets.selectedItem as AssetDataObject;
			
			ManagerService.getInstance().canRemoveAsset(asset.AssetId, new Responder(
				function(result:ResultEvent):void {
					if (Boolean(result.result)) {
            			Alert.show("Are you really want to delete selected Asset and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	ManagerService.getInstance().removeAsset(asset.AssetId);
			                    	asset.Deleted = true;
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Asset because there are not confirmed items", "Delete");
     				}
    			},
    			function(fault:FaultEvent):void {
    				Alert.show("Please contact administrator", "System Error");
    			}
    		));
		}
		
		public function getClientById(id:int):ClientDataObject
		{
			return parentController.getClientById(id);
		}
		
		private function openAsset(asset:AssetDataObject):void 
		{
			var popup:AssetDetail = AssetDetail.Open(asset, chiefs, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onAssetSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onAssetSubmit(event:MouseEvent):void 
		{
			var popup:AssetDetail = event.currentTarget.parentDocument as AssetDetail;
			
			if (!popup.isValid()) {
				return;
			}
			
			if (popup.txtUserPassword.text != popup.txtUserConfirmPswd.text) {
				Alert.show("Passwords are not equal");
				return;
			}
			
			popup.enabled = false;
			
			var asset:AssetDataObject = popup.asset;
		
			asset.Type = "LANDMAN";
			if (popup.cbIsCrewChief.selected) {
				asset.ChiefAssetId = asset.ChiefAssetId;
			} else {
				asset.ChiefAssetId = AssetDataObject(popup.cbCrewChief.selectedItem).AssetId;
			}
			asset.BusinessName = popup.txtBusinessName.text != null ? popup.txtBusinessName.text : "";
			asset.FirstName = popup.txtFirstName.text != null ? popup.txtFirstName.text : "";
			asset.MiddleName = popup.txtMiddleName.text != null ? popup.txtMiddleName.text : "";
			asset.LastName = popup.txtLastName.text != null ? popup.txtLastName.text : "";
			asset.SSN = popup.txtSSN.text != null ? popup.txtSSN.text : "";
			
			if (null == asset.UserInfo) {
				asset.UserInfo = new UserDataObject();
			}
			
			if (0 == asset.UserInfo.UserId) {
				asset.UserInfo.HackingAttempts = 0;
				asset.UserInfo.IsActive = true;
			}
			
			asset.UserInfo.Login = popup.txtUserLogin.text;
			asset.UserInfo.Password = popup.txtUserPassword.text;
			asset.UserInfo.Email = popup.txtUserEmail.text;
			
			ManagerService.getInstance().storeAsset(asset, new Responder(
				function(result:ResultEvent):void
				{
					if (-1 == parentController.model.data.Assets.indexOf(asset)) {
						parentController.model.data.Assets.push(asset);
					}
					
					var collection:ArrayCollection = view.dgAssets.dataProvider as ArrayCollection;
					if (!collection.contains(asset)) {
						collection.addItem(asset);
					}

					popup.enabled = true;
					PopUpManager.removePopUp(popup);

				},
				function(evt:FaultEvent):void {
					onFault(evt);
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
