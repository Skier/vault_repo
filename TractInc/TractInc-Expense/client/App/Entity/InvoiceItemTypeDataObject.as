package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.InvoiceItemTypeDataObject")]
	public class InvoiceItemTypeDataObject
	{
		
        public static const BILL_ITEM_TYPE_DAILY_BILLING:int	= 1;
        public static const BILL_ITEM_TYPE_MILES:int			= 2;
        public static const BILL_ITEM_TYPE_LODGING:int			= 3;
        public static const BILL_ITEM_TYPE_MEALS:int			= 4;
        public static const BILL_ITEM_TYPE_PHONE:int			= 5;
        public static const BILL_ITEM_TYPE_COPIES:int			= 6;
        public static const BILL_ITEM_TYPE_FILING_FEES:int		= 7;
        public static const BILL_ITEM_TYPE_TRAVEL:int			= 8;
        public static const BILL_ITEM_TYPE_POSTAGE:int			= 9;
        public static const BILL_ITEM_TYPE_FAX:int				= 10;
        public static const BILL_ITEM_TYPE_ABSTRACTOR:int		= 11;
        public static const BILL_ITEM_TYPE_NOTARY:int			= 12;
        public static const BILL_ITEM_TYPE_PHOTO:int			= 13;
        public static const BILL_ITEM_TYPE_OTHER:int			= 14;
        public static const BILL_ITEM_TYPE_OTHER_FEES:int		= 15;
        
        public var BillItemTypeId:int;

        public var InvoiceItemTypeId:int;

        public var Name:String;

        public var IsCountable:Boolean;

        public var IsPresetRate:Boolean;

        public var IsSingle:Boolean;

        public var IsAttachRequired:Boolean;

        public var Deleted:Boolean;

	}
	
}
