using System;
using log4net;
using log4net.Repository.Hierarchy;

namespace Dalworth.LeadCentral.Intuit.OAuth
{
    public partial class _Default : System.Web.UI.Page
    {
        private const string RequestOAuthUrl = "RequestOAuth.aspx?realmId=182801782";
        private static readonly ILog Log = LogManager.GetLogger(typeof(Logger));

        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info("Default:Page_Load - redirect to request");
            Response.Redirect(RequestOAuthUrl);
        }
    }
}