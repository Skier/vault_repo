using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class InvoiceLine : ICounterField
    {
        #region InvoiceLine

        public InvoiceLine() 
        { 
            m_invoice = new Invoice(); 
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_invoiceLineId;
            }
            set
            {
                m_invoiceLineId = value;
            }
        }

        public string CounterName
        {
            get { return "InvoiceLine"; }
        }

        #endregion

        #region Find By QuickBooksTxnLineId

        const string SqlFindByQuickBooksTxnLineId = @"Select 
            InvoiceLineId, 
            InvoiceId, 
            QuickBooksTxnLineId, 
            ItemId, 
            LineDescription,
            Quantity,
            Rate,
            RatePercent,
            Amount,
            ServiceDate,            
            IsTaxable

            From InvoiceLine 
            Where QuickBooksTxnLineId = @QuickBooksTxnLineId";

        public static InvoiceLine FindByQuickBooks(int QuickBooksTxnLineId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksTxnLineId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksTxnLineId", QuickBooksTxnLineId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("InvoiceLine not found");
                }
            }
        }
        #endregion

        #region DeleteByInvoice

        const string SqlDeleteByInvoice = @"Delete 
            From InvoiceLine 
            Where InvoiceId = @InvoiceId";

        public static void DeleteByInvoice(int invoiceId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByInvoice))
            {
                Database.PutParameter(dbCommand, "@InvoiceId", invoiceId);
                dbCommand.ExecuteNonQuery();
            }
        }
        #endregion

        #region Find By

        const string SqlFindByInvoice = @"Select 
            InvoiceLineId, 
            InvoiceId, 
            QuickBooksTxnLineId, 
            InvoiceLine.ItemId, 
            LineDescription,            
            Quantity,
            Rate,
            RatePercent,
            Amount,
            ServiceDate,
            IsTaxable,
            
            Item.ItemId,
            Item.QuickBooksListId,
            Item.EditSequence,
            Item.Name,
            Item.SalesPrice

            From InvoiceLine 
            Inner Join Item on Item.ItemId = InvoiceLine.ItemId
            Where InvoiceLine.InvoiceId = @InvoiceId";

        public static List<InvoiceLine> FindBy(int invoiceId)
        {
            List<InvoiceLine> invoiceList = new List<InvoiceLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByInvoice))
            {
                Database.PutParameter(dbCommand, "@InvoiceId", invoiceId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        InvoiceLine invoiceLine = Load(dataReader);

                        if (invoiceLine.Item != null)
                        {
                            invoiceLine.Item.ItemId = dataReader.GetInt32(11);
                            invoiceLine.Item.QuickBooksListId = dataReader.GetInt32(12);
                            invoiceLine.Item.EditSequence = dataReader.GetInt32(13);
                            invoiceLine.Item.Name = dataReader.GetString(14);
                            invoiceLine.Item.SalesPrice = dataReader.GetDecimal(15);                            
                        }

                        invoiceList.Add(invoiceLine);
                    }
                }
            }

            return invoiceList;
        }
        #endregion

        #region Find By Service Lines

        const string SqlFindByInvoiceServiceLines = @"Select 
            InvoiceLineId, 
            InvoiceId, 
            QuickBooksTxnLineId, 
            InvoiceLine.ItemId, 
            LineDescription,            
            Quantity,
            Rate,
            RatePercent,
            Amount,
            ServiceDate,
            IsTaxable
            
            From InvoiceLine 
            Where InvoiceLine.InvoiceId = @InvoiceId
                and InvoiceLine.ItemId is null";

        public static List<InvoiceLine> FindServiceLinesBy(int invoiceId)
        {
            List<InvoiceLine> invoiceList = new List<InvoiceLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByInvoiceServiceLines))
            {
                Database.PutParameter(dbCommand, "@InvoiceId", invoiceId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        InvoiceLine invoiceLine = Load(dataReader);
                        invoiceLine.Item = null;
                        invoiceList.Add(invoiceLine);
                    }
                }
            }

            return invoiceList;
        }
        #endregion
    }
}
