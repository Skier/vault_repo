using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class AssetAssignmentDataObject
    {

        public int AssetAssignmentId;

        public string AFE;

        public string SubAFE;

        public int AssetId;

        public bool Deleted;

        public int ClientId;

        public bool IsClientActive;

        public string AFEStatus;

        public string ProjectStatus;

        public string ClientName;

        public List<RateByAssignmentDataObject> Rates;

    }

}
