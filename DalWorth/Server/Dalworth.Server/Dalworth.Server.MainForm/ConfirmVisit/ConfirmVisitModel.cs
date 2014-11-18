using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.Dashboard;
using Dalworth.Server.Windows;
using DevExpress.XtraScheduler;

namespace Dalworth.Server.MainForm.ConfirmVisit
{
    public class ConfirmVisitModel : IModel
    {
        #region CurrentDispatch

        private Employee m_currentDispatch;
        public Employee CurrentDispatch
        {
            get { return m_currentDispatch; }
            set { m_currentDispatch = value; }
        }

        #endregion

        #region Appointment

        private AppointmentWrapper m_appointment;
        public AppointmentWrapper Appointment
        {
            get { return m_appointment; }
            set { m_appointment = value; }
        }

        #endregion

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region Address

        private Address m_customerAddress;
        public Address CustomerAddress
        {
            get { return m_customerAddress; }
        }

        private Address m_serviceAddress;
        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
        }

        #endregion

        #region ConfirmTimeFrameStart

        private DateTime m_confirmTimeFrameStart;
        public DateTime ConfirmTimeFrameStart
        {
            get { return m_confirmTimeFrameStart; }
        }

        #endregion

        #region DispatchTime

        private DateTime? m_dispatchTime;
        public DateTime? DispatchTime
        {
            get { return m_dispatchTime; }
            set { m_dispatchTime = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            try
            {
                m_customer = Customer.FindBy(m_appointment.Visit);
            }
            catch (DataNotFoundException) {}

            if (m_customer != null)
                m_customerAddress = Address.FindByPrimaryKey(m_customer.AddressId.Value);

            if (m_appointment.Visit.ServiceAddressId.HasValue)
                m_serviceAddress = Address.FindByPrimaryKey(m_appointment.Visit.ServiceAddressId.Value);

            if (!m_dispatchTime.HasValue)
                m_confirmTimeFrameStart = Visit.GetSuggestedConfirmTimeStart(m_appointment.Start.AddMinutes(30));
            else
            {
                DateTime estimatedArrivalTime = m_dispatchTime.Value.AddMinutes(30);

                if (estimatedArrivalTime >= m_appointment.Visit.ConfirmedFrameBegin.Value
                        && estimatedArrivalTime <= m_appointment.Visit.ConfirmedFrameEnd.Value)
                {
                    //When within previous confirmed time frame - do not change it
                    m_confirmTimeFrameStart = m_appointment.Visit.ConfirmedFrameBegin.Value;
                }
                else
                {
                    m_confirmTimeFrameStart = Visit.GetSuggestedConfirmTimeStart(estimatedArrivalTime);
                }                
            }
        }

        #endregion
    }
}
