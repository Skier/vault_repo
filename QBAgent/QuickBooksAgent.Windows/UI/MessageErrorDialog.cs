using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace QuickBooksAgent.Windows.UI
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
            CommonUIResources.Culture = cultureInfo;

            Text = CommonResources.ErrorTitle;

            m_linkDetails.Text = CommonUIResources.BtnDetail;
        }

        private void OnDetailsClick(object sender, EventArgs e)
        {
            m_txtDetail.Visible = !m_txtDetail.Visible;
        }
    }
}