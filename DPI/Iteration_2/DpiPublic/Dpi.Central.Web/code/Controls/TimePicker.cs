using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspListItem = System.Web.UI.WebControls.ListItem;

namespace Dpi.Central.Web.Controls
{
    public enum ControlLayout
    {
        Table,
        Flow
    }

    [DefaultProperty("esi")]
    [ToolboxData("<{0}:TimePicker runat=server></{0}:TimePicker>")]
    public class TimePicker: Control, INamingContainer
    {
        #region Helper classes

        public enum TimeFormatOverride
        {
            None,
            Format12,
            Format24
        }

        #endregion Helper classes

        #region Constants

        public const string TIME_DIVIDER = ":";
        public const string AM_PM_DIVIDER = "&nbsp;";
        public const string HOUR_MARKER = "hh";
        public const string MINUTE_MARKER = "mm";
        public const string SECOND_MARKER = "ss";
        public const string AM_PM_MARKER = "am/pm";

        private const string REQUIRE_SYMBOL = "*";
        private const string REQUIRE_CLASS = "requireIndicator";

        private const bool DEFAULT_ENABLE_VIEW_STATE = true;

        #endregion Constants

        #region Fields

        public static readonly TimeSpan NullValue = TimeSpan.MinValue;

        private TimeSpan _currentValue = NullValue;
        private DropDownList _lstHour = new DropDownList();
        private DropDownList _lstMinute = new DropDownList();
        private DropDownList _lstSecond = new DropDownList();
        private DropDownList _lstAmPm = new DropDownList();

        private string _postHour;
        private string _postMinute;
        private string _postSecond;
        private string _postAmPm;

        #endregion Fields

        #region Constructors

        public TimePicker() {
            _lstHour.ID = "hour";
            _lstMinute.ID = "minute";
            _lstSecond.ID = "second";
            _lstAmPm.ID = "ampm";
            _lstHour.CssClass = "listControl";
            _lstMinute.CssClass = "listControl";
            _lstSecond.CssClass = "listControl";
            _lstAmPm.CssClass = "listControl";
            _lstHour.Attributes["title"] = "Hours";
            _lstMinute.Attributes["title"] = "Minutes";
            _lstSecond.Attributes["title"] = "Seconds";
            _lstAmPm.Attributes["title"] = "AM/PM";
            Controls.Add(_lstHour);
            Controls.Add(_lstMinute);
            Controls.Add(_lstSecond);
            Controls.Add(_lstAmPm);
            EnableViewState = DEFAULT_ENABLE_VIEW_STATE;
        }

        #endregion Constructors

        #region Methods

        protected override void OnInit(EventArgs e) {
            if (!Is24TimeFormat()) {
                if (DateTimeFormatInfo.CurrentInfo.AMDesignator.Length == 0 ||
                    DateTimeFormatInfo.CurrentInfo.PMDesignator.Length == 0) {
                    CultureInfo cultureInfo = (CultureInfo) CultureInfo.CurrentCulture.Clone();
                    cultureInfo.DateTimeFormat.AMDesignator = DateTimeFormatInfo.InvariantInfo.AMDesignator;
                    cultureInfo.DateTimeFormat.PMDesignator = DateTimeFormatInfo.InvariantInfo.PMDesignator;
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                }
            }
            InitLists();
            base.OnInit(e);
            CheckPostBack();
        }

        protected override void Render(HtmlTextWriter writer) {
            if (_currentValue == NullValue) {
                _lstHour.SelectedValue =_postHour;
                _lstMinute.SelectedValue =_postMinute;
                _lstSecond.SelectedValue =_postSecond;
                _lstAmPm.SelectedValue =_postAmPm;
            } else {
                if (Is24TimeFormat()) {
                    _lstHour.SelectedValue =_currentValue.Hours.ToString();
                } else {
                    if (_currentValue.Hours >= 12) {
                        if (_currentValue.Hours == 12)
                            _lstHour.SelectedValue = "12";
                        else
                            _lstHour.SelectedValue =(_currentValue.Hours - 12).ToString();
                        _lstAmPm.SelectedValue =DateTimeFormatInfo.CurrentInfo.PMDesignator;
                    } else {
                        _lstHour.SelectedValue =(_currentValue.Hours == 0 ? 12 : _currentValue.Hours).ToString();
                        _lstAmPm.SelectedValue =DateTimeFormatInfo.CurrentInfo.AMDesignator;
                    }
                }
                _lstMinute.SelectedValue =_currentValue.Minutes.ToString();
                _lstSecond.SelectedValue =_currentValue.Seconds.ToString();
            }

            switch (Layout) {
                case ControlLayout.Table:
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
                    writer.AddStyleAttribute("display", "inline");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    _lstHour.RenderControl(writer);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(TIME_DIVIDER);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    _lstMinute.RenderControl(writer);
                    writer.RenderEndTag();
                    if (IsShowSeconds) {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.Write(TIME_DIVIDER);
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        _lstSecond.RenderControl(writer);
                        writer.RenderEndTag();
                    }
                    if (!Is24TimeFormat()) {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.Write(AM_PM_DIVIDER);
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        _lstAmPm.RenderControl(writer);
                        writer.RenderEndTag();
                    }
                    if (IsRequired) {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, REQUIRE_CLASS);
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(REQUIRE_SYMBOL);
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag(); // tr
                    writer.RenderEndTag(); // table
                    break;
                case ControlLayout.Flow:
                    _lstHour.RenderControl(writer);
                    writer.Write(TIME_DIVIDER);
                    _lstMinute.RenderControl(writer);
                    if (IsShowSeconds) {
                        writer.Write(TIME_DIVIDER);
                        _lstSecond.RenderControl(writer);
                    }
                    if (!Is24TimeFormat()) {
                        writer.Write(AM_PM_DIVIDER);
                        _lstAmPm.RenderControl(writer);
                    }
                    if (IsRequired) {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, REQUIRE_CLASS);
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(REQUIRE_SYMBOL);
                        writer.RenderEndTag();
                    }
                    break;
            }
        }

        private void FixMarkers() {
            if (this.ShowMarkers) {
                if (_lstHour.Items.FindByText(HOUR_MARKER) == null) {
                    _lstHour.Items.Insert(0, new ListItem(HOUR_MARKER, HOUR_MARKER));
                }
                if (_lstMinute.Items.FindByText(MINUTE_MARKER) == null) {
                    _lstMinute.Items.Insert(0, new ListItem(MINUTE_MARKER, MINUTE_MARKER));
                }
                if (_lstSecond.Items.FindByText(SECOND_MARKER) == null) {
                    _lstSecond.Items.Insert(0, new ListItem(SECOND_MARKER, SECOND_MARKER));
                }
                if (_lstAmPm.Items.FindByText(AM_PM_MARKER) == null) {
                    _lstAmPm.Items.Insert(0, new ListItem(AM_PM_MARKER, AM_PM_MARKER));
                }
            } else {
                _lstHour.Items.Remove(HOUR_MARKER);
                _lstMinute.Items.Remove(MINUTE_MARKER);
                _lstSecond.Items.Remove(SECOND_MARKER);
                _lstAmPm.Items.Remove(AM_PM_MARKER);
            }
        }

        private void InitLists() {
            if (!Page.IsPostBack || !EnableViewState) {
                _lstHour.Items.Clear();
                _lstMinute.Items.Clear();
                _lstSecond.Items.Clear();
                _lstAmPm.Items.Clear();
                if (this.ShowMarkers) {
                    _lstHour.Items.Add(new ListItem(HOUR_MARKER, HOUR_MARKER));
                    _lstMinute.Items.Add(new ListItem(MINUTE_MARKER, MINUTE_MARKER));
                    _lstSecond.Items.Add(new ListItem(SECOND_MARKER, SECOND_MARKER));
                    _lstAmPm.Items.Add(new ListItem(AM_PM_MARKER, AM_PM_MARKER));
                }

                int startHour = 0;
                int stopHour = 23;
                if (!Is24TimeFormat()) {
                    startHour = 1;
                    stopHour = 12;
                    _lstAmPm.Items.Add(new ListItem(DateTimeFormatInfo.CurrentInfo.AMDesignator, DateTimeFormatInfo.CurrentInfo.AMDesignator));
                    _lstAmPm.Items.Add(new ListItem(DateTimeFormatInfo.CurrentInfo.PMDesignator, DateTimeFormatInfo.CurrentInfo.PMDesignator));
                }

                for (int i = startHour; i <= stopHour; i++) {
                    _lstHour.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
                }

                for (int i = 0; i < 60; i += MinuteInterval) {
                    _lstMinute.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
                }

                for (int i = 0; i < 60; i++) {
                    _lstSecond.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
                }
            }
        }

        private bool Is24TimeFormat() {
            switch (FormatOverride) {
                case TimeFormatOverride.Format12:
                    return false;
                case TimeFormatOverride.Format24:
                    return true;
                case TimeFormatOverride.None:
                    return !DateTimeFormatInfo.CurrentInfo.LongTimePattern.EndsWith(" tt") &&
                        !DateTimeFormatInfo.CurrentInfo.LongTimePattern.EndsWith(" t");
                default:
                    throw new InvalidEnumArgumentException(string.Format("Invalid TimeFormatOverride value ({})", FormatOverride));
            }
        }

        private void CheckPostBack() {
            _currentValue = NullValue;
            _postHour = Page.Request[_lstHour.UniqueID];
            _postMinute = Page.Request[_lstMinute.UniqueID];
            if (_postHour == null || _postMinute == null || _postHour == HOUR_MARKER || _postMinute == MINUTE_MARKER) {
                return;
            }
            int h = Convert.ToInt32(_postHour);
            int m = Convert.ToInt32(_postMinute);
            int s = 0;
            _postSecond = Page.Request[_lstSecond.UniqueID];
            if (_postSecond != null && _postSecond.Length > 0 && _postSecond != SECOND_MARKER) {
                s = Convert.ToInt32(_postSecond);
            }
            _postAmPm = Page.Request[_lstAmPm.UniqueID];
            if (_postAmPm == DateTimeFormatInfo.CurrentInfo.AMDesignator) {
                if (h == 12) {
                    h = 0;
                }
            } else if (_postAmPm == DateTimeFormatInfo.CurrentInfo.PMDesignator) {
                if (h < 12) {
                    h += 12;
                }
            } else if (_postAmPm != null && !Is24TimeFormat()) {
                return;
            }
            _currentValue = new TimeSpan(0, h, m, s);
        }

        #endregion Methods

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan CurrentTime {
            get { return _currentValue; }
            set {
                if (value != NullValue && (value.TotalMilliseconds < 0 || value.TotalHours >= 24)) {
                    throw new ArgumentOutOfRangeException("value", string.Format("Invalid time value {0}", value));
                }
                _currentValue = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String CurrentTimeString {
            get {
                if (_currentValue == NullValue) {
                    return String.Empty;
                } else {
                    StringBuilder sb = new StringBuilder(10);
                    if (Is24TimeFormat()) {
                        sb.Append(_currentValue.Hours.ToString("00", NumberFormatInfo.CurrentInfo)).
                            Append(TIME_DIVIDER).
                            Append(_currentValue.Minutes.ToString("00", NumberFormatInfo.CurrentInfo));
                        if (IsShowSeconds) {
                            sb.Append(TIME_DIVIDER).
                                Append(_currentValue.Seconds.ToString("00", NumberFormatInfo.CurrentInfo));
                        }
                    } else {
                        sb.Append((_currentValue.Hours >= 12 ?
                            _currentValue.Hours - 12 :
                            _currentValue.Hours == 0 ? 12 : _currentValue.Hours).ToString("00", NumberFormatInfo.CurrentInfo)).
                            Append(TIME_DIVIDER).
                            Append(_currentValue.Minutes.ToString("00", NumberFormatInfo.CurrentInfo));
                        if (IsShowSeconds) {
                            sb.Append(TIME_DIVIDER).
                                Append(_currentValue.Minutes.ToString("00", NumberFormatInfo.CurrentInfo));
                        }
                        sb.Append(' ').
                            Append(_currentValue.Hours >= 12 ?
                                DateTimeFormatInfo.CurrentInfo.PMDesignator :
                                DateTimeFormatInfo.CurrentInfo.AMDesignator);
                    }
                    return sb.ToString();
                }
            }
            set {
                if (value.Length == 0) {
                    _currentValue = NullValue;
                } else {
                    if (Is24TimeFormat()) {
                        _currentValue = TimeSpan.Parse(value);
                    } else {
                        _currentValue = ControlHelper.ParseAmPmTime(value);
                    }
                }
            }
        }

        [DefaultValue(false)]
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

        [DefaultValue(false)]
        public bool IsShowSeconds {
            get {
                object o = ViewState["IsShowSeconds"];
                if (o == null) {
                    return false;
                }
                return (bool) o;
            }
            set { ViewState["IsShowSeconds"] = value; }
        }

        [DefaultValue(1)]
        public int MinuteInterval {
            get {
                object o = ViewState["MinuteInterval"];
                if (o == null) {
                    return 1;
                }
                return (int)o;
            }
            set {
                if (value < 1 || value > 30) {
                    throw new ArgumentOutOfRangeException("value", value.ToString(), "Invalid minute interval");
                }
                ViewState["MinuteInterval"] = value;
            }
        }

        [DefaultValue(true)]
        public bool Enabled {
            get {
                object o = ViewState["Enabled"];
                if (o == null) {
                    return true;
                }
                return (bool) o;
            }
            set {
                ViewState["Enabled"] = _lstHour.Enabled = _lstMinute.Enabled = _lstSecond.Enabled =
                    _lstAmPm.Enabled = value;
            }
        }

        [DefaultValue(DEFAULT_ENABLE_VIEW_STATE)]
        public override bool EnableViewState {
            get { return base.EnableViewState; }
            set {
                base.EnableViewState = _lstHour.EnableViewState = _lstMinute.EnableViewState =
                    _lstSecond.EnableViewState = _lstAmPm.EnableViewState = value;
            }
        }

        [DefaultValue(ControlLayout.Table)]
        [Description("Specifies the way of rendering control")]
        [Category("Appearance")]
        public ControlLayout Layout {
            get {
                object o = ViewState["Layout"];
                if (o == null) {
                    return ControlLayout.Table;
                }
                return (ControlLayout) o;
            }
            set { ViewState["Layout"] = value; }
        }

        /// <summary>
        /// Specifies if list headers (e.g. "hh", "mm") are shown
        /// </summary>
        [DefaultValue(true)]
        [Description("Specifies if list headers (e.g. \"hh\", \"mm\") are shown")]
        [Category("Appearance")]
        public bool ShowMarkers {
            get {
                object o = ViewState["ShowMarkers"];
                if (o == null) {
                    return true;
                }
                return (bool) o;
            }
            set {
                if (value == this.ShowMarkers) {
                    return;
                }
                ViewState["ShowMarkers"] = value;
                FixMarkers();
            }
        }

        /// <summary>
        /// Overrides default 12/24 time format
        /// </summary>
        [DefaultValue(TimeFormatOverride.None)]
        [Description("Overrides default 12/24 time format")]
        [Category("Appearance")]
        public TimeFormatOverride FormatOverride {
            get {
                object o = ViewState["FormatOverride"];
                if (o == null) {
                    return TimeFormatOverride.None;
                }
                return (TimeFormatOverride) o;
            }
            set {
                if (value == FormatOverride) {
                    return;
                }
                ViewState["FormatOverride"] = value;
                FixMarkers();
            }
        }

        #endregion Properties
    }
}