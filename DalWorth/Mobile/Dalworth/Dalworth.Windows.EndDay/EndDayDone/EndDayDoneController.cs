using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Windows.EndDay.EndDayDone
{
    public class EndDayDoneController : SingleFormController<EndDayDoneModel, EndDayDoneView>
    {
        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsRightActionExist = true;
            RightActionName = "Done";
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            View.Destroy();
        }

        #endregion
    }
}
