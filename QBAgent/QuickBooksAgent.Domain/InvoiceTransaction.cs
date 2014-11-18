using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class InvoiceTransaction : ICounterField
    {
        #region InvoiceTransaction

        public InvoiceTransaction()
        {
            m_transactionLineDetails = new List<InvoiceTransactionLineDetail>();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_invoiceTransactionId;
            }
            set
            {
                m_invoiceTransactionId = value;
            }
        }

        public string CounterName
        {
            get { return "InvoiceTransaction"; }
        }

        #endregion

        #region TransactionLineDetails

        List<InvoiceTransactionLineDetail> m_transactionLineDetails;
        public List<InvoiceTransactionLineDetail> TransactionLineDetails
        {
            get { return m_transactionLineDetails; }
            set { m_transactionLineDetails = value; }
        }

        #endregion

        #region Delete by InvoiceId

        private const String SqlDeleteInvoiceId =
            @"Delete From 
                InvoiceTransaction  
            Where 
                InvoiceId = @InvoiceId ";

        public static void Delete(int invoiceId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteInvoiceId))
            {
                Database.PutParameter(dbCommand, "@InvoiceId", invoiceId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
      