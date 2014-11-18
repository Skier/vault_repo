using System;
using System.Configuration;

namespace Dalworth.Server.Web.RugCleaning
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

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            string fullOrigionalpath = Request.Url.ToString();

            if (!(fullOrigionalpath.Contains("www.dalworthrugcleaning.com") || fullOrigionalpath.Contains("localhost")))
            {
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", "http://www.dalworthrugcleaning.com");
            }

            string rewriteURL = string.Empty;

            string fullPath = fullOrigionalpath.ToLower();
            string offer= string.Empty;

            if (fullOrigionalpath.Contains("offer"))
            {
                int slashIdx = fullPath.IndexOf("/offer");
                if (slashIdx > 0 && fullPath.Length > slashIdx + 6)
                {
                    offer = fullPath.Substring(slashIdx + 6, fullPath.Length - slashIdx - 6);
                    rewriteURL = "Index.aspx";
                }
            }
            else if (!(fullOrigionalpath.Contains(".html") ||
                fullOrigionalpath.Contains(".css") ||
                fullOrigionalpath.Contains(".aspx") || fullOrigionalpath.Contains(".xml")))
                return;

            if (fullOrigionalpath.Contains(Link.SITE_MAP_XML))
            {
                Context.RewritePath("SitemapXml.aspx");
                return;
            }

            if (fullOrigionalpath.ToLower().Contains("ie.css"))
            {
                Context.RewritePath("ie.css");
                return;
            }
            if (fullOrigionalpath.ToLower().Contains("style.css"))
            {
                Context.RewritePath("/style.css");
                return;
            }


            if (fullOrigionalpath.ToLower().Contains(".aspx"))
            {
                if (fullOrigionalpath.ToLower().Contains("rug-cleaning.aspx"))
                {
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", Link.PROCESS);
                    return;
                }

                if (fullOrigionalpath.ToLower().Contains("about.aspx") || fullOrigionalpath.ToLower().Contains("contact.aspx"))
                {
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", Link.CONTACT_US);
                    return;
                }

                if (fullOrigionalpath.ToLower().Contains("rug-repairs.aspx"))
                {
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", Link.REPAIRS);
                    return;
                }
            }

            string key = Request.Params.Get("key");
            string keyword = Request.Params.Get("keyword");

            if (offer==string.Empty)
                offer= Request.Params.Get("offer");

            if (!fullOrigionalpath.ToLower().Contains(".html") && string.IsNullOrEmpty(rewriteURL))
                return;

            if (string.IsNullOrEmpty(rewriteURL))
            {
                if (fullPath.Contains(Link.HOME))
                    rewriteURL = "Index.aspx";
                else if (fullPath.Contains(Link.PROCESS))
                    rewriteURL = "RugCleaningProcess.aspx";
                else if (fullPath.Contains(Link.CUSTOMER_REVIEWS))
                    rewriteURL = "CustomerReviews.aspx";
                else if (fullPath.Contains(Link.VIDEO_DALWORTH_RUG_CLEANING))
                    rewriteURL = "VideoRugCleaning.aspx";
                else if (fullPath.Contains(Link.CONTACT_US))
                    rewriteURL = "ContactUs.aspx";
                else if (fullPath.Contains(Link.HOME_CARE))
                    rewriteURL = "HomeCare.aspx";
                else if (fullPath.Contains(Link.FAQ))
                    rewriteURL = "Faq.aspx";
                else if (fullPath.Contains(Link.REPAIRS))
                    rewriteURL = "RugRepairs.aspx";
                else if (fullPath.Contains(Link.PROTECTION))
                    rewriteURL = "RugProtection.aspx";
                else if (fullPath.Contains(Link.REFERRAL))
                    rewriteURL = "ServicePartners.aspx";
                else if (fullPath.Contains(Link.EMERGENCY))
                    rewriteURL = "RugEmergency.aspx";
                else if (fullPath.Contains(Link.RUG_PAD))
                    rewriteURL = "RugPad.aspx";
                else if (fullPath.Contains(Link.PLANO_RUG_CLEANING))
                    rewriteURL = "RugCleaningPlano.aspx";
                else if (fullPath.Contains(Link.SOUTHLAKE_RUG_CLEANING))
                    rewriteURL = "RugCleaningSouthlake.aspx";
                else if (fullPath.Contains(Link.FRISCO_RUG_CLEANING))
                    rewriteURL = "RugCleaningFrisco.aspx";
                else if (fullPath.Contains(Link.INTERESTING_LINKS))
                    rewriteURL = "InterestingLinks.aspx";
                else if (fullPath.Contains(Link.PERSIAN_RUG_CLEANING))
                    rewriteURL = "RugCleaningPersian.aspx";
                else if (fullPath.Contains(Link.AREA_RUG_CLEANING))
                    rewriteURL = "RugCleaningArea.aspx";
                else if (fullPath.Contains(Link.SITE_MAP))
                    rewriteURL = "SiteMap.aspx";
                else if (fullPath.Contains(Link.ORIENTAL_RUG_CLEANING))
                    rewriteURL = "RugCleaningOriental.aspx";
                else if (fullPath.Contains(Link.ALLEN_RUG_CLEANING))
                    rewriteURL = "RugCleaningAllen.aspx";
                else if (fullPath.Contains(Link.CARROLLTON_RUG_CLEANING))
                    rewriteURL = "RugCleaningCarrollton.aspx";
                else if (fullPath.Contains(Link.COLLEYVILLE_RUG_CLEANING))
                    rewriteURL = "RugCleaningColleyville.aspx";
                else if (fullPath.Contains(Link.COPPELL_RUG_CLEANING))
                    rewriteURL = "RugCleaningCoppell.aspx";
                else if (fullPath.Contains(Link.ARLINGTON_RUG_CLEANING))
                    rewriteURL = "RugCleaningArlington.aspx";
                else if (fullPath.Contains(Link.FLOWER_MOUND_RUG_CLEANING))
                    rewriteURL = "RugCleaningFlowerMound.aspx";
                else if (fullPath.Contains(Link.GRAPEVINE_RUG_CLEANING))
                    rewriteURL = "RugCleaningGrapevine.aspx";
                else if (fullPath.Contains(Link.HIGHLAND_PARK_RUG_CLEANING))
                    rewriteURL = "RugCleaningHighlandPark.aspx";
                else if (fullPath.Contains(Link.KELLER_RUG_CLEANING))
                    rewriteURL = "RugCleaningKeller.aspx";
                else if (fullPath.Contains(Link.MANSFIELD_RUG_CLEANING))
                    rewriteURL = "RugCleaningMansfield.aspx";
                else if (fullPath.Contains(Link.MCKINNEY_RUG_CLEANING))
                    rewriteURL = "RugCleaningMcKinney.aspx";
                else if (fullPath.Contains(Link.RICHARDSON_RUG_CLEANING))
                    rewriteURL = "RugCleaningRichardson.aspx";
                else if (fullPath.Contains(Link.DALLAS_RUG_CLEANING))
                    rewriteURL = "RugCleaningDallas.aspx";
                else if (fullPath.Contains(Link.FORT_WORTH_RUG_CLEANING))
                    rewriteURL = "RugCleaningFortWorth.aspx";
                else if (fullPath.Contains(Link.KARASTAN_RUG_CLEANING))
                    rewriteURL = "RugCleaningKarastan.aspx";
                else if (fullPath.Contains(Link.CUSTOMER_SURVEY))
                    rewriteURL = "Feedback.aspx";
                else if (fullPath.Contains(Link.PRIVACY_POLICY))
                    rewriteURL = "PrivacyPolicy.aspx";
            }

            string parameters = string.Empty;

            if (key != null && key.Length > 0)
            {
                if (parameters != string.Empty)
                    parameters = parameters + "&";
                else
                    parameters = parameters + "?";

                parameters = parameters + "key=" + key;
            }

            if (keyword != null && keyword.Length > 0)
            {
                if (parameters != string.Empty)
                    parameters = parameters + "&";
                else
                    parameters = parameters + "?";

                 parameters = parameters + "keyword=" + System.Web.HttpUtility.UrlEncode(keyword);
            }

            if (offer != null && offer.Length > 0)
            {
                if (parameters != string.Empty)
                    parameters = parameters + "&";
                else
                    parameters = parameters + "?";

                parameters = parameters + "offer=" + System.Web.HttpUtility.UrlEncode(offer);
            }

            rewriteURL = rewriteURL + parameters;

            Context.RewritePath(rewriteURL);
        } 

    }
}