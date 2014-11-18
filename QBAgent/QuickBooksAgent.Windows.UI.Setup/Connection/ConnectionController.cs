using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Setup.Connection
{
    public class ConnectionController:SingleFormController<Configuration.QuickBooksConfiguration,ConnectionView>
    {
        protected override void OnModelInitialize(object[] data)
        {
            m_model = Configuration.QuickBooks;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            View.m_defaultLogin.Text = Model.DefaultLogin;
            View.m_ticket.Text = Model.ConnectionTicket;

            View.m_chbSecure.Checked = false;
            
            InitDefaultAction("Cancel", true);
        }

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_ticket.Focus();
        }

        public override void OnDefaultAction()
        {
            View.Destroy();
        }

        protected override bool OnSave()
        {
            bool connectionKeyChanged = Model.ConnectionTicket != View.m_ticket.Text;

            Model.DefaultLogin = View.m_defaultLogin.Text;
            Model.ConnectionTicket = View.m_ticket.Text;

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    Configuration.Save();
                }

                SessionModel.Instance.LoginInfo.Login =
                    Model.DefaultLogin;

                SessionModel.Instance.SkipPasswordRequest = false;

                if (connectionKeyChanged)
                    SessionModel.Instance.SessionTicket = null;

            }
            catch (Exception e)
            {
                EventService.AddEvent(new QuickBooksAgentException("Unable to save config file", e));

                return false;
            }

            return true;
        }
    }
}
