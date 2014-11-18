using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.LeadLookup;
using Dalworth.Server.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Dalworth.Server.MainForm.CreateVisit
{    
    public class CreateVisitController : Controller<CreateVisitModel, CreateVisitView>
    {
        private readonly Color m_colorFocusedRow = Color.FromArgb(49, 106, 197);
        private readonly Color m_colorFocusedRowHidden = Color.FromArgb(122, 150, 223);
        private readonly Color m_colorFocusedRowText = Color.White;
        private readonly Color m_colorExcludedRow = Color.LightGray;
        private readonly Color m_colorExcludedFocusedRow = Color.Black;
        private readonly Color m_colorExcludedFocusedRowHidden = Color.Gray;

        #region AffectedVisit

        private Visit m_affectedVisit;
        public Visit AffectedVisit
        {
            get { return m_affectedVisit; }
        }

        #endregion

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region BaseLead

        public LeadWrapper BaseLead
        {
            get { return Model.BaseLead; }
            set { Model.BaseLead = value; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data!= null && data.Length == 1 && data[0] is Customer)
            {
                Model.CurrentCustomer = (Customer) data[0];
                return;
            }

            if (data != null && data.Length > 0 && data[0] != null)
                Model.ExistingVisit = (Visit) data[0];

            if (data != null && data.Length > 1 && data[1] != null)
                Model.IsGeneratedVisitAdjustment = (bool) data[1];

            if (data != null && data.Length > 2 && data[2] != null)
                Model.IsReschedule = (bool) data[2];
            
            if (data != null && data.Length > 3 && data[3] != null)
                Model.BaseVisit = (Visit) data[3];            

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {   
            View.m_treeTasks.FocusedNodeChanged += OnTasksFocusedNodeChanged;
            View.m_treeTasks.CustomDrawNodeCell += OnTasksCustomDrawNodeCell;
            View.m_treeTasks.NodeCellStyle += OnTasksCellStyle;

            View.m_menuAddRugPickup.ItemClick += OnAddRugPickupClick;
            View.m_menuAddDeflood.ItemClick += OnAddDefloodClick;
            View.m_menuAddMiscellaneous.ItemClick += OnAddMiscellaneousClick;

            View.m_treeTasks.DoubleClick += OnTaskActionClick;
            View.m_menuActionAddHelp.ItemClick += OnActionAddHelpClick;
            View.m_menuActionAddMiscellaneous.ItemClick += OnActionAddMiscellaneousClick;
            View.m_menuActionAddMonitoring.ItemClick += OnActionAddMonitoringClick;
            View.m_menuActionAddRugPickup.ItemClick += OnActionAddRugPickupClick;
            View.m_menuActionIncludeInVisit.ItemClick += OnActionIncludeInVisitClick;
            View.m_menuActionExcludeFromVisit.ItemClick += OnActionExcludeFromVisitItemClick;
            View.m_menuCancelTask.ItemClick += OnCancelTaskClick;
            View.m_menuDeleteTask.ItemClick += OnDeleteTaskClick;

            View.m_chkShowCompleted.CheckedChanged += OnTaskFilterChanged;
            View.m_chkShowFailed.CheckedChanged += OnTaskFilterChanged;
            View.m_chkShowNotReady.CheckedChanged += OnTaskFilterChanged;

            View.m_btnAddTask.Click += OnAddTaskClick;
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;
            
            View.m_ctlCustomerLookup.CustomerChanged += OnCustomerChanged;
            View.m_ctlCustomerLookup.Modified += OnCustomerModified;
            View.m_ctlCustomerLookup.EmailVisible = true;
            View.m_ctlAddressLookup.Modified += OnAddressModified;    
            View.m_tabs.SelectedPageChanged += OnSelectedTabPageChanged;
            View.m_treeTasks.KeyDown += OnTasksKeyDown;

            View.KeyDown += OnKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_ctlAddressLookup.Caption = "Customer Address";
            View.m_menuActionAddMonitoring.Visibility = BarItemVisibility.Never;

            if (Model.ExistingVisit != null)
            {                
                View.Text = Model.IsReadOnly ? "Dalworth - View Visit" : "Dalworth - Edit Visit";

                SetCustomerAndAddressFromVisit(Model.ExistingVisit);                   
                View.m_ctlVisitHeader.Visit = Model.ExistingVisit;                
            } 

            RefreshTasks();

            if (Model.IsReadOnly || Model.ExistingVisit != null) 
            {
                //View.m_ctlCustomerLookup.IsReadOnly = true;
                View.m_ctlAddressLookup.Enabled = false;
            }

            View.m_ctlVisitHeader.IsReadOnly = Model.IsReadOnly;

            View.m_chkShowCompleted.Enabled = !Model.IsReadOnly;
            View.m_chkShowNotReady.Enabled = !Model.IsReadOnly;
            View.m_chkShowFailed.Enabled = !Model.IsReadOnly;
            View.m_btnAddTask.Enabled = !Model.IsReadOnly;
            View.m_btnOk.Enabled = !Model.IsReadOnly;      
      
            if (Model.ExistingVisit == null)
            {
                if (Model.CurrentCustomer != null)
                {
                    View.m_ctlCustomerLookup.Customer = Model.CurrentCustomer;
                    View.m_ctlCustomerLookup.Address = Address.FindByPrimaryKey(Model.CurrentCustomer.AddressId.Value);
                    OnCustomerChanged(View.m_ctlCustomerLookup.Customer, View.m_ctlCustomerLookup.Address);
                }
                else if (Model.BaseVisit != null)
                {
                    SetCustomerAndAddressFromVisit(Model.BaseVisit);
                    OnCustomerChanged(View.m_ctlCustomerLookup.Customer, View.m_ctlCustomerLookup.Address);
                } else
                {
                    if (Model.BaseLead != null)
                        View.m_ctlCustomerLookup.BaseLead = Model.BaseLead;

                    View.m_ctlCustomerLookup.ShowLookupDialog();

                    Model.CurrentCustomer  = View.m_ctlCustomerLookup.Customer;
                    Address currentAddress = View.m_ctlAddressLookup.CurrentAddress;
                    if (Model.CurrentCustomer == null)
                    {
                        m_isCancelled = true;
                        View.Destroy();
                    } else
                    {
                        if (Model.BaseLead == null)
                        {
                            BindingList<LeadWrapper> recentLeads =
                                Lead.FindSimilarLeadWrappers(Model.CurrentCustomer.FirstName, Model.CurrentCustomer.LastName,
                                                             Model.CurrentCustomer.Phone1, Model.CurrentCustomer.Phone2);
                            if (recentLeads.Count > 0)
                            {
                                CustomerAndAddress customerAndAddress =
                                    new CustomerAndAddress(Model.CurrentCustomer, currentAddress);

                                using (LeadLookupController controller = Prepare<LeadLookupController>(recentLeads, customerAndAddress))
                                {
                                    controller.Execute(false);

                                    if (!controller.IsCancelled && controller.SelectedLead != null)
                                        Model.BaseLead = controller.SelectedLead;
                                }
                            }
                        }
                    }
                }

                View.m_chkShowFailed.Checked = true;                    
            }

            if (Model.BaseLead != null && Model.CurrentAddress != null)
            {
                if (Model.BaseLead.PreferredServiceDate.HasValue)
                    View.m_ctlVisitHeader.m_dtpServiceDate.EditValue = Model.BaseLead.PreferredServiceDate.Value;

                switch (Model.BaseLead.Lead.ProjectType)
                {
                    case ProjectTypeEnum.RugCleaning:
                        OnAddRugPickupClick(null, null);
                        break;
                    case ProjectTypeEnum.Deflood:
                        OnAddDefloodClick(null, null);
                        break;
                    case ProjectTypeEnum.Miscellaneous:
                        OnAddMiscellaneousClick(null, null);
                        break;
                }
                
                if(Model.BaseLead.BusinessPartner.QbCustomerTypeListId != null)
                {
                    View.m_ctlProjectEdit.Project.QbCustomerTypeListId =
                        Model.BaseLead.BusinessPartner.QbCustomerTypeListId;
                }

                Customer customer = View.m_ctlCustomerLookup.Customer;
                Address address = View.m_ctlCustomerLookup.Address;
                if (customer != null
                    && string.IsNullOrEmpty(customer.Email)
                    && !string.IsNullOrEmpty(BaseLead.Lead.Email))
                {
                    customer.Email = BaseLead.Lead.Email;
                    OnCustomerModified(customer, address);
                }
            }

            View.m_ctlCustomerLookup.EditButtonText = "&Edit";
            View.m_ctlAddressLookup.EditButtonText = "E&dit";
        }

        #endregion

        #region SetCustomerAndAddressFromVisit

        private void SetCustomerAndAddressFromVisit(Visit visit)
        {
            Customer customer = null;
            Address baseAddress = null;
            Address currentAddress;

            if (visit.CustomerId.HasValue)
            {
                customer = Customer.FindByPrimaryKey(visit.CustomerId.Value);
                baseAddress = Address.FindByPrimaryKey(customer.AddressId.Value);

                View.m_ctlCustomerLookup.Customer = customer;
                View.m_ctlCustomerLookup.Address = baseAddress;
            }


            if (visit.ServiceAddressId.HasValue)
            {
                currentAddress = Address.FindByPrimaryKey(visit.ServiceAddressId.Value);

                View.m_ctlAddressLookup.Customer = customer;
                View.m_ctlAddressLookup.BaseAddress = baseAddress;
                View.m_ctlAddressLookup.CurrentAddress = currentAddress;
                if (baseAddress != null)
                    View.m_ctlAddressLookup.IsBaseAddressActive = baseAddress.ID == currentAddress.ID;
                View.m_ctlAddressLookup.BaseAddressName = "Customer Address";
                Model.CurrentAddress = currentAddress;
            }
            
        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
                OnOkClick(null, null);
        }

        #endregion


        #region OnSelectedTabPageChanged

        private void OnSelectedTabPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (View.m_tabs.SelectedTabPageIndex == 0)
                View.m_ctlVisitHeader.Focus();
            else
                View.m_treeTasks.Focus();
        }

        #endregion

        #region OnTasksKeyDown

        private void OnTasksKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && e.KeyCode == Keys.Enter && View.m_treeTasks.Focused)
            {
                TaskProjectWrapper task
                    = (TaskProjectWrapper)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);

                if (task != null)
                {
                    OnTaskActionClick(null, null);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }                    
            }
        }

        #endregion

        #region OnTasksCustomDrawNodeCell

        private static void OnTasksCustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
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

            TaskProjectWrapper task
                = (TaskProjectWrapper)View.m_treeTasks.GetDataRecordByNode(e.Node);

            if (task != null && !task.IsIncludedInVisit && !task.IsExistInVisit)
            {
                if (e.Node.Focused)
                {
                    e.Appearance.BackColor = View.m_treeTasks.Focused ? m_colorExcludedFocusedRow : m_colorExcludedFocusedRowHidden;
                    e.Appearance.ForeColor = m_colorFocusedRowText;
                }                    
                else
                    e.Appearance.BackColor = m_colorExcludedRow;
            }
            else if (e.Node.Focused)
            {
                e.Appearance.BackColor = View.m_treeTasks.Focused ? m_colorFocusedRow : m_colorFocusedRowHidden;
                e.Appearance.ForeColor = m_colorFocusedRowText;
            }            
        }

        #endregion

        #region OnTasksFocusedNodeChanged

        private bool m_allowSaveDataToModel = true;

        private void OnTasksFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            View.m_ctlProjectEdit.ClearErrors();

            if (e.OldNode != null && m_allowSaveDataToModel)
            {
                TaskProjectWrapper prevTask 
                    = (TaskProjectWrapper)View.m_treeTasks.GetDataRecordByNode(e.OldNode);

                if (prevTask != null)
                {
                    int oldTaskIndex = Model.Tasks.IndexOf(prevTask);

                    if (prevTask.IsProject)
                    {
                        Model.Tasks[oldTaskIndex].Task.Project = View.m_ctlProjectEdit.Project;
                    } else
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
                

            TaskProjectWrapper task = (TaskProjectWrapper) View.m_treeTasks.GetDataRecordByNode(e.Node);

            if (task == null)
                return;

            if (task.IsProject)
            {
                View.m_ctlTaskEdit.Visible = false;
                View.m_ctlProjectEdit.Visible = true;

                View.m_ctlProjectEdit.AreaId = Model.CurrentAddress.AreaId;
                View.m_ctlProjectEdit.Project = task.Task.Project;
                View.m_ctlProjectEdit.Initialize();

                if (Model.IsReadOnly)
                    View.m_ctlProjectEdit.IsEditable = false;
                else
                    View.m_ctlProjectEdit.IsEditable = task.IsIncludedInVisit || task.IsNewlyAdded || task.IsExistInVisit;                
            }
            else
            {
                View.m_ctlTaskEdit.Visible = true;
                View.m_ctlProjectEdit.Visible = false;

                View.m_ctlTaskEdit.OriginalMessage = task.OriginalMessage;
                View.m_ctlTaskEdit.Items = task.Items;
                View.m_ctlTaskEdit.Task = task.Task;
                if (Model.IsReadOnly)
                    View.m_ctlTaskEdit.IsEditable = false;
                else
                    View.m_ctlTaskEdit.IsEditable = task.IsIncludedInVisit || task.IsNewlyAdded || task.IsExistInVisit;                
            }

        }

        #endregion

        #region OnTaskActionClick

        private void OnTaskActionClick(object sender, EventArgs e)
        {
            if (Model.IsReadOnly)
                return;

            TaskProjectWrapper task 
                = (TaskProjectWrapper)View.m_treeTasks.GetDataRecordByNode(View.m_treeTasks.FocusedNode);

            if (task == null)
                return;            

            View.m_menuActionAddHelp.Enabled = task.IsAddHelpAllowed;
            View.m_menuActionAddMiscellaneous.Enabled = task.IsAddMiscAllowed;
            View.m_menuActionAddMonitoring.Enabled = Model.IsAddMonitoringAllowed(task);
            View.m_menuActionAddRugPickup.Enabled = task.IsAddRugPickupAllowed;
            View.m_menuActionIncludeInVisit.Enabled = task.IsIncludeInVisitAllowed;
            View.m_menuActionExcludeFromVisit.Enabled = task.IsExcludeFromVisitAllowed;
            View.m_menuCancelTask.Enabled = task.IsCancelAllowed(Model.ExistingVisit);
            View.m_menuDeleteTask.Enabled = Model.IsDeleteAllowed(task);            

            View.m_menuActionAddHelp.Tag = task;
            View.m_menuActionAddMiscellaneous.Tag = task;
            View.m_menuActionAddMonitoring.Tag = task;
            View.m_menuActionAddRugPickup.Tag = task;
            View.m_menuActionIncludeInVisit.Tag = task;
            View.m_menuActionExcludeFromVisit.Tag = task;
            View.m_menuCancelTask.Tag = task;
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


        private void OnCancelTaskClick(object sender, ItemClickEventArgs e)
        {
            TaskProjectWrapper task = (TaskProjectWrapper)e.Item.Tag;
            TreeListNode currentNode = View.m_treeTasks.FocusedNode;

            OnTasksFocusedNodeChanged(null,
                new FocusedNodeChangedEventArgs(currentNode, currentNode));

            View.m_treeTasks.BeginUpdate();
            m_allowSaveDataToModel = false;
            Model.CancelTaskWithSubtasks(task);
            m_allowSaveDataToModel = true;
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            OnTasksFocusedNodeChanged(null,
                new FocusedNodeChangedEventArgs(null, currentNode));

//            for (int i = Model.Tasks.Count - 1; i >= 0; i--)
//            {
//                if (Model.Tasks[i].ParentId == task.ID && Model.Tasks[i].IsNewlyAdded)
//                    Model.CancelTask(Model.Tasks[i]);
//            }            
        }

        private void OnDeleteTaskClick(object sender, ItemClickEventArgs e)
        {
            TaskProjectWrapper task = (TaskProjectWrapper)e.Item.Tag;
            int removeIndex = Model.Tasks.IndexOf(task);

            m_allowSaveDataToModel = false;
            Model.DeleteTask(task);
            m_allowSaveDataToModel = true;

            if (!task.IsProject)
                Model.SetIncludedExcludedProjectByTask(task);

            if (removeIndex > 0 && removeIndex <= Model.Tasks.Count)
                SelectTask(Model.Tasks[removeIndex - 1]);
            else if (Model.Tasks.Count > 0)
                SelectTask(Model.Tasks[0]);
            View.m_treeTasks.ExpandAll();

            if (View.m_treeTasks.Nodes.Count == 0)
            {
                View.m_ctlTaskEdit.Visible = false;
                View.m_ctlProjectEdit.Visible = false;
            }                
        }

        private void OnActionAddMonitoringClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNew(TaskTypeEnum.Monitoring, (TaskProjectWrapper)e.Item.Tag);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();            
            SelectLastTask();
        }

        private void OnActionAddMiscellaneousClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNew(TaskTypeEnum.Miscellaneous, (TaskProjectWrapper)e.Item.Tag);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastTask();
        }

        private void OnActionAddHelpClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNew(TaskTypeEnum.Help, (TaskProjectWrapper)e.Item.Tag);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastTask();
        }

        private void OnActionAddRugPickupClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNew(TaskTypeEnum.RugPickup, (TaskProjectWrapper)e.Item.Tag);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastTask();
        }

        private void OnActionIncludeInVisitClick(object sender, ItemClickEventArgs e)
        {
            TaskProjectWrapper task = (TaskProjectWrapper) e.Item.Tag;
            View.m_treeTasks.BeginUpdate();
            m_allowSaveDataToModel = false;
            Model.IncludeExcludeInVisit(task, true);
            m_allowSaveDataToModel = true;
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            OnTasksFocusedNodeChanged(null, 
                new FocusedNodeChangedEventArgs(null, View.m_treeTasks.FocusedNode));
        }

        private void OnActionExcludeFromVisitItemClick(object sender, ItemClickEventArgs e)
        {
            TaskProjectWrapper task = (TaskProjectWrapper)e.Item.Tag;
            TreeListNode currentNode = View.m_treeTasks.FocusedNode;

            OnTasksFocusedNodeChanged(null,
                new FocusedNodeChangedEventArgs(currentNode, currentNode));

            View.m_treeTasks.BeginUpdate();
            m_allowSaveDataToModel = false;
            Model.IncludeExcludeInVisit(task, false);
            m_allowSaveDataToModel = true;
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            OnTasksFocusedNodeChanged(null,
                new FocusedNodeChangedEventArgs(null, currentNode));

            for (int i = Model.Tasks.Count - 1; i >= 0; i--)
            {
                if (Model.Tasks[i].ParentId == task.ID && Model.Tasks[i].IsNewlyAdded)
                    Model.Tasks.Remove(Model.Tasks[i]);                
            }
        }

        #endregion

        #region OnAddTaskClick

        private void OnAddTaskClick(object sender, EventArgs e)
        {
            View.m_menuAddDeflood.Enabled = Model.CurrentAddress != null;
            View.m_menuAddRugPickup.Enabled = Model.CurrentAddress != null;          
                           
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
            Model.AddNew(TaskTypeEnum.RugPickup, null);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastProject();
        }

        private void OnAddDefloodClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNew(TaskTypeEnum.Deflood, null);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastProject();
        }

        private void OnAddMiscellaneousClick(object sender, ItemClickEventArgs e)
        {
            View.m_treeTasks.BeginUpdate();
            Model.AddNew(TaskTypeEnum.Miscellaneous, null);
            View.m_treeTasks.EndUpdate();
            View.m_treeTasks.ExpandAll();
            SelectLastProject();
        }

        #endregion

        #region SelectLastProject

        private void SelectLastProject()
        {
            for (int i = Model.Tasks.Count - 1; i >= 0; i--)
            {
                if (Model.Tasks[i].IsProject)
                {
                    SelectTask(Model.Tasks[i]);
                    View.m_ctlProjectEdit.Focus();
                    return;
                }
            }
        }

        #endregion

        #region SelectLastTask

        private void SelectLastTask()
        {
            for (int i = Model.Tasks.Count - 1; i >= 0; i--)
            {
                if (!Model.Tasks[i].IsProject)
                {
                    SelectTask(Model.Tasks[i]);
                    View.m_ctlTaskEdit.Focus();
                    View.m_ctlTaskEdit.SetFocusToNotes();
                    return;
                }
            }
        }

        #endregion

        #region SelectTask

        private void SelectTask(TaskProjectWrapper task)
        {
            View.m_treeTasks.FocusedNode = View.m_treeTasks.FindNodeByKeyID(task.ID);
        }

        #endregion

        #region ApplyFilter

        private void OnTaskFilterChanged(object sender, EventArgs e)
        {
            ApplyFilter(View.m_chkShowFailed.Checked, 
                View.m_chkShowNotReady.Checked, View.m_chkShowCompleted.Checked);
        }

        private bool HasVisibleChildren(TreeListNode node)
        {
            if (!node.HasChildren)
                return false;
            foreach (TreeListNode childNode in node.Nodes)
            {
                TaskProjectWrapper task = (TaskProjectWrapper)View.m_treeTasks.GetDataRecordByNode(childNode);
                if (task.IsVisible)
                    return true;
                if (HasVisibleChildren(childNode))
                    return true;
            }

            return false;
        }


        private void ApplyFilter(TreeListNode node, bool showFailed, bool showNotReady, bool showCompleted)
        {
            TaskProjectWrapper task = (TaskProjectWrapper) View.m_treeTasks.GetDataRecordByNode(node);

            if (task == null)
                return;

            if (!task.IsProject)
            {
                if (task.IsExistInVisit)
                {
                    node.Visible = true;
                    task.IsVisible = true;                        
                } 
                else if (showFailed && task.IsFailed)
                {
                    node.Visible = true;
                    task.IsVisible = true;
                }
                else if (showNotReady && task.IsNotReady)
                {
                    node.Visible = true;
                    task.IsVisible = true;
                }
                else if (showCompleted && task.IsCompleted)
                {
                    node.Visible = true;
                    task.IsVisible = true;
                }
                else if (task.IsFailed || task.IsNotReady || task.IsCompleted)
                {
                    node.Visible = false;
                    task.IsVisible = false;
                }
                else
                {
                    node.Visible = true;
                    task.IsVisible = true;
                }                       
            }

            if (node.HasChildren)
                foreach (TreeListNode childNode in node.Nodes)
                    ApplyFilter(childNode, showFailed, showNotReady, showCompleted);                                    

         
            if (task.IsVisible && node.ParentNode != null)
            {
                TaskProjectWrapper parentTask = (TaskProjectWrapper) View.m_treeTasks.GetDataRecordByNode(node.ParentNode);
                if (!parentTask.IsProject && !parentTask.IsVisible)
                {
                    node.ParentNode.Visible = true;
                    parentTask.IsVisible = true;
                }
            }

        }

        private void ApplyFilter(bool showFailed, bool showNotReady, bool showCompleted)
        {
            foreach (TreeListNode node in View.m_treeTasks.Nodes)
            {
                ApplyFilter(node, showFailed, showNotReady, showCompleted);
            }            

            foreach (TreeListNode node in View.m_treeTasks.Nodes)
            {
                TaskProjectWrapper task = (TaskProjectWrapper)View.m_treeTasks.GetDataRecordByNode(node);
                if (task != null && task.IsProject)
                {
                    node.Visible = HasVisibleChildren(node);
                    task.IsVisible = node.Visible;
                }
                    
            }
        }

        #endregion

        #region IsValid

        private bool IsValid()
        {
            View.ValidateChildren();
            
            if (View.m_errorProvide.HasErrors)
                return false;
            if (View.m_ctlVisitHeader.HasErrors)
                return false;

            if (Model.IsReschedule)
            {
                DateTime? oldServiceDate = Model.ExistingVisit.ServiceDate;
                if (oldServiceDate.HasValue)
                    oldServiceDate = oldServiceDate.Value.Date;

                DateTime? newServiceDate = null;
                if (View.m_ctlVisitHeader.m_dtpServiceDate.EditValue != null)
                    newServiceDate = View.m_ctlVisitHeader.m_dtpServiceDate.DateTime.Date;

                if (!(oldServiceDate == null && newServiceDate == null)
                    && oldServiceDate == newServiceDate)
                {
                    XtraMessageBox.Show("Please select different service date in order to reschedule visit",
                                        "Same service date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    View.m_tabs.SelectedTabPageIndex = 0;
                    return false;                                        
                }                
            }

            OnTasksFocusedNodeChanged(null, 
                new FocusedNodeChangedEventArgs(View.m_treeTasks.FocusedNode, View.m_treeTasks.FocusedNode));            

            if (Model.GetTasksToBeAddedToVisit().Count == 0)
            {
                if (Model.ExistingVisit != null)
                {
                    if (XtraMessageBox.Show("There is no tasks on Visit. Would you like to delete this visit?", "No Tasks", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        View.m_tabs.SelectedTabPageIndex = 1;
                        return false;                        
                    }
                    
                } else
                {
                    XtraMessageBox.Show("Please add Visit Tasks", "No Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    View.m_tabs.SelectedTabPageIndex = 1;
                    return false;                    
                }
            }

            TaskProjectWrapper invalidProject = Model.FindNotValidProject();
            if (invalidProject != null)
            {
                View.m_tabs.SelectedTabPageIndex = 1;
                SelectTask(invalidProject);
                View.m_ctlProjectEdit.ValidateChildren();
                return false;
            }
                
            return true;
        }

        #endregion

        #region OnCustomerChanged

        private void OnCustomerChanged(Customer customer, Address address)
        {
            View.m_ctlAddressLookup.Customer = customer;
            View.m_ctlAddressLookup.BaseAddress = address;
            View.m_ctlAddressLookup.CurrentAddress = address;
            View.m_ctlAddressLookup.IsBaseAddressActive = true;
            View.m_ctlAddressLookup.BaseAddressName = "Customer Address";
            Model.CurrentAddress = address;
            RefreshTasks();

            Model.RefreshHistoryOrders(customer);
            if (Model.HistoryOrders.Count == 0)
            {
                View.m_gridHistoryOrders.Visible = false;
            } else
            {
                View.m_gridHistoryOrders.Visible = true;
                View.m_gridHistoryOrders.DataSource = Model.HistoryOrders;
            }
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

        #region OnAddressModified

        private void OnAddressModified(Address baseAddress, Address currentAddress, bool isBaseAddressActive)
        {
            int oldAddressId = Model.CurrentAddress.ID;
            Model.CurrentAddress = isBaseAddressActive ? baseAddress : currentAddress;
            if (oldAddressId != Model.CurrentAddress.ID)
                RefreshTasks();
        }

        #endregion

        #region RefreshTasks

        private void RefreshTasks()
        {
            View.m_treeTasks.ClearNodes();
            Model.RefreshTasks();
            View.m_treeTasks.DataSource = Model.Tasks;
            View.m_treeTasks.ExpandAll();
            if (View.m_treeTasks.Nodes.Count > 0)
                View.m_treeTasks.FocusedNode = View.m_treeTasks.Nodes[0];
            OnTaskFilterChanged(null, null);
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            Visit visit;

            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime? existingVisitServiceDate = null;

            if (Model.ExistingVisit != null)
            {
                visit = (Visit)Model.ExistingVisit.Clone();
                if (Model.ExistingVisit.ServiceDate.HasValue)
                    existingVisitServiceDate = new DateTime(Model.ExistingVisit.ServiceDate.Value.Year, 
                        Model.ExistingVisit.ServiceDate.Value.Month,Model.ExistingVisit.ServiceDate.Value.Day);
            }
            else
            {
                visit = new Visit(0,
                    (int)VisitStatusEnum.Pending,
                    now,
                    null, null, null, null, null, null, string.Empty,
                    null, null, null, false, false, false, decimal.Zero, null, false);
            }

            if (View.m_ctlVisitHeader.Visit.ServiceDate.HasValue)
                visit.ServiceDate = new DateTime(View.m_ctlVisitHeader.Visit.ServiceDate.Value.Year,
                    View.m_ctlVisitHeader.Visit.ServiceDate.Value.Month, View.m_ctlVisitHeader.Visit.ServiceDate.Value.Day);
            else
                visit.ServiceDate = null;
            
            visit.Notes = View.m_ctlVisitHeader.Visit.Notes;
            visit.PreferedTimeFrom = View.m_ctlVisitHeader.Visit.PreferedTimeFrom;
            visit.PreferedTimeTo = View.m_ctlVisitHeader.Visit.PreferedTimeTo;

            IsPromptForServiceDateEnum serviceDatePromptType = Model.IsPromptForServiceDate(visit); 
            if (serviceDatePromptType != IsPromptForServiceDateEnum.NoPrompt)
            {
                string message = String.Empty;
                if (serviceDatePromptType == IsPromptForServiceDateEnum.MonitoringPrompt)
                    message = "Visit contains Monitoring but Service Date is not set.  Proceed?";
                if (serviceDatePromptType == IsPromptForServiceDateEnum.DefloodPrompt)
                    message = "Visit contains Deflood but Service Date is not set.  Proceed?";

                if (XtraMessageBox.Show(message, "Service Date Check",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    View.m_tabs.SelectedTabPageIndex = 0;
                    View.m_ctlVisitHeader.m_dtpServiceDate.Focus();
                    return;
                }
            }

            if (View.m_ctlCustomerLookup.Customer != null)
            {
                if (string.IsNullOrEmpty(View.m_ctlCustomerLookup.Customer.Email) && Model.ExistingVisit == null)
                {
                    if (XtraMessageBox.Show("Customer Email Is Missing.\n Would you like to enter it?", "Customer Email Warning",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        View.m_tabs.SelectedTabPageIndex = 0;
                        View.m_ctlCustomerLookup.Focus();
                        return;
                    }
                }

                visit.CustomerId = View.m_ctlCustomerLookup.Customer.ID;
            }
            else
                visit.CustomerId = null;

            if (Model.CurrentAddress != null)
                visit.ServiceAddressId = Model.CurrentAddress.ID;
            else
                visit.ServiceAddressId = null;

            CreateVisitResultEnum result;
            try
            {      
                Database.Begin();

                result = Model.CreateVisit(visit);

                if (
                    (visit.ServiceDate.HasValue && visit.ServiceDate.Value == now) ||
                    (existingVisitServiceDate.HasValue && existingVisitServiceDate.Value == now && visit.ServiceDate.HasValue && visit.ServiceDate.Value != now)
                   )
                {
                    PendingTaskGridState.MakePendingTaskGridDirty();
                }

                Database.Commit();
                if (Model.ExistingVisit != null)
                    Host.TraceUserAction("Edit Visit " + visit.ID);
                else
                    Host.TraceUserAction("Create Visit " + visit.ID);

                m_affectedVisit = visit;                
            }
            catch (CreateVisitException ex)
            {
                Database.Rollback();
                View.m_tabs.SelectedTabPageIndex = 1;
                XtraMessageBox.Show(
                    ex.Message, "Unable to edit Visit",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);                                
                return;                
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            if (result == CreateVisitResultEnum.PrintVisit)
            {
                try
                {
                    using (new WaitCursor())
                    {
                        VisitSummaryPackage summaryPackage = new VisitSummaryPackage(visit,Model.IsReschedule);
                        summaryPackage.Print();
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

        protected override bool OnCancel()
        {
            if (!Model.IsReadOnly && Model.ExistingVisit == null)
            {
                if (XtraMessageBox.Show("Do you want to cancel?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    m_isCancelled = true;
                    return false;
                }
                
                return true;
            }

            m_isCancelled = true;
            return false;
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

    }
}
