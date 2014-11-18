using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace QuickBooksAgent.QBSDK
{
    public class QBSessionTicket
    {
        #region Fields

        const String SessionKeyURLPattern = "https://login.quickbooks.com/j/qbn/sdkapp/sessionauth2?serviceid=2004&appid={0}";
        const String SecondSessionKeyURLPattern = "https://login.quickbooks.com/j/qbn/sdkapp/connauth?serviceid=2004&appid={0}&conntkt={1}&sessiontkt={2}";
        const String LoginPageURL = "https://login.quickbooks.com/j/qbn/auth/employee/login/";

        private QBConnectionTicket m_connectionTicket;
        String m_ticket;

        public String Ticket
        {
            get { return m_ticket; }
        }

        public QBConnectionTicket ConnectionTicket
        {
            get { return m_connectionTicket; }
        }

        List<String> m_cookieHeadersList = new List<string>();

        DateTime m_ticketCreateDate;
        DateTime m_ticketLastUseDate;

        #endregion

        #region DownloadPage

        private HttpWebResponse DownloadPage(String url, String referer, bool post)
        {

            System.Net.ServicePointManager.CertificatePolicy = new QBCertificatePolicy();

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            httpWebRequest.Method = post ? "POST" : "GET";
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; NetCaptor 7.2.1; .NET CLR 1.0.3705; .NET CLR 1.1.4322)";
            httpWebRequest.AllowAutoRedirect = false;
            

            if (m_cookieHeadersList.Count > 0)
                httpWebRequest.Headers["Cookie"] = CreateCookieHeader();

            if (referer != null)
                httpWebRequest.Referer = referer;


            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            foreach (String s in httpWebResponse.Headers.GetValues("Set-Cookie"))
                m_cookieHeadersList.Add(s);            

            return httpWebResponse;
        }

        #endregion

        #region CreateCookieHeader
        private String CreateCookieHeader()
        {
            Dictionary<String, String> cookies = new Dictionary<string, string>();
            StringBuilder cookieHeader = new StringBuilder();

            foreach (String s in m_cookieHeadersList)
            {
                if (s.IndexOf("debug") != -1)
                    continue;

                if (s.IndexOf('=') > s.IndexOf(';'))
                    continue;

                String name = s.Substring(0, s.IndexOf('='));
                String value = s.Substring(0, s.IndexOf(';'));

                if (cookies.ContainsKey(name))
                {
                    cookies[name] = value;
                }
                else
                    cookies.Add(name, value);
            }

            foreach (String s in cookies.Values)
            {
                cookieHeader.Append(s);
                cookieHeader.Append(";");
            }

            return cookieHeader.ToString();
        }
        #endregion

        #region Reset
        private void Reset()
        {
            m_cookieHeadersList.Clear();
        }
        #endregion

        #region Constructor

        public QBSessionTicket(QBConnectionTicket connectionTicket)
        {
            m_connectionTicket = connectionTicket;
        }

        #endregion

        #region Recieve

        public void Recieve(QBLoginInfo loginInfo)
        {
            Reset();

            String sessionKeyURL = String.Format(SessionKeyURLPattern,
                            m_connectionTicket.AppCode);

            HttpWebResponse loginPageResponse = DownloadPage(sessionKeyURL,null, false);

            String referer = String.Empty;

            loginPageResponse.Close();

            if (loginPageResponse.StatusCode == HttpStatusCode.Redirect)
            {
                referer = loginPageResponse.Headers["Location"];

                loginPageResponse = DownloadPage(referer,
                    loginPageResponse.ResponseUri.ToString(),
                    false);

                loginPageResponse.Close();
            }
            else
            {

                string response = new StreamReader(loginPageResponse.GetResponseStream()).ReadToEnd();

                if (response.IndexOf("An internal error has occurred") != -1)
                    throw new QBException("Internal QuickBooks error, try to check Application Code");


                throw new QBException("Unknown error");
            }



            StringBuilder url = new StringBuilder();
            url.Append(LoginPageURL + "?");

            url.Append(String.Format("{0}={1}&", "service_flags", ""));
            url.Append(String.Format("{0}={1}&", "jsaction", "loginuser"));
            url.Append(String.Format("{0}={1}&", "qbnstatus", "0"));
            url.Append(String.Format("{0}={1}&", "parentid", "-1"));
            url.Append(String.Format("{0}={1}&", "serviceid", ""));
            url.Append(String.Format("{0}={1}&", "auth_url", ""));
            url.Append(String.Format("{0}={1}&", "challenge_req", "false"));

            url.Append(String.Format("{0}={1}&", "glogin", loginInfo.Login));
            url.Append(String.Format("{0}={1}", "password", UrlEncode(loginInfo.Password)));


            HttpWebResponse sessionPageRespose = DownloadPage(url.ToString(),
                                                          referer,
                                                          true);

            if (sessionPageRespose.StatusCode == HttpStatusCode.Redirect)
            {
                sessionPageRespose.Close();

                sessionPageRespose = DownloadPage(sessionPageRespose.Headers["Location"],
                        loginPageResponse.ResponseUri.ToString(),
                        false);
            }

            String responseString = new StreamReader(sessionPageRespose.GetResponseStream()).ReadToEnd();


            Regex regexSessionKey = new Regex(@">.+?<");
            Regex regexTextareaTag = new Regex("<textarea.+?</textarea>", RegexOptions.IgnoreCase);

            sessionPageRespose.Close();

            if (regexTextareaTag.IsMatch(responseString))
            {

                Match match = regexTextareaTag.Match(responseString);

                String sessionKeyValue = regexSessionKey.Match(match.Value).Value;

                sessionKeyValue = sessionKeyValue.Substring(
                        sessionKeyValue.IndexOf('>') + 1,
                        sessionKeyValue.Length - 2);

                String secondSessionKeyURL =
                        String.Format(SecondSessionKeyURLPattern,
                        ConnectionTicket.AppCode,
                        UrlEncode(m_connectionTicket.Ticket),
                        UrlEncode(sessionKeyValue));


                HttpWebResponse sessionKeyWebResponse = DownloadPage(secondSessionKeyURL,
                    null,
                    false);


                String sessionResponseString = new StreamReader(sessionKeyWebResponse.GetResponseStream()).ReadToEnd();

                sessionKeyWebResponse.Close();

                if (!sessionResponseString.StartsWith("000"))
                    throw new QBSessionKeyException(sessionResponseString);

                m_ticket = sessionResponseString.Substring(4);

                m_ticketLastUseDate = DateTime.Now;
                m_ticketCreateDate = DateTime.Now;


            }
            else
            { 
                // trying to detect error

                if (responseString.IndexOf("password you entered does not match") != -1)
                    throw new QBException("Invalid Password");

                if (responseString.IndexOf("login name you entered was not found") != -1)
                    throw new QBException("Invalid Login");
            }

        }
        #endregion

        #region IsExpired
        public bool IsExpired
        {
            get
            {
                return m_ticketLastUseDate.AddHours(1) < DateTime.Now
                       || m_ticketCreateDate.AddDays(1) < DateTime.Now;
            }
        }
        #endregion

        internal void UpdateUsageDate()
        {
            m_ticketLastUseDate = DateTime.Now;
        }

        #region UrlEncode
        public static String UrlEncode(String url)
        {
	        String SAFECHARS = "0123456789" +					// Numeric
					        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +	// Alphabetic
					        "abcdefghijklmnopqrstuvwxyz" +
					        "-_.!~*'()";					// RFC2396 Mark characters
	        String HEX = "0123456789ABCDEF";

            String plaintext = url;
	        String encoded = "";
	        for (int i = 0; i < plaintext.Length; i++ ) {
		        char ch = plaintext[i];
	            if (ch == ' ') {
		            encoded += "+";				// x-www-urlencoded, rather than %20
		        } else if (SAFECHARS.IndexOf(ch) != -1) {
		            encoded += ch;
		        } else {
		            char charCode = ch;
			        if (charCode > 255) {
				        encoded += "+";
			        } else {
				        encoded += "%";
				        encoded += HEX[(charCode >> 4) & 0xF];
				        encoded += HEX[charCode & 0xF];
			        }
		        }
	        } // for

            return encoded;
        }
        #endregion
    }
}
