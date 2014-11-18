using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;

namespace dalworth.preview
{
    public partial class NoGo : BaseForm
    {
        private bool m_isCancelService;
        private bool m_isButtonPressed;

        public bool IsCancelService
        {
            get { return m_isCancelService; }
        }

        public NoGo()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!m_isButtonPressed)
                e.Cancel = true;
        }

        private void OnYesClick(object sender, EventArgs e)
        {
            MessageBox.Show("Dispatch notified");
            m_isCancelService = true;
            m_isButtonPressed = true;
            Close();            
        }

        private void OnNoClick(object sender, EventArgs e)
        {
            m_isCancelService = false;
            m_isButtonPressed = true;
            Close();                        
        }
    }
}