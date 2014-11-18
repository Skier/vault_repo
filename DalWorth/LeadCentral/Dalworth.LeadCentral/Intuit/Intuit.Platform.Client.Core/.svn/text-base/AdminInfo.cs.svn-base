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
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Intuit.Common.Util;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Encapsulates subscriber information as returend by API_GetAdminsForAllProducts
	/// </summary>
	public class AdminInfo : ICsvSerializable
	{
		/// <summary>
		/// A single subscriber
		/// </summary>
		/// <param name="singleUserNode"></param>
		internal AdminInfo(XmlNode singleUserNode)
		{
			XmlNode n = singleUserNode.SelectSingleNode("./uid");
			if (n != null)
			{
				Uid = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./firstName");
			if (n != null)
			{
				FirstName = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./lastName");
			if (n != null)
			{
				LastName = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./email");
			if (n != null)
			{
				Email = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./productId");
			if (n != null)
			{
				ProductID = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./applicationName");
			if (n != null)
			{
				ApplicationName = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./applicationId");
			if (n != null)
			{
				ApplicationId = n.InnerText;
			}
		}

		internal static List<AdminInfo> ParseAdmins(XmlNode node)
		{
			List<AdminInfo> adminCollections = new List<AdminInfo>();
			XmlNodeList admins = node.SelectNodes("./admins/admin");
			if (admins != null)
			{
				foreach (XmlNode admin in admins)
				{
					adminCollections.Add(new AdminInfo(admin));
				}
			}
			return adminCollections;
		}

		#region Properties

		/// <summary>
		/// Unique ID
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string Uid { get; private set; }

		/// <summary>
		/// First name
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string FirstName { get; private set; }

		/// <summary>
		/// Last name
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string LastName { get; private set; }

		/// <summary>
		/// Email address
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string Email { get; private set; }

		/// <summary>
		/// Product ID
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string ProductID { get; private set; }

		/// <summary>
		/// Application name
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string ApplicationName { get; private set; }

		/// <summary>
		/// Application ID
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		public string ApplicationId { get; private set; }

		#endregion

		/// <summary>
		/// <see cref="ICsvSerializable.GetCsvHeader"/>
		/// Do NOT localize
		/// </summary>
		public string GetCsvHeader()
		{
			return CsvHelper.BuildCsvLine(new List<string>
			                              {
			                              	"Uid",
			                              	"First Name",
			                              	"Last Name",
			                              	"Email",
			                              	"Product ID",
			                              	"Application Name",
			                              	"Application ID"
			                              });
		}

		/// <summary>
		/// <see cref="ICsvSerializable.GetCsvLine"/>
		/// </summary>
		public string GetCsvLine()
		{
			return CsvHelper.BuildCsvLine(new List<string>
			                              {
			                              	Uid,
			                              	FirstName,
			                              	LastName,
			                              	Email,
			                              	ProductID,
			                              	ApplicationName,
			                              	ApplicationId
			                              });
		}
	}
}