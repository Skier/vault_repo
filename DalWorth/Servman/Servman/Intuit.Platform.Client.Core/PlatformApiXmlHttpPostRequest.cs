/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Intuit.Common.Util;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Encapsulates the XML over a HTTP POST request made to the platform backend.
	/// </summary>
	public class PlatformApiXmlHttpPostRequest
	{
		private const string ApiActionHeader = "QUICKBASE-ACTION";
		private const string ContentType = "application/xml";
		private const string Method = "POST";

		/// <summary>
		/// <see cref="ApiRequestLogger"/>
		/// </summary>
		public const string Utf8 = "utf8";

		/// <summary>
		/// <see cref="ApiRequestLogger"/>
		/// </summary>
		public const string Utf16 = "utf16";

		private const string LoggedApiCallExtension = ".txt";
		private const string LoggedApiCallPrefixRequest = "req-";
		private const string LoggedApiCallPrefixResponse = "res-";
		private readonly string m_Action;
		private readonly IPlatformHost m_Host;
		private readonly PlatformApiRequestXmlDocument m_ReqDoc;
		private readonly string m_RequestID;
		private readonly HttpWebRequest m_Request;

		/// <summary>
		/// The kind of delegate you have to implement so you can hook it up to <see cref="LogApiRequest"/>
		/// </summary>
		/// <param name="data">a byte array containing the request or response</param>
		/// <param name="requestId">the unique id of the request/response pair</param>
		/// <param name="isRequest">true if this was a request, false if it's a response</param>
		/// <param name="apiAction">the HTTP header indicating which API was invoked</param>
		/// <param name="encoding">the logger can get called twice for the same item, one time with the internal UTF-16 encoded request and one time after it converts the request to UTF-8 about to be sent via HTTP. This parameter will be "utf8" (<see cref="PlatformApiXmlHttpPostRequest.Utf8"/>) or "utf16" (<see cref="PlatformApiXmlHttpPostRequest.Utf16"/>) depending on the encoding of the byte buffer.</param>
		public delegate void ApiRequestLogger(byte[] data, string requestId, bool isRequest, string apiAction, string encoding);

		///<summary>
		/// Called twice for each request (once with UTF-16 and once with UTF-8 encoded data), and once for the response (with UTF-16 encoded data).
		///</summary>
		public event ApiRequestLogger LogApiRequest;

		/// <summary>
		/// Whether or not to add diagnostic details to the notify callback.
		/// </summary>
		public bool TrackRequestDetailsForErrorLogging { get; set; }

		/// <summary>
		/// The workplace cookie, if one was found in the response.
		/// </summary>
		public string WorkplaceCookie { get; private set; }

		/// <summary>
		/// The document containing the request.
		/// </summary>
		public PlatformApiRequestXmlDocument ReqDoc
		{
			get
			{
				return m_ReqDoc;
			}
		}

		/// <summary>
		/// The WebRequest used to send this API call
		/// </summary>
		internal HttpWebRequest Request
		{
			get
			{
				return m_Request;
			}
		}

		/// <summary>
		/// Instantiates a request to the given platform host, application DBID and API-action.
		/// </summary>
		/// <seealso cref="PlatformApiXmlHttpPostRequest(IPlatformHost,string,string,string,PlatformApiRequestXmlDocument)"/>
		public PlatformApiXmlHttpPostRequest(IPlatformHost host, string dbid, string action)
			: this(host, dbid, action, null, null)
		{
		}

		/// <summary>
		/// Instantiates a request to the given platform host, application DBID and API-action.
		/// </summary>
		/// <param name="host">A valid platform host</param>
		/// <param name="dbid">main or the application/database instance ID</param>
		/// <param name="action">one of the API acions understood by the platform</param>
		/// <param name="requestId">unique ID for the request/response. If you pass in null, will use a timestamp based on DateTime.UtcNow</param>
		/// <param name="request">if you already have a request document you can pass it in, otherwise just pass in null</param>
		public PlatformApiXmlHttpPostRequest(IPlatformHost host, string dbid, string action, string requestId, PlatformApiRequestXmlDocument request)
		{
			m_Host = host;
			string url = host.MakeDbidUrl(dbid).ToString();
			m_Request = HttpRequestHelper.CreateHttpRequest(url);
			m_Request.Headers.Add(ApiActionHeader, action);
			m_Action = action;
			m_Request.ContentType = ContentType;
			m_Request.Method = Method;
			m_Request.CookieContainer = new CookieContainer();
			m_RequestID = requestId ?? DateTime.UtcNow.Ticks.ToString();
			m_ReqDoc = request ?? new PlatformApiRequestXmlDocument(m_RequestID);
		}

		/// <summary>
		/// Executes the request including logging and error handling.
		/// </summary>
		/// <param name="notify">callback delegate for diagnostic notifications</param>
		/// <returns>the response from the platform</returns>
		/// <exception cref="System.Net.WebException">The time-out period for the request expired.-or- An error occurred while processing the request.</exception>
		public XmlDocument ExecuteRequest(WorkNotification notify)
		{
			byte[] requestContentUtf8 = CreateRequestData();
			XmlDocument respXML = GetResponseXML(notify, requestContentUtf8);
			HandleErrors(m_Host, respXML);
			return respXML;
		}

		/// <summary>
		/// Check the response for any errors it might indicate. Will throw an exception derived from PlatformApiXmlHttpError if API response indicates an error.
		/// Will throw an exception derived from PlatformClientException if it has a problem determining success or error.
		/// </summary>
		/// <param name="host">passed into the exception that would be thrown on error</param>
		/// <param name="responseXml">the QuickBase response to examine</param>
		private static void HandleErrors(IPlatformHost host, XmlNode responseXml)
		{
			XmlNode errCodeNode = responseXml.SelectSingleNode("//errcode");

			if (errCodeNode == null)
			{
				throw new PlatformClientException(host, Resources.PlatformClientException_ExceptionMissingErrcodeElement_API_response_without_errcode_element_);
			}

			int errorCode;
			if (!Int32.TryParse(errCodeNode.InnerText, out errorCode))
			{
				throw new PlatformClientException(host, String.Format(Resources.PlatformClientException_ExceptionNonNumericErrorCode_Error_code___0___not_numeric_, errorCode));
			}

			if (errorCode == 0)
			{
				// 0 indicates success
				return;
			}

			XmlNode errTextNode = responseXml.SelectSingleNode("//errtext");
			if (errTextNode == null)
			{
				throw new PlatformApiXmlHttpError(host, errorCode, String.Format(Resources.PlatformClientException_ExceptionErrorNoText_Error__0_, errorCode));
			}

			string errorText = errTextNode.InnerText;
			XmlNode errDetailNode = responseXml.SelectSingleNode("//errdetail");
			string errorDetail = errDetailNode != null ? errDetailNode.InnerText : null;
			if (!string.IsNullOrEmpty(errorDetail))
			{
				throw new PlatformApiXmlHttpError(host, errorCode, String.Format(Resources.PlatformClientException_ExceptionErrorDetail__0___Error__1___Detail___2__, errorText, errorCode, errorDetail));
			}
			throw new PlatformApiXmlHttpError(host, errorCode, String.Format(Resources.PlatformClientException_ExceptionErrorNoDetail__0___Error__1__, errorText, errorCode));
		}

		/// <summary>
		/// Executes the HTTP request, fixes the response encoding if necessary, then loads an XmlDocument with the response.
		/// Logs information to the notify delegate if provided and enabled by diagnostic settings.
		/// </summary>
		/// <param name="notify"></param>
		/// <param name="requestContentUtf8"></param>
		/// <returns></returns>
		/// <exception cref="System.Net.WebException">The time-out period for the request expired.-or- An error occurred while processing the request.</exception>
		private XmlDocument GetResponseXML(WorkNotification notify, ICollection<byte> requestContentUtf8)
		{
			if (notify != null)
			{
				notify(NotificationLevel.DiagnosticLog, string.Format(Resources.PlatformApiXmlHttpPostRequest_GetResponseXML_Invoking_API_call__0__1_, m_Request.Headers[ApiActionHeader], (requestContentUtf8 != null ? Resources.PlatformApiXmlHttpPostRequest_GetResponseXML___content_length__ + requestContentUtf8.Count : string.Empty)), false, null);
			}

			if (notify == null || !TrackRequestDetailsForErrorLogging)
			{
				return GetResponseXMLRaw();
			}

			try
			{
				return GetResponseXMLRaw();
			}
			catch (WebException)
			{
				StringBuilder dump = new StringBuilder();
				dump.AppendFormat(Resources.PlatformApiXmlHttpPostRequest_GetResponseXML_Request_to__0__failed_, m_Request.Address);
				dump.AppendLine();
				dump.AppendLine();
				dump.AppendLine(Resources.PlatformApiXmlHttpPostRequest_GetResponseXML_Headers_);
				HttpRequestHelper.DumpHttpHeaders(dump, m_Request);
				if (requestContentUtf8 != null)
				{
					dump.AppendLine();
					dump.AppendLine(Resources.PlatformApiXmlHttpPostRequest_GetResponseXML_See_source_of_this_summary_for_request_content_);
					dump.AppendLine("<div style=\"display:none\">");
					dump.Append(requestContentUtf8);
					dump.AppendLine();
					dump.AppendLine("</div>");
				}
				notify(NotificationLevel.DiagnosticLog, dump.ToString(), false, null);
				throw;
			}
		}

		/// <summary>
		/// Executes the HTTP request, fixes the response encoding if necessary, then loads an XmlDocument with the response.
		/// If there is an Auth Cookie from Workplace we store that away as it can be used by IDS in certain
		/// scenarios.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.Net.WebException">The time-out period for the request expired.-or- An error occurred while processing the request.</exception>
		private XmlDocument GetResponseXMLRaw()
		{
			string fixedResponse;
			using (WebResponse wr = m_Request.GetResponse())
			{
				ExtractWorkplaceCookie(wr);
				byte[] response = StreamHelper.StreamToByteArray(wr.GetResponseStream());
				wr.Close();
				LogUtf8(response, false);
				fixedResponse = EncodingFixer.FixQuickBaseEncoding(response);
			}
			XmlDocument respXML = new XmlDocument();
			respXML.LoadXml(fixedResponse);
			return respXML;
		}

		/// <summary>
		/// Sniffs the response for "qbn" cookies which we can pass through to IDS in certain scenarios.
		/// </summary>
		/// <param name="wr">The WebResponse</param>
		private void ExtractWorkplaceCookie(WebResponse wr)
		{
			HttpWebResponse hwr = (HttpWebResponse)wr;
			CookieCollection cookieColl = hwr.Cookies;
			StringBuilder qbnCookies = new StringBuilder();
			foreach (Cookie c in cookieColl)
			{
				if (c.Name.StartsWith("qbn."))
				{
					if (qbnCookies.Length > 0)
					{
						qbnCookies.Append("; ");
					}
					qbnCookies.Append(c.Name).Append("=").Append(c.Value);
				}
			}
			WorkplaceCookie = qbnCookies.Length == 0 ? null : qbnCookies.ToString();
		}


		/// <summary>
		/// Takes the request XmlDocument, and initializes the WebRequest's request stream with a UTF8-encoded version.
		/// Returns the request as a UTF8-encoded byte array and/or logs data to disk if requested by diagnostic settings.
		/// </summary>
		/// <returns></returns>
		private byte[] CreateRequestData()
		{
			if (TrackRequestDetailsForErrorLogging || LogApiRequest != null)
			{
				string requestContentUtf16 = ReqDoc.OuterXml;
				LogUtf16(Encoding.Unicode.GetBytes(requestContentUtf16), true);
				UTF8Encoding utf8 = new UTF8Encoding(false, true);
				byte[] requestContentUtf8 = utf8.GetBytes(requestContentUtf16);
				LogUtf8(requestContentUtf8, true);
				m_Request.ContentLength = requestContentUtf8.Length;
				using (Stream rqStream = m_Request.GetRequestStream())
				{
					rqStream.Write(requestContentUtf8, 0, requestContentUtf8.Length);
				}
				return requestContentUtf8;
			}

			using (Stream rqStream = m_Request.GetRequestStream())
			{
				// This will save the request XML as UTF-8 encoded into the WebRequest object.
				// XmlDocument.Save produces UTF-8 by default if no encoding is specified in the XML declaration.
				// Since QuickBase doesn't accept an XML declaration in the request, we leave it off.
				// But we are happy about the coincidence that .NET's default matches the platform's expectation.
				ReqDoc.Save(rqStream);
			}
			return null;
		}

		/// <summary>
		/// Creates a logger you can use to attach to <see cref="LogApiRequest"/>. This logger will write all requests and responses to the file system.
		/// </summary>
		/// <param name="loggedReqResFilePrefix">Path and file name prefix for the files this logger writes the data to</param>
		/// <returns>an instance of <see cref="ApiRequestLogger"/> you can attach to <see cref="LogApiRequest"/></returns>
		public static ApiRequestLogger CreateDiskLogger(string loggedReqResFilePrefix)
		{
			return delegate(byte[] utf8Data, string requestId, bool isRequest, string apiAction, string encoding)
			       {
			       	using (FileStream fs = new FileStream(loggedReqResFilePrefix + requestId + (isRequest ? LoggedApiCallPrefixRequest : LoggedApiCallPrefixResponse) + apiAction + "-" + encoding + LoggedApiCallExtension, FileMode.CreateNew, FileAccess.Write))
			       	{
			       		fs.Write(utf8Data, 0, utf8Data.Length);
			       	}
			       };
		}

		private void LogUtf8(byte[] utf8Content, bool isRequest)
		{
			if (LogApiRequest != null)
			{
				LogApiRequest(utf8Content, m_RequestID, isRequest, m_Action, Utf8);
			}
		}

		private void LogUtf16(byte[] utf16Content, bool isRequest)
		{
			if (LogApiRequest != null)
			{
				LogApiRequest(utf16Content, m_RequestID, isRequest, m_Action, Utf16);
			}
		}
	}
}