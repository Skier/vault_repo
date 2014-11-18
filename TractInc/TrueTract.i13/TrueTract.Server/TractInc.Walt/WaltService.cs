using TractInc.Walt.Entity;

namespace TractInc.Walt
{
    public class WaltService
    {
        private User m_userBC;
        
        public UserInfo SignUp(UserInfo userInfo)
        {
            return UserBC.SignUp(userInfo);
        }

        public void Login(string login)
        {
            UserBC.Login(login);
        }

        private User UserBC
        {
            get
            {
                if (null == m_userBC)
                {
                    m_userBC = new User();
                }

                return m_userBC;
            }
        }

    }
}
