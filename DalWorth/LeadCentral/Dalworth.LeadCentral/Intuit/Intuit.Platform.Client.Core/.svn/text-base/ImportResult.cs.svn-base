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
using Intuit.Common.Util;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Wrapper around the result returned from a call to API_ImportCSV. Can represent a single result, can also be used to aggregrate multiple results.
	/// </summary>
	public class ImportResult
	{
		private const string ColonSpace = ": ";
		private const string CommaSpace = ", ";

		/// <summary>
		/// Creates an "empty" result, i.e. the basis for adding more results afterwards when aggregating multiple results.
		/// </summary>
		public ImportResult()
		{
		}

		/// <summary>
		/// Parse the response from an API_ImportCSV call
		/// </summary>
		/// <param name="result"></param>
		public ImportResult(XmlNode result)
		{
			AddResult(result);
		}

		/// <summary>
		/// Total number of records the import contained.
		/// </summary>
		public int TotalInput { get; private set; }

		/// <summary>
		/// Number of records where added as the result of the import.
		/// </summary>
		public int Added { get; private set; }

		/// <summary>
		/// Number of records where updated/changed as the result of the import.
		/// </summary>
		public int Updated { get; private set; }

		/// <summary>
		/// Number of records that didn't contain changes relative to the existing data.
		/// </summary>
		public int Unchanged { get; private set; }

		private readonly List<string> m_RecordedRecordIDS = new List<string>();

		/// <summary>
		/// List of the record-ID# of the records that were imported (one for each line of the import).
		/// </summary>
		public List<string> RecordedRecordIds
		{
			get
			{
				return m_RecordedRecordIDS;
			}
		}

		/// <summary>
		/// Add the results from one API_ImportCSV to another for the purpose of aggregrating the results
		/// </summary>
		/// <param name="result">the result to be added to this result.</param>
		public void Add(ImportResult result)
		{
			TotalInput += result.TotalInput;
			Added += result.Added;
			Updated += result.Updated;
			Unchanged += result.Unchanged;
			RecordedRecordIds.AddRange(result.RecordedRecordIds);
		}

		/// <summary>
		/// Add the results from one API_ImportCSV to another for the purpose of aggregrating the results
		/// </summary>
		/// <param name="result">the result to be added to this result.</param>
		public void AddResult(XmlNode result)
		{
			XmlNode n = result.SelectSingleNode("//num_recs_input");
			if (n != null)
			{
				TotalInput += Int32.Parse(n.InnerText);
			}
			n = result.SelectSingleNode("//num_recs_added");
			if (n != null)
			{
				Added += Int32.Parse(n.InnerText);
			}
			n = result.SelectSingleNode("//num_recs_updated");
			if (n != null)
			{
				Updated += Int32.Parse(n.InnerText);
			}
			n = result.SelectSingleNode("//num_recs_unchanged");
			if (n != null)
			{
				Unchanged += Int32.Parse(n.InnerText);
			}
			XmlNodeList rids = result.SelectNodes("//rids/rid");
			if (rids != null)
			{
				foreach (XmlNode rid in rids)
				{
					RecordedRecordIds.Add(rid.InnerText);
				}
			}
		}

		/// <summary>
		/// Prints a summary of this result to notify as UserFacingInfo, and dumps the recorded RecordIDs as DiagnosticLog.
		/// The header string will be pre-pending to the summary.
		/// </summary>
		/// <param name="header"></param>
		/// <param name="notify"></param>
		public void SummarizeForUser(string header, WorkNotification notify)
		{
			if (notify != null)
			{
				StringBuilder sum = new StringBuilder();
				sum.Append(header);
				sum.Append(ColonSpace);
				sum.Append(Resources.ImportResult_RecordsAdded_Records_added);
				sum.Append(ColonSpace);
				sum.Append(Added);
				sum.Append(CommaSpace);
				sum.Append(Resources.ImportResult_RecUpdated_updated);
				sum.Append(ColonSpace);
				sum.Append(Updated);
				sum.Append(CommaSpace);
				sum.Append(Resources.ImportResult_RecUnchanged_unchanged);
				sum.Append(ColonSpace);
				sum.Append(Unchanged);
				sum.Append(CommaSpace);
				sum.Append(Resources.ImportResult_TotalSent_total_sent);
				sum.Append(ColonSpace);
				sum.Append(TotalInput);
				notify(NotificationLevel.UserFacingInfo, sum.ToString(), false, null);
				StringBuilder sb = new StringBuilder();
				sb.Append(Resources.ImportResult_IdsOfRecords_IDs_of_records);
				sum.Append(ColonSpace);
				AppendRecordIds(sb);
				notify(NotificationLevel.DiagnosticLog, sb.ToString(), false, null);
			}
		}

		/// <summary>
		/// Generates a summary of this result including the recorded RecordIDs.
		/// </summary>
		public string GetSummary()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Resources.ImportResult_RecordsTotal_Records_total);
			sb.AppendLine(ColonSpace);
			sb.AppendLine(TotalInput.ToString());
			sb.Append(Resources.ImportResult_RecordsAdded_Records_added);
			sb.AppendLine(ColonSpace);
			sb.AppendLine(Added.ToString());
			sb.Append(Resources.ImportResult_RecordsUpdated_Records_updated);
			sb.AppendLine(ColonSpace);
			sb.AppendLine(Updated.ToString());
			sb.Append(Resources.ImportResult_RecordsUnchanged_Records_unchanged);
			sb.AppendLine(ColonSpace);
			sb.AppendLine(Unchanged.ToString());
			sb.Append(Resources.ImportResult_IdsOfRecords_IDs_of_records);
			sb.AppendLine(ColonSpace);
			AppendRecordIds(sb);
			return sb.ToString();
		}

		private void AppendRecordIds(StringBuilder sb)
		{
			sb.AppendLine(String.Join(",", RecordedRecordIds.ToArray()));
		}
	}
}