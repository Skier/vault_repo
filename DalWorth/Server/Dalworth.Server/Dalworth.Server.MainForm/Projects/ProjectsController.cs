using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CreateProject;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Database=Dalworth.Server.Data.Database;
using Task=Dalworth.Server.Domain.Task;
using Dalworth.Server.MainForm.CustomerLookup;
using Dalworth.Server.MainForm.ProjectCustomerHistory;


namespace Dalworth.Server.MainForm.Projects
{
    public class ProjectsController : NestedController<ProjectsModel, ProjectsView>
    {
        private bool m_disableAutoFilter = false;
        private MainFormController m_mainFormController;

        #region FocusedProject

        private ProjectWrapper FocusedProject
        {
            get
            {                               
                object row = View.m_gridViewProjects.GetRow(View.m_gridViewProjects.FocusedRowHandle);
                if (row != null)
                    return (ProjectWrapper)row;
                return null;
            }
        }

        #endregion

        #region SelectedProjects

        private List<ProjectWrapper> SelectedProjects
        {
            get
            {
                List<ProjectWrapper> result = new List<ProjectWrapper>();

                int[] selectedRows = View.m_gridViewProjects.GetSelectedRows();
                if (selectedRows != null)
                {
                    foreach (int rowHandle in selectedRows)
                    {
                        result.Add((ProjectWrapper)View.m_gridViewProjects.GetRow(rowHandle));
                    }
                }

                return result;
            }
        }

        #endregion

        #region SelectedTask

        private TaskWrapperOnProjectsScreen SelectedTask
        {
            get
            {
                int[] selectedRows = View.m_gridViewTasks.GetSelectedRows();
                if (selectedRows == null || selectedRows.Length == 0)
                    return null;
                return (TaskWrapperOnProjectsScreen)View.m_gridViewTasks.GetRow(selectedRows[0]);
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
            View.m_btnCreateProject.Click += OnCreateProjectClick;
            View.m_btnEditProject.Click += OnEditProjectClick;
            View.m_btnPrintProjects.Click += OnPrintProjectsClick;
            View.m_menuPrintTicket.ItemClick += OnPrintTicketClick;
            View.m_menuPrintLeadSheet.ItemClick += OnPrintLeadSheetClick;

            View.m_gridViewProjects.FocusedRowChanged += OnProjectsFocusedRowChanged;            
            View.m_gridViewProjects.ColumnFilterChanged += OnProjectsColumnFilterChanged;

            View.m_repositoryItemVisitLink.Click += OnLinkVisitClick;
            View.m_repositoryItemDashboard.Click += OnLinkDashboardClick;

            View.m_txtServmanTicketNumber.TextChanged += OnTicketNumberChanged;

            View.m_cmbStatus.SelectedIndexChanged += OnFiltersChanged;
            View.m_cmbType.SelectedIndexChanged += OnFiltersChanged;
            View.m_dateRange.DateRangeValueChanged += OnFiltersChanged;
            View.m_cmbProjectManager.SelectedIndexChanged += OnFiltersChanged;

            View.m_txtProjectId.KeyDown += OnFieldsKeyDown;
            View.m_txtPhoneNo.KeyDown += OnFieldsKeyDown;
            View.m_txtCustomer.KeyDown += OnFieldsKeyDown;
            View.m_txtAddress.KeyDown += OnFieldsKeyDown;
            View.m_txtServmanTicketNumber.KeyDown += OnFieldsKeyDown;
            
            View.m_btnClear.Click += OnClearClick;
            View.m_btnRefresh.Click += OnRefreshClick;
            View.m_ctlCustomerEdit.Modified += OnCustomerModified;
            View.m_ctlAddressEdit.Modified += OnAddressModified;

            View.m_gridViewTasks.KeyDown += OnGridViewTasksKeyDown;
            View.m_gridViewTasks.FocusedRowChanged += OnTasksFocusedRowChanged;
            View.m_linkProjectId.Click += OnProjectIdClick;
            View.m_gridViewProjects.DoubleClick += OnGridProjectsDoubleClick;
            View.m_gridViewProjects.KeyDown += OnGridProjectsKeyDown;

            View.m_gridProjects.DataSource = Model.Projects;
            View.m_txtCustomer.Focus();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            foreach (Employee projectManager in Model.ProjectManagers)
            {
                View.m_cmbProjectManager.Properties.Items.Add(
                    new ImageComboBoxItem(projectManager.DisplayName, (object)projectManager.ID));
            }

            View.m_ctlAddressEdit.BaseAddressName = "Customer address";
            View.m_ctlAddressEdit.Caption = "Project Address";
        }

        #endregion        

        #region OnGridViewTasksKeyDown

        private void OnGridViewTasksKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridTasks.Focused)
            {
                TaskWrapperOnProjectsScreen task = SelectedTask;
                if (task != null)
                {
                    if (View.m_gridViewTasks.FocusedColumn.Name == View.m_colLinkVisit.Name)
                        OnLinkVisitClick(null, null);
                    else if (View.m_gridViewTasks.FocusedColumn.Name == View.m_colLinkDashboard.Name)
                        OnLinkDashboardClick(null, null);
                }
            }
        }

        #endregion

        #region SelectProject

        public void SelectProject(Project project, bool singleProject)
        {
            if (project == null)
            {
                ProjectWrapper prevFocusedProject = FocusedProject;
                RefreshData();
                if (prevFocusedProject != null)
                    TryToSelectProject(prevFocusedProject.Project);
                return;                
            }

            if (singleProject)
                RefreshData(project);
            else
                RefreshData();

            TryToSelectProject(project);
        }

        private bool TryToSelectProjectExact(Project project)
        {
            for (int rowIndex = 0; rowIndex < Model.Projects.Count; rowIndex++)
            {
                if (Model.Projects[rowIndex].Project.ID == project.ID)
                {
                    int rowHandle = View.m_gridViewProjects.GetRowHandle(rowIndex);
                    if (rowHandle >= 0)
                    {
                        View.m_gridViewProjects.ClearSelection();
                        View.m_gridViewProjects.FocusedRowHandle = rowHandle;
                        View.m_gridViewProjects.SelectRow(rowHandle);
                        return true;
                    }
                }
            }

            return false;
        }

        private void TryToSelectProject(Project project)
        {
            if (!TryToSelectProjectExact(project))
            {
                if (Model.Projects.Count > 0)
                    TryToSelectProject(Model.Projects[0].Project);                            
            }
        }

        #endregion

        #region Link Click handlers

        private void OnLinkVisitClick(object sender, EventArgs e)
        {
            Visit visit = FindVisitByTask();
            if (visit != null)
                m_mainFormController.ShowVisitsForm(visit);
        }

        private void OnLinkDashboardClick(object sender, EventArgs e)
        {
            Visit visit = FindVisitByTask();
            if (visit != null)
                m_mainFormController.ShowDashboard(visit);
        }

        private Visit FindVisitByTask()
        {
            Visit visit = null;

            try
            {
                visit = Model.FindVisit(SelectedTask);
            }
            catch (DataNotFoundException)
            {
                XtraMessageBox.Show("This task is not included in any Visit", "No Visit", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            return visit;
        }

        #endregion

        #region OnProjectsColumnFilterChanged

        private void OnProjectsColumnFilterChanged(object sender, EventArgs e)
        {
            OnProjectsFocusedRowChanged(null, null);
        }

        #endregion

        #region OnProjectsFocusedRowChanged

        private void OnProjectsFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ProjectWrapper project = FocusedProject;
            if (project == null)
            {
                View.m_gridTasks.DataSource = null;

                View.m_ctlCustomerEdit.Customer = null;
                View.m_ctlCustomerEdit.Address = null;

                View.m_ctlAddressEdit.Customer = null;
                View.m_ctlAddressEdit.BaseAddress = null;
                View.m_btnEditProject.Enabled = false;
                View.m_btnPrintProjects.Enabled = false;
            } else
            {                
                View.m_gridTasks.DataSource = Task.FindTaskWrappersOnProjectsScreen(project.Project);

                View.m_ctlCustomerEdit.Customer = project.Customer;
                View.m_ctlCustomerEdit.Address = project.CustomerAddress;

                View.m_ctlAddressEdit.Customer = project.Customer;
                View.m_ctlAddressEdit.BaseAddress = project.CustomerAddress;
                View.m_ctlAddressEdit.CurrentAddress = project.ServiceAddress;

                if (project.ServiceAddress != null && project.CustomerAddress != null 
                    && project.ServiceAddress.ID == project.CustomerAddress.ID)
                {
                    View.m_ctlAddressEdit.IsBaseAddressActive = true;
                }                    
                else
                    View.m_ctlAddressEdit.IsBaseAddressActive = false;                

                View.m_btnEditProject.Enabled =
                    project.ProjectType == ProjectTypeEnum.Construction
                    || project.ProjectType == ProjectTypeEnum.Content;

                View.m_btnPrintProjects.Enabled = true;
            }

            ReshowNotes();
        }

        #endregion

        #region OnTasksFocusedRowChanged

        private void OnTasksFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            TaskWrapperOnProjectsScreen task = SelectedTask;
            if (task == null)
                View.m_txtTaskNotes.Text = string.Empty;
            else
                View.m_txtTaskNotes.Text = task.Task.Notes;
        }

        private void ReshowNotes()
        {
            TaskWrapperOnProjectsScreen task = SelectedTask;
            if (task == null)
            {
                ProjectWrapper project = FocusedProject;

                if (project != null && (project.ProjectType == ProjectTypeEnum.Construction
                        || project.ProjectType == ProjectTypeEnum.Content))
                {
                    View.m_txtTaskNotes.Text = project.Description;
                } else
                    View.m_txtTaskNotes.Text = string.Empty;
            }                
            else
                View.m_txtTaskNotes.Text = task.Task.Notes;
        }

        #endregion

        #region OnTicketNumberChanged

        private void OnTicketNumberChanged(object sender, EventArgs e)
        {
            if (View.m_txtServmanTicketNumber.Text.Length < 6)
                return;
            else
                OnFiltersChanged(sender, e);
        }

        #endregion

        #region OnFieldsKeyDown

        private void OnFieldsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            else
                OnFiltersChanged(sender, e);
        }

        #endregion

        #region OnFiltersChanged

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            if (m_disableAutoFilter)
                return;

            ProjectWrapper selectedProject = FocusedProject;

            RefreshData();
           
            if (selectedProject != null)
                TryToSelectProject(selectedProject.Project);
        }

        #endregion

        #region RefreshData

        private void RefreshData(Project project = null)
        {
            string jobNumber;
            if (project != null)
                jobNumber = project.ID.ToString();
            else
                jobNumber = View.m_txtProjectId.Text;

            string firstName = Utils.ParseFirstName(View.m_txtCustomer.Text);
            string lastName = Utils.ParseLastName(View.m_txtCustomer.Text);

            string city = Utils.ParseCity(View.m_txtAddress.Text);
            string zip = Utils.ParseZip(View.m_txtAddress.Text);
            string block = Utils.ParseBlock(View.m_txtAddress.Text);
            string street = Utils.ParseStreet(View.m_txtAddress.Text);
            string phoneNo = Utils.ExtractDigits(View.m_txtPhoneNo.Text);

            Model.UpdateProjects(jobNumber, View.m_txtServmanTicketNumber.Text,
                firstName, lastName, phoneNo, 
                city, zip, street, block, 
                View.m_cmbStatus.SelectedIndex == 0 ? null
                     : (ProjectStatusEnum?)View.m_cmbStatus.SelectedIndex,
                View.m_cmbType.SelectedIndex == 0 ? null
                     : (ProjectTypeEnum?)View.m_cmbType.SelectedIndex,
                View.m_dateRange.EditValue,
                View.m_cmbProjectManager.SelectedIndex == 0 ? null
                     : (int?)View.m_cmbProjectManager.EditValue);

            View.m_gridProjects.DataSource = Model.Projects;
            OnProjectsFocusedRowChanged(null, null);
        }

        #endregion        

        #region OnRefreshClick

        private void OnRefreshClick(object sender, EventArgs e)
        {
            ProjectWrapper selectedProject = FocusedProject;
            RefreshData();
            if (selectedProject != null)
                TryToSelectProject(selectedProject.Project);
        }

        #endregion

        #region OnClearClick

        private void ClearFilters()
        {
            m_disableAutoFilter = true;
            View.m_txtProjectId.Text = string.Empty;            
            View.m_txtPhoneNo.Text = string.Empty;
            View.m_txtCustomer.Text = string.Empty;
            View.m_txtAddress.Text = string.Empty;
            View.m_cmbStatus.SelectedIndex = 0;
            View.m_cmbType.SelectedIndex = 0;
            View.m_txtServmanTicketNumber.Text = string.Empty;
            View.m_dateRange.Clear();
            View.m_cmbProjectManager.SelectedIndex = 0;
            m_disableAutoFilter = false;            
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            ClearFilters();
            RefreshData();
        }

        #endregion

        #region OnCustomerModified

        private void OnCustomerModified(Customer customer, Address address)
        {
            try
            {
                Database.Begin();
                customer.Modified = DateTime.Now;
                address.Modified = DateTime.Now;
                Customer.Update(customer);
                Address.Update(address);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            ProjectWrapper project = FocusedProject;

            if (project.ServiceAddress.ID == project.CustomerAddress.ID)
                project.ServiceAddress = (Address)project.CustomerAddress.Clone();
            Model.Projects.ResetBindings();
            OnProjectsFocusedRowChanged(null, null);            
        }

        #endregion

        #region OnAddressModified

        private void OnAddressModified(Address baseAddress, Address currentAddress, bool isBaseAddressActive)
        {
            ProjectWrapper selectedProject = FocusedProject;
            try
            {
                Database.Begin();                
                
                if (isBaseAddressActive)
                {
                    selectedProject.Project.ServiceAddressId = baseAddress.ID;
                    selectedProject.ServiceAddress = (Address)baseAddress.Clone();
                    Project.UpdateAndLog(selectedProject.Project);
//                    if (currentAddress.ID != baseAddress.ID)
//                    {
//                        CustomerAddressAdditional.Delete(
//                            new CustomerAddressAdditional(
//                            selectedProject.Project.CustomerId,
//                            currentAddress.ID));
//                        Address.Delete(currentAddress);
//                    }
                        
                }
                else
                {
                    selectedProject.ServiceAddress = currentAddress;
                    selectedProject.Project.ServiceAddressId = selectedProject.ServiceAddress.ID;
                    Project.UpdateAndLog(selectedProject.Project);
                }
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            Model.Projects.ResetBindings();
            OnProjectsFocusedRowChanged(null, null);            
        }


        #endregion

        #region OnCreateProjectClick

        private void OnCreateProjectClick(object sender, EventArgs e)
        {
            CreateProject();           
        }

        private void CreateProject()
        {
            CustomerAndAddress customer;
            using (CustomerLookupController controller = Prepare <CustomerLookupController>())
            {
                controller.Execute(false);
                if (!controller.IsCustomerSelected)
                    return;

                customer = controller.Customer;
            }

            BindingList <ProjectWrapper> recentProjects = Model.FindRecentProjects(customer.Customer.ID);

            ProjectWrapper selectedProject = null;

            if (recentProjects.Count > 0)
            {
                using (ProjectCustomerHistoryController controller = Prepare<ProjectCustomerHistoryController>(customer, recentProjects))
                {
                    controller.Execute(false);
                    
                    if (controller.IsCancelled)
                        return;

                    if (controller.SelectedProject != null)
                        selectedProject = controller.SelectedProject;
                }
                
            }

            using (CreateProjectController controller = Prepare<CreateProjectController>(customer, selectedProject))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                RefreshData();
                if (!TryToSelectProjectExact(controller.CreatedProject))
                    SelectProject(controller.CreatedProject, true);
                
            }                                           
        }

        #endregion

        #region OnEditProjectClick

        private void OnEditProjectClick(object sender, EventArgs e)
        {
            ProjectWrapper project = FocusedProject;

            if (project == null)
            {
                XtraMessageBox.Show("Please select Project to edit", "No Project selected", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;                
            }

            if (project.Project.ProjectType != ProjectTypeEnum.Construction
                && project.Project.ProjectType != ProjectTypeEnum.Content
                && project.Project.ProjectType != ProjectTypeEnum.BasementSystems)
            {
                return;
            }

            using (CreateProjectController controller = Prepare<CreateProjectController>(null, project))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                RefreshData();
                if (!TryToSelectProjectExact(project.Project))
                    SelectProject(project.Project, true);
            }

            View.m_btnEditProject.Focus();
        }

        #endregion

        #region OnProjectIdClick

        private void OnProjectIdClick(object sender, EventArgs e)
        {
            ProjectWrapper project = FocusedProject;
            if (project.Project.ProjectType != ProjectTypeEnum.Construction
                && project.Project.ProjectType != ProjectTypeEnum.Content
                && project.Project.ProjectType != ProjectTypeEnum.BasementSystems
                )
            {
                XtraMessageBox.Show(project.Project.ProjectTypeText + " project cannot be edited", 
                    "Unable to Edit project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OnEditProjectClick(null, null);
        }

        private void OnGridProjectsDoubleClick(object sender, EventArgs e)
        {
            GridHitInfo hitInfo = View.m_gridViewProjects.CalcHitInfo(
                View.m_gridProjects.PointToClient(Cursor.Position));            

            if (hitInfo.InRow)
                OnEditProjectClick(null, null);
        }

        private void OnGridProjectsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridProjects.Focused && Model.Projects.Count > 0)
                OnEditProjectClick(null, null);            
        }

        #endregion

        #region OnPrintProjectsClick

        private void OnPrintProjectsClick(object sender, EventArgs e)
        {
            List<ProjectWrapper> selectedProjects = SelectedProjects;

            foreach (ProjectWrapper project in selectedProjects)
            {
                if (project.Project.ProjectType != ProjectTypeEnum.Construction
                    && project.Project.ProjectType != ProjectTypeEnum.Content)
                {
                    XtraMessageBox.Show("It is allowed to print Construction or Content projects only. Please select corresponding projects to print", "Unable to print projects", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            View.m_menuPrintProjects.ShowPopup(
                View.m_btnPrintProjects.PointToScreen(new Point(-122, 0)));

            for (int i = 0; i < View.m_menuPrintProjects.ItemLinks.Count; i++)
            {
                if (View.m_menuPrintProjects.ItemLinks[i].Enabled)
                {
                    View.m_menuPrintProjects.ItemLinks[i].Focus();
                    break;
                }
            }
        }

        #endregion

        #region OnPrintTicketClick

        private void OnPrintTicketClick(object sender, ItemClickEventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    Model.Print(SelectedProjects);
                }
                catch (Exception ex)
                {
                      XtraMessageBox.Show(ex.Message, "Unable to print report",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region OnPrintLeadSheetClick

        private void OnPrintLeadSheetClick(object sender, ItemClickEventArgs e)
        {
            LeadSheet report;

            using (new WaitCursor())
            {
                report = new LeadSheet();
                report.SetDataSource(Model.GetPrintedTickets(SelectedProjects));
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;
            }

            try
            {
                report.PrintToPrinter(0, false, 1, 0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Unable to print report",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}
