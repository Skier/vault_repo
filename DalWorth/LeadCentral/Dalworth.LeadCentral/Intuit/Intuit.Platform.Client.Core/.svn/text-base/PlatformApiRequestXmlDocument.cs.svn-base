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
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// A helper class to build API requests.
	/// </summary>
	public class PlatformApiRequestXmlDocument : XmlDocument
	{
		private readonly string m_RequestID;
		private XmlElement m_QdbapiElement;

		/// <summary>
		/// Instantiates a new document, using the optional requestId
		/// </summary>
		public PlatformApiRequestXmlDocument(string requestId)
		{
			m_RequestID = requestId;
		}

		private XmlElement QdbapiElement
		{
			get
			{
				if (m_QdbapiElement == null)
				{
					m_QdbapiElement = CreateElement("qdbapi");
					AppendChild(QdbapiElement);
					AddTextParameter("encoding", "utf-8");
					AddTextParameter("udata", m_RequestID);
				}
				return m_QdbapiElement;
			}
		}

		/// <summary>
		/// Creates a new element with the given <paramref name="name"/>, appends the <paramref name="node"/> to that new element, and appends the new element to <paramref name="appendTo"/>.
		/// </summary>
		public XmlElement AddNode(XmlElement appendTo, string name, XmlNode node)
		{
			XmlElement elem = CreateElement(name);
			elem.AppendChild(node);
			appendTo.AppendChild(elem);
			return elem;
		}

		/// <summary>
		/// Add an API parameter of type CData
		/// </summary>
		public void AddCDataParameter(string name, string value)
		{
			AddNode(QdbapiElement, name, CreateCDataSection(value));
		}

		/// <summary>
		/// Add an API parameter of type Text
		/// </summary>
		public void AddTextParameter(string name, string value)
		{
			AddNode(QdbapiElement, name, CreateTextNode(value));
		}

		/// <summary>
		/// Add an API parameter identifying a field
		/// </summary>
		public void AddFieldParameter(string idOrName, string value)
		{
			if (string.IsNullOrEmpty(idOrName))
			{
				throw new ArgumentException(Resources.PlatformClientException_MustProvideIDOrNameOfTheField_Must_provide_id_or_name_of_the_field, "idOrName");
			}
			XmlElement field = AddNode(QdbapiElement, "field", CreateTextNode(value ?? String.Empty));
			int id;
			XmlAttribute attr = CreateAttribute(Int32.TryParse(idOrName, out id) ? "fid" : "name");
			attr.Value = idOrName;
			field.Attributes.Prepend(attr);
		}

		/// <summary>
		/// Add a parameter that consists of a query.
		/// </summary>
		public void AddQueryParameter(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				return;
			}
			if (query.StartsWith("{") && query.EndsWith("}"))
			{
				AddTextParameter("query", query);
			}
			else
			{
				int id;
				AddTextParameter(Int32.TryParse(query, out id) ? "qid" : "qname", query);
			}
		}
	}
}