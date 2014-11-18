using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Setup.About;
using QuickBooksAgent.Windows.UI.Setup.Menu;
using QuickBooksAgent.Windows.UI.Setup.Application;
using QuickBooksAgent.Windows.UI.Setup.Connection;
using QuickBooksAgent.Windows.UI.Setup.Register;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Menu
{
    public class SetupMenuController : SingleFormController<SetupMenuModel, SetupMenuView>
    {
        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_mbConnection.Click += new EventHandler(OnQuickBooksClick);
            View.m_mbApplication.Click += new EventHandler(OnApplicationClick);
            View.m_btnRegister.Click += new EventHandler(OnRegisterClick);
            View.m_btnAbout.Click += new EventHandler(OnAboutClick);
            View.m_menuRegister.Click += new EventHandler(OnRegisterClick);
            View.m_menuApplication.Click += new EventHandler(OnApplicationClick);
            View.m_menuAbout.Click += new EventHandler(OnAboutClick);

            IsDefaultActionExist = false;
            DefaultActionName = "None";
            
            View.m_btnRegister.Select();            
        }

        private void OnRegisterClick(object sender, EventArgs e)
        {
            RegisterController registerController
                = SingleFormController.Prepare<RegisterController>();
            registerController.Closed += new SingleFormClosedHandler(OnRegisterClosed);
            registerController.Execute();
        }

        void OnRegisterClosed(SingleFormController controller)
        {
            View.m_btnRegister.Select();
        }        

        void OnQuickBooksClick(object sender, EventArgs e)
        {
            ConnectionController connectionController
                = SingleFormController.Prepare<ConnectionController>();
            connectionController.Closed += new SingleFormClosedHandler(OnQuickBooksClosed);
            connectionController.Execute();
        }

        void OnQuickBooksClosed(SingleFormController controller)
        {
            View.m_mbConnection.Select();
        }

        void OnApplicationClick(object sender, EventArgs e)
        {
            ApplicationController applicationController
                = SingleFormController.Prepare<ApplicationController>();

            applicationController.Closed += new SingleFormClosedHandler(OnApplicationClosed);
            applicationController.Execute();
        }

        void OnApplicationClosed(SingleFormController controller)
        {
            View.m_mbApplication.Select();
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutController aboutController
                = SingleFormController.Prepare<AboutController>();

            aboutController.Closed += new SingleFormClosedHandler(OnAboutClosed);
            aboutController.Execute();
        }

        void OnAboutClosed(SingleFormController controller)
        {
            View.m_btnAbout.Select();
        }
        
    }
}
