using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain.Reports
{
    public class ConstructionLead
    {
        #region Constructor

        public ConstructionLead(DateTime date, int leadsCount, int libertyMutualCount, 
                                int stateFarmCount, int signUpCount, int closedCount, 
                                string signUpNames, string closedNames)
        {
            m_date = date;
            m_leadsCount = leadsCount;
            m_libertyMutualCount = libertyMutualCount;
            m_stateFarmCount = stateFarmCount;
            m_signUpCount = signUpCount;
            m_closedCount = closedCount;
            m_signUpNames = signUpNames;
            m_closedNames = closedNames;
        }

        #endregion


        #region Date

        private DateTime m_date;
        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }

        #endregion

        #region LeadsCount

        private int m_leadsCount;
        public int LeadsCount
        {
            get { return m_leadsCount; }
            set { m_leadsCount = value; }
        }

        #endregion

        #region LibertyMutualCount

        private int m_libertyMutualCount;
        public int LibertyMutualCount
        {
            get { return m_libertyMutualCount; }
            set { m_libertyMutualCount = value; }
        }

        #endregion

        #region StateFarmCount

        private int m_stateFarmCount;
        public int StateFarmCount
        {
            get { return m_stateFarmCount; }
            set { m_stateFarmCount = value; }
        }

        #endregion

        #region SignUpCount

        private int m_signUpCount;
        public int SignUpCount
        {
            get { return m_signUpCount; }
            set { m_signUpCount = value; }
        }

        #endregion

        #region ClosedCount

        private int m_closedCount;
        public int ClosedCount
        {
            get { return m_closedCount; }
            set { m_closedCount = value; }
        }

        #endregion

        #region SignUpNames

        private string m_signUpNames;
        public string SignUpNames
        {
            get { return m_signUpNames; }
            set { m_signUpNames = value; }
        }

        #endregion

        #region ClosedNames

        private string m_closedNames;
        public string ClosedNames
        {
            get { return m_closedNames; }
            set { m_closedNames = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call GetIncrementedDates(?StartDate, ?EndDate);

            select idl.ID,
            COALESCE(LeadCount, 0) as QtyLead,
            COALESCE(LeadLmCount, 0) as QtyLeadLm,
            COALESCE(LeadSfCount, 0) as QtyLeadSf,
            COALESCE(SignUpCount, 0) as QtySignUp,
            COALESCE(LastPaymentCount, 0) as QtyLastPayment,
            COALESCE(SignUpName, '') as NamesSignUp,
            COALESCE(LastPaymentName, '') as NamesLastPayment

            from  TmpIncrementedDateList idl

            left join (
                SELECT Date(CreateDate) as LeadCreateDate, count(ID) as LeadCount FROM Project
                where (ProjectTypeId = 4 or ProjectTypeId = 5)
                group by Date(CreateDate)) LeadCount on LeadCount.LeadCreateDate = idl.ID

            left join (
                SELECT Date(CreateDate) as LeadLmCreateDate, count(ID) as LeadLmCount FROM Project
                where (ProjectTypeId = 4 or ProjectTypeId = 5) and AdvertisingSourceId in ({0})
                group by Date(CreateDate)) LeadLmCount on LeadLmCount.LeadLmCreateDate = idl.ID

            left join (
                SELECT Date(CreateDate) as LeadSfCreateDate, count(ID) as LeadSfCount FROM Project
                where (ProjectTypeId = 4 or ProjectTypeId = 5) and AdvertisingSourceId in ({1})
                group by Date(CreateDate)) LeadSfCount on LeadSfCount.LeadSfCreateDate = idl.ID

            left join (
                SELECT Date(SignUpDate) as SignUpDate, count(ProjectId) as SignUpCount FROM ProjectConstructionDetail
                where SignUpDate is not null
                group by Date(SignUpDate)) SignUps on SignUps.SignUpDate = idl.ID

            left join (
                SELECT Date(LastPaymentDate) as LastPaymentDate, count(ProjectId) as LastPaymentCount FROM ProjectConstructionDetail
                where LastPaymentDate is not null
                group by Date(LastPaymentDate)) LastPayments on LastPayments.LastPaymentDate = idl.ID

            left join (
                SELECT Date(SignUpDate) as SignUpNameDate, GROUP_CONCAT(c.LastName) SignUpName FROM ProjectConstructionDetail pcd
                    inner join Project p on p.ID = pcd.ProjectId
                    inner join Customer c on c.ID = p.CustomerId
                where SignUpDate is not null
                group by Date(SignUpDate)) SignUpNames on SignUpNames.SignUpNameDate = idl.ID

            left join (
                SELECT Date(LastPaymentDate) as LastPaymentNameDate, GROUP_CONCAT(c.LastName) LastPaymentName FROM ProjectConstructionDetail pcd
                    inner join Project p on p.ID = pcd.ProjectId
                    inner join Customer c on c.ID = p.CustomerId
                where LastPaymentDate is not null
                group by Date(LastPaymentDate)) LastPaymentNames on LastPaymentNames.LastPaymentNameDate = idl.ID;

            DROP TABLE IF EXISTS TmpIncrementedDateList;";

        public static List<ConstructionLead> Find(DateTime startDate, DateTime endDate)
        {
            List<ConstructionLead> result = new List<ConstructionLead>();

            using (IDbCommand dbCommand = Database.PrepareCommand(
                string.Format(SqlFind, Configuration.LibertyMutualAdsourceIds, 
                Configuration.StateFarmAdsourceIds)))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ConstructionLead constructionLead = new ConstructionLead(
                            dataReader.GetDateTime(0),
                            dataReader.GetInt32(1),
                            dataReader.GetInt32(2),
                            dataReader.GetInt32(3),
                            dataReader.GetInt32(4),
                            dataReader.GetInt32(5),
                            dataReader.GetString(6),
                            dataReader.GetString(7));

                        result.Add(constructionLead);
                    }
                }
            }

            return result;
        }

        #endregion
    
    }
}
