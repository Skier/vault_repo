package UI.landman
{

    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.events.CollectionEvent;
    import mx.events.DataGridEvent;
    import mx.events.ListEvent;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.controls.ComboBox;
    import mx.managers.PopUpManager;
    import mx.events.FlexEvent;
    import mx.events.ValidationResultEvent;
    import flash.events.Event;
    import util.ArrayUtil;
    import util.DateUtil;
    import common.StatusesRegistry;
    import common.TypesRegistry;
    import mx.collections.ListCollectionView;
    import mx.events.PropertyChangeEvent;
    import mx.events.PropertyChangeEventKind;
    import mx.events.DynamicEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.controls.ComboBase;
    import mx.events.CloseEvent;
    import mx.controls.Label;
    import App.Entity.BillItemDataObject;
    import App.Entity.WorkLogDataObject;
    import App.Service.LandmanService;
    import App.Entity.BillItemTypeDataObject;
    import App.Entity.RateByAssignmentDataObject;
    import App.Entity.AssetAssignmentDataObject;
    import App.Entity.AssetDataObject;
    import App.Entity.BillItemStatusDataObject;

    [Bindable]
    public class ItemsController
    {
        
        public static const MAX_AMOUNT: Number = 1000000;

        public var view: ItemsView;
        public var Model: ItemsModel = new ItemsModel();
        public var mainApp: DiaryController;
        public var mainModel: DiaryModel;
        
        public var totalAmount:Number = 0;

        private var date: Date;
        
        private var closeWindow:Boolean = false;

        public function ItemsController(view: ItemsView, parent: DiaryController): void {
            this.view = view;
            mainApp = parent;
            mainModel = mainApp.Model;
        }
        
        public function get BillingDate():Date {
            return date;
        }

        public function set BillingDate(d:Date):void {
            date = d;
            var asset:AssetDataObject = mainApp.Model.asset;
            view.title = "User Name: " + asset.FirstName + " " + asset.LastName +
                ", Billing Date: " + DateUtil.format(date) +
                " - " + Model.dailyEvent.assignment.AFE +
                " / " + Model.dailyEvent.assignment.SubAFE;
        }
        
        public function tryToAddBillItem(item:Object):void {
        	if ((Model.dailyEvent.billItems.getItemIndex(BillItemDataObject(item)) == Model.dailyEvent.billItems.length - 1)
            		&& (0 != BillItemDataObject(item).QtyTemp)
            		&& (0 != BillItemDataObject(item).BillRateTemp)
            		&& Model.dailyEvent.isEditable) {
            	AddBillItem();
            }
        }
        
        public function OnBillItemTypeEditorChange(event:ListEvent):void {
            var itemType:BillItemTypeDataObject = BillItemTypeDataObject(ComboBox(event.target).selectedItem);
            var item:BillItemDataObject = BillItemDataObject(event.currentTarget.data);
            
            tryToAddBillItem(item);
            
            item.BillItemTypeIdTemp = itemType.BillItemTypeId;
            
            item.QtyTemp = 0;
            item.BillRateTemp = 0;
            if (null == itemType || 0 == itemType.BillItemTypeId) {
                return;
            }

            if (!itemType.IsCountable) {
                item.QtyTemp = 1;
            }

            var rate:RateByAssignmentDataObject = Model.dailyEvent.assignment.ratesHash[itemType.BillItemTypeId];
            if (itemType.IsPresetRate) {
                if (null == rate) {
                    item.BillRateTemp = 0;
                } else {
                    item.BillRateTemp = rate.BillRate;
                }
            }
            
            processBillItemTypes();
            if (null == view.billItemGrid.editedItemRenderer) {
            	return;
            }
			view.billItemGrid.editedItemRenderer.dispatchEvent(new PropertyChangeEvent(PropertyChangeEventKind.UPDATE));
        }
        
        public function refreshBillItemTypes(control:ComboBox, data:Object):void {
        	processBillItemTypes();
        	
        	Model.billItemTypesFiltered.removeAll();
        	if (null != data) {
	        	var billItem:BillItemDataObject = BillItemDataObject(data);
       			if (0 != billItem.BillItemTypeIdTemp) {
       				var itemType:BillItemTypeDataObject = TypesRegistry.instance.getBillItemTypeByName(billItem.BillItemTypeName);
		        	if (-1 == Model.billItemTypes.getItemIndex(itemType)) {
   		    			Model.billItemTypesFiltered.addItem(itemType);
   	    			}
   	    		}
        	}
        	for each (var type:BillItemTypeDataObject in Model.billItemTypes) {
        		if (!Model.billItemTypesFiltered.contains(type)) {
        			Model.billItemTypesFiltered.addItem(type);
        		}
        	}
        	
        	if (null != data) {
       			control.selectedItem = TypesRegistry.instance.getBillItemTypeByName(billItem.BillItemTypeName);
       		}
        }
        
        public function processBillItemTypes():void {
			Model.billItemTypesView.filterFunction = typesFilter;
			Model.billItemTypesView.refresh();
			Model.billItemTypes = new ArrayCollection(Model.billItemTypesView.toArray());
        }

		private function typesFilter(item:Object):Boolean {
			var itemType:BillItemTypeDataObject = BillItemTypeDataObject(item);
			
			for each (var billItem:BillItemDataObject in Model.dailyEvent.billItems) {
				if (0 == billItem.BillItemTypeIdTemp) {
					continue;
				}
				if ((billItem.BillItemTypeIdTemp == itemType.BillItemTypeId)
						&& (itemType.IsSingle)) {
					return false;
				}
			}
			return (itemType.BillItemTypeId != BillItemTypeDataObject.BILL_ITEM_TYPE_DAILY_BILLING);
		}

        public function GetItemAmount(item:Object, column:DataGridColumn):Number {
        	recalcTotalAmount();
            return item.BillRateTemp * item.QtyTemp;
        }
        
        public function GetItemAmountString(item:Object, column:DataGridColumn):String {
        	var amt:Number = GetItemAmount(item, column);
        	return (Math.round(amt * 100) / 100).toFixed(2);
        }

		public function dailyBillingChanged(evt:ListEvent):void {
            if (0 == Model.dailyEvent.billItems.length) {
            	AddBillItem();
            }
            if (0 != ComboBox(evt.currentTarget).selectedIndex) {
            	Model.dailyEvent.dailyBillItem.IsMarkedToRemove = false;
            }
            recalcTotalAmount();
  		}
  		
  		private var _canSave:Boolean = false;
  		public function get canSave():Boolean {
  			return _canSave;
  		}
  		public function set canSave(value:Boolean):void {
  			_canSave = value;
  		}
  		
  		public function dailyQtyChanged(evt:PropertyChangeEvent):void {
  			canSave = 0 != Model.dailyEvent.dailyBillItem.QtyTemp;
  		}
            
        public function recalcTotalAmount():void {
        	if (null != view.billItemGrid.editedItemPosition) {
        		if ((-1 != view.billItemGrid.editedItemPosition.rowIndex)
        				&& (view.billItemGrid.editedItemPosition.rowIndex < Model.dailyEvent.billItems.length)) {
        			tryToAddBillItem(Model.dailyEvent.billItems[view.billItemGrid.editedItemPosition.rowIndex]);
        		}
        	}
            totalAmount = 0;

            var i:int;

            for (i = 0; i < Model.dailyEvent.billItems.length; i ++) {
                var item:BillItemDataObject = BillItemDataObject(Model.dailyEvent.billItems[i]);
                totalAmount += item.BillRateTemp * item.QtyTemp;
            }

            if (null != Model.dailyEvent.dailyBillItem) {
            	totalAmount += Model.dailyEvent.dailyBillItem.BillRateTemp * Model.dailyEvent.dailyBillItem.QtyTemp;
            }
            view.invalidateDisplayList();
            view.invalidateProperties();
            view.saveButton.invalidateProperties();
        }
        
        public function onRemoveBillItem(obj:Object):void {
        	var item:BillItemDataObject = BillItemDataObject(obj);
        	if (1 == item.BillItemTypeIdTemp) {
        		Alert.show("Are you sure you want to remove all items?", "Remove AFE/Project", 3, view, removeBillItems);
        	} else {
        		Alert.show("Are you sure you want to remove this item?", "Remove Item", 3, view, removeBillItem);
        	}
        }

        private function removeBillItem(event:CloseEvent):void {
        	var item:BillItemDataObject = BillItemDataObject(view.billItemGrid.selectedItem);
            if (event.detail == Alert.YES) {
                item.IsMarkedToRemove = true;
            }
            
            recalcTotalAmount();
        }
            
        private function removeBillItems(event:CloseEvent):void {
            if (event.detail == Alert.YES) {
            	for each (var item:BillItemDataObject in Model.dailyEvent._billItems) {
                	item.IsMarkedToRemove = true;
            	}
        		Model.dailyEvent.dailyBillItem.QtyTemp = 0;
            }
            
            recalcTotalAmount();
        }
        
        private function validateBillItems():Boolean {
        	view.messagesBox.removeAllChildren();
        	
            var i:int;
            var valid:Boolean = true;
            var realLength:int = Model.dailyEvent.billItems.length;

            for (i = 0; i < Model.dailyEvent._billItems.length; i ++) {
                var item:BillItemDataObject = BillItemDataObject(Model.dailyEvent._billItems[i]);
                if (0 == item.BillItemTypeIdTemp) {
   	                realLength --;
   	                continue;
       	        }
                if (1 != item.BillItemTypeIdTemp) {
            	    if ((!item.IsMarkedToRemove) && ((0 == item.BillRateTemp) || (0 == item.QtyTemp))) {
                	    valid = false;
	                }
                }
                if (!item.IsPresetRate
                		&& !item.IsMarkedToRemove) {
               		var assignment:AssetAssignmentDataObject = AssetAssignmentDataObject(mainApp.Model.assignmentsHash[Model.dailyEvent.subAfe]);
                	var rate:RateByAssignmentDataObject = RateByAssignmentDataObject(assignment.ratesHash[item.BillItemTypeIdTemp]);
   	           		if (null != rate
       	       				&& rate.ShouldNotExceedRate
           	   				&& (item.BillRateTemp > rate.BillRate)) {
           				addErrorMessage("Bill rate for \"" + item.BillItemTypeName
           					+ "\" item type should not exceed $" + rate.BillRate.toFixed(3));
               		}
                }
            }
            
            if (!valid) {
            	addErrorMessage("Fill fields marked with red asterisk");
            }
            if (totalAmount > MAX_AMOUNT) {
            	addErrorMessage("Total amount exceeds maximum value");
            }
            if (8 < Model.dailyEvent.dailyBillItem.QtyTemp + Model.dailyEvent.group.totalDailyBill - Model.dailyEvent.dailyBillItem.Qty) {
            	addErrorMessage("Total daily billing hours exceeds maximum value");
            }
            if ((0 == Model.dailyEvent.dailyBillItem.QtyTemp) && (0 < realLength)) {
            	addErrorMessage("Pick daily billing hours value");
            }

            return 0 == view.messagesBox.numChildren;
        }
        
        private function addErrorMessage(message:String):void {
        	var label:Label = new Label();
            label.text = message;
   			view.messagesBox.addChild(label);
        }
        
        public function onOkClick():void {
        	closeWindow = true;
        	SaveBillItems();
        }

        public function onApplyClick():void {
        	closeWindow = false;
        	SaveBillItems();
        }
		
		private var cancelling:Boolean = false;
		
        public function SaveBillItems():void {
            if (!validateBillItems()) {
   	    		closeWindow = false;
       	        return;
           	}

			view.enabled = false;
			var hasToSave:Boolean = false;
			
			if (null == Model.dailyEvent.dailyBillItem.WorkLogInfo) {
				var workLogInfo:WorkLogDataObject = new WorkLogDataObject();
				workLogInfo.BillItemId = Model.dailyEvent.dailyBillItem.BillItemId;
				workLogInfo.BillItemInfo = Model.dailyEvent.dailyBillItem;
				workLogInfo.LogMessage = view.taWorkLog.text;
				Model.dailyEvent.dailyBillItem.WorkLogInfo = workLogInfo;
			} else {
				Model.dailyEvent.dailyBillItem.WorkLogInfo.LogMessage = view.taWorkLog.text;
			}
			
            for (var i:int = 0; i < Model.dailyEvent._billItems.length; i ++) {
                var item:BillItemDataObject = BillItemDataObject(Model.dailyEvent._billItems[i]);
                if ((0 == item.BillItemId)
	               		&& ((0 == item.BillItemTypeIdTemp)
	               			|| item.IsMarkedToRemove)) {
                   	Model.dailyEvent.removeBillItem(item);
                   	i --;
                   	continue;
                }
                
                if (!item.IsMarkedToRemove) {
                	item.NotesTemp = item.Notes;
                	item.fromTempFields();
                }
                
                if (0 != item.BillItemCompositionId) {
                	item.isSaved = true;
                	continue;
                }
                
                item.BillingDate = DateUtil.format(date);
                item.AssetAssignmentId = Model.dailyEvent.assignment.AssetAssignmentId;
                if (BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED == item.Status) {
                	item.Status = BillItemStatusDataObject.BILL_ITEM_STATUS_CHANGED;
                }
                hasToSave = true;
                item.isSaved = false;
            }
            
            if (!hasToSave) {
            	onUpdateFinished(null);
            } else {
            	Model.dailyEvent.saveItems(new Responder(onItemsSaved, onItemsFailed));
            }
        }
        
        private function onItemsSaved(obj:Object):void {
        	if (closeWindow) {
        		view.onClose();
        	} else {
        		AddBillItem();
        		view.enabled = true;
        	}
        }
        
        private function onItemsFailed(event:FaultEvent):void {
            Alert.show("Cannot save bill items");
        	view.enabled = true;
        }
        
        private function onUpdateFinished(obj:Object):void {
        	if (closeWindow) {
        		view.onClose();
        	} else {
        		AddBillItem();
        	}
   			view.enabled = true;
        }

        public function CancelBillItems():void {
            for (var i:int = 0; i < Model.dailyEvent._billItems.length; i ++) {
                var item:BillItemDataObject = BillItemDataObject(Model.dailyEvent._billItems[i]);
                
                if (0 == item.BillItemId) {
                    Model.dailyEvent.removeBillItem(item);
                    i --;
                    continue;
                }
                
                item.toTempFields();
            }
            
            closeWindow = true;
            cancelling = true;
            view.onClose();
        }

        public function AddBillItem(): BillItemDataObject {
            var billItem:BillItemDataObject = new BillItemDataObject();
            billItem.BillId = Model.bill.BillId;
            billItem.BillItemTypeId = 0;
            billItem.Status = BillItemStatusDataObject.BILL_ITEM_STATUS_NEW;
            billItem.BillRate = 0;
            billItem.Qty = 0;
            billItem.BillingDate = DateUtil.format(Model.dailyEvent.date);
            billItem.AssetAssignmentId = Model.assignment.AssetAssignmentId;
            Model.dailyEvent.addBillItem(billItem);
            return billItem;
        }
        
    }

}
