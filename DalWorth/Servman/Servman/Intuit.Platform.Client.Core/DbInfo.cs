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
using System.Diagnostics;
using System.Xml;
using Intuit.Common.Util;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Representing something that has a Dbid, either a database instance (application) or table.
	/// An application has a dbid and a pretty name (which is also the customer-facing name). The pretty name can be changed by the admin.
	/// A table has a dbid and a pretty name (also the customer-facing name) which can be changed by the admin.
	/// A table also has a table alias that's immutable after the table's creation, usually the string "_dbid_" and the original table name, e.g. "_dbid_company" for a Company table.
	/// Depending on how the info for a table is retrieved, you might get its pretty name or its table alias.
	/// </summary>
	public class DbInfo : IDbid
	{
		/// <summary>
		/// This class gets instantiated by one of the XML-parsing methods.
		/// </summary>
		protected DbInfo()
		{
		}

		/// <summary>
		/// Immutable name assigned after a table is created.
		/// </summary>
		public string TableAlias { get; protected set; }

		/// <summary>
		/// Customer facing name of the application or table
		/// </summary>
		public string PrettyName { get; protected set; }

		#region IDbid Members

		/// <summary>
		/// The DBID uniquely identifiying this instance or table
		/// </summary>
		public string Dbid { get; protected set; }

		#endregion

		/// <summary>
		/// Returns  "[" + <see cref="Dbid "/> + "] " + <see cref="PrettyName" />
		/// </summary>
		public string DbidAndPrettyname
		{
			get
			{
				return "[" + Dbid + "] " + PrettyName;
			}
		}

		/// <summary>
		/// Rturns <see cref="PrettyName" /> + " (" +  <see cref="Dbid "/> + ")"
		/// </summary>
		public string PrettynameAndDbid
		{
			get
			{
				return PrettyName + " (" + Dbid + ")";
			}
		}

		/// <summary>
		/// Returns  <see cref="Dbid "/>.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Dbid;
		}

		internal static DbInfo ParseDbInfoNode(XmlNode dbInfoNode)
		{
			XmlNode name = dbInfoNode.SelectSingleNode("./dbname");
			XmlNode dbid = dbInfoNode.SelectSingleNode("./dbid");
			if (name != null && dbid != null)
			{
				return new DbInfo {Dbid = dbid.InnerText, PrettyName = name.InnerText};
			}
			throw new PlatformClientException(null, String.Format(Resources.PlatformClientException_ExceptionInvalidDbidNode_Unable_to_parse_dbid_node____0___, dbInfoNode.OuterXml));
		}

		internal static DbInfo ParseChdbidNode(XmlNode chdbidNode)
		{
			XmlAttribute tableAlias = chdbidNode.Attributes["name"];
			string dbid = chdbidNode.InnerText;
			if (tableAlias != null && !String.IsNullOrEmpty(dbid))
			{
				return new DbInfo {Dbid = dbid, TableAlias = tableAlias.InnerText};
			}
			throw new PlatformClientException(null, String.Format(Resources.PlatformClientException_ExceptionInvalidChdbidNode_Unable_to_parse_chdbid_node____0___, chdbidNode.OuterXml));
		}

		/// <summary>
		/// Used by ParseGetGrantedDBs which is used by GetGrantedDBs.
		/// </summary>
		/// <param name="dbinfo">the object abstracting the dbinfo node</param>
		/// <returns>Return true if you want to include the DB in the list, false if not.</returns>
		public delegate bool FilterDbInfo(DbInfo dbinfo);

		/// <summary>
		/// Parse the response of a GetGrantedDB API-call, taking the provided filter into account.
		/// </summary>
		/// <param name="dbinfos">the XML from the API call</param>
		/// <param name="filter">optional filter to remove instances you're not interested in</param>
		/// <returns>all instances/tables returned by GetGrantedDB minues the filtered ones</returns>
		public static List<DbInfo> ParseGetGrantedDBs(XmlNode dbinfos, FilterDbInfo filter)
		{
			XmlNodeList dbinfoList = dbinfos.SelectNodes("//dbinfo");
			if (dbinfoList == null)
			{
				return null;
			}
			List<DbInfo> retval = new List<DbInfo>();
			foreach (XmlNode node in dbinfoList)
			{
				DbInfo dbInfo = ParseDbInfoNode(node);

				if (filter != null && !filter(dbInfo))
				{
					continue;
				}
				retval.Add(dbInfo);
			}
			return retval;
		}

		/// <summary>
		/// Due to the HTTP API sometimes returning pretty names and sometimes table aliases, it's sometimes required to merge the information we have about a table from two different API calls.
		/// </summary>
		/// <param name="other">the pretty name and table alias of this other instance will be copied over to this instance if it doesn't already contain that information</param>
		internal void Absorb(DbInfo other)
		{
			if (!Dbid.Equals(other.Dbid))
			{
				throw new PlatformClientException(null, Resources.PlatformClientException_CanOnlyAbsorbInfoFromAnotherDbinfoIfTheirDbidsMatch_Can_only_absorb_info_from_another_DbInfo_if_their_dbids_match);
			}

			if (PrettyName == null)
			{
				PrettyName = other.PrettyName;
			}

			if (TableAlias == null)
			{
				TableAlias = other.TableAlias;
			}

			if (PrettyName == null)
			{
				Debug.Assert(other.PrettyName == null);
			}
			else
			{
				if (other.PrettyName != null)
				{
					Debug.Assert(PrettyName.Equals(other.PrettyName));
				}
			}

			if (TableAlias == null)
			{
				Debug.Assert(other.TableAlias == null);
			}
			else
			{
				if (other.TableAlias != null)
				{
					Debug.Assert(TableAlias.Equals(other.TableAlias));
				}
			}
		}

		/// <summary>
		/// Creates a Uri that logs the user directly into the instance described by dbid on that host.
		/// </summary>
		public static Uri MakeDirectLoginUrl(IPlatformHost host, string dbid)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host", Resources.PlatformClientException_MustSpecifyValidHost_Must_specify_valid_host);
			}
			if (dbid == null)
			{
				throw new ArgumentNullException("dbid", Resources.PlatformClientException_MustSpecifyValidDBID_Must_provide_a_valid_dbid);
			}

			return WebHelper.AppendQueryParameter(WebHelper.WorkaroundCachingProblemByAddingFakeParameter(host.MakeDbidUrl(dbid)), "direct", "1");
		}
	}
}