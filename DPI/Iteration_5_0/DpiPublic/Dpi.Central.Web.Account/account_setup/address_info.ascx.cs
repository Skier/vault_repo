using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class AddressInfoControl : UserControl
    {
        #region Static Members

        public static void SetSynchronization(AddressInfoControl src, AddressInfoControl dst, CheckBox trigger)
        {
            SetSynchronizationWithIndicator(src.txtAddress1, dst.txtAddress1, trigger);
            SetSynchronizationWithIndicator(src.txtAddress2, dst.txtAddress2, trigger);
            SetSynchronizationWithIndicator(src.txtCity, dst.txtCity, trigger);
            SetSynchronizationWithIndicator(src.txtZip, dst.txtZip, trigger);

            SetSynchronizationWithTrigger(trigger, src.txtAddress1, dst.txtAddress1);
            SetSynchronizationWithTrigger(trigger, src.txtAddress2, dst.txtAddress2);
            SetSynchronizationWithTrigger(trigger, src.txtCity, dst.txtCity);
            SetSynchronizationWithTrigger(trigger, src.ddlState, dst.ddlState);
            SetSynchronizationWithTrigger(trigger, src.txtZip, dst.txtZip);
        }

        private static void SetSynchronizationWithTrigger(CheckBox trigger, WebControl src, WebControl dst)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(trigger.Attributes["onclick"] != null ? trigger.Attributes["onclick"] + ";" : string.Empty);
            sb.Append("SynchronizeItemsWithTrigger('");
            sb.Append(src.ClientID);
            sb.Append("','");
            sb.Append(dst.ClientID);
            sb.Append("','");
            sb.Append(trigger.ClientID);
            sb.Append("')");

            trigger.Attributes.Add("onclick", sb.ToString());
        }

        private static void SetSynchronizationWithIndicator(WebControl src, WebControl dst, CheckBox indicator)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SynchronizeItemsWithIndicator('");
            sb.Append(src.ClientID);
            sb.Append("','");
            sb.Append(dst.ClientID);
            sb.Append("','");
            sb.Append(indicator.ClientID);
            sb.Append("')");

            src.Attributes.Add("onpropertychange", sb.ToString());
        }

        #endregion

        #region Web Form Designer generated code

        protected RequiredFieldValidator vldStreet;
        protected System.Web.UI.WebControls.TextBox txtCity;
        protected System.Web.UI.WebControls.DropDownList ddlState;
        protected System.Web.UI.WebControls.TextBox txtZip;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfCity;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfState;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfZip;
        protected System.Web.UI.WebControls.TextBox txtAddress1;
        protected System.Web.UI.WebControls.TextBox txtAddress2;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReZip;

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

        }

        #endregion

        #region Public Methods

        public void LoadFromObject(IAddr2 addressInfo) 
        {
            if (addressInfo == null) {
                throw new ArgumentNullException("addressInfo");
            }

            if (!(addressInfo is CustAddress)) {
                throw new ArgumentException("CustAddress object type is required.");
            }

            txtAddress1.Text = ((CustAddress)addressInfo).Address1;
            txtAddress2.Text = ((CustAddress)addressInfo).Address2;

            txtCity.Text = addressInfo.City;
            ddlState.SelectedValue = addressInfo.State;
            txtZip.Text = addressInfo.Zipcode;
        }

        public void SaveIntoObject(ref IAddr2 addressInfo)
        {
            if (addressInfo == null) {
                throw new ArgumentNullException("addressInfo");
            }

            if (!(addressInfo is CustAddress)) {
                throw new ArgumentException("CustAddress object type is required.");
            }

            ((CustAddress)addressInfo).FormattedStreetAddress = txtAddress1.Text + " " + txtAddress2.Text;
            
            addressInfo.City = txtCity.Text;
            addressInfo.State = ddlState.SelectedValue;
            addressInfo.Zipcode = txtZip.Text;
        }

        public string GetVerbatumAddress()
        {
            return txtAddress1.Text + " " + txtAddress2.Text;
        }

        public void DisableStateAndCity()
        {
            ddlState.Enabled = txtZip.Enabled = false;
        }

        public void SetFirstTabIndex(short firstTabIndex)
        {
            txtAddress1.TabIndex = firstTabIndex;
            txtAddress2.TabIndex = (short)(firstTabIndex + 1);
            txtCity.TabIndex = (short)(firstTabIndex + 2);
            ddlState.TabIndex = (short)(firstTabIndex + 3);
            txtZip.TabIndex = (short)(firstTabIndex + 4);
        }

        public short GetLastTabIndex()
        {
            return txtZip.TabIndex;
        }

        #endregion
    }
}