using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class FloodProduction
    {
        #region Constructor

        public FloodProduction(DateTime date, int floodScheduledQty, int floodSoldQty, decimal floodSoldPct, decimal floodRevenue)
        {
            m_date = date;
            m_floodScheduledQty = floodScheduledQty;
            m_floodSoldQty = floodSoldQty;
            m_floodSoldPct = floodSoldPct;
            m_floodRevenue = floodRevenue;
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

        #region FloodScheduledQty

        private int m_floodScheduledQty;
        public int FloodScheduledQty
        {
            get { return m_floodScheduledQty; }
            set { m_floodScheduledQty = value; }
        }

        #endregion

        #region FloodSoldQty

        private int m_floodSoldQty;
        public int FloodSoldQty
        {
            get { return m_floodSoldQty; }
            set { m_floodSoldQty = value; }
        }

        #endregion

        #region FloodSoldPct

        private decimal m_floodSoldPct;
        public decimal FloodSoldPct
        {
            get { return m_floodSoldPct; }
            set { m_floodSoldPct = value; }
        }

        #endregion

        #region FloodRevenue

        private decimal m_floodRevenue;
        public decimal FloodRevenue
        {
            get { return m_floodRevenue; }
            set { m_floodRevenue = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call ReportFloodProduction(?StartDate, ?EndDate);";

        public static List<FloodProduction> Find(DateTime startDate, DateTime endDate)
        {
            List<FloodProduction> result = new List<FloodProduction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        FloodProduction floodProduction = new FloodProduction(
                            dataReader.GetDateTime(0),
                            dataReader.GetInt32(1),
                            dataReader.GetInt32(2),
                            dataReader.GetInt32(1) != 0 ? (decimal)dataReader.GetInt32(2) / dataReader.GetInt32(1) : 0,
                            (dataReader.GetDecimal(3)));

                        result.Add(floodProduction);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
