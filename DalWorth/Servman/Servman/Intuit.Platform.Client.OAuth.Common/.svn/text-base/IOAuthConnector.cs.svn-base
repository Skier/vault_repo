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
using Intuit.Platform.Client.Core;

namespace Intuit.Platform.Client.OAuth.Common
{
	/// <summary>
	/// Lists all the useful methods exposed by <see cref="OAuthConnector"/>.
	/// </summary>
	public interface IOAuthConnector : IRequestAuthorizer
	{
		/// <summary>
		/// Calls <see cref="ExchangeAppTokenForConsumerKeyAndSecret"/> if <see cref="MissingConsumerKeyOrSecret"/> is true.
		/// </summary>
		void RequestConsumerKeyIfNeeded();

		/// <summary>
		/// Whether or not the key store has the Consumer Key and Consumer Secret.
		/// </summary>
		/// <returns>false if the consumer context is known, otherwise true</returns>
		bool MissingConsumerKeyOrSecret();

		/// <summary>
		/// IPP uses a "four-legged" approach, where the Consumer Key and Secret can be retrieved from the server based on a "Parent/Master Consumer Key", which is actually the AppToken.
		/// Call this to query IPP for Consumer Key and Secret based on the Parent Consumer Key (AppToken) and store them in the key store.
		/// </summary>
		void ExchangeAppTokenForConsumerKeyAndSecret();

		/// <summary>
		/// The user needs to sign into Workplace to authorize your application, which in turn will make IPP return a verifier code (which you will have to provide to <see cref="ExchangeVerifierForAccessToken"/>. Use this function to retrieve the URL. You should then display a web browser with this URL.
		/// There's two ways to get the verifier back:
		/// 1. IPP will display the verifier on the last page. You can provide a text box and ask the user to copy the verifier code from that last page and paste it into your text box.
		/// 2. Provide a <paramref name="callbackUrl"/>. IPP will display a big button on the last page ("Return to [application name]"), and redirect to your callbackUrl when the user clicks on it. It will append a query to it (something like "?oauth_token=blabla&oauth_verifier=blabla"). Use <see cref="ParseGrantPageResponseQuery"/> to extract the verifier from that.
		/// Hold on to <paramref name="oAuthAccessGrantRequestSession"/> until you've received the verifier code.
		/// </summary>
		/// <param name="callbackUrl">null if you will ask the user to copy &amp; paste the verifier from the last page, or a URL you will intercept to extract the verifier from</param>
		/// <param name="oAuthAccessGrantRequestSession">temporary session information you should hold on to and provide to <see cref="ExchangeVerifierForAccessToken"/>.</param>
		/// <returns>the URL to show in a web browser</returns>
		string GetGrantPageUrl(string callbackUrl, out IOAuthAccessGrantRequestSession oAuthAccessGrantRequestSession);

		/// <summary>
		/// If you chose to retrieve the verifier using a callback Url, this will parse it from the query string appended by IPP. Only pass in the query part, not the full Url.
		/// </summary>
		/// <param name="query">the query string appended by IPP to your callback Url</param>
		/// <returns>the verifier code</returns>
		string ParseGrantPageResponseQuery(string query);

		/// <summary>
		/// When the user copies &amp; pastes the verfier code from the grant page into your app (or you've received it via the callback Url) this function helps verify the code. You can use the <paramref name="message"/> to tell the user what's wrong about the verifier.
		/// </summary>
		/// <param name="verifier">the verifier returned by grant page</param>
		/// <param name="message">the error message describing the problem with the verifier, or null if there's no problem</param>
		/// <returns>true if the verifier is valid, false if invalid</returns>
		bool ValidateVerifier(string verifier, out string message);

		/// <summary>
		/// Retrieves the Access Token and Secret from IPP's OAuth service. Requires that the user retrieved the <paramref name="verifierCode"/> (see <see cref="GetGrantPageUrl"/>).
		/// </summary>
		/// <param name="iOAuthAccessGrantRequestSession">The request session that you were provided when you called <see cref="GetGrantPageUrl"/></param>
		/// <param name="verifierCode">the verifier code you retrieved, either because the user copied &amp; pasted it from the grant page into your application or because you received a callback on the callbackUrl you provided to <see cref="GetGrantPageUrl"/></param>
		void ExchangeVerifierForAccessToken(IOAuthAccessGrantRequestSession iOAuthAccessGrantRequestSession, string verifierCode);

		/// <summary>
		/// Whether or not the key store has the Access Token and Secret.
		/// </summary>
		/// <returns>false if both access token and token secret are known</returns>
		bool MissingAccessTokenOrSecret();

		/// <summary>
		/// Whether or not we're authenticated. True if both <see cref="MissingConsumerKeyOrSecret"/> and <see cref="MissingAccessTokenOrSecret"/> report false.
		/// </summary>
		bool IsAuthenticated { get; }

		/// <summary>
		/// Just used by the IDSTestApp
		/// </summary>
		string GetAuthQueryParamsForGET(Uri destinationURL);

		/// <summary>
		/// Just used by the IDSTestApp
		/// </summary>
		string GetAuthHeaderForGET(Uri destinationURL);
	}
}