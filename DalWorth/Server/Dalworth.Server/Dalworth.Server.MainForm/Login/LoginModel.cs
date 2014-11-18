using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Login
{
    public enum LoginResult
    {
        Allowed,
        LoginOrPasswordIncorrect,
        UserInactive
    }

    public class LoginModel
    {
        #region Employee

        private Employee m_employee;
        public Employee Employee
        {
            get { return m_employee; }
        }

        #endregion

        #region GetLoginResult

        public LoginResult GetLoginResult (string userName, string password)
        {
            Employee employee = Employee.FindBy(userName, EmployeeTypeEnum.Dispatch);
            if (employee != null && employee.Password == password)
            {
                if (!employee.IsActive)
                    return LoginResult.UserInactive;

                employee.LoadSecurityPermissions(null);
                m_employee = employee;
                return LoginResult.Allowed;
            } 
            else
            {
                return LoginResult.LoginOrPasswordIncorrect;
            }
        }

        #endregion        
    }
}
