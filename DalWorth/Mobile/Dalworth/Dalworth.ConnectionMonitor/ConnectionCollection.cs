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
	///		This class is a collection of connections. 
	///		It's wired to a ConnectionMonitor object.
	///		When a connection is added the monitor can handle the insertion.
	/// </summary>
    public class ConnectionCollection : System.Collections.ObjectModel.KeyedCollection<string, Connection>
    {
        private ConnectionMonitor monitor;

		/// <summary>
		/// Constructor of the ConnectionCollection class.
		/// It sets the ConnectioMonitor reference.
		/// </summary>
		/// <param name="monitor">
		///		ConnectionMonitor to wire up the connections.
		/// </param>
        public ConnectionCollection(ConnectionMonitor monitor)
        {
            this.monitor = monitor;
        }       

        protected override string GetKeyForItem(Connection item)
        {
			Guard.ArgumentNotNull(item, "item");


			return item.ConnectionTypeName;
        }

        protected override void InsertItem(int index, Connection item)
        {
            base.InsertItem(index, item);
            monitor.ConnectionAdded(item);
        }

		protected override void RemoveItem(int index)
		{
			monitor.ConnectionRemoved(Items[index]);
			base.RemoveItem(index);
		}
	}
}
