using System.Data.SqlClient;
using TractInc.MapOptix.Data;
using TractInc.MapOptix.Entity;

namespace TractInc.MapOptix
{
    public class User
    {
        #region Fields

        private UserDataMapper m_userDM;

        #endregion

        #region Methods

        public UserInfo SignUp(UserInfo user)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                UserDM.Create(conn, user);
                UserDM.AddProfile(conn, user);
            }

            return user;
        }

        #endregion

        #region Properties

        private UserDataMapper UserDM
        {
            get
            {
                if (null == m_userDM)
                {
                    m_userDM = new UserDataMapper();
                }

                return m_userDM;
            }
        }

        #endregion

    }
}
