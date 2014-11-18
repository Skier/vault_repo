using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class ServiceAddressPage : BaseAccountSetupPage
    {
        #region Web Form Designer generated code

        protected TextBox txtFirstName;
        protected TextBox txtLastName;
        protected TextBox txtEmail;
        protected TextBox Textbox5;
        protected TextBox Textbox6;
        protected TextBox Textbox7;
        protected DropDownList ddlPhoneComp;
        protected CheckBox chkMailingAddress;
        protected TextBox txtPassword;
        protected TextBox txtConfirmPassword;
        protected RequiredFieldValidator vldRfFirstName;
        protected RequiredFieldValidator vldRfLastName;
        protected RegularExpressionValidator vldReEmail;
        protected CustomValidator vldCstPrevPhone;
        protected CompareValidator vldCmpPassword;
        protected RequiredFieldValidator vldRfPassword;
        protected HtmlGenericControl mailAddressDiv;
        protected RequiredFieldValidator vldRfConfirmPassword;
        protected CustomValidator vldCstSecondContact;
        protected CustomValidator vldCstFirstContact;
        protected RequiredFieldValidator vldRfFirstContact;
        protected PhoneNumberBox phnSecondContact;
        protected PhoneNumberBox phnPrevPhone;
        protected RequiredFieldValidator vldRfEmail;
        protected RegularExpressionValidator vldRePassword;
        protected PhoneNumberBox phnFirstContact;
        protected ImageButton btnPayCreditCard;
        protected ImageButton btnPayCheck;
        protected AddressInfoControl ctrlServiceAddress;
        protected AddressInfoControl ctrlMailAddress;
        protected Label asd;
        protected Label lblZipCode;
        protected System.Web.UI.WebControls.ImageButton btnBack;
        protected Dpi.Central.Web.Controls.DateBox dateBirthday;
        protected CustomValidator vldCstDateBirthday;
        protected ImageButton m_btnOnEnterStub;

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
            this.vldCstDateBirthday.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.OnDateBirthdayValidate);
            this.vldCstFirstContact.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.PhoneNumberServerValidate);
            this.vldCstSecondContact.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.PhoneNumberServerValidate);
            this.vldCstPrevPhone.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.PhoneNumberServerValidate);
            this.btnBack.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
            this.btnPayCheck.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
            this.btnPayCreditCard.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Mode == OperatingMode.Production) {
                lblZipCode.Text = string.Format(ZIP_CODE_FORMAT, Model.Info.Zip, Model.Info.Provider.ILECName);

                LoadProviders();
                LoadCustomerInfo();
                LoadServiceAddressInfo();
                LoadMailAddressInfo();

                ctrlServiceAddress.DisableStateAndCity();
            }

            SetTogglePanelEffect();

            AddressInfoControl.SetSynchronization(ctrlServiceAddress, ctrlMailAddress, chkMailingAddress);

            SetTabOrder();
        }

        private void PhoneNumberServerValidate(object source, ServerValidateEventArgs args)
        {
            PhoneNumberBox phoneNumberBox = (PhoneNumberBox) Page.FindControl(((BaseValidator) source).ControlToValidate);
            args.IsValid = phoneNumberBox.IsValid;
        }

        private void OnDateBirthdayValidate(object source, ServerValidateEventArgs args)
        {
            if (dateBirthday.IsNull) {
                args.IsValid = true;
            } else if (!dateBirthday.IsValid) {
                args.IsValid = false;
            } else if (dateBirthday.Date > DateTime.Now) {
                args.IsValid = false;
            }
        }

        private void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.NEW_ACC_ORDER_SUMMARY_URL);
        }

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            if (!IsValid) {
                return;
            }

            try {
                SaveCustomerInfo();
                SaveServiceAddressInfo();
                SaveMailAddressInfo();

                if (sender == btnPayCreditCard) {
                    Response.Redirect(SiteMap.NEW_ACC_PAY_CREDIT_CARD_URL);
                } else if (sender == btnPayCheck) {
                    Response.Redirect(SiteMap.NEW_ACC_PAY_CHECK_URL);
                } else {
                    throw new ApplicationException("Unknown sender: " + sender.ToString());
                }

            } catch (DeliveryAddressLineParserException ex) {
                SetErrorMessage(ex.Message);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Implementation

        private void SetTogglePanelEffect()
        {
            mailAddressDiv.EnableViewState = false;

            chkMailingAddress.Attributes.Add("onclick", "TogglePanel('" + mailAddressDiv.ClientID + "', 1, '" + chkMailingAddress.ClientID + "');");

            if (!IsPostBack) {
                chkMailingAddress.Checked = Model.Info.IsMailAddressTheSame;
            }

            if (chkMailingAddress.Checked) {
                mailAddressDiv.Style.Add("display", "none");
            }
        }

        private void SetTabOrder()
        {
            txtFirstName.TabIndex = 1;
            txtLastName.TabIndex = (short)(txtFirstName.TabIndex + 1);
            txtEmail.TabIndex = (short)(txtLastName.TabIndex + 1);
            dateBirthday.TabIndex = (short)(txtEmail.TabIndex + 1);
            phnFirstContact.TabIndex = (short)(dateBirthday.TabIndex + 3);
            phnSecondContact.TabIndex = (short)(phnFirstContact.TabIndex + 3);
            phnPrevPhone.TabIndex = (short)(phnSecondContact.TabIndex + 3);
            ddlPhoneComp.TabIndex = (short)(phnPrevPhone.TabIndex + 3);
            chkMailingAddress.TabIndex = (short)(ddlPhoneComp.TabIndex + 1);
            ctrlServiceAddress.SetFirstTabIndex(chkMailingAddress.TabIndex);
            ctrlMailAddress.SetFirstTabIndex(ctrlServiceAddress.GetLastTabIndex());
            txtPassword.TabIndex = (short)(ctrlMailAddress.GetLastTabIndex() + 1);
            txtConfirmPassword.TabIndex = (short)(txtPassword.TabIndex + 1);
            btnPayCreditCard.TabIndex = (short)(txtConfirmPassword.TabIndex + 1);
            btnPayCheck.TabIndex = (short)(btnPayCreditCard.TabIndex + 1);
            btnBack.TabIndex = (short)(btnPayCheck.TabIndex + 1);
        }

        private void LoadProviders()
        {
            IILECInfo[] providers = OrgSvc.GetILECs(Map);

            ddlPhoneComp.DataSource = providers;
            ddlPhoneComp.DataValueField = "ILECCode";
            ddlPhoneComp.DataTextField = "ILECName";

            ddlPhoneComp.DataBind();
        }

        private void LoadCustomerInfo()
        {
            Model.EnsureCustomerInfo();

            ICustInfo ci = Model.Info.CustomerInfo;

            txtFirstName.Text = ci.FirstName;
            txtLastName.Text = ci.LastName;

            if (ci.Birthday == null || ci.Birthday == String.Empty) {
                dateBirthday.IsNull = true;
            } else {
                dateBirthday.Date = DateTime.Parse(ci.Birthday);
            }

            txtEmail.Text = ci.Email;
            phnFirstContact.PhoneNumber = ci.Contact;

            if (ci.Contact2 != null && ci.Contact2.Trim().Length > 0) {
                phnSecondContact.PhoneNumber = ci.Contact2;
            }

            if (ci.PrevPhone != null && ci.PrevPhone.Trim().Length > 0) {
                phnPrevPhone.PhoneNumber = ci.PrevPhone;
            }

            ddlPhoneComp.SelectedValue = ci.PrevILEC;

            txtPassword.Text = Model.Info.WebAccessPassword;
        }

        private void LoadServiceAddressInfo()
        {
            Model.EnsureServiceAddressInfo();
            ctrlServiceAddress.LoadFromObject(Model.Info.ServiceAddress);
        }

        private void LoadMailAddressInfo()
        {
            Model.EnsureMailAddressInfo();
            Model.EnsureServiceAddressInfo();

            Model.Info.MailAddress.State = Model.Info.ServiceAddress.State;
            Model.Info.MailAddress.Zipcode = Model.Info.ServiceAddress.Zipcode;

            ctrlMailAddress.LoadFromObject(Model.Info.MailAddress);
        }

        private void SaveCustomerInfo()
        {
            ICustInfo ci = Model.Info.CustomerInfo;

            ci.FirstName = txtFirstName.Text;
            ci.LastName = txtLastName.Text;
            if (dateBirthday.IsNull) {
                ci.Birthday = string.Empty;
            } else {
                ci.Birthday = dateBirthday.Date.ToString("MM/dd/yyyy");
            }
            ci.Email = txtEmail.Text;
            ci.Contact = phnFirstContact.PhoneNumber;
            ci.Contact2 = phnSecondContact.PhoneNumber;
            ci.PrevPhone = phnPrevPhone.PhoneNumber;
            ci.PrevILEC = ddlPhoneComp.SelectedValue;

            Model.Info.WebAccessPassword = txtPassword.Text.Trim();
            Model.Info.IsMailAddressTheSame = chkMailingAddress.Checked;
        }

        private void SaveServiceAddressInfo()
        {
            IAddr2 sa = Model.Info.ServiceAddress;
            ctrlServiceAddress.SaveIntoObject(ref sa);
            Model.Info.VerbatumServiceAddress = ctrlServiceAddress.GetVerbatumAddress();
        }

        private void SaveMailAddressInfo()
        {
            IAddr2 ma = Model.Info.MailAddress;

            if (chkMailingAddress.Checked) {
                ctrlServiceAddress.SaveIntoObject(ref ma);
                Model.Info.VerbatumMailingAddress = ctrlServiceAddress.GetVerbatumAddress();
            } else {
                ctrlMailAddress.SaveIntoObject(ref ma);
                Model.Info.VerbatumMailingAddress = ctrlMailAddress.GetVerbatumAddress();
            }
        }

        #endregion

        #region Test Stuff

        internal override AccountSetupModel Model
        {
            get
            {
                if (Mode == OperatingMode.Test) {
                    if (Session["AccountSetupModelTest"] == null) {
                        AccountSetupModel model = new OrderSummaryPage.TestModel(Map);
                        model.Info.Zip = "29115";
                        model.Info.Provider = new OrderSummaryPage.TestILECInfo();
                        Session["AccountSetupModelTest"] = model;
                    }

                    return (AccountSetupModel) Session["AccountSetupModelTest"];
                }

                return base.Model;
            }
        }

        #endregion
    }
}