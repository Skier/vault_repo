using System;
using System.Web.UI;
using Dpi.Central.Web.Controls;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Wireless
{
    public class Tabs : UserControl
    {
        #region Web Form Designer generated code

        protected Dpi.Central.Web.Controls.TabControl _tabControl;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tabControl.SelectedIndexChanged += new System.EventHandler(this._tabControl_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            string path = Request.Url.AbsolutePath;

            if (path.IndexOf(new Uri(SiteMap.WRLS_CUSTOMER_INFO_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 1;
            } else {
                _tabControl.SelectedIndex = 0;
            }

            if (!IsPostBack) {
                IWireless_Custdata customerData = CustSvc.GetWirelessCustData(AccountPage.Map, AccountPage.GetAccountNumber());

                _tabControl.NoteLine1 = "Hello, " + NameFormatter.Format(customerData.NameFirst, customerData.NameLast);
                _tabControl.NoteLine2 = PhoneNumberFormatter.Format(customerData.PhNumber);
            }
        }

        private void _tabControl_SelectedIndexChanged(object sender, System.EventArgs e) 
        {
            Tab tab = _tabControl.Tabs[_tabControl.SelectedIndex];
            Response.Redirect(tab.Tag);
        }

        #endregion

        #region Properties

        private BaseAccountPage AccountPage
        {
            get { return (BaseAccountPage)base.Page; }
        }

        #endregion
    }
}