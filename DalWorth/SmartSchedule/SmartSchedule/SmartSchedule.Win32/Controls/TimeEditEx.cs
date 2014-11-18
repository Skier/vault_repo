using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace SmartSchedule.Win32.Controls
{
    public class TimeEditEx : TimeEdit
    {
        public delegate void TimeChangedHandler(object sender, EventArgs e);
        public event TimeChangedHandler TimeChanged;

        private int? m_minuteIncrementInterval;
        public int? MinuteIncrementInterval
        {
            get { return m_minuteIncrementInterval; }
            set { m_minuteIncrementInterval = value; }
        }

        private void RefreshCurrentTime()
        {
            DateTime oldTime = Time;
            try
            {
                DateTime newTime = DateTime.Parse(Text);
                Time = new DateTime(Time.Year, Time.Month, Time.Day,
                                    newTime.Hour, newTime.Minute, newTime.Second);

                if (TimeChanged != null && oldTime != Time)
                    TimeChanged.Invoke(this, EventArgs.Empty);
            }
            catch (FormatException){}
        }


        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();

            if (TimeChanged != null)
                TimeChanged.Invoke(this, EventArgs.Empty);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
           
            if (!char.IsDigit(Convert.ToChar(e.KeyValue)))
                RefreshCurrentTime();
        }

        private int GetClosestMinutes(int currentMinutes, bool isUp)
        {
            int currentClockPart = currentMinutes/m_minuteIncrementInterval.Value;
            int nextPart = isUp ? currentClockPart + 1 : currentMinutes%m_minuteIncrementInterval.Value == 0
                ? currentClockPart - 1 : currentClockPart;            

            int newMinute = nextPart*m_minuteIncrementInterval.Value;

            if (newMinute >= 60)
                return 60 - newMinute;
            if (newMinute < 0)
                return 60 + newMinute;
            return newMinute;
        }

        protected override void OnSpin(SpinEventArgs e)
        {
            base.OnSpin(e);

            if ((SelectionStart == 2 || SelectionStart == 3) && m_minuteIncrementInterval.HasValue)
            {
                int selectionStart = SelectionStart;                

                Time = new DateTime(Time.Year, Time.Month, Time.Day, Time.Hour, 
                    GetClosestMinutes(Time.Minute, e.IsSpinUp), Time.Second);                
                e.Handled = true;

                SelectionStart = selectionStart;
                SelectionLength = 2;
            }

            if (SelectionStart != 0)
                return;

            if (e.IsSpinUp)
            {
                if (Time.Hour == 11)
                {
                    Time = new DateTime(Time.Year, Time.Month, Time.Day, 12, Time.Minute, Time.Second);
                    e.Handled = true;                    
                }
                else if (Time.Hour == 23)
                {
                    Time = new DateTime(Time.Year, Time.Month, Time.Day, 0, Time.Minute, Time.Second);
                    e.Handled = true;                    
                }
            }
            else
            {
                if (Time.Hour == 12)
                {
                    Time = new DateTime(Time.Year, Time.Month, Time.Day, 11, Time.Minute, Time.Second);
                    e.Handled = true;
                }
                else if (Time.Hour == 0)
                {
                    Time = new DateTime(Time.Year, Time.Month, Time.Day, 23, Time.Minute, Time.Second);
                    e.Handled = true;
                }
            }                          
        }
    }
}
