using System;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AccountingCreditMemo;
using Dalworth.Server.MainForm.AccountingInvoiceEdit;
using Dalworth.Server.MainForm.AccountingPayment;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.CreateProject
{
    public class CreateProjectController : Controller<CreateProjectModel, CreateProjectView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region CreatedProject

        public Project CreatedProject
        {
            get { return Model.ProjectWrapper.Project; }
        }

        #endregion

        #region SelectedQbTransaction

        private QbTransaction SelectedQbTransaction
        {
            get
            {
                if (View.m_gridViewBillPayTransactions.FocusedRowHandle >= 0)
                    return (QbTransaction)View.m_gridViewBillPayTransactions.GetRow(
                        View.m_gridViewBillPayTransactions.FocusedRowHandle);
                
                return null;
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length >=1 && data[0] != null)
                Model.Customer = (CustomerAndAddress)data[0];

              if (data != null && data.Length >=2 && data[1] != null)
                Model.ProjectWrapper = (ProjectWrapper) data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnPrint.Click += OnPrintClick;

            View.m_ctlCustomerLookup.CustomerChanged += OnCustomerChanged;
            View.m_ctlCustomerLookup.Modified += OnCustomerModified;

            View.m_txtJobNumber.Validated += OnJobNumberValidated;
            View.m_cmbProjectType.Validated += OnProjectTypeValidated;

            View.m_dtpDeclineDate.Validated += OnDeclineSignUpDatesValidated;
            View.m_dtpCompleteDate.Validated += OnDeclineSignUpDatesValidated;
            View.m_dtpSignUpDate.DateTimeChanged += OnDeclineSignUpDatesValidated;
            View.m_txtJobCost.TextChanged += OnDeclineSignUpDatesValidated;

            View.m_cmbBillPayType.SelectedValueChanged += OnBillPayTypeChanged;
            View.KeyDown += OnViewKeyDown;

            View.m_gridBillPayTransactions.DoubleClick += OnBillPaysGridDoubleClick;
            View.m_gridViewBillPayTransactions.KeyDown += OnGridViewBillPaysKeyDown;

            View.m_gridViewBillPayTransactions.RowCellStyle += OnBillPayAmountCellStyle;
            View.m_gridViewBillPayTransactions.CustomUnboundColumnData += OnBillPayBalanceData;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
           View.m_cmbProjectManager.Properties.Items.Add(
                new ImageComboBoxItem(string.Empty, (object)0));

            foreach (Employee projectManager in Model.ProjectManagers)
            {
                View.m_cmbProjectManager.Properties.Items.Add(
                    new ImageComboBoxItem(projectManager.DisplayName, (object) projectManager.ID));
            }
            View.m_dtpLeadDate.Properties.MaxValue = DateTime.Now.Date;            
            View.m_dtpSignUpDate.Properties.MaxValue = DateTime.Now.Date;
            View.m_dtpDeclineDate.Properties.MaxValue = DateTime.Now.Date;
            View.m_dtpCompleteDate.Properties.MaxValue = DateTime.Now.Date;

            View.m_gridBillPayTransactions.DataSource = Model.FilteredQbTransactions;
            View.m_colIssueDate.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            
            View.m_cmbProjectType.Properties.Items.Add(
                new ImageComboBoxItem(string.Empty, (object)0));
            View.m_cmbProjectType.Properties.Items.Add(
               new ImageComboBoxItem("Construction", (object)ProjectTypeEnum.Construction));
            View.m_cmbProjectType.Properties.Items.Add(
                new ImageComboBoxItem("Basement Systems", (object)ProjectTypeEnum.BasementSystems));
            View.m_cmbProjectType.Properties.Items.Add(
                new ImageComboBoxItem("Content", (object)ProjectTypeEnum.Content));
           
            Project project = Model.ProjectWrapper.Project;

            if (project.ProjectType > 0)
                View.m_cmbProjectType.EditValue = (ProjectTypeEnum)project.ProjectTypeId;
            else
                View.m_cmbProjectType.EditValue = (object)0;

            SetCustomerAndAddressFromProject();

            if (!Model.IsCreateProject)
            {
                View.Text = "Dalworth - Edit Project";
                ProjectConstructionDetail detail = Model.ProjectWrapper.ConstructionDetail;
                View.m_lblNumber.Text = project.ID.ToString();
                View.m_dtpLeadDate.DateTime = project.CreateDate;

                if (detail.ProjectManagerEmployeeId.HasValue)
                    View.m_cmbProjectManager.EditValue = detail.ProjectManagerEmployeeId.Value;

                View.m_chkSelfGeneratedLead.Checked = detail.IsSelfGeneratedLead;

                if (detail.ScopeDate.HasValue)
                    View.m_dtpScopeDate.DateTime = detail.ScopeDate.Value;

                if (detail.SignUpDate.HasValue)
                {
                    View.m_dtpSignUpDate.DateTime = detail.SignUpDate.Value;
                    View.m_txtJobNumber.Enabled = true;
                    if (detail.JobNumber != null)
                        View.m_txtJobNumber.Text = detail.JobNumber;
                } 

                if (detail.DeclineDate.HasValue)
                    View.m_dtpDeclineDate.DateTime = detail.DeclineDate.Value;

                if (detail.ActualCompletionDate.HasValue)
                    View.m_dtpCompleteDate.DateTime = detail.ActualCompletionDate.Value;

                if (detail.EstimatedAmount != decimal.Zero)
                    View.m_txtEstimatedAmount.EditValue = detail.EstimatedAmount;

                if (detail.JobCost != decimal.Zero)
                    View.m_txtJobCost.EditValue = detail.JobCost;

                View.m_txtNotes.Text = project.Description;
                View.m_dtpLeadDate.Select();
            } else
            {
                View.Text = "Dalworth - Create Project";
                View.m_dtpLeadDate.DateTime = DateTime.Now;
                View.m_ctlCustomerLookup.Customer = Model.Customer.Customer;
                View.m_ctlCustomerLookup.Address = Model.Customer.Address;

                View.m_cmbProjectManager.Focus();
            }

            View.m_ctlAddressLookup.Caption = "Customer Address";
            View.m_ctlAddressLookup.Enabled = false;

            View.m_ctlProjectEdit.IsEditable = true;
            View.m_ctlProjectEdit.IsQbSalesRepVisible = true;
            View.m_ctlProjectEdit.Project = project;
            View.m_ctlProjectEdit.AreaId = Model.ProjectWrapper.ServiceAddress.AreaId;
            View.m_ctlProjectEdit.IsInsuranceVisible = true;
            View.m_ctlProjectEdit.Initialize();

            View.m_lblStatus.Text = project.ProjectStatusText;
            View.m_lblProgress.Text = Model.ProjectWrapper.Progress;

            RefreshAmounts();
            RefreshBillPayTransactions();
        }

        #endregion

        #region RefreshBillPayTransactions

        private void RefreshBillPayTransactions()
        {
            Boolean showInvoices = false;
            Boolean showPayments = false;
            Boolean showCredits = false;
            
            QbTransaction selectedBillPay = SelectedQbTransaction;

            if (View.m_cmbBillPayType.SelectedIndex == 0)
            {
                showInvoices = true;
                showPayments = true;
                showCredits = true;
            }
            else if (View.m_cmbBillPayType.SelectedIndex == 1)
            {
                showInvoices = true;
            }
            else if (View.m_cmbBillPayType.SelectedIndex == 2)
            {
                showPayments = true;
            }
            else if (View.m_cmbBillPayType.SelectedIndex == 3)
            {
                showCredits = true;
            }

            Model.FilteredQbTransactions.Clear();
            foreach(QbTransaction transaction in Model.QbTransactions)
            {
                if ((transaction.Type == QbTransactionTypeEnum.Invoice && !showInvoices)
                    || (transaction.Type == QbTransactionTypeEnum.Payment && !showPayments)
                    || (transaction.Type == QbTransactionTypeEnum.CreditMemo && !showCredits))
                {
                    continue;
                } else 
                    Model.FilteredQbTransactions.Add(transaction);
            }

            SelectQbTransaction(selectedBillPay);
            
            RefreshBalances();
            if (View.m_gridViewBillPayTransactions.RowCount > 0)
                View.m_gridViewBillPayTransactions.SelectRow(View.m_gridViewBillPayTransactions.RowCount);
        }

        #endregion

        #region OpenBillPayTransaction

        private void OpenBillPayTransaction(QbTransaction transaction)
        {

            if (transaction.Type == QbTransactionTypeEnum.Invoice)
            {
                using (InvoiceEditController controller = Prepare<InvoiceEditController>(transaction))
                {
                    controller.Execute(false);
                }
            }
            else if (transaction.Type == QbTransactionTypeEnum.Payment)
            {
                using (AccountingPaymentController controller = Prepare<AccountingPaymentController>(transaction))
                {
                    controller.Execute(false);
                }
            }
            else if (transaction.Type == QbTransactionTypeEnum.CreditMemo)
            {
                using (CreditMemoController controller = Prepare<CreditMemoController>(transaction))
                {
                    controller.Execute(false);
                }
            }
        }

        #endregion

        #region SelectQbTransaction

        public void SelectQbTransaction(QbTransaction transaction)
        {
            View.m_gridViewBillPayTransactions.ClearSelection();

            if (transaction == null)
                return;

            for (int rowIndex = 0; rowIndex < Model.FilteredQbTransactions.Count; rowIndex++)
            {
                int rowHandle = View.m_gridViewBillPayTransactions.GetRowHandle(rowIndex);

                if (rowHandle >= 0)
                {
                    QbTransaction currentBillPay =
                        (QbTransaction)View.m_gridViewBillPayTransactions.GetRow(rowHandle);

                    if (currentBillPay == transaction)
                    {
                        View.m_gridViewBillPayTransactions.ClearSelection();
                        View.m_gridViewBillPayTransactions.FocusedRowHandle = rowHandle;
                        View.m_gridViewBillPayTransactions.SelectRow(rowHandle);

                        return;
                    }
                }
            }
        }

        #endregion

        #region OnCustomerChanged

        private void OnCustomerChanged(Customer customer, Address address)
        {
            View.m_ctlAddressLookup.Customer = customer;
            Model.ProjectWrapper.Customer = customer;
            View.m_ctlAddressLookup.BaseAddress = address;
            View.m_ctlAddressLookup.CurrentAddress = address;
            View.m_ctlAddressLookup.IsBaseAddressActive = true;
            View.m_ctlAddressLookup.BaseAddressName = "Customer Address";
            Model.ProjectWrapper.ServiceAddress = address;            
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

        #region SetCustomerAndAddressFromProject

        private void SetCustomerAndAddressFromProject()
        {
            
            View.m_ctlCustomerLookup.Customer = Model.ProjectWrapper.Customer;
            View.m_ctlCustomerLookup.Address = Model.ProjectWrapper.CustomerAddress;

            View.m_ctlAddressLookup.Customer = Model.ProjectWrapper.Customer;
            View.m_ctlAddressLookup.BaseAddress = Model.ProjectWrapper.CustomerAddress;
            View.m_ctlAddressLookup.CurrentAddress = Model.ProjectWrapper.CustomerAddress;
            

           /* if (Model.ProjectWrapper.Project.ServiceAddressId.HasValue)
            {
                View.m_ctlAddressLookup.Customer = Model.ProjectWrapper.Customer;
                View.m_ctlAddressLookup.BaseAddress = Model.ProjectWrapper.CustomerAddress;
                View.m_ctlAddressLookup.CurrentAddress = Model.ProjectWrapper.ServiceAddress;
                if (Model.ProjectWrapper.CustomerAddress != null)
                    View.m_ctlAddressLookup.IsBaseAddressActive = 
                        Model.ProjectWrapper.CustomerAddress.ID == Model.ProjectWrapper.ServiceAddress.ID;
                View.m_ctlAddressLookup.BaseAddressName = "Customer Address";                
            }*/

        }

        #endregion

        #region IsProjectStatusCheckedIn

        private Boolean IsProjectStatusCheckedIn()
        {
            ProjectConstructionProgressEnum oldProgress =
                Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress;

            //count of not voided credits
            int notVoidedCreditsCount = 0;
            foreach (QbTransaction billPay in Model.QbTransactions)
            {
                if (billPay.Type == QbTransactionTypeEnum.CreditMemo)
                    notVoidedCreditsCount++;
            }

            Boolean readyToDecline = (View.m_dtpSignUpDate.EditValue == null
                                      && Model.ProjectWrapper.ConstructionDetail.LastBillingDate == null
                                      && Model.ProjectWrapper.ConstructionDetail.LastPaymentDate == null
                                      && notVoidedCreditsCount == 0);


            if (View.m_dtpDeclineDate.EditValue != null
                && !readyToDecline)
            {
                XtraMessageBox.Show("Project cannot be declined with non voided invoices, payments or credits", "Alert",
                                                          MessageBoxButtons.OK, MessageBoxIcon.Error,
                                                          MessageBoxDefaultButton.Button1);
                View.m_tabProjetDetail.SelectedTabPage = View.m_tabpgTransaction;
                return false;
            }

            if (View.m_dtpDeclineDate.EditValue != null
                && readyToDecline)
            {
                if (oldProgress != ProjectConstructionProgressEnum.Declined)
                {
                    DialogResult result = XtraMessageBox.Show("Are you sure project is Declined?", "Confirmation",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                              MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                            ProjectConstructionProgressEnum.Declined;
                        Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Completed;
                        return true;
                    }

                    return false;
                }
            } 
            else if (View.m_dtpSignUpDate.EditValue == null
                && View.m_dtpDeclineDate.EditValue == null
                && View.m_dtpCompleteDate.EditValue == null
                && Model.ProjectWrapper.ConstructionDetail.LastBillingDate == null
                && Model.ProjectWrapper.ConstructionDetail.LastPaymentDate == null
                && notVoidedCreditsCount == 0)
            {
                if (oldProgress == ProjectConstructionProgressEnum.Declined 
                    || oldProgress == ProjectConstructionProgressEnum.PaidInFull)
                {
                    DialogResult result = XtraMessageBox.Show("Would you like to re-open Project?", "Confirmation",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                              MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                            ProjectConstructionProgressEnum.Lead;
                        Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Open;
                        return true;
                    }

                    return false;
                } 

                Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                    ProjectConstructionProgressEnum.Lead;
                Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Open;
                return true;
            }
            else if (View.m_dtpDeclineDate.EditValue == null
               && View.m_dtpCompleteDate.EditValue != null)
            {
                if (oldProgress != ProjectConstructionProgressEnum.PaidInFull)
                {
                    DialogResult result = XtraMessageBox.Show("Would you like to complete Project?", "Confirmation",
                                                              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                                              MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                            ProjectConstructionProgressEnum.PaidInFull;
                        Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Completed;
                        return true;
                    }

                    if (result == DialogResult.No)
                    {
                        Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                            ProjectConstructionProgressEnum.Job;
                        Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Open;
                        return true;
                    }
                    return false;
                } 
            }
            else
            {
                if (oldProgress == ProjectConstructionProgressEnum.Declined
                    || oldProgress == ProjectConstructionProgressEnum.PaidInFull)
                {
                    DialogResult result = XtraMessageBox.Show("Would you like to re-open Project?", "Confirmation",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                              MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                            ProjectConstructionProgressEnum.Job;
                        Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Open;
                        return true;
                    }
                    return false;
                }

                Model.ProjectWrapper.ConstructionDetail.ProjectConstructionProgress =
                    ProjectConstructionProgressEnum.Job;
                Model.ProjectWrapper.Project.ProjectStatus = ProjectStatusEnum.Open;
                return true;
            }

            return true;
        }

        #endregion

        #region RefreshAmounts

        private static void UpdateCurrencyLabel(LabelControl label, decimal value)
        {
            label.Text = value.ToString("c");
            if (value < decimal.Zero)
                label.Appearance.ForeColor = Color.Red;
            else
                label.ForeColor = Color.Black;
        }

        private void RefreshAmounts()
        {
            Model.RefreshProjectAmounts();

            if (Model.ProjectWrapper.ConstructionDetail.LastBillingDate != null)
                View.m_lblLastBillingDate.Text = Model.ProjectWrapper.ConstructionDetail.LastBillingDate.Value.ToString("d");
            else
                View.m_lblLastBillingDate.Text = "N/A";

            if (Model.ProjectWrapper.ConstructionDetail.LastPaymentDate != null)
                View.m_lblLastPaymentDate.Text = Model.ProjectWrapper.ConstructionDetail.LastPaymentDate.Value.ToString("d");
            else
                View.m_lblLastPaymentDate.Text = "N/A";

            UpdateCurrencyLabel(View.m_lblBilledAmount, Model.ProjectWrapper.ConstructionDetail.BilledAmount);
            UpdateCurrencyLabel(View.m_lblCollectedAmount, Model.ProjectWrapper.Project.PaidAmount);
            UpdateCurrencyLabel(View.m_lblCreditAmount2, Model.CreditAmount);
            UpdateCurrencyLabel(View.m_lblOutstandingAmount, Model.OutstandingAmount);
        }

        #endregion

        #region OnBillPayAmountCellStyle

        private void OnBillPayAmountCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (View.m_colBillPayAmount.Name != e.Column.Name 
                && View.m_colBalance.Name != e.Column.Name)
                return;

            QbTransaction transaction = (QbTransaction)View.m_gridViewBillPayTransactions.GetRow(e.RowHandle);

            if (transaction != null)
            {
                if (View.m_colBillPayAmount.Name == e.Column.Name && transaction.TotalAmount < decimal.Zero)
                    e.Appearance.ForeColor = Color.Red;
                else if (View.m_colBalance.Name == e.Column.Name && transaction.Balance < decimal.Zero)
                    e.Appearance.ForeColor = Color.Red;
            }
        }

        #endregion

        #region OnBillPayBalanceData

        private void OnBillPayBalanceData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (View.m_colBalance.Name != e.Column.Name)
                return;

            QbTransaction transaction =
                (QbTransaction)View.m_gridViewBillPayTransactions.GetRow(e.RowHandle);

            e.Value = transaction.Balance;
        }

        #endregion

        #region OnBillPayGridLayout

        private void RefreshBalances()
        {
            decimal currBalance = 0;
            int rowCount = View.m_gridViewBillPayTransactions.DataRowCount;
            for (int rowHandle = (rowCount - 1); rowHandle >= 0; rowHandle--)
            {
                if (rowHandle >= 0)
                {
                    QbTransaction transaction =
                        (QbTransaction)View.m_gridViewBillPayTransactions.GetRow(rowHandle);
                                        
                    currBalance += transaction.TotalAmount;
                    transaction.Balance = currBalance;
                }
            }
        }

        #endregion

        #region OnBillPayTypeChanged

        private void OnBillPayTypeChanged(object sender, EventArgs e)
        {
            RefreshBillPayTransactions();
        }

        #endregion

        #region OnDeclineSignUpDatesValidated

        private void OnDeclineSignUpDatesValidated(object sender, EventArgs e)
        {
            string signUpErrorStr = string.Empty;
            string declineErrorStr = string.Empty;
            string completeErrorStr = string.Empty;
            string jobCostErrorStr = string.Empty;

            if (View.m_dtpDeclineDate.EditValue != null)
            {
                if (View.m_dtpCompleteDate.EditValue != null)
                    completeErrorStr = "Complete Date cannot coexists with Decline Date";                    

                if (View.m_dtpSignUpDate.EditValue != null)
                {
                    signUpErrorStr = "SignUp Date cannot coexists with Decline Date";
                    declineErrorStr = "Decline Date cannot coexists with SignUp Date";
                }
                if (View.m_txtJobCost.EditValue != null 
                    && (decimal)View.m_txtJobCost.EditValue != decimal.Zero)
                {
                    jobCostErrorStr = "Job Cost cannot be greater than 0 if Decline Date is set";
                    if (View.m_dtpSignUpDate.EditValue != null)
                        declineErrorStr = "Decline Date can't be set if Job Cost > 0 or SignUp Date is set";
                    else
                        declineErrorStr = "Decline Date can't be set if Job Cost > 0";
                }
            }

            View.m_errorProvider.SetError(View.m_dtpSignUpDate, signUpErrorStr);
            View.m_errorProvider.SetError(View.m_dtpDeclineDate, declineErrorStr);
            View.m_errorProvider.SetError(View.m_dtpCompleteDate, completeErrorStr);
            View.m_errorProvider.SetError(View.m_txtJobCost, jobCostErrorStr);
        }

        #endregion

        #region OnJobNumberValidated

        private void OnJobNumberValidated(object sender, EventArgs e)
        {
            int maxLenght = 12;

            if (View.m_dtpSignUpDate.EditValue != null && View.m_txtJobNumber.EditValue == null)
            {
                View.m_errorProvider.SetError(View.m_txtJobNumber, "Job Number Required");
                return;
            }
            else if (View.m_txtJobNumber.EditValue != null)
            {
                if (View.m_txtJobNumber.EditValue.ToString().Length > maxLenght)
                {
                    View.m_errorProvider.SetError(View.m_txtJobNumber, "Job Number can't be longer than " + maxLenght);
                    return;
                } else 
                {
                    ProjectConstructionDetail existing =
                        ProjectConstructionDetail.FindByJobNumber(View.m_txtJobNumber.EditValue.ToString());

                    if (existing != null && existing.ProjectId != Model.ProjectWrapper.Project.ID)
                    {
                        View.m_errorProvider.SetError(View.m_txtJobNumber, "Job Number must be unique!");
                        return;
                    }
                }
            }

            View.m_errorProvider.SetError(View.m_txtJobNumber, string.Empty);
        }

        #endregion

        #region OnProjectTypeValidated

        private void OnProjectTypeValidated(object sender, EventArgs e)
        {
            if ((int)View.m_cmbProjectType.EditValue  == 0)
            {
                View.m_errorProvider.SetError(View.m_cmbProjectType, "Required");
            }
        }
        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (Save())
                Model.Print(true);
        }

        #endregion

        #region OnPrintClick

        private void OnPrintClick(object sender, EventArgs e)
        {
            if (Save())
                Model.Print(false);
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnBillPaysGridDoubleClick

        private void OnBillPaysGridDoubleClick(object sender, EventArgs e)
        {
            QbTransaction transaction = SelectedQbTransaction;
            if (transaction == null)
                return;

            OpenBillPayTransaction(transaction);
        }

        #endregion

        #region OnViewKeyDown

        private void OnViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && !View.m_gridBillPayTransactions.Focused)
            {
                OnOkClick(sender, e);
            }
        }

        #endregion

        #region OnGridViewBillPaysKeyDown

        private void OnGridViewBillPaysKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridBillPayTransactions.Focused)
            {
                QbTransaction billPay = SelectedQbTransaction;
                if (billPay != null)
                {
                    OpenBillPayTransaction(billPay);
                }
            }
        }

        #endregion

        #region Save

        private bool Save()
        {
            View.m_errorProvider.ClearErrors();
            View.m_ctlProjectEdit.ClearErrors();

            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
            {
                View.m_tabProjetDetail.SelectedTabPage = View.m_tabpgGeneral;
                return false;
            }
            if (View.m_ctlProjectEdit.HasErrors)
            {
                View.m_tabProjetDetail.SelectedTabPage = View.m_tabpgGeneral;
                View.m_ctlProjectEdit.Focus();
                return false;
            }
          
            Model.ProjectWrapper.Project = View.m_ctlProjectEdit.Project;
            Model.ProjectWrapper.ProjectInsurance = View.m_ctlProjectEdit.ProjectInsurance;

            if (!IsProjectStatusCheckedIn())
                return false;

            Project project = Model.ProjectWrapper.Project;
            ProjectConstructionDetail detail = Model.ProjectWrapper.ConstructionDetail;

            project.ProjectTypeId = (int)View.m_cmbProjectType.EditValue;
            project.Description = View.m_txtNotes.Text;
            project.CreateDate = View.m_dtpLeadDate.DateTime;

            if (View.m_cmbProjectManager.EditValue == null || (int)View.m_cmbProjectManager.EditValue == 0)
                detail.ProjectManagerEmployeeId = null;
            else
                detail.ProjectManagerEmployeeId = (int)View.m_cmbProjectManager.EditValue;

            detail.IsSelfGeneratedLead = View.m_chkSelfGeneratedLead.Checked;

            if (View.m_dtpScopeDate.EditValue != null)
                detail.ScopeDate = View.m_dtpScopeDate.DateTime;
            else
                detail.ScopeDate = null;

            if (View.m_dtpSignUpDate.EditValue != null)
            {
                detail.SignUpDate = View.m_dtpSignUpDate.DateTime;

                if (View.m_txtJobNumber.EditValue != null)
                    detail.JobNumber = View.m_txtJobNumber.EditValue.ToString();
                else
                    detail.JobNumber = null;
            }
            else
            {
                detail.SignUpDate = null;
                detail.JobNumber = null;
            }

            if (View.m_dtpDeclineDate.EditValue != null)
                detail.DeclineDate = View.m_dtpDeclineDate.DateTime;
            else
                detail.DeclineDate = null;

            if (View.m_dtpCompleteDate.EditValue != null)
                detail.ActualCompletionDate = View.m_dtpCompleteDate.DateTime;
            else
                detail.ActualCompletionDate = null;

            if (View.m_txtEstimatedAmount.EditValue != null)
                detail.EstimatedAmount = (decimal)View.m_txtEstimatedAmount.EditValue;
            else
                detail.EstimatedAmount = decimal.Zero;

            if (View.m_txtJobCost.EditValue != null)
                detail.JobCost = (decimal)View.m_txtJobCost.EditValue;
            else
                detail.JobCost = decimal.Zero;

            try
            {
                Database.Begin();
                Model.InsertUpdateProject();
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
            
            View.Destroy();

            return true;
        }

        #endregion
    }
}
