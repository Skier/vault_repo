
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
        import App.Entity.BillItemStatusDataObject;
        import common.StatusesRegistry;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.BillItem")]
        public dynamic class BillItem extends _BillItem
        {
            private var _relatedNotes:ActiveCollection = new ActiveCollection();
            private var _newNotes:ArrayCollection = new ArrayCollection();
            
            public function BillItem():void {
            	this.RelatedBillItemAttachment.addEventListener(CollectionEvent.COLLECTION_CHANGE, onAttachmentsChange);
           		RelatedWorkLog.addEventListener("loaded", onWorkLogLoaded);
            }
            
            private var _attach:BillItemAttachment = null;
            
            private var _workLog:String = '';
            public function get workLog():String {
            	return _workLog;
            }
            public function set workLog(value:String):void {
            	_workLog = value;
            	if (1 == RelatedWorkLog.length) {
            		WorkLog(RelatedWorkLog[0]).LogMessage = value;
            	}
            }
            
            private var _isWorkLogLoaded:Boolean = false;
            public function get isWorkLogLoaded():Boolean {
            	return _isWorkLogLoaded;
            }
            public function set isWorkLogLoaded(value:Boolean):void {
            	_isWorkLogLoaded = value;
            }
            
            private function onAttachmentsChange(evt:CollectionEvent):void {
            	if (0 == this.BillItemCompositionId) {
            		return;
            	}
            	
            	if (evt.location >= RelatedBillItemAttachment.length) {
            		return;
            	}
            	
            	if (CollectionEventKind.ADD == evt.kind) {
            		_attach = BillItemAttachment(this.RelatedBillItemAttachment[evt.location]);
            		
            		if (RelatedBillItemComposition.RelatedBillItem.IsLoaded) {
            			processBillItems();
            		} else {
            			RelatedBillItemComposition.RelatedBillItem.addEventListener("loaded", onBillItemsLoaded);
            		}
            	}
            }
            
            public function onWorkLogLoaded(evt:DynamicLoadEvent):void {
            	RelatedWorkLog.removeEventListener("loaded", onWorkLogLoaded);
            	_isWorkLogLoaded = true;
				if (1 == RelatedWorkLog.length) {
           			workLog = WorkLog(RelatedWorkLog[0]).LogMessage;
           		}
            }
            
            private function onBillItemsLoaded(evt:DynamicLoadEvent):void {
            	ActiveCollection(evt.data).removeEventListener("loaded", onBillItemsLoaded);
            	processBillItems();
            }
            
            private function processBillItems():void {
            	for each (var item:BillItem in RelatedBillItemComposition.RelatedBillItem) {
           			if (this == item) {
           				continue;
           			}
           			
           			item._attach = _attach;
           			item.tryAddAttachment();
            	}
            }
            
            public function tryAddAttachment():void {
           		if (RelatedBillItemAttachment.IsLoaded) {
           			processAttachments();
           		} else {
           			RelatedBillItemAttachment.addEventListener("loaded", onAttachmentsLoaded);
           		}
            }
            
            private function onAttachmentsLoaded(evt:DynamicLoadEvent):void {
            	RelatedBillItemAttachment.removeEventListener("loaded", onAttachmentsLoaded);
            	processAttachments();
            }
            
            private function processAttachments():void {
            	var alreadyContains:Boolean = false;
       			for each (var oldAttach:BillItemAttachment in RelatedBillItemAttachment) {
       				if (_attach.FileName == oldAttach.FileName) {
       					alreadyContains = true;
       					break;
       				}
       			}
       			
       			if (alreadyContains) {
       				return;
       			}
           		
           		var newAttach:BillItemAttachment = new BillItemAttachment();
           		newAttach.FileName = _attach.FileName;
           		newAttach.OriginalFileName = _attach.OriginalFileName;
           		newAttach.RelatedBillItem = this;
           		newAttach.save();
           		RelatedBillItemAttachment.addItem(newAttach);
            }
            
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
            
            // public var StatusTemp:BillItemStatus;
            private var _statusTemp:BillItemStatusDataObject;
            public function get StatusTemp():BillItemStatusDataObject {
            	return _statusTemp;
            }
            public function set StatusTemp(value:BillItemStatusDataObject):void {
            	_statusTemp = value;
            }
            
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
            	StatusTemp = StatusesRegistry.instance.getBillItemStatusByName(RelatedBillItemStatus.Status);
            	NotesTemp = Notes;
            	IsMarkedToRemove = false;
            }

            public function fromTempFields():void {
            	if (null != RelatedBillItemTypeTemp) {
            		RelatedBillItemType = RelatedBillItemTypeTemp;
            	}
            	if (null != StatusTemp) {
           			RelatedBillItemStatus = ActiveRecords.BillItemStatus.findByPrimaryKey(StatusTemp.Status);
            		Qty = QtyTemp;
            		BillRate = BillRateTemp;
            		Notes = NotesTemp;
           		}
            	IsMarkedToRemove = false;
            }
            
            public function isBillItemEditableOld():Boolean {
                return ((Status == BillItemStatusDataObject.BILL_ITEM_STATUS_NEW)
                    || (Status == BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED)
                    || (Status == BillItemStatusDataObject.BILL_ITEM_STATUS_CHANGED));
            }
            
            public function isBillItemEditable():Boolean {
                return isBillItemEditableOld()
                    && (0 == BillItemCompositionId);
            }
            
        }
      }
    