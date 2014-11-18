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

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Container for various Win32 constants.
	/// </summary>
	public abstract class WindowStyles
	{
		// ReSharper disable InconsistentNaming

		/// <summary>
		/// 0x00800000
		/// </summary>
		public const uint WS_BORDER = 0x00800000;

		/// <summary>
		///  0x00C0000: WS_BORDER | WS_DLGFRAME
		/// </summary>
		public const uint WS_CAPTION = 0x00C00000; /* WS_BORDER | WS_DLGFRAME  */

		/// <summary>
		/// 0x40000000
		/// </summary>
		public const uint WS_CHILD = 0x40000000;

		/// <summary>
		/// Same as WS_CHILD
		/// </summary>
		public const uint WS_CHILDWINDOW = WS_CHILD;

		/// <summary>
		/// 0x02000000
		/// </summary>
		public const uint WS_CLIPCHILDREN = 0x02000000;

		/// <summary>
		/// 0x04000000
		/// </summary>
		public const uint WS_CLIPSIBLINGS = 0x04000000;

		/// <summary>
		/// 0x08000000
		/// </summary>
		public const uint WS_DISABLED = 0x08000000;

		/// <summary>
		/// 0x00400000
		/// </summary>
		public const uint WS_DLGFRAME = 0x00400000;

		/// <summary>
		/// 0x00000010
		/// </summary>
		public const uint WS_EX_ACCEPTFILES = 0x00000010;

		/// <summary>
		/// 0x00040000
		/// </summary>
		public const uint WS_EX_APPWINDOW = 0x00040000;

		/// <summary>
		/// 0x00000200
		/// </summary>
		public const uint WS_EX_CLIENTEDGE = 0x00000200;

		/// <summary>
		/// 0x02000000
		/// </summary>
		public const uint WS_EX_COMPOSITED = 0x02000000;

		/// <summary>
		/// 0x00000400
		/// </summary>
		public const uint WS_EX_CONTEXTHELP = 0x00000400;

		/// <summary>
		/// 0x00010000
		/// </summary>
		public const uint WS_EX_CONTROLPARENT = 0x00010000;

		/// <summary>
		/// 0x00000001
		/// </summary>
		public const uint WS_EX_DLGMODALFRAME = 0x00000001;

		/// <summary>
		/// 0x00080000
		/// </summary>
		public const uint WS_EX_LAYERED = 0x00080000;

		/// <summary>
		/// 0x00400000
		/// </summary>
		public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring

		/// <summary>
		/// 0x00000000
		/// </summary>
		public const uint WS_EX_LEFT = 0x00000000;

		/// <summary>
		/// 0x00004000
		/// </summary>
		public const uint WS_EX_LEFTSCROLLBAR = 0x00004000;

		/// <summary>
		/// 0x00000000
		/// </summary>
		public const uint WS_EX_LTRREADING = 0x00000000;

		/// <summary>
		/// 0x00000040
		/// </summary>
		public const uint WS_EX_MDICHILD = 0x00000040;

		/// <summary>
		/// 0x08000000
		/// </summary>
		public const uint WS_EX_NOACTIVATE = 0x08000000;

		/// <summary>
		/// 0x00100000
		/// </summary>
		public const uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children

		/// <summary>
		/// 0x00000004
		/// </summary>
		public const uint WS_EX_NOPARENTNOTIFY = 0x00000004;

		/// <summary>
		/// WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE
		/// </summary>
		public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);

		/// <summary>
		/// WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST
		/// </summary>
		public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);

		/// <summary>
		/// 0x00001000
		/// </summary>
		public const uint WS_EX_RIGHT = 0x00001000;

		/// <summary>
		/// 0x00000000
		/// </summary>
		public const uint WS_EX_RIGHTSCROLLBAR = 0x00000000;

		/// <summary>
		/// 0x00002000
		/// </summary>
		public const uint WS_EX_RTLREADING = 0x00002000;

		/// <summary>
		/// 0x00020000
		/// </summary>
		public const uint WS_EX_STATICEDGE = 0x00020000;

		/// <summary>
		/// 0x00000080
		/// </summary>
		public const uint WS_EX_TOOLWINDOW = 0x00000080;

		/// <summary>
		/// 0x00000008
		/// </summary>
		public const uint WS_EX_TOPMOST = 0x00000008;

		/// <summary>
		/// 0x00000020
		/// </summary>
		public const uint WS_EX_TRANSPARENT = 0x00000020;

		/// <summary>
		/// 0x00000100
		/// </summary>
		public const uint WS_EX_WINDOWEDGE = 0x00000100;

		/// <summary>
		/// 0x00020000
		/// </summary>
		public const uint WS_GROUP = 0x00020000;

		/// <summary>
		/// 0x00100000
		/// </summary>
		public const uint WS_HSCROLL = 0x00100000;

		/// <summary>
		/// WS_MINIMIZE
		/// </summary>
		public const uint WS_ICONIC = WS_MINIMIZE;

		/// <summary>
		/// 0x01000000
		/// </summary>
		public const uint WS_MAXIMIZE = 0x01000000;

		/// <summary>
		/// 0x00010000
		/// </summary>
		public const uint WS_MAXIMIZEBOX = 0x00010000;

		/// <summary>
		/// 0x20000000
		/// </summary>
		public const uint WS_MINIMIZE = 0x20000000;

		/// <summary>
		/// 0x00020000
		/// </summary>
		public const uint WS_MINIMIZEBOX = 0x00020000;

		/// <summary>
		/// 0x00000000
		/// </summary>
		public const uint WS_OVERLAPPED = 0x00000000;

		/// <summary>
		/// WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX
		/// </summary>
		public const uint WS_OVERLAPPEDWINDOW =
			(WS_OVERLAPPED |
			 WS_CAPTION |
			 WS_SYSMENU |
			 WS_THICKFRAME |
			 WS_MINIMIZEBOX |
			 WS_MAXIMIZEBOX);

		/// <summary>
		/// 0x80000000
		/// </summary>
		public const uint WS_POPUP = 0x80000000;

		/// <summary>
		/// WS_POPUP | WS_BORDER | WS_SYSMENU
		/// </summary>
		public const uint WS_POPUPWINDOW =
			(WS_POPUP |
			 WS_BORDER |
			 WS_SYSMENU);

		/// <summary>
		/// WS_THICKFRAME
		/// </summary>
		public const uint WS_SIZEBOX = WS_THICKFRAME;

		/// <summary>
		/// 0x00080000
		/// </summary>
		public const uint WS_SYSMENU = 0x00080000;

		/// <summary>
		/// 0x00010000
		/// </summary>
		public const uint WS_TABSTOP = 0x00010000;

		/// <summary>
		/// 0x00040000
		/// </summary>
		public const uint WS_THICKFRAME = 0x00040000;

		/// <summary>
		/// WS_OVERLAPPED
		/// </summary>
		public const uint WS_TILED = WS_OVERLAPPED;

		/// <summary>
		/// WS_OVERLAPPEDWINDOW
		/// </summary>
		public const uint WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

		/// <summary>
		/// 0x10000000
		/// </summary>
		public const uint WS_VISIBLE = 0x10000000;

		/// <summary>
		/// 0x00200000
		/// </summary>
		public const uint WS_VSCROLL = 0x00200000;

		// ReSharper restore InconsistentNaming
	}
}