using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class OrderHistory
    {
        public OrderHistory(){ }

        #region Address

        public string Address
        {
            get
            {
                return Street + ", " + City + ", " + State;
            }
        }

        #endregion

        #region ExclusiveCompany

        public Company ExclusiveCompany
        {
            get
            {
                if (m_exclusiveCompanyId == null)
                    return null;
                return Company.GetCompany(m_exclusiveCompanyId.Value);
            }
            set
            {
                if (value == null)
                    m_exclusiveCompanyId = null;
                else
                    m_exclusiveCompanyId = value.ID;
            }
        }

        #endregion

        #region ExclusiveCompanyName

        public string ExclusiveCompanyName
        {
            get
            {
                if (m_exclusiveCompanyId == null)
                    return string.Empty;
                return ExclusiveCompany.Name;
            }
        }

        #endregion

        #region FindSorted

        private const string SqlFindSorted =
            @"SELECT * FROM OrderHistory 
                where TicketNumber not in (SELECT TicketNumber FROM visit where TicketNumber is not null) 
                    {0}
                order by DateSchedule, DateTimeCall";

        public static List<OrderHistory> FindSorted(DateTime? date)
        {
            List<OrderHistory> result = new List<OrderHistory>();

            string queryString =
                string.Format(SqlFindSorted, date == null ? string.Empty : "and Date(DateSchedule) = ?DateSchedule");

            using (IDbCommand dbCommand = Database.PrepareCommand(queryString))
            {
                if (date != null)
                    Database.PutParameter(dbCommand, "?DateSchedule", date.Value.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;

        }

        #endregion
    }
}
      