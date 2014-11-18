using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Banking.CreditCardCharges;
using QuickBooksAgent.Windows.UI.Banking.Menu;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI.Customers.Manage;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.DatabaseManager.Accounts;
using QuickBooksAgent.Windows.UI.ManageTime.Menu;
using QuickBooksAgent.Windows.UI.Setup.About;
using QuickBooksAgent.Windows.UI.Setup.Register;
using QuickBooksAgent.Windows.UI.Synchronize.Basic;
using QuickBooksAgent.Windows.UI.DatabaseManager.Menu;
using QuickBooksAgent.Windows.UI.Setup.Connection;
using System.Diagnostics;
using QuickBooksAgent.Windows.UI.DatabaseManager.Vendors;
using QuickBooksAgent.Windows.UI.DatabaseManager.Items;
using QuickBooksAgent.Windows.UI.DatabaseManager.Employees;
using QuickBooksAgent.Windows.UI.ManageTime.Single;
using QuickBooksAgent.Windows.UI.ManageTime.Weekly;
using QuickBooksAgent.Windows.UI.Setup.Application;
using QuickBooksAgent.Windows.UI.Banking.WriteCheck;

namespace QuickBooksAgent.Windows.UI.Menu.MainMenu
{
    public class MainMenuController:SingleFormController<MainMenuModel,MainMenuView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_mbManageTime.Click += new EventHandler(OnManageTimeClick);
            View.m_mbCustomer.Click += new EventHandler(OnCustomerClick);
            View.m_mbBanking.Click += new EventHandler(OnBankingClick);
            View.m_mbAccounts.Click += new EventHandler(OnAccountsClick);
            View.m_mbEmployees.Click += new EventHandler(OnEmployeesClick);
            View.m_mbVendors.Click += new EventHandler(OnVendorsClick);
            View.m_mbItems.Click += new EventHandler(OnItemsClick);
            View.m_mbSetup.Click += new EventHandler(OnSetupClick);
            View.m_mbSynchronize.Click += new EventHandler(OnSyncClick);
            
            
            View.m_menuManageTime.Click += new EventHandler(OnManageTimeClick);
            View.m_menuSetup.Click += new EventHandler(OnSetupClick);
            View.m_menuSynchronize.Click += new EventHandler(OnSyncClick);

            // Database
            View.m_menuCustomers.Click += new EventHandler(OnCustomerClick);
            View.m_menuVendors.Click += new EventHandler(OnVendorsClick);
            View.m_menuEmployee.Click += new EventHandler(OnEmployeesClick);
            View.m_menuItems.Click += new EventHandler(OnItemsClick);
            View.m_menuAccounts.Click += new EventHandler(OnAccountsClick);

            // Manage Time
            View.m_menuSingleTimeEntry.Click += new EventHandler(OnSingleTimeEntry);
            View.m_menuWeeklyTimeSheet.Click += new EventHandler(OnWeeklyTimeSheet);

            // Setup
            View.m_menuSetupApplication.Click += new EventHandler(OnApplicationClick);
            View.m_menuRegister.Click += new EventHandler(OnRegisterClick);
            View.m_menuSetupConnection.Click += new EventHandler(OnConnectionClick);
            View.m_menuAbout.Click += new EventHandler(OnAboutClick);

            //Banking
            View.m_menuBankingAddCheck.Click += new EventHandler(OnAddCheck);
            View.m_menuBankingCreditCard.Click += new EventHandler(OnCreditCardClick);

            View.m_menuManager.Init(null);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            if (!Model.IsSynchAllowed)
                View.m_mbSetup.Select();
            else
                View.m_mbSynchronize.Select();

            IsDefaultActionExist = true;
            DefaultActionName = "More";

            if (Model.IsManageTimeAllowed)
                View.m_menuManager.SetFocus(View.m_mbManageTime);
            else if (Model.IsSynchAllowed)
                View.m_menuManager.SetFocus(View.m_mbSynchronize);
            else
                View.m_menuManager.SetFocus(View.m_mbSetup);                        
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {
            base.OnViewActivated();

                        
            View.m_menuManageTime.Enabled = Model.IsManageTimeAllowed;
            View.m_menuCustomers.Enabled = Model.IsCustomersAllowed;
            View.m_menuBanking.Enabled = Model.IsBankingAllowed;
            View.m_menuAccounts.Enabled = Model.IsAccountsAllowed;
            View.m_menuEmployee.Enabled = Model.IsEmployeesAllowed;
            View.m_menuVendors.Enabled = Model.IsVendorsAllowed;
            View.m_menuItems.Enabled = Model.IsItemsAllowed;
            View.m_menuSynchronize.Enabled = Model.IsSynchAllowed;


            View.m_mbManageTime.Enabled = View.m_menuManageTime.Enabled;
            View.m_mbCustomer.Enabled = View.m_menuCustomers.Enabled;
            View.m_mbBanking.Enabled = View.m_menuBanking.Enabled;
            View.m_mbAccounts.Enabled = View.m_menuAccounts.Enabled;
            View.m_mbEmployees.Enabled = View.m_menuEmployee.Enabled;
            View.m_mbVendors.Enabled = View.m_menuVendors.Enabled;
            View.m_mbItems.Enabled = View.m_menuItems.Enabled;
            View.m_mbSetup.Enabled = View.m_menuSetup.Enabled;
            View.m_mbSynchronize.Enabled = View.m_menuSynchronize.Enabled;            
        }

        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            View.m_menuManager.ScrollForward();
        }

        #endregion
                
        
        #region OnManageTimeClick

        void OnManageTimeClick(object sender, EventArgs e)
        {
            ManageTimeMenuController manageTimeMenuController =
                Prepare<ManageTimeMenuController>();

            manageTimeMenuController.Closed += new SingleFormClosedHandler(OnManageTimeClosed);
            manageTimeMenuController.Execute();            
        }

        void OnManageTimeClosed(SingleFormController controller)
        {
            View.m_mbManageTime.Select();
        }
        
        void OnManageTimeMenuClosed(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbManageTime);
        }

        #endregion

        #region OnSingleTimeEntry

        void OnSingleTimeEntry(object sender, EventArgs e)
        {
            SingleTimeTrackingController singleTimeTrackingController
                = Prepare<SingleTimeTrackingController>();

            singleTimeTrackingController.Closed += new SingleFormClosedHandler(OnManageTimeMenuClosed);
            singleTimeTrackingController.Execute();
        }

        #endregion

        #region OnWeeklyTimeSheet

        void OnWeeklyTimeSheet(object sender, EventArgs e)
        {
            WeeklyTimeTrackingController weeklyTimeTrackingController
                = Prepare<WeeklyTimeTrackingController>();

            weeklyTimeTrackingController.Closed += new SingleFormClosedHandler(OnManageTimeMenuClosed);
            weeklyTimeTrackingController.Execute();
        }

        #endregion        

        #region OnCustomerClick

        void OnCustomerClick(object sender, EventArgs e)
        {
            ManageCustomerController manageCustomerController =
                Prepare<ManageCustomerController>();
            
            if (sender is MenuButton)
                manageCustomerController.Closed += new SingleFormClosedHandler(OnCustomerClose);
            else
                manageCustomerController.Closed += new SingleFormClosedHandler(OnCustomerMenuClose);
                
            manageCustomerController.Execute();
        }

        void OnCustomerClose(SingleFormController controller)
        {
            View.m_mbCustomer.Select();
        }
        
        void OnCustomerMenuClose(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbCustomer);
        }

        #endregion

        #region OnBankingClick

        void OnBankingClick(object sender, EventArgs e)
        {
            BankingMenuController bankingMenuController
                = Prepare<BankingMenuController>();
            bankingMenuController.Closed += new SingleFormClosedHandler(OnBankingClose);
            bankingMenuController.Execute();
        }

        void OnBankingClose(SingleFormController controller)
        {
            View.m_mbBanking.Select();
        }
        
        void OnBankingMenuClose(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbBanking);
        }

        #endregion        
        
        #region OnAddCheck

        void OnAddCheck(object sender, EventArgs e)
        {
            WriteCheckController writeCheckController
                = Prepare<WriteCheckController>(null, null, false, null);
            writeCheckController.Closed += new SingleFormClosedHandler(OnBankingMenuClose);
            writeCheckController.Execute();
        }

        #endregion

        #region CreditCard

        private void OnCreditCardClick(object sender, EventArgs e)
        {
            CreditCardController creditCardController
                = Prepare<CreditCardController>();
            creditCardController.Closed += new SingleFormClosedHandler(OnBankingMenuClose);            
            creditCardController.Execute();
        }

        #endregion
                
        #region OnAccountsClick

        private void OnAccountsClick(object sender, EventArgs e)
        {
            AccountsController accountsController
                = Prepare<AccountsController>();

            if (sender is MenuButton)
                accountsController.Closed += new SingleFormClosedHandler(OnAccountsClose);
            else
                accountsController.Closed += new SingleFormClosedHandler(OnAccountsMenuClose);
            
            accountsController.Execute();
        }

        void OnAccountsClose(SingleFormController controller)
        {
            View.m_mbAccounts.Select();
        }        
        
        void OnAccountsMenuClose(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbAccounts);
        }        

        #endregion

        #region OnEmployeesClick

        void OnEmployeesClick(object sender, EventArgs e)
        {
            EmployeesController employeesController
                = Prepare<EmployeesController>();
            
            if (sender is MenuButton)                
                employeesController.Closed += new SingleFormClosedHandler(OnEmployeesClose);
            else
                employeesController.Closed += new SingleFormClosedHandler(OnEmployeesMenuClose);
            
            employeesController.Execute();
        }

        void OnEmployeesClose(SingleFormController controller)
        {
            View.m_mbEmployees.Select();
        }        
        
        void OnEmployeesMenuClose(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbEmployees);
        }        

        #endregion

        #region OnVendorsClick

        void OnVendorsClick(object sender, EventArgs e)
        {
            VendorsController vendorsController
                = Prepare<VendorsController>();

            if (sender is MenuButton)
                vendorsController.Closed += new SingleFormClosedHandler(OnVendorsClose);
            else
                vendorsController.Closed += new SingleFormClosedHandler(OnVendorsMenuClose);
            
            vendorsController.Execute();
        }

        void OnVendorsClose(SingleFormController controller)
        {
            View.m_mbVendors.Select();
        }
        
        void OnVendorsMenuClose(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbVendors);
        }

        #endregion

        #region OnItemsClick

        void OnItemsClick(object sender, EventArgs e)
        {
            ItemsController itemsController
                = Prepare<ItemsController>();
            
            if (sender is MenuButton)
                itemsController.Closed += new SingleFormClosedHandler(OnItemsClose);
            else
                itemsController.Closed += new SingleFormClosedHandler(OnItemsMenuClose);
            
            itemsController.Execute();
        }

        void OnItemsClose(SingleFormController controller)
        {
            View.m_mbItems.Select();
        }        
        
        void OnItemsMenuClose(SingleFormController controller)
        {
            View.m_menuManager.SetFocus(View.m_mbItems);
        }        

        #endregion

        #region OnSetupClick

        void OnSetupClick(object sender, EventArgs e)
        {
            SetupMenuController setupMenuController
                = Prepare<SetupMenuController>();

            setupMenuController.Closed += new SingleFormClosedHandler(OnSetupClosed);
            setupMenuController.Execute();
        }

        void OnSetupClosed(SingleFormController controller)
        {
            if (Model.IsSynchAllowed)
                View.m_mbSynchronize.Select();
            else
                View.m_mbSetup.Select();
        }

        #endregion

        #region OnConnectionClick

        void OnConnectionClick(object sender, EventArgs e)
        {
            ConnectionController connectionController
                = Prepare<ConnectionController>();
            connectionController.Closed += new SingleFormClosedHandler(OnConnectionClosed);
            connectionController.Execute();
        }

        void OnConnectionClosed(SingleFormController controller)
        {            
            if (Model.IsSynchAllowed)
                View.m_menuManager.SetFocus(View.m_mbSynchronize);
            else
                View.m_menuManager.SetFocus(View.m_mbSetup);            
        }        
        
        #endregion

        #region OnRegisterClick

        private void OnRegisterClick(object sender, EventArgs e)
        {
            RegisterController registerController
                = SingleFormController.Prepare<RegisterController>();
            registerController.Closed += new SingleFormClosedHandler(OnSetupClosed);
            registerController.Execute();
        }

        #endregion

        #region OnAboutClick

        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutController aboutController
                = SingleFormController.Prepare<AboutController>();
            aboutController.Closed += new SingleFormClosedHandler(OnSetupClosed);
            aboutController.Execute();
        }

        #endregion

        #region OnApplicationClick

        void OnApplicationClick(object sender, EventArgs e)
        {
            ApplicationController applicationController
                = Prepare<ApplicationController>();

            applicationController.Closed += new SingleFormClosedHandler(OnSetupClosed);
            applicationController.Execute();
        }

        #endregion

        #region OnSyncClick

        void OnSyncClick(object sender, EventArgs e)
        {
            SynchronizeBasicController synchronizeBasicController =
                Prepare<SynchronizeBasicController>();

            if (sender is MenuButton)
                synchronizeBasicController.Closed += new SingleFormClosedHandler(OnSynchClosed);
            else
                synchronizeBasicController.Closed += new SingleFormClosedHandler(OnSynchMenuClosed);
            
            synchronizeBasicController.Execute();

        }

        void OnSynchClosed(SingleFormController controller)
        {
            OnViewActivated();
                        
            if (Model.IsDatabaseAllowed)
                View.m_mbManageTime.Select();
            else
                View.m_mbSynchronize.Select();
        }

        void OnSynchMenuClosed(SingleFormController controller)
        {
            OnViewActivated();

            
            if (Model.IsDatabaseAllowed)
                View.m_menuManager.SetFocus(View.m_mbManageTime);
            else
                View.m_menuManager.SetFocus(View.m_mbSynchronize);
        }
        

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return !base.OnCancel("Are you sure you want to quit from Q-Agent?");
        }

        #endregion
    }
}
