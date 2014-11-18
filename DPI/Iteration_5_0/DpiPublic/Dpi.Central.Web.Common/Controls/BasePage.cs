using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    public class BasePage : Page
    {
        #region Constants

        protected const string HEADER_IMG_PATH = "~/images/header.jpg";

        private const string INTERNAL_ERROR = "Internal error";
        private const string BODY_TAG_ERROR = "<BODY> tag must have runat=\"server\" and id=\"_body\" attributes to use ClickButtonOnEnter fuction.";

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

        private Control _errorControl;
        private HtmlForm _form;
        private Menu _menu;

        #endregion

        #region Override Methods

        protected override void OnInit(EventArgs e)
        {
            InitLayout();

            _errorControl = CreateErrorControl();

            AddErrorControlToPage();
            
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            foreach (IValidator validator in this.Validators) {
                if (validator is BaseValidator) {
                    ((BaseValidator) validator).EnableClientScript = EnableValidatorsClientScript;
                }
            }

            base.OnPreRender(e);
        }

        #endregion

        #region Virtual Methods

        protected virtual void InitLayout() 
        {
            Form.Controls.AddAt(0, Menu);
            Form.Controls.AddAt(0, CreateImage(HEADER_IMG_PATH));
        }

        protected virtual Control CreateErrorControl() 
        {
            return ErrorControlFactory.Instance.CreateLabelErrorControl();
        }

        protected virtual void AddErrorControlToPage()
        {
            Form.Controls.AddAt(2, ErrorControl);
        }

        protected virtual void SetErrorMessage(string errorMessage) 
        {
            ErrorControlFactory.Instance.SetErrorMessage(ErrorControl, errorMessage);
        }

        #endregion

        #region Protected Methods

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

        protected void SetErrorMessage(Exception ex)
        {
            string message;
#if DEBUG
            StringBuilder sb = new StringBuilder(ex.Message);

            Exception exception = ex.InnerException;
            
            while (exception != null) {
                sb.Append("->");
                sb.Append(exception.Message);

                exception = exception.InnerException;
            }

            message = sb.ToString();
#else
            message = INTERNAL_ERROR;
            // TODO: add log entry.
#endif
            SetErrorMessage(message);
        }

        #endregion
        
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

        protected Control ErrorControl
        {
            get { return _errorControl; }
        }

        protected virtual bool EnableValidatorsClientScript 
        {
            get { return false; }
        }

        internal HtmlGenericControl Body
        {
            get
            {
                if (_body == null) {
                    throw new ApplicationException(BODY_TAG_ERROR);
                }

                return _body;
            }
        }

        #endregion
    }
}