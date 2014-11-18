using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public partial class TimeFrame
    {
        public TimeFrame(){}

        #region Date

        public DateTime Date
        {
            get { return m_timeStart.Date; }
        }

        #endregion        

        #region Text

        public string Text
        {
            get
            {
                if (m_timeStart.Hour == 1 && m_timeEnd.Hour == 23)
                    return "ANY";
                return Utils.FormatTime(m_timeStart) + "-" + Utils.FormatTime(m_timeEnd);
            }
        }

        #endregion

        #region TimeFrames

        private static Dictionary<int, TimeFrame> m_timeFrames;
        public static Dictionary<int, TimeFrame> TimeFrames
        {
            get
            {
                if (m_timeFrames != null)
                    return m_timeFrames;

                List<TimeFrame> timeFrames = Find();
                m_timeFrames = new Dictionary<int, TimeFrame>();
                foreach (TimeFrame timeFrame in timeFrames)
                    m_timeFrames.Add(timeFrame.ID, timeFrame);
                return m_timeFrames;
            }
        }


        #endregion    

        #region GetMinVisitStartTime

        public DateTime GetMinVisitStartTime(Technician technician)
        {
            return Utils.RoundTo15Min(TimeStart.AddMinutes(-technician.DriveTimeMinutes));
        }

        #endregion

        #region GetMaxVisitStartTime

        public DateTime GetMaxVisitStartTime(Technician technician)
        {
            //Should arrive 30 min before frame end
            return Utils.RoundTo15Min(TimeEnd.AddMinutes(-technician.DriveTimeMinutes - 30));
        }

        #endregion

        #region IsSecondaryAreaPlacement

        private bool m_isSecondaryAreaPlacement;
        public bool IsSecondaryAreaPlacement
        {
            get { return m_isSecondaryAreaPlacement; }
            set { m_isSecondaryAreaPlacement = value; }
        }

        #endregion

        #region IsSecondaryAreaPlacementText

        public string IsSecondaryAreaPlacementText
        {
            get
            {
                if (!IsAllowed)
                    return string.Empty;

                return IsSecondaryAreaPlacement ? "Yes" : string.Empty;
            }
        }

        #endregion


        #region IsDelayedBooking

        private bool m_isDelayedBooking;
        public bool IsDelayedBooking
        {
            get { return m_isDelayedBooking; }
            set { m_isDelayedBooking = value; }
        }

        #endregion

        #region IsDelayedBookingText

        public string IsDelayedBookingText
        {
            get
            {
                if (!IsAllowed)
                    return string.Empty;

                return IsDelayedBooking ? "Yes" : string.Empty;
            }
        }

        #endregion               

        #region Rank

        private int m_rank;
        /// <summary>
        /// Ranks goes from 0. Higher rank - less priority        
        /// </summary>
        public int Rank
        {
            get { return m_rank; }
            set { m_rank = value; }
        }

        private int m_primaryRank;
        public int PrimaryRank
        {
            get { return m_primaryRank; }
            set { m_primaryRank = value; }
        }

        private int m_secondaryRank;
        public int SecondaryRank
        {
            get { return m_secondaryRank; }
            set { m_secondaryRank = value; }
        }

        #endregion

        #region SecondaryRankText

        public string SecondaryRankText
        {
            get
            {
                if (IsAllowed && m_secondaryRank >= 0)
                    return (m_secondaryRank + 1).ToString();
                return string.Empty;
            }
        }

        #endregion

        #region IsAny

        public bool IsAny
        {
            get { return TimeStart.Hour == 1; }
        }

        #endregion

        #region IsAllowed

        private bool m_isAllowed;
        public bool IsAllowed
        {
            get { return m_isAllowed; }
            set { m_isAllowed = value; }
        }

        #endregion

        #region TimeDistance

        public double TimeDistance(TimeFrame frame)
        {
            return Math.Abs(TimeStart.Subtract(frame.TimeStart).TotalMinutes);
        }

        #endregion

    }
}
