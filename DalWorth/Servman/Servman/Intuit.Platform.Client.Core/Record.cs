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

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// A simple wrapper around an individual record returned as part of an API_DoQuery call.
	/// A record is made up of one or more fields (represented by the FieldInfo structure), and has a value for each field (always a string).
	/// 
	/// TODO: Allow type-safe retrieval of values, by using the FieldInfo's type information.
	/// </summary>
	public class Record : Dictionary<FieldInfo, string>
	{
		private Dictionary<string, FieldInfo> m_FieldsById;
		private Dictionary<string, FieldInfo> m_FieldsByLabel;
		private string m_RecordId;
		private string m_UpdateId;

		private Dictionary<string, FieldInfo> FieldsByLabel
		{
			get
			{
				return m_FieldsByLabel ?? (m_FieldsByLabel = new Dictionary<string, FieldInfo>());
			}
		}

		private Dictionary<string, FieldInfo> FieldsById
		{
			get
			{
				return m_FieldsById ?? (m_FieldsById = new Dictionary<string, FieldInfo>());
			}
		}

		/// <summary>
		/// All the fields this record contains.
		/// </summary>
		public IEnumerator<FieldInfo> Fields
		{
			get
			{
				return Keys.GetEnumerator();
			}
		}

		/// <summary>
		/// </summary>
		/// <returns>the record ID# of this record, or null if the query didn't ask for it</returns>
		public string RecordId
		{
			get
			{
				if (m_RecordId == null)
				{
					FieldInfo fi = Schema.GetRecordIdField(m_FieldsByLabel);
					if (fi == null)
					{
						return null;
					}
					m_RecordId = this[fi];
				}
				return m_RecordId;
			}
		}

		/// <summary>
		/// A value assinged by the platform for this update operation
		/// </summary>
		public string UpdateId
		{
			get
			{
				return m_UpdateId;
			}
		}

		/// <summary>
		/// See if this record has the value for a field with the given label.
		/// </summary>
		/// <param name="label">label of the field in this record you want the value of</param>
		/// <param name="value">will be set to the value if present</param>
		/// <returns>true if present, otherwise false</returns>
		public bool TryFindFieldValueByLabel(string label, out string value)
		{
			FieldInfo field;
			if (FieldsByLabel.TryGetValue(label, out field))
			{
				value = this[field];
				return true;
			}
			value = null;
			return false;
		}

		/// <summary>
		/// The value contained in this record for the given field.
		/// </summary>
		/// <param name="fieldInfo">one of the FieldInfo objects returned by the Fields property</param>
		/// <returns>the value in this record for the desired field</returns>
		public string GetFieldValue(FieldInfo fieldInfo)
		{
			return this[fieldInfo];
		}

		/// <summary>
		/// Retrieve the value of a field in this record by using the field's id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public string GetFieldValueById(string id)
		{
			return this[FieldsById[id]];
		}

		/// <summary>
		/// Retrieve the value of a field in this record by using the field's label.
		/// </summary>
		/// <param name="label"></param>
		/// <returns></returns>
		public string GetFieldValueByLabel(string label)
		{
			return this[FieldsByLabel[label]];
		}

		internal void SetFieldValue(FieldInfo f, string value)
		{
			FieldsByLabel[f.Label] = f;
			FieldsById[f.Id] = f;
			this[f] = value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		internal void SetUpdateId(string value)
		{
			m_UpdateId = value;
		}
	}
}