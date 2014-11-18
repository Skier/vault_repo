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

using System.Xml;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// The result of a call to CreateDatabase() containing the new dbid of the 
	/// created application as well as the new dbid of the main table in the application.
	/// </summary>
	public class DatabaseCreateStatus
	{
		private DatabaseCreateStatus(string dbId, string appId, string appToken)
		{
			DbId = dbId;
			AppId = appId;
			AppToken = appToken;
		}

		/// <summary>
		/// Dbid of the application
		/// </summary>
		public string DbId { get; protected set; }

		/// <summary>
		/// App ID
		/// </summary>
		public string AppId { get; protected set; }

		/// <summary>
		/// AppToken
		/// </summary>
		public string AppToken { get; protected set; }

		internal static DatabaseCreateStatus ParseCreateDatabase(XmlNode responseDoc)
		{
			XmlNode dbIdNode = responseDoc.SelectSingleNode("//dbid");
			XmlNode appDbIdNode = responseDoc.SelectSingleNode("//appdbid");
			XmlNode appTokenNode = responseDoc.SelectSingleNode("//apptoken");
			if (appDbIdNode == null && dbIdNode == null)
			{
				return null;
			}
			string dbId = dbIdNode != null ? dbIdNode.InnerText : string.Empty;
			string appDbId = appDbIdNode != null ? appDbIdNode.InnerText : string.Empty;
			string appTokenText = appTokenNode != null ? appTokenNode.InnerText : string.Empty;
			DatabaseCreateStatus response = new DatabaseCreateStatus(dbId, appDbId, appTokenText);
			return response;
		}
	}
}