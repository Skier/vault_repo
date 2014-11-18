using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.Authenticate
{
    public class AuthenticateController : Controller<AuthenticateModel, AuthenticateView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion        

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtPassword.Select();
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            string password = View.m_txtPassword.Text;
            View.m_txtPassword.Text = string.Empty;
            string errorText;
            if (password == string.Empty)
                errorText = "Please enter password";
            else
                errorText = Model.Authenticate(password);

            if (errorText != string.Empty)
            {
                MessageBox.Show(errorText, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_txtPassword.Select();
                return;
            }                

            View.Destroy();            
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs eventArgs)
        {
            m_isCancelled = true;
            View.Destroy();            
        }

        #endregion

        #region IsAccessAllowed

        private static DateTime m_lastAuthenticationPromt = DateTime.MinValue;
        public static bool IsAccessAllowed(UserRoleEnum minimumRequiredRole)
        {
            if ((int)User.CurrentRole < (int)minimumRequiredRole)
            {
                if (DateTime.Now.Subtract(m_lastAuthenticationPromt).TotalMilliseconds < 100)
                    return false;
                
                using (AuthenticateController controller = Prepare<AuthenticateController>())
                {
                    controller.Execute(false);

                    if (controller.IsCancelled)
                    {
                        m_lastAuthenticationPromt = DateTime.Now;
                        return false;                        
                    }

                    if ((int)User.CurrentRole < (int)minimumRequiredRole)
                    {
                        MessageBox.Show("Your account has no access to this feature", "Access denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        m_lastAuthenticationPromt = DateTime.Now;
                        return false;
                    }
                }                
            }

            if (minimumRequiredRole == UserRoleEnum.Dispatrcher || minimumRequiredRole == UserRoleEnum.Supervisor)
                User.ResetLogOutTimer();
            return true;
        }

        #endregion
    }
}
