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
	/// A place where the varous keys and tokens involved in the process can be stored and retrieved from.
	/// You should provide the key store, and persists the values inbetween application runs.
	/// </summary>
	public interface IOAuthKeyStore
	{
		/// <summary>
		/// An IPP concept, part of the "four-legged" authentication approach. This should be set to your AppToken.
		/// </summary>
		string ParentConsumerKey { get; set; }
		/// <summary>
		/// The Consumer Key, a value used by the Consumer (you) to identify itself to the Service Provider (IPP)
		/// </summary>
		string ConsumerKey { get; set; }
		/// <summary>
		/// The Consumer Secret, a secret used by the Consumer (you) to establish ownership of the Consumer Key.
		/// </summary>
		string ConsumerSecret { get; set; }
		/// <summary>
		/// The Access Token, a value used by the Consumer (you) to gain access to the Protected Resources on behalf of the User, instead of using the User's Service Provider (IPP) credentials
		/// </summary>
		string AccessToken { get; set; }
		/// <summary>
		/// The Token Secret, a secret used by the Consumer (you) to establish ownership of a given Token.
		/// </summary>
		string AccessTokenSecret { get; set; }
	}
}