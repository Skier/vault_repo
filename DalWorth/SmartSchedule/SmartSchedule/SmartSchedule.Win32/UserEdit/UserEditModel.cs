using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.UserEdit
{
    public class UserEditModel : IModel
    {
        #region User

        private User m_user;
        public User User
        {
            get { return m_user; }
            set { m_user = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion               
    }
}
