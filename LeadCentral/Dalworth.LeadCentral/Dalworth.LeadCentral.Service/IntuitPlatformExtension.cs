using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Dalworth.LeadCentral.Service
{
    public class IntuitPlatformExtension
    {
        private const string RequestTokenPostTemplate = @"<qdbapi>
<appToken>{0}</appToken>
<desc>{1}<desc>
<amount>{2}<amount>
</qdbapi>";
        private const string RequestTokenUri = "https://workplace.intuit.com/api/v1/billing/requesttoken/{0}";
        private const string ChargePostTemplate = @"<qdbapi>
<dbid>{0}</dbid>
<chargeTokenVerifier>{1}</chargeTokenVerifier>
<requestToken>{2}</requestToken>
</qdbapi>";
        private const string ChargeUri = "https://workplace.intuit.com/api/v1/billing/charge/{0}";

        public string DbId { get; private set; }
        public string AppToken { get; private set; }

        public IntuitPlatformExtension(string dbId, string appToken)
        {
            DbId = dbId;
            AppToken = appToken;
        }

        public string GetRequestToken(decimal amount, string description, out string errorMessage)
        {
            try
            {
                string errorCode = null;

                var response = GetRequestTokenResponse(description, amount);
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    var xml = reader.ReadToEnd();
                    var doc = XDocument.Parse(xml);
                    if (doc.Root != null)
                    {
                        var errorCodeElement = doc.Root.Element("ErrorCode");
                        if (errorCodeElement != null)
                            errorCode = errorCodeElement.Value;

                        if (errorCode == "0")
                        {
                            errorMessage = string.Empty;
                            var requestTokenElement = doc.Root.Element("RequestToken");
                            if (requestTokenElement != null)
                                return requestTokenElement.Value;
                        } else
                        {
                            var errorMsgElement = doc.Root.Element("ErrorMessage");
                            errorMessage = errorMsgElement != null ? errorMsgElement.Value : string.Empty;
                            return string.Empty;
                        }
                    }
                }
            }
            catch (CookieException ex)
            {
                errorMessage = ex.Message;
                return string.Empty;
            }

            errorMessage = "Can not parse response from Intuit server";
            return string.Empty;
        }

        public void Charge(string tokenVerifier, string requestToken, out string errorMessage)
        {
            try
            {
                string errorCode = null;

                var response = GetChargeResponse(tokenVerifier, requestToken);
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    var xml = reader.ReadToEnd();
                    var doc = XDocument.Parse(xml);
                    if (doc.Root != null)
                    {
                        var errorCodeElement = doc.Root.Element("ErrorCode");
                        if (errorCodeElement != null)
                            errorCode = errorCodeElement.Value;

                        if (errorCode == "0")
                        {
                            errorMessage = string.Empty;
                            return;
                        }

                        var errorMsgElement = doc.Root.Element("ErrorMessage");
                        errorMessage = errorMsgElement != null ? errorMsgElement.Value : string.Empty;
                        return;
                    }
                }
            }
            catch (CookieException ex)
            {
                errorMessage = ex.Message;
                return;
            }

            errorMessage = "Can not parse response from Intuit server";
        }

        private HttpWebResponse GetRequestTokenResponse(string desc, decimal amount)
        {
            var uri = string.Format(RequestTokenUri, DbId);
            var content = string.Format(RequestTokenPostTemplate, AppToken, desc, amount);

            return GetPostResponse(uri, content);
        }

        private HttpWebResponse GetChargeResponse(string tokenVerifier, string requestToken)
        {
            var uri = string.Format(ChargeUri, DbId);
            var content = string.Format(ChargePostTemplate, DbId, tokenVerifier, requestToken);

            return GetPostResponse(uri, content);
        }

        private static HttpWebResponse GetPostResponse(string uri, string content)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            byte[] postBytes = Encoding.ASCII.GetBytes(content);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            var requestStream = request.GetRequestStream();

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            return (HttpWebResponse)request.GetResponse();
        }

    }
}
