package UI.Document
{
	import Domain.Document;
	import Domain.DataMapperRegistry;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.rpc.events.ResultEvent;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class DocumentSearchController
	{
		public var View:DocumentSearchView;
		public var Parent:DocumentController;
		public var Model:DocumentSearchModel;
		
		public function DocumentSearchController(view:DocumentSearchView, parent:DocumentController):void{
			View = view;
			Parent = parent;
			Model = new DocumentSearchModel;
		}
		
		public function set Template(document:Document):void{
			Model.CurrentDocument = document;
			Model.Items.removeAll();
			getCount();
		}
		
		public function ShowDocuments():void{
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Document.GetListByDoc(Model.CurrentDocument);
			asyncToken.addResponder(new Responder(getListSuccess, onFault));
		}
		
		public function MatchDocument(document:Document):void{
			document.IsNew = false;
			Parent.Init(document);
		}
		
		private function getCount():void
		{
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Document.GetCountByDoc(Model.CurrentDocument);
			asyncToken.addResponder(new Responder(getCountSuccess, onFault));
		}
		
		private function getCountSuccess(event:ResultEvent):void{
			var count:int = event.result as int
			Model.Count = count;
			if (count > 0){
				Parent.Model.IsMatched = true;
			} else {
				Parent.Model.IsMatched = false;
			}
		}
		
		private function getListSuccess(docList:Array):void{
			Model.Items = new ArrayCollection(docList);
		}
		
		private function onFault(faultEvent:FaultEvent):void{
			Alert.show(faultEvent.fault.message);
		}
	}
}