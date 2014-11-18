using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Servman.Domain.intermediate
{
    public class Deflood
    {
        #region SourceOfFlood

        private string m_sourceOfFlood;
        public string SourceOfFlood
        {
            get { return (m_sourceOfFlood ?? string.Empty).ToUpper(); }
            set { m_sourceOfFlood = value; }
        }

        #endregion

        #region DateOfFlood

        private DateTime m_dateOfFlood;
        public DateTime DateOfFlood
        {
            get { return m_dateOfFlood == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_dateOfFlood; }
            set { m_dateOfFlood = value; }
        }

        #endregion

        #region AffectedRooms

        private string m_affectedRooms;
        public string AffectedRooms
        {
            get { return (m_affectedRooms ?? string.Empty).ToUpper(); }
            set { m_affectedRooms = value; }
        }

        #endregion

        #region OrderedBy

        private string m_orderedBy;
        public string OrderedBy
        {
            get { return (m_orderedBy ?? string.Empty).ToUpper(); }
            set { m_orderedBy = value; }
        }

        #endregion

        #region InsuranceAgency

        private string m_insuranceAgency;
        public string InsuranceAgency
        {
            get { return (m_insuranceAgency ?? string.Empty).ToUpper(); }
            set { m_insuranceAgency = value; }
        }

        #endregion

        #region InsuranceAgenyPhone

        private string m_insuranceAgenyPhone;
        public string InsuranceAgenyPhone
        {
            get { return (m_insuranceAgenyPhone ?? string.Empty).ToUpper(); }
            set { m_insuranceAgenyPhone = value; }
        }

        #endregion

        #region InsuranceAgent

        private string m_insuranceAgent;
        public string InsuranceAgent
        {
            get { return (m_insuranceAgent ?? string.Empty).ToUpper(); }
            set { m_insuranceAgent = value; }
        }

        #endregion

        #region InsuranceCarrier

        private string m_insuranceCarrier;
        public string InsuranceCarrier
        {
            get { return (m_insuranceCarrier ?? string.Empty).ToUpper(); }
            set { m_insuranceCarrier = value; }
        }

        #endregion

        #region InsuranceAdjustor

        private string m_insuranceAdjustor;
        public string InsuranceAdjustor
        {
            get { return (m_insuranceAdjustor ?? string.Empty).ToUpper(); }
            set { m_insuranceAdjustor = value; }
        }

        #endregion

        #region InsuranceAdjustorPhone

        private string m_insuranceAdjustorPhone;
        public string InsuranceAdjustorPhone
        {
            get { return (m_insuranceAdjustorPhone ?? string.Empty).ToUpper(); }
            set { m_insuranceAdjustorPhone = value; }
        }

        #endregion


        
    }
}
