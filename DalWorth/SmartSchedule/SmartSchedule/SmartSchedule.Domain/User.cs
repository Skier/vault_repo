using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Timers;
using SmartSchedule.Data;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public partial class User
    {
        public delegate void CurrentUserChangedHandler(User newUser);
        public static event CurrentUserChangedHandler CurrentUserChanged;

        #region Constructors

        public User(){}

        static User()
        {
            if (Configuration.IsClientApplication)
            {
                m_logoutTimer = new Timer(Configuration.AutoLogOutSeconds * 1000);
                m_logoutTimer.Elapsed += OnLogoutTimerElapsed;                
            }
        }

        #endregion

        private static Timer m_logoutTimer;

        #region Current

        private static User m_current;
        public static User Current
        {
            get
            {
                if (m_current == null)
                    m_current = Anonymous;
                return m_current;
            }
        }

        #endregion

        #region Anonymous

        private static User m_anonymous;
        public static User Anonymous
        {
            get
            {
                if (m_anonymous == null)
                    m_anonymous = new User(1, (int)UserRoleEnum.Anonymous, "Anonymous", string.Empty, true);

                return m_anonymous;
            }
        }

        #endregion

        #region Sync

        private static User m_sync;
        public static User Sync
        {
            get
            {
                if (m_sync == null)
                    m_sync = new User(2, (int)UserRoleEnum.Supervisor, "Sync", string.Empty, true);

                return m_sync;
            }
        }

        #endregion

        #region OnLogoutTimerElapsed

        private static void OnLogoutTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            LogOut();
        }

        #endregion


        #region LogIn LogOut

        public static void Authenticate(User user)
        {
            m_current = user;
            m_logoutTimer.Start();
            if (CurrentUserChanged != null)
                CurrentUserChanged(Current);
        }        
        
        public static void LogOut()
        {
            m_current = null;
            m_logoutTimer.Stop();
            if (CurrentUserChanged != null)
                CurrentUserChanged(Current);
        }

        public static void ResetLogOutTimer()
        {
            m_logoutTimer.Stop();
            m_logoutTimer.Start();
        }

        #endregion

        #region CurrentRole

        public static UserRoleEnum CurrentRole
        {
            get { return Current.UserRole; }
        }

        #endregion

        #region UserRole

        public UserRoleEnum UserRole
        {
            get { return (UserRoleEnum) m_userRoleId; }
            set { m_userRoleId = (int) value; }
        }

        public string UserRoleText
        {
            get { return UserRole.ToString(); }
        }

        #endregion

        #region ActiveText

        public string ActiveText
        {
            get { return IsActive ? "Yes" : "No"; }
        }

        #endregion

        #region FindUser

        public static UserResult FindUser(string passwordHash)
        {
            User user = FindByPasswordHash(passwordHash);
            if (user == null)
                return new UserResult(null, "Invalid password");

            if (!user.IsActive)
                return new UserResult(null, "Account is disabled");

            return new UserResult(user, string.Empty);
        }

        #endregion

        #region Find By Password Hash

        private const string SqlFindByPasswordHash =
            @"SELECT * FROM `user`
                where Password = ?Password";

        public static User FindByPasswordHash(string passwordHash)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPasswordHash))
            {
                Database.PutParameter(dbCommand, "?Password", passwordHash);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion        
    }

    [DataContract]
    public class UserResult
    {
        #region UserResult

        public UserResult(User user, string error)
        {
            m_user = user;
            m_error = error;
        }

        #endregion

        #region User

        private User m_user;
        [DataMember]
        public User User
        {
            get { return m_user; }
            set { m_user = value; }
        }

        #endregion

        #region Error

        private string m_error;
        [DataMember]
        public string Error
        {
            get { return m_error; }
            set { m_error = value; }
        }

        #endregion
    }
}
      