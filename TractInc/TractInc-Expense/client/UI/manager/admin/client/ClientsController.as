package UI.manager.admin.client
{
	
	import mx.controls.Alert;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.collections.ArrayCollection;
	import mx.managers.PopUpManager;
	import mx.events.CloseEvent;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	import mx.core.IFlexDisplayObject;
	import flash.display.DisplayObject;
	import flash.events.MouseEvent;
	import common.TypesRegistry;
	import UI.manager.admin.AdminController;
	import App.Entity.ClientDataObject;
	import App.Entity.AFEDataObject;
	import App.Entity.ProjectDataObject;
	import App.Entity.AssetDataObject;
	import App.Entity.InvoiceItemTypeDataObject;
	import App.Entity.DefaultInvoiceRateDataObject;
	import App.Entity.AFEStatusDataObject;
	import App.Entity.AssetAssignmentDataObject;
	import App.Service.ManagerService;
	import mx.rpc.events.ResultEvent;
	import App.Entity.ProjectStatusDataObject;
	import mx.collections.ListCollectionView;
	import mx.collections.Sort;
	import mx.collections.SortField;
	
	public class ClientsController
	{
		public var view:ClientsView;
		
		[Bindable]
		public var model:ClientsModel;
		
		[Bindable]
		public var parentController:AdminController;
		
		public function ClientsController(view:ClientsView, parentController:AdminController) 
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new ClientsModel();
		}
		
		public function open():void {
			view.dgAssignments.dataProvider = null;
			view.dgSubAfes.dataProvider = null;
			view.dgAfes.dataProvider = null;
			
			model.clients = new ArrayCollection();
			model.clients.filterFunction = function(obj:Object):Boolean {
				var client:ClientDataObject = ClientDataObject(obj);
				if (client.Deleted) {
					return false;
				}
				return (2 == view.cbClientsFilter.selectedIndex)
					|| (1 == view.cbClientsFilter.selectedIndex) && !client.Active
					|| (0 == view.cbClientsFilter.selectedIndex) && client.Active;
			};
			model.clients.refresh();
			
			for each (var client:ClientDataObject in parentController.model.data.Clients) {
				model.clients.addItem(client);
			}
			
			model.afesByClient = new Array();
			for each (var afe:AFEDataObject in parentController.parentController.appController.Model.afesHash) {
				var afes:ArrayCollection = ArrayCollection(model.afesByClient[afe.ClientId]);
				if (null == afes) {
					afes = new ArrayCollection();
					afes.filterFunction = function(obj:Object):Boolean {
						var afe:AFEDataObject = AFEDataObject(obj);
						if(afe.Deleted) {
							return false;
						}
						return (4 == view.cbAfeFilter.selectedIndex)
							|| (3 == view.cbAfeFilter.selectedIndex) && ("UNLOCKED" == afe.AFEStatus)
							|| (2 == view.cbAfeFilter.selectedIndex) && ("LOCKED" == afe.AFEStatus)
							|| (1 == view.cbAfeFilter.selectedIndex) && ("EXPIRED" == afe.AFEStatus)
							|| (0 == view.cbAfeFilter.selectedIndex) && ("ISSUED" == afe.AFEStatus);
					};
					afes.sort = new Sort();
					afes.sort.fields = [new SortField("AFE", true)];
					afes.refresh();
					
					model.afesByClient[afe.ClientId] = afes;
				}
				afes.addItem(afe);
			}
			
			model.projectsByAfe = new Array();
			for each (var project:ProjectDataObject in parentController.parentController.appController.Model.projectsHash) {
				var projects:ArrayCollection = ArrayCollection(model.projectsByAfe[project.AFE]);
				if (null == projects) {
					projects = new ArrayCollection();
					projects.filterFunction = function(obj:Object):Boolean {
						var project:ProjectDataObject = ProjectDataObject(obj);
						if(project.Deleted) {
							return false;
						}
						return (4 == view.cbProjectFilter.selectedIndex)
							|| (3 == view.cbProjectFilter.selectedIndex) && ("UNLOCKED" == project.SubAFEStatus)
							|| (2 == view.cbProjectFilter.selectedIndex) && ("LOCKED" == project.SubAFEStatus)
							|| (1 == view.cbProjectFilter.selectedIndex) && ("EXPIRED" == project.SubAFEStatus)
							|| (0 == view.cbProjectFilter.selectedIndex) && ("ISSUED" == project.SubAFEStatus);
					}
					projects.sort = new Sort();
					projects.sort.fields = [new SortField("SubAFE", true)];
					projects.refresh();
					
					model.projectsByAfe[project.AFE] = projects;
				}
				projects.addItem(project);
			}
			
			model.assetsByProject = new Array();
			model.assignmentsByProject = new Array();
			for each (var assignment:AssetAssignmentDataObject in parentController.model.data.Assignments) {
				if (assignment.Deleted) {
					continue;
				}
				
				var assets:ArrayCollection = ArrayCollection(model.assetsByProject[assignment.SubAFE]);
				if (null == assets) {
					assets = new ArrayCollection();
					assets.filterFunction = function(obj:Object):Boolean {
						return !AssetDataObject(obj).Deleted;
					}
					assets.refresh();
					
					model.assetsByProject[assignment.SubAFE] = assets;
				}
				
				var asset:AssetDataObject = AssetDataObject(parentController.parentController.model.assetsHash[assignment.AssetId]);
				
				if (null == asset) {
					continue;
				}
				
				if (!assets.contains(asset)) {
					assets.addItem(asset);
				}
				
				var assignments:ArrayCollection = ArrayCollection(model.assignmentsByProject[assignment.SubAFE]);
				if (null == assignments) {
					assignments = new ArrayCollection();
					assignments.filterFunction = function(obj:Object):Boolean {
						return !AssetAssignmentDataObject(obj).Deleted;
					}
					assignments.refresh();
					
					model.assignmentsByProject[assignment.SubAFE] = assignments;
				}
				
				assignments.addItem(assignment);
			}
		}
		
		public function getClientById(id:int):ClientDataObject
		{
			var clients:ArrayCollection = view.dgClients.dataProvider as ArrayCollection;
			
			for each (var client:ClientDataObject in clients) {
				if (client.ClientId == id) {
					return client;
				}
			}

			return null;
		}
		
		public function onClickNewClient():void 
		{
			var client:ClientDataObject = new ClientDataObject();
			client.Active = true;
			
			client.DefaultRates = new Array();
			for each (var itemType:InvoiceItemTypeDataObject in TypesRegistry.instance.countableInvoiceItemTypes) {
				var rate:DefaultInvoiceRateDataObject = new DefaultInvoiceRateDataObject();
				rate.InvoiceItemTypeId = itemType.InvoiceItemTypeId;
				rate.InvoiceRate = 0;
				
				client.DefaultRates.push(rate);
			}

			var collection:ArrayCollection = view.dgClients.dataProvider as ArrayCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onClientsCollectionChanged);
			}
			
			openClient(client);
		}
		
		public function onClickEditClient():void 
		{
			var client:ClientDataObject = view.dgClients.selectedItem as ClientDataObject;
			if (client == null) {
				return;
			} else {
				openClient(client);
			}
		}
		
		public function onClickDeleteClient():void 
		{
			var client:ClientDataObject = view.dgClients.selectedItem as ClientDataObject;
			
			ManagerService.getInstance().canRemoveClient(client.ClientId, new Responder(
				function(result:ResultEvent):void {
					if (Boolean(result.result)) {
            			Alert.show("Are you really want to delete selected Client and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
		                    		ManagerService.getInstance().removeClient(client.ClientId);
			                    	client.Deleted = true;
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Client because there are not confirmed items", "Delete");
     				}
    			},
    			function(fault:FaultEvent):void {
    				Alert.show("Please contact administrator", "System Error");
    			}
    		));
		}
		
		public function onClickNewAfe():void 
		{
			var afe:AFEDataObject = new AFEDataObject();
			afe.ClientId = (view.dgClients.selectedItem as ClientDataObject).ClientId;
			afe.IsNew = true;
			afe.AFEStatus = "ISSUED";
			
			var collection:ArrayCollection = view.dgAfes.dataProvider as ArrayCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAfesCollectionChanged);
			}
			
			openAfe(afe);
		}
		
		public function onClickEditAfe():void 
		{
			var afe:AFEDataObject = view.dgAfes.selectedItem as AFEDataObject;
			if (afe == null) {
				return;
			} else {
				openAfe(afe);
			}
		}
		
		public function onClickDeleteAfe():void 
		{
			var afe:AFEDataObject = view.dgAfes.selectedItem as AFEDataObject;
			
			ManagerService.getInstance().canRemoveAfe(afe.AFE, new Responder(
				function(result:ResultEvent):void {
					if (Boolean(result.result)) {
            			Alert.show("Are you really want to delete selected AFE and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	ManagerService.getInstance().removeAfe(afe.AFE);
			                    	afe.Deleted = true;
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected AFE because there are not confirmed items", "Delete");
     				}
    			},
    			function(fault:FaultEvent):void {
    				Alert.show("Please contact administrator", "System Error");
    			}
    		));
		}
		
		public function onClickNewSubAfe():void 
		{
			var subAfe:ProjectDataObject = new ProjectDataObject();
			subAfe.AFE = (view.dgAfes.selectedItem as AFEDataObject).AFE;
			subAfe.IsNew = true;
			subAfe.SubAFEStatus = "ISSUED";
			
			var collection:ArrayCollection = view.dgSubAfes.dataProvider as ArrayCollection;
			if (collection) {
				collection.addEventListener(CollectionEvent.COLLECTION_CHANGE, onSubAfesCollectionChanged);
			}
			
			openSubAfe(subAfe);
		}
		
		public function onClickEditSubAfe():void 
		{
			var subAfe:ProjectDataObject = view.dgSubAfes.selectedItem as ProjectDataObject;
			if (subAfe == null) {
				return;
			} else {
				openSubAfe(subAfe);
			}
		}
		
		public function getAssetById(id:int):AssetDataObject
		{
			return parentController.getAssetById(id);
		}
		
		public function onClickDeleteSubAfe():void 
		{
			var subAfe:ProjectDataObject = view.dgSubAfes.selectedItem as ProjectDataObject;
			if (subAfe == null) {
				return;
			}
			
			ManagerService.getInstance().canRemoveProject(subAfe.SubAFE, new Responder(
				function(result:ResultEvent):void {
					if (Boolean(result.result)) {
            			Alert.show("Are you really want to delete selected Project and all its dependences?", "Delete",
			                Alert.YES | Alert.NO , null, 
            			    function (event:CloseEvent):void {
			                    if (event.detail == Alert.YES) {
			                    	ManagerService.getInstance().removeProject(subAfe.SubAFE);
			                    	subAfe.Deleted = true;
            			        }
            			    }, null, Alert.NO
            			);
     				} else {
     					Alert.show("Cannot delete selected Project because there are not confirmed items", "Delete");
     				}
    			},
    			function(fault:FaultEvent):void {
    				Alert.show("Please contact administrator", "System Error");
    			}
    		));
		}
		
		private function openClient(client:ClientDataObject):void 
		{
			var popup:ClientDetail = ClientDetail.Open(client, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onClientSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onClientSubmit(event:MouseEvent):void 
		{
			var popup:ClientDetail = event.currentTarget.parentDocument as ClientDetail;
			
			var client:ClientDataObject = popup.client;
		
			client.Active = popup.cbIsActive.selected;
			client.ClientName = popup.txtClientName.text;
			client.ClientAddress = popup.txtClientAddress.text;
			
			popup.enabled = false;
			
			ManagerService.getInstance().storeClient(client, new Responder(
				function(result:ResultEvent):void {
					var collection:Array = parentController.model.data.Clients;
					
					if (-1 == collection.indexOf(client)) {
						collection.push(client);
					}
					PopUpManager.removePopUp(popup);
					
					if (-1 == ArrayCollection(view.dgClients.dataProvider).list.getItemIndex(client)) {
						ArrayCollection(view.dgClients.dataProvider).addItem(client);
						
						model.afesByClient[client.ClientId] = new ArrayCollection();
					}
					
					view.dgClients.selectedItem = client;
					view.onClientChange();
				},
				function(evt:FaultEvent):void {
					popup.enabled = true;
					onFault(evt);
				})
			);
		}
		
		private function onClickCancel(event:MouseEvent):void 
		{
			var popup:IFlexDisplayObject = event.currentTarget.parentDocument as IFlexDisplayObject;
			PopUpManager.removePopUp(popup);
		}
		
 		private function onClientsCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ArrayCollection(view.dgClients.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgClients.selectedIndex = lastGridIndex;
				view.dgClients.scrollToIndex(lastGridIndex);
			}
		}
		
		private function openAfe(afe:AFEDataObject):void 
		{
			var popup:AfeDetail = AfeDetail.Open(afe, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onAfeSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onAfeSubmit(event:MouseEvent):void 
		{
			var popup:AfeDetail = event.currentTarget.parentDocument as AfeDetail;
			
			var afe:AFEDataObject = popup.afe;
		
			var afeCode:String = popup.txtAfeCode.text;
			var afeName:String = popup.txtAfeName.text;
			var status:String = AFEStatusDataObject(popup.cbAfeStatus.selectedItem as AFEStatusDataObject).Status;
			
			var a:AFEDataObject = getAfe(afeCode);
			
			if (a != null && a != afe) {
				Alert.show("AFE code already exists.");
				return;
			}
		
			afe.AFE = afeCode;
			afe.AFEName = afeName;
			afe.AFEStatus = status;
			
			popup.enabled = false;
			ManagerService.getInstance().storeAFE(afe, new Responder(
				function(result:ResultEvent):void {
					afe.IsNew = false;
					
					var collection:Array = parentController.parentController.appController.Model.afesHash;
					
					if (null == collection[afe.AFE]) {
						collection[afe.AFE] = afe;
					}
					
					if (-1 == ArrayCollection(view.dgAfes.dataProvider).list.getItemIndex(afe)) {
						ArrayCollection(view.dgAfes.dataProvider).addItem(afe);
						
						model.projectsByAfe[afe.AFE] = new ArrayCollection();
					}
					
					PopUpManager.removePopUp(popup);
					
					view.onAfeChange(afe);
				},
				function(evt:FaultEvent):void {
					popup.enabled = true;
					onFault(evt);
				})
			);
		}
		
 		private function onAfesCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ArrayCollection(view.dgAfes.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgAfes.selectedIndex = lastGridIndex;
				view.dgAfes.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getAfe(id:String):AFEDataObject
		{
			var collection:ArrayCollection = view.dgAfes.dataProvider as ArrayCollection;
			
			for each (var afe:AFEDataObject in collection) {
				if (afe.AFE == id) {
					return afe;
				}
			}
			
			return null;
		}
		
		private function openSubAfe(subAfe:ProjectDataObject):void 
		{
			var popup:SubAfeDetail = SubAfeDetail.Open(subAfe, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onSubAfeSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
			popup.afes = model.afesByClient[ClientDataObject(view.dgClients.selectedItem).ClientId];
		}
		
		private function onSubAfeSubmit(event:MouseEvent):void 
		{
			var popup:SubAfeDetail = event.currentTarget.parentDocument as SubAfeDetail;
			
			var subAfe:ProjectDataObject = popup.subAfe;
			
			var subAfeName:String = popup.txtSubAfeName.text;
			var subAfeShortName:String = popup.txtSubAfeShortName.text;
			var subAfeStatus:String = (popup.cbSubAfeStatus.selectedItem as ProjectStatusDataObject).Status;
			var subAfeTemporary:Boolean = popup.cbTemporary.selected;
			var oldRelatedAfe:String = subAfe.AFE;
			
			var sA:ProjectDataObject = getSubAfe(subAfeName, subAfeShortName);
			
			if (sA != null && sA != subAfe) {
				Alert.show("Project Name and project Short Name must be unique.");
				return;
			}
			
			subAfe.SubAFE = subAfeName;
			subAfe.ShortName = subAfeShortName;
			subAfe.SubAFEStatus = subAfeStatus;
			
			if (null != popup.cbAfe.selectedItem) {
				subAfe.AFE = AFEDataObject(popup.cbAfe.selectedItem).AFE;
			}
			
			subAfe.Temporary = subAfeTemporary;
			
			if (subAfe.AFE != oldRelatedAfe) {
				subAfe.Assignments = new Array();
				
				for each (var assignment:AssetAssignmentDataObject in model.assignmentsByProject[subAfe.SubAFE]) {
					assignment.AFE = subAfe.AFE;
					// assignment.SubAFE = subAfe.SubAFE;
					subAfe.Assignments.push(assignment);
				}
			}
			
			popup.enabled = false;
			ManagerService.getInstance().storeProject(subAfe, new Responder(
				function(result:ResultEvent):void {
					subAfe.IsNew = false;
					
					var collection:Array = parentController.parentController.appController.Model.projectsHash;
					
					if (null == collection[subAfe.SubAFE]) {
						collection[subAfe.SubAFE] = subAfe;
						ArrayCollection(view.dgSubAfes.dataProvider).addItem(subAfe);
						
						model.assetsByProject[subAfe.AFE] = new ArrayCollection();
						model.assignmentsByProject[subAfe.AFE] = new ArrayCollection();
					}
					
					if ((subAfe.AFE != oldRelatedAfe)
							&& (null != collection[subAfe.SubAFE])) {
						collection[subAfe.SubAFE] = null;
						ArrayCollection(view.dgSubAfes.dataProvider).removeItemAt(ArrayCollection(view.dgSubAfes.dataProvider).getItemIndex(subAfe));
					}
					
					PopUpManager.removePopUp(popup);
					
					view.onProjectChange(subAfe);
				},
				function(evt:FaultEvent):void {
					popup.enabled = true;
					onFault(evt);
				})
			);
		}
		
 		private function onSubAfesCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ArrayCollection(view.dgSubAfes.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgSubAfes.selectedIndex = lastGridIndex;
				view.dgSubAfes.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getSubAfe(subAfeName:String, subAfeShortName:String):ProjectDataObject
		{
			var collection:ArrayCollection = view.dgSubAfes.dataProvider as ArrayCollection;
			
			for each (var sA:ProjectDataObject in collection) {
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
