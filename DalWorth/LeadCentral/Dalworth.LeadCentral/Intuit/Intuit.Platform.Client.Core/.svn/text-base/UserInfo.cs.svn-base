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
using System.Text;
using System.Xml;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Wraps user information returned by the platform. Depending on your access levels and which user you're querying, many of the fields might not be filled.
	/// </summary>
	public class UserInfo
	{
		private string m_RoleNames;
		private List<RoleInfo> m_Roles;

		/// <summary>
		/// Parse the user info from the given XML snippet
		/// </summary>
		/// <param name="singleUserNode"></param>
		public UserInfo(XmlNode singleUserNode)
		{
			LastAccess = DateTime.MinValue;
			ID = singleUserNode.Attributes.GetNamedItem("id").InnerText;
			XmlNode n = singleUserNode.SelectSingleNode("./firstName");
			if (n != null)
			{
				FirstName = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./name");
			if (n != null)
			{
				Name = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./lastName");
			if (n != null)
			{
				LastName = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./login");
			if (n != null)
			{
				Login = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./email");
			if (n != null)
			{
				Email = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./screenName");
			if (n != null)
			{
				ScreenName = n.InnerText;
			}
			n = singleUserNode.SelectSingleNode("./lastAccess");
			if (n != null)
			{
				LastAccess = PlatformSessionContext.ParseDateTimeField(n.InnerText);
			}
			XmlNodeList roleNodes = singleUserNode.SelectNodes("./roles/role");
			Roles = RoleInfo.ParseRoles(roleNodes);
		}

		/// <summary>
		/// Last time the user accessed the system
		/// </summary>
		public DateTime LastAccess { get; private set; }

		/// <summary>
		/// User name
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// "User ID"
		/// </summary>
		public string ScreenName { get; private set; }

		/// <summary>
		/// Email address
		/// </summary>
		public string Email { get; private set; }

		/// <summary>
		/// Same as ScreenName?
		/// </summary>
		public string Login { get; private set; }

		/// <summary>
		/// Last name
		/// </summary>
		public string LastName { get; private set; }

		/// <summary>
		/// First name
		/// </summary>
		public string FirstName { get; private set; }

		/// <summary>
		/// Internal ID
		/// </summary>
		public string ID { get; private set; }

		/// <summary>
		/// List of roles assigned to this user
		/// </summary>
		public List<RoleInfo> Roles
		{
			get
			{
				return m_Roles;
			}
			private set
			{
				if (value != m_Roles)
				{
					m_Roles = value;
					m_RoleNames = null;
				}
			}
		}

		/// <summary>
		/// Names all the roles this user has, comma-separated
		/// </summary>
		public string RoleNames
		{
			get
			{
				return m_RoleNames ?? (m_RoleNames = BuildRoleNamesString());
			}
		}

		private string BuildRoleNamesString()
		{
			var roleNames = new StringBuilder();
			foreach (RoleInfo roleInfo in Roles)
			{
				if (roleNames.Length > 0)
				{
					roleNames.Append(",");
				}
				roleNames.Append(roleInfo.Name);
			}
			return roleNames.ToString();
		}

		/// <summary>
		/// Parse multiple user infos
		/// </summary>
		public static List<UserInfo> ParseUsers(XmlNodeList nodes)
		{
			var uis = new List<UserInfo>();
			if (nodes != null)
			{
				foreach (XmlNode n in nodes)
				{
					uis.Add(new UserInfo(n));
				}
			}
			return uis;
		}

		/// <summary>
		/// Under rare (and probably invalid) circumstances, a user can have a role that's
		/// actually not part of the application's regular role definitions.
		/// This function will give you a map of all the roles used by users in the list, using the role ID as the key.
		/// In most cases it will overlap 100% with the list of roles reported by GetRoleInfo. In the above described situation,
		/// it might contain more.
		/// </summary>
		/// <param name="uis">a UserInfo collection, in most cases a list of all users in an instance</param>
		/// <returns>Map of role id to RoleInfo object for all roles assigned to these users</returns>
		public static IDictionary<string, RoleInfo> ExtractRolesUsedByUsers(IEnumerable<UserInfo> uis)
		{
			var roles = new Dictionary<string, RoleInfo>();
			foreach (UserInfo ui in uis)
			{
				foreach (RoleInfo ri in ui.Roles)
				{
					if (!roles.ContainsKey(ri.ID))
					{
						roles.Add(ri.ID, ri);
					}
				}
			}
			return roles;
		}

		/// <summary>
		/// Uses the output of ExtractRolesUsedByUsers and a list of app-defined roles to extract a list of roles
		/// assigned to users that don't exist in the app definion.
		/// </summary>
		public static IList<RoleInfo> DiffActualRolesFromAppRoles(IDictionary<string, RoleInfo> rolesOfUsers, IList<RoleInfo> appDefinedRoles)
		{
			IList<RoleInfo> diff = new List<RoleInfo>();
			foreach (RoleInfo ri1 in rolesOfUsers.Values)
			{
				bool foundMatchingRoleInAppsRoleDefinitions = false;
				foreach (RoleInfo ri2 in appDefinedRoles)
				{
					if (ri1.ID.Equals(ri2.ID))
					{
						foundMatchingRoleInAppsRoleDefinitions = true;
					}
				}
				if (!foundMatchingRoleInAppsRoleDefinitions)
				{
					diff.Add(ri1);
				}
			}
			return diff;
		}
	}
}