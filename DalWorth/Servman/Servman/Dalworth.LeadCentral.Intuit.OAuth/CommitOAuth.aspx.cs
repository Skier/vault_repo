using System;
using Servman.Service;

namespace Dalworth.LeadCentral.Intuit.OAuth
{
    public partial class CommitOAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var oAuthContext = (OAuthContext)Context.Session[OAuthContext.OAuthContextKey];
            if (oAuthContext == null)
                throw new Exception("No OAuthContext found.");

            var verifier = oAuthContext.Connector.ParseGrantPageResponseQuery(Request.QueryString.ToString());

            string error;
            if (!oAuthContext.Connector.ValidateVerifier(verifier, out error))
                throw new Exception(error);

            oAuthContext.RequestAccessToken(verifier);
            oAuthContext.SetCurrentConnectionActive();

            Response.Redirect("Finish.aspx");
        }
    }
}