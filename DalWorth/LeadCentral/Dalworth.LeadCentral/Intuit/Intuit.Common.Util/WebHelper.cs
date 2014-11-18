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
using System.Collections.Specialized;
using System.Web;

namespace Intuit.Common.Util
{
	/// <summary>
	/// Helper functions to help with URLs and other WWW stuff.
	/// </summary>
	public class WebHelper
	{
		// ReSharper disable MemberCanBePrivate.Global

		/// <summary>
		/// Some browsers will not reload the application unless the URL changes. Appending this (useless) timestamp parameter tricks the browser into reloading the app freshly.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static Uri WorkaroundCachingProblemByAddingFakeParameter(Uri url)
		{
			return AppendQueryParameter(url, "timestamp", DateHelper.GetMillisecondsSince01011970UTC(DateTime.Now).ToString());
		}

		/// <summary>
		/// Appends the name-value pair to the query component of the Uri, UrlEncoding them in the process.
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Uri AppendQueryParameter(Uri uri, string name, string value)
		{
			return AppendQueryString(uri, UrlEncodeNameValuePair(name, value));
		}

		/// <summary>
		/// UrlEncodes each name value pair using <see cref="UrlEncodeNameValuePair"/> and joins them together using an ampersand.
		/// </summary>
		public static string BuildEncodedQueryString(NameValueCollection nvc)
		{
			if (nvc == null || nvc.Count == 0)
			{
				return String.Empty;
			}
			return String.Join("&", Array.ConvertAll(nvc.AllKeys, key => UrlEncodeNameValuePair(key, nvc[key])));
		}

		/// <summary>
		/// UrlEncodes both name and value and puts them together as "name=value".
		/// </summary>
		public static string UrlEncodeNameValuePair(string name, string value)
		{
			return String.Format("{0}={1}", UrlEncode(name), UrlEncode(value));
		}

		/// <summary>
		/// Appends the (already URL-encoded) query string (something like &quot;name=value&amp;name2=value2&quot;) to the provided URI's query string.
		/// </summary>
		/// <param name="uri">an existing URL with or without existing query parameters</param>
		/// <param name="queryString">new query parameters to be added to the URL. if there are already some on the URL, these new ones will be added to the end</param>
		/// <returns>the URL with the new queryString appended to the end</returns>
		public static Uri AppendQueryString(Uri uri, string queryString)
		{
			if (queryString == null)
			{
				return uri;
			}
			while (queryString.StartsWith("&") || queryString.StartsWith("?"))
			{
				queryString = queryString.Substring(1);
			}
			if (queryString.Length == 0)
			{
				return uri;
			}
			var baseUri = new UriBuilder(uri);
			if (baseUri.Query != null && baseUri.Query.Length > 1)
			{
				string baseQuery = baseUri.Query.Substring(1);
				baseUri.Query = baseQuery + (baseQuery.EndsWith("&") ? String.Empty : "&") + queryString;
			}
			else
			{
				baseUri.Query = queryString;
			}
			return baseUri.Uri;
		}

		/// <summary>
		/// Adds the name-value pair to the collection unless the name or the value are null or empty
		/// </summary>
		public static void AddNVPairIfNotEmpty(NameValueCollection nvc, string name, string value)
		{
			if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(value))
			{
				nvc.Add(name, value);
			}
		}

		/// <summary>
		/// Wraps <see cref="HttpUtility.HtmlEncode(string)"/>
		/// </summary>
		public static string HtmlEncode(string unencodedString)
		{
			return HttpUtility.HtmlEncode(unencodedString);
		}

		/// <summary>
		/// Wraps <see cref="Uri.EscapeDataString(string)"/>
		/// </summary>
		public static string UrlEncode(string unencodedString)
		{
			return Uri.EscapeDataString(unencodedString);
		}

		/// <summary>
		/// Wraps <see cref="HttpUtility.UrlDecode(string)"/>
		/// </summary>
		public static string UrlDecode(string encodedString)
		{
			return HttpUtility.UrlDecode(encodedString);
		}

		/// <summary>
		/// Wraps <see cref="HttpUtility.ParseQueryString(string)"/>.
		/// </summary>
		/// <param name="query">a query string from a URL</param>
		/// <returns>the query parameters broken up into name-value pairs</returns>
		public static NameValueCollection ParseQueryString(string query)
		{
			return HttpUtility.ParseQueryString(query);
		}

		///<summary>
		/// Builds a mailto: style URI for the given parameters
		///</summary>
		///<param name="emailAddress">main recipient</param>
		///<param name="subjectLine">subject line for email</param>
		///<param name="emailBody">body of the email</param>
		///<param name="ccAdress">carbon copy recipient</param>
		///<param name="bccAddress">blind carbon copy recipient</param>
		///<returns>the complete URI</returns>
		public static string BuildMailtoUri(string emailAddress, string subjectLine, string emailBody, string ccAdress, string bccAddress)
		{
			String url = "mailto:";
			if (!String.IsNullOrEmpty(emailAddress))
			{
				url += UrlEncode(emailAddress);
			}
			NameValueCollection nvc = new NameValueCollection();
			AddNVPairIfNotEmpty(nvc, "subject", subjectLine);
			AddNVPairIfNotEmpty(nvc, "body", emailBody);
			AddNVPairIfNotEmpty(nvc, "cc", ccAdress);
			AddNVPairIfNotEmpty(nvc, "bcc", bccAddress);
			String queryString = BuildEncodedQueryString(nvc);
			if (!String.IsNullOrEmpty(queryString))
			{
				url += "?" + queryString;
			}
			return url;
		}

		// ReSharper restore MemberCanBePrivate.Global
	}
}