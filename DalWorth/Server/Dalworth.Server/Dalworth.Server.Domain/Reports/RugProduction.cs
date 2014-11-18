using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class RugProduction
    {
        #region Constructor

        public RugProduction(DateTime date, int pickupScheduledQty, int pickupCompletedQty, decimal pickupCompletedPct, int pickupRugsNumber, decimal pickupEstimatedAmt, decimal orderBookedAvg, int deliveryCompletedQty, int deliveryRugsNumber, decimal rugCleaningClosedAmt, decimal orderCompletedAvg)
        {
            m_date = date;
            m_pickupScheduledQty = pickupScheduledQty;
            m_pickupCompletedQty = pickupCompletedQty;
            m_pickupCompletedPct = pickupCompletedPct;
            m_pickupRugsNumber = pickupRugsNumber;
            m_pickupEstimatedAmt = pickupEstimatedAmt;
            m_orderBookedAvg = orderBookedAvg;
            m_deliveryCompletedQty = deliveryCompletedQty;
            m_deliveryRugsNumber = deliveryRugsNumber;
            m_rugCleaningClosedAmt = rugCleaningClosedAmt;
            m_orderCompletedAvg = orderCompletedAvg;
        }

        #endregion

        #region Date

        private DateTime m_date;
        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }

        #endregion

        #region PickupScheduledQty

        private int m_pickupScheduledQty;
        public int PickupScheduledQty
        {
            get { return m_pickupScheduledQty; }
            set { m_pickupScheduledQty = value; }
        }

        #endregion

        #region PickupCompletedQty

        private int m_pickupCompletedQty;
        public int PickupCompletedQty
        {
            get { return m_pickupCompletedQty; }
            set { m_pickupCompletedQty = value; }
        }

        #endregion

        #region PickupCompletedPct

        private decimal m_pickupCompletedPct;
        public decimal PickupCompletedPct
        {
            get { return m_pickupCompletedPct; }
            set { m_pickupCompletedPct = value; }
        }

        #endregion

        #region PickupRugsNumber

        private int m_pickupRugsNumber;
        public int PickupRugsNumber
        {
            get { return m_pickupRugsNumber; }
            set { m_pickupRugsNumber = value; }
        }

        #endregion

        #region PickupEstimatedAmt

        private decimal m_pickupEstimatedAmt;
        public decimal PickupEstimatedAmt
        {
            get { return m_pickupEstimatedAmt; }
            set { m_pickupEstimatedAmt = value; }
        }

        #endregion

        #region OrderBookedAvg

        private decimal m_orderBookedAvg;
        public decimal OrderBookedAvg
        {
            get { return m_orderBookedAvg; }
            set { m_orderBookedAvg = value; }
        }

        #endregion

        #region DeliveryCompletedQty

        private int m_deliveryCompletedQty;
        public int DeliveryCompletedQty
        {
            get { return m_deliveryCompletedQty; }
            set { m_deliveryCompletedQty = value; }
        }

        #endregion

        #region DeliveryRugsQty

        private int m_deliveryRugsNumber;
        public int DeliveryRugsNumber
        {
            get { return m_deliveryRugsNumber; }
            set { m_deliveryRugsNumber = value; }
        }

        #endregion

        #region RugCleaningClosedAmt

        private decimal m_rugCleaningClosedAmt;
        public decimal RugCleaningClosedAmt
        {
            get { return m_rugCleaningClosedAmt; }
            set { m_rugCleaningClosedAmt = value; }
        }

        #endregion

        #region OrderCompletedAvg

        private decimal m_orderCompletedAvg;
        public decimal OrderCompletedAvg
        {
            get { return m_orderCompletedAvg; }
            set { m_orderCompletedAvg = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"call ReportRugProduction(?StartDate, ?EndDate);";

        public static List<RugProduction> Find(DateTime startDate, DateTime endDate)
        {
            List<RugProduction> result = new List<RugProduction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        decimal pickupCompletePct =
                            dataReader.GetInt32(1) != 0 ? (dataReader.GetDecimal(2) / dataReader.GetDecimal(1)) : 0;
                        decimal orderBookedAvg =
                            dataReader.GetInt32(2) != 0 ? (dataReader.GetDecimal(4) / dataReader.GetDecimal(2)) : 0;
                        decimal orderCompletedAvg =
                            dataReader.GetInt32(5) != 0 ? (dataReader.GetDecimal(7) / dataReader.GetDecimal(5)) : 0;

                        RugProduction rugProduction = new RugProduction(
                            dataReader.GetDateTime(0),
                            dataReader.GetInt32(1),
                            dataReader.GetInt32(2),
                            pickupCompletePct,
                            dataReader.GetInt32(3),
                            dataReader.GetDecimal(4),
                            orderBookedAvg,
                            dataReader.GetInt32(5),
                            dataReader.GetInt32(6),
                            dataReader.GetDecimal(7),
                            orderCompletedAvg);

                        result.Add(rugProduction);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
