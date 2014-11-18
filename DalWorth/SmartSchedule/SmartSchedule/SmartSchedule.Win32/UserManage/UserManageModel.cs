using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.UserManage
{
    public class UserManageModel : IModel
    {
        #region Users

        private BindingList<User> m_users;
        public BindingList<User> Users
        {
            get { return m_users; }
        }

        #endregion

        #region Init

        public void Init()
        {
            RefreshUsers();
        }

        #endregion

        #region RefreshUsers

        public void RefreshUsers()
        {
            List<User> users = WcfClient.WcfClient.Instance.FindUsers();
            foreach (var user in users)
            {
                if (user.ID == User.Sync.ID)
                {
                    users.Remove(user);
                    break;                    
                }
            }

            m_users = new BindingList<User>(users);                        
        }

        #endregion
    }
}
