using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.MonitoringReadings
{
    public class MonitoringReadingsModel : IModel
    {
        #region DefloodTask

        private Task m_defloodTask;
        public Task DefloodTask
        {
            get { return m_defloodTask; }
            set { m_defloodTask = value; }
        }

        #endregion

        #region Readings

        private BindingList<ReadingHistory> m_readings;
        public BindingList<ReadingHistory> Readings
        {
            get { return m_readings; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_readings = new BindingList<ReadingHistory>(ReadingHistory.Find(m_defloodTask));
        }

        #endregion
    }
}
