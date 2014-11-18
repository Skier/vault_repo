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
using System.Collections.Generic;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Abstracts the existing Intuit platform hosts (QuickBase and Workplace) as IMMUTABLE objects. Use <see cref="CustomPlatformHost"/> for other domains/hosts, e.g. Beta &amp; Dev hosts.
	/// </summary>
	public sealed class PlatformHost : IPlatformHost
	{
		/// <summary>
		/// "https://"
		/// </summary>
		public const string Https = "https://";

		/// <summary>
		/// "http://"
		/// </summary>
		public const string Http = "http://";

		private const string WwwQuickbaseCom = "www.quickbase.com";
		private const string WorkplaceIntuitCom = "workplace.intuit.com";
       
		internal const string DbidPath = "/db/";

		/// <summary>
		/// http://quickbase.intuit.com
		/// </summary>
		public static readonly PlatformHost QuickBaseNonSecure = new PlatformHost(Resources.PlatformHost_QuickBaseNonSecure_Intuit_QuickBase__non_secure_, false, WwwQuickbaseCom);

		/// <summary>
		/// https://quickbase.intuit.com
		/// </summary>
		public static readonly PlatformHost QuickBaseSecure = new PlatformHost(Resources.PlatformHost_QuickBaseSecure_Intuit_QuickBase, true, WwwQuickbaseCom);

		/// <summary>
		/// http://workplace.intuit.com
		/// </summary>
		public static readonly PlatformHost WorkPlaceNonSecure = new PlatformHost(Resources.PlatformHost_WorkPlaceNonSecure_Intuit_Workplace__non_secure_, false, WorkplaceIntuitCom);

		/// <summary>
		/// https://workplace.intuit.com
		/// </summary>
		public static readonly PlatformHost WorkPlaceSecure = new PlatformHost(Resources.PlatformHost_WorkPlaceSecure_Intuit_Workplace, true, WorkplaceIntuitCom);

		/// <summary>
		/// All the predefined platform hosts
		/// </summary>
        private static readonly IPlatformHost[] KnownIntuitHosts = new IPlatformHost[] { QuickBaseSecure, QuickBaseNonSecure, WorkPlaceSecure, WorkPlaceNonSecure};

		/// <summary>
		/// Enumeration of predefined IPlatformHost instances in this class.
		/// </summary>
		public static IEnumerable<IPlatformHost> KnownHosts
		{
			get
			{
				return (IEnumerable<IPlatformHost>)KnownIntuitHosts.Clone(); // make sure KnownIntuitHosts stays unchanged
			}
		}

		internal PlatformHost(string customerFacingName, bool useSecureConnectionScheme, string hostname)
		{
			CustomerFacingName = customerFacingName;
			UseSecureConnectionScheme = useSecureConnectionScheme;
			Hostname = hostname;
		}

		/// <summary>
		/// Whether or not this given <paramref name="host"/> is one of the <see cref="KnownHosts"/>.
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public static bool IsKnownHost(IPlatformHost host)
		{
			foreach (IPlatformHost knownHost in KnownIntuitHosts)
			{
				if (knownHost == host)
				{
					return true;
				}
			}
			return false;
		}

		#region IPlatformHost Members

		/// <summary>
		/// Name of the platform, suitable to be used user-facing
		/// </summary>
		public string CustomerFacingName { get; private set; }

		/// <summary>
		/// Hostname of the platform
		/// </summary>
		public string Hostname { get; private set; }

		/// <summary>
		/// Whether or not HTTPS is used
		/// </summary>
		public bool UseSecureConnectionScheme { get; private set; }

		/// <summary>
		/// <see cref="Https"/> or <see cref="Http"/>
		/// </summary>
		public string ConnectionScheme
		{
			get
			{
				return UseSecureConnectionScheme ? Https : Http;
			}
		}

		/// <summary>
		/// Constructs the URL that points to the given dbid for this context.
		/// </summary>
		/// <param name="dbid"></param>
		/// <returns>the Uri for this dbid, or null if dbid is null or empty</returns>
		public Uri MakeDbidUrl(string dbid)
		{
			return MakeDbidUrl(ConnectionScheme, Hostname, dbid);
		}

		internal static Uri MakeDbidUrl(string connectionScheme, string hostname, string dbid)
		{
			if (String.IsNullOrEmpty(dbid) || String.IsNullOrEmpty(hostname))
			{
				return null;
			}

			return new Uri((String.IsNullOrEmpty(connectionScheme) ? Https : connectionScheme) + hostname + DbidPath + dbid);
		}

		#endregion

		/// <summary>
		/// Use your own app's name instead of the generic platform name
		/// </summary>
		/// <param name="yourAppsCustomerFacingName">the app's customer facing name, to be used in messaging and errors</param>
		/// <param name="host">the platform your app is hosted on</param>
		/// <returns>an instance of PlatformHost with which you can initialize a PlatformSessionContext instance</returns>
		public static CustomPlatformHost KnownHostWithAppSpecificName(string yourAppsCustomerFacingName, PlatformHost host)
		{
			return new CustomPlatformHost(yourAppsCustomerFacingName, host.UseSecureConnectionScheme, host.Hostname);
		}

		/// <summary>
		/// If all we have is a hostname, see if it's the hostname of one of the <see cref="KnownHosts"/>.
		/// </summary>
		/// <returns>null if not found</returns>
		public static IPlatformHost GuessFromHostname(string hostname)
		{
			IPlatformHost foundOne = null;
			foreach (var host in KnownIntuitHosts)
			{
				if (host.Hostname.Equals(hostname, StringComparison.InvariantCultureIgnoreCase))
				{
					if (foundOne == null || !foundOne.UseSecureConnectionScheme) // get the last one that matches, prefer secure hosts
					{
						foundOne = host;
					}
				}
			}
			return foundOne;
		}
	}
}