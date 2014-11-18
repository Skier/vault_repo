using System;
using System.IO;
using System.Net;
using System.Text;

namespace Intuit.Common.Util
{
	/// <summary>
	/// A helper class to create and manage HTTP requests and responses
	/// </summary>
	public class HttpRequestHelper
	{
		/// <summary>
		/// Set this to an instance of IWebProxy to make the factory method use that proxy, otherwise it will use WebRequest.GetSystemWebProxy() (Internet Explorer's settings essentially) with CredentialCache.DefaultCredentials.
		/// </summary>
		public static IWebProxy Proxy { get; set; }

		/// <summary>
		/// Simply converts the string to a Uri and calls <see cref="CreateHttpRequest(Uri)"/>
		/// </summary>
		public static HttpWebRequest CreateHttpRequest(string uri)
		{
			return CreateHttpRequest(new Uri(uri));
		}

		/// <summary>
		/// Creates a HttpRequest with either the default proxy information or (if present) the proxy defined in the <see cref="Proxy"/> property.
		/// </summary>
		public static HttpWebRequest CreateHttpRequest(Uri uri)
		{
			// Uncomment this line if you want to be able to talk to a dev server with a bad certificate. NEVER use this outside of development and testing.
			//ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			AddProxy(webRequest);
			return webRequest;
		}

		///<summary>
		///Adds the <see cref="Proxy"/> information to the given <paramref name="webRequest"/>.
		///</summary>
		public static void AddProxy(WebRequest webRequest)
		{
			if (Proxy != null)
			{
				webRequest.Proxy = Proxy;
			}
			else
			{
				IWebProxy proxy = WebRequest.GetSystemWebProxy();
				proxy.Credentials = CredentialCache.DefaultCredentials;
				webRequest.Proxy = proxy;
			}
		}

		/// <summary>
		/// If the provided exception was caused by a HttpWebRequest, returns the HTTP status code and the description of that status.
		/// </summary>
		/// <param name="exception">the WebException returned by the HTTP request</param>
		/// <param name="statusCode">the HTTP status code</param>
		/// <param name="statusDescription">a description of the status code</param>
		/// <param name="onlyProtocolErrors">If true, will only report a status if the exception status is ProtocolError.</param>
		/// <returns>true if a status code was found, false if the exception didn't point to one</returns>
		public static bool GetHttpStatus(Exception exception, out HttpStatusCode statusCode, out string statusDescription, bool onlyProtocolErrors)
		{
			WebException wrapped = exception as WebException;
			if (wrapped != null)
			{
				if (!onlyProtocolErrors || wrapped.Status == WebExceptionStatus.ProtocolError)
				{
					HttpWebResponse response = wrapped.Response as HttpWebResponse;
					if (response != null)
					{
						statusCode = response.StatusCode;
						statusDescription = response.StatusDescription;
						return true;
					}
				}
			}
			statusCode = 0;
			statusDescription = null;
			return false;
		}

		/// <summary>
		/// Calls GetHttpStatus on the given exception to extract the HTTP protocol-level error code, and converts the HttpStatusCode to an int.
		/// </summary>
		/// <param name="exception"></param>
		/// <returns>the HttpStatusCode as an int, 0 if there was no status code to extract</returns>
		public static int GetHttpProtocolError(Exception exception)
		{
			HttpStatusCode code;
			string description;
			if (GetHttpStatus(exception, out code, out description, true))
			{
				return (int)code;
			}
			return 0;
		}

		/// <summary>
		/// Dumps all the headers of the given request into the provided StringBuilder.
		/// </summary>
		public static void DumpHttpHeaders(StringBuilder dump, WebRequest request)
		{
			foreach (string header in request.Headers.AllKeys)
			{
				string[] values = request.Headers.GetValues(header);
				if (values != null)
				{
					foreach (string val in values)
					{
						if (val != null)
						{
							dump.Append(header);
							dump.Append("=");
							dump.AppendLine(val);
						}
					}
				}
			}
		}

		/// <summary>
		/// Reads the response from the request and puts it into a string.
		/// </summary>
		/// <param name="webRequest">the request to read from </param>
		/// <returns>the complete response</returns>
		public static string GetResponseText(WebRequest webRequest)
		{
			using (Stream responseStream = webRequest.GetResponse().GetResponseStream())
			{
				using (StreamReader responseReader = new StreamReader(responseStream))
				{
					return responseReader.ReadToEnd();
				}
			}
		}
	}
}