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
	partial class LongTextDialog
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.labelContent = new System.Windows.Forms.Label();
			this.labelDescription = new System.Windows.Forms.Label();
			this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
			this.richTextBoxContent = new System.Windows.Forms.RichTextBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonWordWrap = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.AutoSize = true;
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(770, 554);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                     | System.Windows.Forms.AnchorStyles.Left)
			                                                                    | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.labelContent);
			this.splitContainer1.Panel1.Controls.Add(this.labelDescription);
			this.splitContainer1.Panel1.Controls.Add(this.richTextBoxDescription);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.richTextBoxContent);
			this.splitContainer1.Size = new System.Drawing.Size(833, 536);
			this.splitContainer1.SplitterDistance = 136;
			this.splitContainer1.TabIndex = 2;
			// 
			// labelContent
			// 
			this.labelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelContent.AutoSize = true;
			this.labelContent.Location = new System.Drawing.Point(0, 118);
			this.labelContent.Name = "labelContent";
			this.labelContent.Size = new System.Drawing.Size(47, 13);
			this.labelContent.TabIndex = 2;
			this.labelContent.Text = "Content:";
			// 
			// labelDescription
			// 
			this.labelDescription.AutoSize = true;
			this.labelDescription.Location = new System.Drawing.Point(4, 6);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(63, 13);
			this.labelDescription.TabIndex = 1;
			this.labelDescription.Text = "Description:";
			// 
			// richTextBoxDescription
			// 
			this.richTextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                            | System.Windows.Forms.AnchorStyles.Left)
			                                                                           | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxDescription.Location = new System.Drawing.Point(0, 22);
			this.richTextBoxDescription.Name = "richTextBoxDescription";
			this.richTextBoxDescription.ReadOnly = true;
			this.richTextBoxDescription.Size = new System.Drawing.Size(830, 93);
			this.richTextBoxDescription.TabIndex = 0;
			this.richTextBoxDescription.Text = "";
			// 
			// richTextBoxContent
			// 
			this.richTextBoxContent.AcceptsTab = true;
			this.richTextBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxContent.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxContent.Name = "richTextBoxContent";
			this.richTextBoxContent.ReadOnly = true;
			this.richTextBoxContent.Size = new System.Drawing.Size(833, 396);
			this.richTextBoxContent.TabIndex = 0;
			this.richTextBoxContent.Text = "";
			this.richTextBoxContent.WordWrap = false;
			// 
			// buttonSave
			// 
			this.buttonSave.AutoSize = true;
			this.buttonSave.Enabled = false;
			this.buttonSave.Location = new System.Drawing.Point(678, 554);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(86, 23);
			this.buttonSave.TabIndex = 3;
			this.buttonSave.Text = "Save changes";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
			// 
			// buttonWordWrap
			// 
			this.buttonWordWrap.AutoSize = true;
			this.buttonWordWrap.Location = new System.Drawing.Point(570, 554);
			this.buttonWordWrap.Name = "buttonWordWrap";
			this.buttonWordWrap.Size = new System.Drawing.Size(102, 23);
			this.buttonWordWrap.TabIndex = 4;
			this.buttonWordWrap.Text = "Toggle word wrap";
			this.buttonWordWrap.UseVisualStyleBackColor = true;
			this.buttonWordWrap.Click += new System.EventHandler(this.ButtonWordWrapClick);
			// 
			// LongTextDialog
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(868, 595);
			this.Controls.Add(this.buttonWordWrap);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.buttonClose);
			this.Name = "LongTextDialog";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "LongTextDialog";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label labelContent;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.RichTextBox richTextBoxDescription;
		private System.Windows.Forms.RichTextBox richTextBoxContent;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonWordWrap;
	}
}