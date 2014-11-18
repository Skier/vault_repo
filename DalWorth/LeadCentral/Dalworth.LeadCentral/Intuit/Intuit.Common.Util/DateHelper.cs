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
using Intuit.Common.Util.Properties;

namespace Intuit.Common.Util
{
	/// <summary>
	/// Helper functions for date-related problems.
	/// </summary>
	public class DateHelper
	{
		///<summary>
		/// Defined as January 1st, 1970 UTC. Used in various ways as a reference point for date arithmatic.
		///</summary>
		/// <see>GetMillisecondsSince01011970UTC</see>
		public static readonly DateTime EpochJanFirst1970UTC = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// For a given local time, get the number of milliseconds since 1/1/1970 00:00:00 UTC.
		/// </summary>
		/// <param name="dt">a point in time</param>
		/// <returns>milliseconds since 1/1/1970 00:00:00 UTC</returns>
		public static long GetMillisecondsSince01011970UTC(DateTime dt)
		{
			return (long)(dt.ToUniversalTime() - EpochJanFirst1970UTC).TotalMilliseconds;
		}

		/// <summary>
		/// Returns the date using standard formatting or "n/a" if the date was DateTime.MinValue.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static string ToDateString(DateTime dateTime)
		{
			return dateTime != DateTime.MinValue ? dateTime.ToString() : Resources.DateHelper_ToDateString_Date_Not_Available;
		}

		/// <summary>
		/// Buids a UTC DateTime object for the given date.
		/// </summary>
		/// <param name="year">desired year (use 4 digit year)</param>
		/// <param name="month">1-12</param>
		/// <param name="day">1-31</param>
		/// <returns></returns>
		public static DateTime MakeUtcDateX(int year, int month, int day)
		{
			return new DateTime(year, month, day, 0, 0, 0, 0, DateTimeKind.Utc);
		}
	}
}