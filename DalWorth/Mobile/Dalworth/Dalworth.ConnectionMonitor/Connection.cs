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
	///		This abstract class represents the connection type interface.
	/// </summary>
	///	<remarks>
	///		Concrete implementations should provide connection status and
	///		current Network name when get connected or disconnected firing a 
	///		StateChanged event.
	/// </remarks>
    public abstract class Connection
    {
		private string connectionTypeName;
		private uint price;

		/// <summary>
		///		This method raises the StateChanged event from a connection concrete 
		///		implementation.
		/// </summary>
		/// <param name="arg">
		///		Status change information in an <see cref="StateChangedEventArgs"/> object.
		/// </param>
		protected void RaiseStateChanged(StateChangedEventArgs arg)
		{
			if (StateChanged != null)
				StateChanged(this, arg);
		}

		/// <summary>
		///		Default functionality for constructors of concrete derived classes of the <see cref="Connection"/> class.
		/// </summary>
		/// <param name="connectionTypeName">
		///		Provides the connection type name for the connection type object.
		/// </param>
		/// <param name="price">
		///		Provides the price for the connection type object.
		/// </param>
        protected Connection(string connectionTypeName, uint price)
        {
            this.connectionTypeName = connectionTypeName;
            this.price = price;
        }

		/// <summary>
		///		Event fired when something in the connection status has changed.
		/// </summary>
		/// <remarks>
		///		After subscribing to this event, the <see cref="UpdateStatus"/> method
		///		should be called to get the initial status from the connection type
		///		if it's already connected (firing this event).
		/// </remarks>
        public event EventHandler<StateChangedEventArgs> StateChanged;

		/// <summary>
		///		String containing the connection type name.
		/// </summary>
        public string ConnectionTypeName
        {
            get { return connectionTypeName; }
        }

		/// <summary>
		///		Pricing information for the connection.
		/// </summary>
        public uint Price
        {
            get { return price; }
            set { price = value; }
        }

		/// <summary>
		///		Get the connection status for the connection type.
		/// </summary>
        public abstract bool IsConnected { get;}

		/// <summary>
		///		Update the StateChanged event subscribers if the connection is already connected.
		/// </summary>
		public void UpdateStatus()
		{
			if (IsConnected)
			{
				StateChangedEventArgs arg = new StateChangedEventArgs(true, GetNetworkName());
				RaiseStateChanged(arg);
			}
		}

		/// <summary>
		///		Get the current network name from the connection from the concrete implementations.
		/// </summary>
		/// <returns>
		///		A string with the current network name if the connection is connected, 
		///		or an empty string if it's not.
		/// </returns>
		public abstract string GetNetworkName();

		/// <summary>
		///     Returns a string containing detailed information about the connection 
		/// </summary>
		/// <returns></returns>
		public abstract string GetDetailedInfoString();

    }
}
