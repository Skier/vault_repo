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
using System.Xml;
using System.IO;

namespace Dalworth.ConnectionMonitor
{
    public static class ConnectionMonitorFactory 
	{
		private static ConnectionMonitor monitor = new ConnectionMonitor();
		private static bool initialized;

        public static ConnectionMonitor Create()
        {
            lock (monitor)
            {
                if (!initialized)
                {
                    List<string> networkNames = ConnectionMonitorNativeHelper.GetNetworkList();
                    foreach (string name in networkNames)
                    {
                        monitor.Networks.Add(new Network(name));
                    }

                    monitor.Connections.Add(new DesktopConnection("DesktopConnection", 1));
                    monitor.Connections.Add(new NicConnection("NicConnection", 1));
                    monitor.Connections.Add(new CellConnection("CellConnection", 3));                    

                    initialized = true;
                }
            }
            return monitor;
        }




		/// <summary>
		/// Provides access to the single instance of the ConnectionMonitor.
		/// </summary>
		public static ConnectionMonitor Instance
		{
			get 
			{
				lock (monitor)
				{
					if (!initialized)
                        throw new InvalidOperationException("The connection monitor instance should be initialized first.");
					return monitor;
				}
			}
		}
    }
}
