/*
 * Copyright (c) 2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 * Contributors:
 *
 *    Intuit Partner Platform - initial contribution 
 *
 */

using System;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Intuit.Common.Util;
using Intuit.Platform.Client.OAuth.Common;

namespace Intuit.Platform.Client.OAuth
{
	/// <summary>
	/// The central class that wraps the DevDefined OAuth library for use with <see cref="Intuit.Platform.Client.Core.PlatformSessionContext"/>.
	/// Call the constructor with your set of Urls and your key store, then assign it to the PlatformSessionContext (before that you should set the AppToken on the PlatformSessionContext).
	/// </summary>
	public class OAuthConnector : IOAuthConnector
	{
		private const int VerifierLength = 7;
		private readonly IOAuthUrls m_Urls;
		private readonly IOAuthKeyStore m_KeyStore;

		public OAuthConnector(IOAuthUrls urls, IOAuthKeyStore keyStore)
		{
			m_Urls = urls;
			m_KeyStore = keyStore;
		}

		public bool ValidateVerifier(string verifier, out string message)
		{
			if (String.IsNullOrEmpty(verifier))
			{
				message = "Missing verifier code";
				return false;
			}
			if (verifier.Trim().Length != VerifierLength)
			{
				message = "Invalid verifier code - must be 7 characters long";
				return false;
			}
			message = null;
			return true;
		}

		public void ExchangeAppTokenForConsumerKeyAndSecret()
		{
			if (String.IsNullOrEmpty(m_KeyStore.ParentConsumerKey))
			{
				throw new ArgumentException("Need to provide non-null non-empty parentConsumerKey");
			}
			String url = String.Format(m_Urls.DynamicKeyRetrievalUrl, m_KeyStore.ParentConsumerKey);
			var webReq = HttpRequestHelper.CreateHttpRequest(url);
			webReq.Method = "GET";
			webReq.ContentType = "application/xml";
			string responseText = HttpRequestHelper.GetResponseText(webReq);
			// Example String to parse
			//oauth_consumer_secret=UKJ2VuYT0sPQg6SxBaqdsIs1xmNB0uRF0ylMIaRT&oauth_consumer_key=SAxUOTqo3Dr53XiSs5FzYrtrr
			if (String.IsNullOrEmpty(responseText))
			{
				throw new ArgumentException("Invalid server response");
			}
			NameValueCollection values = WebHelper.ParseQueryString(responseText);
			string dynamicConsumerSecret = values.Get("oauth_consumer_secret");
			string dynamicConsumerKey = values.Get("oauth_consumer_key");
			m_KeyStore.ConsumerKey = dynamicConsumerKey;
			m_KeyStore.ConsumerSecret = dynamicConsumerSecret;
		}

		public string GetGrantPageUrl(string callbackUrl, out IOAuthAccessGrantRequestSession oAuthAccessGrantRequestSession)
		{
			IOAuthSession session = CreateOAuthSession(false);
			IToken requestToken = session.GetRequestToken();
			oAuthAccessGrantRequestSession = new AccessGrantRequestSession
			                                 {
			                                 	OAuthSession = session,
			                                 	RequestToken = requestToken
			                                 };
			return session.GetUserAuthorizationUrlForToken(requestToken, callbackUrl);
		}

		public void ExchangeVerifierForAccessToken(IOAuthAccessGrantRequestSession iOAuthAccessGrantRequestSession, string verifierCode)
		{
			AccessGrantRequestSession accessGrantRequestSession = iOAuthAccessGrantRequestSession as AccessGrantRequestSession;
			if (accessGrantRequestSession == null)
			{
				throw new ArgumentException("Invalid IOAuthAccessGrantRequestSession", "iOAuthAccessGrantRequestSession");
			}
			if (String.IsNullOrEmpty(verifierCode) || verifierCode.Trim().Length != VerifierLength)
			{
				throw new Exception("Missing or invalid Verifier Code");
			}
			accessGrantRequestSession.RequestAccessToken(m_KeyStore, verifierCode);
		}

		public string GetAuthQueryParamsForGET(Uri destinationURL)
		{
			IOAuthSession oAuthSession = CreateOAuthSession(false);
			oAuthSession.AccessToken = CreateAccessToken();
			return ConsumerRequestExtensions.ForUri(ConsumerRequestExtensions.ForMethod(oAuthSession.Request(), "GET"), destinationURL).SignWithToken().Context.GenerateUri().AbsoluteUri;
		}

		public string GetAuthHeaderForGET(Uri destinationURL)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(destinationURL);
			req.Method = "GET";
			Authorize(req, null);
			return req.Headers.Get("Authorization");
		}

		public void Authorize(WebRequest xrequest, string bodyIfFormEncodedParams)
		{
            IOAuthConsumerContext consumerContext = CreateConsumerContext(true);
			IOAuthSession oAuthSession = CreateOAuthSessionWithConsumerContext(consumerContext);
			oAuthSession.AccessToken = CreateAccessToken();
			string oAuthHeader = GetOAuthHeaderForRequest(oAuthSession, xrequest, bodyIfFormEncodedParams);
			xrequest.Headers.Add("Authorization", oAuthHeader);
		}

		private static string GetOAuthHeaderForRequest(IOAuthSession oAuthSession, WebRequest webRequest, string bodyIfFormEncodedParams)
		{
			IConsumerRequest consumerRequest = oAuthSession.Request();
			consumerRequest = ConsumerRequestExtensions.ForMethod(consumerRequest, webRequest.Method);
			consumerRequest = ConsumerRequestExtensions.ForUri(consumerRequest, webRequest.RequestUri);
			if (webRequest.Headers.Count > 0)
			{
				ConsumerRequestExtensions.AlterContext(consumerRequest, context => context.Headers = webRequest.Headers);
				if (webRequest.Headers[HttpRequestHeader.ContentType] == "application/x-www-form-urlencoded")
				{
					consumerRequest = ConsumerRequestExtensions.WithFormParameters(consumerRequest, HttpUtility.ParseQueryString(bodyIfFormEncodedParams));
				}
			}
			consumerRequest = consumerRequest.SignWithToken();
			return consumerRequest.Context.GenerateOAuthParametersForHeader();
		}

		public void Initialize(string appToken)
		{
			m_KeyStore.ParentConsumerKey = appToken;
		}

		public string ParseGrantPageResponseQuery(string query)
		{
			if (String.IsNullOrEmpty(query))
			{
				throw new ArgumentException("Invalid input");
			}
			NameValueCollection queryParams = WebHelper.ParseQueryString(query);
			//string authToken = queryParams.Get("oauth_token"); // not needed?
			string oauthVerifier = queryParams.Get("oauth_verifier");
			string message;
			if (!ValidateVerifier(oauthVerifier, out message))
			{
				throw new ArgumentException(String.Format("Invalid verifier \"{0}\".", oauthVerifier));
			}
			return oauthVerifier;
		}

		public bool MissingAccessTokenOrSecret()
		{
			return String.IsNullOrEmpty(m_KeyStore.AccessToken) || String.IsNullOrEmpty(m_KeyStore.AccessTokenSecret);
		}

		public bool IsAuthenticated
		{
			get
			{
				return !MissingAccessTokenOrSecret()  && !MissingConsumerKeyOrSecret();
			}
		}

		public void RequestConsumerKeyIfNeeded()
		{
			if (MissingConsumerKeyOrSecret())
			{
				ExchangeAppTokenForConsumerKeyAndSecret();
			}
		}

		public bool MissingConsumerKeyOrSecret()
		{
			return String.IsNullOrEmpty(m_KeyStore.ConsumerKey) || String.IsNullOrEmpty(m_KeyStore.ConsumerSecret);
		}

		private IToken CreateAccessToken()
		{
			return new TokenBase
			{
				Token = m_KeyStore.AccessToken,
				ConsumerKey = m_KeyStore.ConsumerKey,
				TokenSecret = m_KeyStore.AccessTokenSecret
			};
		}

		private IOAuthConsumerContext CreateConsumerContext(bool useHeader)
		{
			IOAuthConsumerContext consumerContext  = new OAuthConsumerContext
			       {
			       	ConsumerKey = m_KeyStore.ConsumerKey,
			       	ConsumerSecret = m_KeyStore.ConsumerSecret,
			       	SignatureMethod = SignatureMethod.HmacSha1,
			       };
			if (useHeader)
			{
				consumerContext.UseHeaderForOAuthParameters = true;
			}
			return consumerContext;
		}
	
		private IOAuthSession CreateOAuthSession(bool useHeader)
		{
			IOAuthConsumerContext consumerContext = CreateConsumerContext(useHeader);
			return CreateOAuthSessionWithConsumerContext(consumerContext);
		}

		private IOAuthSession CreateOAuthSessionWithConsumerContext(IOAuthConsumerContext consumerContext)
		{
			return new OAuthSession(consumerContext,
			                        m_Urls.RequestTokenUrl,
			                        m_Urls.AuthorizeRequestUrl,
			                        m_Urls.AccessTokenUrl);
		}
	}
}