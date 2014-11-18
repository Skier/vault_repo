
      package App.Domain
      {
        import App.Domain.Codegen.*;
        import weborb.data.ActiveCollection;
        import mx.collections.ArrayCollection;
        import mx.events.CollectionEvent;
        import mx.events.CollectionEventKind;
        import weborb.data.DynamicLoadEvent;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.events.FaultEvent;
        import mx.events.PropertyChangeEvent;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.BillItem")]
        public dynamic class BillItem extends _BillItem
        {
            private var _relatedNotes:ActiveCollection = new ActiveCollection();
            private var _newNotes:ArrayCollection = new ArrayCollection();
            
            public function get relatedNotes():ActiveCollection 
            {
            	return _relatedNotes;
            }
            
	        public function loadNotes():void 
	        {
	        	_newNotes.removeAll();

	        	if (BillItemId != 0 && !_relatedNotes.IsLoaded) {
	        		if (!_relatedNotes.IsLoading) {
						_relatedNotes = ActiveRecords.Note.findByRelatedItemIdAndItemType(BillItemId, Note.NOTE_TYPE_BILL_ITEM, {Monitored:false});
	        		}
					_relatedNotes.addEventListener("loaded", onNotesLoaded);
	        	}
	        }
	        
	        public function saveNotes(responder:Responder=null):void 
	        {
	        	_newNotes.removeAll();

	        	for each (var note:Note in _relatedNotes) {
	        		note.RelatedItemId = BillItemId;
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
	        
            public var QtyTemp:int;

            public var BillRateTemp:Number;

            public var RelatedBillItemTypeTemp:BillItemType;
            
            public var IsMarkedToRemove:Boolean;
            
            public var StatusTemp:BillItemStatus;
            public var NotesTemp:String;
            
            public var isSaved:Boolean = false;
            public var isSelected:Boolean = false;
            public function setSelected(selected:Boolean):void 
            {
            	isSelected = selected;
            }
            
            public function toTempFields():void {
            	QtyTemp = Qty;
           		BillRateTemp = BillRate;
            	RelatedBillItemTypeTemp = RelatedBillItemType;
            	StatusTemp = RelatedBillItemStatus;
            	NotesTemp = Notes;
            	IsMarkedToRemove = false;
            }

            public function fromTempFields():void {
            	Qty = QtyTemp;
            	BillRate = BillRateTemp;
            	RelatedBillItemType = RelatedBillItemTypeTemp;
            	RelatedBillItemStatus = StatusTemp;
            	Notes = NotesTemp;
            	IsMarkedToRemove = false;
            }
            
            public function isBillItemEditable():Boolean {
                return (Status == BillItemStatus.BILL_ITEM_STATUS_NEW)
                    || (Status == BillItemStatus.BILL_ITEM_STATUS_REJECTED)
                    || (Status == BillItemStatus.BILL_ITEM_STATUS_CHANGED);
            }
            
        }
      }
    