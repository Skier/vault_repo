using System;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.Menu.MainMenu
{
    public class MainMenuModel
    {        
        #region IsDatabaseAllowed

        public bool IsDatabaseAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsManageTimeAllowed

        public bool IsManageTimeAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsCustomersAllowed

        public bool IsCustomersAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsBankingAllowed

        public bool IsBankingAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsAccountsAllowed

        public bool IsAccountsAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsEmployeesAllowed

        public bool IsEmployeesAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsVendorsAllowed

        public bool IsVendorsAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion

        #region IsItemsAllowed

        public bool IsItemsAllowed
        {
            get
            {
                return Database.IsDatabaseExist();
            }
        }

        #endregion                

        #region IsSynchAllowed

        public bool IsSynchAllowed
        {
            get
            {
                return !String.Empty.Equals(
                            Configuration.QuickBooks.ConnectionTicket);
            }
        }

        #endregion

    }
}
