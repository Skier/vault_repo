using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    public enum OperatingMode
    {
        Test,
        Production
    }

    public class BasePage : Page
    {
        #region Constants

        protected const string INTERNAL_ERROR = "Internal error";
        private const string BODY_TAG_ERROR = "<BODY> tag must have runat=\"server\" and id=\"_body\" attributes to use ClickButtonOnEnter fuction.";

        protected const string HEADER_IMG_PATH = "~/images/header.jpg";
        protected const string ERROR_MSG_CSS_CLASS = "page_wide_error_message";

        #endregion

        #region Static Members

        protected static HtmlTableCell CreateCell(HtmlTable table, string cssClass)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();

            cell.Attributes.Add("class", cssClass);

            row.Cells.Add(cell);
            table.Rows.Add(row);

            return cell;
        }

        protected static HtmlTableCell CreateCell(HtmlTableRow row, string cssClass)
        {
            HtmlTableCell cell = new HtmlTableCell();

            cell.Attributes.Add("class", cssClass);

            row.Cells.Add(cell);

            return cell;
        }

        protected static HtmlImage CreateImage(string src)
        {
            HtmlImage image = new HtmlImage();

            image.Src = src;

            return image;
        }

        #endregion

        #region Fields

        protected HtmlGenericControl _body;

        private Label _errorMessageLabel;
        private HtmlForm _form;
        private Menu _menu;

        #endregion

        #region Override Methods

        protected override void OnInit(EventArgs e)
        {
            InitLayout();
            _errorMessageLabel = AddErrorMessageLabel();
            _errorMessageLabel.EnableViewState = false;
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            foreach (IValidator validator in this.Validators) {
                if (validator is BaseValidator) {
                    ((BaseValidator) validator).EnableClientScript = EnableValidatorsClientScript;
                }
            }

            _errorMessageLabel.Visible = _errorMessageLabel.Text != string.Empty;

            base.OnPreRender(e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns menu index in the page's controls collection.</returns>
        protected virtual void InitLayout()
        {
            Form.Controls.AddAt(0, Menu);
            Form.Controls.AddAt(0, CreateImage(HEADER_IMG_PATH));
        }

        protected void SetEnterKeyPressHandler(Control defaultButton)
        {
            if (_body == null) {
                throw new ApplicationException(BODY_TAG_ERROR);
            }

            _body.Attributes.Add("onkeypress", "return ClickButtonOnEnter(event, '" + defaultButton.ClientID + "');");
        }

        protected void RemoveEnterKeyPressHandler()
        {
            if (_body == null) {
                throw new ApplicationException(BODY_TAG_ERROR);
            }

            _body.Attributes.Remove("onkeypress");
        }

        protected void SetEnterKeyPressHandler(ControlCollection controls, Control defaultButton)
        {
            foreach (Control control in controls) {
                SetEnterKeyPressHandler(control.Controls, defaultButton);

                if (control is WebControl) {
                    ((WebControl) control).Attributes.Add("onkeypress", "return ClickButtonOnEnter(event, '" + defaultButton.ClientID + "');");
                }
            }
        }

        protected void SetErrorMessage(string errorMessage)
        {
            _errorMessageLabel.Text = errorMessage;
        }

        protected void SetErrorMessage(Exception ex)
        {
#if DEBUG
            _errorMessageLabel.Text = ex.Message;
#else
            _errorMessageLabel.Text = INTERNAL_ERROR;
            // TODO: add log entry.
#endif
        }

        #endregion

        protected virtual Label AddErrorMessageLabel() 
        {
            Label errorMessageLabel = new Label();
            errorMessageLabel.CssClass = ERROR_MSG_CSS_CLASS;

            Form.Controls.AddAt(0, errorMessageLabel);

            return errorMessageLabel;
        }

        #region Properties

        protected HtmlForm Form
        {
            get
            {
                if (_form == null) {
                    _form = ControlHelper.GetHtmlForm();
                }

                return _form;
            }
        }

        protected Menu Menu
        {
            get
            {
                if (_menu == null) {
                    _menu = MenuFactory.GetInstance().CreateMenu();
                }

                return _menu;
            }
        }

        protected virtual bool EnableValidatorsClientScript 
        {
            get { return false; }
        }

        protected OperatingMode Mode
        {
            get
            {
                string value = ConfigurationSettings.AppSettings["Mode"];

                if (value == null || value == string.Empty) {
                    return OperatingMode.Production;
                }

                try {
                    return (OperatingMode) Enum.Parse(typeof (OperatingMode), value, true);
                } catch (ArgumentException ex) {
                    throw new ApplicationException("The value '" + value + "' of 'Mode' application settings key is incorrect. The possible values are 'test' and 'production'.", ex);
                }
            }
        }

        #endregion
    }
}