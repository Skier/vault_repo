/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 * Contributors:
 *
 *    Intuit Partner Platform - initial contribution 
 *
 */
using System;
using System.Xml;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Encapsulates a billing info describing the billing state of an application.
	/// </summary>
	public class BillingInfo
	{
		/// <summary>
		/// Application is in "GRACE" billing status.
		/// </summary>
		public const string StatusGrace = "GRACE";

		/// <summary>
		/// Application is in "OK" billing status.
		/// </summary>
		public const string StatusOk = "OK";

		/// <summary>
		/// Creates a BillingInfo by parsing the XML returned by an API call
		/// </summary>
		/// <param name="billingNode"></param>
		public BillingInfo(XmlNode billingNode)
		{
			XmlNode node = billingNode.SelectSingleNode("//status");
			if (node != null)
			{
				Status = node.InnerText;
			}
			node = billingNode.SelectSingleNode("//lastPaymentDate");
			if (node != null)
			{
				LastPaymentDate = PlatformSessionContext.ParseDateTimeField(node.InnerText);
			}
			node = billingNode.SelectSingleNode("//dbid");
			if (node != null)
			{
				DbId = node.InnerText;
			}
		}

		/// <summary>
		/// Billing status
		/// </summary>
		public string Status { get; private set; }

		/// <summary>
		/// Last payment date
		/// </summary>
		public DateTime LastPaymentDate { get; private set; }

		/// <summary>
		/// The instance's ID
		/// </summary>
		public string DbId { get; private set; }

		/// <summary>
		/// True if <see cref="Status"/> is <see cref="StatusGrace"/>
		/// </summary>
		public bool HasStatusGrace()
		{
			return Status == StatusGrace;
		}

		/// <summary>
		/// True if <see cref="Status"/> is <see cref="StatusOk"/>
		/// </summary>
		public bool HasStatusOk()
		{
			return Status == StatusOk;
		}
	}
}