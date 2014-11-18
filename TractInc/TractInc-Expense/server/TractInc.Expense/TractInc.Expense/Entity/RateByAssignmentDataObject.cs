using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class RateByAssignmentDataObject
    {

        public int RateByAssignmentId;

        public int AssetAssignmentId;

        public int BillItemTypeId;

        public decimal BillRate;

        public decimal InvoiceRate;

        public bool ShouldNotExceedRate;

        public bool Deleted;

        public int Dummy;

    }

}
