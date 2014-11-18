using System;
using System.Collections.Generic;
using System.Data;
using MobileTech.Data;

namespace MobileTech.Domain
{
    public partial class RouteScheduleQueue:ICounterField
    {

        #region Constructors

        public RouteScheduleQueue()
        {

        }

        #endregion

        #region Extra fields

        RouteSchedule m_schedule;
        public RouteSchedule Schedule
        {
            get { return m_schedule; }
            set { m_schedule = value; }
        }

        Customer m_customer;

        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        public bool IsServiced
        {
            get
            {
                return m_routeScheduleQueueStatusId == (int)RouteScheduleQueueStatusEnum.Serviced;
            }
        }

        public RouteScheduleQueueStatusEnum Status
        {
            get
            {
                return (RouteScheduleQueueStatusEnum)m_routeScheduleQueueStatusId;
            }
            set
            {
                m_routeScheduleQueueStatusId = (int)value;
            }
        }


        #endregion

        #region Finders

        #region Find by route schedule

        const String SqlFindByRouteSchedule = "Select RouteScheduleQueueId,LocationId,RouteScheduleId,RouteScheduleQueueStatusId,DateCreated  From RouteScheduleQueue Where RouteScheduleId = @RouteScheduleId and LocationId = @LocationId";

        public static RouteScheduleQueue FindBy(RouteSchedule routeSchedule)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindByRouteSchedule);

            Database.PutParameter(dbCommand, "@RouteScheduleId", routeSchedule.RouteScheduleId);

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                if (dataReader.Read())
                    return Load(dataReader);
            }

            return null;
        }

        #endregion

        #region Find by current route

        public static List<RouteScheduleQueue> FindCurrent()
        {
            return FindBy(Route.Current);
        }

        #endregion

        #region Find By Route

        const String SqlFindByRoute = "Select " +
            /* 0 */	"RouteScheduleQueue.RouteScheduleQueueId," +
            /* 1 */ "RouteScheduleQueue.LocationId, " +
            /* 2 */ "RouteScheduleQueue.RouteScheduleId, " +
            /* 3 */ "RouteScheduleQueue.RouteScheduleQueueStatusId," +
            /* 4 */ "RouteScheduleQueue.DateCreated," +
            /* 5 */ "RouteScheduleQueueStatus.Name," +
            /* 6 */ "RouteScheduleQueueStatus.Description," +
            /* 7 */ "Customer.CustomerId," +
            /* 8 */ "Customer.Name " +
                "From RouteScheduleQueue " +
                "Inner Join RouteSchedule On RouteScheduleQueue.RouteScheduleId = RouteSchedule.RouteScheduleId and RouteScheduleQueue.LocationId = RouteSchedule.LocationId " +
                "Inner Join RouteScheduleQueueStatus On RouteScheduleQueueStatus.RouteScheduleQueueStatusId = RouteScheduleQueue.RouteScheduleQueueStatusId " +
            /*"Inner Join RouteCustomer On RouteSchedule.CustomerId = RouteCustomer.CustomerId " +*/
                "Inner Join Customer On RouteSchedule.CustomerId = Customer.CustomerId " +
                "Where RouteSchedule.RouteNumber = @RouteNumber and RouteSchedule.LocationId = @LocationId Order By Customer.Name";


        public static List<RouteScheduleQueue> FindBy(Route route)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindByRoute);

            Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
            Database.PutParameter(dbCommand, "@LocationId", route.LocationId);

            List<RouteScheduleQueue> rv = new List<RouteScheduleQueue>();

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    RouteScheduleQueue routeScheduleQueue = Load(dataReader);
                    routeScheduleQueue.Customer = new Customer(
                        dataReader.GetInt32(7),
                        dataReader.GetString(8));

                    rv.Add(routeScheduleQueue);
                }
            }

            return rv;
        }

        #endregion

        #endregion

        #region Service

        #region Populate

        public static void Populate(Route route, DateTime date)
        {
            List<RouteSchedule> routeScheduleList = new List<RouteSchedule>();

            RouteSchedule.Find(route, date, routeScheduleList);

            if (routeScheduleList.Count == 0)
                throw new Exception("Route schedule not found");

            foreach (RouteSchedule routeSchedule in routeScheduleList)
            {
                RouteScheduleQueue item = new RouteScheduleQueue(0,
                    route.LocationId,
                    routeSchedule.RouteScheduleId,
                    (int)RouteScheduleQueueStatusEnum.Init,
                    date);

                Counter.Assign(item);

                Insert(item);
            }

        }

        public static void Populate(Route route)
        {
            Populate(route, DateTime.Now);
        }

        #endregion

        #region CleanUp

        const String SqlCleanUp = "Delete From RouteScheduleQueue Where RouteScheduleId in (select RouteScheduleId from RouteSchedule where RouteNumber = @RouteNumber and LocationId = @LocationId)";

        public static void CleanUp(Route route)
        {

            IDbCommand dbCommand = Database.PrepareCommand(SqlCleanUp);

            Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
            Database.PutParameter(dbCommand, "@LocationId", route.LocationId);

            dbCommand.ExecuteNonQuery();

        }

        #endregion

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
               return m_routeScheduleQueueId;
            }
            set
            {
                m_routeScheduleQueueId = value;
            }
        }

        const String counterName = "RouteScheduleQueue";
        public String CounterName
        {
            get 
            {
                return counterName;
            }
        }

        #endregion
    }
}
