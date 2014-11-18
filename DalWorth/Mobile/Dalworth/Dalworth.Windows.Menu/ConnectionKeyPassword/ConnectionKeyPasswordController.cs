using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.SDK;
using Dalworth.Windows.Menu.MainMenu;

namespace Dalworth.Windows.Menu.ConnectionKeyPassword
{
    public class ConnectionKeyPasswordController : SingleFormController<ConnectionKeyPasswordModel, ConnectionKeyPasswordView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = IsRightActionExist = true;
            LeftActionName = "OK";
            RightActionName = "Exit";

            View.m_txtPassword.Focus();
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            if (View.m_txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please enter password");
                return;
            }

            string key;
            try
            {
                key = Crypto.Decrypt(View.m_txtPassword.Text, Configuration.ConnectionKeyEnc);
            }
            catch (Exception e)
            {                
                MessageBox.Show("Password is incorrect, try again");
                View.m_txtPassword.Text = string.Empty;
                Host.Trace("ConnectionKeyPasswordController:Decrypt", e.Message + e.StackTrace);
                return;
            }

            Configuration.ConnectionKey = key;            
            MainMenuController controller = Prepare<MainMenuController>();
            View.Destroy();
            controller.Execute();            
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            MainFormController.Instance.Form.Close();            
        }

        #endregion
    }
}
