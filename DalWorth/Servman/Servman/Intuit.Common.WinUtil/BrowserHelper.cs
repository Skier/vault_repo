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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Intuit.Common.Util;
using Intuit.Common.WinUtil.Properties;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Functions to help with interacting with web browsers.
	/// </summary>
	public class BrowserHelper
	{
		private const string InternetExplorer = "IExplore.exe";

		/// <summary>
		/// Opens a URL in the OS' default browser. If a problem occurs, will try to fall back on Internet Explorer, and if that fails will show dialog with error message
		/// </summary>
		/// <param name="uri">the URL to open</param>
		/// <param name="showAlertOnError">if true, Win32Exceptions are caught and displayed as a message box.</param>
		public static void OpenUrlInDefaultBrowser(Uri uri, bool showAlertOnError)
		{
			try
			{
				// Let Windows figure out how to open the URL, it will choose the default browser
				Process.Start(uri.ToString());
			}
			catch (Win32Exception we)
			{
				// System.ComponentModel.Win32Exception ("The system cannot find the file specified") is a known exception that occurs when Firefox is default browser.
				// It actually opens the browser but STILL throws this exception so we can just ignore it.
				// See http://www.devtoolshed.com/node/23 http://kb.mozillazine.org/Windows_error_opening_Internet_shortcut_or_local_HTML_file_-_Firefox for some background
				if (we.NativeErrorCode == 2)
				{
					return;
				}
				// If not this exception, then attempt to open the URL in IE instead.
				if (showAlertOnError)
				{
					try
					{
						Process.Start(InternetExplorer, uri.ToString());
					}
					catch (Win32Exception we2)
					{
						MessageBox.Show(String.Format(Resources.BrowserHelper_OpenUrlInDefaultBrowser_, uri, we2), Resources.BrowserHelper_OpenUrlInDefaultBrowser_Problem_opening_web_browser);
					}
				}
				else
				{
					Process.Start(InternetExplorer, uri.ToString());
				}
			}
		}

		///<summary>
		/// Convenience function for OpenDefaultEmailClient if you don't want to specify CC or BCC.
		///</summary>
		public static void OpenDefaultEmailClient(string emailAddress, string subjectLine, string emailBody)
		{
			OpenDefaultEmailClient(emailAddress, subjectLine, emailBody, null, null);
		}

		/// <summary>
		/// Uses the mailto: protocol handler to open the default email client to send an email.
		/// </summary>
		/// <exception cref="Win32Exception">if Windows reports an error, with the exception of error 1155 ("No application is associated with the specified file for this operation"), in which case this function just return false.</exception>
		/// <returns>true if no error was reported, false if Windows reported error 1155 ("No application is associated with the specified file for this operation")</returns>
// ReSharper disable MemberCanBePrivate.Global
		public static bool OpenDefaultEmailClient(string emailAddress, string subjectLine, string emailBody, string ccAdress, string bccAddress)
// ReSharper restore MemberCanBePrivate.Global
		{
			try
			{
				Process.Start(WebHelper.BuildMailtoUri(emailAddress, subjectLine, emailBody, ccAdress, bccAddress));
			}
			catch (Win32Exception win32Exception)
			{
				if (win32Exception.NativeErrorCode != 1155) // error code 1155 means no application is registered to handle the mailto: scheme.
				{
					throw;
				}
				return false;
			}
			return true;
		}

		/// <summary>
		/// Attempts to resize the web browser so it fits the web page it's displaying. Call this right after the WebBrowser.DocumentCompleted event.
		/// </summary>
		/// <param name="controlInChargeOfSize">The control to resize. e.g., if the WebBrowser is docked in a panel or form, pass that container.
		/// If the WebBrowser is not auto-sized based on some other control, pass in the WebBrowser control itself.
		/// </param>
		/// <param name="webBrowser">the WebBrowser control</param>
		public static void ResizeBrowserToPage(Control controlInChargeOfSize, WebBrowser webBrowser)
		{
			if (webBrowser.Document != null && webBrowser.Document.Window != null && webBrowser.Document.Body != null)
			{
				Size page = webBrowser.Document.Body.ScrollRectangle.Size;
				Size curr = webBrowser.ClientSize;
				// first, shrink until our control is definitely smaller than the page
				while (curr.Width >= page.Width || curr.Height >= page.Height)
				{
					controlInChargeOfSize.Width -= page.Width / 3;
					controlInChargeOfSize.Height -= page.Height/ 4;
					controlInChargeOfSize.PerformLayout();
					page = webBrowser.Document.Body.ScrollRectangle.Size;
					curr = webBrowser.ClientSize;
				}
				Size margin = webBrowser.Margin.Size;
				Size newSize = page + margin;
				Size sizeDiff = newSize - curr;
				controlInChargeOfSize.Size += sizeDiff;
				controlInChargeOfSize.PerformLayout();
				Size outerSize = webBrowser.ClientRectangle.Size;
				Size innerSize = webBrowser.Document.Window.Size;
				Size adornmentSize = outerSize - innerSize;
				controlInChargeOfSize.Size += adornmentSize;
				Application.DoEvents();
			}
		}
	}
}