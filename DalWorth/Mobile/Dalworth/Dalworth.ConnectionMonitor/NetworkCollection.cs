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
	///		This class is a collection of networks. 
	///		It's wired to a ConnectionMonitor object.
	///		When a network is added, it set the manager into the network.
	/// </summary>
	public class NetworkCollection : System.Collections.ObjectModel.KeyedCollection<string, Network>
    {
        private ConnectionMonitor monitor;

		/// <summary>
		/// Constructor of the NetworkCollection class.
		/// It sets the ConnectionMonitor reference.
		/// </summary>
		/// <param name="monitor">
		///  ConnectionMonitor to wire up the networks.
		/// </param>
        public NetworkCollection(ConnectionMonitor monitor)
        {
			this.monitor = monitor;
        }

        protected override string GetKeyForItem(Network item)
        {
			Guard.ArgumentNotNull(item, "item");

			return item.Name;
        }

        protected override void InsertItem(int index, Network item)
        {
            base.InsertItem(index, item);
			item.SetMonitor(this.monitor);
        }

		protected override void RemoveItem(int index)
		{
			Items[index].DetachMonitor(this.monitor);
			base.RemoveItem(index);
		}
    }
}
