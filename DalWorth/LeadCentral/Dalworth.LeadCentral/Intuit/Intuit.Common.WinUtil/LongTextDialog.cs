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
	/// A simple dialog that shows two texts, one called description and one called content, and optionally allows the content to be editable.
	/// </summary>
	public partial class LongTextDialog : Form
	{
		private LongTextDialog(string caption, string description, string content, bool editable)
		{
			InitializeComponent();
			Load += ((sender, e) => Text = caption);
			buttonSave.Visible = editable;
			buttonSave.Enabled = editable;
			richTextBoxDescription.Text = description;
			richTextBoxContent.Text = content;
			// read the current text, since the control does some stuff with whitespace and line breaks etc that will cause the content to look changed otherwise.
			SavedText = richTextBoxContent.Text;
			richTextBoxContent.ReadOnly = !editable;
		}

		/// <summary>
		/// Displays an instance of this class (modal to the provided owner) and returns the edited content.
		/// </summary>
		public static string ShowText(IWin32Window owner, string caption, string text, string description, bool editable)
		{
			using (LongTextDialog f = new LongTextDialog(caption, description, (text ?? ""), editable))
			{
				f.ShowDialog(owner);
				return f.SavedText;
			}
		}

		private void ButtonSaveClick(object sender, EventArgs e)
		{
			SavedText = richTextBoxContent.Text;
		}

		///<summary>
		/// The contents of the lower section of the dialog
		///</summary>
		public string SavedText { get; set; }

		private void ButtonWordWrapClick(object sender, EventArgs e)
		{
			richTextBoxContent.WordWrap = !richTextBoxContent.WordWrap;
		}
	}
}