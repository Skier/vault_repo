package UI.Document.Tract
{
	import UI.Document.DocumentController;
	import Domain.Tract;
	import mx.managers.PopUpManager;
	import Domain.Tractexception;
	import mx.rpc.AsyncToken;
	import Domain.DataMapperRegistry;
	import mx.collections.ArrayCollection;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import UI.AppController;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	[Bindable]
	public class TractController
	{
		
		public var View:TractView;
		public var Model:TractModel;
		
		private var App:AppController;
		
		public function TractController(view:TractView):void {
			View = view;
			Model = new TractModel();
		}
		
		public function Init(tract:Tract):void {
			Model.tract = tract;
			getExceptions();
		}
		
		public function OnClickComplete():void {
			
			if (!View.IsValid()) {
				return;
			}
			
			Model.tract.save(false, new Responder(onSavedOk, onFault));

            PopUpManager.removePopUp(View);
		}
		
		public function OnClickCancel():void {
            PopUpManager.removePopUp(View);
		}
		
		public function OnClickScopeUrl():void {
			var url:URLRequest = new URLRequest("../scopeScatch/ScopeScetch.html");
			navigateToURL(url, "_blank");
		}
		
		public function OnClickAddException():void {
			var popupWin:TractExceptionView = TractExceptionView(PopUpManager.createPopUp(View, TractExceptionView, true));
			popupWin.exception = new Tractexception();
			popupWin.exception.isNew = true;
			popupWin.parentCollection = Model.exceptions;
		}
		
		public function OnClickEditException():void {
			var popupWin:TractExceptionView = TractExceptionView(PopUpManager.createPopUp(View, TractExceptionView, true));
			popupWin.exception = View.dgExceptions.selectedItem as Tractexception;
			popupWin.exception.isNew = false;
			popupWin.parentCollection = Model.exceptions;
		}
		
		public function OnClickRemoveException():void {
			var idx:int = View.dgExceptions.selectedIndex;
			var exception:Tractexception = View.dgExceptions.selectedItem as Tractexception;
			exception.remove();
			if (idx > -1) {
				Model.exceptions.removeItemAt(idx);
			}
		}
		
		private function onSavedOk(obj:Object):void {
			for each (var exception:Tractexception in Model.exceptions){
				exception.TractID = Model.tract.TractID;
				exception.save();
			}

			if (Model.tract.isNew) {
				Model.parentCollection.addItem(Model.tract);
			}
		}
		
		private function getExceptions():void {
			if (Model.tract.TractID == 0)
				return;
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Tractexception.getByTractID(Model.tract.TractID);
			asyncToken.addResponder(new Responder(gotExceptionsOk, onFault));
		}
		
		private function gotExceptionsOk(exceptionsList:Array):void {
			Model.exceptions = new ArrayCollection(exceptionsList);
		}
		
		private function onFault(event:FaultEvent):void {
			Alert.show(event.fault.message, "Fault");
		}
		
	}
}