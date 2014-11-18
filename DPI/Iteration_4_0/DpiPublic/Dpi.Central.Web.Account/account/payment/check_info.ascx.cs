using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.Components;

namespace Dpi.Central.Web.Account.Payment
{
    public class CheckInfoControl : UserControl
    {
        #region Web Form Designer generated code

        protected TextBox txtBankAccountNumber;
        protected RequiredFieldValidator vldRfBankAccountNumber;
        protected RegularExpressionValidator vldReBankAccountNumber;
        protected TextBox txtBankRouteNumber;
        protected RequiredFieldValidator vldRfBankRouteNumber;
        protected RegularExpressionValidator vldReBankRouteNumber;
        protected RequiredFieldValidator vldRfDrvLicState;
        protected TextBox txtDrvLicNumber;
        protected DropDownList lstDrvLicState;
        protected System.Web.UI.WebControls.CustomValidator vldCstBankRouteNumber;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkToggleDetails;
        protected RequiredFieldValidator vldRfDrvLicNumber;

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
            this.vldCstBankRouteNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstBankRouteNumber_ServerValidate);

        }

        #endregion

        #region Event Handlers

        private void vldCstBankRouteNumber_ServerValidate(object source, ServerValidateEventArgs args) 
        {
            if (!vldReBankRouteNumber.IsValid) {
                return;
            }

            BankCheck check = new BankCheck(BankRouteNumber, BankAccountNumber, DriverLicenseNumber, DriverLicenseState, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            StringCollection errors = check.Validate();
            args.IsValid = errors.Count == 0;
        }

        #endregion

        #region Properties

        public short FirstTabIndex
        {
            set
            {
                txtBankAccountNumber.TabIndex = value++;
                txtBankRouteNumber.TabIndex = value++;
                lstDrvLicState.TabIndex = value++;
                txtDrvLicNumber.TabIndex = value++;
            }
        }

        public short LastTabIndex
        {
            get { return txtDrvLicNumber.TabIndex; }
        }

        public bool Enabled
        {
            set
            {
                txtBankAccountNumber.Enabled = txtBankRouteNumber.Enabled = lstDrvLicState.Enabled = txtDrvLicNumber.Enabled = value;
                vldCstBankRouteNumber.Enabled = vldRfBankAccountNumber.Enabled = vldReBankAccountNumber.Enabled = vldRfBankRouteNumber.Enabled = vldReBankRouteNumber.Enabled = vldRfDrvLicState.Enabled = vldRfDrvLicNumber.Enabled = value;

                if (!value) {
                    lstDrvLicState.SelectedIndex = 0;
                    txtDrvLicNumber.Text = new string('*', DriverLicenseNumber.Length);
                }
            }
        }

        public string BankAccountNumber
        {
            get { return txtBankAccountNumber.Text; }
            set { txtBankAccountNumber.Text = value; }
        }

        public string BankRouteNumber 
        {
            get { return txtBankRouteNumber.Text; }
            set { txtBankRouteNumber.Text = value; }
        }

        public string DriverLicenseState 
        {
            get { return lstDrvLicState.SelectedValue; }
            set { lstDrvLicState.SelectedValue = value; }
        }

        public string DriverLicenseNumber
        {
            get { return txtDrvLicNumber.Text; }
            set { txtDrvLicNumber.Text = value; }
        }

        public HtmlControl ToggleDetailsControl
        {
            get { return lnkToggleDetails; }
        }

        #endregion
    }
}