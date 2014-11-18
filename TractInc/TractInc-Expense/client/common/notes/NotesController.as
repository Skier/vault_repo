package common.notes
{
	import mx.events.CollectionEvent;
	import mx.collections.ArrayCollection;
	import common.PermissionsRegistry;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import flash.display.DisplayObject;
	import mx.managers.PopUpManager;
	import App.Entity.BillDataObject;
	import App.Entity.NoteDataObject;
	import App.Entity.BillItemDataObject;
	import App.Domain.User;
	import App.Domain.Bill;
	import App.Service.LandmanService;
	import mx.rpc.events.ResultEvent;
	import App.Entity.BillItemCompositionDataObject;
	
	[Bindable]
	public class NotesController
	{
		public var model:NotesModel;
		public var view:NotesView;
		
		public function NotesController(view:DisplayObject, item:*) 
		{
			this.view = view as NotesView;
			model = new NotesModel();
			model.item = item;
			
			if (item is BillDataObject) {
				model.itemId = BillDataObject(item).BillId;
				model.itemType = NoteDataObject.NOTE_TYPE_BILL;
			} else if (item is BillItemDataObject) {
				model.itemId = BillItemDataObject(item).BillItemId;
				model.itemType = NoteDataObject.NOTE_TYPE_BILL_ITEM;
			} else if (item is BillItemCompositionDataObject) {
				model.itemId = BillItemCompositionDataObject(item).BillItemCompositionId;
				model.itemType = NoteDataObject.NOTE_TYPE_MULTIDAY_ITEM;
			}
		}
		
		public function onClickOk():void 
		{
			var note:NoteDataObject = new NoteDataObject();
			note.RelatedItemId = model.itemId;
			note.ItemType = model.itemType;
			note.SenderId = AppController.User.UserId;
			note.Posted = new Date();
			note.NoteText = view.txtNewNote.text;
			note.SenderName = AppController.User.Login;
			
			if (null == model.item.Notes) {
				model.item.Notes = new Array();
			}
			
			model.item.Notes.push(note);
			
			if (model.item is BillDataObject) {
				LandmanService.getInstance().storeNotes(model.item.Notes, new Responder(
					function(evt:ResultEvent):void {
					},
					function(fault:FaultEvent):void {
						Alert.show("Please contact administrator", "System Error");
					}
				));
			}
			
			view.txtNewNote.text = "";
			view.r.dataProvider = model.item.Notes;
		}
		
		public function onClickClose():void 
		{
			if (null != view.onClose) {
				view.onClose();
			}
			
			PopUpManager.removePopUp(view);
		}
		
	}
	
}
