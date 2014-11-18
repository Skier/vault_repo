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
using System.IO;
using System.Windows.Forms;
using Intuit.Common.Util;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// A simple window that displays some HTML in a web browser control, with convenience buttons to Print, Email, Copy To Clipboard.
	/// </summary>
	public partial class HtmlDisplayerWithPrintButton : Form
	{
		private readonly string m_Text;

		/// <summary>
		/// Constructor for the the Visual Studio designer
		/// </summary>
		public HtmlDisplayerWithPrintButton()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Convenience constructor for when you're displaying the details of a completed <see cref="TimedWorkWithResultsDisplayed"/>.
		/// </summary>
		/// <param name="work">completed work</param>
		public HtmlDisplayerWithPrintButton(TimedWorkWithResultsDisplayed work)
			: this(work.GetResultHtml(), work.SuggestedMessageCaption(true), work.GetResultMessage())
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="html">The HTML to be displayed in the browser control (required)</param>
		/// <param name="caption">text for title bar of this window</param>
		/// <param name="text">Optional plain text version of the HTML content (use null if you don't have it). Used if user chooses to copy content to clipboard (will copy both HTML and text version of the content to clipboard).</param>
		// ReSharper disable MemberCanBePrivate.Global
		public HtmlDisplayerWithPrintButton(string html, string caption, string text)
			// ReSharper restore MemberCanBePrivate.Global
			: this()
		{
			m_Text = text;
			StringHelper.EnforceParameterNotNull("html", html);
			if (!String.IsNullOrEmpty(caption))
			{
				Load += ((sender, e) => Text = caption);
			}
			webBrowser1.Navigate("about:blank");
			if (webBrowser1.Document != null)
			{
				HtmlDocument doc = webBrowser1.Document.OpenNew(true);

				if (doc != null)
				{
					doc.Write(html);
				}
			}
		}

		private void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonPrintClick(object sender, EventArgs e)
		{
			webBrowser1.Print();
		}

		private void ButtonEmailClick(object sender, EventArgs e)
		{
			if (webBrowser1.Document != null)
			{
				BrowserHelper.OpenDefaultEmailClient(null, '"' + Text + '"', m_Text??webBrowser1.DocumentText);
			}
		}

		private void ButtonCopyToClipboardClick(object sender, EventArgs e)
		{
			if (webBrowser1.Document != null)
			{
				string html = webBrowser1.DocumentText;
				string text = m_Text ?? html;

				using (MemoryStream utf8Html = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
				{
					DataObject data = new DataObject();
					data.SetData(DataFormats.UnicodeText, text);
					data.SetData(DataFormats.Html, utf8Html);
					Clipboard.Clear();
					Clipboard.SetDataObject(data, true);
				}
			}
		}
	}
}