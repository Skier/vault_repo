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
	///		Concrete <see cref="ApiConnection"/> implementation for cellular
	///		connections (GPRS, 1X, EDGE, etc.)
	/// </summary>
    public class CellConnection : SystemStateConnectionBase
    {
		protected SystemState gprsCoverageState;

		/// <summary>
		///		Constructor for the <see cref="CellConnection"/> class.
		/// </summary>
		/// <param name="connectionType">
		///		Provides the connection type name for the connection type object.
		/// </param>
		/// <param name="price">
		///		Provides the price for the connection type object.
		/// </param>
		public CellConnection(string name, uint price)
			: base(name, price)
        {
			this.nativeConnectionType = NativeConnectionType.Cell;
			this.systemProperty = SystemProperty.ConnectionsCellularCount;
			handledProperty = new SystemState(this.systemProperty);
			gprsCoverageState = new SystemState(SystemProperty.PhoneGprsCoverage);
			UpdateConnectedStatus();
			handledProperty.Changed += OnCellPropertyChange;
			gprsCoverageState.Changed += OnCellPropertyChange;
		}

		private void UpdateConnectedStatus()
		{
			isConnected = ((int)handledProperty.CurrentValue > 0 ||
						   (int)gprsCoverageState.CurrentValue > 0);
		}

		private void OnCellPropertyChange(object sender, ChangeEventArgs args)
		{
			string networkName = string.Empty;
			bool newConnectedStatus = ((int)args.NewValue > 0);

			if (newConnectedStatus != isConnected)
			{
				UpdateConnectedStatus();
				if (isConnected)
				{
					networkName = GetNetworkName();
				}
				StateChangedEventArgs arg = new StateChangedEventArgs(isConnected, networkName);
				RaiseStateChanged(arg);
			}
		}

		public override string GetNetworkName()
		{
			if ((int)handledProperty.CurrentValue > 0)
				return ConnectionMonitorNativeHelper.GetCurrentNetworkNameForConnection(nativeConnectionType);
			else
				return string.Empty;
		}

		protected override void  Dispose(bool disposing)
		{
			if (disposing && gprsCoverageState != null)
			{
				gprsCoverageState.Changed -= OnCellPropertyChange;
				gprsCoverageState.Dispose();
			}
			base.Dispose(disposing);
		}
	}

}
