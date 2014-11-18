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

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// A simple wrapper around the result returned by API_DoQuery
	/// </summary>
	public class RecordSet : List<Record>
	{
		/// <summary>
		/// Constructs a new empty Record and adds it to this set
		/// </summary>
		/// <returns>new empty Record that's part of this set</returns>
		public Record AddNewRecord()
		{
			Record record = new Record();
			Add(record);
			return record;
		}

		internal static RecordSet ParseQueryResponse(XmlNode respXML)
		{

            XmlNodeList fieldInfo = respXML.SelectNodes("./field");
            Dictionary<string, FieldInfo> responseFields = new Dictionary<string, FieldInfo>();
            if (fieldInfo != null && fieldInfo.Count > 0)
            {
                RecordSet recordList = new RecordSet();
                Record recordFields = recordList.AddNewRecord();
                foreach (XmlNode fieldNode in fieldInfo)
                {
                    
                    FieldInfo f = new FieldInfo(fieldNode);
                    responseFields[f.Id] = f;
                    recordFields.SetFieldValue(f, f.Value);

                    // Added support for record id.
                    XmlNode updateID = respXML.SelectSingleNode("./update_id");
                    if (updateID != null)
                    {
                        recordFields.SetUpdateId(updateID.InnerText);
                    }
                   
                }

                return recordList;
            }

			XmlNodeList fields = respXML.SelectNodes("//table/fields/field");
			Dictionary<string, FieldInfo> fieldsInResponse = new Dictionary<string, FieldInfo>();
			if (fields != null)
			{
				foreach (XmlNode fieldNode in fields)
				{
					FieldInfo f = new FieldInfo(fieldNode);
					fieldsInResponse[f.Id] = f;

				}
			}
			XmlNodeList records = respXML.SelectNodes("//table/records/record");
			if (records != null)
			{
				RecordSet recordList = new RecordSet();
				foreach (XmlNode recordNode in records)
				{
					XmlNodeList cells = recordNode.SelectNodes("./f");
					if (cells != null)
					{
						Record recordFields = recordList.AddNewRecord();
						foreach (XmlNode cell in cells)
						{
							string fId = cell.Attributes["id"].InnerText;
							string value = cell.InnerText;
							recordFields.SetFieldValue(fieldsInResponse[fId], value);
						}

						// Added support for record id.
						XmlNode updateID = recordNode.SelectSingleNode("./update_id");
						if (updateID != null)
						{
							recordFields.SetUpdateId(updateID.InnerText);
						}
					}
				}
				return recordList;
			}

			return null;
		}
	}
}