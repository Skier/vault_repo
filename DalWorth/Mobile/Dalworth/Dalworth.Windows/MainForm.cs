using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;

namespace Dalworth.Windows
{
    public partial class MainForm : Form
    {
        private static MainForm s_instance;
        internal InternalMessageWindow m_messageWindow;

        private MainForm()
        {
            InitializeComponent();

            m_messageWindow = new InternalMessageWindow(this);
            RegisterHKeys.RegisterRecordKey(m_messageWindow.Hwnd);

            s_instance = this;
        }

        public static MainForm Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new MainForm();

                return s_instance;
            }
        }
    }
}