using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class BillDataObject
    {

		public const string BILL_STATUS_NEW       = "NEW";
		public const string BILL_STATUS_SUBMITTED = "SUBMITTED";
		public const string BILL_STATUS_REJECTED  = "REJECTED";
		public const string BILL_STATUS_CHANGED   = "CHANGED";
		public const string BILL_STATUS_CORRECTED = "CORRECTED";
		public const string BILL_STATUS_APPROVED  = "APPROVED";
		public const string BILL_STATUS_CONFIRMED = "CONFIRMED";
		public const string BILL_STATUS_DECLINED  = "DECLINED";
		public const string BILL_STATUS_VERIFIED  = "VERIFIED";

        public int BillId;

        public string Status;

        public string StartDate;

        public int AssetId;

        public int TotalDailyBill;

        public decimal DailyBillAmt;

        public decimal OtherBillAmt;

        public decimal TotalBillAmt;

        public IList<BillItemDataObject> BillItems;

        public IList<NoteDataObject> Notes;

        public IList<BillItemCompositionDataObject> Compositions;

        public IList<UserDataObject> Users;

        public AssetDataObject AssetInfo;

        public List<BillItemAttachmentDataObject> Attachments;

        public void Assign(BillDataObject billInfo)
        {
            BillId = billInfo.BillId;
            Status = billInfo.Status;
            StartDate = billInfo.StartDate;
            AssetId = billInfo.AssetId;
            TotalDailyBill = billInfo.TotalDailyBill;
            DailyBillAmt = billInfo.DailyBillAmt;
            OtherBillAmt = billInfo.OtherBillAmt;
            TotalBillAmt = billInfo.TotalBillAmt;
            BillItems = billInfo.BillItems;
            Notes = billInfo.Notes;
            Compositions = billInfo.Compositions;
            Users = billInfo.Users;
            AssetInfo = billInfo.AssetInfo;
        }

    }

}
