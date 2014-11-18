using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class User
{
    private const string SQL_SELECT_BY_FILTER =
        @"select u.*
            from [User] u
                inner join Person p on u.PersonId = p.PersonId
           where 1=1";

    private const string SQL_SELECT_BY_LOGIN = "select "
        + " UserId, "
        + " PersonId, "
        + " Login, "
        + " Password, "
        + " IsActive, "
        + " HackingAttempts "
        + " from [User] "
        + " where Login = '{0}'";
    
    public Person Personal = null;
    public UserPreference Preference = null;
    public List<Role> RoleList = null;
    
    #region ConfigurationSettings

    private const string SMTP_SERVER_KEY = "smtp";
    private const string SMTP_PORT_KEY = "port";
    private const string SMTP_USER_KEY = "username";
    private const string SMTP_PASSWORD_KEY = "password";
    private const string EMAIL_FROM_KEY = "from";
    private const string EMAIL_SUBJECT_KEY = "subject";

    private const int DEFAULT_SMTP_PORT = 25;
    private const string DEFAULT_EMAIL_FROM = "admin@truetract.com";
    private const string DEFAULT_EMAIL_SUBJECT = "Password recovery";
    private const string EMAIL_BODY_TEMPLATE = @"
==============================
login: {0}
password: {1}
==============================
";
    #endregion

    #region Fields

    private const int MAX_ATTEMPTS = 5;
    private const string DEFAULT_SITE = "http://logicland.com.ua";
    private const int NEW_TRACTS = 8;

    #endregion

    #region Methods

    public User()
    {
    }

    public static User SignUp(Person person, string login, string password)
    {
        User existingUser = User.GetUserByLogin(login);

        if (null != existingUser)
        {
            throw new Exception(string.Format("User with login {0} already exists", login));
        }

        User user = new User();
        user.Login = login;
        user.Password = password;
        user.HackingAttempts = 0;
        user.IsActive = true;

        Database.Begin();

        try
        {
            Person.Insert(person);

            user.PersonId = person.PersonId;
            User.Insert(user);

            UserRole userRole = new UserRole();
            userRole.UserId = user.UserId;
            userRole.RoleId = UserRole.INITIAL_USER_ROLE;
            UserRole.Insert(userRole);

            UserPreference up = new UserPreference();
            up.UserId = user.UserId;
            up.DefaultSite = User.DEFAULT_SITE;
            up.NewTracts = User.NEW_TRACTS;
            UserPreference.Insert(up);
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        
        Database.Commit();

        return user;
    }

    public static User UserLogin(string login, string password)
    {
        User user;

        user = User.GetUserByLogin(login);

        if (null != user)
        {
            if (!user.IsActive)
            {
                throw new Exception("User is inactive.");
            }

            if (user.Password != password)
            {

                user.HackingAttempts++;

                if (user.HackingAttempts >= MAX_ATTEMPTS)
                {
                    user.IsActive = false;
                    user.HackingAttempts = 0;
                }

                Database.Begin();
                try {
                    User.Update(user);
                }
                catch (Exception ex)
                {
                    Database.Rollback();
                    throw ex;
                }
                Database.Commit();                

                throw new Exception("Invalid password");
            }

        }
        else
        {
            throw new Exception("User not found");
        }

        if ( user.HackingAttempts > 0)
        {
            user.HackingAttempts = 0;
            Database.Begin();
            try {
                User.Update(user);
            }
            catch (Exception ex)
            {
                Database.Rollback();
                throw ex;
            }
            Database.Commit();                
        }

        return user;
    }

    public static bool SendPassword(String login)
    {
        User userInfo = GetUserByLogin(login);
        if (null == userInfo)
        {
            return false;
        }

        Person personInfo = Person.FindByPrimaryKey(userInfo.PersonId);
        if (null == personInfo) {
            return false;
        }

        SmtpClient smtpClient = new SmtpClient();
        string host = ConfigurationManager.AppSettings[User.SMTP_SERVER_KEY];
        if (null == host || host.Length == 0)
        {
            throw new ConfigurationErrorsException("SMTP hostname not found");
        }
        smtpClient.Host = host;

        string port_str = ConfigurationManager.AppSettings[User.SMTP_PORT_KEY];
        if (null == port_str || port_str.Length == 0)
        {
            smtpClient.Port = User.DEFAULT_SMTP_PORT;
        }
        else
        {
            smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings[User.SMTP_PORT_KEY]);
        }

        string username = ConfigurationManager.AppSettings[User.SMTP_USER_KEY];
        string password = ConfigurationManager.AppSettings[User.SMTP_PASSWORD_KEY];
        if (username != null && password != null)
        {
            smtpClient.Credentials = new NetworkCredential(username, password);
        }

        MailAddress to = new MailAddress(personInfo.Email);
        MailAddress from;

        if (ConfigurationManager.AppSettings[User.EMAIL_FROM_KEY] == null)
        {
            from = new MailAddress(User.DEFAULT_EMAIL_FROM);
        }
        else
        {
            from = new MailAddress(ConfigurationManager.AppSettings[User.EMAIL_FROM_KEY]);
        }

        MailMessage message = new MailMessage(from, to);

        string subject = ConfigurationManager.AppSettings[User.EMAIL_SUBJECT_KEY];
        if (subject != null && subject != String.Empty)
        {
            message.Subject = subject;
        }
        else
        {
            message.Subject = User.DEFAULT_EMAIL_SUBJECT;
        }

        message.IsBodyHtml = false;

        message.Body = String.Format(User.EMAIL_BODY_TEMPLATE, userInfo.Login, userInfo.Password);

        smtpClient.Send(message);

        return true;
    }

    public static void ChangePassword(int userId, 
            string oldPassword,
            string newPassword)
    {
        User user = User.FindByPrimaryKey(userId);
        if ( null != user ) {
            if ( user.Password != oldPassword ) {
                throw new Exception("Invalid current password.");
            }

            user.Password = newPassword;

            Database.Begin();

            try
            {
                User.Update(user);
            }
            catch (Exception ex)
            {
                Database.Rollback();
                throw ex;
            }
            
            Database.Commit();

        } else {
            throw new Exception(string.Format("User with id {0} is not found", userId));
        }
    }

    public static void Save(User user)
    {

        Database.Begin();

        try
        {
            if ( 0 != user.UserId ) {
                Person.Update(user.Personal);
                User.Update(user);
                UserPreference.Update(user.Preference);
                UserRole.DeleteByUser(user);
            } else {
                Person.Insert(user.Personal);

                user.PersonId = user.Personal.PersonId;
                User.Insert(user);

                user.Preference.UserId = user.UserId;
                UserPreference.Insert(user.Preference);
            }

            foreach (Role role in user.RoleList) {
                UserRole ur = new UserRole();
                ur.RoleId = role.RoleId;
                ur.UserId = user.UserId;
                UserRole.Insert(ur);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        
        Database.Commit();
    }

    private static User GetUserByLogin(string login)
    {
        string sql = String.Format(User.SQL_SELECT_BY_LOGIN, login);

        User userInfo = null;
        using (
            IDataReader dataReader =
                Database.PrepareCommand(sql).ExecuteReader())
        {
            if (dataReader.Read())
            {
                userInfo = Load(dataReader);
            }
        }
        return userInfo;
    }
    
/*    
    private const string SQL_CREATE =
        @"
        INSERT INTO [User] (
            Login,
            Password,
            IsActive,
            HackingAttempts,
            NewTracts,
            DefaultSite
            ) 
        VALUES ( '{0}', '{1}', {2}, {3}, {4}, '{5}')

        select scope_identity();
    ";
    public static User Create(User userInfo)
    {
        string sql = String.Format(SQL_CREATE,
                                   userInfo.Login,
                                   userInfo.Password,
                                   userInfo.IsActive ? 1 : 0,
                                   userInfo.HackingAttempts,
                                   userInfo.NewTracts,
                                   userInfo.DefaultSite);

        userInfo.UserId =
            int.Parse(Database.PrepareCommand(sql).ExecuteScalar().ToString());

        return userInfo;
    }

    private const string SQL_UPDATE =
        @"
        UPDATE [User] set 
            Login = '{0}', 
            Password = '{1}', 
            IsActive = {2},
            HackingAttempts = {3},
            NewTracts = {4}, 
            DefaultSite = '{5}'
        WHERE UserId = {6}
    ";

    public void Update(IDbTransaction tran)
    {
        string sql =
            String.Format(SQL_UPDATE,
                          Login,
                          Password,
                          IsActive ? 1 : 0,
                          HackingAttempts,
                          NewTracts,
                          DefaultSite,
                          UserId);

        Database.PrepareCommand(sql, tran.Connection, tran).ExecuteNonQuery();
    }
*/
    #endregion

/*
                inner join UserRole ur on u.UserId = ur.UserId
                left outer join Company co on p.CompanyId = co.CompanyId
                left outer join Client cl on p.ClientId = cl.ClientId
 */ 

    public static List<User> Search(String login, String firstName, String lastName, int roleId, bool isActive, int companyId, int clientId) {
        String query = User.SQL_SELECT_BY_FILTER;
        query += " and u.IsActive = @IsActive ";
        if ( "" != login ) {
            query += " and u.Login like @Login ";
        }
        if ( "" != firstName ) {
            query += " and p.FirstName like @FirstName ";
        }
        if ( "" != lastName ) {
            query += " and p.LastName like @LastName ";
        }
        if ( 0 != roleId ) {
            query += " and 1=(select 1 from UserRole ur where ur.UserId=u.UserId and ur.RoleId = @RoleId) ";
        }
        if ( 0 != companyId ) {
            query += " and p.CompanyId = @CompanyId ";
        }
        if ( 0 != clientId ) {
            query += " and p.ClientId = @ClientId ";
        }
        using (IDbCommand dbCommand = Database.PrepareCommand(query))
        {
            Database.PutParameter(dbCommand, "@IsActive", isActive);
            if ( "" != login ) {
                Database.PutParameter(dbCommand, "@Login", login);
            }
            if ( "" != firstName ) {
                Database.PutParameter(dbCommand, "@FirstName", firstName);
            }
            if ( "" != lastName ) {
                Database.PutParameter(dbCommand, "@LastName", lastName);
            }
            if ( 0 != roleId ) {
                Database.PutParameter(dbCommand, "@RoleId", roleId);
            }
            if ( 0 != companyId ) {
                Database.PutParameter(dbCommand, "@CompanyId", companyId);
            }
            if ( 0 != clientId ) {
                Database.PutParameter(dbCommand, "@ClientId", clientId);
            }

            List<User> result = new List<User>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    }

}
}
