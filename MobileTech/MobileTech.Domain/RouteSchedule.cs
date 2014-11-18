using System;
using System.Data;
using MobileTech.Data;
using System.Collections.Generic;

namespace MobileTech.Domain
{
    public partial class RouteSchedule
    {
        #region Constructors
        public RouteSchedule()
        {

        }
        #endregion

        #region Extra fields
        Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }
        #endregion

        #region Finders

        #region Sql Statements
        const String SqlFindToday = "Select LocationId,RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate From RouteSchedule Where RouteNumber = @RouteNumber and LocationId = @LocationId and DayOfWeekNumber = @DayOfWeekNumber and StartDate <= getdate() and EndDate >= getdate() order by Sequence";
        const String SqlFindByDate = "Select LocationId,RouteScheduleId,DayOfWeekNumber,CustomerId,RouteNumber,Frequency,Sequence,StartDate,EndDate From RouteSchedule Where RouteNumber = @RouteNumber and LocationId = @LocationId and DayOfWeekNumber = @DayOfWeekNumber and StartDate <= @Date and EndDate >= @Date2 order by Sequence";
        #endregion


        public static int Find(Route route, DateTime date, List<RouteSchedule> rv)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindByDate);



            Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
            Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
            Database.PutParameter(dbCommand, "@DayOfWeekNumber", (int)date.DayOfWeek);
            Database.PutParameter(dbCommand, "@Date", date);
            Database.PutParameter(dbCommand, "@Date2", date);

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    RouteSchedule routeSchedule = Load(dataReader);

                    rv.Add(routeSchedule);
                }
            }

            return rv.Count;
        }

        public static int FindToday(Route route, List<RouteSchedule> rv)
        {
            return Find(route, DateTime.Now, rv);
        }


        public static bool IsScheduleExists(Route route, DateTime date)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindByDate);

            Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
            Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
            Database.PutParameter(dbCommand, "@DayOfWeekNumber", (int)date.DayOfWeek);
            Database.PutParameter(dbCommand, "@Date", date);
            Database.PutParameter(dbCommand, "@Date2", date);

            using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
            {
                return dataReader.Read();
            }
        }

        #endregion
    }
}
