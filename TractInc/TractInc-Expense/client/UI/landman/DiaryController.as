package UI.landman
{

    import mx.collections.ArrayCollection;
    import mx.formatters.DateFormatter;
    import flash.geom.Point;
    import flash.display.DisplayObject;
    import mx.controls.Alert;
    import mx.events.CollectionEvent;
    import mx.events.DragEvent;
    import mx.managers.DragManager;
    import mx.controls.DataGrid;
    import mx.collections.ListCollectionView;
    import util.ArrayUtil;
    import util.DateUtil;
    import mx.collections.Sort;
    import mx.collections.SortField;
    import common.StatusesRegistry;
    import common.TypesRegistry;
    import mx.events.DynamicEvent;
    import calendar.Calendar;
    import mx.core.UIComponent;
    import flash.events.MouseEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.events.CloseEvent;
    import mx.binding.utils.ChangeWatcher;
    import App.Entity.BillDataObject;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import App.Entity.BillItemDataObject;
    import App.Service.LandmanService;
    import App.Entity.BillItemCompositionDataObject;
    import App.Entity.UserDataObject;
    import App.Entity.NoteDataObject;
    import App.Entity.LandmanDataObject;
    import App.Entity.AssetAssignmentDataObject;
    import App.Entity.AFEDataObject;
    import App.Entity.ProjectDataObject;
    import App.Entity.RateByAssignmentDataObject;
    import App.Entity.DictionariesDataObject;
    import App.Entity.AssetDataObject;
    import App.Entity.BillItemTypeDataObject;
    import App.Entity.BillItemStatusDataObject;
    import App.Entity.ClientDataObject;

    [Bindable]
    public class DiaryController
    {
    	
       	public var parentController:ExpenseController;
        
        private var _afes:Object = new Object();
        private var _subAfes:Object = new Object();

        public var view:DiaryView;
        public var Model:DiaryModel = new DiaryModel();
        
        private var _ratesCounter:int = 0;

        public function DiaryController(view: DiaryView, pc:ExpenseController): void {
            this.view = view;
            this.parentController = pc;
        }
        
        private function getBillByDate(date:Date):BillDataObject {
            var date1st:Date = new Date(date.fullYear, date.month, 1);
            var date2nd:Date = new Date(date.fullYear, date.month, 16);
            if ((date1st.time <= date.time) && (date2nd.time > date.time)) {
                return BillDataObject(Model.billsHash[date1st.time]);
            } else {
                return BillDataObject(Model.billsHash[date2nd.time]);
            }
        }
        
        public function callItemsView(date:Date, assignment:AssetAssignmentDataObject):void {
            if (null == date) {
                return;
            }
            
            var eventGroup:DailyEventGroup = DailyEventGroup(Model.eventGroups.getItemAt(int(date.date - view.cal.startDate.date)));
            eventGroup.composition = Composition(view.dgCompositeItems.selectedItem);

            var itemsView:ItemsView = ItemsView.Open(DisplayObject(view), true);
            var itemsModel:ItemsModel = itemsView.Controller.Model;
            itemsView.enabled = false;
            
            itemsModel.bill = Model.bill;
            itemsModel.assignment = assignment;
            
            var event:DailyEvent = eventGroup.getEventByAssignmentId(assignment.AssetAssignmentId);
            if (null == event) {
                event = new DailyEvent(eventGroup, null, assignment);
                var dailyBillItem:BillItemDataObject = new BillItemDataObject();
                dailyBillItem.BillId = itemsModel.bill.BillId;
                dailyBillItem.Qty = 0;
                
                dailyBillItem.AssetAssignmentId = assignment.AssetAssignmentId;
                dailyBillItem.BillRate = assignment.ratesHash[BillItemTypeDataObject.BILL_ITEM_TYPE_DAILY_BILLING].BillRate;
                dailyBillItem.BillItemTypeId = TypesRegistry.instance.dailyBillItemType.BillItemTypeId;
                dailyBillItem.Status = BillItemStatusDataObject.BILL_ITEM_STATUS_NEW;
                dailyBillItem.BillingDate = DateUtil.format(date);
                
                event.addBillItem(dailyBillItem);
            }
            
            itemsModel.dailyEvent = event;
            ChangeWatcher.watch(event.dailyBillItem, ['QtyTemp'], itemsView.Controller.dailyQtyChanged);

            var dailyItemArray:Array = new Array();
            dailyItemArray.push(event.dailyBillItem);
            itemsView.dailyBillingGrid.dataProvider = dailyItemArray;
            
            if (event.isEditable && ((event.billItems.length > 0) || (0 != event.dailyBillItem.Qty))) {
            	itemsView.Controller.canSave = true;
                itemsView.Controller.AddBillItem();
            }
            
            itemsView.billItemGrid.dataProvider = event.billItems;
            
            var toFilter:ArrayCollection = new ArrayCollection();
            ArrayUtil.addRange(toFilter, TypesRegistry.instance.billItemTypes);
            itemsModel.billItemTypesView = new ListCollectionView(toFilter);
            itemsView.Controller.processBillItemTypes();
            
            itemsView.Controller.BillingDate = date;

            itemsView.Controller.recalcTotalAmount();
            itemsView.enabled = true;
        }
        
        public function callSubmitView():void {
            var date:Date = new Date();
            var maxDate:Date;
            if (16 > date.date) {
                maxDate = new Date(date.fullYear, date.month, 16);
            } else {
                if (11 == date.month) {
                    maxDate = new Date(date.fullYear + 1, 0, 1);
                } else {
                    maxDate = new Date(date.fullYear, date.month + 1, 1);
                }
            }
            
            var submitView:SubmitView = SubmitView.Open(view, true);
            var submitModel:SubmitModel = submitView.Controller.Model;
            
            for each (var bill:BillDataObject in Model.billsHash) {
                if ((Date.parse(bill.StartDate) >= maxDate.time) || !bill.isBillEditable()) {
                    continue;
                }
                bill.toSubmit = false;
                submitModel.bills.addItem(bill);
            }
            var sort:Sort = new Sort();
            sort.fields = [new SortField("StartDate", true, true)];
            submitModel.bills.sort = sort;
            submitModel.bills.refresh();
            submitModel.bills.sort = null;
        }
        
        public function goToDate(date:Date):void {
            Model.billDate = getStartDate(date);
            LoadBillItems();
        }

        public function formatDate(d: Date):String {
            var r:String = d.toDateString();
            return r;
        }

        public function open(asset:AssetDataObject, landmanData:LandmanDataObject, mainModel:AppModel, date:Date = null): void
        {
            Model = new DiaryModel();
            Model.bills = new ArrayCollection(landmanData.Bills);
            Model.mainModel = mainModel;
            Model.landmanData = landmanData;
            
            if (null == date) {
                Model.billDate = new Date();
            } else {
                Model.billDate = date;
            }
            Model.billsHash = new Array();
            Model.asset = asset;
            
            for each (var bill:BillDataObject in Model.bills) {
                Model.billsHash[Date.parse(bill.StartDate)] = bill;
            }
            
            for each (var assignment:AssetAssignmentDataObject in landmanData.AssetAssignments) {
            	if (null == assignment.ratesHash) {
            		assignment.ratesHash = new Array();
            	}
            	
            	for each (var rate:RateByAssignmentDataObject in assignment.Rates) {
            		if (rate.Deleted) {
            			continue;
            		}
            		
            		assignment.ratesHash[rate.BillItemTypeId] = rate;
            	}
            	
            	assignment.afe = AFEDataObject(mainModel.afesHash[assignment.AFE]);
            	assignment.project = ProjectDataObject(mainModel.projectsHash[assignment.SubAFE]);
            	
            	_afes[assignment.AFE] = assignment.afe;
                _subAfes[assignment.SubAFE] = assignment.project;
            }
            
            Model.assignmentsHash = new Array();
            Model.assignmentsByIdHash = new Array();
            
            Model.currentAssignments = new ArrayCollection(landmanData.AssetAssignments);

            for each (var assetAssignment:AssetAssignmentDataObject in Model.currentAssignments) {
                Model.assignmentsHash[assetAssignment.SubAFE] = assetAssignment;
                Model.assignmentsByIdHash[assetAssignment.AssetAssignmentId] = assetAssignment;
            }
            
            Model.currentAssignmentsFiltered = new ListCollectionView(Model.currentAssignments);
            Model.currentAssignmentsFiltered.filterFunction = assignmentsFilter;
            Model.currentAssignmentsFiltered.refresh();
            
       		goToDate(Model.billDate);
        }
        
        private function assignmentsFilter(item:Object):Boolean {
        	var assignment:AssetAssignmentDataObject = AssetAssignmentDataObject(item);
            return assignment.isEditable()
            	&& ((null == view.cbClientFilter.selectedItem)
            		|| (ClientDataObject(view.cbClientFilter.selectedItem).ClientId) == assignment.ClientId);
        }
        
        public function LoadBillItems():void {
        	view.enabled = false;
           	view.tnBill.selectedChild = view.boxBillInfo;
			view.cal.startDate = Model.billDate;
			
            Model.currentBills = new ArrayCollection();

            Model.processedBills = new ArrayCollection();
            
            for each (var bill:BillDataObject in Model.bills) {
            	var date:Date = new Date();
            	var startDate:Date = new Date(Date.parse(bill.StartDate));
            	if (date.time >= startDate.time) {
	            	if (bill.isBillEditable()) {
        	    		Model.currentBills.addItem(bill);
            		} else {
            			Model.processedBills.addItem(bill);
            		}
            	}
            }
            
       		LandmanService.getInstance().getBill(BillDataObject(Model.billsHash[Model.billDate.time]).BillId,
       			new Responder(
       				function(result:ResultEvent):void {
       					if (null == result.result) {
       						Alert.show("Bill not found. Contact the administrator.", "Application error");
       						return;
       					}
       					
            			Model.bill = getBillByDate(Model.billDate);
            			
       					if (Model.bill == null) {
       						Model.bill = BillDataObject(Model.billsHash[Model.billDate.time]);
       					}
       						
   						Model.bill.assign(BillDataObject(result.result));
   						
   						if (null == BillDataObject(result.result).BillItems) {
   							Model.bill.BillItems = new Array();
   						} else {
	   						Model.bill.BillItems = BillDataObject(result.result).BillItems;
	   					}
	   					
	   					if (null == BillDataObject(result.result).Notes) {
	   						Model.bill.Notes = new Array();
	   					} else {
       						Model.bill.Notes = BillDataObject(result.result).Notes;
       					}
       					
       					if (null == BillDataObject(result.result).Compositions) {
       						Model.bill.Compositions = new Array();
       					} else {
       						Model.bill.Compositions = BillDataObject(result.result).Compositions;
       					}
       					
       					if (Model.currentBills.contains(Model.bill)) {
       						view.tnBills.selectedChild = view.vbCurrentBills;
       					} else {
       						view.tnBills.selectedChild = view.vbProcessedBills;
       					}
       				
       					Model.currentBillItems = new ArrayCollection(Model.bill.BillItems);
            			ProcessBillItems();
            			
            			view.panelNotes.rpt.dataProvider = Model.bill.Notes;
            			
            			view.onAllClientsFilterClick();
       				},
       				function(fault:FaultEvent):void {
       					Alert.show("Failed to load bill items. Contact the administrator.", "Application error");
       				}
       			)
       		);
        }

        private function ProcessBillItems():void {
            Model.billItemsByDay = new Array();
            var day: Number;
            var dailyItems:DayBillItems;
            for (day = 1; day <= 31; day ++) {
                dailyItems = new DayBillItems();
                dailyItems.itemsByAssignment = new Array();
                Model.billItemsByDay[day] = dailyItems;
            }
            
			var item:BillItemDataObject;
            for (var i:int = 0; i < Model.currentBillItems.length; i++) {
                item = BillItemDataObject(Model.currentBillItems.getItemAt(i));
                dailyItems = DayBillItems(Model.billItemsByDay[new Date(Date.parse(item.BillingDate)).date]);

                for (var j:int = 0; j < Model.currentAssignments.length; j++) {
                    var assignment:AssetAssignmentDataObject = AssetAssignmentDataObject(Model.currentAssignments.getItemAt(j));
                    if (item.AssetAssignmentId == assignment.AssetAssignmentId) {
                        if (null == dailyItems.itemsByAssignment[assignment.AssetAssignmentId]) {
                            dailyItems.itemsByAssignment[assignment.AssetAssignmentId] = new ArrayCollection();
                        }
                        dailyItems.itemsByAssignment[assignment.AssetAssignmentId].addItem(item);
                        break;
                    }
                }

                item.toTempFields();
                item.IsMarkedToRemove = false;
            }
            
            updateEvents();
        }
        
        public function updateEvents():void {
            Model.isReadOnly = !Model.bill.isBillEditable();

            if (Model.currentBills.contains(Model.bill)) {
            	view.dgBills.selectedIndex = Model.currentBills.getItemIndex(Model.bill);
            } else if (Model.processedBills.contains(Model.bill)){
            	view.dgProcessedBills.selectedIndex = Model.processedBills.getItemIndex(Model.bill);
            }
            
            view.isLastMonth = ((new Date()).time >= view.cal.startDate.time) && ((new Date()).time < view.cal.endDate.time);

            Model.eventGroups.removeAll();
            for (var k:Number = view.cal.startDate.date; k <= view.cal.endDate.date; k ++) {
                var date:Date = new Date(Model.billDate.fullYear, Model.billDate.month, k);
                var eventGroup:DailyEventGroup = new DailyEventGroup(view.cal, date, Model.bill);
                Model.eventGroups.addItem(eventGroup);
                for each (var assignmentDayItems:ArrayCollection in Model.billItemsByDay[k].itemsByAssignment) {
                    if (0 < assignmentDayItems.length) {
                        var event:DailyEvent = new DailyEvent(
                        	eventGroup,
                        	assignmentDayItems,
                        	AssetAssignmentDataObject(Model.assignmentsByIdHash[BillItemDataObject(assignmentDayItems[0]).AssetAssignmentId]));
                    }
                }
            }
            
        	Model.clients = new ArrayCollection(Model.landmanData.Clients);
        	
        	Model.types = TypesRegistry.instance.billItemTypes;
			processCompositions();
        	view.enabled = true;
        }
        
        public function getStartDate(date:Date):Date {
            if (16 <= date.date) {
                return new Date(date.fullYear, date.month, 16);
            } else {
                return new Date(date.fullYear, date.month, 1);
            }
        }
        
        private function getEndDate(startDate:Date):Date {
            if (1 == startDate.date) {
                return new Date(startDate.fullYear, startDate.month, 15);
            } else if (16 == startDate.date) {
                var endDate:Date;
                try {
                    endDate = new Date(startDate.fullYear, startDate.month + 1, 1);
                } catch (ex:Error) {
                    endDate = new Date(startDate.fullYear + 1, 0, 1);
                }
                return new Date(endDate.time - 1);
            } else {
                throw new Error("Internal error. Please contact administrator.");
            }
        }
        
        public function openCreateComposition():void {
        	var compositionView:CompositionView = CompositionView.Open(view, true);
        	var compositionModel:CompositionModel = compositionView.Controller.model;
        	compositionModel.types = TypesRegistry.instance.billItemTypes;
        }
        
        public function openEditComposition():void {
        	var compositionView:CompositionView = CompositionView.Open(view, true);
        	var compositionModel:CompositionModel = compositionView.Controller.model;
        	compositionView.tiAmount.text = Composition(view.dgCompositeItems.selectedItem).compositeRecord.Amount.toString();
        	compositionView.tiDescription.text = Composition(view.dgCompositeItems.selectedItem).compositeRecord.Description;
        	
        	compositionModel.types = TypesRegistry.instance.billItemTypes;
        	compositionView.cbItemTypes.selectedIndex = Composition(view.dgCompositeItems.selectedItem).compositeRecord.BillItemTypeId - 2;
        	
        	compositionModel.composition = Composition(view.dgCompositeItems.selectedItem);
        }
        
        private function processCompositions():void {
        	Model.compositions = new ArrayCollection();
       		
            var billItemStatusByCompositionId:Array = new Array();

			var item:BillItemDataObject;
            for (var i:int = 0; i < Model.currentBillItems.length; i++) {
                item = BillItemDataObject(Model.currentBillItems.getItemAt(i));
                if (0 == item.BillItemCompositionId) {
                	continue;
                }
                
                if (null == billItemStatusByCompositionId[item.BillItemCompositionId]) {
                	billItemStatusByCompositionId[item.BillItemCompositionId] = item.Status;
                } else if (BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED == item.Status) {
                	billItemStatusByCompositionId[item.BillItemCompositionId] = item.Status;
                }
            }
                
			for each (var compositionRecord:BillItemCompositionDataObject in Model.bill.Compositions) {
       			var composition:Composition = new Composition();
       			composition.compositeRecord = compositionRecord;
       			
       			Model.compositions.addItem(composition);
       			
	            for each (item in Model.currentBillItems) {
    	        	if ((0 != item.BillItemCompositionId)
    	        			&& (item.BillItemCompositionId == compositionRecord.BillItemCompositionId)) {
    	        		if (BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED == billItemStatusByCompositionId[item.BillItemCompositionId]) {
		       				composition.isRejected = true;
            				item.Status = BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED;
            				item.StatusTemp = BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED;
    	        		} else if (BillItemStatusDataObject.BILL_ITEM_STATUS_NEW != billItemStatusByCompositionId[item.BillItemCompositionId]) {
		       				composition.isEditable = false;
    	        		}
	            	}
    	        }
    	        
        		composition.isLoading = true;
        		if (0 == composition.days.length) {
	   	    		for each (var day:DailyEventGroup in Model.eventGroups) {
      					composition.addItem(day);
	   	    		}
        		}
        	
        		composition.restoreTemporaryItems();
        		composition.isLoading = false;
       		}
			
       		buildCalendar();
        }

        public function buildCalendar():void {
        	var composition:Composition = Composition(view.dgCompositeItems.selectedItem);

       		if (null != Model.composition) {
	    		Model.composition.storeTemporaryItems();
       		}
        	
        	if (null == composition) {
        		Model.composition = null;
        		return;
        	} else {
        		Model.composition = composition;
        	}
        	
        	Model.composition.isLoading = true;
        	if (0 == Model.composition.days.length) {
	   	    	for each (var day:DailyEventGroup in Model.eventGroups) {
      				Model.composition.addItem(day);
	   	    	}
        	}
        	
        	Model.composition.restoreTemporaryItems();
        	Model.composition.isLoading = false;
        }
		
        public function proceedComposition(composition:Composition, responder:Responder = null):void {
        	view.enabled = false;
        	var itemsCount:int = composition.itemsCount;
        	
        	composition.storeTemporaryItems();
        	
        	composition.compositeRecord.BillItems = new Array();
			for each (var project:DailyEvent in composition.temporaryItems) {
       			var item:BillItemDataObject = new BillItemDataObject();
       			item.AssetAssignmentId = project.assignment.AssetAssignmentId;
       			item.BillId = composition.compositeRecord.BillId;
       			item.BillingDate = DateUtil.format(project.group.date);
       			item.BillItemCompositionId = composition.compositeRecord.BillItemCompositionId;
       			item.BillItemTypeId = composition.compositeRecord.BillItemTypeId;
       			item.BillRate = composition.amount / itemsCount;
       			item.Qty = 1;
       			item.Status = BillItemDataObject.BILL_ITEM_STATUS_NEW;
       			
       			composition.compositeRecord.BillItems.push(item);
			}
			
        	LandmanService.getInstance().storeComposition(composition.compositeRecord, new Responder(
        		function(result:ResultEvent):void {
        			var updatedComposition:BillItemCompositionDataObject = BillItemCompositionDataObject(result.result);

        			for each (var day:DailyEventGroup in composition.days) {
        				for each (var project:DailyEvent in day.events) {
        					for each (var oldItem:BillItemDataObject in project.billItems) {
        						if (oldItem.BillItemCompositionId == composition.compositeRecord.BillItemCompositionId) {
        							project.removeBillItem(oldItem);
        						}
        					}
        					
        					if (composition.temporaryItems.contains(project)) {
        						for each (var newItem:BillItemDataObject in updatedComposition.BillItems) {
        							if ((newItem.BillingDate == project.dailyBillItem.BillingDate)
        									&& (newItem.AssetAssignmentId == project.assignment.AssetAssignmentId)) {
        								project.addBillItem(newItem);
        							}
        						}
        					}
        				}
        			}
        			
        			composition.isProceeded = true;
					composition.isChanged = false;
					composition.isRejected = false;
        			composition.compositeRecord = updatedComposition;
        			composition.restoreTemporaryItems();
        			
        			Model.bill.assign(updatedComposition.BillInfo);
        			
        			if (null == responder) {
        				view.enabled = true;
        			} else {
        				responder.result(result);
        			}
        		},
        		function(fault:FaultEvent):void {
        			if (null == responder) {
        				view.enabled = true;
        				Alert.show("Cannot save composite item", "System Error");
        			} else {
        				responder.fault(fault);
        			}
        		}
        	));
        }
        
        public function proceedCompositions(onSuccess:Function = null):void {
        	var compositionsCount:int = 0;
        	var hasAnyProcessed:Boolean = false;
        	
        	for each (var compositionInfo:Composition in Model.compositions) {
        		if (compositionInfo.isProceeded) {
        			continue;
        		}
        		
        		if (0 == compositionInfo.temporaryItems.length) {
        			Model.compositions.removeItemAt(Model.compositions.getItemIndex(compositionInfo));
        			continue;
        		}
        		
        		view.enabled = false;
        		hasAnyProcessed = true;
        		
        		compositionsCount++;
        		
        		proceedComposition(compositionInfo, new Responder(
        			function(result:ResultEvent):void {
        				compositionsCount--;
        				
        				if (0 == compositionsCount) {
        					view.enabled = true;
        					
        					if (null != onSuccess) {
        						onSuccess();
        					}
        				}
        			},
        			function(fault:FaultEvent):void {
        				view.enabled = true;
        				Alert.show("Cannot save composite items", "System Error");
        			}
        		));
        	}
        	
        	if (!hasAnyProcessed && (null != onSuccess)) {
       			onSuccess();
        	}
        }
        
        public function removeCompositeItem(confirm:Boolean = true, responder:Responder = null):void {
        	if (confirm) {
	            Alert.show("Are you really want to delete selected composite item?", "Delete",
				    Alert.YES | Alert.NO , null, 
        	    	function (event:CloseEvent):void {
				        if (event.detail == Alert.YES) {
			    	    	processRemoveCompositeItem(responder);
            			}
	            	}, null, Alert.NO
    	   		);
    	   	} else {
    	   		processRemoveCompositeItem(responder);
    	   	}
        }
        
        public function processRemoveCompositeItem(responder:Responder = null):void {
			var composition:Composition = Composition(view.dgCompositeItems.selectedItem);
			composition.storeTemporaryItems();
			
			if (0 != composition.compositeRecord.BillItemCompositionId) {
	        	view.enabled = false;
				for each (var day:DailyEventGroup in composition.days) {
					for each (var project:DailyEvent in day.events) {
						for each (var item:BillItemDataObject in project.billItems) {
							if (composition.compositeRecord.BillItemCompositionId == item.BillItemCompositionId) {
								project.removeBillItem(item);
							}
						}
					}
					day.updateTotals();
				}
				
				LandmanService.getInstance().removeComposition(composition.compositeRecord, new Responder(
					function(result:ResultEvent):void {
						Model.bill.assign(BillDataObject(result.result));
						view.enabled = true;
						
						if (null != responder) {
							responder.result(result);
						}
					},
					function(fault:FaultEvent):void {
						view.enabled = true;
						if (null != responder) {
							responder.fault(fault);
						}
					}
				));
			}
			
			Model.composition = null;
		    Model.compositions.removeItemAt(view.dgCompositeItems.selectedIndex);
		    
		    buildCalendar();
        }
        
        public function removeProject(project:DailyEvent):void {
            Alert.show("Are you really want to delete selected project from this day?", "Delete",
			    Alert.YES | Alert.NO , null, 
            	function (event:CloseEvent):void {
			        if (event.detail == Alert.YES) {
			        	processRemoveProject(project);
            		}
            	}, null, Alert.NO
       		);
        }
        
        private function processRemoveProject(project:DailyEvent):void {
        	for each (var item:BillItemDataObject in project._billItems) {
        		item.IsMarkedToRemove = true;
        	}
        	project.saveItems(new Responder(
            	function(result:ResultEvent):void {
            		view.enabled = true;
            	},
            	function(fault:FaultEvent):void {
            		view.enabled = true;
            		Alert.show("Cannot remove project.", "System Error");
            	}
            ));
        }
        
        public function changeProject(event:DailyEvent, assignment:AssetAssignmentDataObject, onClose:Function):void {
        	event.assignment = assignment;
        	for each (var item:BillItemDataObject in event._billItems) {
        		var itemType:BillItemTypeDataObject = TypesRegistry.instance.getBillItemTypeById(item.BillItemTypeId);
        		if (itemType.IsPresetRate) {
        			item.BillRate = RateByAssignmentDataObject(assignment.ratesHash[item.BillItemTypeId]).BillRate;
        		}
        		
        		item.isSaved = false;
        	}
           	event.saveItems(new Responder(
           		function(result:*):void {
           			onClose();
           		},
           		function(fault:FaultEvent):void {
           			onClose();
           		}
           	));
        }
		
    }
    
}
