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
using System.Runtime.InteropServices;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Container for DllImport declarations of User32.DLL functions.
	/// </summary>
	public class User32Dll
	{
		/// <summary>
		/// SetParent
		/// </summary>
		[DllImport("user32.dll")]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		/// <summary>
		/// GetParent
		/// </summary>
		[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
		public static extern IntPtr GetParent(IntPtr hWnd);

		/// <summary>
		/// SetLastError
		/// </summary>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		/// <summary>
		/// SetLastError
		/// </summary>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		/// <summary>
		/// BringWindowToTop
		/// </summary>
		[DllImport("user32.dll")]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		/// <summary>
		/// FindWindowEx
		/// </summary>
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);
	}
}