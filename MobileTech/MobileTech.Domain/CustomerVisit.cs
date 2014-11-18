using System;
using System.Data;
using MobileTech.Data;


namespace MobileTech.Domain
{
    public partial class CustomerVisit:ICounterField
    {
        public CustomerVisit()
        {

        }

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_customerVisitId;
            }
            set
            {
                m_customerVisitId = value;
            }
        }

        private const String counterName = "CustomerVisit";
        public string CounterName
        {
            get { return counterName; }
        }

        #endregion


        #region Prepare

        public static CustomerVisit Prepare()
        {
            CustomerVisit customerVisit = new CustomerVisit();

            customerVisit.SessionId = Session.ActiveSession.SessionId;
            customerVisit.RouteNumber = Route.Current.RouteNumber;
            customerVisit.LocationId = Route.Current.LocationId;
            customerVisit.DateStarted = DateTime.Now;

            return customerVisit;
        }

        #endregion


        #region FindCurrent

        const String SqlFindCurrent = "Select SessionId, "
        + " CustomerVisitId, "
        + " CustomerId, "
        + " RouteNumber, "
        + " LocationId, "
        + " DateStarted, "
        + " DateFinished "
        + " From CustomerVisit Where DateFinished Is Null and LocationId = @LocationId and RouteNumber = @RouteNumber and SessionId = @SessionId";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MobileTech.Data.DataNotFoundException"></exception>
        public static CustomerVisit FindCurrent()
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindCurrent);

            Database.PutParameter(dbCommand, "@LocationId", Route.Current.LocationId);
            Database.PutParameter(dbCommand, "@RouteNumber", Route.Current.RouteNumber);
            Database.PutParameter(dbCommand, "@SessionId", Session.ActiveSession.SessionId);

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                if (dataReader.Read())
                    return Load(dataReader);
            }

            throw new DataNotFoundException("Current customer visit not found");
        }

        #endregion
    }
}
