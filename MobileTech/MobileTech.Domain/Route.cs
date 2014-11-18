using System;
using System.Data;
using MobileTech.Data;
using System.Collections.Generic;

namespace MobileTech.Domain
{
    public partial class Route
    {

        #region Constructors

        public Route()
        {

        }

        #endregion

        #region Extra fields

        public RouteStatusEnum Status
        {
            get { return (RouteStatusEnum)m_routeStatusId; }
            set { m_routeStatusId = (int)value; }
        }

        Location m_location;

        public Location Location
        {
            get 
            {
                if (m_location == null)
                {
                    if (m_locationId == 0)
                        throw new MobileTechException("Location id = 0");


                    m_location = Location.FindByPrimaryKey(m_locationId);
                }

                return m_location; 
            }
            set { m_location = value; }
        }


        public override string ToString()
        {
            return m_name;
        }

        #endregion

        #region Finders

        #region SQL Statements
        private const string SqlFindByEmployeeAndStatus = "Select LocationId,RouteNumber,RouteStatusId,RouteTypeId,EmployeeId,Name,DocumentNumberPrefix,DocumentNumberSequence,Active,InventorySync From Route Where EmployeeId = @EmployeeId  and LocationId = @LocationId and RouteStatusId = @RouteStatusId";
        private const String SqlFindByEmployee = "Select LocationId,RouteNumber,RouteStatusId,RouteTypeId,EmployeeId,Name,DocumentNumberPrefix,DocumentNumberSequence,Active,InventorySync From Route Where EmployeeId = @EmployeeId and LocationId = @LocationId";
        private const String SqlFindByLocation = "Select LocationId,RouteNumber,RouteStatusId,RouteTypeId,EmployeeId,Name,DocumentNumberPrefix,DocumentNumberSequence,Active,InventorySync From Route Where LocationId = @LocationId";
        private const String SqlFindCurrent = "Select LocationId,RouteNumber,RouteStatusId,RouteTypeId,EmployeeId,Name,DocumentNumberPrefix,DocumentNumberSequence,Active,InventorySync From Route Where Active=1";
        private const String SqlDeactivateAll = "Update Route Set Active = 0 Where Active=1";
        private const String SqlActivate = "Update Route Set Active = 1 Where RouteNumber = @RouteNumber and LocationId = @LocationId";
        #endregion

        public static List<Route> FindBy(Employee employee)
        {
            IDbCommand command = Database.PrepareCommand(SqlFindByEmployee);

            Database.PutParameter(command, "@EmployeeId", employee.EmployeeId);
            Database.PutParameter(command, "@LocationId", employee.LocationId);

            List<Route> rv = new List<Route>();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    rv.Add(Load(dataReader));
                }
            }

            return rv;
        }

        public static Route FindByEmployee(Employee employee)
        {
            IDbCommand command = Database.PrepareCommand(SqlFindByEmployee);

            Database.PutParameter(command, "@EmployeeId", employee.EmployeeId);
            Database.PutParameter(command, "@LocationId", employee.LocationId);


            using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (dataReader.Read())
                {
                    return Load(dataReader);
                }
            }

            return null;
        }



        public static Route FindCurrent()
        {
            IDbCommand command = Database.PrepareCommand(SqlFindCurrent);

            using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (dataReader.Read())
                {
                    return Load(dataReader);
                }
            }

            return null;
        }

        public static Route FindBy(Employee employee, RouteStatusEnum status)
        {
            IDbCommand command = Database.PrepareCommand(SqlFindByEmployeeAndStatus);

            Database.PutParameter(command, "@EmployeeId", employee.EmployeeId);
            Database.PutParameter(command, "@RouteStatusId", (int)status);

            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    return Load(dataReader);
                }
            }

            return null;
        }

        public static void FindBy(Location location,List<Route> rv)
        {
            IDbCommand command = Database.PrepareCommand(SqlFindByLocation);
            Database.PutParameter(command, "@LocationId", location.LocationId);


            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rv.Add(Load(reader));
                }
            }

        }

        #endregion

        #region Service

        private static Route s_currentRoute;

        public static Route Current
        {
            get 
            {
                if (s_currentRoute == null)
                {
                    s_currentRoute = FindCurrent();
                }

                return s_currentRoute; 
            }
            set 
            {
                s_currentRoute = value;
            }
        }

        [TransactionRequired]
        public static void Activate(Route route)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlDeactivateAll);

            dbCommand.ExecuteNonQuery();

            dbCommand = Database.PrepareCommand(SqlActivate);

            Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
            Database.PutParameter(dbCommand, "@LocationId", route.LocationId);

            int rowsAffected = dbCommand.ExecuteNonQuery();

            if (rowsAffected < 1)
                throw new MobileTechException("Route activate error");
        }


        const String SqlUpdateStatus = "Update Route Set RouteStatusId = @RouteStatusId where RouteNumber = @RouteNumber and LocationId = @LocationId";

        [TransactionRequired]
        public static void ChangeStatus(RouteStatusEnum newStatus)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateStatus))
            {

                Database.PutParameter(dbCommand, "@RouteStatusId", (int)newStatus);
                Database.PutParameter(dbCommand, "@LocationId", Current.LocationId);
                Database.PutParameter(dbCommand, "@RouteNumber", Current.RouteNumber);

                dbCommand.ExecuteNonQuery();
            }
            
            Current.Status = newStatus;
        }


        public static void Reset()
        {
            s_currentRoute = null;
        }

        #endregion


    }
}
