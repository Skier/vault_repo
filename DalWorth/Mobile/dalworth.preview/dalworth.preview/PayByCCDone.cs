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
    public partial class PayByCCDone : BaseForm
    {
        public PayByCCDone()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_lblCCNumber.Text = Model.CurrentNumber;
            m_lblAmount.Text = Model.CurrentAmountReceived.ToString("C");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBox.Show("Dispatch notified");
            base.OnClosing(e);
        }

        private void OnDoneClick(object sender, EventArgs e)
        {
            Close();
        }                
    }
}