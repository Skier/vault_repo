using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.MainForm
{
    public class MainFormModel
    {
        #region CurrentDispatch

        private Employee m_currentDispatch;
        public Employee CurrentDispatch
        {
            get { return m_currentDispatch; }
            set { m_currentDispatch = value; }
        }

        #endregion
    }
}
