package App.Entity
{
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.NoteDataObject")]
	public class NoteDataObject
	{
		
		public static const NOTE_TYPE_BILL:String            = "BILL";
		public static const NOTE_TYPE_BILL_ITEM:String       = "BILL_ITEM";
		public static const NOTE_TYPE_MULTIDAY_ITEM:String   = "MULTIDAY_ITEM";
		public static const NOTE_TYPE_INVOICE:String         = "INVOICE";
		public static const NOTE_TYPE_INVOICE_ITEM:String    = "INVOICE_ITEM";
		
        public var NoteId:int;

        public var RelatedItemId:int;

        public var ItemType:String;

        public var SenderId:int;

        public var Posted:Date;

        public var NoteText:String;
        
        public var Dummy:int;
        
        public var SenderName:String;
		
	}
}