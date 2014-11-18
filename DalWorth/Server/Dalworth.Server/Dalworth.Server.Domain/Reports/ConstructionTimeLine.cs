using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class ConstructionTimeLine
    {
        private readonly Project m_project;
        private readonly ProjectConstructionDetail m_projectDetail;
        private readonly Employee m_projectManager;
        private readonly Customer m_customer;

        #region Constructor

        public ConstructionTimeLine(Project project, ProjectConstructionDetail projectDetail, 
                                    Employee projectManager, Customer customer)
        {
            m_project = project;
            m_projectDetail = projectDetail;
            m_projectManager = projectManager;
            m_customer = customer;
        }

        #endregion

        #region ProjectManagerName

        public string ProjectManagerName
        {
            get { return m_projectManager == null ? "Unknown" : m_projectManager.DisplayName;}
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region LeadDate

        public DateTime LeadDate
        {
            get { return m_project.CreateDate; }
        }

        #endregion

        #region ScopeDate

        public DateTime? ScopeDate
        {
            get { return m_projectDetail.ScopeDate; }
        }

        #endregion

        #region SelfGeneratedLead

        public bool SelfGeneratedLead
        {
            get { return m_projectDetail.IsSelfGeneratedLead; }
        }

        #endregion

        #region EstimatedAmount

        public decimal? EstimatedAmount
        {
            get { return m_projectDetail.EstimatedAmount == 0 ? (decimal?)null : m_projectDetail.EstimatedAmount; }
        }

        #endregion

        #region DeclineDate

        public DateTime? DeclineDate
        {
            get { return m_projectDetail.DeclineDate; }
        }

        #endregion

        #region SignUpDate

        public DateTime? SignUpDate
        {
            get { return m_projectDetail.SignUpDate; }
        }

        #endregion

        #region BillingDate

        public DateTime? BillingDate
        {
            get { return m_projectDetail.LastBillingDate; }
        }

        #endregion

        #region PaymentDate

        public DateTime? PaymentDate
        {
            get { return m_projectDetail.LastPaymentDate; }
        }

        #endregion

        #region AmountBilled

        public decimal? AmountBilled
        {
            get { return m_projectDetail.BilledAmount == decimal.Zero ? (decimal?)null : m_projectDetail.BilledAmount; }
        }

        #endregion

        #region AmountCollected

        public decimal? AmountCollected
        {
            get { return m_project.PaidAmount == decimal.Zero ? (decimal?)null : Math.Abs(m_project.PaidAmount); }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"select * from Project p
                inner join ProjectConstructionDetail pcd on pcd.ProjectId = p.ID
                inner join Customer c on c.ID = p.CustomerId
                left join Employee e on e.ID = pcd.ProjectManagerEmployeeId
            where (p.ProjectTypeId = 4 or p.ProjectTypeId = 5)
                  and MONTH(p.CreateDate) = ?MonthNumber and YEAR(p.CreateDate) = ?YearNumber";

        public static List<ConstructionTimeLine> Find(int month, int year)
        {
            List<ConstructionTimeLine> result = new List<ConstructionTimeLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?MonthNumber", month);
                Database.PutParameter(dbCommand, "?YearNumber", year);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {                    
                    while (dataReader.Read())
                    {
                        Project project = Project.Load(dataReader);
                        ProjectConstructionDetail detail = ProjectConstructionDetail.Load(
                            dataReader, Project.FieldsCount);
                        Customer customer = Customer.Load(dataReader, 
                            Project.FieldsCount + ProjectConstructionDetail.FieldsCount);

                        Employee manager = null;

                        if (!dataReader.IsDBNull(Project.FieldsCount + 
                            ProjectConstructionDetail.FieldsCount + Customer.FieldsCount))
                        {
                            manager = Employee.Load(dataReader, Project.FieldsCount +
                                ProjectConstructionDetail.FieldsCount + Customer.FieldsCount);
                        }

                        result.Add(new ConstructionTimeLine(project, detail, manager, customer));
                    }
                }
            }

            return result;
        }

        #endregion
    }

    public class ConstructionTimeLineNotNull
    {
        private ConstructionTimeLine m_originalData;

        #region Constructor

        public ConstructionTimeLineNotNull(ConstructionTimeLine originalData)
        {
            m_originalData = originalData;
        }

        #endregion

        #region ProjectManagerName

        public string ProjectManagerName
        {
            get { return m_originalData.ProjectManagerName; }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_originalData.CustomerName; }
        }

        #endregion

        #region LeadDate

        public DateTime LeadDate
        {
            get { return m_originalData.LeadDate; }
        }

        #endregion

        #region ScopeDate

        public DateTime ScopeDate
        {
            get { return m_originalData.ScopeDate == null ? DateTime.MinValue : m_originalData.ScopeDate.Value; }
        }

        #endregion

        #region SelfGeneratedLead

        public bool SelfGeneratedLead
        {
            get { return m_originalData.SelfGeneratedLead; }
        }

        #endregion

        #region EstimatedAmount

        public decimal EstimatedAmount
        {
            get { return m_originalData.EstimatedAmount == null ? 0 : m_originalData.EstimatedAmount.Value; }
        }

        #endregion

        #region DeclineDate

        public DateTime DeclineDate
        {
            get { return m_originalData.DeclineDate == null ? DateTime.MinValue : m_originalData.DeclineDate.Value; }
        }

        #endregion

        #region SignUpDate

        public DateTime SignUpDate
        {
            get { return m_originalData.SignUpDate == null ? DateTime.MinValue : m_originalData.SignUpDate.Value; }
        }

        #endregion

        #region BillingDate

        public DateTime BillingDate
        {
            get { return m_originalData.BillingDate == null ? DateTime.MinValue : m_originalData.BillingDate.Value; }
        }

        #endregion

        #region PaymentDate

        public DateTime PaymentDate
        {
            get { return m_originalData.PaymentDate == null ? DateTime.MinValue : m_originalData.PaymentDate.Value; }
        }

        #endregion

        #region AmountBilled

        public decimal AmountBilled
        {
            get { return m_originalData.AmountBilled == null ? 0 : m_originalData.AmountBilled.Value; }
        }

        #endregion

        #region AmountCollected

        public decimal AmountCollected
        {
            get { return m_originalData.AmountCollected == null ? 0 : m_originalData.AmountCollected.Value; }
        }

        #endregion
    }
}
