package UI.manager.admin
{
	import UI.manager.ManagerController;
	import App.Domain.Client;
	import App.Domain.Asset;
	import mx.collections.ArrayCollection;
	
	public class AdminController
	{
		
		public var view:AdminView;
		public var model:AdminModel;
		public var parentController:ManagerController;
		
		public function AdminController(view:AdminView, parentController:ManagerController) 
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new AdminModel();
		}
		
		public function getClientById(id:int):Client 
		{
			return view.viewClients.getClientById(id);
		}
		
		public function getAssetById(id:int):Asset
		{
			return view.viewAssets.getAssetById(id);
		}
		
		public function setAdminState(state:int):void 
		{
			switch (state) 
			{
				case AdminModel.ADMIN_VIEW_CLIENTS:
					view.tnAdmin.selectedChild = view.viewClients;
					break;
				case AdminModel.ADMIN_VIEW_ASSETS:
					view.tnAdmin.selectedChild = view.viewAssets;
					break;
			}
		}
		
	}
}