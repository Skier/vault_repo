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
	///		This abstract class implements an API - based connection type
	///		without the SystemProperty and the connection type to implement.
	/// </summary>
	/// <remarks>
	///		This class uses the SystemState managed API to monitor the connection status
	///		change, and also uses the <see cref="ConnectionMonitorNativeHelper"/> class which is a native API 
	///		wrapper to get the network list and the active network for this connection.
	///		The concrete implementations should provide the SystemProperty to monitor and 
	///		the expected connection type from the API wrapper.
	/// </remarks>
	public abstract class SystemStateConnectionBase : Connection, IDisposable
	{
		protected bool isConnected;
		protected SystemState handledProperty;
		protected NativeConnectionType nativeConnectionType;
		protected SystemProperty systemProperty;

		/// <summary>
		///		Default logic for the construction of concrete classes derived from the <see cref="ApiConnection"/> 
		///		abstract class.
		/// </summary>
		/// <param name="connectionType">
		///		Provides the connection type name for the connection type object.
		/// </param>
		/// <param name="price">
		///		Provides the price for the connection type object.
		/// </param>
		/// <param name="connType">
		///		<see cref="ConnMonitorApi.CurrentConnectionType"/> expected from the wrapper class
		///		when the connection gets connected and the networkname is retrieved.
		/// </param>
		/// <param name="systemProperty">
		///		SystemProperty which must be monitored by the ApiConnection to fire 
		///		the status changed event and the connected status.
		/// </param>
		protected SystemStateConnectionBase(string name, uint price, NativeConnectionType connType, SystemProperty systemProperty)
			: base(name, price)
		{
			this.nativeConnectionType = connType;
			this.systemProperty = systemProperty;
			handledProperty = new SystemState(this.systemProperty);
			isConnected = ((int)handledProperty.CurrentValue > 0);
			handledProperty.Changed += OnPropertyChange;
		}


		protected SystemStateConnectionBase(string name, uint price)
			: base(name, price)
		{
		}

		/// <summary>
		///		Get the connection status for the connection type.
		/// </summary>
		public override bool IsConnected
		{
			get { return isConnected; }
		}

		private void OnPropertyChange(object sender, ChangeEventArgs args)
		{
			string networkName = string.Empty;
			bool newConnectedStatus = ((int)args.NewValue > 0);

			if (newConnectedStatus != isConnected)
			{
				isConnected = newConnectedStatus;
				if (isConnected)
				{
					networkName = GetNetworkName();
				}
				StateChangedEventArgs arg = new StateChangedEventArgs(isConnected, networkName);
				RaiseStateChanged(arg);
			}
		}

		/// <summary>
		///		Gets the network name for the connection.
		/// </summary>
		/// <returns>String with the network name for the connection. If it's not connected returns an empty string.</returns>
		public override string GetNetworkName()
		{
			return ConnectionMonitorNativeHelper.GetCurrentNetworkNameForConnection(nativeConnectionType);
		}

		/// <summary>
		///		Gets detailed info about the connection status.
		/// </summary>
		/// <returns>
		///		String with detailed connection status information.
		///	</returns>
		public override string GetDetailedInfoString()
		{
			return ConnectionMonitorNativeHelper.GetConnectionDetailedStatusText(this.nativeConnectionType);
		}

		/// <summary>
		/// Disposes the resources used by the connection object.
		/// </summary>
		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				handledProperty.Changed -= OnPropertyChange;
				handledProperty.Dispose();
			}
		}

		~SystemStateConnectionBase()
		{
			Dispose(false);
		}
	}
}
