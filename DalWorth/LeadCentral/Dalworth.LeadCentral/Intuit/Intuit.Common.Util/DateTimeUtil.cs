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
using Intuit.Common.Util.Properties;

namespace Intuit.Common.Util
{
	/// <summary>
	/// Helper functions to aid with date and time related problems.
	/// </summary>
	public class DateTimeUtil
	{
		/// <summary>
		/// Cuts off any time resolution smaller than seconds
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TimeSpan CleanSecondFractions(TimeSpan value)
		{
			if (value == TimeSpan.MinValue)
			{
				return value;
			}
			return TimeSpan.FromSeconds((int)value.TotalSeconds);
		}

		/// <summary>
		/// Returns false if timespan is MinValue or Zero.
		/// </summary>
		/// <param name="duration"></param>
		/// <returns></returns>
		public static bool NonZeroTimeSpan(TimeSpan duration)
		{
			return duration != TimeSpan.MinValue && duration != TimeSpan.Zero;
		}

		/// <summary>
		/// Rounds timespan to seconds. The rounding is perfomed via a double to int cast.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int RoundToSeconds(TimeSpan t)
		{
			return (int)t.TotalSeconds;
		}

		/// <summary>
		/// The day of when a period ends, the period being defined by when it begins and how many days it is.
		/// Essentially the day before the next period starts.
		/// </summary>
		/// <param name="beginDate"></param>
		/// <param name="periodLength"></param>
		/// <returns></returns>
		public static DateTime DatePeriodEndDate(DateTime beginDate, int periodLength)
		{
			return beginDate.AddDays(periodLength - 1).Date;
		}

		/// <summary>
		/// Strips down the time resoluion and only leaves the date part (i.e. midnight that day).
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DateTime RemoveTimeComponent(DateTime value)
		{
			return value.Date;
		}

		/// <summary>
		/// Attempts to interpret a user-entered string as a time duration.
		/// </summary>
		/// <param name="durStr"></param>
		/// <param name="valid"></param>
		public static TimeSpan ParseDurationInput(string durStr, out bool valid)
		{
			valid = false;
			if (String.IsNullOrEmpty(durStr))
			{
				valid = true;
				return TimeSpan.MinValue;
			}
			Double dur = 0.0;
			if (durStr.EndsWith("h"))
			{
				durStr = durStr.Remove(durStr.Length - 1);
			}
			else if (durStr.EndsWith("min"))
			{
				// "30min" => "0:30"
				durStr = ":" + durStr.Remove(durStr.Length - 3);
			}
			if (durStr.IndexOf(":") >= 0)
			{
				string[] subStrs = durStr.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
				if (subStrs.Length <= 2)
				{
					Double hours = 0;
					if (subStrs.Length == 1 || Double.TryParse(subStrs[0], out hours))
					{
						Double mins;
						if (Double.TryParse(subStrs[subStrs.Length - 1], out mins))
						{
							Double fracHours = mins / 60;
							dur = hours + fracHours;
							valid = true;
						}
					}
				}
			}
			else
			{
				if (Double.TryParse(durStr, out dur))
				{
					valid = true;
				}
			}
			if (dur == 0)
			{
				return TimeSpan.MinValue;
			}
			return TimeSpan.FromHours(dur);
		}

		/// <summary>
		/// Throws FormatException if the value doesn't parse!
		/// </summary>
		public static string FormatTimeSpan(string val)
		{
			return FormatTimeSpan(val, false);
		}

		/// <summary>
		/// Throws FormatException if the value doesn't parse!
		/// </summary>
		public static string FormatTimeSpan(string val, bool decimalPortions)
		{
			TimeSpan dur = TimeSpan.Parse(val);
			if (dur == TimeSpan.MinValue || dur.TotalSeconds == 0)
			{
				return String.Empty;
			}
			if (decimalPortions)
			{
				if (dur.TotalHours < 1)
				{
					return dur.TotalMinutes + "min";
				}
				return dur.TotalHours + "h";
			}
			StringBuilder s = new StringBuilder();
			int wholehours = (int)dur.TotalHours;
			s.Append(wholehours);
			s.Append(":");
			dur = dur.Subtract(TimeSpan.FromHours(wholehours));
			if (dur.TotalMinutes < 10)
			{
				s.Append("0");
			}
			s.Append(dur.TotalMinutes);
			return s.ToString();
		}

		/// <summary>
		/// Throws FormatException if the value doesn't parse!
		/// </summary>
		public static string FormatDateTimeForTimeClock(string val)
		{
			DateTime date = DateTime.Parse(val);

			if (date == DateTime.MinValue)
			{
				return String.Empty;
			}
			return DateTime.Parse(val).ToString(Resources.FormatDateTimeForTimeClock);
		}

		/// <summary>
		/// Creates an array of DateTime objects for each day in the given date range.
		/// </summary>
		public static DateTime[] CreateDateTimeArrayBasedOnDateRange(DateTime begin, DateTime end)
		{
			TimeSpan t = end.Subtract(begin);
			int len = (int)t.TotalDays;
			DateTime[] arrayBasedOnDateRange = new DateTime[len + 1];
			for (int i = 0; i <= len; i++)
			{
				arrayBasedOnDateRange[i] = begin.AddDays(i);
			}
			return arrayBasedOnDateRange;
		}

		/// <summary>
		/// Determines if date is the given date range.
		/// </summary>
		public static bool DateRangeContainsDate(DateTime date, DateTime beginRange, DateTime endRange)
		{
			return beginRange.CompareTo(date) <= 0 && endRange.CompareTo(date) >= 0;
		}

		/// <summary>
		/// If the string contains a time duration in [hours:]min:secs format, converts it to a decimal representation.
		/// </summary>
		/// <seealso cref="ConvertHoursMinutesFormatToDecimal"/>
		/// <seealso cref="CheckForHoursMinutesFormat"/>
		public static string ConvertToDecimalIfRequired(string qty, string decimalFormat)
		{
			if (CheckForHoursMinutesFormat(qty))
			{
				return ConvertHoursMinutesFormatToDecimal(qty).ToString(decimalFormat);
			}
			return qty;
		}

		/// <summary>
		/// Takes a string that's assumed to be a time duration in [hours:]min:secs format and converts it to a decimal representing the hours (and fractions of hours).
		/// Examples:
		/// 2:00:00 -> 2
		/// 2:30:00 -> 2.5
		/// 0:30 -> 0.5
		/// 0:08 -> 0.002222222... (8/3600)
		/// </summary>
		/// <exception cref="Exception"></exception>
		public static decimal ConvertHoursMinutesFormatToDecimal(string durationInHoursAndMinutes)
		{
			var parts = durationInHoursAndMinutes.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
			decimal hours,
			        minutes,
			        seconds = 0;
			if (parts.Length == 3)
			{
				hours = Decimal.Parse(parts[0]);
				minutes = Decimal.Parse(parts[1]);
				seconds = Decimal.Parse(parts[2]);
			}
			else if (parts.Length == 2)
			{
				hours = Decimal.Parse(parts[0]);
				minutes = Decimal.Parse(parts[1]);
			}
			else
			{
				throw new Exception(string.Format(Resources.Error_ConvertHoursMinutesFormatToDecimal_Unable_to_parse_time_duration, durationInHoursAndMinutes));
			}
			decimal dec = hours + ((minutes + (seconds / 60)) / 60);
			return dec;
		}

		/// <summary>
		/// Simply checks if there's a colon in the string.
		/// </summary>
		public static bool CheckForHoursMinutesFormat(string qty)
		{
			return qty != null && qty.Contains(":");
		}

		/// <summary>
		/// Orders the Date/Times so that if supposedEarlierDateTime refers to a date/time that's after supposedLaterDateTime, the two will be swapped.
		/// </summary>
		public static void OrderDates(ref DateTime supposedEarlierDateTime, ref DateTime supposedLaterDateTime)
		{
			if (supposedEarlierDateTime.CompareTo(supposedLaterDateTime) > 0)
			{
				DateTime temp = supposedEarlierDateTime;
				supposedEarlierDateTime = supposedLaterDateTime;
				supposedLaterDateTime = temp;
			}
		}
	}
}