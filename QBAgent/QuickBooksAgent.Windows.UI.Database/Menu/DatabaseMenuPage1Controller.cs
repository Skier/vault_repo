using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Banking.Menu;
using QuickBooksAgent.Windows.UI.Banking.WriteCheck;
using QuickBooksAgent.Windows.UI.DatabaseManager.Accounts;
using QuickBooksAgent.Windows.UI.DatabaseManager.Employees;
using QuickBooksAgent.Windows.UI.DatabaseManager.Vendors;
using QuickBooksAgent.Windows.UI.DatabaseManager.Items;
using QuickBooksAgent.Windows.UI.Customers.Manage;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Menu
{
    public class DatabaseMenuPage1Controller:SingleFormController<DatabaseMenuPage1Model,DatabaseMenuPage1View>
    {
        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_mbCustomers.Click += new EventHandler(OnCustomerClick);
            View.m_mbEmployees.Click += new EventHandler(OnEmployeesClick);
            View.m_mbNext.Click += new EventHandler(OnNextClick);
            View.m_mbBanking.Click += new EventHandler(OnBankingClick);

            View.m_menuCustomers.Click += new EventHandler(OnCustomerClick);
            View.m_menuBanking.Click += new EventHandler(OnBankingClick);
            View.m_menuEmployee.Click += new EventHandler(OnEmployeesClick);
            View.m_menuVendors.Click += new EventHandler(OnVendorsClick);
            View.m_menuItems.Click += new EventHandler(OnItemsClick);
            View.m_menuAccounts.Click += new EventHandler(OnAccountsClick);
            View.m_menuWriteCheck.Click += new EventHandler(OnWriteCheckClick);

            IsDefaultActionExist = false;
            DefaultActionName = "None";            
            
            View.m_mbCustomers.Select();
        }

        void OnVendorsClick(object sender, EventArgs e)
        {
            VendorsController vendorsController
                = SingleFormController.Prepare<VendorsController>();
            vendorsController.Closed += new SingleFormClosedHandler(OnVendorClosed);
            vendorsController.Execute();
        }

        void OnVendorClosed(SingleFormController controller)
        {
            View.m_mbNext.Select();
        }        
        
        private void OnWriteCheckClick(object sender, EventArgs e)
        {
            WriteCheckController writeCheckController
                = SingleFormController.Prepare<WriteCheckController>(null, null, false, null);
            writeCheckController.Closed += new SingleFormClosedHandler(OnWriteCheckClosed);
            writeCheckController.Execute();
        }

        void OnWriteCheckClosed(SingleFormController controller)
        {
            View.m_mbBanking.Select();
        }                               

        private void OnBankingClick(object sender, EventArgs e)
        {
            BankingMenuController bankingMenuController
                = SingleFormController.Prepare<BankingMenuController>();
            bankingMenuController.Closed += new SingleFormClosedHandler(OnBankingClosed);
            bankingMenuController.Execute();                        
        }

        void OnBankingClosed(SingleFormController controller)
        {
            View.m_mbBanking.Select();
        }                       

        void OnItemsClick(object sender, EventArgs e)
        {
            ItemsController itemsController
                = SingleFormController.Prepare<ItemsController>();
            itemsController.Closed += new SingleFormClosedHandler(OnItemsClosed);
            itemsController.Execute();
     
        }

        void OnItemsClosed(SingleFormController controller)
        {
            View.m_mbNext.Select();
        }               

        void OnCustomerClick(object sender, EventArgs e)
        {
            ManageCustomerController manageCustomerController
                = SingleFormController.Prepare<ManageCustomerController>();
            manageCustomerController.Closed += new SingleFormClosedHandler(OnCustomerClosed);
            manageCustomerController.Execute();
        }

        void OnCustomerClosed(SingleFormController controller)
        {
            View.m_mbCustomers.Select();
        }

        void OnEmployeesClick(object sender, EventArgs e)
        {
            EmployeesController employeesController
                = SingleFormController.Prepare<EmployeesController>();
            employeesController.Closed += new SingleFormClosedHandler(OnEmployeeClose);
            employeesController.Execute();
        }

        void OnEmployeeClose(SingleFormController controller)
        {
            View.m_mbEmployees.Select();
        }

        void OnNextClick(object sender, EventArgs e)
        {
            DatabaseMenuPage2Controller databaseMenuPage2Controller
                = SingleFormController.Prepare<DatabaseMenuPage2Controller>();
            databaseMenuPage2Controller.Closed += new SingleFormClosedHandler(OnNextClosed);            
            databaseMenuPage2Controller.Execute();            
        }

        void OnNextClosed(SingleFormController controller)
        {            
            View.Destroy();
        }

        private void OnAccountsClick(object sender, EventArgs e)
        {
            AccountsController accountsController
                = SingleFormController.Prepare<AccountsController>();

            accountsController.Closed += new SingleFormClosedHandler(OnAccountsClickClosed);
            accountsController.Execute();
        }

        private void OnAccountsClickClosed(SingleFormController controller)
        {
            View.m_mbNext.Focus();
        }
        
    }
}
