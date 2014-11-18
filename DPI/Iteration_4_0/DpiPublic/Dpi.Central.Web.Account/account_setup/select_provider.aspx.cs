using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class SelectProvider : BaseAccountSetupPage
    {
        #region Const

        private const string MANAGE_STATE_SCRIPT = @"
        <script language='javascript'>
            function EnableDisablePhoneField (){    
                if ( document.all['m_rbnMovePhoneYes'].checked){            
                    document.all['m_rowPhoneFields'].style.display='block';
                } else {
                    document.all['m_rowPhoneFields'].style.display='none'; 
                }                
            }

            function EnableDisableLowIncomeLink (){    
                if ( document.all['m_rbnLowIncomeYes'].checked){            
                    document.all['m_rowLowIncomeLink'].style.display='block';
                } else {
                    document.all['m_rowLowIncomeLink'].style.display='none'; 
                }
                CheckButtonsVisibility();

            }
        </script>";

        #endregion

        #region Web Form Designer generated code

        protected DropDownList m_cmbProviders;
        protected CustomValidator m_vldZip;
        protected RadioButton m_rbnMovePhoneYes;
        protected RadioButton m_rbnMovePhoneNo;
        protected RadioButton m_rbnLowIncomeYes;
        protected RadioButton m_rbnLowIncomeNo;
        protected TextBox m_txtZip;
        protected Footer _footer;
        protected ImageButton m_btnNext;
        protected CustomValidator m_vldProvider;
        protected HtmlInputHidden m_hdnError;
        protected HtmlInputHidden m_hdnProviderTexts;
        protected HtmlInputHidden m_hdnProviderValues;
        protected HtmlInputHidden m_hdnProviderSelectedIndex;
        protected HtmlInputHidden m_hdnIsShowWirelessString;
        protected Label m_lblZipError;
        protected TableRow m_rowPhoneFields;
        protected Label m_lblCaption;
        protected TableRow m_rowProviderField;
        protected TableRow m_rowLowIncomeLink;
        protected Label m_lblLowIncomeLink;
        protected Label m_lblWirelessProducts;
        protected TableRow m_rowLowIncome;
        protected Label m_lblDoYouQualify;
        protected HtmlInputHidden m_hdnIsLowIncomeRowVisible;
        protected TableRow m_rowButtons;
        protected PhoneNumberBox phnPhoneNumber;
        protected CustomValidator vldCstPhoneNumber;

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
            this.vldCstPhoneNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.OnPhoneValidate);
            this.m_vldZip.ServerValidate += new ServerValidateEventHandler(this.OnZipValidate);
            this.m_vldProvider.ServerValidate += new ServerValidateEventHandler(this.OnProviderValidate);
            this.m_btnNext.Click += new ImageClickEventHandler(this.OnNextClick);
            this.Load += new EventHandler(this.OnPageLoad);

        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e)
        {
            RegisterClientScriptBlock("MANAGE_STATE_SCRIPT", MANAGE_STATE_SCRIPT);

            m_rbnMovePhoneYes.Attributes.Add("onclick", "EnableDisablePhoneField();");
            m_rbnMovePhoneNo.Attributes.Add("onclick", "EnableDisablePhoneField();");

            m_rbnLowIncomeYes.Attributes.Add("onclick", "EnableDisableLowIncomeLink();");
            m_rbnLowIncomeNo.Attributes.Add("onclick", "EnableDisableLowIncomeLink();");

            if (!IsPostBack) {
                if (Model.Info.IsDoNotResetModelOnFirstStep) {
                    Model.Info.IsDoNotResetModelOnFirstStep = false;
                } else {
                    ResetModel();
                }

                if (Model.Info.IsMoveExistingPhoneNull) {
                    m_rbnMovePhoneYes.Checked = false;
                    m_rbnMovePhoneNo.Checked = false;
                } else {
                    if (Model.Info.IsMoveExistingPhone) {
                        m_rbnMovePhoneYes.Checked = true;
                        m_rbnMovePhoneNo.Checked = false;
                    } else {
                        m_rbnMovePhoneYes.Checked = false;
                        m_rbnMovePhoneNo.Checked = true;
                    }
                }

                // TODO: move this fields with PhoneNumber value.
                phnPhoneNumber.AreaCode = Model.Info.PhoneFirst3;
                phnPhoneNumber.Prefix = Model.Info.PhoneSecond3;
                phnPhoneNumber.LineNumber = Model.Info.PhoneLast4;

                if (Model.Info.IsQualifyForLowIncomeNull) {
                    m_rbnLowIncomeYes.Checked = false;
                    m_rbnLowIncomeNo.Checked = false;
                } else {
                    if (Model.Info.IsQualifyForLowIncome) {
                        m_rbnLowIncomeYes.Checked = true;
                        m_rbnLowIncomeNo.Checked = false;
                    } else {
                        m_rbnLowIncomeYes.Checked = false;
                        m_rbnLowIncomeNo.Checked = true;
                    }
                }

                m_txtZip.Text = Model.Info.Zip;

                if (Model.Info.Zip != null && Model.Info.Zip != string.Empty) {
                    IILECInfo[] providers = Model.GetProviders(Model.Info.Zip);
                    foreach (IILECInfo provider in providers) {
                        m_cmbProviders.Items.Add(new ListItem(provider.ILECName, provider.OrgId.ToString()));
                        m_hdnProviderValues.Value += provider.OrgId.ToString() + ":";
                        m_hdnProviderTexts.Value += provider.ILECName + ":";
                    }

                    if (Model.Info.Provider != null) {
                        for (int i = 0; i < m_cmbProviders.Items.Count; i++) {
                            if (m_cmbProviders.Items[i].Value == Model.Info.Provider.OrgId.ToString()) {
                                m_cmbProviders.SelectedIndex = i;
                                m_hdnProviderSelectedIndex.Value = i.ToString();
                                break;
                            }
                        }
                    }
                }

                m_lblWirelessProducts.Style["display"] = "none";

                if (Model.Info.IsQualifyForLowIncomeNull) {
                    m_rowLowIncome.Style["display"] = "none";
                    m_rowLowIncomeLink.Style["display"] = "none";
                    m_hdnIsLowIncomeRowVisible.Value = "false";
                } else {
                    m_rowLowIncome.Style["display"] = "block";
                    m_hdnIsLowIncomeRowVisible.Value = "true";
                    if (Model.Info.IsQualifyForLowIncome) {
                        m_rowLowIncomeLink.Style["display"] = "block";
                    } else {
                        m_rowLowIncomeLink.Style["display"] = "none";
                    }
                }

            } else {
                //Handle ajax ViewState          
                m_cmbProviders.Items.Clear();
                if (m_hdnProviderValues.Value != string.Empty) {
                    string[] prodiverValues = m_hdnProviderValues.Value.Split(':');
                    string[] prodiverTexts = m_hdnProviderTexts.Value.Split(':');

                    for (int i = 0; i < prodiverValues.Length; i++) {
                        if (prodiverValues[i] != string.Empty) {
                            m_cmbProviders.Items.Add(new ListItem(prodiverTexts[i], prodiverValues[i]));
                        }
                    }
                }

                if (m_hdnProviderSelectedIndex.Value != string.Empty) {
                    m_cmbProviders.SelectedIndex = int.Parse(m_hdnProviderSelectedIndex.Value);
                }

                if (m_hdnError.Value != string.Empty) {
                    m_lblZipError.Text = m_hdnError.Value;
                } else {
                    m_lblZipError.Text = string.Empty;
                }

                if (m_hdnIsShowWirelessString.Value == "true") {
                    m_lblWirelessProducts.Style["display"] = "block";
                } else {
                    m_lblWirelessProducts.Style["display"] = "none";
                }

                if (m_hdnIsLowIncomeRowVisible.Value == "true") {
                    m_rowLowIncome.Style["display"] = "block";
                    if (m_rbnLowIncomeYes.Checked) {
                        m_rowLowIncomeLink.Style["display"] = "block";
                    } else {
                        m_rowLowIncomeLink.Style["display"] = "none";
                    }
                } else {
                    m_rowLowIncome.Style["display"] = "none";
                    m_rowLowIncomeLink.Style["display"] = "none";
                }

            }

            m_lblWirelessProducts.Text
                = string.Format("Check out our <a href=\"{0}\">wireless products</a>", SiteMap.PUBLIC_WIRELESS_PRODUCTS_URL);

            if (m_rbnMovePhoneYes.Checked) {
                m_rowPhoneFields.Style["display"] = "block";
            } else {
                m_rowPhoneFields.Style["display"] = "none";
            }

            if (m_cmbProviders.Items.Count > 0) {
                m_rowProviderField.Style["display"] = "block";
            } else {
                m_rowProviderField.Style["display"] = "none";
            }

            if ((m_rowLowIncome.Style["display"] == "none"
                || (m_rowLowIncome.Style["display"] == "block" && (m_rbnLowIncomeNo.Checked || m_rbnLowIncomeYes.Checked)))
                && m_cmbProviders.Items.Count > 0 && m_rowProviderField.Style["display"] == "block") {
                m_btnNext.Enabled = true;
                m_btnNext.ImageUrl = "../images/btn_proceed.gif";
            } else {
                m_btnNext.Enabled = false;
                m_btnNext.ImageUrl = "../images/btn_proceed_disabled.gif";
            }
        }

        #endregion

        #region OnNextClick

        private void OnNextClick(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            if (m_rbnMovePhoneYes.Checked == false && m_rbnMovePhoneNo.Checked == false) {
                Model.Info.IsMoveExistingPhoneNull = true;
                Model.Info.PhoneFirst3 = string.Empty;
                Model.Info.PhoneSecond3 = string.Empty;
                Model.Info.PhoneLast4 = string.Empty;
            } else {
                Model.Info.IsMoveExistingPhoneNull = false;

                Model.Info.IsMoveExistingPhone = m_rbnMovePhoneYes.Checked;
                if (Model.Info.IsMoveExistingPhone) {
                    // TODO: move this fields with PhoneNumber value.
                    Model.Info.PhoneFirst3 = phnPhoneNumber.AreaCode;
                    Model.Info.PhoneSecond3 = phnPhoneNumber.Prefix;
                    Model.Info.PhoneLast4 = phnPhoneNumber.LineNumber;
                } else {
                    Model.Info.PhoneFirst3 = string.Empty;
                    Model.Info.PhoneSecond3 = string.Empty;
                    Model.Info.PhoneLast4 = string.Empty;
                }
            }

            if (m_hdnIsLowIncomeRowVisible.Value == "false") {
                Model.Info.IsQualifyForLowIncomeNull = true;
            } else {
                if (!m_rbnLowIncomeYes.Checked && !m_rbnLowIncomeNo.Checked) {
                    Model.Info.IsQualifyForLowIncomeNull = true;
                } else {
                    Model.Info.IsQualifyForLowIncomeNull = false;
                    Model.Info.IsQualifyForLowIncome = m_rbnLowIncomeYes.Checked;
                }
            }

            Model.Info.Zip = m_txtZip.Text;

            Model.Info.Provider = Model.GetProviders(Model.Info.Zip)[m_cmbProviders.SelectedIndex];

            Response.Redirect(SiteMap.NEW_ACC_SELECT_PACKAGE_URL);
        }

        #endregion        

        #region Validation

        private void OnPhoneValidate(object source, ServerValidateEventArgs args) 
        {
            if (m_rbnMovePhoneYes.Checked) {
                if (phnPhoneNumber.PhoneNumber == string.Empty) {
                    vldCstPhoneNumber.ErrorMessage = "<br>Required field cannot be left blank";
                    args.IsValid = false;
                } else {
                    vldCstPhoneNumber.ErrorMessage = "<br>The Phone Number provided is invalid";
                    args.IsValid = phnPhoneNumber.IsValid;
                }
            }
        }

        private void OnZipValidate(object source, ServerValidateEventArgs args)
        {
            if (m_txtZip.Text.Length == 0) {
                m_vldZip.ErrorMessage = "<br>Please enter your zip code.";
                args.IsValid = false;
            } else if (!IsAllDigits(m_txtZip.Text, 5)) {
                m_vldZip.ErrorMessage = "<br>Please enter your 5-digit zip code.";
                args.IsValid = false;
            } else {
                IILECInfo[] providers = Model.GetProviders(m_txtZip.Text);

                if (providers.Length == 0) {
                    m_vldZip.ErrorMessage = "<br>" + Model.GetProvidersErrorMessage(m_txtZip.Text);
                    args.IsValid = false;
                }
            }

            if (args.IsValid) {
                if (!m_rbnMovePhoneNo.Checked && !m_rbnMovePhoneYes.Checked) {
                    m_vldZip.ErrorMessage = "<br>Would you like to keep your existing number? Please select Yes or No.";
                    args.IsValid = false;
                } else if (m_rowLowIncome.Style["display"] == "block"
                    && !m_rbnLowIncomeNo.Checked && !m_rbnLowIncomeYes.Checked) {
                    m_vldZip.ErrorMessage = "<br>Do you qualify for low income assistance? Please select Yes or No.";
                    args.IsValid = false;
                }
            }
        }

        private void OnProviderValidate(object source, ServerValidateEventArgs args)
        {
            if (m_vldZip.IsValid && m_cmbProviders.SelectedIndex < 0) {
                m_vldProvider.ErrorMessage = "<br>Please select the local telephone company in your area.";
                args.IsValid = false;
            }
        }

        private bool IsAllDigits(string s, int digitsCount)
        {
            if (s.Length != digitsCount) {
                return false;
            }

            foreach (char c in s) {
                if (!char.IsDigit(c)) {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}