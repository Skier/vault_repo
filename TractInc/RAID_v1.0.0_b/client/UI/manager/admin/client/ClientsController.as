package UI.manager.admin.client
{
	import mx.controls.Alert;
	import App.Domain.Client;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import flash.display.DisplayObject;
	import flash.events.MouseEvent;
	import mx.collections.ArrayCollection;
	import mx.managers.PopUpManager;
	import weborb.data.ActiveCollection;
	import mx.events.CloseEvent;
	import App.Domain.Afe;
	import App.Domain.SubAfe;
	import mx.core.IFlexDisplayObject;
	import App.Domain.AfeStatus;
	import App.Domain.SubAfeStatus;
	import UI.manager.admin.AdminController;
	import App.Domain.Asset;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	import App.Domain.AssetAssignment;
	
	public class ClientsController
	{
		public var view:ClientsView;
		public var model:ClientsModel;
		public var parentController:AdminController;
		
		public function ClientsController(view:ClientsView, parentController:AdminController) 
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new ClientsModel();
			
		}
		
		public function getClientById(id:int):Client 
		{
			var clients:ArrayCollection = view.dgClients.dataProvider as ArrayCollection;
			
			for each (var client:Client in clients) {
				if (client.ClientId == id) {
					return client;
				}
			}

			return null;
		}
		
		public function onClickNewClient():void 
		{
			var client:Client = new Client();
			client.Active = true;
			
			var collection:ActiveCollection = view.dgClients.dataProvider as ActiveCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onClientsCollectionChanged);
			}
			
			openClient(client);
		}
		
		public function onClickEditClient():void 
		{
			var client:Client = view.dgClients.selectedItem as Client;
			if (client == null) {
				return;
			} else {
				openClient(client);
			}
		}
		
		public function onClickDeleteClient():void 
		{
			var client:Client = view.dgClients.selectedItem as Client;
			
			client.canDeleteClient(
				function(can:Boolean):void {
					if (can) {
            			Alert.show("Are you really want to delete selected Client and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	client.deleteClient();
			                    	var clients:ActiveCollection = ActiveCollection(view.dgClients.dataProvider);
			                    	clients.removeItemAt(clients.getItemIndex(client));
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Client because there are not confirmed items", "Delete");
     				}
    			}
    		);
		}
		
		public function onClickNewAfe():void 
		{
			var afe:Afe = new Afe();
			afe.RelatedClient = view.dgClients.selectedItem as Client;
			
			var collection:ActiveCollection = view.dgAfes.dataProvider as ActiveCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAfesCollectionChanged);
			}
			
			openAfe(afe);
		}
		
		public function onClickEditAfe():void 
		{
			var afe:Afe = view.dgAfes.selectedItem as Afe;
			if (afe == null) {
				return;
			} else {
				openAfe(afe);
			}
		}
		
		public function onClickDeleteAfe():void 
		{
			var afe:Afe = view.dgAfes.selectedItem as Afe;
			
			afe.canDeleteAfe(
				function(can:Boolean):void {
					if (can) {
            			Alert.show("Are you really want to delete selected AFE and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	afe.deleteAfe();
			                    	var afes:ActiveCollection = ActiveCollection(view.dgAfes.dataProvider);
			                    	afes.removeItemAt(afes.getItemIndex(afe));
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected AFE because there are not confirmed items", "Delete");
     				}
    			}
    		);
		}
		
		public function onClickNewSubAfe():void 
		{
			var subAfe:SubAfe = new SubAfe();
			subAfe.RelatedAfe = view.dgAfes.selectedItem as Afe;
			
			var collection:ActiveCollection = view.dgSubAfes.dataProvider as ActiveCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onSubAfesCollectionChanged);
			}
			
			openSubAfe(subAfe);
		}
		
		public function onClickEditSubAfe():void 
		{
			var subAfe:SubAfe = view.dgSubAfes.selectedItem as SubAfe;
			if (subAfe == null) {
				return;
			} else {
				openSubAfe(subAfe);
			}
		}
		
		public function getAssetById(id:int):Asset 
		{
			return parentController.getAssetById(id);
		}
		
		public function onClickDeleteSubAfe():void 
		{
			var subAfe:SubAfe = view.dgSubAfes.selectedItem as SubAfe;
			if (subAfe == null) {
				return;
			}
			
			subAfe.canDeleteSubAfe(
				function(can:Boolean):void {
					if (can) {
            			Alert.show("Are you really want to delete selected Project and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	subAfe.deleteSubAfe();
			                    	var subAfes:ActiveCollection = ActiveCollection(view.dgSubAfes.dataProvider);
			                    	subAfes.removeItemAt(subAfes.getItemIndex(subAfe));
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Project because there are not confirmed items", "Delete");
     				}
    			}
    		);
		}
		
		private function openClient(client:Client):void 
		{
			var popup:ClientDetail = ClientDetail.Open(client, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onClientSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onClientSubmit(event:MouseEvent):void 
		{
			var popup:ClientDetail = event.currentTarget.parentDocument as ClientDetail;
			
			var client:Client = popup.client;
		
			client.Active = popup.cbIsActive.selected;
			client.ClientName = popup.txtClientName.text;
			client.ClientAddress = popup.txtClientAddress.text;
			
			client.save(false, new Responder(
				function (client:Client):void {
					var collection:ArrayCollection = view.dgClients.dataProvider as ArrayCollection;
					if (!collection.contains(client)) {
						collection.addItem(client);
					}
					PopUpManager.removePopUp(popup);
				}, onFault));
		}
		
		private function onClickCancel(event:MouseEvent):void 
		{
			var popup:IFlexDisplayObject = event.currentTarget.parentDocument as IFlexDisplayObject;
			PopUpManager.removePopUp(popup);
		}
		
 		private function onClientsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ActiveCollection(view.dgClients.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgClients.selectedIndex = lastGridIndex;
				view.dgClients.scrollToIndex(lastGridIndex);
			}
		}
		
		private function openAfe(afe:Afe):void 
		{
			var popup:AfeDetail = AfeDetail.Open(afe, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onAfeSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onAfeSubmit(event:MouseEvent):void 
		{
			var popup:AfeDetail = event.currentTarget.parentDocument as AfeDetail;
			
			var afe:Afe = popup.afe;
		
			var afeCode:String = popup.txtAfeCode.text;
			var afeName:String = popup.txtAfeName.text;
			var status:AfeStatus = popup.cbAfeStatus.selectedItem as AfeStatus;
			
			var a:Afe = getAfe(afeCode);
			
			if (a != null && a != afe) {
				Alert.show("AFE code already exists.");
				return;
			}
		
			afe.AFE = afeCode;
			afe.AFEName = afeName;
			afe.RelatedAfeStatus = status;
			
			afe.save(false, new Responder(
				function (afe:Afe):void {
					var collection:ArrayCollection = view.dgAfes.dataProvider as ArrayCollection;
					if (!collection.contains(afe)) {
						collection.addItem(afe);
					}
					PopUpManager.removePopUp(popup);
				}, onFault));
		}
		
 		private function onAfesCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ActiveCollection(view.dgAfes.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAfes.selectedIndex = lastGridIndex;
				view.dgAfes.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getAfe(id:String):Afe 
		{
			var collection:ArrayCollection = view.dgAfes.dataProvider as ArrayCollection;
			
			for each (var afe:Afe in collection) {
				if (afe.AFE == id) {
					return afe;
				}
			}
			
			return null;
		}
		
		private function openSubAfe(subAfe:SubAfe):void 
		{
			var popup:SubAfeDetail = SubAfeDetail.Open(subAfe, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onSubAfeSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onSubAfeSubmit(event:MouseEvent):void 
		{
			var popup:SubAfeDetail = event.currentTarget.parentDocument as SubAfeDetail;
			
			var subAfe:SubAfe = popup.subAfe;
			
			var subAfeName:String = popup.txtSubAfeName.text;
			var subAfeShortName:String = popup.txtSubAfeShortName.text;
			var subAfeStatus:SubAfeStatus = popup.cbSubAfeStatus.selectedItem as SubAfeStatus;
			var subAfeTemporary:Boolean = popup.cbTemporary.selected;
			var oldRelatedAfe:Afe = subAfe.RelatedAfe;
			
			var sA:SubAfe = getSubAfe(subAfeName, subAfeShortName);
			
			if (sA != null && sA != subAfe) {
				Alert.show("Project Name and project ShortName must be unique.");
				return;
			}
			
			subAfe.SubAFE = subAfeName;
			subAfe.ShortName = subAfeShortName;
			subAfe.RelatedSubAfeStatus = subAfeStatus;
			subAfe.RelatedAfe = Afe(popup.cbAfe.selectedItem);
			subAfe.Temporary = subAfeTemporary;
			
			if (subAfe.RelatedAfe != oldRelatedAfe) {
				for each (var assignment:AssetAssignment in subAfe.RelatedAssetAssignment) {
					assignment.RelatedAfe = subAfe.RelatedAfe;
				}
			}
			
			subAfe.save(
				true,
				new Responder(
					function (subAfe:SubAfe):void {
						var collection:ArrayCollection = view.dgSubAfes.dataProvider as ArrayCollection;
						if (!collection.contains(subAfe)) {
							collection.addItem(subAfe);
						} else if ((subAfe.RelatedAfe != oldRelatedAfe)
								&& (collection.contains(subAfe))) {
							collection.removeItemAt(collection.getItemIndex(subAfe));
						}
						PopUpManager.removePopUp(popup);
					},
					onFault
				)
			);
		}
		
 		private function onSubAfesCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ActiveCollection(view.dgSubAfes.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgSubAfes.selectedIndex = lastGridIndex;
				view.dgSubAfes.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getSubAfe(subAfeName:String, subAfeShortName:String):SubAfe 
		{
			var collection:ArrayCollection = view.dgSubAfes.dataProvider as ArrayCollection;
			
			for each (var sA:SubAfe in collection) {
				if (sA.SubAFE == subAfeName || sA.ShortName == subAfeShortName) {
					return sA;
				}
			}
			
			return null;
		}
		
		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
		
	}
}