using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.Data;
using MobileTech.ServiceLayer;
using MobileTech.Windows.UI.Odometer;

namespace MobileTech.Windows.UI.StartDay
{
	public class StartDayModel:IModel,IOdometerModel
    {
        #region Fields
        List<Location> m_locationList;
        List<Route> m_routeList;
        List<Employee> m_employeeList;


        Location m_location;
        Route m_route;
        Employee m_employee;

        DateTime m_date = DateTime.Now;

        #endregion

        #region Constructors

        public StartDayModel()
		{

        }

        #endregion

        #region IModel Members


        public void Init()
        {
            m_locationList = Location.Find();
            m_location = m_locationList[0];
            m_routeList = Route.Find();
            m_route = m_routeList[0];
            m_employeeList = Employee.Find();
            m_employee = m_employeeList[0];
        }

        #endregion

        #region Model Data

        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }

        public Route Route
        {
            get { return m_route; }
            set { m_route = value; }
        }

        public Employee Employee
        {
            get { return m_employee; }
            set { m_employee = value; }
        }

        public Location Location
        {
            get { return m_location;  }
            set 
            { 
                m_location = value;
            }
        }

        public List<Employee> EmployeeList
        {
            get { return m_employeeList; }
            set { m_employeeList = value; }
        }

        #endregion

        #region IOdometerModel Members

        int m_odometerReading;

        public int OdometerReading
        {
            get
            {
                return m_odometerReading;
            }
            set
            {
                m_odometerReading = value;
            }
        }

        #endregion

        #region Events

        #endregion

        #region Service

        public void Save()
		{

            if (Location == null)
                throw new MobileTechException("Location not defined");

            if (Employee == null)
                throw new MobileTechException("Employee not defined");

            if (Route == null)
                throw new MobileTechException("Route not defined");

            if (!(RouteSchedule.IsScheduleExists(Route,Date)))
                throw new MobileTechException("Schedule for this route not defined");

            Database.Begin();

            try
            {

                m_route.EmployeeId = Employee.EmployeeId;

                Route.Activate(m_route);

                Route.Update(m_route);

                PeriodTransaction.ChangePeriod(PeriodTransactionTypeEnum.SOP);

                RouteScheduleQueue.CleanUp(m_route);

                RouteScheduleQueue.Populate(m_route, Date);

                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();

                throw e;
            }
		}

        #endregion
    }
}
