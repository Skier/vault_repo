using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Invoice : ICounterField
    {
        #region Invoice

        public Invoice() 
        {
            m_entityState = new EntityState();
            m_customer = new Customer(); 
            m_invoiceLines = new List<InvoiceLine>();
            m_linkedTransactions = new List<InvoiceTransaction>();
            m_account = new Account(); //ARAccount
        }

        #endregion

        #region InvoiceLines

        List<InvoiceLine> m_invoiceLines;
        public List<InvoiceLine> InvoiceLines
        {
            get { return m_invoiceLines; }
            set { m_invoiceLines = value; }
        }

        #endregion

        #region LinkedTransactions

        List<InvoiceTransaction> m_linkedTransactions;
        public List<InvoiceTransaction> LinkedTransactions
        {
            get { return m_linkedTransactions; }
            set { m_linkedTransactions = value; }
        }

        #endregion

        #region DiscountLineAccount

        private Account m_discountLineAccount;
        public Account DiscountLineAccount
        {
            get { return m_discountLineAccount; }
            set { m_discountLineAccount = value; }
        }

        #endregion

        #region SalesTaxLineAccount

        private Account m_salesTaxLineAccount;
        public Account SalesTaxLineAccount
        {
            get { return m_salesTaxLineAccount; }
            set { m_salesTaxLineAccount = value; }
        }

        #endregion

        #region ShippingLineAccount

        private Account m_shippingLineAccount;
        public Account ShippingLineAccount
        {
            get { return m_shippingLineAccount; }
            set { m_shippingLineAccount = value; }
        }

        #endregion        

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_invoiceId;
            }
            set
            {
                m_invoiceId = value;
            }
        }

        public string CounterName
        {
            get { return "Invoice"; }
        }

        #endregion

        #region Find By Entity State

        const string SqlFindByEntityState = @"Select 
            Invoice.InvoiceId,
            Invoice.QuickBooksTxnId,  
            Invoice.EntityStateId,  
            Invoice.EditSequence,  
            Invoice.TimeCreated,  
            Invoice.TimeModified,  
            Invoice.TxnNumber,
            Invoice.CustomerId,  
            Invoice.ARAccountId,
            Invoice.TxnDate,
            Invoice.RefNumber,
            Invoice.BillAddr1,
            Invoice.BillAddr2,
            Invoice.BillAddr3,
            Invoice.BillAddr4,
            Invoice.BillCity,
            Invoice.BillState,
            Invoice.BillPostalCode,
            Invoice.BillCountry,
            Invoice.ShipAddr1,
            Invoice.ShipAddr2,
            Invoice.ShipAddr3,
            Invoice.ShipAddr4,
            Invoice.ShipCity,
            Invoice.ShipState,
            Invoice.ShipPostalCode,
            Invoice.ShipCountry,
            Invoice.TermsId,  
            Invoice.DueDate,              
            Invoice.ShipDate,  
            Invoice.Subtotal,
            Invoice.SalesTaxPercentage,
            Invoice.SalesTaxTotal,
            Invoice.AppliedAmount,
            Invoice.BalanceRemaining,
            Invoice.Memo,
            Invoice.IsPaid,
            Invoice.IsToBePrinted,  
            Invoice.DiscountLineAmount,
            Invoice.DiscountLineRatePercent,
            Invoice.DiscountLineIsTaxable,
            Invoice.DiscountLineAccountId,
            Invoice.SalesTaxLineAmount,
            Invoice.SalesTaxLineRatePercent,
            Invoice.SalesTaxLineAccountId,
            Invoice.ShippingLineAmount,
            Invoice.ShippingLineAccountId,
            Invoice.IsCustomerTaxable,
            Invoice.TaxCalculationType,

                                                         
            Customer.QuickBooksListId,
            Customer.FullName,

	        Terms.TermsId, 
	        Terms.QuickBooksListId, 
	        Terms.Name, 
	        Terms.StdDueDays, 
	        Terms.StdDiscountDays, 
	        Terms.DiscountPct,

            ArAcc.QuickBooksListId as ArAccountListID,
            DiscountAcc.QuickBooksListId as DiscountAccountListID,
            SalesTaxAcc.QuickBooksListId as SalesTaxAccountListID,
            ShippingAcc.QuickBooksListId as ShippingAccountListID

            From Invoice 
            Inner Join Customer On Customer.CustomerId = Invoice.CustomerId
            Left Outer Join Terms on Terms.TermsId = Invoice.TermsId 
            Left Outer Join Account as ArAcc on Invoice.ARAccountId = ArAcc.AccountId
            Left Outer Join Account as DiscountAcc on Invoice.DiscountLineAccountId = DiscountAcc.AccountId
            Left Outer Join Account as SalesTaxAcc on Invoice.SalesTaxLineAccountId = SalesTaxAcc.AccountId
            Left Outer Join Account as ShippingAcc on Invoice.ShippingLineAccountId = ShippingAcc.AccountId
            Where Invoice.EntityStateId = @EntityStateId";

        public static List<Invoice> FindBy(EntityState entityState)
        {
            List<Invoice> invoiceList = new List<Invoice>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Invoice invoice = Load(dataReader);

                        invoice.Customer.CustomerId = dataReader.GetInt32(7);
                        invoice.Customer.QuickBooksListId = dataReader.GetInt32(49);
                        invoice.Customer.FullName = dataReader.GetString(50);

                        if (invoice.Terms != null)
                        {
                            invoice.Terms.TermsId = dataReader.GetInt32(51);
                            invoice.Terms.QuickBooksListId = dataReader.GetInt32(52);
                            invoice.Terms.Name = dataReader.GetString(53);
                            if (!dataReader.IsDBNull(54))
                                invoice.Terms.StdDueDays = dataReader.GetInt32(54);
                            if (!dataReader.IsDBNull(55))
                                invoice.Terms.StdDiscountDays = dataReader.GetInt32(55);
                            if (!dataReader.IsDBNull(56))
                                invoice.Terms.DiscountPct = dataReader.GetDecimal(56);
                        }

                        if (!dataReader.IsDBNull(57))
                        {
                            invoice.Account.QuickBooksListId = dataReader.GetInt32(57);
                        }
                                                                        
                        if (!dataReader.IsDBNull(58))
                        {
                            invoice.DiscountLineAccount = new Account();    
                            invoice.DiscountLineAccount.QuickBooksListId = dataReader.GetInt32(58);
                        }

                        if (!dataReader.IsDBNull(59))
                        {
                            invoice.SalesTaxLineAccount = new Account();    
                            invoice.SalesTaxLineAccount.QuickBooksListId = dataReader.GetInt32(59);
                        }

                        if (!dataReader.IsDBNull(60))
                        {
                            invoice.ShippingLineAccount = new Account();
                            invoice.ShippingLineAccount.QuickBooksListId = dataReader.GetInt32(60);
                        }
                        
                        invoiceList.Add(invoice);
                    }
                }
            }

            foreach (Invoice invoice in invoiceList)
            {
                invoice.InvoiceLines = InvoiceLine.FindBy(invoice.InvoiceId);
            }

            return invoiceList;
        }
        #endregion

        #region Find By QuickBooksTxnId

        const string SqlFindByQuickBooksTxnId = @"Select 
            Invoice.InvoiceId,
            Invoice.QuickBooksTxnId,  
            Invoice.EntityStateId,  
            Invoice.EditSequence,  
            Invoice.TimeCreated,  
            Invoice.TimeModified,  
            Invoice.TxnNumber,
            Invoice.CustomerId,  
            Invoice.ARAccountId,
            Invoice.TxnDate,
            Invoice.RefNumber,
            Invoice.BillAddr1,
            Invoice.BillAddr2,
            Invoice.BillAddr3,
            Invoice.BillAddr4,
            Invoice.BillCity,
            Invoice.BillState,
            Invoice.BillPostalCode,
            Invoice.BillCountry,
            Invoice.ShipAddr1,
            Invoice.ShipAddr2,
            Invoice.ShipAddr3,
            Invoice.ShipAddr4,
            Invoice.ShipCity,
            Invoice.ShipState,
            Invoice.ShipPostalCode,
            Invoice.ShipCountry,
            Invoice.TermsId,  
            Invoice.DueDate,              
            Invoice.ShipDate,  
            Invoice.Subtotal,
            Invoice.SalesTaxPercentage,
            Invoice.SalesTaxTotal,
            Invoice.AppliedAmount,
            Invoice.BalanceRemaining,
            Invoice.Memo,
            Invoice.IsPaid,
            Invoice.IsToBePrinted,  
            Invoice.DiscountLineAmount,
            Invoice.DiscountLineRatePercent,
            Invoice.DiscountLineIsTaxable,
            Invoice.DiscountLineAccountId,
            Invoice.SalesTaxLineAmount,
            Invoice.SalesTaxLineRatePercent,
            Invoice.SalesTaxLineAccountId,
            Invoice.ShippingLineAmount,
            Invoice.ShippingLineAccountId,
            Invoice.IsCustomerTaxable,
            Invoice.TaxCalculationType

            From Invoice 
            Where QuickBooksTxnId = @QuickBooksTxnId";

        public static Invoice FindByQuickBooks(int QuickBooksTxnId)
        {            
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksTxnId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksTxnId", QuickBooksTxnId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Invoice not found");
                }
            }
        }
        #endregion

        #region Find By Customer

        const string SqlFindByCustomer = @"Select 
            Invoice.InvoiceId,
            Invoice.QuickBooksTxnId,  
            Invoice.EntityStateId,  
            Invoice.EditSequence,  
            Invoice.TimeCreated,  
            Invoice.TimeModified,  
            Invoice.TxnNumber,
            Invoice.CustomerId,  
            Invoice.ARAccountId,
            Invoice.TxnDate,
            Invoice.RefNumber,
            Invoice.BillAddr1,
            Invoice.BillAddr2,
            Invoice.BillAddr3,
            Invoice.BillAddr4,
            Invoice.BillCity,
            Invoice.BillState,
            Invoice.BillPostalCode,
            Invoice.BillCountry,
            Invoice.ShipAddr1,
            Invoice.ShipAddr2,
            Invoice.ShipAddr3,
            Invoice.ShipAddr4,
            Invoice.ShipCity,
            Invoice.ShipState,
            Invoice.ShipPostalCode,
            Invoice.ShipCountry,
            Invoice.TermsId,  
            Invoice.DueDate,              
            Invoice.ShipDate,  
            Invoice.Subtotal,
            Invoice.SalesTaxPercentage,
            Invoice.SalesTaxTotal,
            Invoice.AppliedAmount,
            Invoice.BalanceRemaining,
            Invoice.Memo,
            Invoice.IsPaid,
            Invoice.IsToBePrinted,  
            Invoice.DiscountLineAmount,
            Invoice.DiscountLineRatePercent,
            Invoice.DiscountLineIsTaxable,
            Invoice.DiscountLineAccountId,
            Invoice.SalesTaxLineAmount,
            Invoice.SalesTaxLineRatePercent,
            Invoice.SalesTaxLineAccountId,
            Invoice.ShippingLineAmount,
            Invoice.ShippingLineAccountId,
            Invoice.IsCustomerTaxable,
            Invoice.TaxCalculationType,

	        Terms.TermsId, 
	        Terms.QuickBooksListId, 
	        Terms.Name, 
	        Terms.StdDueDays, 
	        Terms.StdDiscountDays, 
	        Terms.DiscountPct

            From Invoice 
            Left Outer Join Terms on Terms.TermsId = Invoice.TermsId 
            Where CustomerId = @CustomerId";

        public static List<Invoice> FindByCustomer(int CustomerId)
        {
            List<Invoice> invoiceList = new List<Invoice>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCustomer))
            {
                Database.PutParameter(dbCommand, "@CustomerId", CustomerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Invoice invoice = Load(dataReader);

                        if (invoice.Terms != null)
                        {
                            invoice.Terms.TermsId = dataReader.GetInt32(49);
                            invoice.Terms.QuickBooksListId = dataReader.GetInt32(50);
                            invoice.Terms.Name = dataReader.GetString(51);
                            if (!dataReader.IsDBNull(52))
                                invoice.Terms.StdDueDays = dataReader.GetInt32(52);
                            if (!dataReader.IsDBNull(53))
                                invoice.Terms.StdDiscountDays = dataReader.GetInt32(53);
                            if (!dataReader.IsDBNull(54))
                                invoice.Terms.DiscountPct = dataReader.GetDecimal(54);                            
                        }

                        invoiceList.Add(invoice);
                    }
                }
            }

            return invoiceList;
        }
        #endregion        
        
        #region DeleteOlderThan

        private const string SqlDeleteOlderThanTransactionLineDetail =
            @"delete from InvoiceTransactionLineDetail
                where InvoiceTransactionId IN 
	                (
	                select distinct InvoiceTransactionId from InvoiceTransaction
	                inner join Invoice on Invoice.InvoiceId = InvoiceTransaction.InvoiceId
			                where EntityStateId = 1 and TimeModified < @Date
	                )";
        
        private const string SqlDeleteOlderThanTransaction =
            @"delete from InvoiceTransaction
                where InvoiceId IN 
	                (
	                select InvoiceId from Invoice
			                where EntityStateId = 1 and TimeModified < @Date
	                )";

        private const string SqlDeleteOlderThanInvoiceLine =
            @"delete from InvoiceLine
                where InvoiceId IN 
	                (
	                select InvoiceId from Invoice
			                where EntityStateId = 1 and TimeModified < @Date
	                )";

        private const string SqlDeleteOlderThanInvoice =
            @"delete from Invoice
	            where EntityStateId = 1 and TimeModified < @Date
            ";


        public static void DeleteOlderThan(DateTime date)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThanTransactionLineDetail))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThanTransaction))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThanInvoiceLine))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThanInvoice))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }
            
        }

        #endregion    

        #region ToString

        public override string ToString()
        {
            string rv = string.Empty;
            
            if (RefNumber != null)
                rv += "#" + RefNumber;
            
            if (TxnDate.HasValue)
            {
                if (rv == String.Empty)
                    rv += TxnDate.Value.ToString("yyyy-MM-dd");
                else
                    rv += ", " + TxnDate.Value.ToString("yyyy-MM-dd");                
            }

            if (rv == String.Empty)
                rv = "Invoice";
                        
            return rv;
        }

        #endregion
    }
}
