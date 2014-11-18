package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.InvoiceDataObject")]
	public class InvoiceDataObject
	{
		
        public var InvoiceId:int;

        public var InvoiceNumber:String;

        public var ClientId:int;

        public var ClientName:String;

        public var ClientAddress:String;

        public var ClientActive:Boolean;

        public var Status:String;

        public var StartDate:String;

        public var TotalDailyAmt:int;

        public var DailyInvoiceAmt:Number;

        public var OtherInvoiceAmt:Number;

        public var TotalInvoiceAmt:Number;

        public var InvoiceItems:Array;

        public var BillItems:Array;
        
        public var isNew:Boolean;
        
        public var Landman:String;
        
        public var Assignments:Array;

	}
	
}
