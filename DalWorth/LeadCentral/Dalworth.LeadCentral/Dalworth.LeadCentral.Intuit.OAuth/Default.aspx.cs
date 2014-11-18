using System;

namespace Dalworth.LeadCentral.Intuit.OAuth
{
    public partial class _Default : System.Web.UI.Page
    {
        private const string RequestOAuthUrl = "RequestOAuth.aspx?realmId=182801782";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(RequestOAuthUrl);
        }
    }
}