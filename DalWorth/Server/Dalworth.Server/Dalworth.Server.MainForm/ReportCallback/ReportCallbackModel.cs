using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportCallback
{
    public class ReportCallbackModel : IModel
    {
        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region CallbackList

        private BindingList<CallbackReportWrapper> m_callbackList;
        public BindingList<CallbackReportWrapper> CallbackList
        {
            get { return m_callbackList; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData()
        {
            List<CallbackReportWrapper> callbackList = CallbackReportWrapper.Find();
            callbackList.Sort();

            m_callbackList = new BindingList<CallbackReportWrapper>(callbackList);            
        }

        #endregion        
    }    
}
