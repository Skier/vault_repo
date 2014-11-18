using System;

namespace TractInc.ScopeScetch.Entity
{
    public class User
    {
        
        public int UserId;
        public String Login;
        public String Password;
        public String Email;
        public bool IsActive;
        public int HackingAttempts;
        public int NewTracts;
            
        public User() {
        }

        public User(int userId, string login, string password, string email, bool isActive, int hackingAttempts, int newTracts)
        {
            UserId = userId;
            Login = login;
            Password = password;
            Email = email;
            IsActive = isActive;
            HackingAttempts = hackingAttempts;
            NewTracts = newTracts;
        }
    }
}
