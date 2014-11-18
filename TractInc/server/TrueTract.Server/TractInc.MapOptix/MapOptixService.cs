using TractInc.MapOptix.Entity;

namespace TractInc.MapOptix
{
    public class MapOptixService
    {
        private User m_userBC;

        public UserInfo SignUp(UserInfo userInfo)
        {
            return UserBC.SignUp(userInfo);
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
