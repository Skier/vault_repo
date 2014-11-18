using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.ManageTime.Menu
{
    public class ManageTimeMenuModel
    {
        #region IsSingleTimeTrackingAllowed

        public bool IsSingleTimeTrackingAllowed
        {
            get
            {
                return true;
//                return Employee.FindBy(EntityState.Synchronized).Count > 0
//                       || Vendor.FindBy(EntityState.Synchronized).Count > 0;

            }
        }

        #endregion
    }
}
