namespace TractInc.TrueTract.Entity
{
    public class UserInfo
    {
        public int UserId;
        public string Login;
        public string FirstName;
        public string LastName;
        public string PhoneNumber;
        public string Password;
        public string Email;
        public bool IsActive;
        public int HackingAttempts;
        public string DefaultSite;
        public int ClientId;

        public UserInfo() {
        }

        public UserInfo(string login, string firstName, string lastName, string phoneNumber, string password,
                        string email, bool isActive, int hackingAttempts, string defaultSite, int clientId) {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            Email = email;
            IsActive = isActive;
            HackingAttempts = hackingAttempts;
            DefaultSite = defaultSite;
            ClientId = clientId;
                        }

        public UserInfo(int userId, string login, string firstName, string lastName, string phoneNumber, 
                        string password, string email, bool isActive, int hackingAttempts, string defaultSite, int clientId)
            : this(login, firstName, lastName, phoneNumber, password, email, isActive, hackingAttempts, defaultSite, clientId) {
            
            UserId = userId;

            }
    }
}