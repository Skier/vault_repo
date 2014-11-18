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
using System.Collections.ObjectModel;

namespace Dalworth.ConnectionMonitor
{
	/// <summary>
	///		This class has the responsibility to manage the connections and networks, and
	///		the connected status of the device.
	/// </summary>
	/// <remarks>
	///		The monitor has two collections, a connections collection and a networks collection.
	///		It wires the added connections to get the connection status changes
	///		and to get the current Network.
	///		It also publishes the connection status of the device through the <see cref="IsConnected"/>
	///		property, and an <see cref="ActiveNetworkChanged"/> event to inform the subscribers when
	///		the active network has changed.
	/// </remarks>
    public class ConnectionMonitor
    {
        private ConnectionCollection connections;
        private NetworkCollection networks;
		private Connection activeConnection;
		private Network activeNetwork;
		private EventHandler<StateChangedEventArgs> onStateChanged;



		/// <summary>
		/// This event is fired when the active network has changed.
		/// </summary>
		public EventHandler<ActiveNetworkChangedEventArgs> ActiveNetworkChanged;

		/// <summary>
		/// Constructor of the <see cref="ConnectionMonitor"/> class.
		/// </summary>
        public ConnectionMonitor()
        {
            connections = new ConnectionCollection(this);
            networks = new NetworkCollection(this);
			onStateChanged = OnStateChanged;
        }

		/// <summary>
		/// Get the networks collection.
		/// </summary>
        public NetworkCollection Networks
        {
            get { return networks; }
        }

		/// <summary>
		/// Get the connections collection.
		/// </summary>
        public ConnectionCollection Connections
        {
            get { return connections; }
        }

		/// <summary>
		/// Get the active connection or null if there is not an active one.
		/// </summary>
        public Connection ActiveConnection
        {
            get { return activeConnection; }
        }

        internal void ConnectionAdded(Connection connection)
        {
            connection.StateChanged += onStateChanged;
			connection.UpdateStatus();
		}

		/// <summary>
		/// Get the active connection or null if there is not an active one.
		/// </summary>
		public Network ActiveNetwork
        {
            get { return activeNetwork; }
        }

		/// <summary>
		/// Gets if the device is connected according to the ConnectionMonitor connections.
		/// </summary>
        public bool IsConnected
        {
            get { return (this.activeConnection != null); }
        }


		private void OnStateChanged(object sender, StateChangedEventArgs args)
		{
			//1. Update ActiveConnection

			Connection connection = (Connection)sender;

			if (connection.IsConnected == false)
				this.activeConnection = null;
			else
				this.activeConnection = connection;

			//2. Update ActiveNetwork
			//   Get the assigned Network for the ActiveConnection
			//   and set isConnected Property

			if (this.activeConnection != null && args.NetworkName.Length > 0)
			{
				this.activeNetwork = this.networks[args.NetworkName];
			}
			else
			{
				this.activeNetwork = null;
			}

			if (this.ActiveNetworkChanged != null)
			{
				if (this.ActiveNetwork != null)
					ActiveNetworkChanged(this, new ActiveNetworkChangedEventArgs(true, this.ActiveNetwork.Name));
				else
					ActiveNetworkChanged(this, new ActiveNetworkChangedEventArgs(false, null));
			}
		}

		internal void ConnectionRemoved(Connection connection)
		{
			connection.StateChanged -= onStateChanged;
		}
	}
}
