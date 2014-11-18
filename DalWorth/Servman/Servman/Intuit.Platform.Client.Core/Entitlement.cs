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

using System.Collections.Generic;
using System.Xml;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Describes individual entitlement
	/// </summary>
	public class Entitlement
	{
		/// <summary>
		/// Unique ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Term
		/// </summary>
		public string Term { get; private set; }

		/// <summary>
		/// Term ID
		/// </summary>
		public string TermId { get; private set; }

		/// <summary>
		/// Entitlement constructor based on API XML response
		/// </summary>
		/// <param name="singleEntitlementNode"></param>
		internal Entitlement(XmlNode singleEntitlementNode)
		{
			Id = singleEntitlementNode.Attributes.GetNamedItem("id").InnerText;
			XmlNode n = singleEntitlementNode.SelectSingleNode("./name");
			if (n != null)
			{
				Name = n.InnerText;
			}
			n = singleEntitlementNode.SelectSingleNode("./term");
			if (n != null)
			{
				TermId = n.Attributes.GetNamedItem("id").InnerText;
				Term = n.InnerText;
			}
		}

		/// <summary>
		/// Parses all the entitlement elements of the API_GetEntitlementValues response
		/// </summary>
		internal static List<Entitlement> ParseEntitlements(XmlNode node)
		{
			List<Entitlement> entitlements = new List<Entitlement>();
			XmlNodeList entitlementNodes = node.SelectNodes("./entitlements/entitlement");
			if (entitlementNodes != null)
			{
				foreach (XmlNode e in entitlementNodes)
				{
					entitlements.Add(new Entitlement(e));
				}
			}
			return entitlements;
		}
	}
}