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
using System.Xml;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// A simple wrapper around the field structure returned by some calls.
	///
	/// TODO: Many attributes that are potentially described by a field node are missing from this wrapper.
	/// </summary>
	public class FieldInfo
	{
		private const string RecordIdFieldRole = "recordid";

        internal FieldInfo(XmlNode fieldInfoNode)
        {
            XmlAttribute fieldId = fieldInfoNode.Attributes["id"];
            if(fieldId != null)
            {
                Id = fieldId.InnerText;
            }

            XmlAttribute fieldType = fieldInfoNode.Attributes["field_type"];
            if (fieldType != null)
            {
                FieldType = fieldType.InnerText;
            }
            XmlNode fieldLabel = fieldInfoNode.SelectSingleNode("./label");
            if (fieldLabel != null)
            {
                Label = fieldLabel.InnerText;
            }

            XmlNode fieldRequired = fieldInfoNode.SelectSingleNode("./required");
            if (fieldRequired != null)
            {
                Required = fieldRequired.InnerText.Equals("1");
            }
            XmlAttribute role = fieldInfoNode.Attributes["role"];
            if (role != null)
            {
                Role = role.InnerText;
            }
            XmlNode n = fieldInfoNode.SelectSingleNode("./maxlength");
            if (n != null)
            {
                MaxLength = Int32.Parse(n.InnerText);
            }
            XmlNode fid = fieldInfoNode.SelectSingleNode("./fid");
            if (fid != null)
            {
                Id = fid.InnerText;
            }
            XmlNode ftype = fieldInfoNode.SelectSingleNode("./type");
            if (ftype != null)
            {
                FieldType = ftype.InnerText;
            }
            XmlNode fName = fieldInfoNode.SelectSingleNode("./name");
            if (fName != null)
            {
                Label = fName.InnerText;
            }
            XmlNode fValue = fieldInfoNode.SelectSingleNode("./value");
            if (fValue != null)
            {
                Value = fValue.InnerText;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value of the field in particular Record.</value>
        public string Value { get; private set; }

		/// <summary>
		/// User-facing label
		/// </summary>
		public string Label { get; private set; }

		/// <summary>
		/// Max length for values
		/// </summary>
		public int MaxLength { get; private set; }

		/// <summary>
		/// Field ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Type of field
		/// </summary>
		public string FieldType { get; private set; }

		/// <summary>
		/// Whether or not the field is required to have value
		/// </summary>
		public bool Required { get; private set; }

		/// <summary>
		/// Role
		/// </summary>
		public string Role { get; private set; }

		/// <summary>
		/// Is this the record ID# field?
		/// </summary>
		public bool IsRecordId
		{
			get
			{
				return Role != null && Role.Equals(RecordIdFieldRole);
			}
		}
	}
}