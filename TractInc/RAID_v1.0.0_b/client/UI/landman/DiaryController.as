package UI.landman
{

    import App.Domain.*;
    import weborb.data.DynamicLoadEvent;
    import mx.collections.ArrayCollection;
    import mx.formatters.DateFormatter;
    import flash.geom.Point;
    import flash.display.DisplayObject;
    import mx.controls.Alert;
    import weborb.data.ActiveCollection;
    import mx.events.CollectionEvent;
    import mx.events.DragEvent;
    import mx.managers.DragManager;
    import mx.controls.DataGrid;
    import mx.collections.ListCollectionView;
    import util.ArrayUtil;
    import util.DateUtil;
    import weborb.data.ActiveCollection;
    import mx.collections.Sort;
    import mx.collections.SortField;
    import common.StatusesRegistry;
    import common.TypesRegistry;
    import mx.events.DynamicEvent;
    import calendar.MonthPeriodGrid;
    import calendar.MonthPeriodCell;
    import mx.core.UIComponent;

    [Bindable]
    public class DiaryController
    {
        private var _afes:Object = new Object();
        private var _subAfes:Object = new Object();

        public var view:DiaryView;
        public var Model:DiaryModel = new DiaryModel();
        
        private var _ratesCounter:int = 0;

        public function DiaryController(view: DiaryView): void {
            this.view = view;
        }

        public function onEventClick(event:DynamicEvent): void {
            var dailyEvent:DailyEvent = DailyEvent(event.data);
            if ("" == event.data.description) {
                return;
            }
            callItemsView(dailyEvent.date, Model.assignmentsHash[dailyEvent.description]);
        }
        
        private function getBillByDate(date:Date):Bill {
            var date1st:Date = new Date(date.fullYear, date.month, 1);
            var date2nd:Date = new Date(date.fullYear, date.month, 16);
            if ((date1st.time <= date.time) && (date2nd.time > date.time)) {
                return Model.billsHash[date1st.time];
            } else {
                return Model.billsHash[date2nd.time];
            }
        }
        
        public function callItemsView(date:Date, assignment:AssetAssignment):void {
            if (null == date) {
                return;
            }
            
            var eventGroup:DailyEventGroup = DailyEventGroup(Model.eventGroups.getItemAt(int(date.date - view.cal.startDate.date)));

            var itemsView:ItemsView = ItemsView.Open(DisplayObject(view), true);
            var itemsModel:ItemsModel = itemsView.Controller.Model;
            itemsView.enabled = false;
            
            itemsModel.bill = getBillByDate(date);
            itemsModel.assignment = assignment;
            
            var event:DailyEvent = eventGroup.getEventByAssignmentId(assignment.AssetAssignmentId);
            if (null == event) {
                event = new DailyEvent(eventGroup, null, assignment);
                var dailyBillItem:BillItem = new BillItem();
                dailyBillItem.RelatedBill = itemsModel.bill;
                dailyBillItem.Qty = 0;
                dailyBillItem.RelatedAssetAssignment = assignment;
                dailyBillItem.BillRate = assignment.ratesHash[BillItemType.BILL_ITEM_TYPE_DAILY_BILLING].BillRate;
                dailyBillItem.RelatedBillItemType = TypesRegistry.getInstance().dailyBillItemType;
                dailyBillItem.RelatedBillItemStatus = StatusesRegistry.getInstance().getBillItemStatusByName(BillItemStatus.BILL_ITEM_STATUS_NEW);
                dailyBillItem.BillingDate = DateUtil.format(date);
                event.addBillItem(dailyBillItem);
                // eventGroup.addEvent(event);
            }
            
            itemsModel.dailyEvent = event;

            var dailyItemArray:Array = new Array();
            dailyItemArray.push(event.dailyBillItem);
            itemsView.dailyBillingGrid.dataProvider = dailyItemArray;
            
            if (event.isEditable() && ((event.billItems.length > 0) || (0 != event.dailyBillItem.Qty))) {
                itemsView.Controller.AddBillItem();
            }
            
            for each (var item:BillItem in event.billItems) {
                item.loadNotes();
            }
            event.dailyBillItem.loadNotes();
            
            itemsView.billItemGrid.dataProvider = event.billItems;
            
            var toFilter:ArrayCollection = new ArrayCollection();
            ArrayUtil.addRange(toFilter, TypesRegistry.getInstance().billItemTypes);
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
            
            var submitView:SubmitView = SubmitView.Open(DisplayObject(view), true);
            var submitModel:SubmitModel = submitView.Controller.Model;
            
            for each (var bill:Bill in Model.billsHash) {
                if ((Date.parse(bill.StartDate) >= maxDate.time) || !bill.isBillEditable() || (BillStatus.BILL_STATUS_REJECTED == bill.Status)) {
                    continue;
                }
                bill.toSubmit = false;
                submitModel.bills.addItem(bill);
            }
            var sort:Sort = new Sort();
            sort.fields = [new SortField("StartDate", true)];
            submitModel.bills.sort = sort;
            submitModel.bills.refresh();
            submitModel.bills.sort = null;
        }
        
        public function onDragEnter(event:DynamicEvent):void {
            if ((event.data.dragSource.hasFormat('items') || event.data.dragSource.hasFormat('format'))
                    && (null != event.data)) {
                DragManager.acceptDragDrop(MonthPeriodCell(event.data.currentTarget));
            }
        }

        public function onDragOver(event:DynamicEvent):void {
            var date:Date = view.cal.getDateUnderMouse();
            if (null != date) {
                if (getBillByDate(date).isBillEditable()) {
                    DragManager.showFeedback(DragManager.COPY);
                    return;
                }
            }
            DragManager.showFeedback(DragManager.NONE);
        }

        public function onDragDrop(evt:DynamicEvent):void {
            var assignment:AssetAssignment;
            var date:Date = evt.data.currentTarget.date;
            
            var group:DailyEventGroup = DailyEventGroup(Model.eventGroups.getItemAt(int(date.date - view.cal.startDate.date)));
            if (8 == group.totalHours) {
                DragManager.showFeedback(DragManager.NONE);
            } else {
	            if (null != evt.data.dragSource.dataForFormat('items')) {
	                assignment = AssetAssignment(evt.data.dragSource.dataForFormat('items')[0]);
	                callItemsView(date, assignment);
	            } else if (null != evt.data.dragSource.dataForFormat('format')) {
	                var event:DailyEvent = DailyEvent(evt.data.dragSource.dataForFormat('format'));
                	
	                if (8 < group.totalHours + event.totalDailyBill) {
	                    DragManager.showFeedback(DragManager.NONE);
	                } else {
	                	if (!event.changeDate(group, date)) {
	                		DragManager.showFeedback(DragManager.NONE);
	                	}
	                }
	            }
            }
        }
        
        public function goToDate(date:Date):void {
            Model.billDate = getStartDate(date);
            LoadBillItems();
        }

        public function Previous():void {
            var date:Date = view.cal.startDate;
            view.cal.startDate = null;
            view.cal.endDate = null;
            if (16 == date.date) {
                view.cal.startDate = new Date(date.fullYear, date.month, 1);
            } else {
                if (0 == date.month) {
                    view.cal.startDate = new Date(date.fullYear - 1, 11, 16);
                } else {
                    view.cal.startDate = new Date(date.fullYear, date.month - 1, 16);
                }
            }
            Model.billDate = view.cal.startDate;
            LoadBillItems();
        }
        
        public function Next(): void {
            var date:Date = view.cal.startDate;
            view.cal.startDate = null;
            view.cal.endDate = null;
            if (1 == date.date) {
                view.cal.startDate = new Date(date.fullYear, date.month, 16);
            } else {
                if (11 == date.month) {
                    view.cal.startDate = new Date(date.fullYear + 1, 1, 1);
                } else {
                    view.cal.startDate = new Date(date.fullYear, date.month + 1, 1);
                }
            }
            Model.billDate = view.cal.startDate;
            LoadBillItems();
        }
        
        public function formatDate(d: Date): String {
            var r: String = d.toDateString();
            return r;
        }

        public function open(asset:Asset, bills:ActiveCollection, date:Date = null): void 
        {
            Model = new DiaryModel();
            view.cal.dataProvider = null;
            
            if (null == date) {
                Model.billDate = new Date();
            } else {
                Model.billDate = date;
            }
            Model.billsHash = new Array();
            Model.asset = asset;
            for each (var bill:Bill in bills) {
                Model.billsHash[Date.parse(bill.StartDate)] = bill;
            }
            
            var afes:ActiveCollection = ActiveRecords.Afe.findBySql(
                "select distinct a.[AFE], a.[ClientId], a.[AFEName], a.[AFEStatus], a.[Deleted]" +
                "  from Afe a inner join AssetAssignment" +
                "       on a.[AFE] = AssetAssignment.[AFE]" +
                " where a.Deleted = 0" +
                "   and AssetAssignment.[AssetId] = " + Model.asset.AssetId);
            afes.addEventListener("loaded", OnAfesLoaded);
        }

        private function OnAfesLoaded(evt:DynamicLoadEvent):void 
        {
            ActiveCollection(evt.data).removeEventListener("loaded", OnAfesLoaded);
            
            for each (var item:Afe in evt.data as ArrayCollection) {
                _afes[item.AFE] = item;
            }
            
            var subAfes:ActiveCollection = ActiveRecords.SubAfe.findBySql(
                "select distinct sa.[SubAFE], sa.[AFE], sa.[SubAFEStatus], sa.[ShortName], sa.[Deleted], sa.[Temporary]" +
                "  from SubAfe sa inner join AssetAssignment" +
                "       on sa.[SubAFE] = AssetAssignment.[SubAFE]" +
                " where sa.Deleted = 0" + 
                "   and AssetAssignment.[AssetId] = " + Model.asset.AssetId);
            
            subAfes.addEventListener("loaded", OnSubAfesLoaded);
        }
        
        private function OnSubAfesLoaded(evt:DynamicLoadEvent):void {
            ActiveCollection(evt.data).removeEventListener("loaded", OnSubAfesLoaded);
            
            for each (var item:SubAfe in evt.data as ArrayCollection) {
                _subAfes[item.SubAFE] = item;
            }
            
            Model.currentAssignments = ActiveRecords.AssetAssignment.findByAssetId(Model.asset.AssetId);
            Model.currentAssignments.addEventListener("loaded", OnCurrentAssignmentLoaded);
        }
        
        private function OnCurrentAssignmentLoaded(evt:DynamicLoadEvent): void {
            ActiveCollection(evt.data).removeEventListener("loaded", OnCurrentAssignmentLoaded);
            
            for each (var assetAssignment:AssetAssignment in Model.currentAssignments) {
                assetAssignment.RelatedAfe = _afes[assetAssignment.AFE];
                assetAssignment.RelatedSubAfe = _subAfes[assetAssignment.SubAFE];
            }
            
            Model.currentAssignmentsFiltered = new ListCollectionView(Model.currentAssignments);
            Model.currentAssignmentsFiltered.filterFunction = assignmentsFilter;
            Model.currentAssignmentsFiltered.refresh();
            
            Model.assignmentsHash = new Array();
            var i:int;
            for (i = 0; i < Model.currentAssignments.length; i++) {
                var assignment:AssetAssignment = AssetAssignment(Model.currentAssignments.getItemAt(i));
                Model.assignmentsHash[assignment.SubAFE] = assignment;
                assignment.ratesHash = new Array();
                var rates:ActiveCollection = ActiveRecords.RateByAssignment.findByAssetAssignmentIdAndDeleted(
                	assignment.AssetAssignmentId, 0);
                if (rates.IsLoaded) {
                    ProcessRates(rates);
                } else {
                	_ratesCounter++;
                    rates.addEventListener("loaded", OnAssignmentRatesLoaded);
                }
            }
        }
        
        private function assignmentsFilter(item:Object):Boolean {
            var result:Boolean = AssetAssignment(item).isEditable();
            return result;
        }
        
        private function OnAssignmentRatesLoaded(evt:DynamicLoadEvent):void {
            var rates:ActiveCollection = ActiveCollection(evt.data);
            rates.removeEventListener("loaded", OnAssignmentRatesLoaded);
            ProcessRates(rates);
            
            _ratesCounter--;
            if (0 == _ratesCounter) {
            	goToDate(Model.billDate);
            }
        }
        
        private function ProcessRates(rates:ActiveCollection):void {
            for each (var rate:RateByAssignment in rates) {
                rate.RelatedAssetAssignment.ratesHash[rate.BillItemTypeId] = rate;
            }
        }
        
        public function LoadBillItems():void {
            view.enabled = false;
            var sql:String =
            "SELECT [BillItemId] " +
            ",[BillItemTypeId] " +
            ",[BillId] " + 
            ",BillItem.[AssetAssignmentId] " +
            ",[BillingDate] " + 
            ",[Qty] " +
            ",[BillRate] " + 
            ",[Status] " +
            ",[Notes] " +
            "FROM BillItem " +
            "inner join AssetAssignment on BillItem.[AssetAssignmentId] = AssetAssignment.[AssetAssignmentId] " +
            "where AssetAssignment.AssetId = " + Model.asset.AssetId +
            " and year(BillItem.BillingDate) = " + String(Model.billDate.fullYear) +
            " and month(BillItem.BillingDate) = " + String(Model.billDate.month + 1) +
            " order by BillItemTypeId asc, BillItem.AssetAssignmentId asc";
            
            Model.currentBillItems = ActiveRecords.BillItem.findBySql(sql);
            if (Model.currentBillItems.IsLoaded) {
                ProcessBillItems();
            } else {
                Model.currentBillItems.addEventListener("loaded", OnBillItemsLoaded);
            }
        }

        private function OnBillItemsLoaded(evt:DynamicLoadEvent): void {
            Model.currentBillItems.removeEventListener("loaded", OnBillItemsLoaded);
            ProcessBillItems();
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

            for (var i:int = 0; i < Model.currentBillItems.length; i++) {
                var item:BillItem = BillItem(Model.currentBillItems.getItemAt(i));
                
                dailyItems = DayBillItems(Model.billItemsByDay[new Date(Date.parse(item.BillingDate)).date]);

                for (var j:int = 0; j < Model.currentAssignments.length; j++) {
                    var assignment:AssetAssignment = AssetAssignment(Model.currentAssignments.getItemAt(j));
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
            view.cal.startDate = Model.billDate;
            Model.bill = getBillByDate(Model.billDate);
            view.cal.endDate = getEndDate(Model.billDate);
            view.isLastMonth = ((new Date()).time >= view.cal.startDate.time) && ((new Date()).time < view.cal.endDate.time);
            view.cal.dataProvider = new ArrayCollection();

            view.calendarPanel.addNotes(Model.bill);
            
            Model.eventGroups.removeAll();
            for (var k: Number = view.cal.startDate.date; k <= view.cal.endDate.date; k ++) {
                var date:Date = new Date(Model.billDate.fullYear, Model.billDate.month, k);
                var eventGroup:DailyEventGroup = new DailyEventGroup(view.cal, date, Model.bill);
                Model.eventGroups.addItem(eventGroup);
                for each (var assignmentDayItems:ArrayCollection in Model.billItemsByDay[k].itemsByAssignment) {
                    if (0 < assignmentDayItems.length) {
                        var event:DailyEvent = new DailyEvent(eventGroup, assignmentDayItems, null);
                    }
                }
            }
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
        
    }
    
}
