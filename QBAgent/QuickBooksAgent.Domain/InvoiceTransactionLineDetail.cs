using System;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class InvoiceTransactionLineDetail : ICounterField
    {
        public InvoiceTransactionLineDetail() { }

        #region Delete by InvoiceTransactionId

        private const String SqlDeleteByInvoiceTransactionId =
            @"Delete From 
                InvoiceTransactionLineDetail  
            Where 
                InvoiceTransactionId = @InvoiceTransactionId ";

        public static void Delete(int invoiceTransactionId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByInvoiceTransactionId))
            {
                Database.PutParameter(dbCommand, "@InvoiceTransactionId", invoiceTransactionId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_invoiceTransactionLineDetailId;
            }
            set
            {
                m_invoiceTransactionLineDetailId = value;
            }
        }

        public string CounterName
        {
            get { return "InvoiceTransactionLineDetail"; }
        }

        #endregion
    }
}
      