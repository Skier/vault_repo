package UI.DashBoard
{
	import Domain.*;
	
	public class MainController
	{
		public var appController:AppController;
		private var view:MainView;
		
		public function MainController(view:MainView, parent:AppController):void {
			this.view = view;
			appController = parent;
		}
		
	}
}