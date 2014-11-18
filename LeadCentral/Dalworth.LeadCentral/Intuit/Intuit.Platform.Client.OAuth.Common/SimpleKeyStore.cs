/*
 * Copyright (c) 2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 * Contributors:
 *
 *    Intuit Partner Platform - initial contribution 
 *
 */

using System.Collections.Specialized;
using System.ComponentModel;
using Intuit.Common.Util;

namespace Intuit.Platform.Client.OAuth.Common
{
	/// <summary>
	/// A simple implementation that just saves the keys in memory. Use <see cref="ToUrlEncodedString"/> to get a URLencoded representation of the values you can persists somewhere and restore using <see cref="LoadFromUrlEncodedString"/>.
	/// </summary>
	public class SimpleKeyStore : IOAuthKeyStore, INotifyPropertyChanged
	{
		#region IOAuthKeyStore Members

		private string m_ConsumerKey;

		public string ConsumerKey
		{
			get
			{
				return m_ConsumerKey;
			}
			set
			{
				if (m_ConsumerKey != value)
				{
					m_ConsumerKey = value;
					OnPropertyChanged("ConsumerKey");
				}
			}
		}

		private string m_ConsumerSecret;

		public string ConsumerSecret
		{
			get
			{
				return m_ConsumerSecret;
			}
			set
			{
				if (m_ConsumerSecret != value)
				{
					m_ConsumerSecret = value;
					OnPropertyChanged("ConsumerSecret");
				}
			}
		}

		private string m_AccessToken;

		public string AccessToken
		{
			get
			{
				return m_AccessToken;
			}
			set
			{
				if (m_AccessToken != value)
				{
					m_AccessToken = value;
					OnPropertyChanged("AccessToken");
				}
			}
		}

		private string m_AccessTokenSecret;

		public string AccessTokenSecret
		{
			get
			{
				return m_AccessTokenSecret;
			}
			set
			{
				if (m_AccessTokenSecret != value)
				{
					m_AccessTokenSecret = value;
					OnPropertyChanged("AccessTokenSecret");
				}
			}
		}

		private string m_ParentConsumerKey;

		public string ParentConsumerKey
		{
			get
			{
				return m_ParentConsumerKey;
			}
			set
			{
				if (m_ParentConsumerKey != value)
				{
					m_ParentConsumerKey = value;
					OnPropertyChanged("ParentConsumerKey");
				}
			}
		}

		#endregion

		public override string ToString()
		{
			return ToUrlEncodedString(true);
		}

		public void LoadFromUrlEncodedString(string urlEncodedKeys)
		{
			NameValueCollection nvc = WebHelper.ParseQueryString(urlEncodedKeys);
			foreach (var key in nvc.AllKeys)
			{
				SetProperty(key, nvc[key]);
			}
		}

		private bool SetProperty(string name, string value)
		{
			switch (name)
			{
				case "ParentConsumerKey":
					ParentConsumerKey = value;
					return true;
				case "ConsumerKey":
					ConsumerKey = value;
					return true;
				case "ConsumerSecret":
					ConsumerSecret = value;
					return true;
				case "AccessToken":
					AccessToken = value;
					return true;
				case "AccessTokenSecret":
					AccessTokenSecret = value;
					return true;
				default:
					return false;
			}
		}

		public string ToUrlEncodedString(bool includeParentConsumerKey)
		{
			NameValueCollection values = new NameValueCollection();
			if (includeParentConsumerKey)
			{
				WebHelper.AddNVPairIfNotEmpty(values, "ParentConsumerKey", ParentConsumerKey);
			}
			WebHelper.AddNVPairIfNotEmpty(values, "ConsumerKey", ConsumerKey);
			WebHelper.AddNVPairIfNotEmpty(values, "ConsumerSecret", ConsumerSecret);
			WebHelper.AddNVPairIfNotEmpty(values, "AccessToken", AccessToken);
			WebHelper.AddNVPairIfNotEmpty(values, "AccessTokenSecret", AccessTokenSecret);
			return WebHelper.BuildEncodedQueryString(values);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}