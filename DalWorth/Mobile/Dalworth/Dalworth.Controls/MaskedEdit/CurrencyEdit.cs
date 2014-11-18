using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Controls
{
    public class CurrencyEdit : DigitsEdit
    {
        private const decimal SYMBOL_LENGTH = (decimal)7.5;

        #region Fields

        string m_currencyString = "";
        string separator = CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator;
        bool m_negative = false;
        bool m_isAllowNegative = false;
        bool m_isAllowNull = false;

        #endregion

        #region Properties

        /// <summary>
        /// Read only. Returns the contents of the textbox as a 
        /// decimal. is affected by the state of the IsNegative
        /// property.
        /// </summary>
        public decimal? Value
        {
            get
            {
                if (m_currencyString.Length == 0)
                {
                    if (m_isAllowNull)
                        return null;
                    else
                        return decimal.Zero;
                }

                decimal rv;
                if (m_currencyString.Length > 3)
                {
                    rv = decimal.Parse(m_currencyString.Insert(m_currencyString.Length - 2, "."), new CultureInfo("en-US"));
                }
                else
                {
                    rv = decimal.Parse(m_currencyString) / 100;
                }


                if (IsNegative)
                    rv *= -1;

                return rv;
            }

            set
            {
                if (value == null)
                {
                    m_currencyString = string.Empty;
                }

                else
                {
                    if (value.Value < 0)
                        IsNegative = true;
                    else
                        IsNegative = false;

                    m_currencyString = value.Value.ToString("0.00", new CultureInfo("en-US"));
                    m_currencyString = m_currencyString.Replace(".", String.Empty);
                    m_currencyString = long.Parse(m_currencyString).ToString();
                    m_currencyString = m_currencyString.Replace("-", "");

                }

                FillText();
            }
        }

        /// <summary>
        /// Causes the textbox to display a negative sign in front of
        /// the number. Also affects the sign of the Value property
        /// </summary>
        public bool IsNegative
        {
            get { return m_negative; }
            set
            {
                m_negative = value;
                FillText();
            }
        }

        public bool IsAllowNegative
        {
            get { return m_isAllowNegative; }
            set { m_isAllowNegative = value; }
        }

        public bool IsAllowNull
        {
            get { return m_isAllowNull; }
            set { m_isAllowNull = value; }
        }

        #endregion

        #region Constructor

        public CurrencyEdit()
        {
            Multiline = true;
            TextAlign = HorizontalAlignment.Right;
            FillText();
        }

        #endregion

        #region OnGotFocus

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            PositionCaret();
        }

        #endregion

        #region OnKeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;

            if (e.KeyChar == (char)Keys.Back) // backspace or delete
            {
                if (m_isAllowNull && m_currencyString.Length == 1 && m_currencyString[0] != '0')
                {
                    m_currencyString = "0";
                }
                else if (m_currencyString.Length > 0)
                    m_currencyString = m_currencyString.Substring(0, m_currencyString.Length - 1);
                else
                    return;
            }
            else if (e.KeyChar >= (char)Keys.D0 && e.KeyChar <= (char)Keys.D9) // a numeral
            {
                if (!(e.KeyChar == (char)Keys.D0 && Value.HasValue && Value == 0))
                {
                    if (m_currencyString == "0")
                        m_currencyString = e.KeyChar.ToString();
                    else
                        m_currencyString = m_currencyString + e.KeyChar;

                    if (m_currencyString.Length > 3)
                    {                        
                        try
                        {
                            decimal.Parse(m_currencyString.Insert(m_currencyString.Length - 2, "."), new CultureInfo("en-US"));
                        }
                        catch (Exception)
                        {
                            m_currencyString = m_currencyString.Remove(m_currencyString.Length - 1, 1);
                        }
                    }
                }
            }

            else if (e.KeyChar == '-' && m_isAllowNegative)
                IsNegative = !IsNegative;
            else
                return;

            FillText();
            PositionCaret();
        }

        #endregion

        #region OnClick/DoubleClick

        protected override void OnClick(EventArgs e)
        {
            PositionCaret();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            PositionCaret();
        }

        #endregion

        #region PositionCaret

        private void PositionCaret()
        {
            SelectionStart = Text.Length;
            SelectionLength = 0;
        }

        #endregion

        #region FillText

        void FillText()
        {            
            string sign = (IsNegative ? "-" : "");
            string outputText;

            switch (m_currencyString.Length)
            {
                case 0:
                    if (m_isAllowNull)
                        outputText = string.Empty;
                    else
                        outputText = "0" + separator + "00";

                    break;
                case 1:
                    outputText = sign + "0" + separator + "0" + m_currencyString;
                    break;
                case 2:
                    outputText = sign + "0" + separator + m_currencyString;
                    break;
                default:
                    outputText = sign + m_currencyString.Substring(0, m_currencyString.Length - 2) + separator +
                        m_currencyString.Substring(m_currencyString.Length - 2);
                    break;
            }

            if (SYMBOL_LENGTH * outputText.Length > Width)
            {
                Text = outputText.Remove(0, outputText.Length - (int)(Width / SYMBOL_LENGTH));
            }
            else
            {
                Text = outputText;
            }
        }

        #endregion
    }

}
