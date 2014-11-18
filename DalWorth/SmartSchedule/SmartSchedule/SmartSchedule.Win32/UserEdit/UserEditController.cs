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
using SmartSchedule.SDK;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.UserEdit
{
    public class UserEditController : Controller<UserEditModel, UserEditView>
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
            if (data.Length > 0 && data[0] != null)
                Model.User = (User)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_txtLogin.Validating += OnLoginValidating;

            View.m_btnSave.Click += OnSaveClick;
            View.m_btnCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            foreach (UserRoleEnum userRole in Enum.GetValues(typeof(UserRoleEnum)))
            {
                if (userRole == UserRoleEnum.Anonymous)
                    continue;

                View.m_cmbUserRole.Properties.Items.Add(
                    new ImageComboBoxItem(userRole.ToString(), userRole));                
            }

            View.m_cmbUserRole.SelectedIndex = 0;
            View.m_chkIsActive.Checked = true;

            if (Model.User != null)
            {
                View.m_txtLogin.Text = Model.User.Login;
                foreach (ImageComboBoxItem item in View.m_cmbUserRole.Properties.Items)
                {
                    if ((UserRoleEnum)item.Value == Model.User.UserRole)
                    {
                        View.m_cmbUserRole.SelectedItem = item;
                        break;
                    }
                }
                View.m_chkIsActive.Checked = Model.User.IsActive;
            }

            View.AlwaysAllowedControls.Add(View.m_btnCancel);
            View.MinRequiredUserRole = (Model.User == null || Model.User.UserRole != UserRoleEnum.Administrator)
                ? UserRoleEnum.Supervisor : UserRoleEnum.Administrator;            
        }

        #endregion

        #region OnLoginValidating

        private void OnLoginValidating(object sender, CancelEventArgs cancelEventArgs)
        {
            View.m_errorProvider.SetError(View.m_txtLogin, 
                View.m_txtLogin.Text == string.Empty ? "Please enter User name" : string.Empty);
        }

        #endregion

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs eventArgs)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            if (Model.User == null && View.m_txtPassword.Text == string.Empty)
            {
                XtraMessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_txtPassword.Focus();
                return;
            }

            if (View.m_txtPassword.Text != string.Empty && View.m_txtPassword.Text.Length < 4)
            {
                XtraMessageBox.Show("Password should be at least 4 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_txtPassword.Text = string.Empty;
                View.m_txtPasswordConfirm.Text = string.Empty;
                View.m_txtPassword.Focus();
                return;
            }

            if (View.m_txtPassword.Text != string.Empty && View.m_txtPassword.Text != View.m_txtPasswordConfirm.Text)
            {
                XtraMessageBox.Show("Passwords don't match, please enter again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_txtPassword.Text = string.Empty;
                View.m_txtPasswordConfirm.Text = string.Empty;
                View.m_txtPassword.Focus();
                return;
            }            

            User.ResetLogOutTimer();
            if (Model.User == null)
                Model.User = new User(0);

            Model.User.UserRole = (UserRoleEnum) ((ImageComboBoxItem) View.m_cmbUserRole.SelectedItem).Value;
            if (User.CurrentRole != UserRoleEnum.Administrator && Model.User.UserRole == UserRoleEnum.Administrator)
            {
                XtraMessageBox.Show("You have to be administrator to assign Administrator role to other users", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_cmbUserRole.Focus();
                return;
            }            

            Model.User.Login = View.m_txtLogin.Text;
            if (View.m_txtPassword.Text != string.Empty)
                Model.User.Password = Hash.ComputeHash(View.m_txtPassword.Text);
            Model.User.IsActive = View.m_chkIsActive.Checked;

            string error = WcfClient.WcfClient.Instance.AddEditUser(Model.User);
            if (error != string.Empty)
            {
                XtraMessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_txtPassword.Text = string.Empty;
                View.m_txtPasswordConfirm.Text = string.Empty;
                View.m_txtPassword.Focus();
                return;
            }

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
