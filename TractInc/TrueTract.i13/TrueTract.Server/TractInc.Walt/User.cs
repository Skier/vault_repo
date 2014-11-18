using System;
using System.Data.SqlClient;
using TractInc.MapOptix;
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

// create user in mapoptix database

            MapOptix.Entity.UserInfo exUser = new MapOptix.Entity.UserInfo();
            exUser.UserId = user.UserId;
            exUser.ProfileId = MapOptix.Entity.UserInfo.DEFAULT_PROFILE_ID;
            exUser.UserAdmin = MapOptix.Entity.UserInfo.DEFAULT_ADMIN;
            exUser.UserActive = MapOptix.Entity.UserInfo.DEFAULT_ACTIVE;
            
            MapOptixService exService = new MapOptixService();

            exService.SignUp(exUser);
// ---------------------------------
            
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
