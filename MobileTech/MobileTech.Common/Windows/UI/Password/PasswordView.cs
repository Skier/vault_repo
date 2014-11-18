using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MobileTech.Windows.UI.Password
{
    public partial class PasswordView : BaseForm
    {
        

        public PasswordView()
        {
         	InitializeComponent();
        }

        PasswordModel m_model;

        public override void BindData(Object data)
        {
            if (!(data is PasswordModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (PasswordModel)data;

        }


        #region ApplyUIResources
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
           Resources.Culture = cultureInfo;

           m_mbDone.Text = CommonResources.BtnDone;
           m_lbPassword.Text = Resources.EnterPassword;

           Text = Resources.Title;
        }
        #endregion

        private void OnDoneClick(object sender, EventArgs e)
        {
            OnDone();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnDone();
            }
        }

        private void OnDone()
        {
            m_model.PasswordUsed = m_txtPassword.Text.ToString();
            if (m_model.CheckPassword(m_model.PasswordUsed))
            {
                m_model.PasswordPassed = true;
                Destroy();
            }
            else
            {
                    MessageDialog.Show(MessageDialogType.Information,
                        CommonResources.MsgInvalidPassword);
            }
        }
    }
}