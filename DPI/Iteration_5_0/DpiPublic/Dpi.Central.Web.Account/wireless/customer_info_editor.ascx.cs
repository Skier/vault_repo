using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless
{
    public class CustomerInfoEditor : UserControl
    {
        #region Web Form Designer generated code

        protected TextBox txtFirstName;
        protected TextBox txtAddress1;
        protected TextBox txtLastName;
        protected TextBox txtAddress2;
        protected TextBox txtCity;
        protected TextBox txtEmail;
        protected DropDownList ddlState;
        protected Dpi.Central.Web.Controls.PhoneNumberBox phnContactNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfFirstName;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfLastName;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfEmail;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfAddress1;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfCity;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfState;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfZipCode;
        protected System.Web.UI.WebControls.CustomValidator vldCstContactNumber;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReEmail;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReZipCode;
        protected System.Web.UI.WebControls.TextBox txtZipCode;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vldCstContactNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstContactNumber_ServerValidate);

        }

        #endregion

        #region Event Handlers

        private void vldCstContactNumber_ServerValidate(object source, ServerValidateEventArgs args) 
        {
            if (phnContactNumber.PhoneNumber == string.Empty) {
                args.IsValid = false;
                vldCstContactNumber.ErrorMessage = "<br>Required field cannot be left blank";
            } else {
                args.IsValid = phnContactNumber.IsValid;
                vldCstContactNumber.ErrorMessage = "<br>The Contact Number provided is invalid";
            }
        }

        #endregion

        #region Private Methods

        public void LoadFrom(IWireless_Custdata customerInfo) 
        {
            if (customerInfo == null) {
                throw new ArgumentNullException("customerInfo");
            }

            txtFirstName.Text = customerInfo.NameFirst;
            txtLastName.Text = customerInfo.NameLast;
            phnContactNumber.PhoneNumber = customerInfo.ContactNumber;
            txtAddress1.Text = customerInfo.Addr1;
            txtAddress2.Text = customerInfo.Addr2;
            txtCity.Text = customerInfo.City;
            txtEmail.Text = customerInfo.Email;
            txtZipCode.Text = customerInfo.Zip;
            ddlState.SelectedValue = customerInfo.State;
        }

        public void LoadTo(ref IWireless_Custdata customerInfo)
        {
            if (customerInfo == null) {
                throw new ArgumentNullException("customerInfo");
            }

            customerInfo.NameFirst = txtFirstName.Text;
            customerInfo.NameLast = txtLastName.Text;
            customerInfo.ContactNumber = phnContactNumber.PhoneNumber;
            customerInfo.Addr1 = txtAddress1.Text;
            customerInfo.Addr2 = txtAddress2.Text;
            customerInfo.City = txtCity.Text;
            customerInfo.Email = txtEmail.Text;
            customerInfo.Zip = txtZipCode.Text;
            customerInfo.State = ddlState.SelectedValue;
        }

        #endregion
    }
}