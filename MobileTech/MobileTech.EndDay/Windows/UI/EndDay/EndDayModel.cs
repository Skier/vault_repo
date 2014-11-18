using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.Data;
using MobileTech.ServiceLayer;
using MobileTech.Windows.UI.Odometer;

namespace MobileTech.Windows.UI.EndDay
{
	public class EndDayModel:IOdometerModel
    {
        #region Service

        public void Save()
		{
            try
            {
                Database.Begin();

                PeriodTransaction.ChangePeriod(PeriodTransactionTypeEnum.EOP);

                RouteScheduleQueue.CleanUp(Route.Current);

                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();

                throw e;
            }
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
    }
}
