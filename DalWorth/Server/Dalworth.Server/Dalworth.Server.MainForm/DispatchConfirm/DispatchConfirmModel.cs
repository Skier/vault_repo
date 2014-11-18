using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.DispatchConfirm
{
    public class DispatchConfirmModel
    {
        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region DispatchTime

        private DateTime m_dispatchTime;
        public DateTime DispatchTime
        {
            get { return m_dispatchTime; }
            set { m_dispatchTime = value; }
        }

        #endregion

        #region GetConfirmationReasons

        public string GetConfirmationReasons()
        {
            string result = string.Empty;

            if (m_visit.ConfirmedFrameBegin.HasValue
                && m_visit.ConfirmedFrameEnd.HasValue
                && (m_dispatchTime.AddMinutes(30) < m_visit.ConfirmedFrameBegin.Value
                || m_dispatchTime.AddMinutes(30) > m_visit.ConfirmedFrameEnd.Value))
            {
                result += "  * Estimated arrival time is out of confirmed time frame\n";
            }

            if (m_visit.ConfirmLeftMessage)
                result += "  * Left a message on last confirmation\n";

            if (m_visit.IsCallOnYourWay)
                result += "  * Customer asked to call on the way\n";

            return result;
        }

        #endregion
    }
}
