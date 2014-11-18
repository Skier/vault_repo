using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Data;
using QuickBooksAgent.Windows.UI.Banking.ManageCheck;
using QuickBooksAgent.Windows.UI.Banking.ManageCreditCard;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Domain;
using System.Diagnostics;


namespace QuickBooksAgent.Windows.UI.DatabaseManager.Accounts
{
    public class AccountsController : SingleFormController<AccountsModel, AccountsView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {            
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_menuManageChecks.Click += new EventHandler(OnManageChecksClick);
            View.m_menuManageCCCharges.Click += new EventHandler(OnManageCCChargesClick);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_table.AddColumn(new TableColumn(0, 0, new AccountTableCellRenderer(),
                                                   null, new AccountTableHeaderCellRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 0, new AccountTableCellRenderer(),
                                                   null, new AccountTableHeaderCellRenderer()));
            View.m_table.GetColumn(1).Width = 80;
            View.m_table.BindModel(Model);            

            if (Model.GetRowCount() > 0)
            {
                View.m_table.Select(0);
                View.m_table.Focus();
                UpdateMenu();
            }

            IsDefaultActionExist = false;
            DefaultActionName = "None";
        }

        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            UpdateMenu();
        }

        #endregion

        #region UpdateMenu

        private void UpdateMenu()
        {
            if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
            {
                Account account 
                    = (Account) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);
                
                    View.m_menuManageChecks.Enabled
                        = account.AccountType == AccountType.Bank;
                
                    View.m_menuManageCCCharges.Enabled
                        = account.AccountType == AccountType.CreditCard;
            }
        }

        #endregion                

        #region OnManageCCChargesClick

        private void OnManageCCChargesClick(object sender, EventArgs e)
        {
            Account account = (Account)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);
            
            ManageCreditCardController manageCreditCardController
                = SingleFormController.Prepare<ManageCreditCardController>(account);
            manageCreditCardController.Closed += new SingleFormClosedHandler(OnManagerClosed);
            manageCreditCardController.Execute();
        }

        #endregion
        
        #region OnManageChecksClick

        private void OnManageChecksClick(object sender, EventArgs e)
        {
            Account account = (Account) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);
            
            ManageCheckController manageCheckController
                = SingleFormController.Prepare<ManageCheckController>(account);
            manageCheckController.Closed += new SingleFormClosedHandler(OnManagerClosed);
            manageCheckController.Execute();
        }        

        #endregion

        #region OnManagerClosed

        private void OnManagerClosed(SingleFormController controller)
        {
            View.m_table.Focus();
        }

        #endregion

        #region Renderer Classes

        private class AccountTableCellRenderer : DefaultTableCellRenderer
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

                drawControl.StringFormat.Alignment =
                    (column == 0) ? System.Drawing.StringAlignment.Near :
                        System.Drawing.StringAlignment.Far;

                return drawControl;
            }

        }

        private class AccountTableHeaderCellRenderer : DefaultTableHeaderRenderer
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

                drawControl.BackColor = System.Drawing.Color.LightGray;

                drawControl.StringFormat.Alignment = System.Drawing.StringAlignment.Center;
                
                return drawControl;
            }

        }
        
        #endregion
    }
}
