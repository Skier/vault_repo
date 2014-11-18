using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI
{
    public partial class MainForm : Form
    {

#if !SMARTPHONE
        internal Microsoft.WindowsCE.Forms.InputPanel m_sip;
#endif

        private static MainForm s_instance;

        private MainForm()
        {
            InitializeComponent();

#if !SMARTPHONE
            m_sip = new Microsoft.WindowsCE.Forms.InputPanel();
#endif
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