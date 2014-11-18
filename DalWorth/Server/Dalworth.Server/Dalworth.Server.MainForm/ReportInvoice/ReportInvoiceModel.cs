using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Reports;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportInvoice
{
    public class ReportInvoiceModel : IModel
    {
        #region StartDate

        private DateTime m_startDate;
        public DateTime StartDate
        {
            get { return m_startDate; }
        }

        #endregion

        #region EndDate

        private DateTime m_endDate;
        public DateTime EndDate
        {
            get { return m_endDate; }
        }

        #endregion

        #region InvoiceWrappers

        private BindingList<InvoiceWrapper> m_invoiceWrappers;
        public BindingList<InvoiceWrapper> InvoiceWrappers
        {
            get { return m_invoiceWrappers; }
        }

        #endregion

        #region Init

        public void Init()
        {

        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData(DateTime startDate, DateTime endDate)
        {
            m_startDate = startDate;
            m_endDate = endDate;

            m_invoiceWrappers = new BindingList<InvoiceWrapper>(
                InvoiceWrapper.Find(startDate, endDate));
        }

        #endregion        
    }    

    public class InvoiceWrapper
    {
        private QbInvoice m_invoice;
        private Customer m_customer;

        #region InvoiceWrapper

        public InvoiceWrapper(QbInvoice invoice, Customer customer)
        {
            m_invoice = invoice;
            m_customer = customer;
        }

        #endregion

        #region Date

        public DateTime Date
        {
            get { return m_invoice.CreatedDate; }
        }

        #endregion

        #region Number

        public string Number
        {
            get { return m_invoice.RefNumber; }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region Memo

        public string Memo
        {
            get { return m_invoice.Memo; }
        }

        #endregion

        #region Amount

        public decimal Amount
        {
            get { return m_invoice.TotalAmount; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"select i.*, c.* from qbInvoice i                
              inner join QbCustomer qc on qc.id = i.qbCustomerId
              inner join Customer c on c.ID = qc.CustomerId
                where Date(CreatedDate) >= ?StartDate and Date(CreatedDate) <= ?EndDate
              order by i.CreatedDate desc";

        public static List<InvoiceWrapper> Find(DateTime startDate, DateTime endDate)
        {
            List<InvoiceWrapper> result = new List<InvoiceWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?StartDate", startDate.Date);
                Database.PutParameter(dbCommand, "?EndDate", endDate.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(new InvoiceWrapper(QbInvoice.Load(dataReader),
                            Customer.Load(dataReader, QbInvoice.FieldsCount)));
                    }
                }

                return result;
            }
            
        }

        #endregion
    }
}
