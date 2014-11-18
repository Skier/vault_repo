using System;
using System.Data.SqlClient;
using TractInc.Walt.Data;
using TractInc.Walt.Entity;

namespace TractInc.Walt
{
    public class User
    {
        #region Fields

        private UserDataMapper m_userDM;

        #endregion

        #region Methods

        public UserInfo SignUp(UserInfo user)
        {
            using (SqlConnection exConn = SQLHelper.CreateConnection())
            {
                exConn.Open();
                
                if (UserDM.Exists(exConn, user.UserName))
                {
                    throw new Exception(string.Format("User [{0}] already exists in walt", user.UserName));
                } else
                {
                    UserDM.Create(exConn, user);
                }
                
            }

            return user;
        }

        public void Login(string login)
        {
            using (SqlConnection exConn = SQLHelper.CreateConnection())
            {
                exConn.Open();

                if (!UserDM.Exists(exConn, login))
                {
                    throw new Exception(string.Format("User [{0}] not found in walt", login));
                }

            }
        }

        #endregion

        #region Properties

        private UserDataMapper UserDM {
            get {
                if (null == m_userDM) {
                    m_userDM = new UserDataMapper();
                }
                
                return m_userDM;
            }
        }

        #endregion
        
    }
}
