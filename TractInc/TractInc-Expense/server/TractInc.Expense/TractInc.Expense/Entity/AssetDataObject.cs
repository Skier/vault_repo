using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class AssetDataObject
    {

        public int AssetId;

        public string Type;

        public int ChiefAssetId;

        public string BusinessName;

        public string FirstName;

        public string MiddleName;

        public string LastName;

        public string SSN;

        public bool Deleted;

        public List<BillDataObject> Bills;

        public List<DefaultBillRateDataObject> DefaultRates;

        public List<AssetAssignmentDataObject> Assignments;

        public UserDataObject UserInfo;

    }

}
