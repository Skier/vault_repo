using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.Components
{
    public class TimeEditEx : TimeEdit
    {
        public delegate void TimeChangedHandler(object sender, EventArgs e);
        public event TimeChangedHandler TimeChanged;

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

        protected override void OnSpin(SpinEventArgs e)
        {
            base.OnSpin(e);

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
