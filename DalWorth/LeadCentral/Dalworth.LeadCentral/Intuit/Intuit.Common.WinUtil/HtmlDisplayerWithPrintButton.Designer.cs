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
	partial class HtmlDisplayerWithPrintButton
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlDisplayerWithPrintButton));
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonCopyToClipboard = new System.Windows.Forms.Button();
			this.buttonEmail = new System.Windows.Forms.Button();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// webBrowser1
			// 
			this.webBrowser1.AllowWebBrowserDrop = false;
			this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
			this.webBrowser1.Location = new System.Drawing.Point(12, 12);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.ScriptErrorsSuppressed = true;
			this.webBrowser1.Size = new System.Drawing.Size(752, 313);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.WebBrowserShortcutsEnabled = false;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.AutoSize = true;
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonClose.Location = new System.Drawing.Point(321, 3);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(100, 23);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// buttonPrint
			// 
			this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPrint.AutoSize = true;
			this.buttonPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonPrint.Location = new System.Drawing.Point(215, 3);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(100, 23);
			this.buttonPrint.TabIndex = 2;
			this.buttonPrint.Text = "Print";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.ButtonPrintClick);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.buttonCopyToClipboard);
			this.flowLayoutPanel1.Controls.Add(this.buttonEmail);
			this.flowLayoutPanel1.Controls.Add(this.buttonPrint);
			this.flowLayoutPanel1.Controls.Add(this.buttonClose);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(340, 331);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(424, 29);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// buttonCopyToClipboard
			// 
			this.buttonCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopyToClipboard.AutoSize = true;
			this.buttonCopyToClipboard.Location = new System.Drawing.Point(3, 3);
			this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
			this.buttonCopyToClipboard.Size = new System.Drawing.Size(100, 23);
			this.buttonCopyToClipboard.TabIndex = 0;
			this.buttonCopyToClipboard.Text = "Copy to clipboard";
			this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
			this.buttonCopyToClipboard.Click += new System.EventHandler(this.ButtonCopyToClipboardClick);
			// 
			// buttonEmail
			// 
			this.buttonEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEmail.AutoSize = true;
			this.buttonEmail.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonEmail.Location = new System.Drawing.Point(109, 3);
			this.buttonEmail.Name = "buttonEmail";
			this.buttonEmail.Size = new System.Drawing.Size(100, 23);
			this.buttonEmail.TabIndex = 1;
			this.buttonEmail.Text = "Email";
			this.buttonEmail.UseVisualStyleBackColor = true;
			this.buttonEmail.Click += new System.EventHandler(this.ButtonEmailClick);
			// 
			// HtmlDisplayerWithPrintButton
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(776, 372);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.webBrowser1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "HtmlDisplayerWithPrintButton";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonEmail;
		private System.Windows.Forms.Button buttonCopyToClipboard;
	}
}