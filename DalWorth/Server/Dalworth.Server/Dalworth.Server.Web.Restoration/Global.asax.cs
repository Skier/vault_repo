using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Dalworth.Server.Domain;
using Dalworth.Server.Data;

namespace Dalworth.Server.Web.Restoration
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Dalworth.Server.SDK.Configuration.ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            Dalworth.Server.SDK.Configuration.Login = ConfigurationManager.AppSettings["Login"];
            Dalworth.Server.SDK.Configuration.Password = ConfigurationManager.AppSettings["Password"];
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        /*
         *  Allow to add key like index.html/1234    
         */
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string fullOrigionalpath = Request.Url.ToString();

            if (!(fullOrigionalpath.Contains(".html") || fullOrigionalpath.Contains(".css") ||
               fullOrigionalpath.Contains(".aspx") || fullOrigionalpath.Contains(".xml")))
            {
                if (!fullOrigionalpath.Contains("partner"))
                    return;
            }
                

            string relativePath = Request.AppRelativeCurrentExecutionFilePath;

            string rewriteURL;

            try
            {
                rewriteURL = Request.ApplicationPath + "/" + GetRewriteUrl(relativePath);
            }
            catch (DataNotFoundException ex)
            {
                return;
            }
            
            Context.RewritePath(rewriteURL);
        }

        private string GetRewriteUrl(string relativePath)
        {
            if (relativePath.EndsWith(".css") || relativePath.EndsWith(".png"))
            {
                if (relativePath.StartsWith("~/"))
                    relativePath = relativePath.Substring(2, relativePath.Length - 2);
                return relativePath;
            }

            if(!(( relativePath.Contains("Service.aspx") || relativePath.Contains("ErrorPage.aspx"))))
            {
                return "ErrorPage.aspx";
            }

            if (relativePath.Contains(".aspx"))
            {
                int lastSlashIdx = relativePath.LastIndexOf("/");

                if (lastSlashIdx > 0)
                {
                    relativePath = relativePath.Substring(lastSlashIdx + 1);
                }
                else if (relativePath.StartsWith("~/"))
                    relativePath = relativePath.Substring(2, relativePath.Length - 2);

                return relativePath;
            }


            string newUrl = "Index.html";

            relativePath = relativePath.ToLower();

            if (relativePath.Contains("sitemap.xml"))
            {
                newUrl = "SiteMapXml.aspx";
            }
            else if (relativePath.Contains("thank-you.html"))
            {
                newUrl = "ThankYou.aspx";
            }
            else if (relativePath.Contains("site-map.html"))
            {
                newUrl = "SiteMapPage.aspx";
            }
            else if (relativePath.Contains("customer-survey.html"))
            {
                newUrl = "Feedback.aspx";
            }
            else
            {
                if (relativePath.StartsWith("~/"))
                    relativePath = relativePath.Substring(2, relativePath.Length - 2);

                if (relativePath.Contains("partner"))
                    relativePath = "partner.html";

                try
                {
                    WebSiteArticle article;
                    using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                    {
                        connection.Open();
                        article = WebSiteArticle.FindByURL(relativePath, connection);
                    }

                    switch (article.WebSiteArticleTypeId)
                    {
                        case 1:
                            newUrl = "Index.aspx?articleid=" + article.ID;
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            newUrl = "LandingPage.aspx?articleid=" + article.ID;
                            break;
                    }
                }
                catch (DataNotFoundException ex)
                {
                    if (RedirectOldPages())
                        throw ex;

                    newUrl = "ErrorPage.aspx";
                }
            }

            string keyword = Request.Params.Get("keyword");

            string parameters = string.Empty;

            if (keyword != null && keyword.Length > 0)
                newUrl = newUrl + "&keyword=" + System.Web.HttpUtility.UrlEncode(keyword);

            return newUrl;
        }

        private bool RedirectOldPages()
        {
            bool result = false;

            string currentUrl = Request.AppRelativeCurrentExecutionFilePath.ToLower();

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                if (currentUrl.Contains("floodrestoration.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(2, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (currentUrl.Contains("firerestoration.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(15, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (currentUrl.Contains("moldcleanup.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(3, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (currentUrl.Contains("contentsrestoration.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(16, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (currentUrl.Contains("structuralrestoration.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(17, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (
                    currentUrl.Contains("aboutdalworthrestoration.html")
                    || currentUrl.Contains("dalworthrestorationcareers.html")
                    || currentUrl.Contains("dalworthrestorationcareers.html")
                    || currentUrl.Contains("dalworthrestorationinsurancetraining.html")
                    || currentUrl.Contains("residentialservice.html")
                    || currentUrl.Contains("emergencyservice.html")
                    )
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(1, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (currentUrl.Contains("contactdalworthrestoration.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(14, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (currentUrl.Contains("insuranceinformation.html"))
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(11, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }

                if (
                    currentUrl.Contains("commercialservice.html")
                    || currentUrl.Contains("environmentstabilization.html")
                    || currentUrl.Contains("documentsanddata.html")
                    || currentUrl.Contains("largecommercialloss.html")
                    )
                {
                    Response.Status = "301 Moved Permanently";
                    WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(9, connection);
                    Response.AddHeader("Location", article.Url);
                    return true;
                }
            }

            return result;
        }
    }
}