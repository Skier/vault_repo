using System;
using System.Collections.Generic;
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

namespace Dalworth.Server.MainForm.MonitoringReadingEdit
{
    public class MonitoringReadingEditModel : IModel
    {
        #region ExistingReading

        private MonitoringReading m_existingReading;
        public MonitoringReading ExistingReading
        {
            get { return m_existingReading; }
            set { m_existingReading = value; }
        }

        #endregion

        #region IsEditable

        private bool m_isEditable;
        public bool IsEditable
        {
            get { return m_isEditable; }
            set { m_isEditable = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            
        }

        #endregion
    }
}
