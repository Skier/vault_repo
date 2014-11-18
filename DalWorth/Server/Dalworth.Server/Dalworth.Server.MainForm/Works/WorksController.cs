using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.TaskEdit;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.Works
{
    public class WorksController : NestedController<WorksModel, WorksView>
    {
        private bool m_disableAutoFilter = false;
        private MainFormController m_mainFormController;

        #region SelectedWork

        private WorkWrapper SelectedWork
        {
            get
            {
                int[] selectedRows = View.m_gridViewWorks.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                    return null;
                return (WorkWrapper)View.m_gridViewWorks.GetRow(selectedRows[0]);
            }
        }

        #endregion

        #region SelectedVisit

        private VisitWrapper SelectedVisit
        {
            get
            {
                int[] selectedRows = View.m_gridViewVisits.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                    return null;
                return (VisitWrapper)View.m_gridViewVisits.GetRow(selectedRows[0]);
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            m_mainFormController = (MainFormController)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_gridViewWorks.FocusedRowChanged += OnWorksFocusedRowChanged;
            View.m_gridViewWorks.ColumnFilterChanged += OnWorksColumnFilterChanged;
            View.m_linkVisitDashboard.Click += OnVisitDashboardClick;
            View.m_linkVisitVisit.Click += OnVisitVisitClick;
            
            View.m_txtWorkId.TextChanged += OnFiltersChanged;
            View.m_txtVan.TextChanged += OnFiltersChanged;
            View.m_cmbTechnician.SelectedIndexChanged += OnFiltersChanged;
            View.m_txtDispatch.TextChanged += OnFiltersChanged;
            View.m_cmbStatus.SelectedIndexChanged += OnFiltersChanged;
            View.m_dateRange.DateRangeValueChanged += OnFiltersChanged;

            View.m_btnClear.Click += OnClearClick;
            View.m_btnRefresh.Click += OnRefreshClick;

            View.m_gridViewVisits.KeyDown += OnGridVisitsKeyDown;

            View.m_gridWorks.DataSource = Model.Works;
            View.m_cmbTechnician.Focus();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            foreach (Employee technician in Model.Technicians)
            {
                View.m_cmbTechnician.Properties.Items.Add(
                    new ImageComboBoxItem(technician.DisplayName, (object) technician.ID));
            }
        }

        #endregion        

        #region OnGridVisitsKeyDown

        private void OnGridVisitsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridVisits.Focused)
            {
                VisitWrapper visit = SelectedVisit;
                if (visit != null)
                {
                    if (View.m_gridViewVisits.FocusedColumn.Name == View.m_colLinkDashboard.Name)
                        OnVisitDashboardClick(null, null);
                    else if (View.m_gridViewVisits.FocusedColumn.Name == View.m_colLinkVisit.Name)
                        OnVisitVisitClick(null, null);
                }
            }            
        }

        #endregion


        #region SelectWork

        public void SelectWork(Work work, bool singleWork)
        {
            if (work == null)
            {
                WorkWrapper prevFocusedWork = SelectedWork;
                RefreshData(null);
                if (prevFocusedWork != null)
                    TryToSelectWork(prevFocusedWork.Work);
                return;
            }

            if (singleWork)
                RefreshData(work.ID);
            else
                RefreshData(null);

            TryToSelectWork(work);

        }

        private void TryToSelectWork(Work work)
        {
            for (int rowIndex = 0; rowIndex < Model.Works.Count; rowIndex++)
            {
                if (Model.Works[rowIndex].Work.ID == work.ID)
                {
                    int rowHandle = View.m_gridViewWorks.GetRowHandle(rowIndex);
                    if (rowHandle >= 0)
                    {
                        View.m_gridViewWorks.ClearSelection();
                        View.m_gridViewWorks.FocusedRowHandle = rowHandle;
                        View.m_gridViewWorks.SelectRow(rowHandle);
                        return;
                    }
                }
            }

            if (Model.Works.Count > 0)
                TryToSelectWork(Model.Works[0].Work);
        }


        #endregion

        #region Link handlers

        private void OnVisitVisitClick(object sender, EventArgs e)
        {
            m_mainFormController.ShowVisitsForm(SelectedVisit.Visit);            
        }

        private void OnVisitDashboardClick(object sender, EventArgs e)
        {
            m_mainFormController.ShowDashboard(SelectedVisit.Visit);
        }

        #endregion
        
        #region OnVisitsFocusedRowChanged

        private void OnWorksColumnFilterChanged(object sender, EventArgs e)
        {
            OnWorksFocusedRowChanged(null, null);
        }

        private void OnWorksFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {            
            WorkWrapper work = SelectedWork;
            if (work == null)
            {
                View.m_gridVisits.DataSource = null;
            } else
            {
                work.Visits = Model.FindVisits(work.Work.ID);
                View.m_gridVisits.DataSource = work.Visits;
            }
        }

        #endregion        

        #region OnFiltersChanged

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            if (m_disableAutoFilter)
                return;

            WorkWrapper selectedWork = SelectedWork;
            RefreshData(null);
            if (selectedWork != null)
                TryToSelectWork(selectedWork.Work);
        }

        #endregion

        #region RefreshData

        public void RefreshData(int? exactWorkId)
        {            
            Model.UpdateWorks(exactWorkId,
                View.m_txtWorkId.Text,
                View.m_cmbTechnician.SelectedIndex == 0 ? (int?)null : (int)View.m_cmbTechnician.EditValue,
                View.m_txtDispatch.Text, 
                View.m_txtVan.Text,
                View.m_cmbStatus.SelectedIndex == 0 ? null : (WorkStatusEnum?) (int)View.m_cmbStatus.EditValue,
                View.m_dateRange.EditValue);

            View.m_gridWorks.DataSource = Model.Works;
            OnWorksFocusedRowChanged(null, null);
        }

        #endregion        

        #region OnRefreshClick

        private void OnRefreshClick(object sender, EventArgs e)
        {
            WorkWrapper selectedWork = SelectedWork;
            RefreshData(null);
            if (selectedWork != null)
                TryToSelectWork(selectedWork.Work);
        }

        #endregion

        #region OnClearClick

        private void ClearFilters()
        {
            m_disableAutoFilter = true;
            View.m_txtWorkId.Text = string.Empty;
            View.m_txtVan.Text = string.Empty;
            View.m_cmbTechnician.SelectedIndex = 0;
            View.m_txtDispatch.Text = string.Empty;
            View.m_cmbStatus.SelectedIndex = 0;
            View.m_dateRange.Clear();
            m_disableAutoFilter = false;            
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            ClearFilters();
            RefreshData(null);
        }

        #endregion        
    }
}
