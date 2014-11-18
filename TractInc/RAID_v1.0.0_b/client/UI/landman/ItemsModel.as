package UI.landman
{

    import mx.collections.ArrayCollection;
    import mx.collections.ListCollectionView;
    import App.Domain.AssetAssignment;
    import App.Domain.BillItem;
    import App.Domain.Bill;

    [Bindable]
    public class ItemsModel
    {
        
        public static const DAILY_BILLING_CONSTS:ArrayCollection = new ArrayCollection(
            [{label: ""}, {label: "1/8"}, {label: "1/4"}, {label: "3/8"}, {label: "1/2"},
             {label: "5/8"}, {label: "3/4"}, {label: "7/8"}, {label: "All"}]);

        public var assignment:AssetAssignment;
        public var billItemTypesView:ListCollectionView;
        public var billItemTypes:ArrayCollection;
        public var bill:Bill;
        public var dailyEvent:DailyEvent;
        
        public var billItemTypesFiltered:ArrayCollection = new ArrayCollection();
        
        public function get dailyBillingCollection():ListCollectionView {
        	var view:ListCollectionView = new ListCollectionView(DAILY_BILLING_CONSTS);
        	view.filterFunction = function(item:Object):Boolean {
        		if (0 != dailyEvent.dailyBillItem.QtyTemp) {
        			return (8 - DAILY_BILLING_CONSTS.getItemIndex(item)) + dailyEvent.dailyBillItem.QtyTemp >= dailyEvent.group.totalHours;
        		} else {
	        		return (8 - DAILY_BILLING_CONSTS.getItemIndex(item)) >= dailyEvent.group.totalHours;
        		}
        	}
        	view.refresh();
        	return view;
        }

    }

}
