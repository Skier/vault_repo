using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class BillItemDataObject
    {

		public const string BILL_ITEM_STATUS_NEW       = "NEW";
		public const string BILL_ITEM_STATUS_SUBMITTED = "SUBMITTED";
		public const string BILL_ITEM_STATUS_REJECTED  = "REJECTED";
		public const string BILL_ITEM_STATUS_CHANGED   = "CHANGED";
		public const string BILL_ITEM_STATUS_CORRECTED = "CORRECTED";
		public const string BILL_ITEM_STATUS_APPROVED  = "APPROVED";
		public const string BILL_ITEM_STATUS_CONFIRMED = "CONFIRMED";
		public const string BILL_ITEM_STATUS_DECLINED  = "DECLINED";
		public const string BILL_ITEM_STATUS_VERIFIED  = "VERIFIED";

        public int BillItemId;

        public int BillItemTypeId;

        public int BillId;

        public int AssetAssignmentId;

        public string BillingDate;

        public int Qty;

        public decimal BillRate;

        public string Status;

        public int BillItemCompositionId;

        public WorkLogDataObject WorkLogInfo = null;

        public BillItemAttachmentDataObject AttachmentInfo = null;

        public List<NoteDataObject> Notes;

        public bool IsMarkedToRemove;

        public int Dummy;

    }

}
