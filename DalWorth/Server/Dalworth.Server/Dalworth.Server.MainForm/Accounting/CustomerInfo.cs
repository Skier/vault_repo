using System;
using Dalworth.Server.Data;
using Dalworth.Server.Windows;
using Dalworth.Server.Domain;


namespace Dalworth.Server.MainForm.Accounting
{
    public partial class CustomerInfo : BaseControl
    {
        private CustomerProjectWrapper m_customerProjectWrapper;
        public event CustomerProjectWrapperHandler EditCustomer;
        public event CustomerProjectWrapperHandler CreateVisit;
        public event CustomerProjectWrapperHandler CreateProject;

        public CustomerInfo()
        {
            InitializeComponent();
            m_btnEditCustomer.Click += OnEditCustomerClick;
            m_btnCreateCustomerVisit.Click += OnCreateVisitClick;
            m_btnCreateCustomerProject.Click += OnCreateProjectClick;
        }

        public void Clear()
        {
            m_lblCustomerName.Text = string.Empty;
            m_lblAddress.Text = string.Empty;
            m_lblCustomerBalance.Text = string.Empty;
            m_lnkEmail.Text = string.Empty;
            m_lblSalesRep.Text = string.Empty;
            m_lblAdvertisingSource.Text = string.Empty;
            m_lblPhoneHome.Text = string.Empty;
            m_lblPhoneWork.Text = string.Empty;
        }

        public void Initialize(CustomerProjectWrapper wrapper)
        {
            m_customerProjectWrapper = wrapper;

            m_lblAdvertisingSource.Text = string.Empty;
            m_lblSalesRep.Text = string.Empty;

            try
            {
                QbCustomer qbCustomer = QbCustomer.FindParent(wrapper.Customer.ID, null);
                if (!string.IsNullOrEmpty(qbCustomer.QbCustomerTypeListId))
                {
                    QbCustomerType qbCustomerType = QbCustomerType.FindByPrimaryKey(qbCustomer.QbCustomerTypeListId);
                    m_lblAdvertisingSource.Text = qbCustomerType.FullName;
                }

                if (!string.IsNullOrEmpty(qbCustomer.QbSalesRepListId))
                {
                    QbSalesRep salesRep = QbSalesRep.FindByPrimaryKey(qbCustomer.QbSalesRepListId);
                    m_lblSalesRep.Text = salesRep.FullName;

                }
            }
            catch (DataNotFoundException)
            {
            }

            m_lblCustomerName.Text = wrapper.CustomerName;
            m_lblAddress.Text = string.Format("{0}\n{1}", wrapper.CustomerAddress.AddressFirstLine,
                wrapper.CustomerAddress.AddressSecondLine);
            m_lblCustomerBalance.Text = wrapper.CustomerBalance.HasValue ? wrapper.CustomerBalance.Value.ToString("C") : "N/A";
            m_lnkEmail.Text = wrapper.Customer.Email;
            m_lblPhoneHome.Text = Utils.FormatPhone(wrapper.Customer.Phone1);
            m_lblPhoneWork.Text = Utils.FormatPhone(wrapper.Customer.Phone2);
        }

        private void OnEditCustomerClick(object sender, EventArgs args)
        {
            if (EditCustomer != null)
                EditCustomer.Invoke(m_customerProjectWrapper);
        }

        private void OnCreateVisitClick(object sender, EventArgs args)
        {
            if (CreateVisit != null)
                CreateVisit.Invoke(m_customerProjectWrapper);
        }

        private void OnCreateProjectClick(object sender, EventArgs args)
        {
            if (CreateProject != null)
                CreateProject.Invoke(m_customerProjectWrapper);
        }
    }
}
