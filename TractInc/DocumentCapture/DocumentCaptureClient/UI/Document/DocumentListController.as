package UI.Document
{
	import UI.AppController;
	import Domain.Document;
	import Domain.DataMapperRegistry;
	import mx.rpc.Responder;
	import mx.rpc.AsyncToken;
	import mx.rpc.events.FaultEvent;
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.utils.ObjectUtil;
	import mx.rpc.events.ResultEvent;
	
	[Bindable]
	public class DocumentListController
	{
		public var Model:DocumentListModel;
		public var View:DocumentListView;
		public var Parent:AppController;
		
		public function DocumentListController(view:DocumentListView, parent:AppController):void{
			Parent = parent;
			View = view;
			Model = new DocumentListModel();
		}
		
		public function Reload():void{
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Document.findAllAsync();
			asyncToken.addResponder(new Responder(onGetDocuments, onFault));
		}
		
		public function OnCreationComplete():void{
			Reload();
		}
		
		public function OnDocListChange():void{
			var document:Document = View.documentList.selectedItem as Document;
			SetCurrentDocument(document);
		}
		
		public function SetCurrentDocument(document:Document):void{
			Model.CurrentDocument = document;
			View.documentList.selectedItem = document;
		}
		
		public function OnCreateNewDoc():void{
			var document:Document = new Document();
			Model.CurrentDocument = document;
			Parent.OpenDocument(document);
		}
		
		public function OnCopyDoc():void{
			var newDoc:Document = Model.CurrentDocument.copy();
			Model.CurrentDocument = newDoc;
			Parent.OpenDocument(newDoc);
		}
		
		public function OnDeleteDoc():void{
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Document.remove(Model.CurrentDocument);
			asyncToken.addResponder(new Responder(
				function(document:Document):void{
					Model.Documents.removeItemAt(Model.Documents.getItemIndex(document));
					SetCurrentDocument(document);
				}, onFault
			))
		}
		
		public function OnEditDoc():void{
			var doc:Document = Model.CurrentDocument;
			doc.IsNew = false;
			Parent.OpenDocument(doc);
		}
		
		private function onGetDocuments(docList:Array):void{
			Model.Documents = new ArrayCollection(docList);
			if (Model.Documents.length > 0){
				SetCurrentDocument(Model.Documents[0] as Document);
			}
		}

		private function onFault(faultEvent:FaultEvent):void{
			Alert.show(faultEvent.fault.message);
		}
	}
}