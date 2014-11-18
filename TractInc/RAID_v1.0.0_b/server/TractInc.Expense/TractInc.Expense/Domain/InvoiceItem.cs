namespace TractInc.Expense.Domain
{
    public partial class InvoiceItem
    {
        
        public InvoiceItem(BillItem billItem)
        {
            BillItemId = billItem.BillItemId;
            InvoiceItemTypeId = billItem.BillItemTypeId;
            InvoiceDate = billItem.BillingDate;
            AssetAssignmentId = billItem.AssetAssignmentId;
            Qty = billItem.Qty;
            Status = "NEW";
            IsSelected = true;
            
            RateByAssignmentDataMapper rateDM = new RateByAssignmentDataMapper();
            RateByAssignment rate = rateDM.getRateByBillItemId(billItem.BillItemId);
            
            if (rate == null){
                InvoiceRate = billItem.BillRate;
            } else {
                InvoiceRate = rate.InvoiceRate;
            }
            
        }
    
    }
}
        