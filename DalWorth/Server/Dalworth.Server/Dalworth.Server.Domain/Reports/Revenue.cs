using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class Revenue
    {
        #region Constructor

        public Revenue(DateTime date, decimal rugsAmt, decimal floodsAmt, decimal constructionAmt, decimal contentAmt)
        {
            m_date = date;
            m_rugsAmt = rugsAmt;
            m_floodsAmt = floodsAmt;
            m_constructionAmt = constructionAmt;
            m_contentAmt = contentAmt;
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

        #region RugsAmt

        private decimal m_rugsAmt;
        public decimal RugsAmt
        {
            get { return m_rugsAmt; }
            set { m_rugsAmt = value; }
        }

        #endregion

        #region FloodsAmt

        private decimal m_floodsAmt;
        public decimal FloodsAmt
        {
            get { return m_floodsAmt; }
            set { m_floodsAmt = value; }
        }

        #endregion

        #region ConstructionAmt

        private decimal m_constructionAmt;
        public decimal ConstructionAmt
        {
            get { return m_constructionAmt; }
            set { m_constructionAmt = value; }
        }

        #endregion

        #region ContentAmt

        private decimal m_contentAmt;
        public decimal ContentAmt
        {
            get { return m_contentAmt; }
            set { m_contentAmt = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call ReportRevenue(?StartDate, ?EndDate);"; 

        public static List<Revenue> Find(DateTime startDate, DateTime endDate)
        {
            List<Revenue> result = new List<Revenue>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Revenue revenue = new Revenue(
                            dataReader.GetDateTime(0),
                            dataReader.GetDecimal(1),
                            dataReader.GetDecimal(2),
                            dataReader.GetDecimal(3),
                            dataReader.GetDecimal(4));

                        result.Add(revenue);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
