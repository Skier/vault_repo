using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Web.Partner.Models
{
    public class PartnerMembershipProvider : MembershipProvider
    {
        public static WebUser GetCurrentUser()
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                return WebUser.FindByLogin(HttpContext.Current.User.Identity.Name, connection);
            }            
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (string.IsNullOrEmpty(name))
                name = "PartnerMembershipProvider";
            base.Initialize(name, config);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {            
            if (password.Length < 6)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;                
            }

            WebUser webUser;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                webUser = WebUser.FindByLogin(username, connection);
            }
            
            if (webUser != null)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            WebUser dbUser = new WebUser(0, null, username, Hash.ComputeHash(password), null, 
                string.Empty, string.Empty, email, false);

            try
            {
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                {
                    connection.Open();
                    WebUser.Insert(dbUser, connection);
                }
                
                status = MembershipCreateStatus.Success;
            }
            catch (Exception)
            {
                status = MembershipCreateStatus.UserRejected;
            }

            return GetUser(username, false);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            WebUser possibleUser;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                possibleUser = WebUser.FindByLogin(username, connection);
            }
            
            if (possibleUser == null)
                return false;
            if (Hash.ComputeHash(password) == possibleUser.PasswordHash)
                return true;
            return false;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            WebUser possibleUser;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                possibleUser = WebUser.FindByLogin(username, connection);
            }

            if (possibleUser == null)
                return null;

            return new MembershipUser(Name, username, null, possibleUser.Email,
                string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now,
                DateTime.Now, DateTime.Now);
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}