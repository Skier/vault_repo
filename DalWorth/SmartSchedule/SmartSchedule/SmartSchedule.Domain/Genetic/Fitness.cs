using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.Domain.Genetic
{
    public class Fitness
    {
        private double m_driveDistance;
        private double m_balanceStandardDeviation;
        private int m_visitsInSecondaryArea;
        private int m_temporaryAssignedVisits;

        #region Constructor

        public Fitness(double driveDistance, double balanceStandardDeviation, int visitsInSecondaryArea, int temporaryAssignedVisits)
        {
            m_driveDistance = driveDistance;
            m_balanceStandardDeviation = balanceStandardDeviation;
            m_visitsInSecondaryArea = visitsInSecondaryArea;
            m_temporaryAssignedVisits = temporaryAssignedVisits;
        }

        #endregion

        #region DriveDistance

        public double DriveDistance
        {
            get { return m_driveDistance; }
        }

        #endregion

        #region BalanceStandardDeviation

        public double BalanceStandardDeviation
        {
            get { return m_balanceStandardDeviation; }
        }

        #endregion

        #region VisitsInSecondaryArea

        public int VisitsInSecondaryArea
        {
            get { return m_visitsInSecondaryArea; }
        }

        #endregion

        #region TemporaryAssignedVisits

        public int TemporaryAssignedVisits
        {
            get { return m_temporaryAssignedVisits; }
        }

        #endregion

        #region TotalValue

        public double TotalValue
        {
            get 
            {                
                return
                    m_balanceStandardDeviation * Utils.BalanceToMileMultiplier +
                    m_visitsInSecondaryArea * Utils.SecondaryAreaToMileMultiplier + 
                    m_temporaryAssignedVisits * Utils.TempAssignmentToMileMultiplier + 
                    DriveDistance;
            }
        }

        #endregion
    }
}
