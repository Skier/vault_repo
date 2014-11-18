using System;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing;

namespace Dalworth.Server.Controls
{
	/// <summary>
	/// yInputMask Char definition below
	///		0	digit required
	///		9	digit optional
	///		L	lower case letter (a-z) required
	///		l	lower case letter optional
	///		U	upper case letter (A-Z) required
	///		u	upper case letter optional
	///		A	any case letter required
	///		a	any case letter optional
	///		D	letter or digit required
	///		d	letter or digit optional
    ///		#   digit or minus character required
	///		C	any char including punctuation
	///		\	escape char Used for literal mask chars ex: \0 for literal 0 in mask
	/// </summary>
	public class MaskedEdit : TextBox
	{
		private string m_mask;			// input mask
		private string m_format;		// display format (mask with input chars replaced by input char)
		private yInputMaskType m_maskType = yInputMaskType.Custom;
		private char m_inpChar;
		private bool m_maskChg;
		private bool m_stdmaskChg;
		private Hashtable m_regexps;
		private Hashtable m_posNdx;		// hold position translation map
		private int m_caret;
		private bool m_errInvalid;		// Error on invalid Text/Value input? -> true throws error, false ignore
		private int m_reqdCnt;			// required char count
		private int m_optCnt;			// optional char count
        private string m_defaultValue = "";       // default Text

		// allowed mask chars
		private const char MASK_KEY = '@';

		public enum yInputMaskType
		{
			None,			
			Custom
		}

		public MaskedEdit()
		{
			// set default mask, input char
			m_maskType = yInputMaskType.None;
			m_inpChar = '_';
			m_mask = "";
			m_format = "";
			m_caret = 0;
			m_errInvalid = false;
			base.Multiline = false;
            base.ForeColor = Color.Black;
        }
        #region Properties
        private new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
		public bool IsValid
		{
			get{return IsValidString(base.Text);}
		}
		public bool ErrorInvalid
		{
			get{return m_errInvalid;}
			set{m_errInvalid = value;}
		}
		public new bool Multiline
		{
			get{return base.Multiline;}
			// ignore set
		}
		public new int SelectionStart
		{
			get{return base.SelectionStart;}
		}
		public new int SelectionLength
		{
			get{return base.SelectionLength;}
		}
        public new int MaxLength
        {
            get { return base.MaxLength; }
            set
            {
                // prevent setting if Mask is defined
                if (m_maskChg || m_stdmaskChg || vStdyInputMask == yInputMaskType.None)
                    base.MaxLength = value;
            }
        }
        public yInputMaskType vStdyInputMask
        {
            get { return m_maskType; }
            set
            {
                m_stdmaskChg = true;
                m_maskType = value;

                // set mask string
                if (!m_maskChg)
                {
                    switch (value)
                    {
                        case yInputMaskType.None:
                            yInputMask = "";
                            break;
                        case yInputMaskType.Custom:
                            // User responsible for setting yInputMask
                            break;
                        default:
                            throw new ApplicationException("Invalid yInputMaskType");
                    }
                }
                m_stdmaskChg = false;

            }
        }
        public char wInputChar
        {
            // "_" default
            get { return m_inpChar; }
            set
            {
                m_inpChar = value;
                //yInputMask = m_mask;
            }
        }
        public string zValue
        {
            get
            {
                if (m_maskType == yInputMaskType.None)
                    return base.Text;
                else
                {
                    // return text with literals/spaces striped
                    string ret = "";
                    string m = yInputMask;
                    string t = base.Text;
                    if (IsValidString(t))
                    {
                        // strip literals/spaces
                        int tPos = 0;
                        for (int i = 0; i < m.Length; i++)
                        {
                            if (IsMaskChar(m[i]) && t[tPos] != ' ' && t[tPos] != m_inpChar)
                                ret += t[tPos];
                            else if (m[i] == '\\')
                                i++;

                            tPos++;
                        }
                    }

                    return ret;
                }
            }
            set
            {
                if (m_maskType == yInputMaskType.None)
                    base.Text = value;
                else
                {
                    //	Merge input chars with literals
                    string t = "";	// text being assembled from input Value and m_format string
                    int ipos = 0;	// input value position
                    int dif = value.Length - m_reqdCnt;
                    if (value == "")
                        base.Text = m_format;
                    else if (value.Length >= m_reqdCnt && value.Length <= m_reqdCnt + m_optCnt)
                    {
                        for (int fpos = 0; fpos < m_format.Length; fpos++)
                        {
                            if (ipos < value.Length && m_format[fpos] == m_inpChar)
                            {
                                // input char (not literal)
                                if (((string)RegExps[yInputMask[(int)m_posNdx[fpos]]]).IndexOf(' ') != -1)
                                {
                                    // optional
                                    if (dif > 0)
                                    {
                                        t += value[ipos++];
                                        dif--;
                                    }
                                    else
                                        t += m_format[fpos];
                                }
                                else
                                    t += value[ipos++];
                            }
                            else
                                t += m_format[fpos];
                        }
                    }
                    else if (m_errInvalid)
                        MessageBox.Show("Input String Does Not Match Input Mask");

                    // validate input
                    if (IsValidString(t))
                        base.Text = t;
                    else if (m_errInvalid)
                        MessageBox.Show("Input String Does Not Match Input Mask");
                }
            }
        }
        public override string Text
		{
			get{return base.Text;}
			set
			{
				if(m_maskType == yInputMaskType.None)
					base.Text = value;
				else
				{
					// if input string length doesn't match format length
					//	then we have a problem! this means that Text input
					//	string MUST have optional missing chars
					if(value == "")
						base.Text = m_format;
					else if(IsValidString(value) && value.Length == m_format.Length)
					{
						// must check optional input chars
						bool ok = true;
						int fpos = 0;
						while(ok && fpos < m_format.Length)
						{
							if(m_format[fpos] == m_inpChar 
								&& !IsValidChar(value[fpos], (int)m_posNdx[fpos]))
								ok &= value[fpos] == m_inpChar;
							fpos++;
						}

						if(ok)
							base.Text = value;
					}
					else if(m_errInvalid)
                        MessageBox.Show("Input String Does Not Match Input Mask");
				}
			}
		}

        public string yInputMask
        {
            get { return m_mask; }
            set
            {
                m_maskChg = true;
                m_mask = value;

                if (!m_stdmaskChg)
                {
                    // sync yInputMask with vStdyInputMask
                    switch (value)
                    {
                        case "":
                            vStdyInputMask = yInputMaskType.None;
                            break;
                        default:
                            vStdyInputMask = yInputMaskType.Custom;
                            break;
                    }
                }
                SetupMask();
                // runtime handling, reset text if current text is not valid
                if (base.Text.Length == 0 || !IsValidString(base.Text))
                    base.Text = m_format;
                else
                {
                    // reformat current text with new mask
                    this.zValue = this.zValue;
                }

                base.MaxLength = m_format.Length;
                m_maskChg = false;
            }
        }
#endregion
        #region PrivateMethods
        // private methods
		private bool IsValidString(string s)
		{
			bool ret = true;
			int pos = 0;
			// validate considering optional chars
			while(ret && pos < m_format.Length) 
			{
				if(m_format[pos] == m_inpChar)
				{
					// check input is valid including "optional" -> space in regexp
					if(pos >= s.Length)
					{
						// must be optional input
						ret = ((string)RegExps[yInputMask[(int)m_posNdx[pos]]]).IndexOf(' ') != -1;
					}
					else
					{
						// valid or optional
						ret = IsValidChar(s[pos], (int)m_posNdx[pos]);
						if(!ret)
							ret |= ((string)RegExps[yInputMask[(int)m_posNdx[pos]]]).IndexOf(' ') != -1
								&& (s[pos] == ' ' || s[pos] == m_inpChar);
					}
				}
				else
				{
					// check literal match
					if(pos < s.Length)
						ret = s[pos] == m_format[pos];
				}
				pos++;
			}
			return ret;
		}

#endregion
        #region Events
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            base.ForeColor = Color.Black;
            //base.Select(0, base.MaxLength);
            base.SelectionLength = 0;
            base.Select(0, zValue.Length);
            m_defaultValue = zValue;
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (zValue == "")
            {
                zValue = m_defaultValue;
                return;
            }
            base.OnLostFocus(e);
            if (!IsValid)
            {
                if (m_errInvalid)
                {
                    MessageBox.Show("Input String Does Not Match Input Mask.");
                    base.ForeColor = Color.Red;
                }
            }
         }

        protected void OnKeyDownBase(KeyEventArgs e)
	    {
	        base.OnKeyDown(e);
	    }
	    
        protected override void OnKeyDown(KeyEventArgs e)
		{	
			// return true to discontinue processing
			if(vStdyInputMask == yInputMaskType.None)
			{
				base.OnKeyDown(e);
				return;// base.ProcessCmdKey(ref msg, keyData);
			}

			SetValidSelection();

			// NOTES: 
			//	1) break; causes warnings below
			//	2) m_caret tracks caret location, always the start of selected char
			int strt = base.SelectionStart;
			int len = base.SelectionLength;			
			int end = strt + base.SelectionLength - 1;
			string s = base.Text;
			int p;

			switch(e.KeyData)
			{
				case Keys.Left:
					goto case Keys.Up;

				case Keys.Up:
					p = Prev(strt);
					if(p != strt)
					{
						base.SelectionStart = p;
						base.SelectionLength = 1;
					}
					m_caret = p;
					e.Handled = true;
					break;

				case Keys.Left | Keys.Shift:
					goto case Keys.Up | Keys.Shift;

				case Keys.Up | Keys.Shift:
					if((strt < m_caret) || (strt == m_caret && len <= 1))
					{
						// enlarge left
						p = Prev(strt);
						base.SelectionStart -= (strt - p);
						base.SelectionLength = len + (strt - p);
					}
					else
					{
						// shrink right
						base.SelectionLength = len - (end - Prev(end));
					}
					e.Handled = true;
					break;

				case Keys.Right:
					goto case Keys.Down;

				case Keys.Down:
					p = Next(strt);
					if(p != strt)
					{
						base.SelectionStart = p;
						base.SelectionLength = 1;
					}
					m_caret = p;
					e.Handled = true;
					break;

				case Keys.Right | Keys.Shift:					
					goto case Keys.Down | Keys.Shift;

				case Keys.Down | Keys.Shift:
					if(strt < m_caret)
					{
						// shrink left
						p = Next(strt);
						base.SelectionStart += (p - strt);
						base.SelectionLength = len - (p - strt);
					}
					else if(strt == m_caret)
					{
						// enlarge right
						p = Next(end);
						base.SelectionLength = len + (p - end);
					}
					e.Handled = true;
					break;

				case Keys.Delete:
					// delete selection, replace with input format
					base.Text = s.Substring(0, strt) + m_format.Substring(strt, len) + s.Substring(strt + len);
					base.SelectionStart = strt;
					base.SelectionLength = 1;
					m_caret = strt;
					e.Handled = true;
					break;

				case Keys.Home:
					base.SelectionStart = Next(-1);
					base.SelectionLength = 1;
					m_caret = base.SelectionStart;
					e.Handled = true;
					break;

				case Keys.Home | Keys.Shift:
					if(strt <= m_caret && len <= 1)
					{
						// enlarge left
						p = Next(-1);
						base.SelectionStart -= (strt - p);
						base.SelectionLength = len + (strt - p);
					}
					else
					{
						// shrink right
						p = Next(-1);
						base.SelectionStart = p;
						base.SelectionLength = (m_caret - p) + 1;
					}
					e.Handled = true;
					break;

				case Keys.End:
					base.SelectionStart = Prev(base.MaxLength);
					base.SelectionLength = 1;
					m_caret = base.SelectionStart;
					e.Handled = true;
					break;

				case Keys.End | Keys.Shift:
					if(strt < m_caret)
					{
						// shrink left
						p = Prev(base.MaxLength);
						base.SelectionStart = m_caret;
						base.SelectionLength = (p - m_caret + 1);
					}
					else if(strt == m_caret)
					{
						// enlarge right
						p = Prev(base.MaxLength);
						base.SelectionLength = len + (p - end);
					}
					e.Handled = true;
					break;

				case Keys.V | Keys.Control:
					goto case Keys.Insert | Keys.Shift;

				case Keys.Insert | Keys.Shift:
					//					// attempt paste
					//					// NOTES:
					//					//	1) Paste is likely to have literals since it must be copied from somewhere
					//					IDataObject iData = Clipboard.GetDataObject();
					//
					//					// assemble new text
					//					string t = s.Substring(0, strt)
					//						+ (string)iData.GetData(DataFormats.Text)
					//						+ s.Substring(strt + len);
					//
					//					// check if data to be pasted is convertible to inputType
					//					if(IsValidString(t))
					//						base.Text = t;
					//					else if(m_errInvalid)
					//						throw new ApplicationException("Input String Does Not Match Input Mask");
					//
					e.Handled = true;
					break;

				default:
					base.OnKeyDown(e);//		return base.ProcessCmdKey(ref msg, keyData);
					return;
			}

			//base.OnKeyDown(e);
		}
	
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if(vStdyInputMask == yInputMaskType.None || e.KeyChar == '\r')
			{
				base.OnKeyPress(e);
				return;
			}

			int strt = base.SelectionStart;
			int len = base.SelectionLength;
			int p = 1;

			// Handle Backspace -> replace current char with inpchar and select previous
			if(e.KeyChar == 0x08)
			{
				string s = base.Text;
                if (base.SelectionLength == base.MaxLength)
                {
                    strt = base.MaxLength;
                    while (strt > 0)
                    {
                        strt--;
                        if (m_format[strt] == m_inpChar)
                            break;
                    }
                    strt--;
                }
				p = Prev(strt);				
				base.Text = s.Substring(0, strt) + m_inpChar.ToString() + s.Substring(strt + 1);
				base.SelectionStart = p;
				base.SelectionLength = 1;
				m_caret = p;
				e.Handled = true;
				return;
			}

			// update display if valid char entered
			if(IsValidChar(e.KeyChar, (int)m_posNdx[strt]))
			{
				// assemble new text
				string t = "";
				t = base.Text.Substring(0, strt);
				t += e.KeyChar.ToString();

				if(strt + len != base.MaxLength)
				{
					t += m_format.Substring(strt + 1, len - 1);
					t += base.Text.Substring(strt + len);
				}
				else
					t += m_format.Substring(strt + 1);

				base.Text = t;

				// select next input char
				strt = Next(strt);
				base.SelectionStart = strt;
				m_caret = strt;
				base.SelectionLength = 1;
			}
			e.Handled = true;
		}

		protected override void OnKeyUp(KeyEventArgs e)		
		{	
			if(vStdyInputMask == yInputMaskType.None)
			{
				base.OnKeyUp (e);
				return;
			}
			
			SetValidSelection();
		
			base.OnKeyUp (e);
		}

        private void SetValidSelection()
		{	
			// reset selection to include input chars
			int strt = base.SelectionStart;
			int orig = strt;
			int len = base.SelectionLength;

			// reset selection start
			if(strt == base.MaxLength || m_format[strt] != m_inpChar)
			{
				// reset start
				if(Next(strt) == strt) // if on the last character
					strt = Prev(strt);   // stay on the same spot
				else
					strt = Next(strt);   // otherwise move forward one

				base.SelectionStart = strt; // set start
			}

			// reset selection length
			if(len < 1) // if nothing is selected
				base.SelectionLength = 1; // ensure that at least one character is selected
			else if(m_format[orig + len - 1] != m_inpChar)
			{
				len += Next(strt + len) - (strt + len);
				base.SelectionLength = len;
			}

			m_caret = strt;
        }
        #endregion
        #region PrivateVariables
        private Hashtable RegExps
        {
            get
            {
                if (m_regexps == null)
                {
                    m_regexps = new Hashtable();

                    // build regexps
                    m_regexps.Add('0', @"[0-9]");		// digit required
                    m_regexps.Add('9', @"[0-9 ]");		// digit/space not required

                    m_regexps.Add('L', @"[a-z]");		// letter a-z required
                    m_regexps.Add('l', @"[a-z ]");		// letter a-z not required

                    m_regexps.Add('U', @"[A-Z]");		// letter A-Z required
                    m_regexps.Add('u', @"[A-Z ]");		// letter A-Z not required

                    m_regexps.Add('A', @"[a-zA-Z]");	// letter required
                    m_regexps.Add('a', @"[a-zA-Z ]");	// letter not required

                    m_regexps.Add('D', @"[a-zA-Z0-9]");		// letter or digit required
                    m_regexps.Add('d', @"[a-zA-Z0-9 ]");	// letter or digit not required

                    m_regexps.Add('#', @"((\d)|(-))");	    // digit or minus character required

                    m_regexps.Add('C', @".");		// any char

                    // IMPORTANT: You MUST add and new mask chars to this regexp!
                    m_regexps.Add('@', @"[09LlUuAaDd#C]");	// used for input char testing
                }

                return m_regexps;
            }
        }
        private bool IsValidChar(char input, int pos)
		{
			// validate input char against mask
			return Regex.IsMatch(input.ToString(), (string)RegExps[yInputMask[pos]]);
		}

		private bool IsMaskChar(char input)
		{
			// check char
			return Regex.IsMatch(input.ToString(), (string)RegExps[MASK_KEY]);
		}

		private void SetupMask()
		{
			// used to build position translation map from mask string
			//	and input format
			string s = yInputMask;
			m_format = "";

			// reset index
			if(m_posNdx == null)
				m_posNdx = new Hashtable();
			else
				m_posNdx.Clear();

			int cnt = 0;
			m_reqdCnt = 0;
			m_optCnt = 0;

			for(int i = 0; i < s.Length; i++)
			{
				if(IsMaskChar(s[i]))
				{
					m_posNdx.Add(cnt, i);
					m_format += m_inpChar;

					// update optional/required char counts
					if(((string)RegExps[yInputMask[i]]).IndexOf(' ') != -1)
						m_optCnt++;
					else
						m_reqdCnt++;
				}
				else if(s[i] == '\\')
				{
					// escape char
					i++;
					m_format += s[i].ToString();
				}
				else
					m_format += s[i].ToString();

				cnt++;
			}
		}


		private int Prev(int startPos)
		{
			// return previous input char position
			// returns current position if no input chars to the left
			// caller must decide what to do with this
			int strt = startPos;
			int ret = strt;
			while(strt > 0)
			{
				strt--;
				if(m_format[strt] == m_inpChar)
					return strt;
			}
			return ret;			
		}

		private int Next(int startPos)
		{
			// return next input char position
			// returns current position if no input chars to the right
			// caller must decide what to do with this
			int strt = startPos;
			int ret = strt;
			
			while(strt < base.MaxLength - 1)
			{
				strt++;
				if(m_format[strt] == m_inpChar)
					return strt;
			}

			return ret;
        }
        #endregion
    }
}
