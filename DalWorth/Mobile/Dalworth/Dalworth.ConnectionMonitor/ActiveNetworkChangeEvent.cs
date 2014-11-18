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
	///		when the connection status for the network has changed.
	/// </summary>
	public class ActiveNetworkChangedEventArgs : EventArgs
    {
		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="active">
		///		The current connection state of the network.
		/// </param>
		/// <param name="name">
		///		The active network name.
		/// </param>
        public ActiveNetworkChangedEventArgs(bool active, string name)
        {
            this.name = name;
            this.active = active;
        }

        private string name;
        private bool active;

		/// <summary>
		/// True if the event has been fired because of connection became active.
		/// Otherwise false.
		/// </summary>
        public bool Active
        {
            get { return active; }
        }

		/// <summary>
		/// New current network name if the event has been fired because of connection became true.
		/// Otherwise empty string.
		/// </summary>
        public string Name
        {
            get { return name; }
        }
    }
}
