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
using Intuit.Common.Util;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Encapsulates the information about a given role
	/// </summary>
	public class RoleInfo : ICsvSerializable
	{
		// Example:
		//  <role id="16">
		//		<name>Employee</name> 
		//		<access id="3">Basic Access</access> 
		//  </role>
		private RoleInfo(XmlNode singleRoleNode)
		{
			ID = singleRoleNode.Attributes.GetNamedItem("id").InnerText;
			XmlNode n = singleRoleNode.SelectSingleNode("./name");
			if (n != null)
			{
				Name = n.InnerText;
			}
			n = singleRoleNode.SelectSingleNode("./access");
			if (n != null)
			{
				XmlNode idAttr = n.Attributes.GetNamedItem("id");
				if (idAttr != null)
				{
					AccessId = idAttr.InnerText;
				}
				Access = n.InnerText;
			}
		}

		/// <summary>
		/// Level if access, e.g. "Basic Access" or "Administrator"
		/// </summary>
		public string Access { get; private set; }

		/// <summary>
		/// Access ID
		/// </summary>
		public string AccessId { get; private set; }

		/// <summary>
		/// Name of the role as defined by the developer of the application
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Role ID
		/// </summary>
		public string ID { get; private set; }

		internal static List<RoleInfo> ParseRoles(XmlNodeList roleNodes)
		{
			var rolesCollection = new List<RoleInfo>();
			if (roleNodes != null)
			{
				foreach (XmlNode role in roleNodes)
				{
					rolesCollection.Add(new RoleInfo(role));
				}
			}
			return rolesCollection;
		}

		/// <summary>
		/// <see cref="ICsvSerializable.GetCsvHeader"/>
		/// Do NOT localize
		/// </summary>
		public string GetCsvHeader()
		{
			return CsvHelper.BuildCsvLine(new List<string>
			                              {
			                              	"ID",
			                              	"Name",
			                              	"Access ID",
			                              	"Access",
			                              });
		}

		/// <summary>
		/// <see cref="ICsvSerializable.GetCsvLine"/>
		/// </summary>
		public string GetCsvLine()
		{
			return CsvHelper.BuildCsvLine(new List<string>
			                              {
			                              	ID,
			                              	Name,
			                              	AccessId,
			                              	Access,
			                              });
		}
	}
}