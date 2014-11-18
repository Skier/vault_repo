using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class ConstructionManager
    {
        private readonly Project m_project;
        private readonly ProjectConstructionDetail m_projectDetail;
        private readonly Employee m_projectManager;
        private readonly Customer m_customer;

        #region Constructor

        public ConstructionManager(Project project, ProjectConstructionDetail projectDetail, 
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
            get { return m_projectManager == null ? "Unknown" : m_projectManager.DisplayName; }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region StartDate

        public DateTime? StartDate
        {
            get { return m_projectDetail.SignUpDate; }
        }

        #endregion

        #region ClosedDate

        public DateTime? ClosedDate
        {
            get { return m_projectDetail.LastPaymentDate; }
        }

        #endregion

        #region BilledAmount

        public decimal? BilledAmount
        {
            get { return m_projectDetail.BilledAmount == decimal.Zero ? (decimal?)null : m_projectDetail.BilledAmount; }
        }

        #endregion

        #region ReceivedAmount

        public decimal? ReceivedAmount
        {
            get { return m_project.PaidAmount == 0 ? (decimal?)null : Math.Abs(m_project.PaidAmount); }
        }

        #endregion

        #region JobCost

        public decimal? JobCost
        {
            get { return m_projectDetail.JobCost == 0 ? (decimal?)null : m_projectDetail.JobCost; }
        }

        #endregion

        #region ProfitAmount

        public decimal? ProfitAmount
        {
            get { return BilledAmount - JobCost; }
        }

        #endregion

        #region ProfitPercent

        public decimal? ProfitPercent
        {
            get { return ProfitAmount/BilledAmount; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"select * from Project p
                inner join ProjectConstructionDetail pcd on pcd.ProjectId = p.ID
                inner join Customer c on c.ID = p.CustomerId
                left join Employee e on e.ID = pcd.ProjectManagerEmployeeId
            where (p.ProjectTypeId = 4 or p.ProjectTypeId = 5) -- and pcd.LastPaymentDate is not null
                  and pcd.ProjectConstructionProgressId = 4
                  -- and p.ClosedAmount <> 0 and p.PaidAmount <> 0 and ABS(p.PaidAmount) >= p.ClosedAmount
                  and MONTH(pcd.LastPaymentDate) = ?MonthNumber and YEAR(pcd.LastPaymentDate) = ?YearNumber";

        public static List<ConstructionManager> Find(int month, int year)
        {
            List<ConstructionManager> result = new List<ConstructionManager>();

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

                        result.Add(new ConstructionManager(project, detail, manager, customer));
                    }
                }
            }

            return result;
        }

        #endregion
    }

    public class ConstructionManagerNotNull
    {
        private ConstructionManager m_originalData;

        #region Constructor

        public ConstructionManagerNotNull(ConstructionManager originalData)
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

        #region StartDate

        public DateTime StartDate
        {
            get { return m_originalData.StartDate == null ? DateTime.MinValue : m_originalData.StartDate.Value; }
        }

        #endregion

        #region ClosedDate

        public DateTime ClosedDate
        {
            get { return m_originalData.ClosedDate == null ? DateTime.MinValue : m_originalData.ClosedDate.Value; }
        }

        #endregion

        #region BilledAmount

        public decimal BilledAmount
        {
            get { return m_originalData.BilledAmount == null ? 0 : m_originalData.BilledAmount.Value; }
        }

        #endregion

        #region ReceivedAmount

        public decimal ReceivedAmount
        {
            get { return m_originalData.ReceivedAmount == null ? 0 : m_originalData.ReceivedAmount.Value; }
        }

        #endregion

        #region JobCostValue

        public decimal JobCostValue
        {
            get { return m_originalData.JobCost == null ? 0 : m_originalData.JobCost.Value; }
        }

        #endregion

        #region JobCost

        public string JobCost
        {
            get { return JobCostValue == decimal.Zero ? "N/A" : JobCostValue.ToString("C"); }
        }

        #endregion

        #region ProfitAmountValue

        public decimal ProfitAmountValue
        {
            get { return m_originalData.ProfitAmount == null ? 0 : m_originalData.ProfitAmount.Value; }
        }

        #endregion

        #region ProfitAmount

        public string ProfitAmount
        {
            get { return JobCostValue == decimal.Zero ? "N/A" : ProfitAmountValue.ToString("C"); }
        }

        #endregion

        #region ProfitPercentValue

        public decimal ProfitPercentValue
        {
            get { return 100 * (m_originalData.ProfitPercent == null ? 0 : m_originalData.ProfitPercent.Value); }
        }

        #endregion

        #region ProfitPercent

        public string ProfitPercent
        {
            get { return JobCostValue == decimal.Zero ? "N/A" :  (int)ProfitPercentValue + "%"; }
        }

        #endregion
    }
}
