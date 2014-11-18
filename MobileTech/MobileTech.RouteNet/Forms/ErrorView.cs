using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MobileTech.Windows.UI.Forms
{
    public partial class ErrorView : BaseForm
    {
        public ErrorView()
        {
            InitializeComponent();
        }

        public ErrorView(Exception e)
        {
            InitializeComponent();

            m_txtMessage.Text = e.Message;

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

        private void menuButton1_Click(object sender, EventArgs e)
        {
            WinAPI.CloseWindow(this);
        }
    }
}