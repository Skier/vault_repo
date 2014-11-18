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

using System;
using System.Windows.Forms;
using Intuit.Common.Util;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Simple web browser form that knows how to intercept a URL.
	/// </summary>
	public partial class SimpleBrowser : Form
	{
		/// <summary>
		/// Create new instance. Make sure to Dispose, browsers are expensive!
		/// </summary>
		public SimpleBrowser()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Set this to the URL to first browse to. Must be set before the form is shown.
		/// </summary>
		public string InitialUrl { get; set; }

		/// <summary>
		/// Set this to a URL you want to intercept. Any navigation to an URL that starts with this will be intercepted, ie. will close the form with DialogResult.OK and you can retrieve it using <see cref="InterceptedUrl"/>.
		/// </summary>
		public string InterceptUrl { get; set; }

		/// <summary>
		/// The URL that was intercepted (<see cref="InterceptUrl"/>). Use only if form closed with DialogResult.OK.
		/// </summary>
		public Uri InterceptedUrl { get; set; }

		/// <summary>
		/// The text visible in the tool strip on top of the window.
		/// </summary>
		public string ToolstripCaption
		{
			get
			{
				return toolStripLabelCaption.Text;
			}
			set
			{
				toolStripLabelCaption.Text = value;
			}
		}

		/// <summary>
		/// The text in the title bar
		/// </summary>
		public string WindowTitle
		{
			get
			{
				return Text;
			}
			set
			{
				Text = value;
			}
		}

		/// <summary>
		/// If set to true, form will attempt to resize itself after each page navigation so the whole page is shown without scrollbars.
		/// </summary>
		public bool ResizeToPageSize { get; set; }

		/// <summary>
		/// Override Form.OnLoad
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			webBrowserCtl.Navigating += WebBrowserNavigating;
			toolStripProgressBar1.Available = false;
			webBrowserCtl.ProgressChanged += WebBrowserCtlProgressChanged;
			webBrowserCtl.CanGoBackChanged += WebBrowserCtlCanGoBackChanged;
			webBrowserCtl.DocumentCompleted += WebBrowserCtlDocumentCompleted;
			WebBrowserCtlCanGoBackChanged(null, null);
		}

		private void WebBrowserCtlDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (ResizeToPageSize)
			{
				BrowserHelper.ResizeBrowserToPage(this, webBrowserCtl);
			}
		}

		private void WebBrowserCtlCanGoBackChanged(object sender, EventArgs e)
		{
			toolStripButtonBack.Enabled = webBrowserCtl.CanGoBack;
		}

		private void WebBrowserCtlProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
		{
			if (e.CurrentProgress == 0 || e.MaximumProgress == 0 || e.CurrentProgress == e.MaximumProgress)
			{
				toolStripProgressBar1.Available = false;
			}
			else
			{
				toolStripProgressBar1.Available = true;
				int max;
				int curr;
				StringHelper.ScaleTotalAndPartial(e.MaximumProgress, e.CurrentProgress, out max, out curr);
				toolStripProgressBar1.Maximum = max;
				toolStripProgressBar1.Value = curr;
			}
		}

		/// <summary>
		/// Override Form.OnShown
		/// </summary>
		/// <param name="e"></param>
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			if (InitialUrl != null)
			{
				webBrowserCtl.Navigate(InitialUrl);
			}
		}

		private void WebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e != null && e.Url != null && InterceptUrl != null)
			{
				string url = e.Url.ToString();
				if (url.StartsWith(InterceptUrl))
				{
					InterceptedUrl = e.Url;
					DialogResult = DialogResult.OK;
					Close();
				}
			}
		}

		private void CloseButtonClicked(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void BackButtonClicked(object sender, EventArgs e)
		{
			webBrowserCtl.GoBack();
		}

		private void ReloadButtonClicked(object sender, EventArgs e)
		{
			webBrowserCtl.Refresh(WebBrowserRefreshOption.Completely);
		}
	}
}