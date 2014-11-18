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
	///		This class represents a logical network.
	/// </summary>
	/// <remarks>
	///		This is part of the <see cref="ConnectionMonitor"/>.
	///		You can check if a network <see cref="IsConnected"/> and subscribe to a 
	///		<see cref="StateChanged"/> event.
	/// </remarks>
    public class Network
    {
        ConnectionMonitor monitor;
		private string name;
		private bool isConnected;
		EventHandler<ActiveNetworkChangedEventArgs> onActiveNetworkChanged;

		/// <summary>
		/// Constructor for the <see cref="Network"/> class.
		/// </summary>
		/// <param name="name"></param>
		public Network(string name)
        {
            this.name = name;
        }

		/// <summary>
		/// Get the network's name.
		/// </summary>
        public string Name
        {
            get { return name; }
        }

		/// <summary>
		/// Get the connection status of the network.
		/// </summary>
        public bool IsConnected
        {
            get { return isConnected; }
        }

		/// <summary>
		/// Set the <see cref="ConnectionMonitor"/>.
		/// </summary>
		/// <param name="monitor"></param>
        public void SetMonitor(ConnectionMonitor monitor)
        {
			Guard.ArgumentNotNull(monitor, "monitor");

			this.monitor = monitor;
			onActiveNetworkChanged = OnActiveNetworkChanged;
			this.monitor.ActiveNetworkChanged += OnActiveNetworkChanged;
        }

		/// <summary>
		/// This event is fired when the state has changed.
		/// </summary>
		public event EventHandler<StateChangedEventArgs> StateChanged;

		private void OnActiveNetworkChanged(object sender, ActiveNetworkChangedEventArgs args)
		{
			bool previousStatus = isConnected;
			isConnected = (args.Active && args.Name == this.name);
			if (StateChanged != null && previousStatus != isConnected)
			{
				StateChanged(this, new StateChangedEventArgs(isConnected, name));
			}
		}


		internal void DetachMonitor(ConnectionMonitor connectionMonitor)
		{
			if (onActiveNetworkChanged != null)
				connectionMonitor.ActiveNetworkChanged -= onActiveNetworkChanged;
		}
	}
}
