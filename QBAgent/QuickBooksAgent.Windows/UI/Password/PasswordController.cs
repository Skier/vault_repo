using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.QBSDK;

namespace QuickBooksAgent.Windows.UI.Password
{

    public delegate void PasswordControllerCompleteHandler(bool isInfoUpdated);    

    public class PasswordController:SingleFormController<QBLoginInfo,PasswordView>
    {

        public event PasswordControllerCompleteHandler Complete;

        protected override void OnModelInitialize(object[] data)
        {
            m_model = SessionModel.Instance.LoginInfo;
        }

        public override void OnViewActivated()
        {
            base.OnViewActivated();

            View.m_txtPassword.Focus();

            View.m_cmbTransactionHistory.SelectedItem = new DatePeriod(Configuration.QuickBooks.TransactionFreshnessDays);
            if (View.m_cmbTransactionHistory.SelectedIndex == -1)
                View.m_cmbTransactionHistory.SelectedIndex = 2;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            View.m_txtLogin.Text = Model.Login;
            View.m_txtLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnLoginKeyPress);
            View.m_txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnPasswordKeyPress);

            View.m_txtLogin.Visible =
                View.label1.Visible = false;

            View.m_lblTransactionHistory.Visible =
                View.m_cmbTransactionHistory.Visible = IsAskForTransactionFreshness;
            
            
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(null, "All"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(14, "2 Weeks"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(30, "1 Month"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(60, "2 Months"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(90, "3 Months"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(180, "6 Months"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(365, "1 Year"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(730, "2 Years"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(1095, "3 Years"));
        }
        
        private bool IsAskForTransactionFreshness
        {
            get
            {
                return !Database.IsDatabaseExist();
            }
        }

        void OnPasswordKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {                
                if (OnSave())
                    View.Destroy(); 
                else
                {
                    View.m_txtPassword.Text = string.Empty;
                    View.m_txtPassword.Focus();
                }
                    
            }
        }

        void OnLoginKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                View.m_txtPassword.SelectAll();
                View.m_txtPassword.Focus();
            }
        }

        public override bool IsDefaultActionExist
        {
            get
            {
                return true;
            }
        }

        public override string DefaultActionName
        {
            get
            {
                return "Cancel";
            }
        }
        bool m_infoUpdated;

        public bool IsInfoUpdated
        {
            get { return m_infoUpdated; }
        }

        public override void OnDefaultAction()
        {
            View.Destroy();
        }

        protected override bool OnSave()
        {
            if (String.Empty.Equals(View.m_txtPassword.Text)
                || View.m_txtPassword.Text.Length < 1)
            {
                MessageDialog.Show(MessageDialogType.Information,
                    "Please enter password");
                View.m_txtPassword.Focus();
                
                return false;
            }


            Model.Password = View.m_txtPassword.Text;
            Model.Login = View.m_txtLogin.Text;

            try
            {
                Crypto.Decrypt(
                    Model.Password,
                    Configuration.QuickBooks.ConnectionTicket);
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Warning,
                    "Couldn't decrypt connection ticket. Make sure you have entered correct password");
                                
                return false;
            }

            if (IsAskForTransactionFreshness)
            {
                Configuration.QuickBooks.TransactionFreshnessDays =
                    ((DatePeriod) View.m_cmbTransactionHistory.SelectedItem).DaysCount;
                Configuration.Save();                
            }
            
            m_infoUpdated = true;

            return true;            
        }

        public override void OnClose()
        {
            base.OnClose();

            if (Complete != null)
                Complete.Invoke(m_infoUpdated);
        }
    }
}
