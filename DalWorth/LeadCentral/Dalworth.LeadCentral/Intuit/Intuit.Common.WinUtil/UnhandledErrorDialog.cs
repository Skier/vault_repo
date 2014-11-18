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
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Intuit.Common.Util;
using Intuit.Common.WinUtil.Properties;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// A dialog display stack-traces and such in case of a fatal error.
	/// </summary>
	public partial class UnhandledErrorDialog : Form
	{
		/// <summary>
		/// Constructor for the Visual Studio designer
		/// </summary>
		public UnhandledErrorDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="errorDescription">The text to be displayed.</param>
		public UnhandledErrorDialog(string errorDescription) : this()
		{
			richTextBoxErrorDescription.Text = errorDescription;
		}

		private void ButtonCloseClick(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonQuitClick(object sender, EventArgs e)
		{
			Process.GetCurrentProcess().Kill();
		}

		private void ButtonDebugClick(object sender, EventArgs e)
		{
			if (Debugger.Launch())
			{
				Close();
				Debugger.Break();
			}
		}

		private void ButtonSendEmailClick(object sender, EventArgs e)
		{
			BrowserHelper.OpenDefaultEmailClient(null, Resources.UnhandledErrorDialog_ButtonSendEmailClick_Error_Report, richTextBoxErrorDescription.Text);
		}

		private void ButtonCopyToClipboardClick(object sender, EventArgs e)
		{
			Clipboard.Clear();
			Clipboard.SetText(richTextBoxErrorDescription.Text, TextDataFormat.UnicodeText);
		}

		/// <summary>
		/// Shows an <see cref="UnhandledErrorDialog"/> for the given <param name="e">exception to be reported</param>.
		/// </summary>
		/// <param name="featureName">Name of this feature, user facing</param>
		/// <param name="featureVersion">Version of this product, user facing</param>
		/// <param name="featureNamePrefix">a prefix to be used in front of the version</param>
		public static void ReportError(Exception e, string featureName, string featureVersion, string featureNamePrefix)
		{
			IDictionary errorInfo = InitializeErrorInfo(featureName, featureVersion, featureNamePrefix);
			AggregateErrorInfo(errorInfo, e);
			ShowErrorReport(BuildErrorReport(errorInfo), "Error in " + featureName, null);
		}

		/// <summary>
		/// Show the unhandled error dialog box to the user. Used by <see cref="ReportError"/>
		/// </summary>
		public static void ShowErrorReport(string errorReport, string caption, IWin32Window owner)
		{
			using (UnhandledErrorDialog fem = new UnhandledErrorDialog(errorReport))
			{
				fem.Text = caption;
				fem.ShowDialog(owner);
			}
		}

		/// <summary>
		/// Instantiates a new errorInfo dictionary, prefills with feature name and version.
		/// </summary>
		public static IDictionary InitializeErrorInfo(string featureName, string featureVersion, string featurePrefix)
		{
			IDictionary errorInfo = new ListDictionary();
			AddErrorInformation(errorInfo, "Application", featureName);
			AddErrorInformation(errorInfo, featurePrefix + "Version", featureVersion);
			return errorInfo;
		}

		/// <summary>
		/// Recurses through inner exception hierarchy of <param name="e"/> and writes out info into <param name="errorInfo"/>
		/// </summary>
		public static void AggregateErrorInfo(IDictionary errorInfo, Exception e)
		{
			// recurse from outer most to inner most exception
			Exception current = e;
			int exceptionNumber = 0;
			while (current != null)
			{
				AddErrorInformation(errorInfo, current, exceptionNumber);

				// we use the inner most exception as a unique crash location
				AddErrorInformation(errorInfo, "Hashcode", ((UInt32)current.ToString().GetHashCode()).ToString());
				MethodBase mb = current.TargetSite;
				// if this exception was actually thrown, we can check TargetSite for information in which method it was thrown
				if (mb != null)
				{
					// we use the code location where the inner most exception was thrown
					AddErrorInformation(errorInfo, "TargetSite", mb.ToString());
				}
				current = current.InnerException;
				exceptionNumber++;
			}
		}

		/// <summary>
		/// Builds a text version of the error information, ready for display.
		/// </summary>
		/// <param name="errorInfo">dictionary of error data</param>
		/// <returns></returns>
		public static string BuildErrorReport(IDictionary errorInfo)
		{
			StringBuilder builder = new StringBuilder();

			foreach (string name in errorInfo.Keys)
			{
				builder.Append(name);
				builder.Append(" = ");
				builder.Append((string)errorInfo[name]);
				builder.Append("\n\n");
			}
			return builder.ToString();
		}

		/// <summary>
		/// Add the exception to the errorInfo dictionary.
		/// </summary>
		/// <param name="errorInfo">dictionary of error data</param>
		/// <param name="e">the exception to add</param>
		/// <param name="exceptionNumber">the exception number</param>
		private static void AddErrorInformation(IDictionary errorInfo, Exception e, int exceptionNumber)
		{
			AddErrorInformation(errorInfo, "Exception String " + exceptionNumber, e.ToString());
			AddErrorInformation(errorInfo, "Exception Type " + exceptionNumber, e.GetType().ToString());
			AddErrorInformation(errorInfo, "Exception Message " + exceptionNumber, e.Message);
			AddErrorInformation(errorInfo, "Exception StackTrace " + exceptionNumber, e.StackTrace);
		}

		/// <summary>
		/// Add this name-value pair to the errorInfo dictionary.
		/// </summary>
		/// <param name="errorInfo">dictionary of error data</param>
		/// <param name="name">the name of the error data</param>
		/// <param name="value">the value</param>
		public static void AddErrorInformation(IDictionary errorInfo, string name, string value)
		{
			StringHelper.EnforceParameterNotNull("name", name);
			value = StringHelper.EmptyToNull(value);
			if (value != null)
			{
				errorInfo[name] = value;
			}
		}
	}
}