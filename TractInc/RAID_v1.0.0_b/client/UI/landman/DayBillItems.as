package UI.landman
{

    import mx.collections.ArrayCollection;
    import App.Domain.BillItem;
    import App.Domain.BillItemType;

    public class DayBillItems
    {

        public var dayBillTotal:int = 0;
        public var itemsByAssignment:Array;

        public function recalcTotal():void {
            dayBillTotal = 0;
            for each (var items:ArrayCollection in itemsByAssignment) {
                for each (var item:BillItem in items) {
                    if (BillItemType.BILL_ITEM_TYPE_DAILY_BILLING == item.BillItemTypeId) {
                        dayBillTotal += item.Qty;
                        break; // There are only one daily billing record for one assignment
                    }
                }
            }
        }

    }

}
