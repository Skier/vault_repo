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
        public int NewTracts;

        public UserInfo() {
        }

        public UserInfo(string login, string firstName, string lastName, string phoneNumber, string password,
                        string email, bool isActive, int hackingAttempts, int newTracts) {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            Email = email;
            IsActive = isActive;
            HackingAttempts = hackingAttempts;
            NewTracts = newTracts;
                        }

        public UserInfo(int userId, string login, string firstName, string lastName, string phoneNumber, 
                        string password, string email, bool isActive, int hackingAttempts, int newTracts)
            : this(login, firstName, lastName, phoneNumber, password, email, isActive, hackingAttempts, newTracts) {
            
            UserId = userId;

            }
    }
}