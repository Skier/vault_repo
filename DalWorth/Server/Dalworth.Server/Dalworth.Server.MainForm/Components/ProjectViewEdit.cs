using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Dalworth.Server.Windows;
using Dalworth.Server.Domain; 

namespace Dalworth.Server.MainForm.Components
{
    public partial class ProjectViewEdit : BaseControl
    {
        public ProjectViewEdit()
        {
            InitializeComponent();
        }

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
            set 
            { 
                m_project = value;
                UpdateLables();
            }
        }

        public void UpdateLables()
        {
            if (m_project != null)
            {
                m_lblDateCreated.Text = String.Format("{0:M/d/yyyy}", m_project.CreateDate); 
                m_lblProjectId.Text = m_project.ID.ToString();
                m_lblProjectType.Text = m_project.ProjectTypeText;
                m_lblStatus.Text = m_project.ProjectStatusText;
                m_lblClosedAmount.Text = String.Format("{0:0.##}", m_project.ClosedAmount);
            }
        }
    }
}
