using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;

namespace Dalworth.Windows.StartDay.WorkSummary
{
    public class WorkSummaryModel : StartDayBaseModel, IModel
    {
        #region Init

        public void Init()
        {
            StartDayModel.Van = Van.FindByPrimaryKey(StartDayModel.Work.VanId);
        }

        #endregion        
    }
}
