using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{
    public class BillSubmitDataObject
    {

        public int BillId;

        public IList<NoteDataObject> Notes;

        public IList<BillItemAttachmentDataObject> Attachments;

        public IList<BillItemDataObject> BillItems;

        public string Status;

    }
}
