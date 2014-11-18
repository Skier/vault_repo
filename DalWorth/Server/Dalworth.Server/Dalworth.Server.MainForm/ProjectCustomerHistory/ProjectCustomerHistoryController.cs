using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CreateProjectBillPay;
using Dalworth.Server.MainForm.CreateProjectScope;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.ProjectCustomerHistory
{
    public class ProjectCustomerHistoryController : Controller<ProjectCustomerHistoryModel, ProjectCustomerHistoryView>
    {
        #region SelectedProject

        private ProjectWrapper m_selectedProject;
        public ProjectWrapper SelectedProject
        {
            get { return m_selectedProject; }
        }
            
        #endregion

        #region FocusedProject

        public ProjectWrapper FocusedProject
        {
            get
            {
                if (View.m_gridViewProjects.FocusedRowHandle >= 0)
                    return (ProjectWrapper)View.m_gridViewProjects.GetRow(View.m_gridViewProjects.FocusedRowHandle);
                else 
                    return null;
            } 
        }

        #endregion

        #region IsCancelled

        private Boolean m_isCancelled;
        public Boolean IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length >= 1 && data[0] != null)
                Model.Customer = (CustomerAndAddress)data[0];

            if (data != null && data.Length >= 2 && data[1] != null)
                Model.Projects = (BindingList<ProjectWrapper>)data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnNew.Click += OnNewClick;
            View.m_btnEdit.Click += OnEditClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_gridViewProjects.DoubleClick += OnGridDoubleClick;
            View.m_gridViewProjects.KeyDown += OnGridKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridProjects.DataSource = Model.Projects;
            View.m_ctlCustomer.Customer = Model.Customer.Customer;
            
            View.m_gridProjects.Select();
        }

        #endregion

        #region OnNewClick

        private void OnNewClick(object sender, EventArgs e)
        {
            m_selectedProject = null;
            View.Close();
        }

        #endregion

        #region OnEditClick

        private void OnEditClick(object sender, EventArgs e)
        {
            m_selectedProject = FocusedProject;
            View.Close();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Close();
        }

        #endregion

        #region OnGridDoubleClick

        private void OnGridDoubleClick(object sender, EventArgs e)
        {
            m_selectedProject = FocusedProject;
            View.Close();
        }

        #endregion

        #region OnGridKeyDown

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridProjects.Focused)
            {
                m_selectedProject = FocusedProject;
                View.Close();
            }
        }

        #endregion

    }
}
