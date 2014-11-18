using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.ManageTime.Single
{        
    public class SingleTimeTrackingModel : IModel
    {
        #region Fields
        
        public delegate void TimeTrackingAffectedHandler(TimeTracking timeTracking);
        public event TimeTrackingAffectedHandler TimeTrackingAffected;

        #region Employees

        private List<Employee> m_employees;
        public List<Employee> Employees
        {
            get { return m_employees; }
        }

        #endregion

        #region Vendors

        private List<Vendor> m_vendors;
        public List<Vendor> Vendors
        {
            get { return m_vendors; }
        }

        #endregion

        #region Customers

        private List<Customer> m_customers;
        public List<Customer> Customers
        {
            get { return m_customers; }
        }

        #endregion

        #region Items

        private List<Item> m_items;
        public List<Item> Items
        {
            get { return m_items; }
        }

        #endregion

        
        #region TimeTracking

        private TimeTracking m_timeTracking;
        public TimeTracking TimeTracking
        {
            get { return m_timeTracking; }
            set { m_timeTracking = value; }
        }

        #endregion

        #region IsReadOnly

        private bool m_isReadOnly;
        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
            set { m_isReadOnly = value; }
        }

        #endregion

        #region CurrentVendor

        private Vendor m_currentVendor;
        public Vendor CurrentVendor
        {
            get { return m_currentVendor; }
            set { m_currentVendor = value; }
        }

        #endregion

        #region CurrentEmployee

        private Employee m_currentEmployee;
        public Employee CurrentEmployee
        {
            get { return m_currentEmployee; }
            set { m_currentEmployee = value; }
        }

        #endregion

        #region CurrentDate

        private DateTime? m_currentDate;
        public DateTime? CurrentDate
        {
            get { return m_currentDate; }
            set { m_currentDate = value; }
        }

        #endregion

        #region IsCreateNew

        private bool m_isCreateNew;
        public bool IsCreateNew
        {
            get { return m_isCreateNew; }
            set { m_isCreateNew = value; }
        }

        #endregion

        #endregion

        #region Init

        public void Init()
        {
            m_employees = Employee.FindBy(EntityState.Synchronized);
            m_vendors = Vendor.FindBy(EntityState.Synchronized);
            m_customers = Customer.FindBy(EntityState.Synchronized);
            m_items = Item.Find();
        }

        #endregion        

        #region Save

        internal void Save()
        {
            Debug.Assert(m_timeTracking != null, "TimeTracking should be set");
            
            Database.Begin();

            try
            {
                if (IsCreateNew)
                {
                    Counter.Assign(m_timeTracking);
                    TimeTracking.Insert(m_timeTracking);
                } else
                    TimeTracking.Update(m_timeTracking);

                Database.Commit();
                
                if (TimeTrackingAffected != null)
                    TimeTrackingAffected.Invoke(m_timeTracking);
                    
            }
            catch (Exception e)
            {
                Database.Rollback();

                throw e;
            }
        }

        #endregion
    }

    public enum PersonTypeEnum { Vendor, Employee }
}
