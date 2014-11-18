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
    public class CustomerEditModel
    {
        #region Customer

        private CustomerAndAddress m_customer;
        public CustomerAndAddress Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region QbCustomer

        private QbCustomer m_qbCustomer;
        public QbCustomer QbCustomer
        {
            get { return m_qbCustomer; }
            set { m_qbCustomer = value; }
        }

        #endregion
    }
}
