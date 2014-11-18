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
	///		Arguments with connectivity information for an event fired
	///		when the connection status has changed.
	/// </summary>
    public class StateChangedEventArgs : EventArgs
    {
        private bool isConnected;
        private string networkName;

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="isConnected">
		///		The current connection status.
		/// </param>
		/// <param name="networkName">
		///		The active network name.
		/// </param>
        public StateChangedEventArgs(bool isConnected, string networkName)
        {
            this.isConnected = isConnected;
            this.networkName = networkName;
        }

		/// <summary>
		/// Gets the new connection status when the event has been fired.
		/// </summary>
        public bool IsConnected
        {
            get { return isConnected; }
        }


		/// <summary>
		/// Gets the current network name when the event has been fired.
		/// </summary>
        public string NetworkName
        {
            get { return networkName; }
        }
	
    }
}
