using System.Web;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web
{
    /// <summary>
    /// Summary description for GetDynamicPhoneNumber
    /// </summary>
    public class GetDynamicPhoneNumber : IHttpHandler
    {

        private const string Script = @"
document.getElementById('lead-central-dynamic-phone').innerHTML = '{0}';
";
        public void ProcessRequest(HttpContext context)
        {
            var realmId = context.Request["realmId"];
            var leadSourceId = int.Parse(context.Request["leadSource"]);
            var userHostAddress = context.Request.UserHostAddress;
            var pageUrl = context.Request.UrlReferrer.AbsoluteUri;
            var referralUri = context.Request["ref"];

            if (referralUri != null)
                referralUri = referralUri.Replace("@@@", "&");
            
            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);
            var phone = PhoneService.GetDynamicWebPhone(servmanCustomer, leadSourceId, userHostAddress, pageUrl, referralUri);

            context.Response.ContentType = "text/plain";
            if (phone != null)
                context.Response.Write(string.Format(Script, phone.ScreenNumber));
            else
                context.Response.Write("No phones");
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}