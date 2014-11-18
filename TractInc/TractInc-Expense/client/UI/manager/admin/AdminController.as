package UI.manager.admin
{
	import UI.manager.ManagerController;
	import mx.collections.ArrayCollection;
	import App.Entity.ClientDataObject;
	import App.Entity.AssetDataObject;
	import mx.controls.Alert;
	
	public class AdminController
	{
		
		public var view:AdminView;
		
		[Bindable]
		public var model:AdminModel;
		
		public var parentController:ManagerController;
		
		public function AdminController(view:AdminView, parentController:ManagerController) 
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new AdminModel();
		}
		
		public function init():void {
			model.data = parentController.model.data;
			model.assignments = new ArrayCollection(model.data.Assignments);
			
			setAdminState(AdminModel.ADMIN_VIEW_CLIENTS);
		}
		
		public function getClientById(id:int):ClientDataObject
		{
			return view.viewClients.getClientById(id);
		}
		
		public function getAssetById(id:int):AssetDataObject
		{
			return view.viewAssets.getAssetById(id);
		}
		
		public function getClients():ArrayCollection {
			return ArrayCollection(view.viewClients.dgClients.dataProvider);
		}
		
		public function getAssets():ArrayCollection {
			return ArrayCollection(view.viewAssets.dgAssets.dataProvider);
		}
		
		public function setAdminState(state:int):void 
		{
			switch (state) 
			{
				case AdminModel.ADMIN_VIEW_CLIENTS:
					view.tnAdmin.selectedChild = view.viewClients;
					view.viewClients.controller.open();
					break;
				case AdminModel.ADMIN_VIEW_ASSETS:
					view.tnAdmin.selectedChild = view.viewAssets;
					view.viewAssets.controller.open();
					break;
				case AdminModel.ADMIN_VIEW_RATES:
					view.tnAdmin.selectedChild = view.viewRates;
					view.viewRates.controller.open();
					break;
				case AdminModel.ADMIN_VIEW_ASSIGNMENTS:
					view.tnAdmin.selectedChild = view.viewAssignments;
					view.viewAssignments.controller.open();
					break;
			}
		}
		
	}
}