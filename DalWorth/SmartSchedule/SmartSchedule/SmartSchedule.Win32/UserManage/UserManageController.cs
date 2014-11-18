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
using SmartSchedule.Win32.UserEdit;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.UserManage
{
    public class UserManageController : Controller<UserManageModel, UserManageView>
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
            View.m_gridUsersView.DoubleClick += OnAddEditClick;
            View.m_btnAdd.Click += OnAddEditClick;
            View.m_btnEdit.Click += OnAddEditClick;
            View.m_btnClose.Click += OnCloseClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridUsers.DataSource = Model.Users;

            View.AlwaysAllowedControls.Add(View.m_btnClose);
            View.MinRequiredUserRole = UserRoleEnum.Supervisor;            
        }

        #endregion

        #region OnEditClick

        private void OnAddEditClick(object sender, EventArgs eventArgs)
        {
            User user = (User)View.m_gridUsersView.GetRow(View.m_gridUsersView.FocusedRowHandle);
            if (sender == View.m_btnAdd)
                user = null;

            using (UserEditController controller = Prepare<UserEditController>(user))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    Model.RefreshUsers();
                    View.m_gridUsers.DataSource = Model.Users;
                }                        
            }                    
        }

        #endregion

        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion   
    }
}
