using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.VisitSplit
{
    public class VisitSplitController : Controller<VisitSplitModel, VisitSplitView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {            
            Model.Visit1 = (Visit) data[0];
            if (data.Length > 1)
                Model.Visit2 = (Visit)data[1];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;

            View.m_btnMoveTasksToVisit2.Click += OnMoveTasksClick;
            View.m_btnMoveTasksToVisit1.Click += OnMoveTasksClick;

            View.m_gridTasksVisit1View.FocusedRowChanged += OnTasksFocusedRowChanged;
            View.m_gridTasksVisit2View.FocusedRowChanged += OnTasksFocusedRowChanged;                        

            View.m_gridTasksVisit1View.GotFocus += OnTasksVisit1GotFocus;
            View.m_gridTasksVisit2View.GotFocus += OnTasksVisit2GotFocus;

            View.m_gridTasksVisit1View.KeyDown += OnTasksVisit1KeyDown;
            View.m_gridTasksVisit2View.KeyDown += OnTasksVisit2KeyDown;
            View.KeyDown += OnKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_ctlHeaderVisit1.Visit = Model.Visit1;
            View.m_ctlHeaderVisit2.Visit = Model.Visit2;

            View.m_gridTasksVisit1.DataSource = Model.Visit1Tasks;
            View.m_gridTasksVisit2.DataSource = Model.Visit2Tasks;

            if (Model.Mode == DialogMode.Edit)
            {
                View.Text = "Visits Edit";
                View.m_pnlVisit1.Text = "Visit 1";
                View.m_pnlVisit2.Text = "Visit 2";
            }

            EnableDisableMovementButtons();
        }

        #endregion

        #region OnTasksKeyDown

        private void OnTasksVisit1KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && e.KeyCode == Keys.Enter && View.m_btnMoveTasksToVisit2.Enabled)
                OnMoveTasksClick(View.m_btnMoveTasksToVisit2, null);
        }

        private void OnTasksVisit2KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && e.KeyCode == Keys.Enter && View.m_btnMoveTasksToVisit1.Enabled)
                OnMoveTasksClick(View.m_btnMoveTasksToVisit1, null);
        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                OnOkClick(null, null);
                e.Handled = true;
            }
                
        }

        #endregion


        #region IsFormValid

        private bool IsFormValid()
        {
            View.ValidateChildren();

            if (View.m_ctlHeaderVisit1.HasErrors
                || View.m_ctlHeaderVisit2.HasErrors)
            {
                return false;
            }

            if (Model.Mode == DialogMode.Edit)
            {
                if (Model.Visit1Tasks.Count == 0 || Model.Visit2Tasks.Count == 0)
                {
                    if (XtraMessageBox.Show(
                        string.Format("Visit {0} has no tasks. Visit {1} will not be generated. Do you want to continue?", 
                            Model.Visit1Tasks.Count == 0 ? "1" : "2",
                            Model.Visit1Tasks.Count != 0 ? "1" : "2"),
                            "Confirmation",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return false;
                    }
                }
            } else
            {
                if (Model.Visit2Tasks.Count == 0)
                {
                    XtraMessageBox.Show("Please specify tasks to be splitted to another visit", "Unable to split visit",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (Model.Visit1Tasks.Count == 0)
                {
                    XtraMessageBox.Show("Orginal visit should contain at least one task", "Unable to split visit",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }                
            }

            return true;
        }

        #endregion

        #region OnMoveTasksClick

        private void OnMoveTasksClick(object sender, EventArgs e)
        {
            bool isMovementToVisit2 = ((SimpleButton)sender).Name == View.m_btnMoveTasksToVisit2.Name;
            GridView sourceGrid = isMovementToVisit2
                ? View.m_gridTasksVisit1View : View.m_gridTasksVisit2View;
            GridView destinationGrid = !isMovementToVisit2
                ? View.m_gridTasksVisit1View : View.m_gridTasksVisit2View;

            List<int> indexesToMove = new List<int>();

            if (sourceGrid.SelectedRowsCount > 0)
            {
                foreach (int rowHandle in sourceGrid.GetSelectedRows())
                    indexesToMove.Add(sourceGrid.GetDataSourceRowIndex(rowHandle));
            }
            else if (sourceGrid.FocusedRowHandle >= 0)
                indexesToMove.Add(sourceGrid.GetDataSourceRowIndex(sourceGrid.FocusedRowHandle));

            if (indexesToMove.Count > 0)
                Model.MoveTasks(indexesToMove, isMovementToVisit2);

            OnTasksFocusedRowChanged(sourceGrid, 
                new FocusedRowChangedEventArgs(0, sourceGrid.FocusedRowHandle));

            EnableDisableMovementButtons();
            sourceGrid.SelectRow(sourceGrid.FocusedRowHandle);
            destinationGrid.SelectRow(destinationGrid.FocusedRowHandle);
        }

        #endregion

        #region EnableDisableMovementButtons

        private void EnableDisableMovementButtons()
        {
            View.m_btnMoveTasksToVisit1.Enabled = Model.Visit2Tasks.Count > 0;
            View.m_btnMoveTasksToVisit2.Enabled = Model.Visit1Tasks.Count > 0;            
        }

        #endregion

        #region OnTasksFocusedRowChanged

        private void OnTasksFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView affectedGrid = (GridView)sender;
            
            if (e.FocusedRowHandle >= 0)
            {
                TaskWrapper task = (TaskWrapper)affectedGrid.GetRow(e.FocusedRowHandle);

                View.m_txtTaskDetails.Text = "Project: " + task.Task.Project.ProjectTypeText + "\r\n";

                if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                {
                    View.m_txtTaskDetails.Text += "Deflood Notes: " + Model.FindDefloodTask(task).Notes + "\r\n";
                    View.m_txtTaskDetails.Text += "Monitoring Notes: " + task.Task.Notes;
                }
                else
                    View.m_txtTaskDetails.Text += "Notes: " + task.Task.Notes;                                           
            } else
            {
                View.m_txtTaskDetails.Text = string.Empty;
            }
        }

        #endregion

        #region GotFocus

        private void OnTasksVisit1GotFocus(object sender, EventArgs e)
        {
            OnTasksFocusedRowChanged(View.m_gridTasksVisit1View,
                new FocusedRowChangedEventArgs(0, View.m_gridTasksVisit1View.FocusedRowHandle));
        }

        private void OnTasksVisit2GotFocus(object sender, EventArgs e)
        {
            OnTasksFocusedRowChanged(View.m_gridTasksVisit2View,
                new FocusedRowChangedEventArgs(0, View.m_gridTasksVisit2View.FocusedRowHandle));
        }

        #endregion


               
        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {   
            if (!IsFormValid())
                return;

            Model.Visit1 = View.m_ctlHeaderVisit1.Visit;
            Model.Visit2 = View.m_ctlHeaderVisit2.Visit;

            List<GetVisitsRequiringServiceDatePromptResult> visitsNeedPrompt = Model.GetVisitsRequiringServiceDatePrompt();

            if (visitsNeedPrompt.Count > 0)
            {
                string msg = string.Empty;

                if (visitsNeedPrompt.Count == 1)
                {
                    string serviceType = visitsNeedPrompt[0].IsDeflood ? "Deflood" : "Monitoring";
                    msg = "Visit " + visitsNeedPrompt[0].VisitIdDescription + " contains "+serviceType+" and missing Service Date. Proceed?";
                }
                else
                {
                    if (visitsNeedPrompt[0].IsDeflood == visitsNeedPrompt[1].IsDeflood)
                    {
                        string serviceType = visitsNeedPrompt[0].IsDeflood? "Deflood":"Monitoring";
                        msg =  "Visits " + visitsNeedPrompt[0].VisitIdDescription + " and " + visitsNeedPrompt[1].VisitIdDescription + " contain " + serviceType + ". Missing Service Dates.  Proceed?";
                    }
                    else if (visitsNeedPrompt[0].IsDeflood == true)
                    {
                        msg = "Visit " + visitsNeedPrompt[0].VisitIdDescription + " contains Deflood and Visit " + visitsNeedPrompt[1].VisitIdDescription + " contains Monitoring. Missing Service Dates.  Proceed?";
                    }
                    else
                    {
                        msg = "Visit " + visitsNeedPrompt[0].VisitIdDescription + " contains Monitoring and Visit " + visitsNeedPrompt[1].VisitIdDescription + " contains Deflood. Missing Service Dates.  Proceed?";

                    }
                }
               
                if (XtraMessageBox.Show(msg, "Service Date Check",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    List<GetVisitsRequiringServiceDatePromptResult> visit1Results =
                        visitsNeedPrompt.FindAll(delegate(GetVisitsRequiringServiceDatePromptResult r) { return r.VisitId == Model.Visit1.ID; });

                    if (visit1Results.Count > 0)
                    {
                        View.m_ctlHeaderVisit1.m_dtpServiceDate.Focus();
                    }
                    else
                    {
                        View.m_ctlHeaderVisit2.m_dtpServiceDate.Focus();
                    }
                    return;
                }
             
            }

            SplitResult result;
            try
            {
                Database.Begin();
                result = Model.Save();
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            if (result != SplitResult.PrintNone)
            {
                try
                {
                    using (new WaitCursor())
                    {
                        if (result == SplitResult.PrintBoth || result == SplitResult.PrintVisit1)
                        {
                            VisitSummaryPackage summaryPackage = new VisitSummaryPackage(Model.Visit1);
                            summaryPackage.Print();                            
                        }

                        if (result == SplitResult.PrintBoth || result == SplitResult.PrintVisit2)
                        {
                            VisitSummaryPackage summaryPackage = new VisitSummaryPackage(Model.Visit2);
                            summaryPackage.Print();
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Unable to print visit",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

    }
}
