using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class Booking
    {
        #region Constructor

        public Booking(DateTime date, int rugCount, int floodCount, int constructionCount, int contentCount, int miscellaneousCount)
        {
            m_date = date;
            m_rugCount = rugCount;
            m_floodCount = floodCount;
            m_constructionCount = constructionCount;
            m_contentCount = contentCount;
            m_miscellaneousCount = miscellaneousCount;
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

        #region RugCount

        private int m_rugCount;
        public int RugCount
        {
            get { return m_rugCount; }
            set { m_rugCount = value; }
        }

        #endregion

        #region FloodCount

        private int m_floodCount;
        public int FloodCount
        {
            get { return m_floodCount; }
            set { m_floodCount = value; }
        }

        #endregion

        #region ConstructionCount

        private int m_constructionCount;
        public int ConstructionCount
        {
            get { return m_constructionCount; }
            set { m_constructionCount = value; }
        }

        #endregion

        #region ContentCount

        private int m_contentCount;
        public int ContentCount
        {
            get { return m_contentCount; }
            set { m_contentCount = value; }
        }

        #endregion

        #region MiscellaneousCount

        private int m_miscellaneousCount;
        public int MiscellaneousCount
        {
            get { return m_miscellaneousCount; }
            set { m_miscellaneousCount = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call ReportBooking(?StartDate, ?EndDate);";

        public static List<Booking> Find(DateTime startDate, DateTime endDate)
        {
            List<Booking> result = new List<Booking>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Booking booking = new Booking(
                            dataReader.GetDateTime(0),
                            dataReader.GetInt32(1),
                            dataReader.GetInt32(2),
                            dataReader.GetInt32(3),
                            dataReader.GetInt32(4),
                            dataReader.GetInt32(5));

                        result.Add(booking);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}

