using System.Web;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for RedirectCall
    /// </summary>
    public class RedirectCall : IHttpHandler
    {

        const string RedirectCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Dial action=""{0}"" record=""true"">{1}</Dial>
</Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = request["realmId"];
            var callId = request["callId"];
            var phoneNumber = request["phoneNumber"];

            var redirectCallCommitUrl = string.Format("CommitCall.ashx?realmId={0}&amp;callId={1}", realmId, callId);

            response.ContentType = "text/xml";
            response.Write(string.Format(RedirectCallBody, redirectCallCommitUrl, phoneNumber));
            response.End();
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}