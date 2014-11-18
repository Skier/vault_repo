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
using System.Windows.Forms;

namespace Intuit.Common.WinUtil
{
	internal class ProgressIndicatorForm : Form
	{
		internal static string DefaultTitle = string.Empty;
		internal static string DefaultGroup = string.Empty;
		internal static string DefaultDetail = string.Empty;

		private Label m_LabelDetail;
		private Label m_LabelGroup;
		private ProgressBar m_ProgressBar;

		private string m_ProgressDetail = string.Empty;
		private string m_ProgressGroup = string.Empty;
// ReSharper disable InconsistentNaming
		private IContainer components;
// ReSharper restore InconsistentNaming
		private Timer m_ProgressTimer;

		internal ProgressIndicatorForm()
		{
			InitializeComponent();
		}

		internal void UpdateDialogText(string group, string detail)
		{
			bool change = false;
			//only updates the group with text if anything useful was passed in
			if (!string.IsNullOrEmpty(group) && group != m_ProgressGroup)
			{
				m_ProgressGroup = group;
				change = true;
			}
			if (detail != m_ProgressDetail)
			{
				m_ProgressDetail = detail;
				change = true;
			}
			if (change)
			{
				UpdateFormText();
				Refresh();
			}
		}

		private void progressTimer_Tick(object sender, EventArgs e)
		{
			if (m_ProgressBar != null && !m_ProgressBar.IsDisposed)
			{
				if (m_ProgressBar.Value == m_ProgressBar.Maximum)
				{
					m_ProgressBar.Value = 0;
					UpdateFormText();
				}
				else
				{
					m_ProgressBar.PerformStep();
				}
			}
			if (!ProgressWorker.KeepShowingProgress)
			{
				m_ProgressTimer.Stop();
				Close();
			}
		}

		private void UpdateFormText()
		{
			m_LabelDetail.Text = string.IsNullOrEmpty(m_ProgressDetail) ? DefaultDetail : m_ProgressDetail;
			m_LabelGroup.Text = string.IsNullOrEmpty(m_ProgressGroup) ? DefaultGroup : m_ProgressGroup;
			Text = DefaultTitle;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			m_ProgressTimer.Stop();
			base.OnClosing(e);
		}

		private void WizardProgress_VisibleChanged(object sender, EventArgs e)
		{
			m_ProgressTimer.Start();
		}

		#region IDisposable

		private bool m_Disposed;

		protected override void Dispose(bool disposing)
		{
			if (!m_Disposed)
			{
				try
				{
					if (disposing)
					{
						if (components != null)
						{
							components.Dispose();
						}
					}
					// Release the native unmanaged resources you added
					// in this derived class here.
					m_Disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.m_ProgressBar = new System.Windows.Forms.ProgressBar();
			this.m_LabelDetail = new System.Windows.Forms.Label();
			this.m_LabelGroup = new System.Windows.Forms.Label();
			this.m_ProgressTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// m_ProgressBar
			// 
			this.m_ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
			                                                                  | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ProgressBar.Location = new System.Drawing.Point(19, 80);
			this.m_ProgressBar.Name = "m_ProgressBar";
			this.m_ProgressBar.Size = new System.Drawing.Size(217, 20);
			this.m_ProgressBar.Step = 5;
			this.m_ProgressBar.TabIndex = 2;
			// 
			// m_LabelDetail
			// 
			this.m_LabelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                  | System.Windows.Forms.AnchorStyles.Right)));
			this.m_LabelDetail.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.m_LabelDetail.Location = new System.Drawing.Point(19, 44);
			this.m_LabelDetail.Name = "m_LabelDetail";
			this.m_LabelDetail.Size = new System.Drawing.Size(217, 32);
			this.m_LabelDetail.TabIndex = 1;
			// 
			// m_LabelGroup
			// 
			this.m_LabelGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                 | System.Windows.Forms.AnchorStyles.Right)));
			this.m_LabelGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.m_LabelGroup.Location = new System.Drawing.Point(19, 8);
			this.m_LabelGroup.Name = "m_LabelGroup";
			this.m_LabelGroup.Size = new System.Drawing.Size(217, 32);
			this.m_LabelGroup.TabIndex = 0;
			// 
			// m_ProgressTimer
			// 
			this.m_ProgressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
			// 
			// ProgressIndicatorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(254, 106);
			this.ControlBox = false;
			this.Controls.Add(this.m_LabelGroup);
			this.Controls.Add(this.m_LabelDetail);
			this.Controls.Add(this.m_ProgressBar);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MinimizeBox = false;
			this.Name = "ProgressIndicatorForm";
			this.ShowInTaskbar = false;
			this.VisibleChanged += new System.EventHandler(this.WizardProgress_VisibleChanged);
			this.ResumeLayout(false);
		}

		#endregion
	}
}