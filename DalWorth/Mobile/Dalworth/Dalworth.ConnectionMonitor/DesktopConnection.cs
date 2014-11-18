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
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile;

namespace Dalworth.ConnectionMonitor
{

	/// <summary>
	///		Concrete <see cref="ApiConnection"/> implementation for desktop
	///		connections (as DTPT)
	/// </summary>
	public class DesktopConnection : SystemStateConnectionBase
    {
		/// <summary>
		///		Constructor for the <see cref="DesktopConnection"/> class.
		/// </summary>
		/// <param name="connectionType">
		///		Provides the connection type name for the connection type object.
		/// </param>
		/// <param name="price">
		///		Provides the price for the connection type object.
		/// </param>
		public DesktopConnection(string name, uint price)
            : base(name, price,NativeConnectionType.PC,SystemProperty.ConnectionsDesktopCount)
        {
		}
    }

}
