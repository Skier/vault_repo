using System;
using System.Windows.Forms;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Banking.WriteCheck;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.ManageCheck
{
    public class ManageCheckController : SingleFormController<ManageCheckModel, ManageCheckView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.CurrentAccount = (Account) data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_table.Enter += new CellValueHandler(OnTableEnter);
            
            View.m_menuAddCheck.Click += new EventHandler(OnAddCheckClick);
            View.m_menuViewEditCheck.Click += new EventHandler(OnViewEditClick);
            View.m_menuDelete.Click += new EventHandler(OnDeleteClick);
        }

        #endregion               

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();
            
            View.m_lblAccount.Text = Model.CurrentAccount.Name;

            View.m_table.BindModel(Model);
            View.m_table.AddColumn(new TableColumn(0, 0, new CheckTableCellRenderer(),
                                                   null, new CheckTableHeaderCellRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 0, new CheckTableCellRenderer(),
                                                   null, new CheckTableHeaderCellRenderer()));
            View.m_table.AddColumn(new TableColumn(2, 0, new CheckTableCellRenderer(),
                                                   null, new CheckTableHeaderCellRenderer()));

            View.m_table.GetColumn(0).Width = 10;
            View.m_table.GetColumn(2).Width = 80;            
            
            View.m_table.Focus();
            if (Model.GetRowCount() > 0)
            {
                View.m_table.Select(0, 1);                                
            }

            UpdateMenuItems();            
        }

        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            UpdateMenuItems();
        }

        #endregion

        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            if (View.m_menuViewEditCheck.Enabled)
                OnViewEditClick(this, EventArgs.Empty);                
        }

        #endregion        

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            OnViewEditClick(this, EventArgs.Empty);
        }

        #endregion

        #region OnAddCheckClick

        private void OnAddCheckClick(object sender, EventArgs e)
        {
            WriteCheckController writeCheckController
                = SingleFormController.Prepare<WriteCheckController>(
                    EnteredFromEnum.ManageChecks, null, false, Model.CurrentAccount);
            writeCheckController.Closed += new SingleFormClosedHandler(OnAddCheckClosed);
            writeCheckController.Execute();                                    
        }

        #endregion

        #region OnAddCheckClosed

        private void OnAddCheckClosed(SingleFormController controller)
        {
            View.m_table.Focus();

            WriteCheckController writeCheckController = (WriteCheckController) controller;            
            if (!writeCheckController.IsCancelled)
            {
                Model.AddInList(writeCheckController.CreatedCheck);
                View.m_table.Select(0, 1);
            }
             
            UpdateMenuItems();
        }

        #endregion        

        #region OnViewEditClick

        private void OnViewEditClick(object sender, EventArgs e)
        {
            WriteCheckController writeCheckController
                = SingleFormController.Prepare<WriteCheckController>(
                    EnteredFromEnum.ManageChecks,
                    Model.GetObjectAt(View.m_table.CurrentRowIndex, 0),
                    View.m_menuViewEditCheck.Text == "View" ? true : false,
                    Model.CurrentAccount);
            
            writeCheckController.Closed += new SingleFormClosedHandler(OnViewEditCheckClosed);
            writeCheckController.Execute();                                                
        }

        #endregion

        #region OnViewEditCheckClosed

        private void OnViewEditCheckClosed(SingleFormController controller)
        {
            WriteCheckController writeCheckController = (WriteCheckController)controller;
            if (!writeCheckController.IsCancelled)
                Model.ChangeInList(writeCheckController.CreatedCheck);

            View.m_table.Focus();
            UpdateMenuItems();
        }

        #endregion        

        #region OnDeleteClick

        private void OnDeleteClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question,
                "Do you really want to delete check?")
                    == DialogResult.No)            
                return;
            
            Model.Delete((Check) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));    
            UpdateMenuItems();
        }

        #endregion        

        #region UpdateMenuItems

        private void UpdateMenuItems()
        {
            if (View.m_table.CurrentRowIndex < 0 || Model.GetRowCount() == 0)
            {
                View.m_menuViewEditCheck.Text = "View";
                DefaultActionName = View.m_menuViewEditCheck.Text;
                IsDefaultActionExist = false;
                View.m_menuDelete.Enabled = false;
                View.m_menuViewEditCheck.Enabled = false;
                return;
            }

            Check check
                = (Check)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

            if (check.EntityState == EntityState.Created)
            {
                View.m_menuViewEditCheck.Text = "Edit";
                View.m_menuDelete.Enabled = true;
            }                
            else
            {
                View.m_menuViewEditCheck.Text = "View";
                View.m_menuDelete.Enabled = false;
            }

            DefaultActionName = View.m_menuViewEditCheck.Text;            
            View.m_menuViewEditCheck.Enabled = true;                                    
            IsDefaultActionExist = true;                                    
        }

        #endregion        
    }
}
