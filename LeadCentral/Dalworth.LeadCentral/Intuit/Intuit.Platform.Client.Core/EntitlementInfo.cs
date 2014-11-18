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
using System.Xml;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Describes an entitlement
	/// </summary>
	public class EntitlementInfo
	{
		#region Member Variables

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor that parses XML returned by API
		/// </summary>
		/// <param name="entitlementNode"></param>
		internal EntitlementInfo(XmlNode entitlementNode)
		{
			XmlNode n = entitlementNode.SelectSingleNode("//appId");
			if (n != null)
			{
				AppId = n.InnerText;
			}
			n = entitlementNode.SelectSingleNode("//productId");
			if (n != null)
			{
				ProductId = n.InnerText;
			}
			n = entitlementNode.SelectSingleNode("//planName");
			if (n != null)
			{
				PlanName = n.InnerText;
			}
			n = entitlementNode.SelectSingleNode("//planType");
			if (n != null)
			{
				PlanType = n.InnerText;
			}
			n = entitlementNode.SelectSingleNode("//maxUsers");
			if (n != null)
			{
				MaxUsers = int.Parse(n.InnerText);
			}
			n = entitlementNode.SelectSingleNode("//currentUsers");
			if (n != null)
			{
				CurrentUsers = int.Parse(n.InnerText);
			}
			n = entitlementNode.SelectSingleNode("//daysRemainingTrial");
			if (n != null)
			{
				DaysRemaining = int.Parse(n.InnerText);
			}
			n = entitlementNode.SelectSingleNode("//fee");
			if (n != null)
			{
				Fee = double.Parse(n.InnerText);
			}
			n = entitlementNode.SelectSingleNode("//betaExpirationDate");
			if (n != null)
			{
				// comes in longMonth DD, YYYY  format (e.g. June 10,2010)
				BetaExpirationDate = DateTime.Parse(n.InnerText);
			}
			n = entitlementNode.SelectSingleNode("//currentFileUsage");
			if (n != null)
			{
				CurrentFileUsage = long.Parse(n.InnerText);
			}

			Entitlements = Entitlement.ParseEntitlements(entitlementNode);
		}

		#endregion

		#region Properties

		/// <summary>
		/// App ID
		/// </summary>
		public string AppId { get; private set; }

		/// <summary>
		/// Product ID
		/// </summary>
		public string ProductId { get; private set; }

		/// <summary>
		/// Plan Name
		/// </summary>
		public string PlanName { get; private set; }

		/// <summary>
		/// Plan Type
		/// </summary>
		public string PlanType { get; private set; }

		/// <summary>
		/// Max Users
		/// </summary>
		public int MaxUsers { get; private set; }

		/// <summary>
		/// Current count of users
		/// </summary>
		public int CurrentUsers { get; private set; }

		/// <summary>
		/// Days remaining
		/// </summary>
		public int DaysRemaining { get; private set; }

		/// <summary>
		/// Fee
		/// </summary>
		public double Fee { get; private set; }

		/// <summary>
		/// Beta expiration date
		/// </summary>
		public DateTime BetaExpirationDate { get; private set; }

		/// <summary>
		/// Current file usage
		/// </summary>
		public long CurrentFileUsage { get; private set; }

		/// <summary>
		/// List of individual entitlements
		/// </summary>
		public IList<Entitlement> Entitlements { get; private set; }

		#endregion
	}
}