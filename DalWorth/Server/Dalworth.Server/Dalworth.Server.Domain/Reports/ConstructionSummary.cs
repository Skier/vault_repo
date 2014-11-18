using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class ConstructionSummary
    {
        #region Constructor

        public ConstructionSummary(
            DateTime date,
            int leadsQty,
            int signUpsQty,
            decimal scopesAmt,
            decimal billedAmt,
            decimal workInProgressAmt)
        {
            m_date = date;
            m_leadsQty = leadsQty;
            m_signUpsQty = signUpsQty;
            m_scopeAmt = scopesAmt;
            m_billedAmt = billedAmt;
            m_workInProgressAmt = workInProgressAmt;
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

        #region LeadsQty

        private int m_leadsQty;
        public int LeadsQty
        {
            get { return m_leadsQty; }
            set { m_leadsQty = value; }
        }

        #endregion

        #region SignUpsQty

        private int m_signUpsQty;
        public int SignUpsQty
        {
            get { return m_signUpsQty; }
            set { m_signUpsQty = value; }
        }

        #endregion

        #region ScopeAmt

        private decimal m_scopeAmt;
        public decimal ScopeAmt
        {
            get { return m_scopeAmt; }
            set { m_scopeAmt = value; }
        }

        #endregion

        #region BilledAmt

        private decimal m_billedAmt;
        public decimal BilledAmt
        {
            get { return m_billedAmt; }
            set { m_billedAmt = value; }
        }

        #endregion

        #region WorkInProgressAmt

        private decimal m_workInProgressAmt;
        public decimal WorkInProgressAmt
        {
            get { return m_workInProgressAmt; }
            set { m_workInProgressAmt = value; }
        }

        #endregion

        #region ProjectTypeId

        private int m_projectTypeId;
        public int ProjectTypeId
        {
            get { return m_projectTypeId; }
            set { m_projectTypeId = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call ReportConstructionSummary(?StartDate, ?EndDate, ?ProjectTypeId)";

        public static List<ConstructionSummary> Find(DateTime startDate, DateTime endDate, int projectTypeId)
        {
            List<ConstructionSummary> result = new List<ConstructionSummary>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);
                Database.PutParameter(dbCommand, "?ProjectTypeId", projectTypeId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ConstructionSummary summary = new ConstructionSummary(
                            dataReader.GetDateTime(0),
                            dataReader.GetInt32(1),
                            dataReader.GetInt32(2),
                            dataReader.GetDecimal(3),
                            dataReader.GetDecimal(4),
                            dataReader.GetDecimal(5));

                        summary.ProjectTypeId = projectTypeId;

                        result.Add(summary);
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
