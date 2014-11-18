using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    [ValidationProperty("Date"), DefaultProperty("Date")]
    public class DateBox : Control, INamingContainer
    {
        private const string MONTH_ID = "month";
        private const string DAY_ID = "day";
        private const string YEAR_ID = "year";

        private const string MONTH_CLASS = "month";
        private const string DAY_CLASS = "day";
        private const string YEAR_CLASS = "year";

        private const string SEPARATOR = "&nbsp;/&nbsp;";

        private TextBox m_monthTextBox = new TextBox();
        private TextBox m_dayTextBox = new TextBox();
        private TextBox m_yearTextBox = new TextBox();

        public DateBox()
        {
            m_monthTextBox.ID = MONTH_ID;
            m_dayTextBox.ID = DAY_ID;
            m_yearTextBox.ID = YEAR_ID;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack) {
                Month = Page.Request.Params[m_monthTextBox.UniqueID];
                Day = Page.Request.Params[m_dayTextBox.UniqueID];
                Year = Page.Request.Params[m_yearTextBox.UniqueID];
            }

            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            m_monthTextBox.MaxLength = 2;
            m_dayTextBox.MaxLength = 2;
            m_yearTextBox.MaxLength = 4;

            m_monthTextBox.CssClass = MONTH_CLASS;
            m_dayTextBox.CssClass = DAY_CLASS;
            m_yearTextBox.CssClass = YEAR_CLASS;

            m_monthTextBox.TabIndex = TabIndex;
            m_dayTextBox.TabIndex = (short)(TabIndex + 1);
            m_yearTextBox.TabIndex = (short)(TabIndex + 2);

            base.Controls.Add(m_monthTextBox);
            base.Controls.Add(new LiteralControl(SEPARATOR));
            base.Controls.Add(m_dayTextBox);
            base.Controls.Add(new LiteralControl(SEPARATOR));
            base.Controls.Add(m_yearTextBox);
        }

        protected override void OnPreRender(EventArgs e)
        {
            ControlHelper.NeedDwcUtils();

            ControlHelper.SetJumpForwardScript(m_monthTextBox, 2, m_dayTextBox);
            ControlHelper.SetJumpForwardScript(m_dayTextBox, 2, m_yearTextBox);

            ControlHelper.SetJumpBackwardScript(m_yearTextBox, m_dayTextBox);
            ControlHelper.SetJumpBackwardScript(m_dayTextBox, m_monthTextBox);

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Site != null && Site.DesignMode) {
                EnsureChildControls();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            base.Render(writer);

            writer.RenderEndTag();
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

        public bool IsValid
        {
            get
            {
                try {
                    DateTime.Parse(Month + "/" + Day + "/" + Year);
                } catch (Exception) {
                    return false;
                }

                return true;
            }
        }

        public bool IsNull
        {
            set
            {
                if (value) {
                    Month = String.Empty;
                    Day = String.Empty;
                    Year = String.Empty;
                }
            }

            get
            {
                if (Day == String.Empty && Month == String.Empty
                    && Year == String.Empty) {
                    return true;
                }

                return false;
            }
        }

        public DateTime Date
        {
            get
            {
                if (IsValid && !IsNull) {
                    return DateTime.Parse(Month + "/" + Day + "/" + Year);
                } else {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (value.Month.ToString().Length == 1) {
                    Month = "0" + value.Month.ToString();
                } else {
                    Month = value.Month.ToString();
                }

                if (value.Day.ToString().Length == 1) {
                    Day = "0" + value.Day.ToString();
                } else {
                    Day = value.Day.ToString();
                }

                Year = value.Year.ToString();
            }
        }

        private string Month
        {
            get { return m_monthTextBox.Text; }
            set
            {
                if (value == null) {
                    m_monthTextBox.Text = string.Empty;
                } else {
                    m_monthTextBox.Text = value;
                }
            }
        }

        private string Day
        {
            get { return m_dayTextBox.Text; }
            set
            {
                if (value == null) {
                    m_dayTextBox.Text = string.Empty;
                } else {
                    m_dayTextBox.Text = value;
                }
            }
        }

        private string Year
        {
            get { return m_yearTextBox.Text; }
            set
            {
                if (value == null) {
                    m_yearTextBox.Text = string.Empty;
                } else {
                    m_yearTextBox.Text = value;
                }
            }
        }

    }
}