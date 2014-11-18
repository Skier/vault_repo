using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class NoteDataObject
    {

        public const string BILL_ITEM_NOTE_TYPE     = "BILL_ITEM";
        public const string BILL_NOTE_TYPE          = "BILL";
		public const string NOTE_TYPE_MULTIDAY_ITEM = "MULTIDAY_ITEM";
		public const string NOTE_TYPE_INVOICE       = "INVOICE";
		public const string NOTE_TYPE_INVOICE_ITEM  = "INVOICE_ITEM";
        
        public int NoteId;

        public int RelatedItemId;

        public string ItemType;

        public int SenderId;

        public DateTime Posted;

        public string NoteText;

        public int Dummy;

        public string SenderName;

    }

}
