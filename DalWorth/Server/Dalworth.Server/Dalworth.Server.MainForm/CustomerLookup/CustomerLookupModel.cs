using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CustomerLookup
{
    public class CustomerLookupModel : IModel
    {
        #region Customers

        private BindingList<CustomerAndAddress> m_customers;
        public BindingList<CustomerAndAddress> Customers
        {
            get { return m_customers; }
            set { m_customers = value; }
        }

        #endregion

        #region CurrentSearchCriteria

        private SearchPreferredCriteria m_currentSearchCriteria;
        public SearchPreferredCriteria CurrentSearchCriteria
        {
            get { return m_currentSearchCriteria; }
            set { m_currentSearchCriteria = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_customers = new BindingList<CustomerAndAddress>();
//            Refresh(SearchPreferredCriteria.Name, string.Empty, string.Empty, string.Empty, 
//                string.Empty, string.Empty, string.Empty);
        }

        #endregion

        #region Refresh

        public void Refresh(SearchPreferredCriteria criteria, string firstName, string lastName, 
            string phoneHome, string phoneBusiness, string block, string street, string zip)
        {
            m_customers = new BindingList<CustomerAndAddress>(
                Customer.FindCustomerAndAddresses(criteria, firstName, lastName, phoneHome, phoneBusiness, block, street, zip));
            m_currentSearchCriteria = criteria;
        }

        #endregion

        #region CreateNewCustomer

        public void CreateNewCustomer(CustomerAndAddress customer)
        {
            customer.Address.Modified = DateTime.Now;
            Address.Insert(customer.Address);
            customer.Customer.AddressId = customer.Address.ID;
            customer.Customer.Modified = DateTime.Now;
            Customer.Insert(customer.Customer);
        }

        #endregion
    }
}
