using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.Data;
using MobileTech.ServiceLayer;
using MobileTech.Windows.UI.Password;

namespace MobileTech.Windows.UI.Password
{
    public partial class PasswordModel
    {
        #region Fields

        PasswordFunctionality m_functionality;
        bool m_passed = false;
        string m_passwordUsed;

        public PasswordFunctionality PasswordFunctionality
        {
            get { return m_functionality; }
            set { m_functionality = value; }
        }
        public bool PasswordPassed
        {
            get { return m_passed; }
            set { m_passed = value; }
        }
        public string PasswordUsed
        {
            get { return m_passwordUsed; }
            set { m_passwordUsed = value; }
        }

        #endregion

        #region Constructors

        public PasswordModel(PasswordFunctionality command)
		{
            m_functionality = command;
            CheckExists();
        }

        #endregion

        #region IModel Members

        public void Init()
        {
        }

        #endregion

        #region Service

        private void CheckExists()
        {
            m_passed = (!Domain.Password.IsPasswordExists(m_functionality));
        }

        public bool CheckPassword(string password)
        {
            m_passed = Domain.Password.CheckAccess(m_functionality, password);
            return m_passed;
        }

        #endregion
    }
}