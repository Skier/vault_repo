package common.notes
{
	import weborb.data.ActiveRecord;
	import App.Domain.ActiveRecords;
	import App.Domain.Bill;
	import mx.events.CollectionEvent;
	import App.Domain.BillItem;
	import weborb.data.DynamicLoadEvent;
	import App.Domain.Note;
	import weborb.data.ActiveCollection;
	import mx.collections.ArrayCollection;
	import App.Domain.InvoiceItem;
	import common.PermissionsRegistry;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import App.Domain.Invoice;
	import flash.display.DisplayObject;
	import mx.managers.PopUpManager;
	
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
			
			if (item is Bill) {
				model.itemId = Bill(item).BillId;
				model.itemType = Note.NOTE_TYPE_BILL;
			} else if (item is BillItem) {
				model.itemId = BillItem(item).BillItemId;
				model.itemType = Note.NOTE_TYPE_BILL_ITEM;
			} else if (item is Invoice) {
				model.itemId = Invoice(item).InvoiceId;
				model.itemType = Note.NOTE_TYPE_INVOICE;
			} else if (item is InvoiceItem) {
				model.itemId = InvoiceItem(item).InvoiceItemId;
				model.itemType = Note.NOTE_TYPE_INVOICE_ITEM;
			} else {
				throw new Error("Unsupported note type");
			}
/* 
			if (item.relatedNotes.IsLoaded) {
				model.isLoaded = true;
				return;
			}

			var notesList:ArrayCollection = new ArrayCollection();
			notesList = ActiveRecords.Note.findByRelatedItemIdAndItemType(model.itemId, model.itemType);
			notesList.addEventListener("loaded", 
				function (event:DynamicLoadEvent):void {
					model.item.relatedNotes.removeAll();
					for each (var note:Note in event.data as ArrayCollection) {
						model.item.relatedNotes.addItem(note);
					}
					model.isLoaded = true;
				});
 */
 
 			if (!model.item.isLoaded) {
 				model.item.loadNotes();
 			}
		}
		
		public function onClickOk():void 
		{
			var note:Note = new Note();
			note.RelatedItemId = model.itemId;
			note.ItemType = model.itemType;
			note.RelatedUser = PermissionsRegistry.getInstance().user;
			note.Posted = new Date();
			note.NoteText = view.txtNewNote.text;

			view.boxControls.enabled = false;

			note.save(false, new Responder(onSaved, onFault));
		}
		
		public function onClickClose():void 
		{
			PopUpManager.removePopUp(view);
		}
		
		private function onSaved(note:Note):void 
		{
			model.item.relatedNotes.addItem(note);
			view.boxControls.enabled = true;
			view.txtNewNote.text = "";
		}
		
		private function onFault(event:FaultEvent):void 
		{
			view.boxControls.enabled = true;
			Alert.show(event.fault.message);
		}
		
	}
}