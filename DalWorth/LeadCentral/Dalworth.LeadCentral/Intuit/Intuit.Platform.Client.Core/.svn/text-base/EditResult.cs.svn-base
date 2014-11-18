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
using System.Text;
using System.Xml;
using Intuit.Common.Util;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Wrapper around the result returned from a call to API_EditRecord or API_AddRecord.
	/// </summary>
	public class EditResult
	{
		/// <summary>
		/// Parse the response from an API_EditRecord call
		/// </summary>
		/// <param name="result"></param>
		public EditResult(XmlNode result)
		{
			XmlNode n = result.SelectSingleNode("//num_fields_changed");
			if (n != null)
			{
				Changed = Int32.Parse(n.InnerText);
			}
			n = result.SelectSingleNode("//update_id");
			if (n != null)
			{
				UpdateID = n.InnerText;
			}
			n = result.SelectSingleNode("//rid");
			if (n != null)
			{
				RecordID = n.InnerText;
			}
		}

		/// <summary>
		/// Whether or not the edit resulted in a change. False if all the values remained the same inside the database.
		/// </summary>
		public int Changed { get; private set; }

		/// <summary>
		/// The record ID# of the record that was changed or added.
		/// </summary>
		public string RecordID { get; private set; }

		/// <summary>
		/// Update ID
		/// </summary>
		public string UpdateID { get; private set; }

		/// <summary>
		/// Prints a summary of this result to notify as UserFacingInfo, and dumps the recorded RecordIDs as DiagnosticLog.
		/// The header string will be pre-pended to the summary.
		/// </summary>
		/// <param name="header"></param>
		/// <param name="notify"></param>
		public void SummarizeForUser(string header, WorkNotification notify)
		{
			if (notify != null)
			{
				StringBuilder sum = new StringBuilder();
				sum.Append(header);
				sum.Append(": ");
				sum.Append(GetSummary());
				notify(NotificationLevel.UserFacingInfo, sum.ToString(), false, null);
			}
		}

		/// <summary>
		/// Generates a summary of this result including the recorded RecordIDs.
		/// </summary>
		/// <returns></returns>
		public string GetSummary()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat(Resources.EditResult_GetSummary_Fields_changed___0__in_Record_ID____1__with_update_id___2_, Changed, RecordID, UpdateID);
			return sb.ToString();
		}
	}
}