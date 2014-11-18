using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.Design.WebControls;
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
        Float,
        RegExp
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

        #endregion Fields

        #region Events

        [Description("Raised from Validate method. Used for custom validation")]
        public event ValidateEventHandler ServerValidate;

        #endregion Events

        #region Methods

        public void Validate()
        {
            string postBackValue = Context.Request[UniqueID];
            if (postBackValue == null) {
                _isValid = true;
                _errorMessage = null;
                return;
            }

            StringBuilder sb = new StringBuilder(200);
            string txt = Text;
            if (txt.Length == 0) {
                if (IsRequired) {
                    ControlHelper.AppendErrorMessage(sb, ErrorMsgEmpty, FieldName);
                }
            } else {
                string min, max;
                switch (ValueType) {
                    case TextBoxValueType.Integer:
                        min = MinValue;
                        max = MaxValue;
                        int minInt = (min == null ? int.MinValue : Convert.ToInt32(min));
                        int maxInt = (max == null ? int.MaxValue : Convert.ToInt32(max));
                        try {
                            int i = int.Parse(txt);
                            if (minInt != int.MinValue && i < minInt) {
                                ControlHelper.AppendErrorMessage(sb, ErrorMsgValLess, FieldName, minInt);
                            }
                            if (maxInt != int.MaxValue && i > maxInt) {
                                ControlHelper.AppendErrorMessage(sb, ErrorMsgValGreater, FieldName, maxInt);
                            }
                        } catch (FormatException) {
                            ControlHelper.AppendErrorMessage(sb, ErrorMsgNotInt, FieldName);
                        }
                        break;
                    case TextBoxValueType.Float:
                        min = MinValue;
                        max = MaxValue;
                        decimal minDec = (min == null ? decimal.MinValue : Convert.ToDecimal(min));
                        decimal maxDec = (max == null ? decimal.MaxValue : Convert.ToDecimal(max));
                        try {
                            decimal f = decimal.Parse(txt);
                            if (minDec != decimal.MinValue && f < minDec) {
                                ControlHelper.AppendErrorMessage(sb, ErrorMsgValLess, FieldName, minDec);
                            }
                            if (maxDec != decimal.MaxValue && f > maxDec) {
                                ControlHelper.AppendErrorMessage(sb, ErrorMsgValGreater, FieldName, maxDec);
                            }
                        } catch (FormatException) {
                            ControlHelper.AppendErrorMessage(sb, ErrorMsgNotFloat, FieldName);
                        }
                        break;
                    case TextBoxValueType.Digits:
                        for (int i = 0; i < txt.Length; i++) {
                            if (!Char.IsDigit(txt, i)) {
                                ControlHelper.AppendErrorMessage(sb, ErrorMsgNotDigits, FieldName);
                                break;
                            }
                        }
                        break;
                    case TextBoxValueType.RegExp:
                        bool matches = true;
                        string re = ValidationRegExp;
                        if (re != null && re.Length > 0 && txt.Trim().Length > 0) {
                            try {
                                Match mtch = Regex.Match(txt, ValidationRegExp);
                                matches = (mtch.Success && mtch.Index == 0 && mtch.Length == txt.Length);
                            } catch {
                                matches = true;
                            }
                        }
                        if (!matches) {
                            ControlHelper.AppendErrorMessage(sb, ErrorMsgRegExp, FieldName);
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
                    _errorMessage = String.Format(ErrorMsgFail, FieldName);
                }
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            _errorLabel = new Label();
            _errorLabel.CssClass = ControlHelper.ERROR_MESSAGE_CSS_CLASS;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
            if (PerformValidation && !Page.Validators.Contains(this)) {
                Page.Validators.Add(this);
            }
        }
        
        protected override void OnUnload(EventArgs e)
        {
            if (Page != null) {
                Page.Validators.Remove(this);
            }
            base.OnUnload(e);
        }


        protected override void Render(HtmlTextWriter writer)
        {
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
            if (IsRequired && Label == null && ShowRequiredIndicator) {
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

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
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
        [Category("Validation")]
        public bool IsRequired
        {
            get
            {
                object o = ViewState["IsRequired"];
                if (o == null) {
                    return false;
                }
                return (bool) o;
            }
            set { ViewState["IsRequired"] = value; }
        }

        [Category("Validation")]
        [DefaultValue(TextBoxValueType.Text)]
        public TextBoxValueType ValueType
        {
            get
            {
                object o = ViewState["TextBoxValueType"];
                if (o == null) {
                    return TextBoxValueType.Text;
                }
                return (TextBoxValueType) o;
            }
            set
            {
                ViewState["TextBoxValueType"] = value;
                Attributes[VALUE_TYPE_ATTRIBUTE] = value.ToString();
            }
        }

        [Category("Validation")]
        [DefaultValue(null)]
        public string MinValue
        {
            get
            {
                object o = ViewState["MinValue"];
                if (o == null) {
                    return null;
                }
                return (string) o;
            }
            set
            {
                if (value != null && value.Length > 0) {
                    ViewState["MinValue"] = value;
                }
            }
        }

        [Category("Validation")]
        [DefaultValue(null)]
        public string MaxValue
        {
            get
            {
                object o = ViewState["MaxValue"];
                if (o == null) {
                    return null;
                }
                return (string) o;
            }
            set
            {
                if (value != null && value.Length > 0) {
                    ViewState["MaxValue"] = value;
                }
            }
        }

        [Category("Validation")]
        [DefaultValue("")]
        [Editor(typeof (RegexTypeEditor), typeof (UITypeEditor))]
        [Bindable(true)]
        public string ValidationRegExp
        {
            get
            {
                object o = ViewState["ValidationRegExp"];
                if (o == null) {
                    return string.Empty;
                }
                return (string) o;
            }
            set
            {
                if (value != null && value.Length > 0) {
                    ViewState["ValidationRegExp"] = value;
                }
            }
        }


        [Category("Validation")]
        [DefaultValue(ControlHelper.DEFAULT_PERFORM_VALIDATION)]
        [Description("Determines if control validates itself. If true control is added to Page.Validators collestion.")]
        public bool PerformValidation
        {
            get
            {
                object o = ViewState["PerformValidation"];
                if (o == null) {
                    return ControlHelper.DEFAULT_PERFORM_VALIDATION;
                }
                return (bool) o;
            }
            set
            {
                ViewState["PerformValidation"] = value;
                if (value && Page != null && !Page.Validators.Contains(this)) {
                    Page.Validators.Add(this);
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Defines if the error message label shown when control's value is invelid")]
        public bool ShowErrorMessage
        {
            get
            {
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
        public string ErrorMessageCssClass
        {
            get
            {
                object o = ViewState["ErrorMessageCssClass"];
                if (o == null) {
                    return ControlHelper.ERROR_MESSAGE_CSS_CLASS;
                }
                return (string) o;
            }
            set { ViewState["ErrorMessageCssClass"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Defines if the error message label shown when control's value is invelid")]
        public bool ShowRequiredIndicator
        {
            get
            {
                object o = ViewState["ShowRequiredIndicator"];
                if (o == null) {
                    return true;
                }
                return (bool) o;
            }
            set { ViewState["ShowRequiredIndicator"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("CSS class applied to control when its value is not valid")]
        public string InvalidCssClass
        {
            get
            {
                object o = ViewState["InvalidCssClass"];
                return (string) o;
            }
            set { ViewState["InvalidCssClass"] = value; }
        }

        [Browsable(false)]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        [Browsable(false)]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #region Error messages

        [Description("Validation error message shown when required value is not entered")]
        [Category("Validation")]
        [DefaultValue(VAL_MSG_EMPTY)]
        public string ErrorMsgEmpty
        {
            get
            {
                object o = ViewState["ErrorMsgEmpty"];
                if (o == null) {
                    return VAL_MSG_EMPTY;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgEmpty"] = value; }
        }

        [Description("Validation error message shown when value for integer field is not an integer")]
        [Category("Validation")]
        [DefaultValue(VAL_MSG_NOT_INT)]
        public string ErrorMsgNotInt
        {
            get
            {
                object o = ViewState["ErrorMsgNotInt"];
                if (o == null) {
                    return VAL_MSG_NOT_INT;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgNotInt"] = value; }
        }

        [Description("Validation error message shown when value for float field is not a float")]
        [Category("Validation")]
        [DefaultValue(VAL_MSG_NOT_FLOAT)]
        public string ErrorMsgNotFloat
        {
            get
            {
                object o = ViewState["ErrorMsgNotFloat"];
                if (o == null) {
                    return VAL_MSG_NOT_FLOAT;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgNotFloat"] = value; }
        }

        [
            Description(
                "Validation error message shown when value which should contain only digits contains other characters")]
        [Category("Validation")]
        [DefaultValue(VAL_MSG_NOT_DIGITS)]
        public string ErrorMsgNotDigits
        {
            get
            {
                object o = ViewState["ErrorMsgNotDigits"];
                if (o == null) {
                    return VAL_MSG_NOT_DIGITS;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgNotDigits"] = value; }
        }

        [Description("Validation error message shown when value is less then MinValue")]
        [Category("Validation")]
        [DefaultValue(VAL_MSG_VAL_LESS)]
        public string ErrorMsgValLess
        {
            get
            {
                object o = ViewState["ErrorMsgValLess"];
                if (o == null) {
                    return VAL_MSG_VAL_LESS;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgValLess"] = value; }
        }

        [Description("Validation error message shown when value is greayer then MaxValue")]
        [Category("Validation")]
        [DefaultValue(VAL_MSG_VAL_GREATER)]
        public string ErrorMsgValGreater
        {
            get
            {
                object o = ViewState["ErrorMsgValGreater"];
                if (o == null) {
                    return VAL_MSG_VAL_GREATER;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgValGreater"] = value; }
        }

        [Description("General validation error message")]
        [Category("Validation")]
        [DefaultValue(DEFAULT_VALIDATION_FAIL_MSG)]
        public string ErrorMsgFail
        {
            get
            {
                object o = ViewState["ErrorMsgFail"];
                if (o == null) {
                    return DEFAULT_VALIDATION_FAIL_MSG;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgFail"] = value; }
        }

        [Description("General validation error message")]
        [Category("Validation")]
        [DefaultValue(DEFAULT_VALIDATION_FAIL_MSG)]
        public string ErrorMsgRegExp
        {
            get
            {
                object o = ViewState["ErrorMsgRegExp"];
                if (o == null) {
                    return DEFAULT_VALIDATION_FAIL_MSG;
                }
                return (string) o;
            }
            set { ViewState["ErrorMsgRegExp"] = value; }
        }

        #endregion

        #endregion Validation

        [Browsable(false)]
        public ControlLabel Label
        {
            get { return _label; }
            set { _label = value; }
        }

        [Browsable(false)]
        public string FieldName
        {
            get
            {
                string fld = Label == null ? ID : Label.FieldName;
                return fld.Length == 0 ? ID : fld;
            }
        }

        #endregion Properties
    }
}