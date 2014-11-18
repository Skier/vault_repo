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

namespace Dalworth.Server.MainForm.CustomerEdit
{
    public class InsuranceCompanyEditModel
    {
        #region Company

        private InsuranceCompanyAndAddress m_company;
        public InsuranceCompanyAndAddress Company
        {
            get { return m_company; }
            set { m_company = value; }
        }

        #endregion
    }
}
