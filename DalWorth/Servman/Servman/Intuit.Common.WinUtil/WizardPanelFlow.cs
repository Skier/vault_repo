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

using System.Collections.Generic;
using System.Windows.Forms;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// A simple implementation of a multi-panel guided navigation flow, aka a wizard.
	/// Assumes all the panels are placed overlapping each other in a common container.
	/// Each panel also has a Label that might be shown elsewhere (like in a tab interface or other navigation widget).
	/// This flow will Hide() (and disable the Label) and Show() (and enable the Label) them as the user traverses the flow.
	/// </summary>
	public class WizardPanelFlow
	{
		private readonly List<WizardPanel> m_Panels = new List<WizardPanel>();
		private int m_CurrentPanelIndex = -1;

		private int CurrentPanelIndex
		{
			get
			{
				return m_CurrentPanelIndex;
			}
			set
			{
				if (m_CurrentPanelIndex != value)
				{
					UpdatePanelDisplay(m_CurrentPanelIndex, value);
					m_CurrentPanelIndex = value;
					if (CurrentPanelChanged != null)
					{
						CurrentPanelChanged(m_Panels[m_CurrentPanelIndex]);
					}
				}
			}
		}

		/// <summary>
		/// Implemented by the handler for the <see cref="CurrentPanelChanged"/> event.
		/// </summary>
		/// <param name="newCurrentPanel"></param>
		public delegate void CurrentPanelChangedHandler(WizardPanel newCurrentPanel);

		/// <summary>
		/// Fired everytime a panel change occurs.
		/// </summary>
		public event CurrentPanelChangedHandler CurrentPanelChanged;

		/// <summary>
		/// Whether or not we're on the last panel.
		/// </summary>
		/// <returns></returns>
		public bool IsOnLastPanel()
		{
			return CurrentPanelIndex == m_Panels.Count - 1;
		}

		/// <summary>
		/// Whether or not we're on the first panel.
		/// </summary>
		/// <returns></returns>
		public bool IsOnFirstPanel()
		{
			return CurrentPanelIndex == 0;
		}

		/// <summary>
		/// Set the current panel to the first in the flow.
		/// </summary>
		public void ShowFirstPanel()
		{
			CurrentPanelIndex = 0;
		}

		private void UpdatePanelDisplay(int prevPanelIndex, int newPanelIndex)
		{
			if (prevPanelIndex != -1)
			{
				m_Panels[prevPanelIndex].PanelObject.Hide();
				m_Panels[prevPanelIndex].LabelObject.Enabled = false;
			}

			m_Panels[newPanelIndex].PanelObject.Show();
			m_Panels[newPanelIndex].LabelObject.Enabled = true;
		}

		/// <summary>
		/// Navigates to the previous panel in the flow.
		/// </summary>
		public void PreviousPanel()
		{
			CurrentPanelIndex--;
		}

		/// <summary>
		/// Navigates to the next panel in the flow.
		/// </summary>
		public void NextPanel()
		{
			CurrentPanelIndex++;
		}

		/// <summary>
		/// Adds a new WizardPanel with the given panel and label objects to the end of the flow.
		/// </summary>
		/// <param name="panel"></param>
		/// <param name="label"></param>
		public void AddPanel(Panel panel, Label label)
		{
			WizardPanel wizardPanel = new WizardPanel(panel, label);
			AddPanel(wizardPanel);
		}

		/// <summary>
		/// Adds a new WizardPanel to the end of the flow.
		/// </summary>
		/// <param name="wizardPanel"></param>
		public void AddPanel(WizardPanel wizardPanel)
		{
			wizardPanel.PanelObject.Hide();
			wizardPanel.LabelObject.Enabled = false;
			m_Panels.Add(wizardPanel);
		}
	}
}