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

using System.Windows.Forms;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// A panel in a <see cref="WizardPanelFlow"/>.
	/// </summary>
	public class WizardPanel
	{
		/// <summary>
		/// Creates a new WizardPanel
		/// </summary>
		/// <param name="p">Windows Forms panel with the content for this WizardPanel</param>
		/// <param name="l">Label associated with this wizard panel</param>
		public WizardPanel(Panel p, Label l)
		{
			PanelObject = p;
			LabelObject = l;
		}

		/// <summary>
		/// Gets or sets the Windows Forms panel with the content for this WizardPanel
		/// </summary>
		public Panel PanelObject { get; set; }

		/// <summary>
		/// Gets or sets the Label
		/// </summary>
		public Label LabelObject { get; set; }
	}
}