using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddEquipment;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.ChangeEquipmentStatus;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.EquipmentHistory;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.TaskEdit;
using Dalworth.Server.MainForm.TransferEquipment;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraTab;
using GridView=DevExpress.XtraGrid.Views.Grid.GridView;

namespace Dalworth.Server.MainForm.Equipment
{
    public class EquipmentController : NestedController<EquipmentModel, EquipmentView>
    {
        private MainFormController m_mainFormController;

        #region SelectedEquipments

        private List<EquipmentWrapper> SelectedEquipments
        {
            get
            {
                GridView currentGridView = null;
                List<EquipmentWrapper> result = new List<EquipmentWrapper>();

                if (View.m_tabs.SelectedTabPageIndex == 0)
                {
                    currentGridView = View.m_gridEquipmentView;
                }                    
                else
                {
                    GridView view = (GridView) View.m_gridEquipmentIssues.FocusedView;
                                        
                    if (view != null && view.Name == View.m_gridViewEquipmentIssueItems.Name)
                        currentGridView = view;                        
                }

                if (currentGridView == null)
                    return result;

                int[] selectedRows = currentGridView.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                    return result;

                foreach (int row in selectedRows)
                    if (row >= 0)
                    {
                        result.Add(
                            (EquipmentWrapper)currentGridView.GetRow(row));
                    }

                               
                return result;
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
            View.m_btnRefresh.Click += OnRefreshClick;
            View.m_btnAddEquipment.Click += OnAddEquipmentClick;
            View.m_btnChangeStatus.Click += OnChangeStatusClick;
            View.m_btnTransfer.Click += OnTransferClick;
            View.m_btnHistory.Click += OnHistoryClick;

            View.m_tabs.SelectedPageChanged += OnTabPageChanged;
            View.m_cmbIssuesDepth.SelectedIndexChanged += OnIssuesDepthChanged;
            View.m_cmbIssueStatuses.SelectedIndexChanged += OnIssueStatusesChanged;
            View.m_btnResolve.Click += OnResolveClick;

            View.m_gridViewEquipmentIssueTransactions.MasterRowGetRelationCount += OnMasterRowGetRelationCount;
            View.m_gridViewEquipmentIssueTransactions.MasterRowEmpty += OnMasterRowEmpty;
            View.m_gridViewEquipmentIssueTransactions.MasterRowGetChildList += OnMasterRowGetChildList;
            View.m_gridViewEquipmentIssueTransactions.MasterRowGetRelationName += OnMasterRowGetRelationName;
            View.m_gridViewEquipmentIssueTransactions.MasterRowGetLevelDefaultView += OnMasterRowGetLevelDefaultView;        
            View.m_gridViewEquipmentIssueTransactions.RowCellStyle += OnEquipmentIssueRowPaint;
            View.m_linkEquipmentItem.Click += OnLinkEquipmentItemClick;           
            View.m_linkEquipmentTransaction.Click += OnEquipmentTransactionClick;

            InitGridCombos();
            View.m_gridEquipment.DataSource = Model.Equipments;            
        }

        #endregion

        #region InitGridCombos

        private void InitGridCombos()
        {
            foreach (EquipmentType type in Model.EquipmentTypes)
            {
                View.m_cmbType.Items.Add(new ImageComboBoxItem(type.Type, (object)type.ID));
            }

            foreach (Area area in Model.Areas)
            {
                View.m_cmbArea.Items.Add(new ImageComboBoxItem(area.Name, (object)area.ID));
            }
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {            
        }

        #endregion

        #region SelectEquipment

        private void SelectEquipment(EquipmentWrapper wrapper)
        {
            List<EquipmentWrapper> wrappers = new List<EquipmentWrapper>();
            wrappers.Add(wrapper);
            SelectEquipment(wrappers);
        }

        private void SelectEquipment(List<EquipmentWrapper> wrappers)
        {
            List<int> rowHandles = new List<int>();
            foreach (EquipmentWrapper wrapper in wrappers)
            {
                int dataSourceIndex = Model.Equipments.IndexOf(wrapper);

                if (dataSourceIndex >= 0)
                {
                    int rowHandle = View.m_gridEquipmentView.GetRowHandle(dataSourceIndex);
                    if (rowHandle < 0)
                    {
                        View.m_gridEquipmentView.ClearColumnsFilter();
                        SelectEquipment(wrappers);
                        return;
                    }

                    rowHandles.Add(rowHandle);
                }                
            }            

            View.m_gridEquipmentView.ClearSelection();

            foreach (int handle in rowHandles)
                View.m_gridEquipmentView.SelectRow(handle);                       
        }

        #endregion
              
        #region RefreshData

        public void RefreshData()
        {
            using (new WaitCursor())
            {
                if (View.m_tabs.SelectedTabPageIndex == 1)
                {
                    List<int> expandedRows = GetExpandendMasterRowHandles();

                    DateTime? date = null;
                    if ((int)View.m_cmbIssuesDepth.EditValue != 0)
                        date = DateTime.Now.AddDays(-1 * (int)View.m_cmbIssuesDepth.EditValue);


                    View.m_gridEquipmentIssues.BeginUpdate();
                    Model.InitEquipmentIssues(date,
                        (EquipmentIssueStatusEnum)((int)View.m_cmbIssueStatuses.EditValue + 1));
                    View.m_gridEquipmentIssues.DataSource = Model.EquipmentIssueTransactions;
                    View.m_gridEquipmentIssues.EndUpdate();

                    ExpandCollapseRows(expandedRows);
                }
                else
                {
                    Model.Init();
                    View.m_gridEquipment.DataSource = Model.Equipments;
                }                
            }
        }

        #endregion
    
        #region OnRefreshClick

        private void OnRefreshClick(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region OnAddEquipmentClick

        private void OnAddEquipmentClick(object sender, EventArgs e)
        {
            using (AddEquipmentController controller = Prepare<AddEquipmentController>())
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    try
                    {
                        Database.Begin();
                        Model.AddEquipment(controller.AddedEquipment);
                        Database.Commit();
                    }
                    catch (Exception)
                    {
                        Database.Rollback();
                        throw;
                    }

                    RefreshData();
                    SelectEquipment(new EquipmentWrapper(controller.AddedEquipment));
                }
                    
            }
        }

        #endregion

        #region OnChangeStatusClick

        private void OnChangeStatusClick(object sender, EventArgs e)
        {
            List<EquipmentWrapper> selectedEquipment = SelectedEquipments;
            if (selectedEquipment.Count == 0)
            {
                XtraMessageBox.Show("Please select equipment to modify", "No equipment selected",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {

                using (ChangeEquipmentStatusController controller 
                    = Prepare<ChangeEquipmentStatusController>(selectedEquipment))
                {
                    controller.Execute(false);
                    if (!controller.IsCancelled)
                    {
                        try
                        {
                            Database.Begin();
                            Model.UpdateEquipment(controller.ModifiedEquipment,
                                controller.Notes);
                            Database.Commit();
                        }
                        catch (Exception)
                        {
                            Database.Rollback();
                            throw;
                        }

                        RefreshData();
                        SelectEquipment(selectedEquipment);
                    }
                }

            }
        }

        #endregion

        #region OnTransferClick

        private void OnTransferClick(object sender, EventArgs e)
        {
            List<EquipmentWrapper> selectedEquipment = SelectedEquipments;
            if (View.m_tabs.SelectedTabPageIndex == 1 && selectedEquipment.Count == 0)
            {
                XtraMessageBox.Show("Please select equipment to transfer", "No equipment selected",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                using (TransferEquipmentController controller
                    = Prepare<TransferEquipmentController>(selectedEquipment))
                {
                    controller.Execute(false);
                    if (!controller.IsCancelled && controller.ModifiedEquipment.Count > 0)
                    {
                        try
                        {
                            Database.Begin();
                            Model.UpdateEquipment(controller.ModifiedEquipment,
                                controller.Notes);
                            Database.Commit();
                        }
                        catch (Exception)
                        {
                            Database.Rollback();
                            throw;
                        }

                        RefreshData();
                        SelectEquipment(selectedEquipment);
                    }
                }
            }            
        }

        #endregion

        #region OnHistoryClick

        private void OnHistoryClick(object sender, EventArgs e)
        {
            List<EquipmentWrapper> selectedEquipment = SelectedEquipments;
            if (selectedEquipment.Count == 0)
            {
                XtraMessageBox.Show("Please select equipment to view history for", "No equipment selected",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                EquipmentHistoryController controller
                    = Prepare<EquipmentHistoryController>(selectedEquipment);
                controller.Execute(true);
            }            
        }

        private void OnLinkEquipmentItemClick(object sender, EventArgs e)
        {
            OnHistoryClick(null, null);
        }

        #endregion

        #region OnEquipmentTransactionClick

        private void OnEquipmentTransactionClick(object sender, EventArgs e)
        {
            EquipmentIssueWrapper issueTransaction
                = (EquipmentIssueWrapper)View.m_gridViewEquipmentIssueTransactions.GetRow(
                    View.m_gridViewEquipmentIssueTransactions.FocusedRowHandle);

            MainFormController mainFormController = (MainFormController)Configuration.MainFormController;

            if (issueTransaction.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone
                || issueTransaction.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.Completed)
            {
                Work work = Work.FindByPrimaryKey(issueTransaction.WorkTransaction.WorkId);
                mainFormController.ShowWorksForm(work);

            }
            else if (issueTransaction.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
            {
                Visit visit = Visit.FindByPrimaryKey(issueTransaction.WorkTransaction.VisitId.Value);
                mainFormController.ShowVisitsForm(visit);
            }
        }

        #endregion


        #region OnTabPageChanged

        private void OnTabPageChanged(object sender, TabPageChangedEventArgs e)
        {
            View.m_btnAddEquipment.Enabled = View.m_tabs.SelectedTabPageIndex == 0;
            RefreshData();
        }

        #endregion

        #region OnIssuesDepthChanged

        private void OnIssuesDepthChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region OnIssueStatusesChanged

        private void OnIssueStatusesChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region Issues master detail

        private void OnMasterRowGetLevelDefaultView(object sender, MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = View.m_gridViewEquipmentIssueItems;
        }

        private void OnMasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Level1";
        }

        private void OnMasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            EquipmentIssueWrapper issueWrapper
                = (EquipmentIssueWrapper)View.m_gridViewEquipmentIssueTransactions.GetRow(e.RowHandle);
            e.ChildList = Model.EquipmentIssueItems[issueWrapper.EquipmentTransaction.ID];
        }

        private void OnMasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {
            e.IsEmpty = false;
        }

        private void OnMasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        #endregion

        #region OnResolveClick

        private void OnResolveClick(object sender, EventArgs e)
        {
            List<EquipmentWrapper> selectedEquipment = SelectedEquipments;
            if (selectedEquipment.Count == 0)
            {
                XtraMessageBox.Show("Please select equipment to Resolve", "No equipment selected",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool isAtLeastOneUnresolvedExist = false;

            List<EquipmentIssueWrapper> issueWrappers = new List<EquipmentIssueWrapper>();
            foreach (EquipmentWrapper wrapper in selectedEquipment)
            {
                EquipmentIssueWrapper issueWrapper = (EquipmentIssueWrapper) wrapper;
                if (!issueWrapper.IsResolved)
                    isAtLeastOneUnresolvedExist = true;
                issueWrappers.Add(issueWrapper);                
            }

            if (!isAtLeastOneUnresolvedExist)
            {
                XtraMessageBox.Show("Please select unresolved equipment", "No unresolved equipment selected",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                return;
            }

            if (XtraMessageBox.Show("Do you want to resolve selected equipment issues?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            try
            {
                Database.Begin();
                Domain.Equipment.ResolveEquipmentIssues(issueWrappers, Configuration.CurrentDispatchId);
                Database.Commit();

                RefreshData();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region Row expand helpers

        private List<int> GetExpandendMasterRowHandles()
        {
            List<int> result = new List<int>();
            if (Model.EquipmentIssueTransactions == null)
                return result;

            for (int i = 0; i < Model.EquipmentIssueTransactions.Count; i++)
            {
                int rowHandle = View.m_gridViewEquipmentIssueTransactions.GetRowHandle(i);

                if (View.m_gridViewEquipmentIssueTransactions.GetMasterRowExpanded(rowHandle))
                    result.Add(rowHandle);
            }

            return result;
        }

        private void ExpandCollapseRows(List<int> rowsToBeExpanded)
        {
            if (Model.EquipmentIssueTransactions == null)
                return;

            for (int i = 0; i < Model.EquipmentIssueTransactions.Count; i++)
            {
                int rowHandle = View.m_gridViewEquipmentIssueTransactions.GetRowHandle(i);
                if (rowsToBeExpanded.Contains(rowHandle))
                    View.m_gridViewEquipmentIssueTransactions.ExpandMasterRow(rowHandle);
                else
                    View.m_gridViewEquipmentIssueTransactions.CollapseMasterRow(rowHandle);
            }            
        }

        #endregion

        #region OnEquipmentIssueRowPaint

        private void OnEquipmentIssueRowPaint(object sender, RowCellStyleEventArgs e)
        {
            EquipmentIssueWrapper issue
                = (EquipmentIssueWrapper)View.m_gridViewEquipmentIssueTransactions.GetRow(e.RowHandle);

            if (issue.HasUnresolvedIssues)
                e.Appearance.BackColor = Color.FromArgb(255, 102, 102);
            else
                e.Appearance.BackColor = Color.LightGreen;
        }

        #endregion

    }
}
