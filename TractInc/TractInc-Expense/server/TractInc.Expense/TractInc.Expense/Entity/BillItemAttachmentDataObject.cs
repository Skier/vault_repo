using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class BillItemAttachmentDataObject
    {

        public int BillItemAttachmentId;

        public int BillItemId;

        public string FileName;

        public string OriginalFileName;

        public BillItemDataObject BillItemInfo;

        public BillItemCompositionDataObject CompositionInfo;

        public bool IsDeleted;

    }

}
