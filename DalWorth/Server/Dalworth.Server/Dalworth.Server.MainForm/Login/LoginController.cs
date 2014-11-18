using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.ServmanSync;
using Dalworth.Server.MainForm.Dashboard;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Login
{
    public class LoginController : Controller<LoginModel, LoginView>
    {
        #region Form

        public Form Form
        {
            get { return View; }
        }

        #endregion

        #region IsLoggedIn

        private bool m_isLoggedIn;
        public bool IsLoggedIn
        {
            get { return m_isLoggedIn; }
        }

        #endregion

        #region IsLoggedIn

        private Employee m_employee;
        public Employee Employee
        {
            get { return m_employee; }
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;            
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtUserName.Text = Configuration.LastLogin;
        }

        #endregion

        #region OnViewShow

        protected override void OnViewShow(object sender, EventArgs e)
        {
            View.m_txtPassword.Focus();
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {            
//            DigiumRequestProcessor.MatchTransactionsToCalls();
//            MessageBox.Show("Done");
//            return;

            if (View.m_txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Please enter Login", "Login is empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_txtUserName.Focus();
                return;
            }

            LoginResult loginResult = Model.GetLoginResult(View.m_txtUserName.Text, View.m_txtPassword.Text);

            if (loginResult == LoginResult.Allowed)
            {
                m_isLoggedIn = true;
                m_employee = Model.Employee;
                Configuration.CurrentDispatchId = Model.Employee.ID;

                Configuration.LastLogin = View.m_txtUserName.Text;
                Configuration.Save();

                View.Destroy();                
            } else 
            {
                if (loginResult == LoginResult.LoginOrPasswordIncorrect)
                    MessageBox.Show("Login or Password is incorrect", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Your access is temporary disabled. Please contact system administrator", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

                View.m_txtPassword.Clear();
                View.m_txtPassword.Focus();
                return;                                
            }
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isLoggedIn = false;
            View.Destroy();
        }

        #endregion

    }
}
