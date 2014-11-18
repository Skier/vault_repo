/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

using System;
using System.ComponentModel;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// This class exists because PlatformHost is designed as an immutable object to represent Intuit platforms.
	/// By using this class we can data-bind UI widgets to the Hostname, e.g. in a test app that allows the user to enter the hostname of the QuickBase or Workplace server to use.
	/// </summary>
	public class CustomPlatformHost : IPlatformHost, INotifyPropertyChanged
	{
		private string m_CustomerFacingName;
		private string m_Hostname;
		private bool m_UseSecureConnectionScheme;

		/// <summary>
		/// Create new instance of IPlatformHost with a custom user-facing name and hostname.
		/// </summary>
		/// <param name="customerFacingName">A name for the backend, like "Intuit Workplace" or "My Awesome App powered by Workplace"</param>
		/// <param name="useSecureConnectionScheme">true for HTTPS, false for HTTP</param>
		/// <param name="hostname">DNS name, like workplace.intuit.com</param>
		public CustomPlatformHost(string customerFacingName, bool useSecureConnectionScheme, string hostname)
		{
			m_CustomerFacingName = customerFacingName;
			m_UseSecureConnectionScheme = useSecureConnectionScheme;
			m_Hostname = hostname;
		}

		#region IPlatformHost Members

		/// <summary>
		/// The name of the platform, suitable for display to end-users
		/// </summary>
		public string CustomerFacingName
		{
			get
			{
				return m_CustomerFacingName;
			}
			set
			{
				if (m_CustomerFacingName != value)
				{
					m_CustomerFacingName = value;
					OnPropertyChanged("CustomerFacingName");
				}
			}
		}

		/// <summary>
		/// Host name of the platform
		/// </summary>
		public string Hostname
		{
			get
			{
				return m_Hostname;
			}
			set
			{
				if (m_Hostname != value)
				{
					m_Hostname = value;
					OnPropertyChanged("Hostname");
				}
			}
		}

		/// <summary>
		/// Whether or not HTTPS is used
		/// </summary>
		public bool UseSecureConnectionScheme
		{
			get
			{
				return m_UseSecureConnectionScheme;
			}
			set
			{
				if (m_UseSecureConnectionScheme != value)
				{
					m_UseSecureConnectionScheme = value;
					OnPropertyChanged("UseSecureConnectionScheme");
				}
			}
		}

		/// <summary>
		/// <see cref="IPlatformHost.ConnectionScheme"/>
		/// </summary>
		public string ConnectionScheme
		{
			get
			{
				return m_UseSecureConnectionScheme ? PlatformHost.Https : PlatformHost.Http;
			}
		}

		/// <summary>
		/// Constructs the URL that points to the given dbid for this context.
		/// </summary>
		/// <param name="dbid"></param>
		/// <returns>the Uri for this dbid, or null if dbid is null or empty</returns>
		public Uri MakeDbidUrl(string dbid)
		{
			return PlatformHost.MakeDbidUrl(ConnectionScheme, Hostname, dbid);
		}

		#endregion

		/// <summary>
		/// <see cref="INotifyPropertyChanged"/> implementation: fired everytime a property changes
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// <see cref="INotifyPropertyChanged"/> implementation: called by properties when they change, fires <see cref="PropertyChanged"/>
		/// </summary>
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				if (PlatformSessionContext.SyncInvoke != null && PlatformSessionContext.SyncInvoke.InvokeRequired)
				{
					PlatformSessionContext.SyncInvoke.Invoke(PropertyChanged, new object[] { this, new PropertyChangedEventArgs(propertyName) });
				}
				else
				{
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
			}
		}
	}
}