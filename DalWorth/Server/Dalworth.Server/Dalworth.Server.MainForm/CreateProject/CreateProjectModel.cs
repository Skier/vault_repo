using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Domain;
using Dalworth.Server.Data;
using Dalworth.Server.Windows;
using Dalworth.Server.Domain.package;

namespace Dalworth.Server.MainForm.CreateProject
{
    public class CreateProjectModel : IModel
    {
        #region OriginalProject Wrapper

        private ProjectWrapper m_OriginalProjectWrapper;

        #endregion

        #region CustomerAndAddress

        private CustomerAndAddress m_customer;
        public CustomerAndAddress Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region NewProjectType

        private ProjectTypeEnum? m_newProjectType;
        public ProjectTypeEnum? NewProjectType
        {
            get { return m_newProjectType; }
            set { m_newProjectType = value; }
        }

        #endregion

        #region ProjectWrapper

        private ProjectWrapper m_projectWrapper;
        public ProjectWrapper ProjectWrapper
        {
            get { return m_projectWrapper; }
            set { m_projectWrapper = value; }
        }

        #endregion

        #region IsCreateProject

        public bool IsCreateProject
        {
            get { return m_projectWrapper.Project.ID == 0; }
        }

        #endregion

        #region EstimatedScopeAmount

        private decimal m_estimatedScopeAmount;
        public decimal EstimatedScopeAmount
        {
            get { return m_estimatedScopeAmount; }
            set { m_estimatedScopeAmount = value; }
        }

        #endregion

        #region CreditAmount

        private decimal m_creditAmount;
        public decimal CreditAmount
        {
            get { return m_creditAmount; }
            set { m_creditAmount = value; }
        }

        #endregion

        #region UnbilledAmount

        private decimal m_unbilledAmount;
        public decimal UnbilledAmount
        {
            get { return m_unbilledAmount; }
            set { m_unbilledAmount = value; }
        }

        #endregion

        #region OutstandingAmount

        private decimal m_outstandingAmount;
        public decimal OutstandingAmount
        {
            get { return m_outstandingAmount; }
            set { m_outstandingAmount = value; }
        }

        #endregion

        #region ProjectManagers

        private List<Employee> m_projectManagers;
        public List<Employee> ProjectManagers
        {
            get { return m_projectManagers; }
        }

        #endregion

        #region QBCustomer

        private QbCustomer m_qbCustomer;
        public QbCustomer QbCustomer
        {
            get { return m_qbCustomer; }
            set { m_qbCustomer = value; }
        }

        #endregion 

        #region QBProject 

        private QbCustomer m_qbProject;
        public QbCustomer QbProject
        {
            get { return m_qbProject; }
            set { m_qbProject = value; }
        }

        #endregion

        #region QbTransactions

        private BindingList<QbTransaction> m_qbTransactions;
        public BindingList<QbTransaction> QbTransactions
        {
            get { return m_qbTransactions; }
        }

        #endregion

        #region FilteredQbTransactions

        private BindingList<QbTransaction> m_filteredQbTransactions;
        public BindingList<QbTransaction> FilteredQbTransactions
        {
            get { return m_filteredQbTransactions; }
        }

        #endregion

        #region FilteredProjectScopes

        private BindingList<ProjectConstructionScope> m_filteredProjectScopes;
        public BindingList<ProjectConstructionScope> FilteredProjectScopes
        {
            get { return m_filteredProjectScopes; }
        }

        #endregion

        #region Init

        public void Init()
        {            
            m_projectManagers = Employee.FindBy(EmployeeTypeEnum.ProjectManager);

            if (m_projectWrapper != null)
            {
                m_OriginalProjectWrapper = (ProjectWrapper)m_projectWrapper.Clone();
                m_projectWrapper.Project = Project.FindByPrimaryKey(m_projectWrapper.Project.ID);
                m_projectWrapper.ConstructionDetail = ProjectConstructionDetail.FindByPrimaryKey(
                    m_projectWrapper.Project.ID);

                if (Customer == null)
                {
                    Customer customer = Domain.Customer.FindByPrimaryKey( m_projectWrapper.Project.CustomerId.Value);
                    Address address = Address.FindByPrimaryKey(customer.AddressId.Value);
                    Customer = new CustomerAndAddress(customer, address);
                }
            }
            else
            {
                m_projectWrapper = new ProjectWrapper();
                m_projectWrapper.Project = new Project();
                m_projectWrapper.Project.ProjectStatus = ProjectStatusEnum.Open;
                m_projectWrapper.ConstructionDetail = new ProjectConstructionDetail();
                m_projectWrapper.ServiceAddress = Customer.Address;
                m_projectWrapper.CustomerAddress = Customer.Address; ;
                m_projectWrapper.Customer = Customer.Customer;
            }

            try
            {
                m_qbCustomer = QbCustomer.FindParent(Customer.Customer.ID,null);

                if (m_projectWrapper.Project.ID > 0)
                    m_qbProject = QbCustomer.FindByProjectId(m_projectWrapper.Project.ID,null);
            }
            catch (DataNotFoundException ex)
            {}

            List<CustomerProjectWrapper> customerProjectWrappers;
            if (m_projectWrapper.Project.ID > 0)
                customerProjectWrappers = CustomerProjectWrapper.Find(projectId:m_projectWrapper.Project.ID);
            else
                customerProjectWrappers = new  List<CustomerProjectWrapper>();

            CustomerProjectWrapper customerProjectWrapper = null;
            if (customerProjectWrappers.Count > 0)
            {
                customerProjectWrapper = customerProjectWrappers.Find(delegate(CustomerProjectWrapper currentWrapper)
                                                     {
                                                         return !currentWrapper.IsCustomer &&
                                                                currentWrapper.ProjectId == m_projectWrapper.Project.ID;
                                                     });
            }

            if (customerProjectWrapper != null)
            {
                m_qbTransactions = new BindingList<QbTransaction>(QbTransaction.FindBy(customerProjectWrapper));
                m_filteredQbTransactions = new BindingList<QbTransaction>();
                m_filteredProjectScopes = new BindingList<ProjectConstructionScope>();
            }
            else
            {
                m_qbTransactions = new BindingList<QbTransaction>();
                m_filteredQbTransactions = new BindingList<QbTransaction>();
                m_filteredProjectScopes = new BindingList<ProjectConstructionScope>();
            }
        }

        #endregion


        #region RefreshProjectAmounts

        public void RefreshProjectAmounts()
        {
            decimal totalScopeAmount = 0;
            decimal estimatedScopeAmount = 0;
            decimal billedAmount = 0;
            DateTime? lastBillingDate = null;

            decimal paidAmount = 0;
            DateTime? lastPaymentDate = null;

            decimal creditAmount = 0;

            if (QbTransactions != null)
            {
                foreach (QbTransaction transaction in QbTransactions)
                {
                    if (transaction.Type == QbTransactionTypeEnum.Invoice)
                    {
                        billedAmount += transaction.TotalAmount;
                        if (lastBillingDate == null || transaction.CreatedDate > lastBillingDate)
                            lastBillingDate = transaction.CreatedDate;
                    }
                    else if (transaction.Type == QbTransactionTypeEnum.CreditMemo)
                    {
                        creditAmount += transaction.TotalAmount;
                    }
                    else if (transaction.Type == QbTransactionTypeEnum.Payment)
                    {
                        paidAmount += transaction.TotalAmount;
                        if (lastPaymentDate == null || transaction.CreatedDate > lastPaymentDate)
                            lastPaymentDate = transaction.CreatedDate;
                    }
                }
            }

            m_projectWrapper.ConstructionDetail.BilledAmount = billedAmount;
            m_estimatedScopeAmount = estimatedScopeAmount;

            m_projectWrapper.Project.ClosedAmount = totalScopeAmount;
            m_projectWrapper.ConstructionDetail.LastBillingDate = lastBillingDate;
            m_projectWrapper.Project.PaidAmount = paidAmount;
            m_projectWrapper.ConstructionDetail.LastPaymentDate = lastPaymentDate;

            m_creditAmount = creditAmount;
            m_unbilledAmount = totalScopeAmount - billedAmount;
            m_outstandingAmount = billedAmount - creditAmount - paidAmount;
        }

        #endregion

        #region InsertUpdateProject

        public void InsertUpdateProject()
        {
            m_projectWrapper.ConstructionDetail.LastModifiedDate = DateTime.Now;
            m_projectWrapper.Project.CustomerId = m_projectWrapper.Customer.ID;
            m_projectWrapper.Project.ServiceAddressId = m_projectWrapper.ServiceAddress.ID;

            if (IsCreateProject)
            {
                Project.InsertAndLog(m_projectWrapper.Project);
                m_projectWrapper.ConstructionDetail.ProjectId = m_projectWrapper.Project.ID;

                m_projectWrapper.ConstructionDetail.DamageOrigin = string.Empty;
                m_projectWrapper.ConstructionDetail.DamageTypeText = string.Empty;
                ProjectConstructionDetail.Insert(m_projectWrapper.ConstructionDetail);
            } 
            else
            {
                Project.UpdateAndLog(m_projectWrapper.Project);
                ProjectConstructionDetail.Update(m_projectWrapper.ConstructionDetail);          
            }

            if (m_projectWrapper.ProjectInsurance.IsFilled())
            {
                if (m_projectWrapper.ProjectInsurance.ProjectId == m_projectWrapper.Project.ID)
                    ProjectInsurance.Update(m_projectWrapper.ProjectInsurance);
                else
                {
                    m_projectWrapper.ProjectInsurance.ProjectId = m_projectWrapper.Project.ID;
                    ProjectInsurance.Insert(m_projectWrapper.ProjectInsurance);
                }
            }

            if (m_projectWrapper.ConstructionDetail.SignUpDate != null)    
            {
                if (QbCustomer == null)
                {
                    QbCustomer = new QbCustomer();
                    QbCustomer.Fill(Customer.Customer, Customer.Address);
                    QbCustomer.Insert(QbCustomer);
                    QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int)QbSyncActionEnum.CustomerAdd, QbCustomer.ID, null));
                }

                if (QbProject == null)
                {
                    QbProject = new QbCustomer();
                    QbProject.Fill(Customer.Customer, Customer.Address, m_projectWrapper.Project);
                    QbCustomer.Insert(QbProject);
                    QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int)QbSyncActionEnum.JobAdd, QbProject.ID, null));
                }
            }
        }

        #endregion

        #region InsertUpdateProjectScopes

        public void Print(Boolean isAutomatic)
        {
            if (isAutomatic)
            {
                if (m_projectWrapper.IsNeedPrint(m_OriginalProjectWrapper))
                    new ProjectPrint(m_projectWrapper).Print();
            }else
            {
                new ProjectPrint(m_projectWrapper).Print();
            }            
        }

        #endregion
    }
}
