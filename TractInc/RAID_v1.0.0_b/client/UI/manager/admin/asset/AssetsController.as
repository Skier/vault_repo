package UI.manager.admin.asset
{
	import UI.manager.admin.AdminController;
	import App.Domain.ActiveRecords;
	import App.Domain.Asset;
	import weborb.data.DynamicLoadEvent;
	import mx.collections.ArrayCollection;
	import App.Domain.SubAfe;
	import mx.events.CollectionEvent;
	import App.Domain.AssetAssignment;
	import App.Domain.Afe;
	import mx.events.CloseEvent;
	import flash.events.MouseEvent;
	import mx.controls.Alert;
	import flash.display.DisplayObject;
	import mx.managers.PopUpManager;
	import mx.rpc.Responder;
	import mx.core.IFlexDisplayObject;
	import App.Domain.AssetType;
	import mx.rpc.events.FaultEvent;
	import App.Domain.RateByAssignment;
	import App.Domain.BillItemType;
	import App.Domain.Client;
	import weborb.data.ActiveCollection;
	import mx.events.CollectionEventKind;
	import App.Domain.User;
	import App.Domain.Bill;
	import App.Domain.BillStatus;
	import App.Domain.UserAsset;
	import App.Domain.UserRole;
	
	public class AssetsController
	{
		public var view:AssetsView;
		public var model:AssetsModel;

		public var parentController:AdminController;
		
		public function AssetsController(view:AssetsView, parentController:AdminController)
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new AssetsModel();
		}
		
		public function getAssetById(id:int):Asset 
		{
			var assets:ArrayCollection = view.dgAssets.dataProvider as ArrayCollection;
			
			for each (var asset:Asset in assets) {
				if (asset.AssetId == id) {
					return asset;
				}
			}

			return null;
		}
		
		public function onClickNewAsset():void 
		{
			var asset:Asset = new Asset();

			var collection:ActiveCollection = view.dgAssets.dataProvider as ActiveCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAssetsCollectionChanged);
			}
			
			openAsset(asset);
		}
		
		public function onClickEditAsset():void 
		{
			var asset:Asset = view.dgAssets.selectedItem as Asset;
			if (asset == null) {
				return;
			} else {
				openAsset(asset);
			}
		}
		
		public function onClickDeleteAsset():void 
		{
			var asset:Asset = view.dgAssets.selectedItem as Asset;
			
			asset.canDeleteAsset(
				function(can:Boolean):void {
					if (can) {
            			Alert.show("Are you really want to delete selected Asset and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	asset.deleteAsset();
			                    	var assets:ActiveCollection = ActiveCollection(view.dgAssets.dataProvider);
			                    	assets.removeItemAt(assets.getItemIndex(asset));
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Asset because there are not confirmed items", "Delete");
     				}
    			}
    		);
		}
		
		public function onClickNewAssetAssignment():void 
		{
			var asset:Asset = view.dgAssets.selectedItem as Asset;
			if (asset == null) {
				return;
			} else {
				var assetAssignment:AssetAssignment = new AssetAssignment();
				assetAssignment.RelatedAsset = asset;

				var collection:ActiveCollection = view.dgAssetAssignments.dataProvider as ActiveCollection;
				if (collection) {
					collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAssetAssignmentsCollectionChanged);
				}
				
				openAssetAssignment(assetAssignment);
			}
		}
		
		public function onClickEditAssetAssignment():void 
		{
			var assetAssignment:AssetAssignment = view.dgAssetAssignments.selectedItem as AssetAssignment;
			if (assetAssignment == null) {
				return;
			} else {
				openAssetAssignment(assetAssignment);
			}
		}
		
		public function onClickDeleteAssetAssignment():void 
		{
			var assetAssignment:AssetAssignment = view.dgAssetAssignments.selectedItem as AssetAssignment;
			
			assetAssignment.canDeleteAssignment(
				function(can:Boolean):void {
					if (can) {
            			Alert.show("Are you really want to delete selected Asset Assignment and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	assetAssignment.deleteAssignment();
			                    	var assignments:ActiveCollection = ActiveCollection(view.dgAssetAssignments.dataProvider);
			                    	assignments.removeItemAt(assignments.getItemIndex(assetAssignment));
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Asset Assignment because there are not confirmed items", "Delete");
     				}
    			}
    		);
		}
		
		public function onClickEditRate():void 
		{
			var rate:RateByAssignment = view.dgRates.selectedItem as RateByAssignment;
			if (rate == null) {
				return;
			} else {
				openRate(rate);
			}
		}
		
		public function onClickNewNotExceedRate():void 
		{
			var assetAssignment:AssetAssignment = view.dgAssetAssignments.selectedItem as AssetAssignment;
			
			if (assetAssignment == null) {
				return;
			} else {
				var rate:RateByAssignment = new RateByAssignment();
				rate.RelatedAssetAssignment = assetAssignment;
	
				openNotExceedRate(rate);
			}
		}
		
		public function onClickDeleteNotExceedRate():void 
		{
			var rate:RateByAssignment = view.dgNotExceedRates.selectedItem as RateByAssignment;
			
            Alert.show("Are you really want to remove selected Not Exceed Rate ?", "Delete", 
                Alert.YES | Alert.NO , null, 
                function (event:CloseEvent):void {
                    if (event.detail == Alert.YES) {
                    	rate.remove();
                    }
                }, null, Alert.NO);
		}
		
		public function onClickEditNotExceedRate():void 
		{
			var rate:RateByAssignment = view.dgNotExceedRates.selectedItem as RateByAssignment;
			if (rate == null) {
				return;
			} else {
				openNotExceedRate(rate);
			}
		}
		
		public function getClientById(id:int):Client 
		{
			return parentController.getClientById(id);
		}
		
		private function openAsset(asset:Asset):void 
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
			
			if (AssetType(popup.cbAssetType.selectedItem)._Type == AssetType.ASSET_TYPE_LANDMAN && popup.txtUserPassword.text != popup.txtUserConfirmPswd.text) {
				Alert.show("Passwords are not equal");
				return;
			}
			
			var asset:Asset = popup.asset;
		
			asset.RelatedAssetType = popup.cbAssetType.selectedItem as AssetType;
			if (popup.cbIsCrewChief.selected) {
				asset.ChiefAssetId = 0;
			} else {
			asset.ChiefAssetId = Asset(popup.cbCrewChief.selectedItem).AssetId;
			}
			asset.BusinessName = popup.txtBusinessName.text != null ? popup.txtBusinessName.text : "";
			asset.FirstName = popup.txtFirstName.text != null ? popup.txtFirstName.text : "";
			asset.MiddleName = popup.txtMiddleName.text != null ? popup.txtMiddleName.text : "";
			asset.LastName = popup.txtLastName.text != null ? popup.txtLastName.text : "";
			asset.SSN = popup.txtSSN.text != null ? popup.txtSSN.text : "";
			
			var isNewAsset:Boolean = false;
			if (asset.AssetId == 0) {
				isNewAsset = true;
			}
			
			asset.save(false, new Responder(
				function (asset:Asset):void 
				{
					var collection:ArrayCollection = view.dgAssets.dataProvider as ArrayCollection;
					if (!collection.contains(asset)) {
						collection.addItem(asset);
					}

					if (asset.ChiefAssetId == 0) {
						asset.ChiefAssetId = asset.AssetId;
					}

					if (asset.RelatedAssetType._Type == AssetType.ASSET_TYPE_LANDMAN && isNewAsset) {
						createBills(asset);

						var user:User = new User();
						user.Login = popup.txtUserLogin.text;
						user.Password = popup.txtUserPassword.text;
						user.Email = popup.txtUserEmail.text;
						user.HackingAttempts = 0;
						user.IsActive = true;
						
						var userAsset:UserAsset = new UserAsset();
						userAsset.RelatedAsset = asset;
						userAsset.RelatedUser = user;
						
						var userRole:UserRole = new UserRole();
						userRole.RoleId = 2;
						userRole.RelatedUser = user;
						
						user.RelatedUserAsset.addItem(userAsset);
						user.RelatedUserRole.addItem(userRole);
						
						user.save();
					}
					
					asset.save();
					
					PopUpManager.removePopUp(popup);

				}, onFault));
			
		}
		
		private function createBills(asset:Asset):void 
		{
			for (var i:int = 1; i < 13; i++) {
				var startDate:String = "";
				startDate = (i < 10) ? ("0" + i.toString()) : i.toString();

				var bill1:Bill = new Bill();
				bill1.RelatedAsset = asset;
				bill1.Status = BillStatus.BILL_STATUS_NEW;
				bill1.StartDate = startDate + "/01/2007";
				bill1.Notes = "";

				asset.RelatedBill.addItem(bill1);

				var bill2:Bill = new Bill();
				bill2.RelatedAsset = asset;
				bill2.Status = BillStatus.BILL_STATUS_NEW;
				bill2.StartDate = startDate + "/16/2007";
				bill2.Notes = "";

				asset.RelatedBill.addItem(bill2);
			}
		}
		
 		private function onAssetsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastAddedIndex:int = ActiveCollection(view.dgAssets.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAssets.selectedIndex = lastAddedIndex;
				view.dgAssets.scrollToIndex(lastAddedIndex);
			}
		}
		
		private function openAssetAssignment(assetAssignment:AssetAssignment):void 
		{
			var popup:AssetAssignmentDetail = AssetAssignmentDetail.Open(assetAssignment, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onAssetAssignmentSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onAssetAssignmentSubmit(event:MouseEvent):void 
		{
			var popup:AssetAssignmentDetail = event.currentTarget.parentDocument as AssetAssignmentDetail;
			
			var assetAssignment:AssetAssignment = popup.assetAssignment;
		
			var afe:Afe = popup.cbAfes.selectedItem as Afe;
			var subAfe:SubAfe = popup.cbSubAfes.selectedItem as SubAfe;

			var aa:AssetAssignment = getAssetAssignment(afe.AFE, subAfe.SubAFE);
			
			if (aa != null && aa.AssetAssignmentId != assetAssignment.AssetAssignmentId) {
				Alert.show("Asset assignment already exists");
				return;
			}
			
			assetAssignment.RelatedAfe = afe;
			assetAssignment.RelatedSubAfe = subAfe;
			
			assetAssignment.save(false, new Responder(
				function (assetAssignment:AssetAssignment):void {
					var collection:ArrayCollection = view.dgAssetAssignments.dataProvider as ArrayCollection;
					if (!collection.contains(assetAssignment)) {
						collection.addItem(assetAssignment);
					}
					
					for each (var rate:RateByAssignment in popup.rates) {
						rate.save(false, new Responder(
							function (rate:RateByAssignment):void {
								var collection:ArrayCollection = view.dgRates.dataProvider as ArrayCollection;
								if (!collection.contains(rate)) {
									collection.addItem(rate);
								}
							}, onFault));
					}
					
					PopUpManager.removePopUp(popup);
				}, onFault));
		}
		
 		private function onAssetAssignmentsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ActiveCollection(view.dgAssetAssignments.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAssetAssignments.selectedIndex = lastGridIndex;
				view.dgAssetAssignments.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getAssetAssignment(afe:String, subAfe:String):AssetAssignment
		{
			var collection:ArrayCollection = view.dgAssetAssignments.dataProvider as ArrayCollection;

			for each (var aa:AssetAssignment in collection) {
				if (aa.AFE == afe && aa.SubAFE == subAfe) {
					return aa;
				}
			}

			return null;
		}
		
		private function openRate(rate:RateByAssignment):void 
		{
			var collection:ArrayCollection = view.dgRates.dataProvider as ArrayCollection;
			var popup:RateDetail = RateDetail.Open(rate, collection, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onRateSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function openNotExceedRate(rate:RateByAssignment):void 
		{
			var collection:ArrayCollection = view.dgNotExceedRates.dataProvider as ArrayCollection;
			var popup:NotExceedDetail = NotExceedDetail.Open(rate, collection, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onNotExceedSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onRateSubmit(event:MouseEvent):void 
		{
			var popup:RateDetail = event.currentTarget.parentDocument as RateDetail;
			
			var rate:RateByAssignment = popup.rate;
		
			var billItemType:BillItemType = popup.cbBillItemType.selectedItem as BillItemType;
			var billRate:Number = Number(popup.txtBillRate.text);
			var invoiceRate:Number = Number(popup.txtInvoiceRate.text);
			
			var r:RateByAssignment = getRate(billItemType.BillItemTypeId);
			
			if (r != null && r.RateByAssignmentId != rate.RateByAssignmentId) {
				Alert.show("Rate for current type already exists");
				return;
			}
			
			rate.RelatedBillItemType = billItemType;
			rate.BillRate = billRate;
			rate.InvoiceRate = invoiceRate;
			
			rate.save(false, new Responder(
				function (rate:RateByAssignment):void {
					var collection:ArrayCollection = view.dgRates.dataProvider as ArrayCollection;
					if (!collection.contains(rate)) {
						collection.addItem(rate);
					}
					PopUpManager.removePopUp(popup);
				}, onFault));
		}
		
		private function onNotExceedSubmit(event:MouseEvent):void 
		{
			var popup:NotExceedDetail = event.currentTarget.parentDocument as NotExceedDetail;
			
			var rate:RateByAssignment = popup.rate;
		
			var billItemType:BillItemType = popup.cbBillItemType.selectedItem as BillItemType;
			var billRate:Number = Number(popup.txtBillRate.text);
			var invoiceRate:Number = billRate;
			
			var r:RateByAssignment = getRate(billItemType.BillItemTypeId);
			
			if (r != null && r.RateByAssignmentId != rate.RateByAssignmentId) {
				Alert.show("Rate for current type already exists");
				return;
			}
			
			rate.RelatedBillItemType = billItemType;
			rate.BillRate = billRate;
			rate.InvoiceRate = invoiceRate;
			rate.ShouldNotExceedRate = true;
			
			rate.save(false, new Responder(
				function (rate:RateByAssignment):void {
					var collection:ArrayCollection = view.dgNotExceedRates.dataProvider as ArrayCollection;
					if (!collection.contains(rate)) {
						collection.addItem(rate);
					}
					PopUpManager.removePopUp(popup);
				}, onFault));
		}
		
		private function getRate(billItemTypeId:int):RateByAssignment 
		{
			var collection:ArrayCollection = view.dgRates.dataProvider as ArrayCollection;

			for each (var r:RateByAssignment in collection) {
				if (r.BillItemTypeId == billItemTypeId) {
					return r;
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
			
			for each (var asset:Asset in assets) {
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
