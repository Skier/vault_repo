package UI.landman
{

    import mx.collections.ArrayCollection;
    import App.Domain.*;
    import mx.controls.Alert;
    import mx.events.CollectionEvent;
    import mx.events.DataGridEvent;
    import mx.events.ListEvent;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.controls.ComboBox;
    import mx.managers.PopUpManager;
    import weborb.data.ActiveCollection;
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
    import weborb.data.DynamicLoadEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.controls.ComboBase;
    import mx.events.CloseEvent;
    import mx.controls.Label;

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

        // Accessors

        public function get BillingDate():Date {
            return date;
        }

        public function set BillingDate(d:Date):void {
            date = d;
            var asset: Asset = mainApp.Model.asset;
            view.title = "User Name: " + asset.FirstName + " " + asset.LastName +
                ", Billing Date: " + DateUtil.format(date) +
                " - " + Model.assignment.AFE +
                " / " + Model.assignment.SubAFE;
        }
        
        public function tryToAddBillItem(item:Object):void {
        	if ((Model.dailyEvent.billItems.getItemIndex(BillItem(item)) == Model.dailyEvent.billItems.length - 1)
            		&& (0 != BillItem(item).QtyTemp)
            		&& (0 != BillItem(item).BillRateTemp)
            		&& Model.dailyEvent.isEditable()) {
            	AddBillItem();
            }
        }
        
        public function OnBillItemTypeEditorChange(event:ListEvent):void {
            var itemType:BillItemType = BillItemType(ComboBox(event.target).selectedItem);
            var item:BillItem = BillItem(event.currentTarget.data);
            
            tryToAddBillItem(item);
            
            item.RelatedBillItemTypeTemp = itemType;
            
            item.QtyTemp = 0;
            item.BillRateTemp = 0;
            if (null == itemType || 0 == itemType.BillItemTypeId) {
                return;
            }

            if (!itemType.IsCountable) {
                item.QtyTemp = 1;
            }

            var rate:RateByAssignment = Model.assignment.ratesHash[itemType.BillItemTypeId];
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
	        	var billItem:BillItem = BillItem(data);
        		if (null != billItem.RelatedBillItemTypeTemp) {
        			if (0 != billItem.RelatedBillItemTypeTemp.BillItemTypeId) {
			        	if (-1 == Model.billItemTypes.getItemIndex(billItem.RelatedBillItemTypeTemp)) {
    		    			Model.billItemTypesFiltered.addItem(billItem.RelatedBillItemTypeTemp);
    	    			}
    	    		}
	        	}
        	}
        	for each (var type:BillItemType in Model.billItemTypes) {
        		if (!Model.billItemTypesFiltered.contains(type)) {
        			Model.billItemTypesFiltered.addItem(type);
        		}
        	}
        	
        	if (null != data) {
       			control.selectedIndex = Model.billItemTypesFiltered.getItemIndex(BillItem(data).RelatedBillItemTypeTemp);
       		}
        }
        
        public function processBillItemTypes():void {
			Model.billItemTypesView.filterFunction = typesFilter;
			Model.billItemTypesView.refresh();
			Model.billItemTypes = new ArrayCollection(Model.billItemTypesView.toArray());
        }

		private function typesFilter(item:Object):Boolean {
			var itemType:BillItemType = BillItemType(item);
			
			for each (var billItem:BillItem in Model.dailyEvent.billItems) {
				if (null == billItem.RelatedBillItemTypeTemp) {
					continue;
				}
				if ((billItem.RelatedBillItemTypeTemp.BillItemTypeId == itemType.BillItemTypeId)
						&& (itemType.IsSingle)) {
					return false;
				}
			}
			return (itemType.BillItemTypeId != BillItemType.BILL_ITEM_TYPE_DAILY_BILLING);
		}

        public function GetItemAmount(item:Object, column:DataGridColumn):Number {
        	recalcTotalAmount();
            return item.BillRateTemp * item.QtyTemp;
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
                var item:BillItem = BillItem(Model.dailyEvent.billItems[i]);
                totalAmount += item.BillRateTemp * item.QtyTemp;
            }
            if (null != Model.dailyEvent.dailyBillItem) {
            	totalAmount += Model.dailyEvent.dailyBillItem.BillRateTemp * Model.dailyEvent.dailyBillItem.QtyTemp;
            }
        }
        
        public function onRemoveBillItem(obj:Object):void {
        	var item:BillItem = BillItem(obj);
        	if (1 == item.RelatedBillItemTypeTemp.BillItemTypeId) {
        		Alert.show("Are you sure you want to remove all items?", "Remove AFE/Project", 3, view, removeBillItems);
        	} else {
        		Alert.show("Are you sure you want to remove this item?", "Remove Item", 3, view, removeBillItem);
        	}
        }

        private function removeBillItem(event:CloseEvent):void {
        	var item:BillItem = BillItem(view.billItemGrid.selectedItem);
            if (event.detail == Alert.YES) {
                item.IsMarkedToRemove = true;
                Model.dailyEvent.updateBillItems(null);
            }
        }
            
        private function removeBillItems(event:CloseEvent):void {
            if (event.detail == Alert.YES) {
            	for each (var item:BillItem in Model.dailyEvent._billItems) {
                	item.IsMarkedToRemove = true;
             	}
             	Model.dailyEvent.updateBillItems(null);
        		Model.dailyEvent.dailyBillItem.QtyTemp = 0;
            }
        }
        
        private function validateBillItems():Boolean {
        	view.messagesBox.removeAllChildren();
        	
            var i:int;
            var valid:Boolean = true;
            var realLength:int = Model.dailyEvent.billItems.length;

            for (i = 0; i < Model.dailyEvent._billItems.length; i ++) {
                var item:BillItem = BillItem(Model.dailyEvent._billItems[i]);
                if (null == item.RelatedBillItemTypeTemp || item.RelatedBillItemTypeTemp.BillItemTypeId == 0) {
   	                realLength --;
   	                continue;
       	        }
                if (1 != item.RelatedBillItemTypeTemp.BillItemTypeId) {
            	    if ((0 == item.BillRateTemp) || (0 == item.QtyTemp)) {
                	    valid = false;
	                }
                }
                if (!item.RelatedBillItemTypeTemp.IsPresetRate
                		&& !item.IsMarkedToRemove) {
                	if (null != item.RelatedAssetAssignment.ratesHash) {
	                	var rate:RateByAssignment = RateByAssignment(item.RelatedAssetAssignment.ratesHash[item.RelatedBillItemTypeTemp.BillItemTypeId]);
    	           		if (null != rate
        	       				&& rate.ShouldNotExceedRate
            	   				&& (item.BillRateTemp > rate.BillRate)) {
               				addErrorMessage("Bill rate for \"" + item.RelatedBillItemTypeTemp.Name
               					+ "\" item type should not exceed $" + rate.BillRate.toFixed(3));
	               		}
               		}
                }
/* 
                if (closeWindow
                		&& item.RelatedBillItemTypeTemp.IsAttachRequired
                		&& (0 == item.RelatedBillItemAttachment.length)
                		&& !item.IsMarkedToRemove) {
                	addErrorMessage("Bill items with \"" + item.RelatedBillItemTypeTemp.Name
               				+ "\" type requires file attachment");
                }
 */
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
        	if (!cancelling) {
	            if (!validateBillItems()) {
    	    		closeWindow = false;
        	        return;
            	}
         	}

			view.enabled = false;
			var hasToSave:Boolean = false;
			
            for (var i:int = 0; i < Model.dailyEvent._billItems.length; i ++) {
                var item:BillItem = BillItem(Model.dailyEvent._billItems[i]);
                if (item.IsMarkedToRemove
                		|| (null == item.RelatedBillItemTypeTemp)
                		|| (0 == item.RelatedBillItemTypeTemp.BillItemTypeId)) {
                    if (0 != item.BillItemId) {
                    	hasToSave = true;
                		item.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CHANGED);
                        item.remove(new Responder(
            				onItemSaved,
	        				onItemFailed
            			));
                    }
                    Model.dailyEvent.removeBillItem(item);
                    i --;
                    continue;
                }

                item.fromTempFields();
                item.BillingDate = DateUtil.format(date);
                item.RelatedAssetAssignment = Model.assignment;
                if (BillItemStatus.BILL_ITEM_STATUS_REJECTED == item.Status) {
                	item.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_CHANGED);
                }
                hasToSave = true;
                item.isSaved = false;
               	item.save(true, new Responder(
            		onItemSaved,
        			onItemFailed
            	));
            }
            if (!hasToSave) {
            	onUpdateFinished(null);
            }
        }
        
        private function onItemSaved(obj:Object):void {
        	BillItem(obj).isSaved = true;
        	BillItem(obj).IsMarkedToRemove = false;
        	
        	for each (var item:BillItem in Model.dailyEvent._billItems) {
        		if (!item.isSaved || item.IsMarkedToRemove) {
        			return;
        		}
        	}
        	Model.dailyEvent.group.updateStatus(new Responder(
        		onUpdateFinished,
        		onItemFailed
        	));
        }
        
        private function onItemFailed(event:FaultEvent):void {
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
   			Model.dailyEvent.updateBillItems(null);
        }

        public function CancelBillItems():void {
            for (var i:int = 0; i < Model.dailyEvent._billItems.length; i ++) {
                var item:BillItem = BillItem(Model.dailyEvent._billItems[i]);
                
                item.cancelNotes();
                
                if (0 == item.BillItemId) {
                    Model.dailyEvent.removeBillItem(item);
                    i --;
                    continue;
                }
                
                item.toTempFields();
                
                if (item.RelatedBillItemTypeTemp.IsAttachRequired
                		&& (0 == item.RelatedBillItemAttachment.length)) {
                	item.IsMarkedToRemove = true;
                }
            }
            
            closeWindow = true;
            cancelling = true;
            
            SaveBillItems();
        }

        public function AddBillItem(): BillItem {
            var billItem:BillItem = new BillItem();
            billItem.RelatedBill = Model.bill;
            billItem.RelatedBillItemType = new BillItemType();
            billItem.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_NEW);
            billItem.BillRate = 0;
            billItem.Qty = 0;
            billItem.BillingDate = DateUtil.format(Model.dailyEvent.date);
            Model.dailyEvent.addBillItem(billItem);
            return billItem;
        }
        
    }

}
