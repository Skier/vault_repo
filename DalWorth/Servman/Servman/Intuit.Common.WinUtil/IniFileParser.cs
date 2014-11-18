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

using System.Runtime.InteropServices;
using System.Text;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Class that helps with parsing .INI style files on Windows
	/// </summary>
	public class IniFileParser
	{
		private const int MaxStringlen = 32768;

		private readonly string m_Filename;

		[DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi)]
		private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault,
		                                                  StringBuilder lpReturnedString, int nSize, string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileSectionNamesA", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, int nSize, string lpFileName);

		/// <summary>
		/// Instantiates IniFileParser for the given INI file.
		/// </summary>
		/// <param name="file"></param>
		public IniFileParser(string file)
		{
			m_Filename = file;
		}

		/// <summary>Reads a String from the specified key of the specified section.</summary>
		/// <param name="section">The section to search in.</param>
		/// <param name="key">The key from which to return the value.</param>
		/// <param name="defVal">The value to return if the specified key isn't found.</param>
		/// <returns>Returns the value of the specified section/key pair, or returns the default value if the specified section/key pair isn't found in the INI file.</returns>
		public string ReadString(string section, string key, string defVal)
		{
			StringBuilder sb = new StringBuilder(MaxStringlen);
			GetPrivateProfileString(section, key, defVal, sb, MaxStringlen, m_Filename);
			return sb.ToString();
		}

		/// <summary>
		/// Writes a private profile string (key=value) to the given section
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Write(string section, string key, string value)
		{
			return (WritePrivateProfileString(section, key, value, m_Filename) != 0);
		}

		/// <summary>Retrieves a list of all available sections in the INI file.</summary>
		/// <returns>Returns an ArrayList with all available sections.</returns>
		public string[] GetSectionNames()
		{
			try
			{
				byte[] buffer = new byte[MaxStringlen];
				GetPrivateProfileSectionNames(buffer, MaxStringlen, m_Filename);
				string[] parts = Encoding.ASCII.GetString(buffer).Trim('\0').Split('\0');
				return parts;
			}
			catch
			{
				return null;
			}
		}
	}
}