using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspTextBox = System.Web.UI.WebControls.TextBox;

namespace Dpi.Central.Web.Controls
{

    #region Helper Classes

    public enum TextBoxValueType
    {
        Text,
        Digits,
        Integer,
        Float
    }

    #endregion Helper Classes

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TextBox runat=server></{0}:TextBox>")]
    [ToolboxItemFilter("Dpi.Central.Web.Controls", ToolboxItemFilterType.Require)]
    public class TextBox : AspTextBox, INamingContainer, IRequired, ILabelable, IValidatedControl
    {
        #region Constants

        private const string VAL_MSG_EMPTY = "Field {0} cannot be empty";
        private const string VAL_MSG_NOT_INT = "Value of the field {0} must be an integer";
        private const string VAL_MSG_NOT_FLOAT = "Value of the field {0} must be a number";
        private const string VAL_MSG_NOT_DIGITS = "Value of the field {0} must consist of digits";
        private const string VAL_MSG_VAL_LESS = "Value of the field {0} cannot be less then {1}";
        private const string VAL_MSG_VAL_GREATER = "Value of the field {0} cannot be greater then {1}";
        private const string DEFAULT_VALIDATION_FAIL_MSG = "Value of the field {0} is not valid";
        private const string VALUE_TYPE_ATTRIBUTE = "ValueType";

        #endregion Constants

        #region Fields

        private ControlLabel _label;
        private Label _errorLabel;
        private bool _isValid = true;
        private string _errorMessage;
        private TextBoxValueType _valueType = TextBoxValueType.Text;

        #endregion Fields

        #region Events

        [Description("Raised from Validate method. Used for custom validation")]
        public event ValidateEventHandler ServerValidate;

        #endregion Events

        #region Methods

        public void Validate() {
            string postBackValue = Context.Request[UniqueID];
            if (postBackValue == null) {
                _isValid = true;
                _errorMessage = null;
                return;
            }

            StringBuilder sb = new StringBuilder(200);
            if (Text.Length == 0) {
                if (IsRequired) {
                    ControlHelper.AppendErrorMessage(sb, VAL_MSG_EMPTY, FieldName);
                }
            } else {
                switch (_valueType) {
                    case TextBoxValueType.Integer:
                        int minInt = (MinValue == null ? int.MinValue : Convert.ToInt32(MinValue));
                        int maxInt = (MaxValue == null ? int.MaxValue : Convert.ToInt32(MaxValue));
                        try {
                            int i = int.Parse(Text);
                            if (minInt != int.MinValue && i < minInt) {
                                ControlHelper.AppendErrorMessage(sb, VAL_MSG_VAL_LESS, FieldName, minInt);
                            }
                            if (maxInt != int.MaxValue && i > maxInt) {
                                ControlHelper.AppendErrorMessage(sb, VAL_MSG_VAL_GREATER, FieldName, maxInt);
                            }
                        } catch (FormatException) {
                            ControlHelper.AppendErrorMessage(sb, VAL_MSG_NOT_INT, FieldName);
                        }
                        break;
                    case TextBoxValueType.Float:
                        decimal minDec = (MinValue == null ? decimal.MinValue : Convert.ToDecimal(MinValue));
                        decimal maxDec = (MaxValue == null ? decimal.MaxValue : Convert.ToDecimal(MaxValue));
                        try {
                            decimal f = decimal.Parse(Text);
                            if (minDec != int.MinValue && f < minDec) {
                                ControlHelper.AppendErrorMessage(sb, VAL_MSG_VAL_LESS, FieldName, minDec);
                            }
                            if (maxDec != int.MaxValue && f > maxDec) {
                                ControlHelper.AppendErrorMessage(sb, VAL_MSG_VAL_GREATER, FieldName, maxDec);
                            }
                        } catch {
                            ControlHelper.AppendErrorMessage(sb, VAL_MSG_NOT_FLOAT, FieldName);
                        }
                        break;
                    case TextBoxValueType.Digits:
                        for (int i = 0; i < Text.Length; i++) {
                            if (!Char.IsDigit(Text, i)) {
                                ControlHelper.AppendErrorMessage(sb, VAL_MSG_NOT_DIGITS, FieldName);
                                break;
                            }
                        }
                        break;
                }
            }

            _isValid = sb.Length == 0;

            if (ServerValidate != null) {
                Delegate[] handlers = ServerValidate.GetInvocationList();
                ValidateEventHandler[] validateHandlers = new ValidateEventHandler[handlers.Length];
                handlers.CopyTo(validateHandlers, 0);
                if (!ControlHelper.CallEachValidateHandler(this, validateHandlers, sb)) {
                    _isValid = false;
                }
            }

            if (_isValid) {
                _errorMessage = null;
            } else {
                _errorMessage = sb.ToString();
                if (_errorMessage.Length == 0) {
                    _errorMessage = String.Format(DEFAULT_VALIDATION_FAIL_MSG, FieldName);
                }
            }
        }

        protected override void CreateChildControls() {
            base.CreateChildControls();
            _errorLabel = new Label();
            _errorLabel.CssClass = ControlHelper.ERROR_MESSAGE_CSS_CLASS;
        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            EnsureChildControls();
            if (PerformValidation) {
                Page.Validators.Add(this);
            }
        }

        protected override void Render(HtmlTextWriter writer) {
            bool changeCss = PerformValidation && !IsValid && InvalidCssClass != null && InvalidCssClass.Length > 0;
            string cssClass = null;
            if (changeCss) {
                cssClass = base.CssClass;
                base.CssClass = InvalidCssClass;
            }
            bool renderErrLabel = PerformValidation && ShowErrorMessage && !IsValid && _errorMessage != null &&
                                  _errorMessage.Length > 0;
            if (renderErrLabel) {
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                if (!Width.IsEmpty) {
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, Width.ToString());
                }
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
                writer.AddStyleAttribute("padding", "0 0 3px 0");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
            }
            base.Render(writer);
            if (IsRequired && Label == null) {
                ControlHelper.RenderRequiredSymbol(writer);
            }
            if (renderErrLabel) {
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
                writer.AddStyleAttribute("padding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                _errorLabel.Text = _errorMessage;
                _errorLabel.RenderControl(writer);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            if (changeCss) {
                base.CssClass = cssClass;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer) {
            if (!Width.IsEmpty) {
                string b = null;
                try {
                    b = Page.Request.Browser.Browser;
                } catch {
                }
                if (b != "IE") {
                    Style["width"] = Width.ToString();
                }
            }

            base.AddAttributesToRender(writer);
        }

        #endregion Methods

        #region Properties

        #region Validation

        [DefaultValue(false)]
        [Category("Data")]
        public bool IsRequired {
            get {
                object o = ViewState["IsRequired"];
                if (o == null) {
                    return false;
                }
                return (bool) o;
            }
            set { ViewState["IsRequired"] = value; }
        }

        [Category("Data")]
        [DefaultValue(TextBoxValueType.Text)]
        public TextBoxValueType ValueType {
            get {
                object o = ViewState["TextBoxValueType"];
                if (o == null) {
                    return TextBoxValueType.Text;
                }
                return (TextBoxValueType) o;
            }
            set {
                ViewState["TextBoxValueType"] = value;
                Attributes[VALUE_TYPE_ATTRIBUTE] = _valueType.ToString();
            }
        }

        [Category("Data")]
        [DefaultValue(null)]
        public string MinValue {
            get {
                object o = ViewState["MinValue"];
                if (o == null) {
                    return null;
                }
                return (string) o;
            }
            set { ViewState["MinValue"] = value; }
        }

        [Category("Data")]
        [DefaultValue(null)]
        public string MaxValue {
            get {
                object o = ViewState["MaxValue"];
                if (o == null) {
                    return null;
                }
                return (string) o;
            }
            set { ViewState["MaxValue"] = value; }
        }

        [Category("Behavior")]
        [DefaultValue(ControlHelper.DEFAULT_PERFORM_VALIDATION)]
        [Description("Determines if control validates itself. If true control is added to Page.Validators collestion.")]
        public bool PerformValidation {
            get {
                object o = ViewState["PerformValidation"];
                if (o == null) {
                    return ControlHelper.DEFAULT_PERFORM_VALIDATION;
                }
                return (bool) o;
            }
            set {
                ViewState["PerformValidation"] = value;
                if (value && Page != null && !Page.Validators.Contains(this)) {
                    Page.Validators.Add(this);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Defines if the error message label shown when control's value is invelid")]
        public bool ShowErrorMessage {
            get {
                object o = ViewState["ShowErrorMessage"];
                if (o == null) {
                    return false;
                }
                return (bool) o;
            }
            set { ViewState["ShowErrorMessage"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(ControlHelper.ERROR_MESSAGE_CSS_CLASS)]
        [Description("CSS class applied to error message label when control's value is not valid")]
        public string ErrorMessageCssClass {
            get {
                object o = ViewState["ErrorMessageCssClass"];
                if (o == null) {
                    return ControlHelper.ERROR_MESSAGE_CSS_CLASS;
                }
                return (string) o;
            }
            set { ViewState["ErrorMessageCssClass"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("CSS class applied to control when its value is not valid")]
        public string InvalidCssClass {
            get {
                object o = ViewState["InvalidCssClass"];
                return (string) o;
            }
            set { ViewState["InvalidCssClass"] = value; }
        }

        [Browsable(false)]
        public bool IsValid {
            get { return _isValid; }
            set { _isValid = value; }
        }

        [Browsable(false)]
        public string ErrorMessage {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #endregion Validation

        [Browsable(false)]
        public ControlLabel Label {
            get { return _label; }
            set { _label = value; }
        }

        [Browsable(false)]
        public string FieldName {
            get { return Label == null ? ID : Label.FieldName; }
        }

        #endregion Properties
    }
}