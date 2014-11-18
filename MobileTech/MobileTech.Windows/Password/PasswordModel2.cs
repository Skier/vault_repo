using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Windows.UI.Password2
{
    public class PasswordModel2
    {
        #region Constructor

        public PasswordModel2(Domain.PasswordFunctionality functionality)
        {
            m_functionality = functionality;
        }

        #endregion

        #region PasswordFunctionality

        Domain.PasswordFunctionality m_functionality;
        public Domain.PasswordFunctionality Functionality
        {
            get { return m_functionality; }
            set { m_functionality = value; }
        }

        #endregion

        #region IsPasswordValid

        bool m_passwordValid;
        public bool IsPasswordValid
        {
            get { return m_passwordValid; }
        }

        #endregion

        #region ApplyPassword
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <exception cref="MobileTech.MobileTechAccessInvalidPasswordException"></exception>
        /// <exception cref="System.Exception"></exception>
        public void ApplyPassword(String password)
        {

            if (String.Empty.Equals(password))
                throw new MobileTechAccessInvalidPasswordException(m_functionality);

            m_passwordValid = Domain.Password.CheckAccess(m_functionality, password);

            if (!m_passwordValid)
                throw new MobileTechAccessInvalidPasswordException(m_functionality);
        }

        #endregion

    }
}
