using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    public class ErrorControlFactory
    {
        private const string LABEL_EC_CSS_CLASS = "page_wide_error_message";
        private const string PANEL_EC_CSS_CLASS = "page_wide_error_panel";
        private const string ERROR_MSG_CSS_CLASS = "error_message";

        private static ErrorControlFactory _instance;

        public static ErrorControlFactory Instance
        {
            get
            {
                if (_instance == null) {
                    lock (typeof (ErrorControlFactory)) {
                        if (_instance == null) {
                            _instance = new ErrorControlFactory();
                        }
                    }
                }

                return _instance;
            }
        }

        protected ErrorControlFactory()
        {
        }

        public Panel CreatePanelErrorControl()
        {
            Panel errorMessagePanel = new Panel();
            errorMessagePanel.CssClass = PANEL_EC_CSS_CLASS;
            errorMessagePanel.Visible = false;

            Label errorMessageLabel = new Label();
            errorMessageLabel.CssClass = ERROR_MSG_CSS_CLASS;

            errorMessagePanel.Controls.Add(errorMessageLabel);

            return errorMessagePanel;
        }

        public Label CreateLabelErrorControl()
        {
            Label errorMessageLabel = new Label();

            errorMessageLabel.CssClass = LABEL_EC_CSS_CLASS;
            errorMessageLabel.EnableViewState = false;
            errorMessageLabel.Visible = false;

            return errorMessageLabel;
        }

        public void SetErrorMessage(Control errorControl, string errorMessage) 
        {
            if (errorControl is Panel) {
                ((Label)errorControl.Controls[0]).Text = errorMessage;
            } else if (errorControl is Label) {
                ((Label)errorControl).Text = errorMessage;
            } else {
                throw new ApplicationException("Type of error control is unknown: " + errorControl.GetType().Name + ".");
            }

            errorControl.Visible = errorMessage.Length > 0;
        }
    }
}