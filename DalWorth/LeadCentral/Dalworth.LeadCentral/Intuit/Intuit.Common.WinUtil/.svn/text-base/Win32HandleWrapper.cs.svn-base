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
using System.Windows.Forms;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Use with caution! If you have a handle to a Win32 window, you can use this to create an instance of IWin32Window for it.
	/// </summary>
	public class Win32HandleWrapper : IWin32Window
	{
		private IntPtr m_Handle = IntPtr.Zero;

		/// <summary>
		/// Wrap the given Win32 window handle.
		/// </summary>
		public Win32HandleWrapper(IntPtr h)
		{
			Handle = h;
		}

		#region IWin32Window Members

		/// <summary>
		/// The Win32 window handle
		/// </summary>
		public IntPtr Handle
		{
			get
			{
				return m_Handle;
			}
			set
			{
				m_Handle = value;
			}
		}

		#endregion
	}
}