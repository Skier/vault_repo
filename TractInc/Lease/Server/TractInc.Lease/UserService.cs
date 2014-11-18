using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using TractInc.Lease.Domain;

namespace TractInc.Lease
{
    public class UserService
    {
        #region ConfigurationSettings

        private const string SMTP_SERVER_KEY = "smtp";
        private const string SMTP_PORT_KEY = "port";
        private const string SMTP_USER_KEY = "username";
        private const string SMTP_PASSWORD_KEY = "password";
        private const string EMAIL_FROM_KEY = "from";
        private const string EMAIL_SUBJECT_KEY = "subject";

        private const int DEFAULT_SMTP_PORT = 25;
        private const string DEFAULT_EMAIL_SUBJECT = "Password recovery";
        private const string EMAIL_BODY_TEMPLATE = @"
    password recovery email header
    ==============================

    login: {0}
    password: {1}

    ==============================
    password recovery email footer ";

        #endregion

        #region Fields

        private UserDataMapper m_userDM;

        #endregion

        #region Methods

        public User Login(string login, string password)
        {
            User user;

            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                user = UserDM.GetUserByLogin(tran, login);

                if (null != user)
                {
                    if (user.Password != password)
                    {
                        user.HackingAttempts++;
                        UserDM.Update(tran, user);

                        throw new Exception("Invalid password");
                    }

                    if (!user.IsActive)
                    {
                        throw new Exception("User is inactive.");
                    }

                }
                else
                {
                    throw new Exception("User not found");
                }

                if (user.HackingAttempts > 0)
                {
                    user.HackingAttempts = 0;
                    UserDM.Update(tran, user);
                }

                tran.Commit();
            }

            return user;
        }

        public bool SendPassword(String login)
        {

            User user;

            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                user = UserDM.GetUserByLogin(tran, login);
                tran.Commit();
            }

            if (null == user)
            {
                throw new Exception("User not found");
            }

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                string host = ConfigurationManager.AppSettings[SMTP_SERVER_KEY];
                if (null == host || host.Length == 0)
                {
                    throw new ConfigurationErrorsException("SMTP hostname not found");
                }
                smtpClient.Host = host;

                string port_str = ConfigurationManager.AppSettings[SMTP_PORT_KEY];
                if (null == port_str || port_str.Length == 0)
                {
                    smtpClient.Port = DEFAULT_SMTP_PORT;
                }
                else
                {
                    smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings[SMTP_PORT_KEY]);
                }

                string username = ConfigurationManager.AppSettings[SMTP_USER_KEY];
                string password = ConfigurationManager.AppSettings[SMTP_PASSWORD_KEY];
                if (username != null && password != null)
                {
                    smtpClient.Credentials = new NetworkCredential(username, password);
                }

                string from = ConfigurationManager.AppSettings[EMAIL_FROM_KEY];
                if (from != null)
                {
                    message.From = new MailAddress(from);
                }

                string subject = ConfigurationManager.AppSettings[EMAIL_SUBJECT_KEY];
                if (subject != null && subject != String.Empty)
                {
                    message.Subject = subject;
                }
                else
                {
                    message.Subject = DEFAULT_EMAIL_SUBJECT;
                }

                message.IsBodyHtml = false;

                message.Body = String.Format(EMAIL_BODY_TEMPLATE, user.Login, user.Password);

                smtpClient.Send(message);

            }
            catch (Exception exception)
            {
                throw new Exception("Send failed", exception);
            }

            return true;
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
