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

using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Intuit.Platform.Client.OAuth.Common;

namespace Intuit.Platform.Client.OAuth
{
	/// <summary>
	/// Class used internally to keep track of a request session
	/// </summary>
	internal class AccessGrantRequestSession : IOAuthAccessGrantRequestSession
	{
		public IOAuthSession OAuthSession { get; set; }
		public IToken RequestToken { get; set; }

		internal void RequestAccessToken(IOAuthKeyStore store, string verifierCode)
		{
			IToken accessToken = OAuthSession.ExchangeRequestTokenForAccessToken(RequestToken, verifierCode.Trim());
			store.AccessToken = accessToken.Token;
			store.AccessTokenSecret = accessToken.TokenSecret;
		}
	}
}
