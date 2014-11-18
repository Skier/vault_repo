using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain.package;

namespace Dalworth.Server.Domain
{
    public class StartDayPackage
    {
        #region Fields

        #region Addresses

        private List<Address> m_addresses;
        public List<Address> Addresses
        {
            get { return m_addresses; }
            set { m_addresses = value; }
        }

        #endregion

        #region Customers

        private List<Customer> m_customers;
        public List<Customer> Customers
        {
            get { return m_customers; }
            set { m_customers = value; }
        }

        #endregion

        #region Items

        private List<Item> m_items;
        public List<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        #endregion

        #region Van

        private Van m_van;
        public Van Van
        {
            get { return m_van; }
            set { m_van = value; }
        }

        #endregion        

        #region Visits

        private List<Visit> m_visits;
        public List<Visit> Visits
        {
            get { return m_visits; }
            set { m_visits = value; }
        }

        #endregion

        #region Tasks

        private List<TaskPackage> m_tasks;
        public List<TaskPackage> Tasks
        {
            get { return m_tasks; }
            set { m_tasks = value; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region WorkDetails

        private List<WorkDetail> m_workDetails;
        public List<WorkDetail> WorkDetails
        {
            get { return m_workDetails; }
            set { m_workDetails = value; }
        }

        #endregion

        #region WorkEquipments

        private List<WorkEquipment> m_workEquipments;
        public List<WorkEquipment> WorkEquipments
        {
            get { return m_workEquipments; }
            set { m_workEquipments = value; }
        }

        #endregion

        #endregion        
    }
}
