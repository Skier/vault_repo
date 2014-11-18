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

using System.Net;

namespace Intuit.Platform.Client.Core
{
	///<summary>
	///Has the ability to add information to an API request so that it's accepted as authorized by the platform
	///</summary>
	public interface IRequestAuthorizer
	{
		/// <summary>
		/// Add authorizing information to the given <paramref name="request"/>
		/// </summary>
		/// <param name="request">the HTTP request to be authorized</param>
		/// <param name="bodyIfFormEncodedParams">If this is a POST with a content-type of application/x-www-form-urlencoded, supply the body of the POST</param>
		void Authorize(WebRequest request, string bodyIfFormEncodedParams);

		/// <summary>
		/// Called by <see cref="PlatformSessionContext.RequestAuthorizer"/> upon being assigned an instance of this class.
		/// </summary>
		/// <param name="appToken"></param>
		void Initialize(string appToken);
	}
}