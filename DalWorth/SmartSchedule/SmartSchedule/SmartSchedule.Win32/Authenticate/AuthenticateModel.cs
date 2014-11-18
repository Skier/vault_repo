using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.Authenticate
{
    public class AuthenticateModel : IModel
    {
        #region Init

        public void Init()
        {
        }

        #endregion

        #region Authenticate

        public string Authenticate(string password)
        {
            string passwordHash = Hash.ComputeHash(password);
            UserResult result = WcfClient.WcfClient.Instance.FindUser(passwordHash);
            if (result.User != null)
                User.Authenticate(result.User);
            else
                return result.Error;
            return string.Empty;
        }

        #endregion
    }
}
