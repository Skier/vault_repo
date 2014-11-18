package src.sync
{
	public class SyncController
	{
		
		public var Model:SyncModel;
		public var View:SyncView;
		
		public function SyncController(view:SyncView):void {
			View = view;
			Model = new SyncModel();
		}
		
		public function Init():void {
		
		}
		
	}
}