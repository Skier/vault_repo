using System;
using System.Configuration;
using Dalworth.LeadCentral.Service;

namespace Dalworth.Intuit.Gateway
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Member Variables
        private const string ModeDevelopment = "DEVELOPMENT";

        private static readonly string PrivateKeyName = ConfigurationManager.AppSettings["privateKeyLoc"];
        private static readonly string PrivateKeyPassword = ConfigurationManager.AppSettings["privateKeyPass"];

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Intuit Production Public Certificate for workplace.intuit.com
        //String IntuitPublicCertificateFileName = "intuit-saml-prd-127688837659061285409865340834996638643.crt";

        //Intuit RTB Public Certificate for beta.workplace.intuit.com
        //String IntuitPublicCertificateFileName = "intuit-saml-e2e-1216145799672.crt";
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private static readonly string IntuitPublicKeyName = ConfigurationManager.AppSettings["IntuitPublicKey"];

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ContextHelper.InitContext();
        }
    }
}
