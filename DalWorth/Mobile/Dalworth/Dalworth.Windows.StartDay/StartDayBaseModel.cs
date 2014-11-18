using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Windows.StartDay
{
    public class StartDayBaseModel
    {
        #region StartDayModel

        private StartDayModel m_startDayModel;
        public StartDayModel StartDayModel
        {
            get { return m_startDayModel; }
            set { m_startDayModel = value; }
        }

        #endregion
    }
}
