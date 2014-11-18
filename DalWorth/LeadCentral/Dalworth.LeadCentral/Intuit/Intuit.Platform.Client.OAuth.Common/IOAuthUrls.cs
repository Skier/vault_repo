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

namespace Intuit.Platform.Client.OAuth.Common
{
	/// <summary>
	/// Knows the Urls involved in the authentication process. You can use <see cref="ProductionWorkplaceOAuthUrls"/> if you don't need to access another environment.
	/// </summary>
	public interface IOAuthUrls
	{
		/// <summary>
		/// IPP specific: the URL where to get the Consumer Key and Secret from in exchange for the AppToken. The string should contain a {0} where the AppToken is expected.
		/// </summary>
		string DynamicKeyRetrievalUrl { get; }
		/// <summary>
		/// The Request Token URL, used to obtain an unauthorized Request Token.
		/// </summary>
		string RequestTokenUrl { get; }
		/// <summary>
		/// The User Authorization URL, used to obtain User authorization for Consumer access (also called the Grant Page)
		/// </summary>
		string AuthorizeRequestUrl { get; }
		/// <summary>
		/// The Access Token URL, used to exchange the User-authorized Request Token for an Access Token
		/// </summary>
		string AccessTokenUrl { get; }
	}
}
