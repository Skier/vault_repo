using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class BillItemCompositionDataObject
    {

        public int BillItemCompositionId;

        public int BillId;

        public int BillItemTypeId;

        public decimal Amount;

        public string Description;

        public bool IsDeleted;

        public List<BillItemDataObject> BillItems;

        public BillDataObject BillInfo;

        public BillItemAttachmentDataObject AttachmentInfo;

        public List<NoteDataObject> Notes;

    }

}
