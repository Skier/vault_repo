using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Dalworth.LeadCentral;
using Dalworth.LeadCentral.Service;
using Intuit.Ipp.Saml;

namespace Dalworth.LeadCentral.Intuit.Gateway
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Member Variables
        private const string ModeDevelopment = "DEVELOPMENT";

        //Store this info the web.config or registry
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


        #region Instance Methods

        /// <summary>
        /// Process the SAML request and redirects to target application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("MainApp.aspx");

            if (Request.TotalBytes > 0)
            {
                var myCert = LoadCertificate(PrivateKeyName, PrivateKeyPassword);
                var intuitPublicKey = new X509Certificate2(GetKeyPathName(IntuitPublicKeyName));

                var samlResponse = ServiceProvider.GetSamlResponse(Request, myCert, intuitPublicKey);

                HttpContext.Current.Response.AddHeader("P3P", "CP=\"CAO DSP COR CURa ADMa DEVa PSAa CONo OUR STP BUS PHY ONL UNI FIN COM NAV INT DEM STA\"");

                var dbId = samlResponse.TargetUrl.Substring(samlResponse.TargetUrl.LastIndexOf("=") + 1);
                var realmId = samlResponse.RealmId;
                var intuitTicket = samlResponse.LoginTicket;

                //divOkMessage.Visible = true;
                //divOkMessage.InnerText = "dbid=" + dbId + " | " + "realmId=" + realmId + " | " + "intuitTicket=" + intuitTicket;

                if (RealmIsIncorrect(realmId))
                    Response.Redirect("ErrorPage.htm");

                var ticket = BaseService.Init(intuitTicket, realmId, dbId);

                var redirect = samlResponse.TargetUrl;
                redirect += ("&ticket=" + ticket);

                Response.Redirect(redirect, true);
            }
            else
            {
                if (ConfigurationManager.AppSettings["Mode"] != null
                    && ConfigurationManager.AppSettings["Mode"].ToUpper() == ModeDevelopment)
                {
                    Response.Redirect("ErrorPage.htm");
                }
                else
                {
                    Response.Redirect("FileNotFound.htm");
                }
            }
        }

        private bool RealmIsIncorrect(string realmId)
        {
            return (string.Compare(SDK.StringUtil.ExtractDigits(realmId), realmId) != 0 && realmId.Length == 8);
        }

        /// <summary>
        /// Gets the certificates full path 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private string GetKeyPathName(string keyName)
        {
            return Server.MapPath("~/Keys/" + keyName);
        }
        
        /// <summary>
        /// Gets the certificate based on filepath and password
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private X509Certificate2 LoadCertificate(string fileName, string password)
        {

           string path = GetKeyPathName(fileName);
           if (!File.Exists(path))
               throw new ArgumentException("The certificate file " + fileName + " doesn't exist at path " + path);

           try
           {
               return new X509Certificate2(path, password, X509KeyStorageFlags.MachineKeySet);
           }
           catch (Exception exception)
           {
               throw new ArgumentException("The certificate file " + fileName + " couldn't be loaded - " + exception.Message);
           }
        }

        #endregion
    }
}