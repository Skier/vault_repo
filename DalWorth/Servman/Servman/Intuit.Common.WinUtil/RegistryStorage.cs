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
using System.Security.Cryptography;
using System.Text;
using Intuit.Common.WinUtil.Properties;
using Microsoft.Win32;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// A simple way to store "secured" values in the registry, using the built-in Windows facility to scramble values using the logged-in user's Windows account credentials (<see cref="ProtectedData"/>).
	/// </summary>
	public class RegistryStorage
	{
		private readonly byte[] m_AdditionalEntropy;
		private readonly string m_RegKey;
		private char m_BracketChar = 'X';
		private Encoding m_Encoding = Encoding.ASCII;

		/// <summary>
		/// Instantiate RegistryStorage to save scrambled values under the given registryKey, using the additionalEntropy to enhance the level of security. Always use the same additionalEntropy for encryption and decryption.
		/// </summary>
		public RegistryStorage(string registryKey, byte[] additionalEntropy)
		{
			m_RegKey = registryKey;
			m_AdditionalEntropy = additionalEntropy;
		}

		/// <summary>
		/// To distinguish null from empty string, non-null values are marked by adding the BracketChar in before and after the value saved into the registry.
		/// By default an 'X' is used.
		/// Example:
		/// value == null -&gt; registry value = ""
		/// value == "" -&gt; registry value = "XX"
		/// value = "myvalue" -&gt; registry value = "Xmyvalue"
		/// </summary>
		public char BracketChar
		{
			get
			{
				return m_BracketChar;
			}
			set
			{
				m_BracketChar = value;
			}
		}


		/// <summary>
		/// The encoding to use to store the data in the registry. By default ASCII.
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				return m_Encoding;
			}
			set
			{
				m_Encoding = value;
			}
		}

		/// <summary>
		/// Deletes the registry key that was specified in the constructor.
		/// </summary>
		public void RemoveRegistryKey()
		{
			if (m_RegKey != null)
			{
				Registry.CurrentUser.DeleteSubKey(m_RegKey, false);
			}
		}

		/// <summary>
		/// Sets a value in the registry with the given name and data. The value will be "proteced" using the currently logged-in user's Windows account credentials.
		/// </summary>
		public void SaveSecureRegistryValue(string name, string data)
		{
			if (m_RegKey == null)
			{
				return;
			}
			using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(m_RegKey))
			{
				if (rk == null)
				{
					throw new Exception(Resources.RegistryStorage_SaveSecureRegistryValue_Unable_to_create_registry_entry);
				}
				string bracketedString = BracketString(data);
				byte[] bytes = Encoding.GetBytes(bracketedString);
				byte[] encryptedBytes = ProtectedData.Protect(bytes, m_AdditionalEntropy, DataProtectionScope.CurrentUser);
				rk.SetValue(name, encryptedBytes, RegistryValueKind.Binary);
			}
		}

		/// <summary>
		/// Loads registry key data and decrypts the data.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="data">is set to decrypted data, if found, or null if key or value not found</param>
		/// <returns>true if the key existed and a data was found</returns>
		public bool LoadSecureRegistryValue(string name, out string data)
		{
			if (m_RegKey != null)
			{
				using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(m_RegKey))
				{
					if (rk != null)
					{
						var encryptedData = rk.GetValue(name, null) as byte[];
						if (encryptedData != null)
						{
							try
							{
								byte[] decryptedBytes = ProtectedData.Unprotect(encryptedData, m_AdditionalEntropy, DataProtectionScope.CurrentUser);
								string decryptedString = Encoding.GetString(decryptedBytes);
								data = UnbracketString(decryptedString);
								return true;
							}
							catch (CryptographicException)
							{
								// ignore
							}
						}
					}
				}
			}
			data = null;
			return false;
		}

		/// <summary>
		/// We the bracket the value, to distinguish null from empty string.
		/// "" -> null
		/// BracketChar+BracketChar -> ""
		/// BracketChar+someString+BracketChar -> "someString"
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private string BracketString(string s)
		{
			if (s == null)
			{
				return String.Empty;
			}
			return BracketChar + s + BracketChar;
		}

		private string UnbracketString(string pstring)
		{
			if (String.IsNullOrEmpty(pstring))
			{
				return null;
			}
			if (pstring.Length < 2 || pstring[0] != BracketChar || pstring[pstring.Length - 1] != BracketChar)
			{
				return null;
			}
			return pstring.Substring(1, pstring.Length - 2);
		}

		/// <summary>
		/// Convenience function that wraps <see cref="LoadSecureRegistryValue"/>, either returns the value beloging to the name, or null if the name wasn't found.
		/// </summary>
		public string LoadFromRegistryOrReturnNull(string name)
		{
			string value;
			if (LoadSecureRegistryValue(name, out value))
			{
				return value;
			}
			return null;
		}

		/// <summary>
		/// Convenience function that wraps <see cref="SaveSecureRegistryValue"/>. If value is not null, saves it under the given name; if value is null, deletes the registry value with the given name.
		/// </summary>
		public void SaveToRegistryIfNotNull(string name, string value)
		{
			if (value != null)
			{
				SaveSecureRegistryValue(name, value);
			}
			else
			{
				DeleteRegistryValue(name);
			}
		}

		/// <summary>
		/// Delete the registry value with the given name.
		/// </summary>
		public void DeleteRegistryValue(string name)
		{
			if (m_RegKey == null)
			{
				return;
			}
			using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(m_RegKey, true))
			{
				if (rk == null)
				{
					return;
				}
				if (rk.GetValue(name, null) != null)
				{
					rk.DeleteValue(name);
				}
			}
		}
	}
}