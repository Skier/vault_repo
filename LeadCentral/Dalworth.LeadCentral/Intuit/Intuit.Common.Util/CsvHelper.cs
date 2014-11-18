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

namespace Intuit.Common.Util
{
	///<summary>
	/// Helper functions for creating CSV content, intended for use with API_ImportCSV.
	///</summary>
	public class CsvHelper
	{
		private static readonly char[] CharsRequiringEscape = new[] {',', '"', '\n', '\r'};

		/// <summary>
		/// Helps you escape a value that you need to put into a csv field (e.g. when using API_ImportCSV in IPP)
		/// </summary>
		/// <param name="txt"></param>
		/// <returns></returns>
		public static string EscapeForCsv(string txt)
		{
			if (String.IsNullOrEmpty(txt))
			{
				return String.Empty;
			}
			if (txt.EndsWith(" ") || txt.StartsWith(" ") || txt.IndexOfAny(CharsRequiringEscape) >= 0)
			{
				txt = '"' + txt.Replace("\"", "\"\"") + '"';
			}
			return txt;
		}

		///<summary>
		/// Takes list of unescaped values, and escapes them all and joins them using a comma.
		///</summary>
		public static string BuildCsvLine(List<string> unescapedValues)
		{
			return String.Join(",", unescapedValues.ConvertAll(value => EscapeForCsv(value)).ToArray());
		}

		/// <summary>
		/// Takes a bunch of <see cref="ICsvSerializable"/> objects and builds a CSV string out of them.
		/// </summary>
		/// <typeparam name="T">any type that implements <see cref="ICsvSerializable"/></typeparam>
		/// <param name="itemList">enumerable collection of items</param>
		/// <returns>a string containing the CSV content</returns>
		public static string BuildCsvForCollection<T>(IEnumerable<T> itemList) where T : ICsvSerializable
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ICsvSerializable item in itemList)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.AppendLine(item.GetCsvHeader());
				}
				stringBuilder.AppendLine(item.GetCsvLine());
			}
			return stringBuilder.ToString();
		}
	}
}