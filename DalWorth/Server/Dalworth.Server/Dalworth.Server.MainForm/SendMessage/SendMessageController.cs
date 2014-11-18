using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.SendMessage
{
    public class SendMessageController : Controller<SendMessageModel, SendMessageView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Work = (Work) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnSend.Click += OnOkClick;            

            View.m_txtMessage.Validating += OnMessageValidating;
            View.m_txtMessage.KeyDown += OnMessageKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblTechnician.Text = Model.Technician.DisplayName;
        }

        #endregion

        #region OnMessageKeyDown

        private void OnMessageKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                OnOkClick(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion


        #region OnMessageValidating

        private void OnMessageValidating(object sender, CancelEventArgs e)
        {
            if (View.m_txtMessage.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtMessage, "Please enter message");
            else
                View.m_errorProvider.SetError(View.m_txtMessage, string.Empty);
        }

        #endregion
               
        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            using (new WaitCursor())
                Model.Send(View.m_txtMessage.Text);
            
            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

    }
}
