using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Windows;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Accounting
{
   
    public class AccountingModel : IModel
    {
        #region CustomerProjectWrappers

        private BindingList<CustomerProjectWrapper>  m_customerProjectWrappers;
        public BindingList<CustomerProjectWrapper> CustomerProjectWrappers
        {
            get { return m_customerProjectWrappers; }
        }

        #endregion

        #region ProjectManagers

        private List<Employee> m_projectManagers;
        public List<Employee> ProjectManagers
        {
            get { return m_projectManagers; }
        }

        #endregion

        #region SalesReps

        private List<QbSalesRep> m_qbSalesReps;
        public List<QbSalesRep> SalesReps
        {
            get { return m_qbSalesReps; }
            set { m_qbSalesReps = value; }
        }

        #endregion 

        #region DefaultDateRange

        private DateRange m_defaultDateRange;
        public DateRange DefaultDateRange
        {
            get { return m_defaultDateRange; }
            set { m_defaultDateRange = value; }
        }

        #endregion

        #region ProjectTypes

        private List<ProjectType> m_projectTypes;
        public List<ProjectType> ProjectTypes
        {
            set { m_projectTypes = value; }
            get { return m_projectTypes; }
        }

        #endregion

        #region CustomerTypes

        private List<QbCustomerType> m_qbCustomerTypes;
        public List<QbCustomerType> QbCustomerTypes
        {
            get { return m_qbCustomerTypes; }
        }

        #endregion 

        #region Init

        public void Init()
        {
            m_customerProjectWrappers = new BindingList<CustomerProjectWrapper>();
            m_qbSalesReps = QbSalesRep.FindActive(null);
            m_qbCustomerTypes = QbCustomerType.FindActive(null);
            m_projectTypes = ProjectType.Find(null);

            List<QbCustomerType> level0CustomerTypes = m_qbCustomerTypes.FindAll(delegate(QbCustomerType temp)
                                                                                     { return temp.SubLevel == 0; });
            foreach (QbCustomerType level0CustomerType in level0CustomerTypes)
            {
                QbCustomerType level1CustomerType = m_qbCustomerTypes.Find(delegate(QbCustomerType temp)
                    { return temp.SubLevel == 1 && temp.ParentRefListId == level0CustomerType.ListId; });

                if (level1CustomerType != null)
                {
                    m_qbCustomerTypes.Remove(level0CustomerType);
                }
            }

            m_projectManagers = Employee.FindBy(EmployeeTypeEnum.ProjectManager);
        }

        #endregion

        #region UpdateCustomerProjects

        public void UpdateCustomerProjects(string jobNumber = null, int? exactProjectId = null, string customer = null,
            string qbSalesRepListId = null, string qbCustomerType=null, DateRange dateRange = null, ProjectTypeEnum? projectTypeEnum = null,
            string block = null, string street = null, string city = null, string zip = null, string phoneNumber=null,
            ProjectStatusEnum? projectStatus = null, int ? projectManagerId = null)
        {
            string lastName = string.Empty;
            string firstName = string.Empty;

            if (!string.IsNullOrEmpty(customer))
            {
                string[] names = customer.Split(',');
                lastName = names.Length > 0 ? names[0].Trim() : string.Empty;
                firstName = names.Length > 1 ? names[1].Trim() : string.Empty;
            }

            m_customerProjectWrappers = new BindingList<CustomerProjectWrapper>(
                CustomerProjectWrapper.Find(jobNumber, exactProjectId, lastName, firstName, 
                    dateRange, qbSalesRepListId, qbCustomerType, projectTypeEnum, block, street, city, zip, phoneNumber, projectStatus,
                    projectManagerId));
        }

        #endregion

        #region GetTransactions

        public List<QbTransaction> GetTransactions(CustomerProjectWrapper wrapper)
        {
            return QbTransaction.FindBy(wrapper);
        }

        #endregion
    }
}
