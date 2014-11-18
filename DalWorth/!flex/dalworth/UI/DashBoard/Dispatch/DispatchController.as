package UI.DashBoard.Dispatch
{
	import mx.core.Container;
	import UI.DashBoard.MainController;
	import Domain.*;
	import Service.ServerSyncService;
	import mx.controls.Alert;
	import mx.collections.ArrayCollection;
	import Service.ServerSyncService;
	
	public class DispatchController
	{
		private var _mainController:MainController;
		private var _dispatchDashView:DispatchDashView;
		private var _createWorkView:CreateWorkView;		
		
		[Bindable]
		public var selectedChild:Container;
				
		public function DispatchController(mainController:MainController,
			dispatchDashView:DispatchDashView,
			createWorkView: CreateWorkView){
			 
			this._mainController = mainController;
			this._dispatchDashView = dispatchDashView;
			this._dispatchDashView.dispatchController = this;
			this._createWorkView = createWorkView;
			this._createWorkView.dispatchController = this;
			this.selectedChild = _dispatchDashView;
		}
		
		public function refreshDashborad():void{
			
			var svc:ServerSyncService = new ServerSyncService();
			svc.FindAllTechniciansAndWork(new Date(), onFindAllTechniciansOK, onFindAllTechniciansFailed);
		}

		public function showCreateWork():void{
			this.selectedChild = _createWorkView;			
		}

		public function closeCreateWork():void{
			this.selectedChild = _dispatchDashView;			
		}

		
		private function onFindAllTechniciansOK(techPackages:ArrayCollection):void{
			
			// TODO.  This is very crude.  More work will be done later
			_dispatchDashView.gridRow.removeAllChildren();
			
			for (var i:int = 0; i < techPackages.length; i++)
			{
				var pack:TechnicianPackage = techPackages.getItemAt(i) as TechnicianPackage;
				
				var techItem:TechGridItem = new TechGridItem();
				techItem.initialize();
				techItem.technician = pack.technician;
				techItem.work = pack.work;
				techItem.controller = this;
				
				if (pack.work == null){
					techItem.lblStatus.text = "No work assigned";
				}
				else  {					
					techItem.lblStatus.text = pack.work.WorkStatus.toString();
				}
								
				if (pack.work != null && pack.Tickets != null ){
					techItem.workGrid.dataProvider = pack.Tickets;
				}
				_dispatchDashView.gridRow.addChild(techItem);
			}
		}
		private function onFindAllTechniciansFailed(msg:String):void{
			Alert.show(msg);
		}
		
	}
}