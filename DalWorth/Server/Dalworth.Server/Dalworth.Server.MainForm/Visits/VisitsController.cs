using System;
using System.Collections.Generic;
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
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.TaskEdit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.UI;
using Control=System.Windows.Forms.Control;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.Visits
{
    public class VisitsController : NestedController<VisitsModel, VisitsView>
    {
        private bool m_disableAutoFilter = false;
        private MainFormController m_mainFormController;

        #region FocusedVisit

        private VisitWrapper FocusedVisit
        {
            get
            {                
                if (View.m_gridViewVisits.FocusedRowHandle < 0)
                    return null;

                return (VisitWrapper)View.m_gridViewVisits.GetRow(
                    View.m_gridViewVisits.FocusedRowHandle);
            }
        }

        #endregion

        #region SelectedVisits

        private List<VisitWrapper> SelectedVisits
        {
            get
            {
                List<VisitWrapper> result = new List<VisitWrapper>();

                foreach (int rowHandle in View.m_gridViewVisits.GetSelectedRows())
                    result.Add((VisitWrapper)View.m_gridViewVisits.GetRow(rowHandle));
                return result;
            }
        }

        #endregion


        #region FocusedTask

        private TaskWrapper FocusedTask
        {
            get
            {
                if (View.m_gridViewTasks.FocusedRowHandle < 0)
                    return null;

                return (TaskWrapper)View.m_gridViewTasks.GetRow(
                    View.m_gridViewTasks.FocusedRowHandle);
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
            View.m_gridViewVisits.FocusedRowChanged += OnVisitsFocusedRowChanged;            
            View.m_gridViewVisits.ColumnFilterChanged += OnVisitsColumnFilterChanged;

            View.m_gridVisitsDashboardLink.Click += OnDashboardLinkClick;
            View.m_gridTasksProjectLink.Click += OnProjectLinkClick;
            
            View.m_txtVisitId.KeyDown += OnFieldsKeyDown;
            View.m_txtPhoneNo.KeyDown += OnFieldsKeyDown;
            View.m_txtCustomer.KeyDown += OnFieldsKeyDown;
            View.m_txtAddress.KeyDown += OnFieldsKeyDown;
            View.m_txtServmanTicketNumber.KeyDown += OnFieldsKeyDown;

            View.m_txtServmanTicketNumber.TextChanged += OnTicketNumberChanged;

            View.m_cmbStatus.SelectedIndexChanged += OnFiltersChanged;
            View.m_cmbTechnician.SelectedIndexChanged += OnFiltersChanged;
            View.m_dateRange.DateRangeValueChanged += OnFiltersChanged;

            View.m_btnClear.Click += OnClearClick;
            View.m_btnRefresh.Click += OnRefreshClick;
            View.m_ctlCustomerEdit.Modified += OnCustomerModified;
            View.m_ctlAddressEdit.Modified += OnAddressModified;
            View.m_btnCreateVisit.Click += OnCreateVisitClick;
            View.m_btnEditVisit.Click += OnEditVisitClick;
            View.m_btnPrintVisits.Click += OnPrintVisitsClick;

            View.m_gridViewVisits.KeyDown += OnGridVisitsKeyDown;
            View.m_gridViewTasks.KeyDown += OnGridTasksKeyDown;
            View.m_gridViewTasks.FocusedRowChanged += OnTasksFocusedRowChanged;

            View.m_gridVisits.DataSource = Model.Visits;
            View.m_txtCustomer.Focus();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_ctlAddressEdit.BaseAddressName = "Customer address";
            View.m_ctlAddressEdit.Caption = "Visit Address";
            View.m_gridVisitsServiceDateEdit.MinValue = DateTime.Now;

            foreach (Employee technician in Model.Technicians)
            {
                View.m_cmbTechnician.Properties.Items.Add(
                    new ImageComboBoxItem(technician.DisplayName, (object)technician.ID));
            }
        }

        #endregion        

        #region Redirection on enter

        private void OnGridVisitsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridVisits.Focused
                && View.m_gridViewVisits.FocusedColumn.Name == View.m_colLinkDashboard.Name)
            {
                VisitWrapper visit = FocusedVisit;
                if (visit != null)
                    OnDashboardLinkClick(null, null);
            }
        }

        private void OnGridTasksKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridTasks.Focused
                && View.m_gridViewTasks.FocusedColumn.Name == View.m_colLinkProject.Name)
            {
                TaskWrapper task = FocusedTask;
                if (task != null)
                    OnProjectLinkClick(null, null);
            }
        }

        #endregion       

        #region SelectVisit


        public void SelectVisit(Visit visit, bool singleVisit)
        {
            if (visit == null)
            {
                VisitWrapper prevFocusedVisit = FocusedVisit;
                RefreshData(null);
                if (prevFocusedVisit != null)
                    TryToSelectVisit(prevFocusedVisit.Visit);
                return;
            }
            
            if (singleVisit)
                RefreshData(visit.ID);
            else
                RefreshData(null);
            
            TryToSelectVisit(visit);
        }

        private void TryToSelectVisit(Visit visit)
        {            
            for (int rowIndex = 0; rowIndex < Model.Visits.Count; rowIndex++)
            {
                if (Model.Visits[rowIndex].Visit.ID == visit.ID)
                {
                    int rowHandle = View.m_gridViewVisits.GetRowHandle(rowIndex);
                    if (rowHandle >= 0)
                    {
                        View.m_gridViewVisits.ClearSelection();
                        View.m_gridViewVisits.FocusedRowHandle = rowHandle;                        
                        View.m_gridViewVisits.SelectRow(rowHandle);
                        return;
                    } 
                }
            }

            if (Model.Visits.Count > 0)
                TryToSelectVisit(Model.Visits[0].Visit);            
        }

        #endregion

        #region Link handlers to Dashboard and Project

        private void OnDashboardLinkClick(object sender, EventArgs e)
        {
            m_mainFormController.ShowDashboard(FocusedVisit.Visit);
        }

        private void OnProjectLinkClick(object sender, EventArgs e)
        {
            Project project = Project.FindByPrimaryKey(FocusedTask.Task.ProjectId);
            m_mainFormController.ShowProjectsForm(project);
        }

        #endregion

        #region OnVisitsFocusedRowChanged

        private void OnVisitsColumnFilterChanged(object sender, EventArgs e)
        {
            OnVisitsFocusedRowChanged(null, null);
        }

        private void OnVisitsFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {            
            VisitWrapper visit = FocusedVisit;
            if (visit == null)
            {
                View.m_gridTasks.DataSource = null;

                View.m_ctlCustomerEdit.Customer = null;
                View.m_ctlCustomerEdit.Address = null;

                View.m_ctlAddressEdit.Customer = null;
                View.m_ctlAddressEdit.BaseAddress = null;

                View.m_txtVisitNotes.Text = string.Empty;
                View.m_txtTaskNotes.Text = string.Empty;
            } else
            {
                View.m_gridTasks.DataSource = Model.GetTasks(visit.Visit);

                View.m_ctlCustomerEdit.Customer = visit.Customer;
                View.m_ctlCustomerEdit.Address = visit.CustomerAddress;

                View.m_ctlAddressEdit.Customer = visit.Customer;
                View.m_ctlAddressEdit.BaseAddress = visit.CustomerAddress;
                View.m_ctlAddressEdit.CurrentAddress = visit.ServiceAddress;
                if (visit.ServiceAddress != null && visit.CustomerAddress != null 
                    && visit.ServiceAddress.ID == visit.CustomerAddress.ID)
                {
                    View.m_ctlAddressEdit.IsBaseAddressActive = true;
                }                    
                else
                    View.m_ctlAddressEdit.IsBaseAddressActive = false;

                View.m_ctlAddressEdit.Enabled = visit.Visit.IsEditAllowed && visit.ServiceAddress != null;

                View.m_txtVisitNotes.Text = visit.Notes;
                ReshowTaskNote();
            }
        }

        #endregion        

        #region OnTasksFocusedRowChanged

        private void ReshowTaskNote()
        {
            TaskWrapper task = FocusedTask;
            if (task == null)
                View.m_txtTaskNotes.Text = string.Empty;
            else
                View.m_txtTaskNotes.Text = task.Task.Notes;            
        }

        private void OnTasksFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ReshowTaskNote();
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

            if (((Control)sender).Name == View.m_txtServmanTicketNumber.Name
                && View.m_txtServmanTicketNumber.Text.Length > 0
                && View.m_txtServmanTicketNumber.Text.Length < 6)
            {
                return;
            }

            VisitWrapper selectedVisit = FocusedVisit;

            int visitId = 0;
            try
            {
                visitId = int.Parse(View.m_txtVisitId.Text);
            }
            catch (Exception) { }


            if (visitId == 0)
                RefreshData(null);
            else
                RefreshData(visitId);
            
            if (selectedVisit != null)
                TryToSelectVisit(selectedVisit.Visit);
        }

        #endregion

        #region RefreshData

        public void RefreshData(int? exactVisitId)
        {
            string firstName = Utils.ParseFirstName(View.m_txtCustomer.Text);
            string lastName = Utils.ParseLastName(View.m_txtCustomer.Text);

            string city = Utils.ParseCity(View.m_txtAddress.Text);
            string zip = Utils.ParseZip(View.m_txtAddress.Text);
            string block = Utils.ParseBlock(View.m_txtAddress.Text);
            string street = Utils.ParseStreet(View.m_txtAddress.Text);

            string phoneNo = Utils.ExtractDigits(View.m_txtPhoneNo.Text);

            Model.UpdateVisits(exactVisitId, 
                View.m_txtServmanTicketNumber.Text,
                firstName, lastName, phoneNo,
                city, zip, street, block,
                View.m_cmbStatus.SelectedIndex == 0 ? null
                     : (VisitStatusEnum?)View.m_cmbStatus.SelectedIndex,
                View.m_cmbTechnician.SelectedIndex == 0 ? null 
                     : (int?)View.m_cmbTechnician.EditValue,
                View.m_dateRange.EditValue);

            View.m_gridVisits.DataSource = Model.Visits;
            OnVisitsFocusedRowChanged(null, null);
        }

        #endregion        

        #region OnCreateVisitClick

        private void OnCreateVisitClick(object sender, EventArgs e)
        {
            using (CreateVisitController controller = Prepare<CreateVisitController>())
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    if (Model.Visits.Count == 0)
                        SelectVisit(controller.AffectedVisit, true);
                    else
                        SelectVisit(controller.AffectedVisit, false);
                }
                    
            }
        }

        #endregion

        #region OnEditVisitClick

        private void OnEditVisitClick(object sender, EventArgs e)
        {
            VisitWrapper visit = FocusedVisit;
            if (visit == null)
            {
                XtraMessageBox.Show("Please select Visit to Edit", "No Visit selected", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                return;
            }

            using (CreateVisitController controller = Prepare<CreateVisitController>(visit.Visit))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                    SelectVisit(controller.AffectedVisit, false);
            }
        }

        #endregion

        #region OnPrintVisitsClick

        private void OnPrintVisitsClick(object sender, EventArgs e)
        {
            List<VisitWrapper> selectedVisits = SelectedVisits;
            if (selectedVisits.Count == 0)
            {
                XtraMessageBox.Show("Please select Visits to Print", "No Visits selected", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                return;                
            }

            using (new WaitCursor())
            {
                foreach (VisitWrapper visit in selectedVisits)
                {
                    try
                    {
                        VisitSummaryPackage summaryPackage = new VisitSummaryPackage(visit.Visit);
                        summaryPackage.Print();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Unable to print visit",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }                
            }
        }

        #endregion

        #region OnRefreshClick

        private void OnRefreshClick(object sender, EventArgs e)
        {
            VisitWrapper selectedVisit = FocusedVisit;
            RefreshData(null);
            if (selectedVisit != null)
                TryToSelectVisit(selectedVisit.Visit);
        }

        #endregion

        #region OnClearClick

        private void ClearFilters()
        {
            m_disableAutoFilter = true;
            View.m_txtVisitId.Text = string.Empty;
            View.m_txtServmanTicketNumber.Text = string.Empty;
            View.m_cmbTechnician.SelectedIndex = 0;
            View.m_txtPhoneNo.Text = string.Empty;
            View.m_txtCustomer.Text = string.Empty;
            View.m_txtAddress.Text = string.Empty;
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

                if (FocusedVisit.VisitStatus == VisitStatusEnum.Pending)
                    PendingTaskGridState.MakePendingTaskGridDirty(Configuration.CurrentDispatchId);
                else
                    DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            VisitWrapper visit = FocusedVisit;

            if (visit.ServiceAddress.ID == visit.CustomerAddress.ID)
                visit.ServiceAddress = (Address)visit.CustomerAddress.Clone();
            Model.Visits.ResetBindings();
            OnVisitsFocusedRowChanged(null, null);            
        }

        #endregion

        #region OnAddressModified

        private void OnAddressModified(Address baseAddress, Address currentAddress, bool isBaseAddressActive)
        {
            VisitWrapper focusedVisit = FocusedVisit;
            try
            {
                Database.Begin();                
                
                if (isBaseAddressActive)
                {
                    focusedVisit.Visit.ServiceAddressId = baseAddress.ID;
                    focusedVisit.ServiceAddress = (Address)baseAddress.Clone();
                    Visit.Update(focusedVisit.Visit);
//                    if (currentAddress.ID != baseAddress.ID)
//                    {
//                        CustomerAddressAdditional.Delete(
//                            new CustomerAddressAdditional(
//                            selectedVisit.Visit.CustomerId.Value,
//                            currentAddress.ID));
//                        Address.Delete(currentAddress);
//                    }                        
                }
                else
                {
                    focusedVisit.ServiceAddress = currentAddress;
                    focusedVisit.Visit.ServiceAddressId = focusedVisit.ServiceAddress.ID;
                    Visit.Update(focusedVisit.Visit);
                }
                Database.Commit();

                if (focusedVisit.VisitStatus == VisitStatusEnum.Pending)
                    PendingTaskGridState.MakePendingTaskGridDirty(Configuration.CurrentDispatchId);
                else
                    DashboardState.MakeDashboardDirty(Configuration.CurrentDispatchId);                
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            Model.Visits.ResetBindings();
            OnVisitsFocusedRowChanged(null, null);            
        }


        #endregion        
    }
}
