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
	/// Represents the schema information for a DbInfo (either a DB instance or a table).
	/// Optionally contains schema information for child DbIds.
	/// </summary>
	public class Schema : DbInfo
	{
		#region Delegates

		/// <summary>
		/// A function that knows how to retrieve a schema for a given dbid, e.g. <see cref="PlatformSessionContext.GetSchema(IDbid)" />
		/// </summary>
		public delegate Schema SchemaGetter(IDbid dbid);

		#endregion

		private const string RecordIdFieldname = "Record ID#";
		private const string VarNameMajorVer = "MAJOR_VERSION_";
		private const string VarNameMinorVer = "MINOR_VERSION_";
		private readonly Dictionary<string, DbInfo> m_ChildDbIdsByDbid = new Dictionary<string, DbInfo>();
		private Dictionary<string, FieldInfo> m_Fields;
		private Dictionary<string, string> m_Variables;

		/// <summary>
		/// Instantiates a Schema object for the given dbid and the XML description of its schema returned by the platform API
		/// </summary>
		internal Schema(string dbidToGetSchemaFor, XmlNode tableSchema)
		{
			Dbid = dbidToGetSchemaFor;
			XmlNode node = tableSchema.SelectSingleNode("//table/name");
			if (node != null)
			{
				PrettyName = node.InnerText;
			}
			node = tableSchema.SelectSingleNode("//table/desc");
			if (node != null)
			{
				Description = node.InnerText;
			}
			node = tableSchema.SelectSingleNode("//table/original/cre_date");
			if (node != null)
			{
				CreationDate = PlatformSessionContext.ParseDateTimeField(node.InnerText);
			}
			XmlNodeList vars = tableSchema.SelectNodes("//table/variables/var");
			if (vars != null)
			{
				foreach (XmlNode varNode in vars)
				{
					string name = varNode.Attributes["name"].InnerText;
					string value = varNode.InnerText;
					Variables[name] = value;
				}
			}
			XmlNodeList fields = tableSchema.SelectNodes("//table/fields/field");
			if (fields != null)
			{
				foreach (XmlNode fieldNode in fields)
				{
					var f = new FieldInfo(fieldNode);
					Fields[f.Label] = f;
				}
			}
			XmlNodeList chdbids = tableSchema.SelectNodes("//table/chdbids/chdbid");
			if (chdbids != null)
			{
				foreach (XmlNode chdbid in chdbids)
				{
					DbInfo di = ParseChdbidNode(chdbid);
					m_ChildDbIdsByDbid[di.Dbid] = di;
				}
			}
		}

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// The appVars in this db.
		/// </summary>
		public Dictionary<string, string> Variables
		{
			get
			{
				return m_Variables ?? (m_Variables = new Dictionary<string, string>());
			}
		}

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Map of field labels to fields
		/// </summary>
		public Dictionary<string, FieldInfo> Fields
		{
			get
			{
				return m_Fields ?? (m_Fields = new Dictionary<string, FieldInfo>());
			}
		}

		/// <summary>
		/// List of child tables this instance or table has
		/// </summary>
		public IEnumerable<string> ChildDbids
		{
			get
			{
				return m_ChildDbIdsByDbid.Keys;
			}
		}

		/// <summary>
		/// Finds a table in this schema by its alias using <see cref="FindChildTableDbInfoByTableAlias"/>. If we haven't loaded the schema for that table yet, returns null.
		/// </summary>
		public Schema FindChildTableSchemaByTableAlias(string tableAlias)
		{
			return FindChildTableDbInfoByTableAlias(tableAlias) as Schema;
		}

		/// <summary>
		/// Finds a child table by its alias.
		/// </summary>
		/// <param name="tableAlias"></param>
		/// <returns></returns>
		public DbInfo FindChildTableDbInfoByTableAlias(string tableAlias)
		{
			foreach (DbInfo di in m_ChildDbIdsByDbid.Values)
			{
				if (di.TableAlias.Equals(tableAlias, StringComparison.InvariantCultureIgnoreCase))
				{
					return di;
				}
			}
			return null;
		}

		/// <summary>
		/// For a given set of fields of a table, return the field representing the record id#.
		/// </summary>
		public static FieldInfo GetRecordIdField(Dictionary<string, FieldInfo> fieldsLabelToInfoMap)
		{
			FieldInfo fi = fieldsLabelToInfoMap[RecordIdFieldname];
			if (fi != null)
			{
				return fi;
			}
			foreach (FieldInfo afi in fieldsLabelToInfoMap.Values)
			{
				if (afi.IsRecordId)
				{
					return afi;
				}
			}
			return null;
		}

		/// <summary>
		/// Finds the record ID# field using <see cref="GetRecordIdField"/> and then returns its Id.
		/// </summary>
		/// <returns></returns>
		public string GetRecordIdFieldId()
		{
			FieldInfo fi = GetRecordIdField(Fields);
			if (fi != null)
			{
				return fi.Id;
			}
			return null;
		}

		/// <summary>
		/// Finds a field in <see cref="Fields"/> using the name. If it's not found, returns null.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public FieldInfo FindFieldByName(string name)
		{
			FieldInfo f;
			if (Fields.TryGetValue(name, out f))
			{
				return f;
			}
			return null;
		}

		/// <summary>
		/// For the given list of fields identified by their label, builds a comma-separated list of their IDs.
		/// </summary>
		public StringBuilder BuildColumnFieldIdList(string[] fields)
		{
			var clist = new StringBuilder();
			BuildColumnFieldIdList(clist, fields);
			return clist;
		}

		/// <summary>
		/// Gets the ID for a field based on the field's label. Assumes field by that label exists, will throw exception if it doesn't.
		/// </summary>
		/// <param name="label"></param>
		/// <returns></returns>
		public string GetFieldId(string label)
		{
			return Fields[label].Id;
		}

		/// <summary>
		/// Calls MakeColumnFieldIdList and appends the string to the given StringBuilder that may already contain a partial clist.
		/// </summary>
		/// <param name="clist"></param>
		/// <param name="fields"></param>
		public void BuildColumnFieldIdList(StringBuilder clist, string[] fields)
		{
			if (clist.Length > 0 && clist[clist.Length - 1] != '.')
			{
				clist.Append(".");
			}
			clist.Append(MakeColumnFieldIdList(fields));
		}

		/// <summary>
		/// For a given array of field labels, looks of the field IDs and concatenates with commas inbetween to form a column list (clist) argument for API calls such as DoQuery.
		/// </summary>
		/// <param name="fieldLabels"></param>
		/// <returns></returns>
		public string MakeColumnFieldIdList(string[] fieldLabels)
		{
			return String.Join(".", Array.ConvertAll(fieldLabels, input => GetFieldId(input)));
		}

		/// <summary>
		/// Recursively fleshes out the missing Schema info for a Schema's child-tables.
		/// </summary>
		public void LoadMissingChildSchemas(SchemaGetter schemaGetter)
		{
			// we have to keep track of the newly loaded schemas separately and put them into the base schema later,
			// because replacing them immediately invalidates the enumerator in the foreach loop
			var schemasLoaded = new Dictionary<string, Schema>();
			foreach (string dbid in m_ChildDbIdsByDbid.Keys)
			{
				IDbid d = m_ChildDbIdsByDbid[dbid];
				var s = d as Schema;
				if (s == null)
				{
					s = schemaGetter(d);
					schemasLoaded[dbid] = s;
				}
				else
				{
					s.LoadMissingChildSchemas(schemaGetter);
				}
			}
			foreach (string dbid in schemasLoaded.Keys)
			{
				ReplaceDbInfoWithSchemaAndMerge(dbid, schemasLoaded[dbid]);
			}
		}

		/// <summary>
		/// Load the schema of a table using schemaGetter.
		/// </summary>
		/// <param name="table"></param>
		/// <param name="schemaGetter"></param>
		/// <returns>the loaded schema</returns>
		public Schema LoadSchema(DbInfo table, SchemaGetter schemaGetter)
		{
			return ReplaceDbInfoWithSchemaAndMerge(table.Dbid, schemaGetter(table)) as Schema;
		}

		/// <summary>
		/// Looks in the parentSchema if a table with the tableAlias exists. If it exists, and its schema isn't loaded, loads it using schemaGetter. Returns null if the table doesn't exist.
		/// </summary>
		/// <param name="tableAlias">table alias of the table to be loaded</param>
		/// <param name="schemaGetter">SchemaGetter delegate</param>
		/// <returns>the loaded schema</returns>
		public Schema LoadSubTableSchemaIfNeeded(string tableAlias, SchemaGetter schemaGetter)
		{
			DbInfo table = FindChildTableDbInfoByTableAlias(tableAlias);
			if (table == null)
			{
				return null;
			}
			if (!(table is Schema))
			{
				table = LoadSchema(table, schemaGetter);
			}
			return (Schema)table;
		}

		/// <summary>
		/// Some clients will only load the application's schema first, and then load child table's schemas selectively.
		/// In that process DbInfo objects get replaced with Schema objects. Due to the HTTP API sometimes returning pretty names and sometimes table aliases,
		/// we have to make sure we don't lose information in the process.
		/// </summary>
		/// <param name="dbid"></param>
		/// <param name="newInfo"></param>
		private DbInfo ReplaceDbInfoWithSchemaAndMerge(string dbid, DbInfo newInfo)
		{
			DbInfo oldInfo;
			if (m_ChildDbIdsByDbid.TryGetValue(dbid, out oldInfo))
			{
				newInfo.Absorb(oldInfo);
			}
			m_ChildDbIdsByDbid[dbid] = newInfo;
			return newInfo;
		}

		/// <summary>
		/// Finds a child table by its dbid, null if none exists.
		/// </summary>
		public DbInfo GetChildDbInfo(string dbid)
		{
			DbInfo childDbid;
			if (m_ChildDbIdsByDbid.TryGetValue(dbid, out childDbid))
			{
				return childDbid;
			}
			return null;
		}

		/// <summary>
		/// Assembles the SchemaVersion based on the application variables set by the SDK.
		/// </summary>
		public SchemaVersion GetVersion()
		{
			string majorSchemaVersion;
			if (Variables.TryGetValue(VarNameMajorVer, out majorSchemaVersion))
			{
				string minorSchemaVersion;
				if (Variables.TryGetValue(VarNameMinorVer, out minorSchemaVersion))
				{
					if (!String.IsNullOrEmpty(majorSchemaVersion) && !String.IsNullOrEmpty(minorSchemaVersion))
					{
						return new SchemaVersion(Convert.ToInt32(majorSchemaVersion), Convert.ToInt32(minorSchemaVersion));
					}
				}
			}
			return null;
		}
	}
}