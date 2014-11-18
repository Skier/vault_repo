using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    [ValidationProperty("PhoneNumber"), DefaultProperty("PhoneNumber")]
    public class PhoneNumberBox : Control, INamingContainer
    {
        private const int AREA_CODE_MAX_LENGTH = 3;
        private const int PREFIX_MAX_LENGTH = 3;
        private const int LINE_NUMBER_MAX_LENGTH = 4;
        private const int MAX_LENGTH = AREA_CODE_MAX_LENGTH + PREFIX_MAX_LENGTH + LINE_NUMBER_MAX_LENGTH;

        private const string AREA_CODE_ID = "area_code";
        private const string PREFIX_MAX_ID = "prefix";
        private const string LINE_NUMBER_ID = "line_number";

        private const string AREA_CODE_CLASS = "phone_area_code";
        private const string PREFIX_MAX_CLASS = "phone_prefix";
        private const string LINE_NUMBER_CLASS = "phone_line_number";

        private const string SEPARATOR = "&nbsp;-&nbsp;";

        TextBox areaCodeTextBox = new TextBox();
        TextBox prefixTextBox = new TextBox();
        TextBox lineNumberTextBox = new TextBox();

        protected override void OnLoad(EventArgs e) 
        {
            EnsureChildControls();

            if (Page.IsPostBack) {
                if (Page.Request.Params[areaCodeTextBox.UniqueID] != null) {
                    AreaCode = Page.Request.Params[areaCodeTextBox.UniqueID];
                }

                if (Page.Request.Params[prefixTextBox.UniqueID] != null) {
                    Prefix = Page.Request.Params[prefixTextBox.UniqueID];
                }

                if (Page.Request.Params[lineNumberTextBox.UniqueID] != null) {
                    LineNumber = Page.Request.Params[lineNumberTextBox.UniqueID];
                }
            }

            base.OnLoad(e);
        }
        
        protected override void CreateChildControls()
        {
            areaCodeTextBox.ID = AREA_CODE_ID;
            prefixTextBox.ID = PREFIX_MAX_ID;
            lineNumberTextBox.ID = LINE_NUMBER_ID;

            areaCodeTextBox.MaxLength = AREA_CODE_MAX_LENGTH;
            prefixTextBox.MaxLength = PREFIX_MAX_LENGTH;
            lineNumberTextBox.MaxLength = LINE_NUMBER_MAX_LENGTH;

            areaCodeTextBox.CssClass = AREA_CODE_CLASS;
            prefixTextBox.CssClass = PREFIX_MAX_CLASS;
            lineNumberTextBox.CssClass = LINE_NUMBER_CLASS;

            areaCodeTextBox.Text = AreaCode;
            prefixTextBox.Text = Prefix;
            lineNumberTextBox.Text = LineNumber;

            areaCodeTextBox.TabIndex = TabIndex;
            prefixTextBox.TabIndex = (short)(TabIndex + 1);
            lineNumberTextBox.TabIndex = (short)(TabIndex + 2);

            areaCodeTextBox.Enabled = Enabled;
            prefixTextBox.Enabled = Enabled;
            lineNumberTextBox.Enabled = Enabled;

            base.Controls.Add(areaCodeTextBox);
            base.Controls.Add(new LiteralControl(SEPARATOR));
            base.Controls.Add(prefixTextBox);
            base.Controls.Add(new LiteralControl(SEPARATOR));
            base.Controls.Add(lineNumberTextBox);

            ControlHelper.NeedDwcUtils();

            ControlHelper.SetJumpForwardScript(areaCodeTextBox, AREA_CODE_MAX_LENGTH, prefixTextBox);
            ControlHelper.SetJumpForwardScript(prefixTextBox, PREFIX_MAX_LENGTH, lineNumberTextBox);

            ControlHelper.SetJumpBackwardScript(lineNumberTextBox, prefixTextBox);
            ControlHelper.SetJumpBackwardScript(prefixTextBox, areaCodeTextBox);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Site != null && Site.DesignMode) {
                EnsureChildControls();
            }

            base.Render(writer);
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                object enabled = this.ViewState["Enabled"];

                if (enabled != null) {
                    return (bool) enabled;
                }

                return true;
            }

            set { this.ViewState["Enabled"] = value; }
        }

        [Category("Behavior")]
        [DefaultValue((short) 0)]
        public short TabIndex
        {
            get
            {
                object tabIndex = this.ViewState["TabIndex"];

                if (tabIndex != null) {
                    return (short) tabIndex;
                }

                return 0;
            }

            set { this.ViewState["TabIndex"] = value; }
        }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                if (PhoneNumber == string.Empty) {
                    return true;
                }

                return Regex.IsMatch(AreaCode, @"^\d{" + AREA_CODE_MAX_LENGTH + "}$") && Regex.IsMatch(Prefix, @"^\d{" + PREFIX_MAX_LENGTH + "}$") && Regex.IsMatch(LineNumber, @"^\d{" + LINE_NUMBER_MAX_LENGTH + "}$");
            }
        }

        [Category("DPI Specific")]
        public string AreaCode
        {
            get
            {
                string text = (string) this.ViewState["AreaCode"];

                if (text != null) {
                    return text;
                }

                return string.Empty;
            }

            set { this.ViewState["AreaCode"] = value; }
        }

        [Category("DPI Specific")]
        public string Prefix
        {
            get 
            {
                string text = (string) this.ViewState["Prefix"];

                if (text != null) {
                    return text;
                }

                return string.Empty;
            }

            set { this.ViewState["Prefix"] = value; }
        }

        [Category("DPI Specific")]
        public string LineNumber
        {
            get 
            {
                string text = (string) this.ViewState["LineNumber"];

                if (text != null) {
                    return text;
                }

                return string.Empty;
            }

            set { this.ViewState["LineNumber"] = value; }
        }

        [Category("DPI Specific")]
        public string PhoneNumber
        {
            get { return AreaCode + Prefix + LineNumber; }
            set
            {
                if (value == null || value == string.Empty) {
                    AreaCode = Prefix = LineNumber = string.Empty;
                    return;
                }

                string phoneNumber = value.Trim();

                if (!Regex.IsMatch(phoneNumber, "^\\d{" + MAX_LENGTH + "}$")) {
                    throw new ArgumentException("Phone number " + value + " must have " + MAX_LENGTH + " digit characters.");
                }

                AreaCode = phoneNumber.Substring(0, AREA_CODE_MAX_LENGTH);
                Prefix = phoneNumber.Substring(AREA_CODE_MAX_LENGTH, PREFIX_MAX_LENGTH);
                LineNumber = phoneNumber.Substring(AREA_CODE_MAX_LENGTH + PREFIX_MAX_LENGTH, LINE_NUMBER_MAX_LENGTH);
            }
        }
    }
}