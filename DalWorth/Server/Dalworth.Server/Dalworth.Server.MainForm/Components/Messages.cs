using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class Messages : BaseControl
    {
        #region Message1Text

        public string Message1Text
        {
            get { return m_txtMessage1.Text; }
            set { m_txtMessage1.Text = value; }
        }

        #endregion

        #region Message2Text

        public string Message2Text
        {
            get { return m_txtMessage2.Text; }
            set { m_txtMessage2.Text = value; }
        }

        #endregion

        #region Message3Text

        public string Message3Text
        {
            get { return m_txtMessage3.Text; }
            set { m_txtMessage3.Text = value; }
        }

        #endregion

        #region Labels

        public string Message1LabelText
        {
            get { return m_lblMessage1.Text; }
            set { m_lblMessage1.Text = value; }
        }

        public string Message2LabelText
        {
            get { return m_lblMessage2.Text; }
            set { m_lblMessage2.Text = value; }
        }

        public string Message3LabelText
        {
            get { return m_lblMessage3.Text; }
            set { m_lblMessage3.Text = value; }
        }

        #endregion

        #region ReadOnly

        public bool ReadOnly
        {
            get 
            {
                return m_txtMessage1.Properties.ReadOnly; 
            }
            set
            {
                m_txtMessage1.Properties.ReadOnly = value;
                m_txtMessage2.Properties.ReadOnly = value;
                m_txtMessage3.Properties.ReadOnly = value;
            }
        }

        #endregion

        #region Constructor

        public Messages()
        {
            InitializeComponent();
        }

        #endregion
    }
}
