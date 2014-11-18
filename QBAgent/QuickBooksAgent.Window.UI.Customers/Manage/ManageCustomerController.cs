using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Phone;
using QuickBooksAgent.Data;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI.Customers.Edit;
using QuickBooksAgent.Windows.UI.CustomerOperations.Invoice;
using QuickBooksAgent.Windows.UI.CustomerOperations.InvoiceSelection;
using QuickBooksAgent.Domain;
using System.Diagnostics;

namespace QuickBooksAgent.Windows.UI.Customers.Manage
{    
    public class ManageCustomerController:SingleFormController<ManageCustomerModel,ManageCustomerView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.Enter += new CellValueHandler(OnTableEnter);
            View.m_table.DoubleClick += new EventHandler(OnTableDoubleClick);
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_txtSearch.TextChanged += new System.EventHandler(this.OnTextChanged);

            View.m_menuAdd.Click += new EventHandler(OnAddClick);
            View.m_menuEdit.Click += new EventHandler(OnEditClick);
            View.m_menuDelete.Click += new EventHandler(OnDeleteClick);
            View.m_menuUndo.Click += new EventHandler(OnUndoClick);
            View.m_menuShowInvoices.Click += new EventHandler(OnShowInvoiceClick);
            View.m_menuNewInvoice.Click += new EventHandler(OnAddInvoiceClick);
        }

        #endregion

        #region OnTableRowChanged

        void OnTableRowChanged(int rowIndex)
        {
            UpdateControls(rowIndex);
        }

        #endregion  

        #region OnTextChanged

        private void OnTextChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion
      
        #region UpdateControls

        void UpdateControls(int rowIndex)
        {
            // temparary
            if (rowIndex < 0 || Model.GetRowCount() == 0)
            {
                View.m_menuUndo.Enabled = View.m_menuEdit.Enabled = View.m_menuDelete.Enabled = false;
                View.m_menuShowInvoices.Enabled = View.m_menuNewInvoice.Enabled = false;
                View.m_menuCall.Enabled = false;
                return;
            }

            Customer customer = (Customer)Model.GetObjectAt(rowIndex, 0);

            View.m_menuUndo.Enabled =
                (customer.EntityState == EntityState.Modified);

            View.m_menuDelete.Enabled =
                (customer.EntityState == EntityState.Created);

            View.m_menuShowInvoices.Enabled = View.m_menuNewInvoice.Enabled =
                (customer.EntityState != EntityState.Created);

            View.m_menuEdit.Enabled = true;
            
            //Phone            
            foreach (MenuItem item in View.m_phoneMenus.Keys)
                item.Click -= new EventHandler(OnPhoneItemClick);
            
            View.m_phoneMenus.Clear();
            View.m_menuCall.MenuItems.Clear();
            
            MenuItem currentPhoneItem;
                        
            if (customer.Phone != null && customer.Phone != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Phone: " + customer.Phone;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, customer.Phone);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (customer.Mobile != null && customer.Mobile != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Mobile: " + customer.Mobile;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, customer.Mobile);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (customer.Fax != null && customer.Fax != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Fax: " + customer.Fax;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, customer.Fax);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (customer.Pager != null && customer.Pager != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Pager: " + customer.Pager;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, customer.Pager);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (customer.AltPhone != null && customer.AltPhone != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Other: " + customer.AltPhone;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, customer.AltPhone);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (View.m_phoneMenus.Count == 0)
                View.m_menuCall.Enabled = false;
            else
                View.m_menuCall.Enabled = true;                                                    
        }

        #endregion 

        #region OnPhoneItemClick

        private void OnPhoneItemClick(object sender, EventArgs e)
        {
            try
            {
                Phone.MakeCall(View.m_phoneMenus[(MenuItem) sender] + "\0", true);
            }
            catch (Exception ex)
            {
                MessageDialog.Show(MessageDialogType.Information, string.Format("Couldn't make phone call: {0}", ex.Message));
            }
        }

        #endregion
        

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_table.AddColumn(new TableColumn(0, 0, new ManageCustomerTableCellRenderer(), null, 
                new ManageCustomerTableHeaderRenderer()));            
            View.m_table.AddColumn(new TableColumn(1, 0, new ManageCustomerTableCellRenderer(), null, 
                new ManageCustomerTableHeaderRenderer()));            
            View.m_table.AddColumn(new TableColumn(2, 0, new ManageCustomerTableCellRenderer(), null, 
                new ManageCustomerTableHeaderRenderer()));            

            View.m_table.GetColumn(0).Width = 10;
            View.m_table.GetColumn(2).Width = 70;

            View.m_table.BindModel(Model);


            if (View.m_table.Model.GetRowCount() > 0)
            {
                View.m_table.Select(0);
            } 
        }

        #endregion        

        #region OnViewActivated

        public override void OnViewActivated()
        {
            base.OnViewActivated();

            View.m_table.Focus();
        }

        #endregion

        #region AddCustomer

        private void AddCustomer()
        {
            EditCustomerController editCustomerController =
                    SingleFormController.Prepare<EditCustomerController>(Model.CustomerList);

            editCustomerController.Model.CustomerAffected += 
                new CustomerEditAffectHandler(CustomerEditAffectHandler);

            editCustomerController.Execute();

        }

        #endregion

        #region CustomerEditAffectHandler

        void CustomerEditAffectHandler(Customer customer) 
        {
            Model.CurrentCustomer = customer; 

            Model.Update();

            int currentIndex = Model.GetCurrentIndex();

            if (currentIndex >= 0)
                View.m_table.Select(currentIndex);

            View.m_table.Focus();
        }

        #endregion

        #region EditCustomer

        private void EditCustomer(bool isReadOnly)
        {
            if (View.m_table.CurrentRowIndex < 0 || Model.GetRowCount() == 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please select customer.");
                return;
            }

            EditCustomerController editCustomerController =
                    SingleFormController.Prepare<EditCustomerController>(Model.CustomerList,
                    (Customer)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0), isReadOnly);

            editCustomerController.Model.CustomerAffected += 
                new CustomerEditAffectHandler(CustomerEditAffectHandler);

            editCustomerController.Execute();          
        }

        #endregion

        #region OnDeleteClick

        void OnDeleteClick(object sender, EventArgs e)
        {
            if (Model.CustomerList[View.m_table.CurrentRowIndex].EntityState != EntityState.Created)
                return;
            
            if (MessageDialog.Show(MessageDialogType.Question,
                "Do you really want to delete this customer. Continue?") 
                    == DialogResult.Yes)
                try
                {
                    Model.Delete(View.m_table.CurrentRowIndex);

                    UpdateControls(View.m_table.CurrentRowIndex);
                }
                catch (Exception ex)
                {
                    EventService.AddEvent(new QuickBooksAgentException(
                       "Unable to delete a customer", ex));
                }        
        }

        #endregion 

        #region OnShowInvoiceClick

        void OnShowInvoiceClick(object sender, EventArgs e)
        {
            ManageInvoices();
        }

        #endregion

        #region OnAddInvoiceClick

        void OnAddInvoiceClick(object sender, EventArgs e)
        {
            if (View.m_table.CurrentRowIndex < 0 || Model.GetRowCount() == 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please select customer.");
                return;
            }

            Customer selectedCustomer = (Customer)Model.GetObjectAt(View.m_table.CurrentRowIndex
                    , 0);

            if (selectedCustomer.ModifiedCustomerId != null)
                selectedCustomer = Customer.FindByPrimaryKey(selectedCustomer.ModifiedCustomerId.Value);            

            using (InvoiceController invoiceController =
                SingleFormController.Prepare<InvoiceController>(
                    selectedCustomer))
            {
                invoiceController.Execute();
            }
        }

        #endregion

        #region ManageInvoices

        void ManageInvoices()
        {
            if (View.m_table.CurrentRowIndex < 0 || Model.GetRowCount() == 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please select customer.");
                return;
            }

            Customer selectedCustomer = (Customer)Model.GetObjectAt(View.m_table.CurrentRowIndex
                    ,0);

            if (selectedCustomer.ModifiedCustomerId != null)
                selectedCustomer = Customer.FindByPrimaryKey(selectedCustomer.ModifiedCustomerId.Value);

            using (InvoiceSelectionController invoiceSelectionController =
                SingleFormController.Prepare<InvoiceSelectionController>(
                    selectedCustomer))
            {
                invoiceSelectionController.Execute();
            }
        }

        #endregion

        #region OnAddClick

        void OnAddClick(object sender, EventArgs e)
        {
            AddCustomer();            
        }

        #endregion

        #region OnEditClick

        void OnEditClick(object sender, EventArgs e)
        {
            EditCustomer(false);            
        }

        #endregion

        #region OnDoneClick

        void OnDoneClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion 
       
        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            EditCustomer(true);
        }

        #endregion

        #region OnTableDoubleClick

        private void OnTableDoubleClick(object sender, EventArgs e)
        {
            EditCustomer(true);
        }

        #endregion


        #region OnUndoClick

        void OnUndoClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question,
                "All changes since last syncronization will be lost. Continue?") 
                    == DialogResult.Yes)
                try
                {
                    Model.Undo(View.m_table.CurrentRowIndex);

                    Model.CurrentCustomer = (Customer)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

                    Model.Update();

                    View.m_table.Select(Model.GetCurrentIndex());
                }
                catch(Exception ex)
                {
                    EventService.AddEvent(new QuickBooksAgentException(
                        "Unable to undo changes for customer", ex));
                }
        }

        #endregion       

        #region IsDefaultActionExist
        public override bool IsDefaultActionExist
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region DefaultActionName
        public override string DefaultActionName
        {
            get
            {
                return "View";
            }
        }
        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            EditCustomer(true);            
        }

        #endregion

        #region Search

        void Search()
        {
            int selectIndex = Model.Search(View.m_txtSearch.Text);

            if (selectIndex != -1)
                View.m_table.Select(selectIndex);
            else if (String.Empty.Equals(View.m_txtSearch.Text) &&
                Model.GetRowCount() > 0)
                View.m_table.Select(0);
        }

        #endregion

        #region Renderer Classes

        private class ManageCustomerTableCellRenderer : DefaultTableCellRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                DrawControl drawControl =
                    base.getTableCellRendererComponent(
                    table,
                    value,
                    isSelected,
                    hasFocus,
                    row,
                    column);

                if (column == 0)
                    drawControl.StringFormat.Alignment = StringAlignment.Center;
                else if (column == 1)
                    drawControl.StringFormat.Alignment = StringAlignment.Near;
                else if (column == 2)
                    drawControl.StringFormat.Alignment = StringAlignment.Far;
                                                
                return drawControl;
            }

        }
        
        private class ManageCustomerTableHeaderRenderer : DefaultTableHeaderRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                DrawControl drawControl =
                    base.getTableCellRendererComponent(
                    table,
                    value,
                    isSelected,
                    hasFocus,
                    row,
                    column);

                drawControl.StringFormat.Alignment = StringAlignment.Center;

                return drawControl;
            }
        }

        #endregion
    }
}
