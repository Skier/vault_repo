using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Payment
{
    public class CreditCardInfoControl : UserControl
    {
        private const string SELECT_CARD_TYPE_SCRIPT = @"<SCRIPT language=javascript type=text/javascript>
        function selectCardType(radioButtonId) 
	    {{
		    document.getElementById('{0}').checked = radioButtonId == '{0}';
		    document.getElementById('{1}').checked = radioButtonId == '{1}';
		    document.getElementById('{2}').checked = radioButtonId == '{2}';
		    document.getElementById('{3}').checked = radioButtonId == '{3}';
    		
		    objItem = document.getElementById('{4}');
    		
		    if(objItem == null)
		    {{
			    return;
		    }}
    		
		    objItem.disabled = radioButtonId == '{3}';
	    }}
        </SCRIPT>";

        private const string SELECT_CARD_TYPE_ONCLICK_SCRIPT = @"if (!document.getElementById('{0}').disabled) selectCardType('{0}');";

        #region Web Form Designer generated code

        protected System.Web.UI.WebControls.CustomValidator vldCstExpDate;
        protected System.Web.UI.WebControls.CustomValidator vldCstCreditCardType;
        protected System.Web.UI.WebControls.RadioButton rbVisa;
        protected System.Web.UI.WebControls.RadioButton rbMasterCard;
        protected System.Web.UI.WebControls.RadioButton rbAmericanExpress;
        protected System.Web.UI.WebControls.RadioButton rbDiscover;
        protected System.Web.UI.WebControls.TextBox txtCcNumber;
        protected System.Web.UI.WebControls.DropDownList lstExpMonth;
        protected System.Web.UI.WebControls.DropDownList lstExpYear;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfCcNumber;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReCcNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfCvNumber;
        protected System.Web.UI.HtmlControls.HtmlImage imgVisa;
        protected System.Web.UI.HtmlControls.HtmlImage imgMasterCard;
        protected System.Web.UI.HtmlControls.HtmlImage imgAmericanExpress;
        protected System.Web.UI.HtmlControls.HtmlImage imgDiscover;
        protected System.Web.UI.WebControls.CustomValidator vldCstCcNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfExpMonth;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfExpYear;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkToggleDetails;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReCvNumber;
        protected System.Web.UI.WebControls.TextBox txtCvNumber;

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
            this.vldCstCreditCardType.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstCreditCardType_ServerValidate);
            this.vldCstCcNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstCcNumber_ServerValidate);
            this.vldCstExpDate.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstExpDate_ServerValidate);
            this.Load += new System.EventHandler(this.Page_Load);
            this.Init += new System.EventHandler(this.Page_Init);

        }

        #endregion

        private void Page_Init(object sender, System.EventArgs e) 
        {
            string script = string.Format(SELECT_CARD_TYPE_SCRIPT, rbVisa.ClientID, rbMasterCard.ClientID, rbAmericanExpress.ClientID, rbDiscover.ClientID, txtCvNumber.ClientID);
            Page.RegisterClientScriptBlock("SELECT_CARD_TYPE_SCRIPT_KEY", script);

            rbVisa.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbVisa.ClientID));
            rbMasterCard.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbMasterCard.ClientID));
            rbAmericanExpress.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbAmericanExpress.ClientID));
            rbDiscover.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbDiscover.ClientID));

            imgVisa.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbVisa.ClientID));
            imgMasterCard.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbMasterCard.ClientID));
            imgAmericanExpress.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbAmericanExpress.ClientID));
            imgDiscover.Attributes.Add("onclick", string.Format(SELECT_CARD_TYPE_ONCLICK_SCRIPT, rbDiscover.ClientID));
        }

        public void InitExpirationDateControls()
        {
            IDropDownListItem[] monthItems = DropDownListDate.GetCCMonths(true);
            
            lstExpMonth.DataSource = monthItems;
            lstExpMonth.DataValueField = "DDLValue";
            lstExpMonth.DataTextField = "DDLText";
            
            lstExpMonth.DataBind();

            IDropDownListItem[] yearItems = DropDownListDate.GetYears(DateTime.Now.Year, 20, true);

            lstExpYear.DataSource = yearItems;
            lstExpYear.DataValueField = "DDLValue";
            lstExpYear.DataTextField = "DDLText";
            
            lstExpYear.DataBind();
        }

        private void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) {
                InitExpirationDateControls();

                txtCvNumber.MaxLength = CreditCard.CV_NUMBER_MAX_LENGTH;
            }
        }

        private void vldCstCreditCardType_ServerValidate(object source, ServerValidateEventArgs args) 
        {
            args.IsValid = IsCreditCardTypeSelected;
        }

        private void vldCstExpDate_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            if (lstExpMonth.SelectedValue == string.Empty || lstExpYear.SelectedValue == string.Empty) {
                return;
            }

            int m = int.Parse(lstExpMonth.SelectedValue);
            int y = int.Parse(lstExpYear.SelectedValue);

            DateTime exp = new DateTime(y, m, 1);
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (exp < now) {
                args.IsValid = false;
            }
        }

        private void vldCstCcNumber_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            if (lstExpMonth.SelectedValue == string.Empty || lstExpYear.SelectedValue == string.Empty) {
                return;
            }

            try {
                if (IsCreditCardTypeSelected) {
                    new CreditCard(CardType, CcNumber, CvNumber, ExpMonth, ExpYear, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                } else {
                    args.IsValid = false;
                }
            } catch (CreditCardValidationException) {
                args.IsValid = false;
            } catch (ArgumentException) {
                args.IsValid = false;
            }
        }

        #region Properties

        public short FirstTabIndex
        {
            set
            {
                rbVisa.TabIndex = value++;
                rbMasterCard.TabIndex = value++;
                rbAmericanExpress.TabIndex = value++;
                rbDiscover.TabIndex = value++;
                txtCcNumber.TabIndex = value++;
                lstExpMonth.TabIndex = value++;
                lstExpYear.TabIndex = value++;
                txtCvNumber.TabIndex = value++;
            }
        }

        public short LastTabIndex 
        {
            get { return txtCvNumber.TabIndex; }
        }

        public bool Enabled
        {
            set
            {
                rbVisa.Enabled = rbMasterCard.Enabled = rbAmericanExpress.Enabled = rbDiscover.Enabled = value;
                txtCcNumber.Enabled = txtCvNumber.Enabled = value;
                lstExpMonth.Enabled = lstExpYear.Enabled = value;
                EnabledValidators = value;

                if (!value) {
                    txtCvNumber.Text = new string('*', CvNumber.Length);
                }
            }
        }

        public bool EnabledValidators
        {
            set { vldReCvNumber.Enabled = vldCstCcNumber.Enabled = vldCstCreditCardType.Enabled = vldCstExpDate.Enabled = vldReCcNumber.Enabled = vldRfCcNumber.Enabled = vldRfCvNumber.Enabled = vldRfExpMonth.Enabled = vldRfExpYear.Enabled = value; }
        }

        public CreditCardType CardType 
        {
            get
            {
                if (rbVisa.Checked) {
                    return CreditCardType.VISA;
                } else if (rbMasterCard.Checked) {
                    return CreditCardType.MasterCard;
                } else if (rbAmericanExpress.Checked) {
                    return CreditCardType.AmericanExpress;
                } else if (rbDiscover.Checked) {
                    return CreditCardType.DiscoverCard;
                } else {
                    throw new ApplicationException("Credit Card Type is not selected.");
                }
            }

            set
            {
                rbVisa.Checked = value == CreditCardType.VISA;
                rbMasterCard.Checked = value == CreditCardType.MasterCard;
                rbAmericanExpress.Checked = value == CreditCardType.AmericanExpress;
                rbDiscover.Checked = value == CreditCardType.DiscoverCard;
            }
        }

        public string CcNumber
        {
            get { return txtCcNumber.Text; }
            set { txtCcNumber.Text = value; }
        }

        public int ExpMonth
        {
            get { return Int32.Parse(lstExpMonth.SelectedValue); }
            set { lstExpMonth.SelectedValue = value.ToString(); }
        }

        public int ExpYear 
        {
            get { return Int32.Parse(lstExpYear.SelectedValue); }
            set { lstExpYear.SelectedValue = value.ToString(); }
        }

        public string CvNumber
        {
            get { return txtCvNumber.Text; }
            set { txtCvNumber.Text = value; }
        }

        private bool IsCreditCardTypeSelected
        {
            get { return rbVisa.Checked || rbMasterCard.Checked || rbAmericanExpress.Checked || rbDiscover.Checked; }
        }

        public HtmlControl ToggleDetailsControl 
        {
            get { return lnkToggleDetails; }
        }

        #endregion
    }
}
