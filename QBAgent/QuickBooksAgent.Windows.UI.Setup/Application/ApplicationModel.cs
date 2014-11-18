using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Setup.Application
{
    public class ApplicationModel : IModel
    {
        #region Configuration

        private Configuration.ApplicationConfiguration m_configuration;
        internal Configuration.ApplicationConfiguration Configuration
        {
            get { return m_configuration; }
        }

        #endregion

        #region Employees

        private List<Employee> m_employees;        
        public List<Employee> Employees
        {
            get { return m_employees; }
            set { m_employees = value; }
        }

        #endregion

        #region Vendors

        private List<Vendor> m_vendors;
        public List<Vendor> Vendors
        {
            get { return m_vendors; }
            set { m_vendors = value; }
        }

        #endregion

        #region IsUserIdentificationAllowed

        internal bool IsUserIdentificationAllowed
        {
            get { return Database.IsDatabaseExist(); }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_configuration = QuickBooksAgent.Configuration.App;
            if (IsUserIdentificationAllowed)
            {
                m_employees = Employee.FindBy(EntityState.Synchronized);
                m_vendors = Vendor.FindBy(EntityState.Synchronized);                
            }else
            {
                m_employees = new List<Employee>();
                m_vendors = new List<Vendor>();
            }
                                        
        }

        #endregion        
    }
}
