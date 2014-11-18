using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenNETCF.Phone.Sim;
using QuickBooksAgent.Data;
using QuickBooksAgent.Windows.UI;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Customers.Manage
{
    public delegate void CustomerEditAffectHandler(Customer customer);

    public class ManageCustomerModel : ITableTreeModel, IModel
    {
        #region Fields

        #region CustomerList

        List<Customer> m_customerList;        
        public List<Customer> CustomerList
        {
            get { return m_customerList; }
        }

        #endregion

        #region CurrentCustomer

        Customer m_currentCustomer;
        public Customer CurrentCustomer
        {
            get { return m_currentCustomer; }
            set { m_currentCustomer = value; }
        }

        #endregion

        #endregion

        #region GetCurrentIndex

        public int GetCurrentIndex() 
        {
            return m_customerList.FindIndex(
                delegate(Customer customer) 
                { 
                    return customer == m_currentCustomer; 
                }
            );
        }

        #endregion

        #region ITableModel

        public int GetColumnCount()
        {
            return 3;
        }

        public string GetColumnName(int columnIndex)
        {
            switch(columnIndex)
            {
                case 1:
                    return "Name";
                case 2:
                    return "Balance";
                default:
                    return String.Empty;
            }
        }

        public Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
            {  
                if (m_customerList[rowIndex].EntityState == EntityState.Created)
                    return "C";
                else if (m_customerList[rowIndex].EntityState == EntityState.Deleted)
                    return "D";
                else if (m_customerList[rowIndex].EntityState == EntityState.Modified)
                    return "M";
                else if (m_customerList[rowIndex].EntityState == EntityState.Synchronized)
                    return "S";

                return string.Empty;
            }
            else if (columnIndex == 1)
            {
                int chIndex = m_customerList[rowIndex].FullName.IndexOf(':');

                if (chIndex == -1)
                {
                    return m_customerList[rowIndex].FullName;
                    //String.Format("{0} {1}", m_customerList[rowIndex].FirstName,
                    //m_customerList[rowIndex].LastName);
                }
                else
                    return m_customerList[rowIndex].FullName.Substring(
                        m_customerList[rowIndex].FullName.LastIndexOf(':') + 1);
            }
            else
            {
                if (m_customerList[rowIndex].Balance.HasValue)
                    return m_customerList[rowIndex].Balance.Value.ToString("C");
                else
                    return string.Empty;
            }
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            Debug.Assert(false, "SetValueAt should never invoked");
        }

        public int GetRowCount()
        {
            return m_customerList.Count;
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }
        
        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_customerList[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region IModel Members

        public void Init()
        {                        
            m_customerList = new List<Customer>();

            List<Customer> customerList = Customer.FindByFullName();

            Dictionary<int, Customer> customerMap = new Dictionary<int, Customer>();

            foreach (Customer customer in customerList)
                customerMap.Add(customer.CustomerId, customer);

            foreach (Customer customer in customerList)
            {
                if (customer.ModifiedCustomerId != null)
                {
                    m_customerList.Add(customer);
                    customerMap.Remove(customer.CustomerId);
                    customerMap.Remove(customer.ModifiedCustomerId.Value);
                }          
            }

            foreach (int key in customerMap.Keys)            
                m_customerList.Add(customerMap[key]);            

            this.Update();
        }

        #endregion  
        
        #region Delete

        public void Delete(int CurrentRowIndex)
        {
            Database.Begin();

            try
            {
                Customer customer = (Customer)GetObjectAt(CurrentRowIndex, 0);

                Customer.Delete(customer);

                CustomerList.RemoveAt(CurrentRowIndex);

                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();
                throw e;
            }

            if (Change != null)
                Change.Invoke();
        }

        #endregion      

        #region Undo

        public void Undo(int CurrentRowIndex)
        {
            Database.Begin();

            try
            {
                Customer o = (Customer)GetObjectAt(CurrentRowIndex, 0);

                Customer customer = Customer.FindByPrimaryKey(o.ModifiedCustomerId.Value);                

                Customer.Delete(o);

                CustomerList[CurrentRowIndex] = customer;

                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
            }            
            
            if (Change != null)
                Change.Invoke();
        }

        #endregion   
    
        #region Update

        internal void Update()
        {
            if (Change != null)
                Change.Invoke();

            m_customerList.Sort(new CustomerComparer());
        }

        #endregion

        #region Search

        public int Search(String query)
        {
            int searchIndex = -1;

            if (m_customerList.Count == 0)
                return searchIndex;

            if (!(String.Empty.Equals(query)))
            {
                for (int i = 0; i < m_customerList.Count; i++)
                {
                    if (m_customerList[i].FullName.StartsWith(query,
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        searchIndex = i;
                        break;
                    }
                }                
            }

            return searchIndex;
        }

        #endregion

        #region CustomerComparer

        class CustomerComparer : IComparer<Customer>
        {
            #region IComparer Members

            public int Compare(Customer x, Customer y)
            {
                return new CaseInsensitiveComparer().Compare(x.FullName, y.FullName);
            }

            #endregion

        }

        #endregion

        #region ITableTreeModel Members

        public int GetItemDepth(int rowIndex, int columnIndex)
        {
            if (columnIndex != 1) return 0;

            return m_customerList[rowIndex].FullName.Split(':').Length;
        }

        #endregion
    }
}
