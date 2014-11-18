package App.Domain
{
	import App.Domain.Codegen.*;
	import weborb.data.ActiveCollection;
	import mx.collections.ArrayCollection;
	import weborb.data.DynamicLoadEvent;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
        
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Domain.InvoiceItem")]
    public dynamic class InvoiceItem extends _InvoiceItem
    {
        private var _relatedNotes:ActiveCollection = new ActiveCollection();
        private var _newNotes:ArrayCollection = new ArrayCollection();

        public var isNew:Boolean = false;

		public function get relatedNotes():ActiveCollection
		{
			return _relatedNotes;
		}
          
        public function setSelected(selected:Boolean):void 
        {
         	IsSelected = selected;
        }
        
        public function loadNotes():void 
        {
        	_newNotes.removeAll();

        	if (InvoiceItemId != 0 && !_relatedNotes.IsLoaded) {
        		if (!_relatedNotes.IsLoading) {
					_relatedNotes = ActiveRecords.Note.findByRelatedItemIdAndItemType(InvoiceItemId, Note.NOTE_TYPE_INVOICE_ITEM, {Monitored:false});
        		}
				_relatedNotes.addEventListener("loaded", onNotesLoaded);
        	}
        }
        
        public function saveNotes(responder:Responder=null):void 
        {
        	_newNotes.removeAll();

        	for each (var note:Note in _relatedNotes) {
        		note.RelatedItemId = InvoiceItemId;
        		note.save(false, responder);
        	}
        }
        
        public function cancelNotes(responder:Responder=null):void 
        {
        	for each (var note:Note in _newNotes) {
        		note.remove(new Responder(
        			function (event:*):void {
        				if (_newNotes.length == 0 && responder != null) {
        					responder.result(this);
        				}
        			}, 
        			function (event:FaultEvent):void {
        				if (responder != null) {
	        				responder.fault(event);
        				}
        			}));
        	}
        }
        
        private function onNotesLoaded(event:DynamicLoadEvent):void 
        {
			_relatedNotes.removeEventListener("loaded", onNotesLoaded);
        	_relatedNotes.addEventListener(CollectionEvent.COLLECTION_CHANGE, onRelatedNotesChanged);
        }
        
        private function onRelatedNotesChanged(event:CollectionEvent):void 
        {
        	if (event.kind == CollectionEventKind.ADD) {
        		for each (var note:Note in event.items) {
        			_newNotes.addItem(note);
        		}
        	}
        }
	        
    }
}
    