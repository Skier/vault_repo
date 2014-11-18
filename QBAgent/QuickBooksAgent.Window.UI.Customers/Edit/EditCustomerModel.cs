using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Customers.Manage;

namespace QuickBooksAgent.Windows.UI.Customers.Edit
{    
    public class EditCustomerModel : IModel
    {
        #region Events
        public event CustomerEditAffectHandler CustomerAffected;
        #endregion

        #region Fields
        
        List<Customer> m_customerList;

        public List<Customer> CustomerList
        {
            get { return m_customerList; }
            set { m_customerList = value; }
        }

        Customer m_customer = null;
        public Customer CurrentCustomer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #region IsReadOnly

        private bool m_isReadOnly;
        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
            set { m_isReadOnly = value; }
        }

        #endregion

        #endregion

        #region Init

        public void Init()
        {
            if (CurrentCustomer != null && CurrentCustomer.Terms != null)
            {
                try
                {
                    this.CurrentCustomer.Terms =
                        Terms.FindByPrimaryKey(CurrentCustomer.Terms.TermsId);
                }
                catch (DataNotFoundException)
                {
                }
            }   
        }

        #endregion

        private void UpdateList(Customer customer)
        {
            for (int i = 0; i <= CustomerList.Count - 1;i++)
            {
                if (CustomerList[i].CustomerId == customer.ModifiedCustomerId ||
                    CustomerList[i].CustomerId == customer.CustomerId)
                {
                    CustomerList[i] = customer;
                    break;
                }
            }
        }

        public void Save(Customer customer)
        {
            Debug.Assert(!Database.UnderTransaction, "System transaction has already started");

            Database.Begin();

            try
            {
                if (CurrentCustomer == null)
                    customer.EntityState = EntityState.Created;
                else
                {
                    if (CurrentCustomer.EntityState != EntityState.Created)
                        customer.EntityState = EntityState.Modified;
                    else
                        customer.EntityState = EntityState.Created;

                    customer.QuickBooksListId = CurrentCustomer.QuickBooksListId;
                    customer.EditSequence = CurrentCustomer.EditSequence;

                    if (CurrentCustomer.ModifiedCustomerId == null &&
                        CurrentCustomer.EntityState != EntityState.Created)
                    {
                        customer.CustomerId = 0;
                        customer.ModifiedCustomerId = CurrentCustomer.CustomerId;
                    }
                    else
                    {
                        customer.CustomerId = CurrentCustomer.CustomerId;
                        customer.ModifiedCustomerId = CurrentCustomer.ModifiedCustomerId;
                    }
                }

                if (customer.CustomerId == 0)
                {
                    Counter.Assign(customer);

                    QuickBooksAgent.Domain.Customer.Insert(customer);

                    if (customer.EntityState == EntityState.Modified)
                        UpdateList(customer);
                    else
                        CustomerList.Add(customer);
                }
                else
                {
                    QuickBooksAgent.Domain.Customer.Update(customer);

                    UpdateList(customer);
                }

                if (CustomerAffected != null)
                    CustomerAffected.Invoke(customer);

                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();

                throw e;
            }         
        }
    }
}
