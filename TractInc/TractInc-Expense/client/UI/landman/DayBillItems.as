package UI.landman
{

    import mx.collections.ArrayCollection;
    import App.Entity.BillItemTypeDataObject;
    import App.Entity.BillItemDataObject;

    public class DayBillItems
    {

        public var dayBillTotal:int = 0;
        public var itemsByAssignment:Array;

        public function recalcTotal():void {
            dayBillTotal = 0;
            for each (var items:ArrayCollection in itemsByAssignment) {
                for each (var item:BillItemDataObject in items) {
                    if (BillItemTypeDataObject.BILL_ITEM_TYPE_DAILY_BILLING == item.BillItemTypeId) {
                        dayBillTotal += item.Qty;
                        break;
                    }
                }
            }
        }

    }

}
