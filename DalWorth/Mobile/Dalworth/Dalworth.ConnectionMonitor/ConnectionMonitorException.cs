//===============================================================================
// Microsoft patterns & practices
// Mobile Client Software Factory - July 2006
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.ConnectionMonitor
{
	/// <summary>
	/// Exception thrown by the ConnectionMonitor getting connectivity information.
	/// </summary>
	public class ConnectionMonitorException : Exception
	{
		/// <summary>
		/// Constructor for the ConnectionMonitorException class.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="hResult">Native HResult value if the exception has been thrown because a native API error.</param>
		public ConnectionMonitorException(string message, int hResult)
			: base(message)
		{
			HResult = hResult;
		}
	}
}
