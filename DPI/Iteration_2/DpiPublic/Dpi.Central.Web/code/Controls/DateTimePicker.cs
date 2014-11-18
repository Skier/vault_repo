using System;
using System.ComponentModel;
using System.Web.UI;

namespace Dpi.Central.Web.Controls
{
    [DefaultProperty("esi"),
        ToolboxData("<{0}:DateTimePicker runat=server></{0}:DateTimePicker>")]
    public class DateTimePicker : UserControl
    {
        private DatePicker _date_picker;
        private TimePicker _time_picker;

        public DateTimePicker()
        {
            _date_picker = new DatePicker();
            _time_picker = new TimePicker();
            _date_picker.ID = "Date";
            _time_picker.ID = "Time";
        }

        public DateTime CurrentDateTime
        {
            get
            {
                DateTime date = GetDatePicker().CurrentDate;
                TimeSpan time = GetTimePicker().CurrentTime;
                DateTime result;
                if (date != DateTime.MinValue) {
                    if (TimeSpan.MinValue != time) {
                        result = new DateTime(
                            date.Year,
                            date.Month,
                            date.Day,
                            time.Hours,
                            time.Minutes,
                            time.Seconds);
                    } else {
                        result = new DateTime(
                            date.Year,
                            date.Month,
                            date.Day);
                    }
                } else {
                    result = DateTime.MinValue;
                }

                return result;
            }

            set
            {
                if (value != DateTime.MinValue) {
                    GetDatePicker().CurrentDate = value;
                    GetTimePicker().CurrentTime = value.TimeOfDay;
                }
            }
        }

        public DatePicker GetDatePicker()
        {
            return _date_picker;
        }

        public TimePicker GetTimePicker()
        {
            return _time_picker;
        }

        public void Page_Init(object sender, EventArgs e)
        {
            this.Controls.Add(GetDatePicker());
            this.Controls.Add(GetTimePicker());
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            GetDatePicker().RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("&nbsp;");
            GetTimePicker().RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();

//            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
//            writer.RenderBeginTag(HtmlTextWriterTag.Td);
//            writer.RenderEndTag();
//            writer.RenderEndTag();
//            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
//            writer.RenderBeginTag(HtmlTextWriterTag.Td);
//            writer.RenderEndTag();
//            writer.RenderEndTag();
//            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
//            writer.RenderBeginTag(HtmlTextWriterTag.Td);
//            writer.RenderEndTag();
//            writer.RenderEndTag();
//
//            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
//            writer.RenderBeginTag(HtmlTextWriterTag.Td);
//            GetTimePicker().RenderControl(writer);
//            writer.RenderEndTag();
//            writer.RenderEndTag();
            writer.RenderEndTag();
            //        base.Render(writer);
        }

    }
}