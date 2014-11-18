using System;
using System.Configuration;
using Servman.Service;

namespace Servman.Intuit
{
    public partial class MainApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitEnvironment();
            Response.Redirect("LeadCentral/LeadCentral.html");
        }

        private void InitEnvironment()
        {
            if (IsPostBack)
                return;

            Session.RemoveAll();
            Session.Add(SessionSettings.APP_TOKEN_KEY, ConfigurationManager.AppSettings["APP_TOKEN"]);
            Session.Add(SessionSettings.DB_ID_KEY, Request.QueryString["dbid"]);
            Session.Add(SessionSettings.REALM_ID_KEY, Request.QueryString["ServmanRealmId"]);
            Session.Add(SessionSettings.TICKET_KEY, Request.QueryString["ServmanTicket"]);
        }

        protected void OnRunClick(object sender, EventArgs e)
        {
            Response.Redirect("LeadCentral/LeadCentral.html");
        }
    }
}