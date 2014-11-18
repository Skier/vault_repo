using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    /// <summary>
    ///     Summary description for DataPicker.
    /// </summary>
    [DefaultProperty("esi"),
        ToolboxData("<{0}:DatePicker runat=server></{0}:DatePicker>")]
    public class DatePicker : UserControl, IEnablement
    {
        private const string REQUIRE_SYMBOL = "*";
        private const string REQUIRE_CLASS = "requireIndicator";

        //    private static int MIN_YEAR     = 1999;
        //    private static int NUMBER_YEARS = 20;
        //    private static int MAX_YEAR     = MIN_YEAR + NUMBER_YEARS;

        private bool _is_required = false;
        private int _min_year = 1999;
        private int _max_year = 2019;

        private DateTime _initial_date = DateTime.MinValue;

        public bool IsRequired
        {
            set { _is_required = value; }
            get { return _is_required; }
        }

        public int MinYear
        {
            get { return _min_year; }
            set { _min_year = value; }
        }

        public int MaxYear
        {
            get { return _max_year; }
            set { _max_year = value; }
        }

        public readonly DropDownList lstMonth;
        public readonly DropDownList lstDay;
        public readonly DropDownList lstYear;

        public DatePicker()
        {
            lstMonth = new DropDownList();
            lstDay = new DropDownList();
            lstYear = new DropDownList();
            lstMonth.ID = "Month";
            lstDay.ID = "Day";
            lstYear.ID = "Year";
        }

        public bool Enabled
        {
            get { return lstDay.Enabled && lstMonth.Enabled && lstYear.Enabled; }
            set
            {
                lstMonth.Enabled = value;
                lstDay.Enabled = value;
                lstYear.Enabled = value;
            }
        }

        [Browsable(false)]
        public DateTime CurrentDate
        {
            get
            {
                //if (0 == lstYear.SelectedIndex || 0 == lstMonth.SelectedIndex || 0 == lstDay.SelectedIndex) {
                //    return DateTime.MinValue;
                //} else {
                    DateTime dt = new DateTime(int.Parse(lstYear.SelectedValue),
                                               int.Parse(lstMonth.SelectedValue),
                                               int.Parse(lstDay.SelectedValue));
                    return dt;
                //}
            }
            set { _initial_date = value; }
        }

        private void InitDesign()
        {
            Table _table = new Table();
            _table.CellSpacing = 0;
            _table.CellPadding = 0;
            TableRow _tableRow = new TableRow();

            TableCell _monthCell = new TableCell();
            lstMonth.CssClass = "listControl";
            _monthCell.Controls.Add(lstMonth);

            TableCell _dayCell = new TableCell();
            lstDay.CssClass = "listControl";
            _dayCell.Controls.Add(lstDay);

            TableCell _yearCell = new TableCell();
            lstYear.CssClass = "listControl";
            _yearCell.Controls.Add(lstYear);

            _tableRow.Controls.Add(_monthCell);
            _tableRow.Controls.Add(_dayCell);
            _tableRow.Controls.Add(_yearCell);

            _table.Controls.Add(_tableRow);
            this.Controls.Add(_table);
        }

        private void SetDate()
        {
            if (_initial_date != DateTime.MinValue) {
                DateTime dt = _initial_date;
                if (MinYear > dt.Year || MaxYear < dt.Year) {
                    lstYear.SelectedIndex = 0;
                } else {
                    lstYear.Items.FindByValue(dt.Year.ToString()).Selected = true;
                }
                lstMonth.Items.FindByValue(dt.Month.ToString()).Selected = true;
                lstDay.Items.FindByValue(dt.Day.ToString()).Selected = true;
            }
        }

        private void InitLists()
        {
            //lstMonth.Items.Add(new ListItem("Month", "0"));
            lstMonth.Items.Add(new ListItem("January", "1"));
            lstMonth.Items.Add(new ListItem("February", "2"));
            lstMonth.Items.Add(new ListItem("March", "3"));
            lstMonth.Items.Add(new ListItem("April", "4"));
            lstMonth.Items.Add(new ListItem("May", "5"));
            lstMonth.Items.Add(new ListItem("June", "6"));
            lstMonth.Items.Add(new ListItem("July", "7"));
            lstMonth.Items.Add(new ListItem("August", "8"));
            lstMonth.Items.Add(new ListItem("September", "9"));
            lstMonth.Items.Add(new ListItem("October", "10"));
            lstMonth.Items.Add(new ListItem("November", "11"));
            lstMonth.Items.Add(new ListItem("December", "12"));

            //lstYear.Items.Add(new ListItem("Year", "0"));

            for (int i = MinYear; i <= MaxYear; i++) {
                lstYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            SetNumberDays(31);
        }

        private void SetNumberDays(int days)
        {
            lstDay.Items.Clear();
            //lstDay.Items.Add(new ListItem("Day", "0"));
            for (int i = 0; i < days; i++) {
                int day = i+ 1;
                lstDay.Items.Add(new ListItem(day.ToString(), day.ToString()));
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            SetDate();
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            base.Render(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            if (_is_required) {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, REQUIRE_CLASS);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(REQUIRE_SYMBOL);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        public void OnChangeDate(object sender, EventArgs e)
        {
            int selected = lstDay.SelectedIndex;
            int days = 31;
            if (!(0 == lstYear.SelectedIndex || 0 == lstMonth.SelectedIndex)) {
                days = DateTime.DaysInMonth(int.Parse(lstYear.SelectedValue), int.Parse(lstMonth.SelectedValue));
                selected = days < selected ? days : selected;
            }
            SetNumberDays(days);
            lstDay.SelectedIndex = selected;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitDesign();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack) {
                InitLists();
            }
            lstMonth.Attributes.Add("onChange", "UpdateDaysList(" + lstMonth.ClientID + ", " + lstDay.ClientID + ", " + lstYear.ClientID + ");");
            lstYear.Attributes.Add("onChange", "UpdateDaysList(" + lstMonth.ClientID + ", " + lstDay.ClientID + ", " + lstYear.ClientID + ");");
            base.OnLoad(e);
        }

    }
}