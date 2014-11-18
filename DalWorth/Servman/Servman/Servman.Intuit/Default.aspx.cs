using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Intuit.Ipp.Saml;

namespace Servman.Intuit
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Member Variables
        private const string UrlSchemeHttps = "HTTPS";
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

                divOkMessage.Visible = true;
                divOkMessage.InnerText = samlResponse.GetXml().OuterXml;

                string redirect = samlResponse.TargetUrl;
                redirect += ("&ServmanRealmId=" + samlResponse.RealmId);
                redirect += ("&ServmanTicket=" + samlResponse.LoginTicket);
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