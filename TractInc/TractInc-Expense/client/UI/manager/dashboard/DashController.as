package UI.manager.dashboard
{
	import UI.manager.ManagerController;
	import App.Domain.User;
	import common.PermissionsRegistry;
	
	public class DashController
	{
		
		public var view:DashView;
		public var model:DashModel;
		public var parentController:ManagerController;
		
		public function DashController(view:DashView, parent:ManagerController):void {
			this.view = view;
			this.parentController = parent;
		}
		
		public function init():void {
			model = new DashModel();
			model.currentUser = parentController.appController.Model.CurrentUser;
			view.msgPanel.init(model.currentUser.UserId);
		}
		
	}
}