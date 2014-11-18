using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Work : ICounterField
    {
        public Work(){ }

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Work"; }
        }

        #endregion        

        #region WorkStatus

        public WorkStatusEnum WorkStatus
        {
            get { return (WorkStatusEnum)m_workStatusId; }
            set { m_workStatusId = (int)value; }
        }

        #endregion

        #region FindPendingWorks

        private const string SqlFindByEmployeeAndDate =
            @"SELECT *
            FROM Work
                WHERE TechnicianEmployeeId = @TechnicianEmployeeId
                    AND StartDate >= @StartDateStart
                    AND StartDate <= @StartDateEnd
                    AND WorkStatusId = 1";
                    

        public static List<Work> FindPendingWorks(Employee employee, DateTime startDate)
        {
            List<Work> workList = new List<Work>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEmployeeAndDate))
            {
                Database.PutParameter(dbCommand, "@TechnicianEmployeeId", employee.ID);
                DateTime now = DateTime.Now;
                Database.PutParameter(dbCommand, "@StartDateStart", new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0));                                
                Database.PutParameter(dbCommand, "@StartDateEnd", new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999));                                

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workList.Add(Load(dataReader));
                    }
                }
            }
            return workList;
        }

        #endregion        
    }
}
      