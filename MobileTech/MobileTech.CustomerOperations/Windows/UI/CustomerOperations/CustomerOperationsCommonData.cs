using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Odometer;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.CustomerOperations
{
    public class CustomerOperationsCommonData:IOdometerModel
    {
        public CustomerOperationsCommonData(RouteScheduleQueue routeScheduleQueue)
        {
            m_routeScheduleQueue = routeScheduleQueue;
        }


        #region RouteScheduleQueue

        private RouteScheduleQueue m_routeScheduleQueue;

        public RouteScheduleQueue RouteScheduleQueue
        {
            get { return m_routeScheduleQueue; }
        }

        #endregion

        #region IOdometerModel

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
    }
}
