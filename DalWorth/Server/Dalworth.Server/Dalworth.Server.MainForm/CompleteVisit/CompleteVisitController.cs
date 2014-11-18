using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.Components;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.MainForm.MakePayment;
using Dalworth.Server.MainForm.VisitSplit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.CompleteVisit
{
    public class CompleteVisitController : Controller<CompleteVisitModel, CompleteVisitView>
    {
        private readonly Color m_colorFocusedRow = Color.FromArgb(49, 106, 197);
        private readonly Color m_colorFocusedRowHidden = Color.FromArgb(122, 150, 223);
        private readonly Color m_colorFocusedRowText = Color.White;
        private readonly Color m_ColorFocusedCellLinkText = Color.Blue;
        private readonly Color m_colorExcludedRow = Color.LightGray;
        private readonly Color m_colorExcludedFocusedRow = Color.Black;
        private readonly Color m_colorExcludedFocusedRowHidden = Color.Gray;

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
            Model.Work = (Work)data[0];
            Model.Visit = (Visit)data[1];
            base.OnModelInitialize(data);
        }

        #endregion        

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnCompleteClick;
            View.m_menuCompleteWithoutPayment.ItemClick += OnCompleteWithoutPaymentClick;
            View.m_menuCompleteWithPayment.ItemClick += OnCompleteWithPaymentClick;
            View.m_tabs.SelectedPageChanged += OnSelectedTabPageChanged;

            //Tasks
            View.m_treeTasks.FocusedNodeChanged += OnTasksFocusedNodeChanged;
            View.m_treeTasks.ShowingEditor += OnTasksShowingEditor;
            View.m_treeTasks.CellValueChanging += OnTasksCellValueChanged;
            View.m_treeTasks.NodeCellStyle += OnTasksCellStyle;
            View.m_ctlTaskEdit.FailTypeChanged += OnFailTypeChanged;
            View.m_ctlTaskEdit.ClosedAmountChanged += OnTaskClosedAmountChanged;
            
            View.m_treeTasks.CustomNodeCellEdit += OnTasksCustomNodeCellEdit;
            View.m_treeTasks.CustomDrawNodeCell += OnTasksCustomDrawNodeCell;

            View.m_btnAddTask.Click += OnAddTaskClick;

            View.m_menuAddRugPickup.ItemClick += OnAddRugPickupClick;
            View.m_menuAddDeflood.ItemClick += OnAddDefloodClick;
            View.m_menuAddMiscellaneous.ItemClick += OnAddMiscellaneousClick;

            View.m_treeTasks.DoubleClick += OnTaskActionClick;
            View.m_menuActionAddMiscellaneous.ItemClick += OnActionAddMiscellaneousClick;
            View.m_menuActionAddRugPickup.ItemClick += OnActionAddRugPickupClick;
            View.m_menuDeleteTask.ItemClick += OnDeleteTaskClick;   
         
            View.m_timeComplete.Validating += OnTimeCompleteValidating;
            View.m_timeComplete.ButtonClick += OnTimeCompleteButtonClick;

            View.m_ctlCustomerLookup.Modified += OnCustomerModified;

            View.m_treeTasks.FocusedColumnChanged += OnTasksFocusedColumnChanged;
            View.m_treeTasks.KeyDown += OnTasksKeyDown;
            View.KeyDown += OnKeyDown;

            View.m_txtDropOff.TextChanged += OnEquipmentChanged;
            View.m_txtPickup.TextChanged += OnEquipmentChanged;

            View.m_ctlProjectEdit.IsQbSalesRepRequired = true;
        }

        #endregion
       
        #region OnViewLoad

        protected override void OnViewLoad()
        {               
            View.m_ctlCustomerLookup.Customer = Model.Customer;            
            View.m_ctlCustomerLookup.Address = Model.ServiceAddress;
            View.m_ctlAddressLookup.CurrentAddress = Model.ServiceAddress;
            View.m_ctlAddressLookup.BaseAddress = Model.ServiceAddress;
            View.m_ctlAddressLookup.Caption = "Customer Address";
            View.m_ctlAddressLookup.Enabled = false;
            View.m_txtVisitNotes.Text = Model.Visit.Notes;

            View.m_treeTasks.DataSource = Model.Tasks;
            View.m_treeTasks.ExpandAll();
            SelectLastProject();

            if (Model.Visit.ServiceAddressId != null)
            {
                View.m_txtPickup.Quantities = Model.PickupEquipment;
                View.m_txtDropOff.Quantities = Model.DropOffEquipment;
                UpdateEquipmentTotals();
            }                
            else
                View.m_btnAddTask.Enabled = false;

            if (DateTime.Now.Date != Model.Work.StartDate.Value.Date)
                View.m_timeComplete.Properties.Buttons[2].Visible = false;
            View.m_timeComplete.Time = Model.DefaultCompletionTime;
            View.m_lblVisitNumber.Text = Model.Visit.ID.ToString();
            View.m_ctlCustomerLookup.Select();            
        }

        #endregion        

        #region OnSelectedTabPageChanged

        private void OnSelectedTabPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (View.m_tabs.SelectedTabPageIndex == 0)
                View.m_txtVisitNotes.Focus();
            else if (View.m_tabs.SelectedTabPageIndex == 1)
                View.m_treeTasks.Focus();
        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
                OnCompleteClick(null, null);
        }

        #endregion

        #region OnCustomerModified

        private void OnCustomerModified(Customer customer, Address address)
        {
            try
            {
                Database.Begin();
                customer.Modified = DateTime.Now;
                Customer.Update(customer);
                address.Modified = DateTime.Now;
                Address.Update(address);
                Database.Commit();
                View.m_ctlAddressLookup.Customer = customer;
                View.m_ctlAddressLookup.BaseAddress = address;
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
            
        }

        #endregion


        //Equipment

        #region OnEquipmentChanged

        private void OnEquipmentChanged(object sender, EventArgs eventArgs)
        {
            EquipmentQuantityTextEdit control = sender as EquipmentQuantityTextEdit;
            if (control == View.m_txtDropOff)
                Model.DropOffEquipment = control.Quantities;
            else
                Model.PickupEquipment = control.Quantities;

            Model.RecalculateEquipmentQuantities(control == View.m_txtDropOff);

            if (control == View.m_txtDropOff)
                View.m_txtPickup.Quantities = Model.PickupEquipment;
            else
                View.m_txtDropOff.Quantities = Model.DropOffEquipment;

            UpdateEquipmentTotals();
        }

        #endregion

        #region Update equipment totals

        private void UpdateEquipmentTotals(bool vanEquipment)
        {
            Label affectedTotalsLabel = vanEquipment
                ? View.m_lblEquipmentVanTotals : View.m_lblEquipmentCustomerTotals;
            Dictionary<int, int> totals = vanEquipment ? Model.VanEquipment : Model.CustomerEquipment;

            string resultText = string.Empty;
            foreach (int total in totals.Values)
                resultText += total + "/";
            resultText = resultText.Remove(resultText.Length - 1);

            affectedTotalsLabel.Text = resultText;
        }

        private void UpdateEquipmentTotals()
        {
            UpdateEquipmentTotals(true);
            UpdateEquipmentTotals(false);
        }

        #endregion

        //Tasks

        #region OnTasksCustomDrawNodeCell

        private void OnTasksCustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.Column.ColumnEdit is RepositoryItemHyperLinkEdit
                && e.Node.Focused && !e.Focused)
            {
                Rectangle bounds = new Rectangle(e.Bounds.X + 3, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                e.Appearance.DrawString(e.Cache, e.CellText, bounds);
                e.Handled = true;
            }
        }

        #endregion

        #region OnTasksCellStyle

        private void OnTasksCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null)
                return;

            bool isTreeFocused = View.m_treeTasks.Focused || View.m_treeTasks.ActiveEditor != null;


            if (isTreeFocused && e.Node.Focused && View.m_treeTasks.FocusedColumn != null 
                && e.Column.Name == View.m_treeTasks.FocusedColumn.Name)
            {
                if (e.Column.Name == View.m_colTask.Name)
                    e.Appearance.ForeColor = m_ColorFocusedCellLinkText;
                e.Appearance.BorderColor = Color.Black;                
                return;
            }

            TaskProjectWrapperComplete task
                = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(e.Node);
            

            if (task != null && !task.IsIncludedInVisit)
            {
                if (e.Node.Focused)
                {
                    if (isTreeFocused)
                        e.Appearance.BackColor = m_colorExcludedFocusedRow;
                    else
                        e.Appearance.BackColor = m_colorExcludedFocusedRowHidden;

                    e.Appearance.ForeColor = m_colorFocusedRowText;
                }
                else
                    e.Appearance.BackColor = m_colorExcludedRow;
            }
            else if (e.Node.Focused)
            {
                if (isTreeFocused)
                    e.Appearance.BackColor = m_colorFocusedRow;
                else
                    e.Appearance.BackColor = m_colorFocusedRowHidden;

                e.Appearance.ForeColor = m_colorFocusedRowText;
            }                       
        }

        #endregion

        #region OnTasksFocusedColumnChanged

        private void OnTasksFocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {            
            ActivateDeactivateActionEditor(e.Column);
        }

        private void ActivateDeactivateActionEditor(TreeListColumn column)
        {
            if (column != null && column.Name == View.m_colTaskAction.Name)
                View.m_treeTasks.ShowEditor();
            else
                View.m_treeTasks.HideEditor();
        }

        #endregion

        #region OnTasksKeyDown

        private void OnTasksKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && e.KeyCode == Keys.Enter && View.m_treeTasks.Focused)
            {
                TaskProjectWrapperComplete task
                    = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);

                if (task != null)
                {
                    OnTaskActionClick(null, null);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        #endregion

        #region OnAddTaskClick

        private void OnAddTaskClick(object sender, EventArgs e)
        {
            View.m_menuAddDeflood.Enabled = Model.IsAddDefloodAllowed();
            View.m_menuAddRugPickup.Enabled = true;

            View.m_menuAdd.ShowPopup(
                View.m_btnAddTask.PointToScreen(new Point(View.m_btnAddTask.Width, 0)));

            for (int i = 0; i < View.m_menuAdd.ItemLinks.Count; i++)
            {
                if (View.m_menuAdd.ItemLinks[i].Enabled)
                {
                    View.m_menuAdd.ItemLinks[i].Focus();
                    break;
                }
            }
        }

        private void OnAddRugPickupClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNewTask(TaskTypeEnum.RugPickup, null);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastProject();
        }

        private void OnAddDefloodClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNewTask(TaskTypeEnum.Deflood, null);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastProject();
        }

        private void OnAddMiscellaneousClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNewTask(TaskTypeEnum.Miscellaneous, null);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastProject();
        }

        #endregion

        #region SelectLastProject

        private void SelectLastProject()
        {
            string keyId = string.Empty;

            for (int i = Model.Tasks.Count - 1; i >= 0; i--)
            {
                if (Model.Tasks[i].IsProject)
                {
                    keyId = Model.Tasks[i].ID;
                    break;
                }
            }

            if (keyId != string.Empty)
            {
                View.m_treeTasks.FocusedNode = View.m_treeTasks.FindNodeByKeyID(keyId);
                View.m_ctlProjectEdit.Focus();
            }                
        }

        #endregion

        #region SelectLastTask

        private void SelectLastTask()
        {
            string keyId = string.Empty;

            for (int i = Model.Tasks.Count - 1; i >= 0; i--)
            {
                if (!Model.Tasks[i].IsProject)
                {
                    keyId = Model.Tasks[i].ID;
                    break;
                }
            }

            if (keyId != string.Empty)
            {
                View.m_treeTasks.FocusedNode = View.m_treeTasks.FindNodeByKeyID(keyId);
                View.m_ctlTaskEdit.Focus();
                View.m_ctlTaskEdit.SetFocusToNotes();
            }                
        }

        #endregion

        #region OnTaskActionClick

        private void OnTaskActionClick(object sender, EventArgs e)
        {
            if (View.m_treeTasks.FocusedColumn.Name != View.m_colTask.Name)
                return;

            TaskProjectWrapperComplete task
                = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);

            View.m_menuActionAddMiscellaneous.Enabled = task.IsAddMiscAllowed;
            View.m_menuActionAddRugPickup.Enabled = Model.IsAddRugPickupPartOfDefloodAllowed(task);
            View.m_menuDeleteTask.Enabled = task.IsDeleteAllowed;

            View.m_menuActionAddMiscellaneous.Tag = task;
            View.m_menuActionAddRugPickup.Tag = task;
            View.m_menuDeleteTask.Tag = task;

            Rectangle rectangle
                = View.m_treeTasks.ViewInfo.RowsInfo[View.m_treeTasks.FocusedNode][View.m_colTask].Bounds;

            Point position = new Point(rectangle.Location.X, rectangle.Location.Y + rectangle.Height);

            View.m_menuAction.ShowPopup(View.m_treeTasks.PointToScreen(position));

            for (int i = 0; i < View.m_menuAction.ItemLinks.Count; i++)
            {
                if (View.m_menuAction.ItemLinks[i].Enabled)
                {
                    View.m_menuAction.ItemLinks[i].Focus();
                    break;
                }
            }

        }

        private void OnDeleteTaskClick(object sender, ItemClickEventArgs e)
        {
            TaskProjectWrapperComplete task = (TaskProjectWrapperComplete)e.Item.Tag;
            int removeIndex = Model.Tasks.IndexOf(task);

            m_allowSaveDataToModel = false;
            Model.DeleteTask(task);
            m_allowSaveDataToModel = true;

            if (removeIndex > 0 && removeIndex <= Model.Tasks.Count)
                SelectTask(Model.Tasks[removeIndex - 1]);
            else
                SelectTask(Model.Tasks[0]);
            View.m_treeTasks.ExpandAll();
        }

        private void OnActionAddMiscellaneousClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNewTask(TaskTypeEnum.Miscellaneous, (TaskProjectWrapperComplete)e.Item.Tag);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastTask();
        }

        private void OnActionAddRugPickupClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNewTask(TaskTypeEnum.RugPickup, (TaskProjectWrapperComplete)e.Item.Tag);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastTask();
        }

        #endregion

        #region OnTasksShowingEditor

        private void OnTasksShowingEditor(object sender, CancelEventArgs e)
        {
            TaskProjectWrapperComplete task
                = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);

            if (View.m_treeTasks.FocusedColumn.Name == View.m_colTaskAction.Name)
                e.Cancel = !task.IsTaskActionEditAllowed;
        }

        #endregion

        #region OnTasksCustomNodeCellEdit

        private void OnTasksCustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.Name != View.m_colTaskAction.Name)
                return;

            TaskProjectWrapperComplete task
                = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(e.Node);

            if (task == null)
                return;

            if (task.IsProject || !task.IsIncludedInVisit)
            {
                e.RepositoryItem = View.m_cmbTaskActionNotApplicable;
                return;
            }
                
            if (task.Task.TaskType == TaskTypeEnum.Deflood)
            {
                if (task.IsNewlyAdded)
                    e.RepositoryItem = View.m_cmbTaskActionDefloodAdded;
                else if (task.IsDefloodFirstTimeService)
                    e.RepositoryItem = View.m_cmbTaskActionDefloodFirstTime;
                else
                    e.RepositoryItem = View.m_cmbTaskActionDeflood;
                return;
            }

            if (task.IsNewlyAdded)
                e.RepositoryItem = View.m_cmbTaskActionAdded;
            else
                e.RepositoryItem = View.m_cmbTaskAction;
        }

        #endregion

        #region OnTaskClosedAmountChanged

        private void OnTaskClosedAmountChanged(decimal cost)
        {
            TaskProjectWrapperComplete task
                = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);
            
            if (task.Task.TaskType == TaskTypeEnum.RugPickup 
                && task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                task.Task.EstimatedClosedAmount = cost;
            }                
            else
                task.Task.ClosedAmount = cost;
            
            Model.Tasks.ResetItem(Model.Tasks.IndexOf(task));
            TaskProjectWrapperComplete recalculatedProject = Model.RecalculateProjectClosedAmount(task.Task.ProjectId);

            View.m_treeTasks.FocusedNodeChanged -= OnTasksFocusedNodeChanged;
            Model.Tasks.ResetItem(Model.Tasks.IndexOf(recalculatedProject));
            View.m_treeTasks.FocusedNodeChanged += OnTasksFocusedNodeChanged;
        }

        #endregion

        #region OnTasksFocusedNodeChanged

        private bool m_allowSaveDataToModel = true;

        private void OnTasksFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {   
            ActivateDeactivateActionEditor(View.m_treeTasks.FocusedColumn);

            if (e.OldNode != null && m_allowSaveDataToModel)
            {
                TaskProjectWrapperComplete prevTask
                    = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(e.OldNode);

                if (prevTask != null)
                {
                    int oldTaskIndex = Model.Tasks.IndexOf(prevTask);

                    if (prevTask.IsProject)
                    {
                        Model.Tasks[oldTaskIndex].Task.Project = View.m_ctlProjectEdit.Project;
                    }
                    else
                    {
                        Model.Tasks[oldTaskIndex].Items = View.m_ctlTaskEdit.Items;
                        Model.Tasks[oldTaskIndex].Task = View.m_ctlTaskEdit.Task;                        
                    }
                }
                
            }

            if (e.Node == null)
            {
                View.m_ctlTaskEdit.Visible = false;
                View.m_ctlProjectEdit.Visible = false;
                return;
            }


            TaskProjectWrapperComplete task = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(e.Node);
            if (task.IsProject)
            {
                View.m_ctlTaskEdit.Visible = false;
                View.m_ctlProjectEdit.Visible = true;

                View.m_ctlProjectEdit.AreaId = Model.ServiceAddress.AreaId;
                View.m_ctlProjectEdit.Project = task.Task.Project;
                View.m_ctlProjectEdit.Initialize();
                View.m_ctlProjectEdit.IsEditable = true;
            } else
            {
                View.m_ctlTaskEdit.Visible = true;
                View.m_ctlProjectEdit.Visible = false;

                View.m_ctlTaskEdit.IsClosedAmountEditable = Model.IsClosedAmountAllowed(task);                

                if (m_allowSaveDataToModel && task.IsIncludedInVisit
                    && (task.TaskAction == TaskActionEnum.Fail || task.TaskAction == TaskActionEnum.Book)
                    && !(task.Task.TaskType == TaskTypeEnum.Deflood && !task.IsDefloodFirstTimeService))
                {
                    task.Task.ClosedAmount = decimal.Zero;

                    if (task.Task.TaskType != TaskTypeEnum.RugDelivery)
                        task.Task.EstimatedClosedAmount = decimal.Zero;
                }

                if (task.IsIncludedInVisit && task.Task.TaskType == TaskTypeEnum.Deflood
                    && task.IsDefloodFirstTimeService)
                {
                    View.m_ctlTaskEdit.IsClosedAmountUnknownVisible = true;
                }
                else if (m_allowSaveDataToModel)
                {
                    View.m_ctlTaskEdit.IsClosedAmountUnknownVisible = false;
                    task.Task.IsAmountNotKnown = false;
                }

                View.m_ctlTaskEdit.Items = task.Items;
                View.m_ctlTaskEdit.Task = task.Task;
                View.m_ctlTaskEdit.ClearClosedAmountError();
                View.m_ctlTaskEdit.IsEditable = task.IsIncludedInVisit || task.IsNewlyAdded;

                if (m_allowSaveDataToModel && task.Task.TaskType == TaskTypeEnum.Monitoring &&
                    (!task.IsMonitoringFirstTimeService || Model.FindDefloodTask(task).TaskActionId == (int)TaskActionEnum.InProcess)
                    && task.TaskActionId == (int)TaskActionEnum.Fail)
                {
                    task.Task.TaskFailType = TaskFailTypeEnum.MustReturn;
                    View.m_ctlTaskEdit.Task = task.Task;
                    View.m_ctlTaskEdit.IsEditable = task.IsIncludedInVisit || task.IsNewlyAdded;
                    View.m_ctlTaskEdit.IsFailTypeChangeAllowed = false;

                }
                else if (m_allowSaveDataToModel && task.Task.TaskType == TaskTypeEnum.RugDelivery
                    && task.TaskActionId == (int)TaskActionEnum.Fail)
                {
                    task.Task.TaskFailType = TaskFailTypeEnum.MustReturn;
                    View.m_ctlTaskEdit.Task = task.Task;
                    View.m_ctlTaskEdit.IsEditable = task.IsIncludedInVisit || task.IsNewlyAdded;
                    View.m_ctlTaskEdit.IsFailTypeChangeAllowed = false;
                }
                else
                {
                    View.m_ctlTaskEdit.IsFailTypeChangeAllowed = true;
                }                
            }
        }

        #endregion

        #region OnTasksCellValueChanged

        private void OnTasksCellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == View.m_colTaskAction.Name)
            {
                TaskProjectWrapperComplete task = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(e.Node);
                task.TaskActionId = (int) e.Value;

                if (!task.IsProject && task.IsIncludedInVisit
                    && task.Task.TaskType == TaskTypeEnum.Deflood)
                {
                    Model.ModifyMonitoringByDeflood(task);
                }

                if (!task.IsProject && task.IsIncludedInVisit
                    && task.Task.TaskType == TaskTypeEnum.Monitoring)
                {
                    Model.ModifyDefloodByMonitoring(task);
                }


//                if ((int)e.Value != (int)TaskActionEnum.Fail
//                    && (int)e.Value != (int)TaskActionEnum.Book
//                    && task.Task.TaskFailType == null)
//                {
//                    return;
//                }

                OnTasksFocusedNodeChanged(null, new FocusedNodeChangedEventArgs(e.Node, e.Node));
                task = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(e.Node);               


                if ((int)e.Value == (int)TaskActionEnum.Fail)
                {
                    if (task.Task.TaskType == TaskTypeEnum.RugDelivery)
                        task.Task.TaskFailType = TaskFailTypeEnum.MustReturn;
                    else
                        task.Task.TaskFailType = TaskFailTypeEnum.Cancel;
                }                    
                else
                    task.Task.TaskFailType = null;

                OnTasksFocusedNodeChanged(null, new FocusedNodeChangedEventArgs(null, e.Node));
            }
        }

        #endregion

        #region OnFailTypeChanged

        private void OnFailTypeChanged(Task task, TaskFailTypeEnum? failType)
        {
            if (task.TaskType == TaskTypeEnum.Deflood
                || task.TaskType == TaskTypeEnum.Monitoring)
            {
                TaskProjectWrapperComplete taskWrapper
                    = (TaskProjectWrapperComplete)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);
                taskWrapper.Task.TaskFailType = failType;

                if (task.TaskType == TaskTypeEnum.Deflood)
                    Model.ModifyMonitoringByDeflood(taskWrapper);
                else
                    Model.ModifyDefloodByMonitoring(taskWrapper);                
            }
        }

        #endregion

        #region SelectTask

        private void SelectTask(TaskProjectWrapperComplete task)
        {
            View.m_tabs.SelectedTabPageIndex = 1;
            TreeListNode node = View.m_treeTasks.FindNodeByKeyID(task.ID);
            View.m_treeTasks.FocusedNode = node;
        }

        #endregion

        //Completion
        
        #region IsFormValid

        private bool IsFormValid()
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return false;

            DateTime endTime = new DateTime(
                Model.Work.StartDate.Value.Year,
                Model.Work.StartDate.Value.Month,
                Model.Work.StartDate.Value.Day,
                View.m_timeComplete.Time.Hour,
                View.m_timeComplete.Time.Minute, 0);

            CollisionErrorEnum collision
                = WorkDetail.IsExistCollision(Model.WorkDetail, Model.WorkDetail.TimeBegin, endTime);

            if (collision == CollisionErrorEnum.ProcessedVisit)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Complete. There is time collision with another processed visit",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (collision == CollisionErrorEnum.StartDay)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Complete. There is time collision with Start Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (collision == CollisionErrorEnum.EndDay)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Complete. There is time collision with End Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            ////

            OnTasksFocusedNodeChanged(null,
                new FocusedNodeChangedEventArgs(View.m_treeTasks.FocusedNode, View.m_treeTasks.FocusedNode));

            foreach (TaskProjectWrapperComplete task in Model.Tasks)
            {
                if ((task.Task.TaskType == TaskTypeEnum.RugPickup || task.Task.TaskType == TaskTypeEnum.RugDelivery)
                    && task.IsIncludedInVisit
                    && task.TaskActionId == (int)TaskActionEnum.Complete)
                {
                    if (task.Items == null || task.Items.Count == 0)
                    {
                        XtraMessageBox.Show("Please enter captured Rugs", "No Rugs captured ", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        SelectTask(task);    
                        View.m_ctlTaskEdit.SetFocusToRugsGrid();
                        return false;
                    }

                    foreach (Item item in task.Items)
                    {
                        if (!item.IsValid)
                        {
                            XtraMessageBox.Show("Please make sure you have specified Rugs dimensions", "Invalid Rug(s)", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                            SelectTask(task);
                            View.m_ctlTaskEdit.SetFocusToRugsGrid();
                            return false;
                        }
                    }
                }

                if (task.IsProject)
                {
                    if (!string.IsNullOrEmpty(task.Task.Project.QbSalesRepListId) &&
                        !string.IsNullOrEmpty(task.Task.Project.ExpectedQbSalesRepListId) 
                        && task.Task.Project.ExpectedQbSalesRepListId != task.Task.Project.QbSalesRepListId)
                    {
                        QbSalesRep expectedSalesRep =
                                QbSalesRep.FindByPrimaryKey(task.Task.Project.ExpectedQbSalesRepListId);
                        QbSalesRep selectedSalesRep = QbSalesRep.FindByPrimaryKey(task.Task.Project.QbSalesRepListId);
                            
                        if (XtraMessageBox.Show(
                            "Are you sure that " + selectedSalesRep.DisplayName +
                            " sold the job?  First technician on the job was " + expectedSalesRep.DisplayName,
                            "Confirm Sales Rep selection", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.No)
                        {
                            SelectTask(task);
                            View.m_ctlProjectEdit.m_cmbQbSalesRep.Focus();
                            return false;
                        }
                    }
                }
            }

            TaskProjectWrapperComplete invalidProject = Model.FindNotValidProject();
            if (invalidProject != null)
            {
                SelectTask(invalidProject);
                View.m_ctlProjectEdit.ValidateChildren();
                return false;
            }

            TaskProjectWrapperComplete invalidTask = Model.FindInvalidClosedAmountTask();
            if (invalidTask != null)
            {
                SelectTask(invalidTask);
                View.m_ctlTaskEdit.SetClosedAmountError();
                return false;
            }

            invalidTask = Model.FindInvalidReadingsTask();
            if (invalidTask != null)
            {
                SelectTask(invalidTask);
                XtraMessageBox.Show("Please enter monitoring readings", "Monitoring Readings error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_ctlTaskEdit.SetFocusToReadingsTable();
                return false;
            }

            string editAllowanceErrorMessage = Model.GetVisitEditAllowance();
            if (editAllowanceErrorMessage != string.Empty)
            {
                XtraMessageBox.Show(
                    editAllowanceErrorMessage, "Unable to recomplete Visit",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                View.m_tabs.SelectedTabPageIndex = 1;
                return false;
            }

           
            return true;
        }

        #endregion

        #region OnTimeCompleteValidating

        private void OnTimeCompleteValidating(object sender, CancelEventArgs e)
        {
            if (DateTime.Now.Date == Model.Work.StartDate.Value.Date && View.m_timeComplete.Time > DateTime.Now)
                View.m_errorProvider.SetError(View.m_timeComplete, "Completion time cannot be in the future");
            else if (Model.WorkDetail.TimeArrive.HasValue && View.m_timeComplete.Time < Model.WorkDetail.TimeArrive.Value)
                View.m_errorProvider.SetError(View.m_timeComplete, "Completion time cannot be less than Visit arrival time");
            else if (View.m_timeComplete.Time < Model.WorkDetail.TimeBegin)
                View.m_errorProvider.SetError(View.m_timeComplete, "Completion time cannot be less than Visit start time");
            else
                View.m_errorProvider.SetError(View.m_timeComplete, string.Empty);
        }

        #endregion

        #region OnTimeCompleteButtonClick

        private void OnTimeCompleteButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                View.m_timeComplete.Time = Model.WorkDetail.TimeEnd;
            else if (e.Button.Index == 2)
                View.m_timeComplete.Time = DateTime.Now;
        }

        #endregion


        #region OnCompleteClick

        private void OnCompleteClick(object sender, EventArgs e)
        {
            CompleteVisit(false);
        }

        private void OnCompleteWithPaymentClick(object sender, ItemClickEventArgs e)
        {
            CompleteVisit(true);
        }

        private void OnCompleteWithoutPaymentClick(object sender, ItemClickEventArgs e)
        {
            CompleteVisit(false);
        }


        public void CompleteVisit(bool usePayment)
        {
            if (!IsFormValid())
                return;

            Model.Visit.Notes = View.m_txtVisitNotes.Text;
            VisitCompletePackage completePackage = Model.GetCompletePackage();
            completePackage.CompletionTime = new DateTime(
                Model.Work.StartDate.Value.Year,
                Model.Work.StartDate.Value.Month,
                Model.Work.StartDate.Value.Day,
                View.m_timeComplete.Time.Hour,
                View.m_timeComplete.Time.Minute, 0);

            if (Model.IsAskIfVisitShouldBeClosed())
            {
                if (XtraMessageBox.Show("Customer has no equipment left. Are you sure deflood still in progress?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {                    
                    return;
                }
            }

            if (Model.IsWarnFromClosingVisit())
            {
                if (XtraMessageBox.Show("Customer has equipment left.  Are you sure deflood is completed?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            bool showNextVisitForm = false;
            if (Model.IsNextVisitExists(completePackage))
            {
                if (Model.IsMustArrangeNextVisit())
                {
                    showNextVisitForm = true;
                }
                else
                {
                    DialogResult dialogResult = XtraMessageBox.Show(
                        "Would you like to arrange next Visit(s)?", "Next Visit(s) arrangement",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Cancel)
                        return;

                    if (dialogResult == DialogResult.Yes)
                        showNextVisitForm = true;
                }
            }
            
            if (usePayment)
            {
                using (MakePaymentController controller
                    = Prepare<MakePaymentController>(completePackage))
                {
                    controller.Execute(false);
                    if (controller.IsCancelled)
                        return;

                }
            }

            VisitCompleteResultPackage result;
            try
            {
                Database.Begin();
                result = completePackage.CompleteVisit(Configuration.CurrentDispatchId, null);
                Database.Commit();
                Host.TraceUserAction("Complete Visit " + Model.Visit.ID);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            if (showNextVisitForm)
            {
                if (result.NewVisit1 != null && result.NewVisit2 != null)
                {
                    using (VisitSplitController controller = Prepare<VisitSplitController>(
                        result.NewVisit1, result.NewVisit2))
                    {
                        controller.Execute(false);
                    }                    
                } else
                {
                    Visit newVisit;
                    GeneratedVisitActionEnum newVisitAction;

                    if (result.NewVisit1 != null)
                    {
                        newVisit = result.NewVisit1;
                        newVisitAction = result.NewVisit1Action;
                    }
                    else
                    {
                        newVisit = result.NewVisit2;
                        newVisitAction = result.NewVisit2Action;                        
                    }

                    using (CreateVisitController editVisitcontroller = Prepare<CreateVisitController>(
                        newVisit, newVisitAction == GeneratedVisitActionEnum.Created))
                    {
                        editVisitcontroller.Execute(false);
                    }                    
                }

            } else
            {                         
                if (result.NewVisit1 != null && result.NewVisit1.IsNeedToPrint(
                    null, result.NewVisit1Action == GeneratedVisitActionEnum.Created))
                {
                    VisitSummaryPackage summaryPackage = new VisitSummaryPackage(result.NewVisit1);
                    summaryPackage.Print();
                }

                if (result.NewVisit2 != null && result.NewVisit2.IsNeedToPrint(
                    null, result.NewVisit2Action == GeneratedVisitActionEnum.Created))
                {
                    VisitSummaryPackage summaryPackage = new VisitSummaryPackage(result.NewVisit2);
                    summaryPackage.Print();                    
                }
            }

            View.Destroy();
        }

        #endregion        

        #region OnCancelClick

        protected override bool OnCancel()
        {
            if (XtraMessageBox.Show("Do you want to cancel?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                m_isCancelled = true;
                return false;
            }
            else
                return true;            
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion
    }
}
