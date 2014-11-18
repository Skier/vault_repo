using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class BillItemTypeDataObject
    {

        public int BillItemTypeId;

        public int InvoiceItemTypeId;

        public string Name;

        public bool IsCountable;

        public bool IsPresetRate;

        public bool IsSingle;

        public bool IsAttachRequired;

        public bool Deleted;

    }

}

