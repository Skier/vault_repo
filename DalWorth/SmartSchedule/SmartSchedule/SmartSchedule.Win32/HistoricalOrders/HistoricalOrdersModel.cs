using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.HistoricalOrders
{
    public class HistoricalOrdersModel : IModel
    {
        #region BookingEngine

        private BookingEngine m_bookingEngine;
        public BookingEngine BookingEngine
        {
            get { return m_bookingEngine; }
            set { m_bookingEngine = value; }
        }

        #endregion

        #region Orders

        private BindingList<OrderHistory> m_orders;
        public BindingList<OrderHistory> Orders
        {
            get { return m_orders; }
            set { m_orders = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_orders = new BindingList<OrderHistory>();
            RefreshOrders(null);
        }

        #endregion

        #region RefreshOrders

        public void RefreshOrders(DateTime? date)
        {
            List<OrderHistory> orders = OrderHistory.FindSorted(date);
            m_orders.Clear();
            foreach (OrderHistory order in orders)
                m_orders.Add(order);
        }

        #endregion
    }
}
