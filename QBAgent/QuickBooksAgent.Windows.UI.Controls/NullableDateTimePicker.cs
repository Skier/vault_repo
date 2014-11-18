using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Controls
{
    [ComVisible(false)]
    public class NullableDateTimePicker : DateTimePicker
    {
        #region Events

        public delegate void NullableValueChangedHandler(object sender, EventArgs e);
        public event NullableValueChangedHandler NullableValueChanged;

        #endregion

        #region Member variables

        private bool _isNull;
        private string _nullValue;
        private DateTimePickerFormat _format = DateTimePickerFormat.Long;
        private string _customFormat;
        private string _formatAsString;

        #endregion

        #region Constructor

        public NullableDateTimePicker()
            : base()
        {
            base.Format = DateTimePickerFormat.Custom;
            NullValue = " ";
            Format = DateTimePickerFormat.Long;
            ValueChanged += new EventHandler(OnValueChanged);
        }

        #endregion

        #region Public properties

        public new Object Value
        {
            get
            {
                if (_isNull)
                    return null;
                else
                    return base.Value;
            }
            set
            {
                if (value == null || value == DBNull.Value)
                {
                    SetToNullValue();
                    if (NullableValueChanged != null)
                        NullableValueChanged.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    SetToDateTimeValue();
                    base.Value = (DateTime)value;
                }
            }
        }

        [DefaultValue(DateTimePickerFormat.Long)]
        public new DateTimePickerFormat Format
        {
            get { return _format; }
            set
            {
                _format = value;
                if (!_isNull)
                    SetFormat();
            }
        }

        public new String CustomFormat
        {
            get { return _customFormat; }
            set { _customFormat = value; }
        }

        [DefaultValue(" ")]
        public String NullValue
        {
            get { return _nullValue; }
            set { _nullValue = value; }
        }
        #endregion

        #region Private methods/properties

        private string FormatAsString
        {
            get { return _formatAsString; }
            set
            {
                _formatAsString = value;
                base.CustomFormat = value;
            }
        }

        private void SetFormat()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dtf = ci.DateTimeFormat;
            switch (_format)
            {
                case DateTimePickerFormat.Long:
                    FormatAsString = dtf.LongDatePattern;
                    break;
                case DateTimePickerFormat.Short:
                    FormatAsString = dtf.ShortDatePattern;
                    break;
                case DateTimePickerFormat.Time:
                    FormatAsString = dtf.ShortTimePattern;
                    break;
                case DateTimePickerFormat.Custom:
                    FormatAsString = CustomFormat;
                    break;
            }
        }

        private void SetToNullValue()
        {
            _isNull = true;
            base.CustomFormat = (_nullValue == null || _nullValue == String.Empty) ? " " : "'" + _nullValue + "'";
        }

        private void SetToDateTimeValue()
        {
            if (_isNull)
            {
                SetFormat();
                _isNull = false;
            }
        }
        #endregion

        #region Events

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr HwndFrom;
            public int IdFrom;
            public int Code;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.Value = null;
                OnValueChanged(EventArgs.Empty);
            }
            base.OnKeyUp(e);
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            if (Value == null)
                SetToDateTimeValue();

            if (NullableValueChanged != null)
                NullableValueChanged.Invoke(this, EventArgs.Empty);
        }


        #endregion
    }
}
