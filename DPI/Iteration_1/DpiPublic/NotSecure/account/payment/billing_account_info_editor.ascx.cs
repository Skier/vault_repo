using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class BillingAccountInfoEditor : UserControl
    {
        #region Web Form Designer generated code

        protected TextBox txtBillingFirstName;
        protected TextBox txtBillingLastName;
        protected RequiredFieldValidator vldRfBillingLastName;
        protected TextBox txtBillingAddress;
        protected RequiredFieldValidator vldRfBillingAddress;
        protected TextBox txtBillingCity;
        protected RequiredFieldValidator vldRfBillingCity;
        protected RequiredFieldValidator vldRfBillingState;
        protected TextBox txtBillingZip;
        protected RequiredFieldValidator vldRfBillingZip;
        protected TextBox txtEmailAddress;
        protected RequiredFieldValidator vldRfEmailAddress;
        protected DropDownList ddlBillingState;
        protected System.Web.UI.WebControls.TextBox txtNpa;
        protected System.Web.UI.WebControls.Label lblDefis2;
        protected System.Web.UI.WebControls.TextBox txtNxx;
        protected System.Web.UI.WebControls.Label lblDefis1;
        protected System.Web.UI.WebControls.TextBox txtNumber;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReBillingZip;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReNpa;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReNxx;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReNumber;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReBillingEmail;
        protected RequiredFieldValidator vldRfBillingFirstName;

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
            this.ddlBillingState.DataBinding += new System.EventHandler(this.BillingStateDataBindingHandler);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Fields

        bool _isFilled = false;

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) {
                string state = ddlBillingState.SelectedValue;
                DataBind();
                ddlBillingState.SelectedValue = state;

                // Prepopulation
                if (!_isFilled) {
                    ICustInfoExt2 custInfo = Controller.Instance.CustInfoExt2;

                    if (custInfo.CustInfo != null) {
                        txtBillingFirstName.Text = custInfo.CustInfo.FirstName;
                        txtBillingLastName.Text = custInfo.CustInfo.LastName;
                        txtNpa.Text = custInfo.CustInfo.PhNumber.Substring(0, 3);
                        txtNxx.Text = custInfo.CustInfo.PhNumber.Substring(3, 3);
                        txtNumber.Text = custInfo.CustInfo.PhNumber.Substring(6, 4);
                        txtEmailAddress.Text = custInfo.CustInfo.Email;
                    }

                    if (custInfo.MailAddr != null) {
                        txtBillingAddress.Text = custInfo.MailAddr.FormattedStreetAddress;
                        txtBillingCity.Text = custInfo.MailAddr.City;
                        ddlBillingState.SelectedValue = custInfo.MailAddr.State;
                        txtBillingZip.Text = custInfo.MailAddr.Zipcode;
                    }
                }
            }
        }

        private void BillingStateDataBindingHandler(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            ddl.DataSource = DropDownListDate.GetStatesShort(true);
            ddl.DataTextField = "DDLText";
            ddl.DataValueField = "DDLValue";
        }

        #endregion

        #region Internal Methods

        internal void Update(ref ICustomerRecurringPayment payment, UpdateDirection updateDirection) 
        {
            if (updateDirection == UpdateDirection.FromPage) {
                payment.BillingFirstName = txtBillingFirstName.Text;
                payment.BillingLastName = txtBillingLastName.Text;
                payment.BillingAddress = txtBillingAddress.Text;
                payment.BillingCity = txtBillingCity.Text;
                payment.BillingState = ddlBillingState.SelectedValue;
                payment.BillingZip = txtBillingZip.Text;
                payment.PhNumber = txtNpa.Text + txtNxx.Text + txtNumber.Text;
                payment.EmailAddress = txtEmailAddress.Text;
            } else {
                _isFilled = true;

                txtBillingFirstName.Text = payment.BillingFirstName;
                txtBillingLastName.Text = payment.BillingLastName;
                txtBillingAddress.Text = payment.BillingAddress;
                txtBillingCity.Text = payment.BillingCity;
                ddlBillingState.SelectedValue = payment.BillingState;
                txtBillingZip.Text = payment.BillingZip;
                txtNpa.Text = payment.PhNumber.Substring(0, 3);
                txtNxx.Text = payment.PhNumber.Substring(3, 3);
                txtNumber.Text = payment.PhNumber.Substring(6, 4);
                txtEmailAddress.Text = payment.EmailAddress;
            }
        }

        #endregion
    }
}