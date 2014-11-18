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
	public class ProductionWorkplaceOAuthUrls : IOAuthUrls
	{
		public string DynamicKeyRetrievalUrl
		{
			get
			{
				return "https://oauth.intuit.com/oauth/v1/create_consumer?appToken={0}";
			}
		}

		public string RequestTokenUrl
		{
			get
			{
				return "https://oauth.intuit.com/oauth/v1/get_request_token";
			}
		}

		public string AuthorizeRequestUrl
		{
			get
			{
				return "https://workplace.intuit.com/AuthMgr/Authorize";
			}
		}

		public string AccessTokenUrl
		{
			get
			{
				return "https://oauth.intuit.com/oauth/v1/get_access_token";
			}
		}
	}
}