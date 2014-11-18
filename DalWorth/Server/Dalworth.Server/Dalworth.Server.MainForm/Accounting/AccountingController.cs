using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;

using Dalworth.Server.MainForm.AccountingCreditMemo;
using Dalworth.Server.MainForm.AccountingPayment;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using Dalworth.Server.MainForm.AccountingInvoiceEdit;
using Dalworth.Server.QuickBooks;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.CustomerLookup;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.MainForm.ProjectCustomerHistory;
using Dalworth.Server.MainForm.CreateProject;
using Dalworth.Server.MainForm.MainForm;

namespace Dalworth.Server.MainForm.Accounting
{
    internal delegate void ProcessQbCustomerSyncRequest(QbSyncActionEnum action, Object parameter);
    internal delegate void ShowDashboard(Visit visit);
    public delegate void CustomerProjectWrapperHandler(CustomerProjectWrapper customerProjectWrapper);

    public class AccountingController : NestedController<AccountingModel, AccountingView>
    {
        #region StatusRecord Class

        class StatusRecord
        {
            public StatusRecord(string action, string status, string description)
            {
                Action = action;
                Status = status;
                Description = description;
            }

            #region Action 

            private string m_action;
            public string Action
            {
                get { return m_action; }
                set { m_action = value; }
            }

            #endregion 
            
            #region Status 

            private string m_status;
            public string Status
            {
                get { return m_status; }
                set { m_status = value; }
            }

            #endregion 

            #region Description

            private string m_description;
            public string Description
            {
                get { return m_description; }
                set { m_description = value; }
            }

            #endregion 
        }

        #endregion 

        #region Private Properties

        private QbSync m_qbSync;
        private List<QbSyncRequest> m_syncRequests;
        private int m_currentSyncRequestIdx;
        private readonly BindingList<StatusRecord> m_statusRecords = new BindingList<StatusRecord>();
        private CustomerRequest m_ctlCustomerRequest;
        private InvoiceRequest m_ctlInvoiceRequest;
        private JobRequest m_ctlJobRequest;
        private CustomerInfo m_ctrlCustomerInfo;
        private ProjectInfo m_ctrlProjectInfo;
        private MainFormController m_mainFormController;
        
        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            m_mainFormController = (MainFormController) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_gridStatus.DataSource = m_statusRecords;
            InitializeCustomerProjectInfo();

            View.m_gridCustomersProjectsView.FocusedRowChanged += OnCustomersProjectsFocusedRowChanged;
            View.m_txtCustomer.KeyDown += OnFilterKeyDown;
            View.m_txtProjectId.KeyDown += OnFilterKeyDown;
            View.m_btnClear.Click += OnClearClick;
            View.m_btnRefresh.Click += OnFiltersChanged;
            View.m_txtBlock.KeyDown += OnFilterKeyDown;
            View.m_txtCity.KeyDown += OnFilterKeyDown;
            View.m_txtPhoneNo.KeyDown += OnFilterKeyDown;
            View.m_txtStreet.KeyDown += OnFilterKeyDown;
            View.m_txtZip.KeyDown += OnFilterKeyDown;
            View.m_gridTransactions.DoubleClick += OnTransactionsDoubleClick;
            View.m_btnCreateCustomer.Click += OnCreateCustomerClick;
            View.m_btnCreateProject.Click += OnCreateProjectClick;
            View.m_btnCreateVisit.Click += OnCreateVisitClick;
            View.m_txtCustomerLookup.KeyDown += OnLookup;
            View.m_btnMissingCustomers.Click += OnMissingCustomersClick;

            View.m_txtDaysMissing.Text = @"31";

            var employee = Employee.FindByPrimaryKey(Configuration.CurrentDispatchId);
            
            if (employee.SecurityRoleId != 1)
            {
                View.m_tabSync.TabPages.Remove(View.m_tabPageSync);
            }
            else
            {
                View.m_btnQbSync.Click += OnQbQbSyncClick;
                InitializeRequests();
            }
                
            View.m_tabpageDebug.PageVisible = Configuration.QuickBooksLogLevel == QuickbooksLogLevalEnum.Debug;
        }

        private void InitializeCustomerProjectInfo()
        {
            m_ctrlCustomerInfo = new CustomerInfo
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = "m_ctrlCustomerInfo",
                TabIndex = 6,
                Visible = false
            };

            m_ctrlCustomerInfo.EditCustomer += OnEditCustomer;
            m_ctrlCustomerInfo.CreateVisit += OnCreateVisit;
            m_ctrlCustomerInfo.CreateProject += OnCreateProject;

            m_ctrlProjectInfo = new ProjectInfo
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = "m_ctrlProjectInfo",
                TabIndex = 6,
                Visible = false
            };

            m_ctrlProjectInfo.EditProject += OnEditProject;
            m_ctrlProjectInfo.VisitSelected += OnVisitSelected;

            View.panelControl2.Controls.Add(m_ctrlCustomerInfo);
            View.panelControl2.Controls.Add(m_ctrlProjectInfo);
        }

        private void InitializeRequests()
        {
            m_ctlCustomerRequest = new CustomerRequest
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                TabIndex = 0,
                Visible = false
            };

            m_ctlInvoiceRequest = new InvoiceRequest
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                TabIndex = 0
            };

            m_ctlJobRequest = new JobRequest
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                TabIndex = 0
            };

            m_ctlInvoiceRequest = new InvoiceRequest
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                TabIndex = 0
            };

            View.m_grpRequest.Controls.Add(m_ctlInvoiceRequest);
            View.m_grpRequest.Controls.Add(m_ctlCustomerRequest);
            View.m_grpRequest.Controls.Add(m_ctlJobRequest);
            View.m_grpRequest.Controls.Add(m_ctlInvoiceRequest);
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_cmbProjectManager.Properties.Items.Add(
                    new ImageComboBoxItem("All", 0));
            foreach (Employee projectManager in Model.ProjectManagers)
            {
                View.m_cmbProjectManager.Properties.Items.Add(
                    new ImageComboBoxItem(projectManager.DisplayName, (object)projectManager.ID));
            }

            View.m_gridCustomersProjects.DataSource = Model.CustomerProjectWrappers;

            View.m_cmbSalesRep.Properties.Items.Add(
                    new ImageComboBoxItem("All", string.Empty));
            foreach (QbSalesRep salesRep in Model.SalesReps)
            {
                View.m_cmbSalesRep.Properties.Items.Add(
                    new ImageComboBoxItem(salesRep.DisplayName, salesRep.ListId));
            }

            View.m_cmbAdSource.Properties.Items.Add(
                    new ImageComboBoxItem("All", string.Empty));
            foreach (QbCustomerType customerType in Model.QbCustomerTypes)
            {
                View.m_cmbAdSource.Properties.Items.Add(
                    new ImageComboBoxItem( customerType.Name, customerType.ListId));
            }

            View.m_cmbType.Properties.Items.Add(
                    new ImageComboBoxItem("All", 0));
            foreach (ProjectType projectType in Model.ProjectTypes)
            {
                View.m_cmbType.Properties.Items.Add(
                    new ImageComboBoxItem(projectType.Type, projectType.ID));
            }

            ClearFilter();

            View.m_ctrlDateRange.EditValue = Model.DefaultDateRange;
        }

        #endregion

        #region OnShowDashboard

        public void OnVisitSelected(Visit visit)
        {
            m_mainFormController.ShowDashboard(visit);
        }

        #endregion 

        #region OnEditProject

        private void OnEditProject(CustomerProjectWrapper wrapper)
        {
            var projectWrapper = new ProjectWrapper(wrapper);

            var customerAndAddress = new CustomerAndAddress(wrapper.Customer, projectWrapper.CustomerAddress);
            
            using (var controller = Prepare<CreateProjectController>(customerAndAddress, projectWrapper))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                Refresh();

                for (int i = 0; i < Model.CustomerProjectWrappers.Count; i++)
                {
                    var wrapper1 = Model.CustomerProjectWrappers[i];
                    if (!wrapper1.IsCustomer)
                    {
                        if (wrapper1.ProjectId == controller.CreatedProject.ID)
                        {
                            int rowHandle = View.m_gridCustomersProjectsView.GetRowHandle(i);
                            ShowCustomerProjectWrapper(wrapper1);
                            View.m_gridCustomersProjectsView.FocusedRowHandle = rowHandle;
                            View.m_gridCustomersProjectsView.SelectRow(rowHandle);
                            break;
                        }
                    }
                }
            }  
        }

        #endregion

        #region OnCreateProjectClick

        private void OnCreateProjectClick(object sender, EventArgs args)
        {

            CustomerAndAddress customerAndAddress;
            using (var controller = Prepare<CustomerLookupController>())
            {
                controller.Execute(false);
                if (!controller.IsCustomerSelected)
                    return;

                customerAndAddress = controller.Customer;
            }

            BindingList<ProjectWrapper> recentProjects = Project.FindProjectWrappers(customerId:customerAndAddress.Customer.ID, isActiveAndRecent:true);

            ProjectWrapper selectedProject = null;

            if (recentProjects.Count > 0)
            {
                using (var controller = Prepare<ProjectCustomerHistoryController>(customerAndAddress, recentProjects))
                {
                    controller.Execute(false);

                    if (controller.IsCancelled)
                        return;

                    if (controller.SelectedProject != null)
                        selectedProject = controller.SelectedProject;
                }

            }

            using (var controller = Prepare<CreateProjectController>(customerAndAddress, selectedProject))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                Model.UpdateCustomerProjects(exactProjectId:controller.CreatedProject.ID);
                Refresh();

                for (int i = 0; i < Model.CustomerProjectWrappers.Count; i++)
                {
                    var wrapper1 = Model.CustomerProjectWrappers[i];
                    if (!wrapper1.IsCustomer)
                    {
                        if (wrapper1.ProjectId == controller.CreatedProject.ID)
                        {
                            int rowHandle = View.m_gridCustomersProjectsView.GetRowHandle(i);
                            ShowCustomerProjectWrapper(wrapper1);
                            View.m_gridCustomersProjectsView.FocusedRowHandle = rowHandle;
                            View.m_gridCustomersProjectsView.SelectRow(rowHandle);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region OnCreateCustomerProject

        private void OnCreateProject(CustomerProjectWrapper wrapper)
        {
            var address = Address.FindByPrimaryKey(wrapper.Customer.AddressId.Value, null);
            var customerAndAddress = new CustomerAndAddress(wrapper.Customer, address);
            
            var recentProjects = Project.FindProjectWrappers(customerId:wrapper.Customer.ID);

            ProjectWrapper selectedProject = null;

            if (recentProjects.Count > 0)
            {
                using (var controller = Prepare<ProjectCustomerHistoryController>(customerAndAddress, recentProjects))
                {
                    controller.Execute(false);

                    if (controller.IsCancelled)
                        return;

                    if (controller.SelectedProject != null)
                        selectedProject = controller.SelectedProject;
                }
            }

            using ( var controller = Prepare<CreateProjectController>(customerAndAddress, selectedProject))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                Model.UpdateCustomerProjects(exactProjectId:controller.CreatedProject.ID);
                Refresh();

                for (var i = 0; i < Model.CustomerProjectWrappers.Count; i++)
                {
                    var wrapper1 = Model.CustomerProjectWrappers[i];
                    if (!wrapper1.IsCustomer && (wrapper1.ProjectId == controller.CreatedProject.ID))
                    {
                        var rowHandle = View.m_gridCustomersProjectsView.GetRowHandle(i);
                        ShowCustomerProjectWrapper(wrapper1);
                        View.m_gridCustomersProjectsView.FocusedRowHandle = rowHandle;
                        View.m_gridCustomersProjectsView.SelectRow(rowHandle);
                        break;
                    }
                }
            }                                  
        }

        #endregion

        #region Create Visit

        private void OnCreateVisit(CustomerProjectWrapper wrapper)
        {
            CreateVisit(wrapper.Customer);
        }

        private void OnCreateVisitClick (object sender, EventArgs args)
        {
            CustomerAndAddress customerAndAddress;
            using (var controller = Prepare<CustomerLookupController>())
            {
                controller.Execute(false);
                if (!controller.IsCustomerSelected)
                    return;

                customerAndAddress = controller.Customer;
            }
            CreateVisit(customerAndAddress.Customer);
        }

        private void CreateVisit(Customer customer)
        {
            using (var controller = Prepare<CreateVisitController>(customer))
            {
                controller.Execute(false);
                if (controller.IsCancelled)
                    return;

                View.m_txtCustomer.Text = customer.LastName + (!string.IsNullOrEmpty(customer.FirstName) ? ", " + customer.FirstName : string.Empty);

                var  projects = Project.FindByVisitId(controller.AffectedVisit.ID, null);
                if (projects.Count == 0)
                    return;

                Model.UpdateCustomerProjects(exactProjectId:projects[0].ID);
                Refresh();

                for (int i = 0; i < Model.CustomerProjectWrappers.Count; i++)
                {
                    var wrapper1 = Model.CustomerProjectWrappers[i];
                    if (!wrapper1.IsCustomer)
                    {
                        var idx = projects.FindIndex(delegate(Project temp) { return wrapper1.ProjectId == temp.ID; });
                        if (idx > -1)
                        {
                            var rowHandle = View.m_gridCustomersProjectsView.GetRowHandle(i);
                            ShowCustomerProjectWrapper(wrapper1);
                            View.m_gridCustomersProjectsView.FocusedRowHandle = rowHandle;
                            View.m_gridCustomersProjectsView.SelectRow(rowHandle);
                            break;
                        }
                    }
                }
            }
        }

        #endregion 

        #region OnCreateCustomerClick

        private void OnCreateCustomerClick(object sender, EventArgs args)
        {
            Customer customer;
            using (var controller = Controller.Prepare<CustomerLookupController>(null))
            {
                controller.Execute(false);
                if (!controller.IsCustomerSelected)
                    return;

                customer = controller.Customer.Customer;
            }

            ClearFilter();
            View.m_txtCustomer.Text = customer.LastName + (!string.IsNullOrEmpty(customer.FirstName)? ", " + customer.FirstName:string.Empty);
            Model.UpdateCustomerProjects(customer:View.m_txtCustomer.Text.Trim());
            Refresh();

            for (var i = 0; i < Model.CustomerProjectWrappers.Count; i++ )
            {
                var wrapper = Model.CustomerProjectWrappers[i];
                if (wrapper.Customer.ID == customer.ID)
                {
                    var rowHandle = View.m_gridCustomersProjectsView.GetRowHandle(i);
                    ShowCustomerProjectWrapper(wrapper);
                    View.m_gridCustomersProjectsView.FocusedRowHandle = rowHandle;
                    View.m_gridCustomersProjectsView.SelectRow(rowHandle);
                    break;
                }
            }
        }

        #endregion 

        #region OnEditCustomer

        private void OnEditCustomer(CustomerProjectWrapper wrapper)
        {
            Address address = Address.FindByPrimaryKey(wrapper.Customer.AddressId.Value, null);

            QbCustomer qbCustomer = null;
            try
            {
                qbCustomer = QbCustomer.FindParent(wrapper.Customer.ID, null);
            }
            catch (DataNotFoundException)
            {
                qbCustomer = new QbCustomer(-1);
                qbCustomer.Fill(wrapper.Customer, address);
            }

            string originalQbSalesRepListId = qbCustomer.QbSalesRepListId;
            string originalQbCustomerTypeListId = qbCustomer.QbCustomerTypeListId;

            var controller = Controller.Prepare<CustomerEditController>(
                new CustomerAndAddress(wrapper.Customer, address), qbCustomer);
            controller.Execute(false);
            if (!controller.IsCancelled)
            {
                Customer.Update(wrapper.Customer);
                Address.Update(address);

                if (originalQbSalesRepListId != qbCustomer.QbSalesRepListId || originalQbCustomerTypeListId != qbCustomer.QbCustomerTypeListId)
                {
                    if (qbCustomer.ID == -1)
                        QbCustomer.Insert(qbCustomer);
                    else
                        QbCustomer.Update(qbCustomer);

                    var qbSyncRequest = new QbSyncRequest
                    {
                        QbCustomerId = qbCustomer.ID
                    };
                    
                    if (string.IsNullOrEmpty(qbCustomer.ListId))
                        qbSyncRequest.QbSyncActionId = (int)QbSyncActionEnum.CustomerAdd;
                    else
                        qbSyncRequest.QbSyncActionId = (int) QbSyncActionEnum.CustomerMod;

                    qbSyncRequest.RequestDate = DateTime.Now;
                    QbSyncRequest.Insert(qbSyncRequest);
                }

                ShowCustomerProjectWrapper(wrapper);
            }
        }

        #endregion

        #region OnCustomersProjectsFocusedRowChanged

        private void OnCustomersProjectsFocusedRowChanged(object sender, FocusedRowChangedEventArgs args)
        {
            var wrapper =
                (CustomerProjectWrapper)View.m_gridCustomersProjectsView.GetRow(args.FocusedRowHandle);

            ShowCustomerProjectWrapper(wrapper);
        }

        #endregion

        #region OnTransactionsDoubleClick

        private void OnTransactionsDoubleClick(object sender, EventArgs e)
        {
            if (View.m_gridTransactionsView.FocusedRowHandle < 0)
                return;

            var transaction = (QbTransaction)View.m_gridTransactionsView.GetRow(
                View.m_gridTransactionsView.FocusedRowHandle);

            if (transaction.Type == QbTransactionTypeEnum.Invoice)
            {
                using (var controller = Prepare<InvoiceEditController>(transaction))
                {
                    controller.Execute(false);
                }                
            } 
            else if (transaction.Type == QbTransactionTypeEnum.Payment)
            {
                using (var controller = Prepare<AccountingPaymentController>(transaction))
                {
                    controller.Execute(false);
                }                                
            }
            else if (transaction.Type == QbTransactionTypeEnum.CreditMemo)
            {
                using (var controller = Prepare<CreditMemoController>(transaction))
                {
                    controller.Execute(false);
                }                                
            }
        }

        #endregion

        #region Debug

        #region OnMissingCustomersClick

        private void OnMissingCustomersClick(object sender, EventArgs args)
        {
            m_statusRecords.Clear();

            var qbSync = new QbSync(ReportStatus);
            try
            {
                qbSync.Connect();

                var daysMissing = int.Parse(View.m_txtDaysMissing.Text);
                qbSync.FindMissingCustomers(daysMissing);
            }
            catch (Exception ex)
            {
                ReportStatus("ERROR", "ERROR", ex.Message);
            }
            finally
            {
                qbSync.Disconnect();
            }
        }

        #endregion

        #region OnLookUp


        private void OnLookup (object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            using (var waitCursor = new WaitCursor())
            {
                m_statusRecords.Clear();

                var qbSync = new QbSync(ReportStatus);
                try
                {
                    qbSync.Connect();
                    qbSync.FindCustomerListIds(View.m_txtCustomerLookup.Text);
                }
                catch (Exception ex)
                {
                    ReportStatus("ERROR", "ERROR", ex.Message);
                }
                finally
                {
                    qbSync.Disconnect();
                }
            }
        }

        #endregion
        #endregion

        #region Filter

        private void OnFilterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            OnFiltersChanged(sender, e);
        }

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            Refresh();   
        }
       
        private void ClearFilter()
        {
            View.m_errorProvider.ClearErrors();
            View.m_txtCustomer.Text = string.Empty;
            View.m_txtProjectId.Text = string.Empty;
            View.m_ctrlDateRange.EditValue = Model.DefaultDateRange;
            View.m_txtBlock.Text = string.Empty;
            View.m_txtStreet.Text = string.Empty;
            View.m_txtCity.Text = string.Empty;
            View.m_txtZip.Text = string.Empty;
            View.m_txtPhoneNo.Text = string.Empty;
            View.m_cmbStatus.SelectedIndex = 0;
            View.m_cmbProjectManager.SelectedIndex = 0;
            View.m_cmbSalesRep.SelectedIndex = 0;
            View.m_cmbAdSource.SelectedIndex = 0;
        }

        private void OnClearClick(object sender, EventArgs args)
        {
            ClearFilter();
            Refresh(false);
        }

        #endregion

        #region Refresh

        public void Refresh(bool askToEnterFilterData = true)
        {
            int? exactProjectId = null;
            string jobNumber = null;

            var street = View.m_txtStreet.Text.Trim();
            var block = View.m_txtBlock.Text.Trim();
            var city = View.m_txtCity.Text.Trim();
            var zip  = View.m_txtZip.Text.Trim();
            var phoneNumber = Utils.ExtractDigits(View.m_txtPhoneNo.Text.Trim());
            var customerName = View.m_txtCustomer.Text.Trim();
            var projectStatusStr = (string)View.m_cmbStatus.SelectedItem;
            var projectManagerId = View.m_cmbProjectManager.SelectedIndex == 0 ? null : (int?) View.m_cmbProjectManager.EditValue;
            projectManagerId = (projectManagerId == 0)?null : projectManagerId;

            ProjectStatusEnum? projectStatus = null;
            switch(projectStatusStr)
            {
                case "Open":
                    projectStatus = ProjectStatusEnum.Open;
                    break;
                case "Closed":
                    projectStatus = ProjectStatusEnum.Completed;
                    break;
            }

            View.m_errorProvider.ClearErrors();

            if (street != string.Empty)
            {
                if (block == string.Empty)
                    View.m_errorProvider.SetError(View.m_txtBlock, "Required");
                else if (city == string.Empty && zip == string.Empty)
                {
                    View.m_errorProvider.SetError(View.m_txtCity, "Required city or zip");
                    View.m_errorProvider.SetError(View.m_txtZip, "Required city or zip");
                }

                if (View.m_errorProvider.HasErrors)
                    return;
            }
            
            if (!string.IsNullOrEmpty(View.m_txtProjectId.Text))
            {
                int projectId;
                if (int.TryParse(View.m_txtProjectId.Text, out projectId))
                    exactProjectId = projectId;
                else
                    jobNumber = View.m_txtProjectId.Text.ToUpper();
            }

            string qbSalesRepListId = null;
            if (!string.IsNullOrEmpty(View.m_cmbSalesRep.Text) && View.m_cmbSalesRep.Text != "0")
            {
                qbSalesRepListId = (string)View.m_cmbSalesRep.EditValue;
            }

            string qbCustomerType = null;
            if (!string.IsNullOrEmpty(View.m_cmbAdSource.Text) && View.m_cmbAdSource.Text != "0")
            {
                qbCustomerType = (string)View.m_cmbAdSource.EditValue;
            }

            DateRange dateRange = View.m_ctrlDateRange.EditValue;
            if (dateRange.StartDate == null)
            {
                dateRange = Model.DefaultDateRange;
            }

            ProjectTypeEnum? projectType = null;
            if (View.m_cmbType.SelectedIndex > 0)
            {
                int projectTypeId = Model.ProjectTypes[View.m_cmbType.SelectedIndex - 1].ID;
                projectType = (ProjectTypeEnum)projectTypeId;
            }

            if (askToEnterFilterData && 
                (!projectStatus.HasValue || projectStatus.Value != ProjectStatusEnum.Open) 
                && dateRange == null && string.IsNullOrEmpty(jobNumber) && exactProjectId == null && string.IsNullOrEmpty(customerName)
                && string.IsNullOrEmpty(street) && string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show(@"Please enter JobNumber or Customer Name or Phone Number or Date Created Range",
                                @"Filter", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            if (dateRange != null && dateRange.EndDate.HasValue && dateRange.StartDate.HasValue && dateRange.EndDate.Value.Subtract(dateRange.StartDate.Value).Days > 61
                && (!projectStatus.HasValue || projectStatus.Value != ProjectStatusEnum.Open))
            {
                MessageBox.Show(@"Date Range is too big.  Must be less then 60 days",
                                @"Filter", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            Model.UpdateCustomerProjects(jobNumber, exactProjectId, customerName, qbSalesRepListId, qbCustomerType, dateRange, projectType,
                block, street, city, zip, phoneNumber, 
                projectStatus, projectManagerId);

            View.m_gridCustomersProjects.DataSource = Model.CustomerProjectWrappers;
            if (Model.CustomerProjectWrappers.Count == 0)
            {
                m_ctrlCustomerInfo.Clear();
                m_ctrlProjectInfo.Clear();
            }

            if (Model.CustomerProjectWrappers.Count > 0)
            {
                if (exactProjectId.HasValue || jobNumber != null)
                {
                    for (int i = 0; i < Model.CustomerProjectWrappers.Count; i++)
                    {
                        CustomerProjectWrapper wrapper = Model.CustomerProjectWrappers[i];
                        if (!wrapper.IsCustomer)
                        {
                            if ((wrapper.ProjectConstructionDetail != null
                                && !string.IsNullOrEmpty(wrapper.ProjectConstructionDetail.JobNumber) && wrapper.ProjectConstructionDetail.JobNumber.ToUpper() == jobNumber) 
                                ||
                                (exactProjectId.HasValue && wrapper.ProjectId == exactProjectId.Value))
                            {
                                int rowHandle = View.m_gridCustomersProjectsView.GetRowHandle(i);
                                ShowCustomerProjectWrapper(wrapper);
                                View.m_gridCustomersProjectsView.FocusedRowHandle = rowHandle;
                                View.m_gridCustomersProjectsView.SelectRow(rowHandle);
                                break;
                            }
                        }
                    }
                }
                else
                    ShowCustomerProjectWrapper(Model.CustomerProjectWrappers[0]);
            }
        }

        #endregion

        #region ShowCustomerProjectWrapper

        private void ShowCustomerProjectWrapper(CustomerProjectWrapper wrapper)
        {
            m_ctrlCustomerInfo.Visible = false;
            m_ctrlProjectInfo.Visible = false;

            if (wrapper == null)
            {
                m_ctrlCustomerInfo.Clear();
                m_ctrlProjectInfo.Clear();
                View.m_gridTransactions.DataSource = new BindingList<QbTransaction>();
                return;
            }

            if (wrapper.IsCustomer)
            {
                m_ctrlCustomerInfo.Initialize(wrapper);
                m_ctrlCustomerInfo.Visible = true;
            }
            else
            {
                var projectWrapper = Project.FindProjectWrappers(exactProjectId:wrapper.ProjectId)[0];
                m_ctrlProjectInfo.Initialize(wrapper, projectWrapper);
                m_ctrlProjectInfo.Visible = true;
            }
            View.m_gridTransactions.DataSource = new BindingList<QbTransaction>
                (Model.GetTransactions(wrapper));

        }

        #endregion

        #region ReportStatus

        private void ReportStatus(string action, string status, string description)
        {
            m_statusRecords.Add(new StatusRecord(action, status, description));
            var lastRowHandle = View.m_gridStatusView.GetRowHandle(m_statusRecords.Count == 0 ? 0 : m_statusRecords.Count-1);
            View.m_gridStatusView.FocusedRowHandle = lastRowHandle;
        }

        #endregion

        #region QbSync

        private void OnQbQbSyncClick (object sender, EventArgs args)
        {
            RunBaseSync();
            RunImport();
            RunExport();
        }

        private void RunBaseSync()
        {
            using (var waitCursor = new WaitCursor())
            {
                m_statusRecords.Clear();

                var qbSync = new QbSync(ReportStatus);
                try
                {
                    qbSync.Connect();
                    qbSync.RefreshBaseData();
                }
                catch (Exception ex)
                {
                    ReportStatus("ERROR", "ERROR", ex.Message);
                }
                finally
                {
                    qbSync.Disconnect();
                }
            }
        }

        private void RunImport()
        {
            View.m_btnQbSync.Enabled = false;
            
            using (WaitCursor waitCursor = new WaitCursor())
            {
                
                QbSync qbSync = new QbSync(ReportStatus);
                try
                {
                    qbSync.Connect();
                    qbSync.SyncronizeData();
                }
                catch (Exception ex)
                {
                    ReportStatus("ERROR", "ERROR", ex.Message);
                }
                finally
                {
                    qbSync.Disconnect();
                    View.m_btnQbSync.Enabled = true;
                }
            }
        }

        private void RunExport()
        { 
            m_syncRequests = QbSyncRequest.Find();
            m_currentSyncRequestIdx = 0;

            if (m_syncRequests.Count == 0)
                return;

            View.m_btnQbSync.Enabled = false;

            ProcessNonInteractiveRequests();

            m_syncRequests.Sort(
                delegate(QbSyncRequest request1, QbSyncRequest request2)
                { return request1.ID.CompareTo(request2.ID); });


            m_qbSync = new QbSync(ReportStatus);

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    m_qbSync.Connect();
                }

                QbSyncRequest syncRequest = m_syncRequests[m_currentSyncRequestIdx];

                View.m_grpRequest.Visible = true;
                StartRequestProcessing(syncRequest);
            }
            catch(Exception ex)
            {
                ReportStatus("ERROR", "ERROR", ex.Message);
            }
        }

        private void ProcessNonInteractiveRequests()
        {
            List<QbSyncRequest> customerAddSyncRequests = m_syncRequests.FindAll(
                delegate(QbSyncRequest request) { return request.QbSyncActionId == (int)QbSyncActionEnum.CustomerAdd; }
                );

            foreach (QbSyncRequest customerAddSyncRequest in customerAddSyncRequests)
            {
                List<QbSyncRequest> customerMoSyncRequests = m_syncRequests.FindAll(
                    delegate(QbSyncRequest request)
                    {
                        return request.QbSyncActionId == (int)QbSyncActionEnum.CustomerMod &&
                               request.QbCustomerId == customerAddSyncRequest.QbCustomerId;
                    });

                foreach (QbSyncRequest customerModSyncRequest in customerMoSyncRequests)
                {
                    QbSyncRequest.Delete(customerModSyncRequest);
                    m_syncRequests.Remove(customerModSyncRequest);
                }
            }

            List<QbSyncRequest> jobAddSyncRequests = m_syncRequests.FindAll(
                delegate(QbSyncRequest request) { return request.QbSyncActionId == (int)QbSyncActionEnum.JobAdd; }
                );

            foreach (QbSyncRequest jobAddSyncRequest in jobAddSyncRequests)
            {
                List<QbSyncRequest> jobMoSyncRequests = m_syncRequests.FindAll(
                   delegate(QbSyncRequest request)
                   {
                       return request.QbSyncActionId == (int)QbSyncActionEnum.JobMod &&
                              request.QbCustomerId == jobAddSyncRequest.QbCustomerId;
                   });

                foreach (QbSyncRequest jobModSyncRequest in jobMoSyncRequests)
                {
                    QbSyncRequest.Delete(jobModSyncRequest);
                    m_syncRequests.Remove(jobModSyncRequest);
                }
            }

            List<QbSyncRequest> invoiceAddSyncRequests = m_syncRequests.FindAll(
               delegate(QbSyncRequest request) { return request.QbSyncActionId == (int)QbSyncActionEnum.InvoiceAdd; }
               );

            foreach (QbSyncRequest invoiceAddSyncRequest in invoiceAddSyncRequests)
            {
                QbSyncRequest invoiceVoidSuncRequest = m_syncRequests.Find(
                   delegate(QbSyncRequest request)
                   {
                       return request.QbSyncActionId == (int)QbSyncActionEnum.InvoiceVoid &&
                              request.QbInvoiceId == invoiceAddSyncRequest.QbInvoiceId;
                   });

                if (invoiceVoidSuncRequest != null)
                {
                    QbSyncRequest.Delete(invoiceAddSyncRequest);
                    m_syncRequests.Remove(invoiceAddSyncRequest);
                }
            }


            List<QbSyncRequest> invoiceVoidSyncRequests = m_syncRequests.FindAll(
                delegate(QbSyncRequest request) { return request.QbSyncActionId == (int)QbSyncActionEnum.InvoiceVoid; }
                );

            foreach (QbSyncRequest invoiceVoidSyncRequest in invoiceVoidSyncRequests)
            {
                QbSync.VoidInvoice(invoiceVoidSyncRequest);
                QbSyncRequest.Delete(invoiceVoidSyncRequest);
                m_syncRequests.Remove(invoiceVoidSyncRequest);
            }

        }

        private void StartRequestProcessing(QbSyncRequest syncRequest)
        {
            View.m_grpRequest.Text = @"Process Request " + (m_currentSyncRequestIdx + 1) + @"/" + m_syncRequests.Count;

            var action = (QbSyncActionEnum)syncRequest.QbSyncActionId;
            switch (action)
            {
                case QbSyncActionEnum.CustomerAdd:
                    StartCreateQbCustomer(syncRequest);
                    break;
                case QbSyncActionEnum.CustomerMod:
                    StartModQbCustomer(syncRequest);
                    break;
                case QbSyncActionEnum.JobAdd:
                    StartCreateQbJob(syncRequest);
                    break;
                case QbSyncActionEnum.JobMod:
                    StartModQbJob(syncRequest);
                    break;
                case QbSyncActionEnum.InvoiceAdd:
                    StartCreateQbInvoice(syncRequest);
                    break;
            }
        }

        private void StartCreateQbJob(QbSyncRequest  syncRequest)
        {
            var qbProject = QbCustomer.FindByPrimaryKey(syncRequest.QbCustomerId.Value, null);
            m_ctlJobRequest.Initialize(syncRequest, qbProject, ProcessSyncRequest);
            ShowRequestControl(QbSyncActionEnum.JobAdd);
        }

        private void StartModQbJob(QbSyncRequest syncRequest)
        {
            var qbProject = QbCustomer.FindByPrimaryKey(syncRequest.QbCustomerId.Value, null);
            m_ctlJobRequest.Initialize(syncRequest, qbProject, ProcessSyncRequest);
            ShowRequestControl(QbSyncActionEnum.JobMod);
        }

        private void StartCreateQbCustomer(QbSyncRequest syncRequest)
        {
            var qbCustomer = QbCustomer.FindByPrimaryKey(syncRequest.QbCustomerId.Value, null);
            var similarQbCustomers = m_qbSync.FindSimilarCustomers(qbCustomer);
            m_ctlCustomerRequest.Initialize(syncRequest, qbCustomer, 
                similarQbCustomers, ProcessSyncRequest);
            ShowRequestControl(QbSyncActionEnum.CustomerAdd);
        }

        private void StartModQbCustomer(QbSyncRequest syncRequest)
        {
            var similarQbCustomers = new List<QbCustomer>();
            QbCustomer qbCustomer = QbCustomer.FindByPrimaryKey(syncRequest.QbCustomerId.Value, null);
            m_ctlCustomerRequest.Initialize(syncRequest, qbCustomer, similarQbCustomers, ProcessSyncRequest);
            ShowRequestControl(QbSyncActionEnum.CustomerMod);
        }

        private void StartCreateQbInvoice (QbSyncRequest syncRequest)
        {
            QbInvoice qbInvoice = QbInvoice.FindByPrimaryKey(syncRequest.QbInvoiceId.Value, null);
            m_ctlInvoiceRequest.Initialize(qbInvoice, ProcessSyncRequest);
            ShowRequestControl(QbSyncActionEnum.InvoiceAdd);
        }

        private void ProcessSyncRequest(QbSyncActionEnum action, object data)
        {
            try
            {
                switch (action)
                {
                    case QbSyncActionEnum.CustomerAdd:
                        var qbCustomer = (QbCustomer)data;
                        CreateQbCustomer(m_syncRequests[m_currentSyncRequestIdx], qbCustomer);
                        QbSyncRequest.Delete(m_syncRequests[m_currentSyncRequestIdx]);
                        ReportStatus("Customer Added", "OK", qbCustomer.FullName);
                        break;
                    case QbSyncActionEnum.CustomerMod:
                        var qbCustomer1 = (QbCustomer)data;
                        CreateQbCustomer(m_syncRequests[m_currentSyncRequestIdx], qbCustomer1);
                        QbSyncRequest.Delete(m_syncRequests[m_currentSyncRequestIdx]);
                        ReportStatus("Customer Modified", "OK", qbCustomer1.FullName);
                        break;
                    case QbSyncActionEnum.SkipCustomer:
                        var qbCustomer2 = (QbCustomer)data;
                        SkipQbCustomer(m_syncRequests[m_currentSyncRequestIdx], qbCustomer2);
                        ReportStatus("Customer Skipped", "OK", qbCustomer2.FullName);
                        break;
                    case QbSyncActionEnum.DontSyncCustomer:
                        var qbCustomer3 = (QbCustomer)data;
                        RemoveSyncQbCustomer(m_syncRequests[m_currentSyncRequestIdx], qbCustomer3);
                        ReportStatus("Customer Sync Removed", "OK", qbCustomer3.FullName);
                        break;
                    case QbSyncActionEnum.JobAdd:
                        var qbProject = (QbCustomer)data;
                        CreateJob(m_syncRequests[m_currentSyncRequestIdx], qbProject);
                        QbSyncRequest.Delete(m_syncRequests[m_currentSyncRequestIdx]);
                        ReportStatus("Job Added", "OK", qbProject.FullName);
                        break;
                    case QbSyncActionEnum.JobMod:
                        var qbProject1 = (QbCustomer)data;
                        UpdateJob(m_syncRequests[m_currentSyncRequestIdx], qbProject1);
                        QbSyncRequest.Delete(m_syncRequests[m_currentSyncRequestIdx]);
                        ReportStatus("Job Updated", "OK", qbProject1.FullName);
                        break;
                    case QbSyncActionEnum.SkipJob:
                        var qbProject2 = (QbCustomer)data;
                        SkipQbCustomer(m_syncRequests[m_currentSyncRequestIdx], qbProject2);
                        ReportStatus("Job Skipped", "OK", qbProject2.FullName);
                        break;
                    case QbSyncActionEnum.DontSyncJob:
                        var qbProject3 = (QbCustomer)data;
                        RemoveSyncQbCustomer(m_syncRequests[m_currentSyncRequestIdx], qbProject3);
                        ReportStatus("Job Skipped", "OK", qbProject3.FullName);
                        break;
                    case QbSyncActionEnum.InvoiceAdd:
                        var qbInvoice = (QbInvoice) data;
                        CreateInvoice(m_syncRequests[m_currentSyncRequestIdx], qbInvoice);
                        QbSyncRequest.Delete(m_syncRequests[m_currentSyncRequestIdx]);
                        ReportStatus("Invoice Created", "OK", qbInvoice.TxnNumber.ToString());
                        break;
                    case QbSyncActionEnum.SkipInvoice:
                        var qbInvoice1 = (QbInvoice)data;
                        SkipQbInvoice(m_syncRequests[m_currentSyncRequestIdx], qbInvoice1);
                        ReportStatus("Invoice Skipped", "OK", qbInvoice1.TxnNumber.ToString());
                        break;
                    case QbSyncActionEnum.DontSyncInvoice:
                        var qbInvoice2 = (QbInvoice)data;
                        DontSyncInvoice(m_syncRequests[m_currentSyncRequestIdx], qbInvoice2);
                        ReportStatus("Invoice Sunc Removed", "OK", qbInvoice2.TxnNumber.ToString());
                        break;
                }

                m_currentSyncRequestIdx++;

                if (m_syncRequests.Count > m_currentSyncRequestIdx)
                    StartRequestProcessing(m_syncRequests[m_currentSyncRequestIdx]);
                else
                {
                    View.m_grpRequest.Visible = false;
                    m_qbSync.Disconnect();
                    HideAllRequestControls();
                    View.m_btnQbSync.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ReportStatus("ERROR", "ERROR", ex.ToString());
            }
        }

        private void CreateQbCustomer(QbSyncRequest syncRequest, QbCustomer qbCustomer)
        {
            if (string.IsNullOrEmpty(qbCustomer.ListId))
                m_qbSync.AddCustomer(syncRequest, ref qbCustomer);
            else
                m_qbSync.UpdateCustomer(syncRequest, ref qbCustomer);
        }

        private void SkipQbCustomer(QbSyncRequest syncRequest, QbCustomer qbCustomer)
        {
            while (m_currentSyncRequestIdx + 1 < m_syncRequests.Count)
            {
                QbSyncRequest nextQbSyncRequest = m_syncRequests[m_currentSyncRequestIdx + 1];

                QbCustomer qbCustomer1 =
                    QbCustomer.FindByPrimaryKey(nextQbSyncRequest.QbCustomerId.Value,
                                                null);

                if (qbCustomer1.CustomerId == qbCustomer.CustomerId)
                    m_currentSyncRequestIdx++;
                else
                    break;
            }
        }

        private void RemoveSyncQbCustomer (QbSyncRequest syncRequest, QbCustomer qbCustomer)
        {
            QbSyncRequest.Delete(m_syncRequests[m_currentSyncRequestIdx]);
            while (m_currentSyncRequestIdx + 1 < m_syncRequests.Count)
            {
                QbSyncRequest nextQbSyncRequest = m_syncRequests[m_currentSyncRequestIdx + 1];

                QbCustomer qbCustomer1 =
                    QbCustomer.FindByPrimaryKey(nextQbSyncRequest.QbCustomerId.Value,
                                                null);

                if (qbCustomer1.CustomerId == qbCustomer.CustomerId)
                {
                    QbSyncRequest.Delete(nextQbSyncRequest);
                    m_currentSyncRequestIdx++;
                }
                else
                    break;
            }
        }

        private void UpdateJob(QbSyncRequest syncRequest, QbCustomer qbProject)
        {
            m_qbSync.UpdateCustomer(syncRequest, ref qbProject);
        }


        private void CreateJob(QbSyncRequest syncRequest, QbCustomer qbProject)
        {
           m_qbSync.AddJob(syncRequest, ref qbProject);
        }

        private void CreateInvoice(QbSyncRequest syncRequest, QbInvoice qbInvoice)
        {
            m_qbSync.AddInvoice(syncRequest, qbInvoice);
        }

        private void SkipQbInvoice (QbSyncRequest syncRequest, QbInvoice qbInvoice)
        {
            QbCustomer qbCustomer = QbCustomer.FindByPrimaryKey(qbInvoice.QbCustomerId, null);
            SkipQbCustomer(syncRequest, qbCustomer);
        }

        private void DontSyncInvoice(QbSyncRequest syncRequest, QbInvoice qbInvoice)
        {
            QbCustomer qbCustomer = QbCustomer.FindByPrimaryKey(qbInvoice.QbCustomerId, null);
            RemoveSyncQbCustomer(syncRequest, qbCustomer);
        }

        private void ShowRequestControl(QbSyncActionEnum qbSyncAction)
        {
            HideAllRequestControls();

            switch (qbSyncAction)
            {
                case QbSyncActionEnum.CustomerAdd:
                case QbSyncActionEnum.CustomerMod:
                    m_ctlCustomerRequest.Visible = true;
                    break;
                case QbSyncActionEnum.JobAdd:
                case QbSyncActionEnum.JobMod:
                    m_ctlJobRequest.Visible = true;
                    break;
                case QbSyncActionEnum.InvoiceAdd:
                    m_ctlInvoiceRequest.Visible = true;
                    break;
            }
        }

        private void HideAllRequestControls()
        {
            m_ctlCustomerRequest.Visible = false;
            m_ctlInvoiceRequest.Visible = false;
            m_ctlJobRequest.Visible = false;
        }

        #endregion
    }
}
