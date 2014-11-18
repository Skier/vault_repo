using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class ProjectDataObject
    {

        public string SubAFE;

        public string AFE;

        public string SubAFEStatus;

        public string ShortName;

        public bool Deleted;

        public bool Temporary;

        public bool IsNew = false;

        public List<AssetAssignmentDataObject> Assignments;

    }

}
