using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CallbackSurvey
{   
    public class SurveyInfo
    {
        #region Constructor

        public SurveyInfo(Customer customer, Visit visit, DateTime surveyDate)
        {
            m_customer = customer;
            m_visit = visit;
            m_surveyDate = surveyDate;
        }

        #endregion


        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region SurveyDate

        private DateTime m_surveyDate;
        public DateTime SurveyDate
        {
            get { return m_surveyDate; }
            set { m_surveyDate = value; }
        }

        #endregion

        #region ExistingCallbackTransaction

        private CallbackInquireTransaction m_existingCallbackTransaction;
        public CallbackInquireTransaction ExistingCallbackTransaction
        {
            get { return m_existingCallbackTransaction; }
            set { m_existingCallbackTransaction = value; }
        }

        #endregion


        #region IsUserEntryExists

        public bool IsUserEntryExists
        {
            get
            {
                return m_callbackDaysInterval.HasValue || m_exactDate.HasValue || m_isDoNotCall;
            }
        }

        #endregion


        #region CallbackDaysInterval

        private int? m_callbackDaysInterval;
        public int? CallbackDaysInterval
        {
            get { return m_callbackDaysInterval; }
            set { m_callbackDaysInterval = value; }
        }

        #endregion

        #region ExactDate

        private DateTime? m_exactDate;
        public DateTime? ExactDate
        {
            get { return m_exactDate; }
            set { m_exactDate = value; }
        }

        #endregion

        #region IsDoNotCall

        private bool m_isDoNotCall;
        public bool IsDoNotCall
        {
            get { return m_isDoNotCall; }
            set { m_isDoNotCall = value; }
        }

        #endregion


        #region Save

        public void Save()
        {
            CallbackInquireTransaction transaction;

            if (m_existingCallbackTransaction != null)
                transaction = (CallbackInquireTransaction)m_existingCallbackTransaction.Clone();
            else
                transaction = new CallbackInquireTransaction();

            transaction.CustomerId = m_customer.ID;
            transaction.VisitId = m_visit != null ? m_visit.ID : (int?)null;
            transaction.CallbackInquireDate = m_surveyDate;
            transaction.CallbackDaysInterval = m_callbackDaysInterval;
            transaction.CallbackExactDate = m_exactDate;
            transaction.DoNotCall = m_isDoNotCall;

            if (m_existingCallbackTransaction != null)
                CallbackInquireTransaction.Update(transaction);
            else
                CallbackInquireTransaction.Insert(transaction);

            if (!CallbackInquireTransaction.IsExistTransactionAfter(m_customer.ID,
                    m_visit != null ? m_visit.ID : (int?)null, m_surveyDate))
            {
                Customer customer = Domain.Customer.FindByPrimaryKey(m_customer.ID);
                customer.CallbackInquireDate = m_surveyDate;
                customer.CallbackDaysInterval = transaction.CallbackDaysInterval;
                customer.CallbackExactDate = transaction.CallbackExactDate;
                customer.CallbackDoNotCall = transaction.DoNotCall;
                Customer.Update(customer);
            }            
        }

        #endregion
    }

    public class CallbackSurveyModel : IModel
    {
        #region SurveyInfo

        private SurveyInfo m_surveyInfo;
        public SurveyInfo SurveyInfo
        {
            get { return m_surveyInfo; }
            set { m_surveyInfo = value; }
        }

        #endregion

        #region Init

        public void Init()
        {            
            if (m_surveyInfo.Visit != null)
            {
                try
                {
                    m_surveyInfo.ExistingCallbackTransaction 
                        = CallbackInquireTransaction.FindByCustomerAndVisit(
                            m_surveyInfo.Customer.ID, m_surveyInfo.Visit.ID);
                }
                catch (DataNotFoundException) { }                
            }
        }

        #endregion        
    }
}
