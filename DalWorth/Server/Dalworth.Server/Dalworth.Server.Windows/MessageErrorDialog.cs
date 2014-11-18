using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Dalworth.Server.Controls;

namespace Dalworth.Server.Windows
{
    public partial class MessageErrorDialog : BaseForm
    {
        public MessageErrorDialog(Exception e)
        {
            InitializeComponent();            

            m_lbMessage.Text = e.Message;

            StringBuilder stringBuilder = new StringBuilder();

            while (e != null)
            {
                stringBuilder.Append(e.Message);
                stringBuilder.Append(":\n");
                stringBuilder.Append(e.StackTrace);
                stringBuilder.Append(new String('-', 20));

                e = e.InnerException;
            }

            m_txtDetail.Text = stringBuilder.ToString();
        }
        
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Text = "Error";

            m_btDetail.Text = "Detail >>";
            m_btnDone.Text = "Done";
        }

        private void OnDoneClick(object sender, EventArgs e)
        {
            WinAPI.CloseWindow(this);
        }

        private void OnDetailClick(object sender, EventArgs e)
        {
            m_txtDetail.Visible = !m_txtDetail.Visible;
        }
    }
}