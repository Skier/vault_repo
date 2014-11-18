using System;
using System.Web.UI;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account
{
    public class Tabs : UserControl
    {
        #region Web Form Designer generated code

        protected TabControl _tabControl;

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

            if (path.IndexOf(new Uri(SiteMap.ACCOUNT_SUMMARY_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 0;
            } else if (path.IndexOf(new Uri(SiteMap.ACCOUNT_SETTINGS_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 1;
            } else if (path.IndexOf(new Uri(SiteMap.ORDER_STATUS_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 2;
            } else if (path.IndexOf(new Uri(SiteMap.PAYMENT_SELECTION_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 3;
            } else if (path.IndexOf(new Uri(SiteMap.CC_PAYMENT_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 3;
            } else if (path.IndexOf(new Uri(SiteMap.CHECK_PAYMENT_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 3;
            } else if (path.IndexOf(new Uri(SiteMap.PROMISE_TO_PAY_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 4;
            } else if (path.IndexOf(new Uri(SiteMap.REC_PAYMENTS_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 5;
            } else if (path.IndexOf(new Uri(SiteMap.REC_CC_PAYMENT_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 5;
            } else if (path.IndexOf(new Uri(SiteMap.REC_CHECK_PAYMENT_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 5;
            } else if (path.IndexOf(new Uri(SiteMap.REC_DEACTIVATE_CONFIRMATION_URL).AbsolutePath) != - 1) {
                _tabControl.SelectedIndex = 5;
            } 
        }

        private void _tabControl_SelectedIndexChanged(object sender, System.EventArgs e) 
        {
            Tab tab = _tabControl.Tabs[_tabControl.SelectedIndex];
            Response.Redirect(tab.Tag);
        }

        #endregion
    }
}