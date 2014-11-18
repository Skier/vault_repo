using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QAgent.License.Generator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            m_txtFirstName.KeyPress += new KeyPressEventHandler(OnTextFieldKeyPress);
            m_txtLastName.KeyPress += new KeyPressEventHandler(OnTextFieldKeyPress);
            m_txtCompany.KeyPress += new KeyPressEventHandler(OnTextFieldKeyPress);
                        
            m_txtFirstName.Select();
        }

        private void OnTextFieldKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                OnGenerateClick(sender, EventArgs.Empty);
        }

        private void OnGenerateClick(object sender, EventArgs e)
        {
            if (m_txtFirstName.Text == String.Empty)
            {
                MessageBox.Show("Please enter First Name");
                m_txtFirstName.Focus();
                return;
            }

            MD5 md5 = MD5.Create();
            string s = string.Format("G$G%*@94HE5{0}&312[{1}]4hS3h!4g%{2}*30^41", m_txtFirstName.Text.ToLower(), 
                m_txtLastName.Text.ToLower(), m_txtCompany.Text.ToLower());
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            Int64 value = BitConverter.ToInt64(result, 0);            
            
            if (value < 0)
                value *= -1;
            string valueString = value.ToString();
                        
            if (valueString.Length < 19)
            {
                for (int i = valueString.Length; i < 19; i++)
                    valueString += '0';
            }
            else if (valueString.Length > 19)
            {
                valueString = valueString.Substring(0, 19);
            }

            valueString = valueString.Insert(5, "-");
            valueString = valueString.Insert(11, "-");
            valueString = valueString.Insert(17, "-");
                
            m_txtLicense.Text = valueString;
            
        }
    }
}