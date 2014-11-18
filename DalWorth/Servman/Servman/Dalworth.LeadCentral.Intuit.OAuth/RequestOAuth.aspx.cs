using System;
using System.Configuration;
using Servman.Service;

namespace Dalworth.LeadCentral.Intuit.OAuth
{
    public partial class RequestOAuth : System.Web.UI.Page
    {
        private const string RealmIdKey = "realmId";
        private const string CommitOAuthUrlKey = "OAuthCommitUrl";

        protected void Page_Load(object sender, EventArgs e)
        {
            var realmId = Context.Request.QueryString[RealmIdKey];

            if (realmId != null)
            {
                var oAuthContext = OAuthContext.CreateContext(realmId);
                oAuthContext.RequestConsumerKeyIfNeeded();
                
                var grantPageUrl = oAuthContext.GetGrantPageUrl(ConfigurationManager.AppSettings[CommitOAuthUrlKey]);

                Context.Session[OAuthContext.OAuthContextKey] = oAuthContext;
                Context.Session[OAuthContext.RealmIdKey] = realmId;

                Response.Redirect(grantPageUrl);

            } else
            {
                throw new Exception("Realm Id is null ");
            }

        }
    }
}