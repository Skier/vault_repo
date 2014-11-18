using System;
using System.Web.UI;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Wireless
{
    public class CustomerInfoPage : BaseAccountPage
    {
        #region Fields

        private IWireless_Custdata _customerData;

        #endregion

        #region Web Form Designer generated code

        protected Panel customerInfo;
        protected CustomerInfoViewer ctrlCustomerInfoViewer;
        protected System.Web.UI.WebControls.ImageButton btnChange;
        protected System.Web.UI.WebControls.ImageButton btnUpdate;
        protected System.Web.UI.WebControls.ImageButton btnCancel;
        protected CustomerInfoEditor ctrlCustomerInfoEditor;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnChange.Click += new System.Web.UI.ImageClickEventHandler(this.CustomerInfo_Change);
            this.btnUpdate.Click += new System.Web.UI.ImageClickEventHandler(this.CustomerInfo_Update);
            this.btnCancel.Click += new System.Web.UI.ImageClickEventHandler(this.CustomerInfo_Cancel);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region EventHandlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                _customerData = LoadCustomerData();
                ToggleToCustomerInfoViewer(_customerData);
            }
        }

        public void CustomerInfo_Change(object sender, ImageClickEventArgs e)
        {
            _customerData = LoadCustomerData();
            ToggleToCustomerInfoEditor(_customerData);
        }

        public void CustomerInfo_Update(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            CustSvc.UpdateWirelessCustData(Map, GetAccountNumber(), new CustSvc.UpdateWirelessCustDataCallback(this.UpdateWirelessCustomerDataCallBack));
            ToggleToCustomerInfoViewer(_customerData);
        }

        public void CustomerInfo_Cancel(object sender, ImageClickEventArgs e)
        {
            _customerData = LoadCustomerData();
            ToggleToCustomerInfoViewer(_customerData);
        }

        #endregion

        #region Private Methods

        private IWireless_Custdata LoadCustomerData()
        {
            return CustSvc.GetWirelessCustData(Map, GetAccountNumber());
        }

        private void ToggleToCustomerInfoViewer(IWireless_Custdata customerData)
        {
            ctrlCustomerInfoViewer.LoadFrom(customerData);

            ctrlCustomerInfoViewer.Visible = btnChange.Visible = true;
            ctrlCustomerInfoEditor.Visible = btnUpdate.Visible = btnCancel.Visible = false;
        }

        private void ToggleToCustomerInfoEditor(IWireless_Custdata customerData)
        {
            ctrlCustomerInfoEditor.LoadFrom(customerData);

            ctrlCustomerInfoViewer.Visible = btnChange.Visible = false;
            ctrlCustomerInfoEditor.Visible = btnUpdate.Visible = btnCancel.Visible = true;
        }

        private void UpdateWirelessCustomerDataCallBack(IWireless_Custdata customerData) 
        {
            _customerData = customerData;
            ctrlCustomerInfoEditor.LoadTo(ref _customerData);
        }

        #endregion
    }
}