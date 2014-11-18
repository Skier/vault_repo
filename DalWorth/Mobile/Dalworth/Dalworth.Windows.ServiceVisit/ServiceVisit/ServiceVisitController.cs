using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Dalworth.Windows.ServiceVisit.ItemEdit;
using Dalworth.Windows.ServiceVisit.NoGo;
using Dalworth.Windows.ServiceVisit.SubmitEtc;
using Dalworth.Windows.ServiceVisit.ViewTask;
using Dalworth.Windows.ServiceVisit.VisitReceipt;
using Microsoft.WindowsMobile.Telephony;
using Application=Dalworth.Domain.Application;
using Item=Dalworth.Domain.Item;
using Project=Dalworth.Domain.SyncService.Project;
using Task=Dalworth.Domain.SyncService.Task;
using TaskEquipmentCapture=Dalworth.Domain.SyncService.TaskEquipmentCapture;
using TaskItemDelivery=Dalworth.Domain.SyncService.TaskItemDelivery;
using TaskItemRequirement=Dalworth.Domain.SyncService.TaskItemRequirement;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.ServiceVisit
{
    public class ServiceVisitController : SingleFormController<ServiceVisitModel, ServiceVisitView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Visit = (VisitPackage)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_menuSubmitETC.Click += OnSubmitETcClick;
            View.m_menuNoGo.Click += OnNoGoClick;
            View.m_menuComplete.Click += OnCompleteClick;
            View.m_menuCompleteByDispatch.Click += OnCompleteByDispatchClick;

            View.m_tblTasks.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_tblTasks.SelectionChanged += new SelectionHandler(OnTableSelectionChanged);
            View.m_tabs.SelectedIndexChanged += OnTabChanged;

            View.m_menuAddTaskRugPickup.Click += OnAddTaskRugPickupClick;
            View.m_menuDeleteTask.Click += OnDeleteTaskClick;
            View.m_menuViewTask.Click += OnViewTaskClick;

            View.m_linkPhone1.Click += OnPhoneLinkClick;
            View.m_linkPhone2.Click += OnPhoneLinkClick;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            PopulateTextInfo();

            IsRightActionExist = true;
            RightActionName = "Menu";

            if (!Model.IsRugDelivery)
                View.m_menuCompleteByDispatch.Enabled = false;

            View.m_tblTasks.AddColumn(new TableColumn(0));
            View.m_tblTasks.AddColumn(new TableColumn(1, 60));
            View.m_tblTasks.BindModel(Model);

            OnTabChanged(null, EventArgs.Empty);
            UpdateTasksTotal();
            View.m_tabs.SelectedIndex = 0;
        }

        #endregion

        #region PopulateTextInfo

        private void PopulateTextInfo()
        {
            View.m_lblDate.Text = DateTime.Now.ToString("ddd, MMM dd yyyy");
            View.m_lblCustomerName.Text = Model.Visit.Customer.FirstName + ", " + Model.Visit.Customer.LastName;

            if (Model.Visit.Customer.Phone1 != string.Empty)
                View.m_linkPhone1.Text = Model.Visit.Customer.Phone1;
            else
                View.m_linkPhone1.Visible = false;

            if (Model.Visit.Customer.Phone2 != string.Empty)
                View.m_linkPhone2.Text = Model.Visit.Customer.Phone2;
            else
                View.m_linkPhone2.Visible = false;

            if (Model.Visit.ServiceAddress != null && Model.Visit.ServiceAddress.Map != string.Empty)
                View.m_lblMap.Text = "MAP: " + Model.Visit.ServiceAddress.Map;
            else
                View.m_lblMap.Visible = false;

            if (Model.Visit.ServiceAddress != null)
            {
                View.m_txtAddress.Text = Model.Visit.ServiceAddress.Address1 + "\r\n"
                     + Model.Visit.ServiceAddress.City + ", "
                     + Model.Visit.ServiceAddress.State + ", "
                     + Model.Visit.ServiceAddress.Zip;
            }
            else
            {
                View.m_txtAddress.Text = string.Empty;
            }

            View.m_lblTaskType.Text = TaskType.GetText((TaskTypeEnum)Model.Visit.Tasks[0].Task.TaskTypeId);
            View.m_lblTaskNumber.Text = "TSK: " + Model.Visit.Tasks[0].Task.Number;
            View.m_txtNotes.Text = Model.Visit.Visit.Notes;
            View.m_txtMessage.Text = Model.Visit.Tasks[0].Task.Message;
            View.m_txtNotes.Text = Model.Visit.Tasks[0].Task.Message;            
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {
            View.m_tabs.Focus();
        }

        #endregion

        #region OnPhoneLinkClick

        private void OnPhoneLinkClick(object sender, EventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            if (linkLabel.Visible == false || linkLabel.Text == string.Empty)
                return;

            string customerName = Model.Visit.Customer.FirstName + ", " + Model.Visit.Customer.LastName;
            
            if (MessageDialog.Show(MessageDialogType.Question, 
                string.Format("Do you want to call {0} at {1}?", 
                customerName, linkLabel.Text)) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone phone = new Phone();
                phone.Talk(linkLabel.Text + "\0", false);
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Warning, "Cannot make a call, please check your phone settings");
            }
            
        }

        #endregion

        #region OnNoGoClick

        private void OnNoGoClick(object sender, EventArgs e)
        {
            NoGoController controller = Prepare<NoGoController>(Model.Visit);
            controller.Closed += OnNoGoClosed;
            controller.Execute();
        }

        private void OnNoGoClosed(SingleFormController controller)
        {
            NoGoController noGoController = (NoGoController) controller;
            if (!noGoController.IsCancelled)
                View.Destroy();
        }

        #endregion

        #region OnSubmitETcClick

        private void OnSubmitETcClick(object sender, EventArgs e)
        {
            SubmitEtcController controller = Prepare<SubmitEtcController>(Model.Visit);
            controller.Execute();
        }

        #endregion

        #region OnCompleteClick

        private void OnCompleteClick(object sender, EventArgs e)
        {
            foreach (TaskPackage task in Model.Visit.Tasks)
            {
                if (task.Task.TaskTypeId == (int) TaskTypeEnum.RugPickup
                    && task.Items.Length == 0)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Please enter captured Rugs");
                    View.m_tabs.SelectedIndex = 2;
                    return;
                }
            }

            if (Model.IsRugPickup || Model.IsUnknownTaskType)
            {
                if (MessageDialog.Show(MessageDialogType.Question, "Complete Visit?") == DialogResult.No)
                    return;

                try
                {
                    using (new WaitCursor())
                    {
                        Database.Begin();
                        Model.CompleteVisit(null);
                        Database.Commit();
                        WorkTransaction.Send();
                        View.Destroy();
                    }
                }
                catch (Exception ex)
                {
                    Database.Rollback();
                    MessageDialog.Show(MessageDialogType.Warning,
                            "Unknown application error. Please contact dispatch");
                    Host.Trace("ServiceVisitController::OnCompleteClick", ex.Message + ex.StackTrace);
                    return;
                }
            }
            else // Rug Delivery
            {
                VisitReceiptController controller = Prepare<VisitReceiptController>(Model);
                controller.Closed += OnVisitReceiptClosed;
                controller.Execute();
            }

        }

        private void OnVisitReceiptClosed(SingleFormController controller)
        {
            VisitReceiptController receiptController = (VisitReceiptController) controller;
            if (!receiptController.IsCancelled)
                View.Destroy();
        }

        #endregion

        #region OnCompleteByDispatchClick

        private void OnCompleteByDispatchClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question, "Please make sure you have provided all the necessary information to Dispatch. Complete Visit by Dispatch?") == DialogResult.No)
                return;

            try
            {
                using (new WaitCursor())
                {
                    Database.Begin();
                    Domain.Visit.Complete(Configuration.CurrentTechnicianId, Model.Visit, null, true);                
                    Database.Commit();
                    View.Destroy();
                }
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("ServiceVisitController::OnCompleteByDispatchClick", ex.Message + ex.StackTrace);
                return;
            }            
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return false;
        }

        #endregion        

        #region OnTableSelectionChanged

        private void OnTableSelectionChanged()
        {
            UpdateTasksTotal();
        }

        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            OnTabChanged(null, EventArgs.Empty);
        }

        #endregion

        #region UpdateTasksTotal

        private void UpdateTasksTotal()
        {
            decimal total = Model.GetTasksViewTotal();
            decimal subTotal = total/(1 + Application.TAX_PERCENT);

            View.m_lblVisitSubTotal.Text = subTotal.ToString("C");
            View.m_lblVisitTax.Text = (total - subTotal).ToString("C");
            View.m_lblVisitTotal.Text = total.ToString("C");
        }

        #endregion

        #region OnTabChanged

        private void OnTabChanged(object sender, EventArgs e)
        {
            View.m_menuAddTask.Enabled = true;
            View.m_menuDeleteTask.Enabled = false;            

            if (Model.IsUnknownTaskType || Model.IsRugDelivery)
            {
                View.m_menuAddTaskRugPickup.Enabled = true;
                foreach (TaskPackage task in Model.Visit.Tasks)
                {
                    if (task.Task.ID == 0) // Newly created rug pickup task exist
                    {
                        View.m_menuAddTaskRugPickup.Enabled = false;
                        break;
                    }
                }

            } else
            {
                View.m_menuAddTaskRugPickup.Enabled = false;
            }            

            if (View.m_tabs.SelectedIndex == 2 && View.m_tblTasks.Model.GetRowCount() > 0 
                && View.m_tblTasks.CurrentRowIndex >= 0) // Task selected
            {
                
                Task task = (Task)Model.GetObjectAt(View.m_tblTasks.CurrentRowIndex, 0);
                View.m_menuViewTask.Enabled = task.TaskTypeId != (int) TaskTypeEnum.Unknown;
                
                if (task.ID == 0) // Newly created task
                    View.m_menuDeleteTask.Enabled = true;            
            } else
            {
                View.m_menuViewTask.Enabled = false;
            }
        }

        #endregion

        #region OnAddTaskRugPickupClick

        private void OnAddTaskRugPickupClick(object sender, EventArgs e)
        {
            TaskPackage taskPackage = new TaskPackage();
            taskPackage.Items = new Domain.SyncService.Item[0];
            taskPackage.Project = new Project();
            taskPackage.Task = new Task();
            taskPackage.Task.CreateDate = DateTime.Now;
            taskPackage.Task.ServiceDate = DateTime.Now;
            taskPackage.Task.TaskStatusId = 1;
            taskPackage.Task.TaskTypeId = 1;

            taskPackage.TaskEquipmentCaptures = new TaskEquipmentCapture[0];
            taskPackage.TaskItemDeliveries = new TaskItemDelivery[0];
            taskPackage.TaskItemRequirements = new TaskItemRequirement[0];
            List<TaskPackage> tasks = new List<TaskPackage>(Model.Visit.Tasks);
            tasks.Add(taskPackage);
            Model.Visit.Tasks = tasks.ToArray();

            ViewTaskController controller = Prepare<ViewTaskController>(taskPackage);
            controller.Closed += OnAddTaskRugPickupClosed;
            controller.Execute();            
        }

        private void OnAddTaskRugPickupClosed(SingleFormController controller)
        {
            View.m_tblTasks.Select(Model.Visit.Tasks.Length - 1, 0);
            View.m_tblTasks.Focus();
            UpdateTasksTotal();
            OnTabChanged(null, EventArgs.Empty);            
        }

        #endregion

        #region OnDeleteTaskClick

        private void OnDeleteTaskClick(object sender, EventArgs e)
        {
            if (View.m_tblTasks.CurrentRowIndex >= 0)
            {
                if (MessageBox.Show("Do you want to delete this Task?", "Confirmation",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }

                List<TaskPackage> taskPackages = new List<TaskPackage>(Model.Visit.Tasks);
                taskPackages.RemoveAt(View.m_tblTasks.CurrentRowIndex);
                Model.Visit.Tasks = taskPackages.ToArray();
                if (Model.Visit.Tasks.Length > 0)
                    View.m_tblTasks.Select(0);
                View.m_tblTasks.Update();
                View.m_tblTasks.Focus();
                UpdateTasksTotal();
                OnTabChanged(null, EventArgs.Empty);
            }
        }

        #endregion

        #region OnViewTaskClick

        private void OnViewTaskClick(object sender, EventArgs e)
        {
            if (View.m_tblTasks.CurrentRowIndex >= 0)
            {
                ViewTaskController controller =
                    Prepare<ViewTaskController>(Model.Visit.Tasks[View.m_tblTasks.CurrentRowIndex]);
                controller.Closed += OnViewRugClosed;
                controller.Execute();                
            }
        }

        private void OnViewRugClosed(SingleFormController controller)
        {
            View.m_tblTasks.Focus();
        }

        #endregion
    }
}
