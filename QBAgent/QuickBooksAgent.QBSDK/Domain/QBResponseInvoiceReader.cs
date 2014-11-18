using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseInvoiceReader : QBResponseReader<Invoice>
    {
        #region Convert
        
        protected override Invoice Convert(object item)
        {
            InvoiceRet invoiceRet = (InvoiceRet)item;                        
            
            Invoice invoice = new Invoice(
                null,
                null, 
                null,
                null,
                0,
                invoiceRet.QuickBooksTxnId,
                invoiceRet.EditSequence,
                invoiceRet.TimeCreated,
                invoiceRet.TimeModified,
                invoiceRet.TxnNumber,
                null,
                invoiceRet.TxnDate,
                invoiceRet.RefNumber,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                invoiceRet.DueDate,
                invoiceRet.ShipDate,
                invoiceRet.Subtotal,
                invoiceRet.SalesTaxPercentage,
                invoiceRet.SalesTaxTotal,
                invoiceRet.AppliedAmount,
                invoiceRet.BalanceRemaining,
                invoiceRet.Memo,
                invoiceRet.IsPaid ?? false,
                invoiceRet.IsToBePrinted ?? false,
                null,
                null,
                false,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                false);
            
            invoice.InvoiceLines = new List<InvoiceLine>();
            invoice.LinkedTransactions = new List<InvoiceTransaction>();
            
            invoice.Customer = new Customer();
            invoice.Customer.QuickBooksListId = invoiceRet.CustomerRef.ListId;
            
            if (invoiceRet.TermsRef != null)
            {
                invoice.Terms = new Terms();
                invoice.Terms.QuickBooksListId = invoiceRet.TermsRef.ListId;
            }
                                        
            if (invoiceRet.ARAccountRef != null)
            {
                //I init Account Field. It mens that it stores ListId for ARAccountId
                invoice.Account = new Account();
                invoice.Account.QuickBooksListId = invoiceRet.ARAccountRef.ListId;                    
            }
                
            
            if (invoiceRet.BillAddress != null)
            {
                invoice.BillAddr1 = invoiceRet.BillAddress.Addr1;
                invoice.BillAddr2 = invoiceRet.BillAddress.Addr2;
                invoice.BillAddr3 = invoiceRet.BillAddress.Addr3;
                invoice.BillAddr4 = invoiceRet.BillAddress.Addr4;
                invoice.BillCity = invoiceRet.BillAddress.City;
                invoice.BillCountry = invoiceRet.BillAddress.Country;
                invoice.BillPostalCode = invoiceRet.BillAddress.PostalCode;
                invoice.BillState = invoiceRet.BillAddress.State;
            }
            
            if (invoiceRet.ShipAddress != null)
            {
                invoice.ShipAddr1 = invoiceRet.ShipAddress.Addr1;
                invoice.ShipAddr2 = invoiceRet.ShipAddress.Addr2;
                invoice.ShipAddr3 = invoiceRet.ShipAddress.Addr3;
                invoice.ShipAddr4 = invoiceRet.ShipAddress.Addr4;
                invoice.ShipCity = invoiceRet.ShipAddress.City;
                invoice.ShipCountry = invoiceRet.ShipAddress.Country;
                invoice.ShipPostalCode = invoiceRet.ShipAddress.PostalCode;
                invoice.ShipState = invoiceRet.ShipAddress.State;
            }

            if (invoiceRet.DiscountLineRet != null)
            {                                   
                if (invoiceRet.DiscountLineRet.AccountRef != null)
                {
                    invoice.DiscountLineAccount = new Account();
                    invoice.DiscountLineAccount.QuickBooksListId
                        = invoiceRet.DiscountLineRet.AccountRef.ListId;
                }
                
                invoice.DiscountLineAmount = invoiceRet.DiscountLineRet.Amount;
                invoice.DiscountLineIsTaxable = invoiceRet.DiscountLineRet.IsTaxable ?? false;
                invoice.DiscountLineRatePercent = invoiceRet.DiscountLineRet.RatePercent;
            }

            if (invoiceRet.SalesTaxLineRet != null)
            {
                if (invoiceRet.SalesTaxLineRet.AccountRef != null)
                {
                    invoice.SalesTaxLineAccount = new Account();
                    invoice.SalesTaxLineAccount.QuickBooksListId
                        = invoiceRet.SalesTaxLineRet.AccountRef.ListId;                    
                }

                invoice.SalesTaxLineAmount = invoiceRet.SalesTaxLineRet.Amount;
                invoice.SalesTaxLineRatePercent = invoiceRet.SalesTaxLineRet.RatePercent;
            }
            
            if (invoiceRet.ShippingLineRet != null)
            {
                if (invoiceRet.ShippingLineRet.AccountRef != null)
                {
                    invoice.ShippingLineAccount = new Account();
                    invoice.ShippingLineAccount.QuickBooksListId
                        = invoiceRet.ShippingLineRet.AccountRef.ListId;                                        
                }

                invoice.ShippingLineAmount = invoiceRet.ShippingLineRet.Amount;
            }
            
            //Custom deserialization
            
            //Linked Transactions
            if (invoiceRet.LinkedTransactions != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LinkedTransactionRet));
                foreach (XmlElement linkedTransactionsElement in invoiceRet.LinkedTransactions)
                {
                    LinkedTransactionRet linkedTransactionRet =
                        (LinkedTransactionRet)serializer.Deserialize(new XmlNodeReader(linkedTransactionsElement));
                                        
                    InvoiceTransaction domainLinkedTransaction = new InvoiceTransaction(
                        null,
                        null,
                        0,
                        linkedTransactionRet.TxnID,
                        linkedTransactionRet.TxnDate,
                        linkedTransactionRet.RefNumber,
                        linkedTransactionRet.Amount);
                                        
                    domainLinkedTransaction.TransactionType 
                        = TransactionType.FindBy(linkedTransactionRet.TxnType);
                    
                    domainLinkedTransaction.TransactionLineDetails 
                        = new List<InvoiceTransactionLineDetail>();
                    
                                        
                    if (linkedTransactionRet.TxnLineDetails != null)
                    {
                        XmlSerializer detailsSerializer 
                            = new XmlSerializer(typeof(TxnLineDetailRet));

                        foreach (XmlElement detailElement in linkedTransactionRet.TxnLineDetails)
                        {                                
                            TxnLineDetailRet txnLineDetailRet =
                                (TxnLineDetailRet)detailsSerializer.Deserialize(new XmlNodeReader(detailElement));
                            
                            InvoiceTransactionLineDetail domainDetail = new InvoiceTransactionLineDetail();
                            domainDetail.QuickBooksTxnLineID = txnLineDetailRet.TxnLineID;
                            domainDetail.Amount = txnLineDetailRet.Amount;

                            domainLinkedTransaction.TransactionLineDetails.Add(domainDetail);
                        }                        
                    }

                    invoice.LinkedTransactions.Add(domainLinkedTransaction);                                        
                }
            }
            
            //Invoice Lines
            if (invoiceRet.InvoiceLines != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(InvoiceLineRet));
                foreach (XmlElement invoiceLineElement in invoiceRet.InvoiceLines)
                {
                    InvoiceLineRet invoiceLineRet =
                        (InvoiceLineRet)serializer.Deserialize(new XmlNodeReader(invoiceLineElement));
                    
                    InvoiceLine domainInvoiceLine = new InvoiceLine(
                        null,
                        null,
                        0,
                        invoiceLineRet.TxnLineID,
                        invoiceLineRet.Description,
                        invoiceLineRet.Quantity,
                        invoiceLineRet.Rate,
                        invoiceLineRet.RatePercent,
                        invoiceLineRet.Amount,
                        invoiceLineRet.ServiceDate,
                        invoiceLineRet.IsTaxable ?? false);
                    
                    if (invoiceLineRet.ItemRef != null)
                    {
                        domainInvoiceLine.Item = new Item();
                        domainInvoiceLine.Item.QuickBooksListId = invoiceLineRet.ItemRef.ListId;                        
                    }
                        

                    invoice.InvoiceLines.Add(domainInvoiceLine);
                }

            }

            return invoice;
        }
        
        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Invoice> item)
        {
            #region Process added invoices

            if (item.CommandType == QBCommandTypeEnum.Add)
            {
                QBAffectedObject<Invoice> expectedInvoice;
                try
                {
                    expectedInvoice = new QBAffectedObject<Invoice>(Invoice.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
                
                
                Invoice invoice = item.DomainObject;

                expectedInvoice.DomainObject.EditSequence = invoice.EditSequence;
                expectedInvoice.DomainObject.QuickBooksTxnId = invoice.QuickBooksTxnId;
                expectedInvoice.DomainObject.EntityState = EntityState.Synchronized;

                Invoice.Update(expectedInvoice.DomainObject);

                int responseLineCounter = 0;
                if (expectedInvoice.DomainObject.InvoiceLines != null)
                {
                    foreach (InvoiceLine invoiceLine in expectedInvoice.DomainObject.InvoiceLines)
                    {
                        invoiceLine.QuickBooksTxnLineId = invoice.InvoiceLines[responseLineCounter].QuickBooksTxnLineId;
                        InvoiceLine.Update(invoiceLine);
                        responseLineCounter++;
                    }
                }
            }

            #endregion

            #region Process queried Invoices

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Invoice invoice = item.DomainObject;
                
                try
                {
                    Invoice existingInvoice = Invoice.FindByQuickBooks(invoice.QuickBooksTxnId.Value);

                    invoice.InvoiceId = existingInvoice.InvoiceId;
                    invoice.EntityState = EntityState.Synchronized;

                    foreach (InvoiceTransaction transaction in invoice.LinkedTransactions)
                        InvoiceTransactionLineDetail.Delete(transaction.InvoiceTransactionId);

                    InvoiceTransaction.Delete(invoice.InvoiceId);
                    InvoiceLine.DeleteByInvoice(invoice.InvoiceId);

                    try
                    {
                        InitIdsByQBListIds(invoice);
                        Invoice.Update(invoice);
                        InsertInvoiceDetails(invoice);
                    }
                    catch (DataNotFoundException)
                    {
                        Invoice.Delete(invoice);
                    }
                }
                catch (DataNotFoundException)
                {
                    invoice.EntityState = EntityState.Synchronized;

                    try
                    {
                        InitIdsByQBListIds(invoice);
                        Counter.Assign(invoice);
                        Invoice.Insert(invoice);
                        InsertInvoiceDetails(invoice);
                    }
                    catch (DataNotFoundException)
                    {
                         return;
                    }
                }

            }

            #endregion            
        }

        #region InitIdsByQBListIds
        /// <summary>
        /// Inits all detail ID's in invoice by given QuickBooks ListID. Throw's DataNotFoundException
        /// if no at least one detail record not found
        /// </summary>
        /// <param name="invoice"></param>
        private void InitIdsByQBListIds(Invoice invoice)
        {
            invoice.Customer = Customer.FindByQuickBooksId(invoice.Customer.QuickBooksListId.Value);

            if (invoice.Terms != null)
                invoice.Terms = Terms.FindByQuickBooksId(invoice.Terms.QuickBooksListId);

            if (invoice.Account != null)
                invoice.ARAccountId = Account.FindBy(invoice.Account.QuickBooksListId.Value).AccountId;

            if (invoice.DiscountLineAccount != null)
                invoice.DiscountLineAccountId
                    = Account.FindBy(invoice.DiscountLineAccount.QuickBooksListId.Value).AccountId;

            if (invoice.SalesTaxLineAccount != null)
                invoice.SalesTaxLineAccountId
                    = Account.FindBy(invoice.SalesTaxLineAccount.QuickBooksListId.Value).AccountId;

            if (invoice.ShippingLineAccount != null)
                invoice.ShippingLineAccountId
                    = Account.FindBy(invoice.ShippingLineAccount.QuickBooksListId.Value).AccountId;

            foreach (InvoiceLine line in invoice.InvoiceLines)
            {
                if (line.Item != null)
                    line.Item = Item.FindByQuickBooksId(line.Item.QuickBooksListId);
            }
        }

        #endregion

        #region InsertInvoiceDetails

        private void InsertInvoiceDetails(Invoice invoice)
        {
            foreach (InvoiceTransaction transaction in invoice.LinkedTransactions)
            {
                Counter.Assign(transaction);
                transaction.Invoice = invoice;
                InvoiceTransaction.Insert(transaction);

                foreach (InvoiceTransactionLineDetail detail in transaction.TransactionLineDetails)
                {
                    Counter.Assign(detail);
                    detail.InvoiceTransaction = transaction;
                    InvoiceTransactionLineDetail.Insert(detail);
                }
            }

            foreach (InvoiceLine invoiceLine in invoice.InvoiceLines)
            {
                Counter.Assign(invoiceLine);
                invoiceLine.Invoice = invoice;
                InvoiceLine.Insert(invoiceLine);
            }
        }

        #endregion


        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "InvoiceRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(InvoiceRet) ; }
        }
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "InvoiceQueryRs".Equals(nodeName)
            || "InvoiceAddRs".Equals(nodeName);
        }

        #endregion

        #region InvoiceRet

        [XmlRoot("InvoiceRet")]
        public class InvoiceRet : QBResponseItem
        {
            #region QuickBooksTxnId

            int m_quickBooksTxnId;
            [XmlElement("TxnID")]
            public int QuickBooksTxnId
            {
                get { return m_quickBooksTxnId; }
                set { m_quickBooksTxnId = value; }
            }

            #endregion            
            
            #region TxnNumber

            private int? m_txnNumber;
            [XmlElement("TxnNumber")]
            public int? TxnNumber
            {
                get { return m_txnNumber; }
                set { m_txnNumber = value; }
            }

            #endregion

            #region CustomerRef

            private QBResponseItem m_customerRef;
            [XmlElement("CustomerRef")]
            public QBResponseItem CustomerRef
            {
                get { return m_customerRef; }
                set { m_customerRef = value; }
            }

            #endregion

            #region AccountRef

            private QBResponseItem m_arAccountRef;
            [XmlElement("ARAccountRef")]
            public QBResponseItem ARAccountRef
            {
                get { return m_arAccountRef; }
                set { m_arAccountRef = value; }
            }

            #endregion

            #region TxnDate
            
            DateTime m_txnDate;
            [XmlElement("TxnDate")]
            public DateTime TxnDate
            {
                get { return m_txnDate; }
                set { m_txnDate = value; }
            }
            
            #endregion
            
            #region RefNumber
            
            string m_refNumber;
            [XmlElement("RefNumber")]
            public string RefNumber
            {
                get { return m_refNumber; }
                set { m_refNumber = value; }
            }
            
            #endregion

            #region BillAddress

            private Address m_billAddress;
            [XmlElement("BillAddress")]
            public Address BillAddress
            {
                get { return m_billAddress; }
                set { m_billAddress = value; }
            }

            #endregion

            #region ShipAddress

            private Address m_shipAddress;
            [XmlElement("ShipAddress")]
            public Address ShipAddress
            {
                get { return m_shipAddress; }
                set { m_shipAddress = value; }
            }

            #endregion

            #region TermsRef

            private QBResponseItem m_termsRef;
            [XmlElement("TermsRef")]
            public QBResponseItem TermsRef
            {
                get { return m_termsRef; }
                set { m_termsRef = value; }
            }

            #endregion

            #region DueDate
            
            DateTime? m_dueDate;
            [XmlElement("DueDate")]
            public DateTime? DueDate
            {
                get { return m_dueDate; }
                set { m_dueDate = value; }
            }
            
            #endregion

            #region ShipDate
            
            DateTime? m_shipDate;
            [XmlElement("ShipDate")]
            public DateTime? ShipDate
            {
                get { return m_shipDate; }
                set { m_shipDate = value; }
            }
            
            #endregion

            #region Subtotal

            private decimal? m_subtotal;
            [XmlElement("Subtotal")]
            public decimal? Subtotal
            {
                get { return m_subtotal; }
                set { m_subtotal = value; }
            }

            #endregion

            #region SalesTaxPercentage

            private decimal? m_salesTaxPercentage;
            [XmlElement("SalesTaxPercentage")]
            public decimal? SalesTaxPercentage
            {
                get { return m_salesTaxPercentage; }
                set { m_salesTaxPercentage = value; }
            }

            #endregion

            #region SalesTaxTotal

            private decimal? m_salesTaxTotal;
            [XmlElement("SalesTaxTotal")]
            public decimal? SalesTaxTotal
            {
                get { return m_salesTaxTotal; }
                set { m_salesTaxTotal = value; }
            }

            #endregion

            #region AppliedAmount

            private decimal? m_appliedAmount;
            [XmlElement("AppliedAmount")]
            public decimal? AppliedAmount
            {
                get { return m_appliedAmount; }
                set { m_appliedAmount = value; }
            }

            #endregion

            #region BalanceRemaining

            private decimal? m_balanceRemaining;
            [XmlElement("BalanceRemaining")]
            public decimal? BalanceRemaining
            {
                get { return m_balanceRemaining; }
                set { m_balanceRemaining = value; }
            }

            #endregion

            #region Memo
            
            string m_memo;
            [XmlElement("Memo")]
            public string Memo
            {
                get { return m_memo; }
                set { m_memo = value; }
            }
            
            #endregion            
            
            #region IsPaid

            bool? m_isPaid;
            [XmlElement("IsPaid")]
            public bool? IsPaid
            {
                get { return m_isPaid; }
                set { m_isPaid = value; }
            }
            
            #endregion
                        
            #region IsToBePrinted
            
            bool? m_isToBePrinted;
            [XmlElement("IsToBePrinted")]
            public bool? IsToBePrinted
            {
                get { return m_isToBePrinted; }
                set { m_isToBePrinted = value; }
            }
            
            #endregion

            #region LinkedTransactions

            private XmlElement[] m_linkedTransactions;
            [XmlAnyElement("LinkedTxn")]
            public XmlElement[] LinkedTransactions
            {
                get { return m_linkedTransactions; }
                set { m_linkedTransactions = value; }
            }

            #endregion        
            
            #region InvoiceLines

            private XmlElement[] m_invoiceLines;
            [XmlAnyElement("InvoiceLineRet")]
            public XmlElement[] InvoiceLines
            {
                get { return m_invoiceLines; }
                set { m_invoiceLines = value; }
            }

            #endregion        
                        
            #region DiscountLineRet

            private DiscountLine m_discountLineRet;
            [XmlElement("DiscountLineRet")]
            public DiscountLine DiscountLineRet
            {
                get { return m_discountLineRet; }
                set { m_discountLineRet = value; }
            }

            #endregion

            #region SalesTaxLineRet

            private SalesTaxLine m_salesTaxLineRet;
            [XmlElement("SalesTaxLineRet")]
            public SalesTaxLine SalesTaxLineRet
            {
                get { return m_salesTaxLineRet; }
                set { m_salesTaxLineRet = value; }
            }

            #endregion

            #region ShippingLineRet

            private ShippingLine m_shippingLineRet;
            [XmlElement("ShippingLineRet")]
            public ShippingLine ShippingLineRet
            {
                get { return m_shippingLineRet; }
                set { m_shippingLineRet = value; }
            }

            #endregion
        }

        #endregion

        #region Address

        public class Address
        {
            #region Addr1
            string m_addr1;
            [XmlElement("Addr1")]
            public string Addr1
            {
                get { return m_addr1; }
                set { m_addr1 = value; }
            }
            #endregion

            #region Addr2
            string m_addr2;
            [XmlElement("Addr2")]
            public string Addr2
            {
                get { return m_addr2; }
                set { m_addr2 = value; }
            }
            #endregion

            #region Addr3
            string m_addr3;
            [XmlElement("Addr3")]
            public string Addr3
            {
                get { return m_addr3; }
                set { m_addr3 = value; }
            }
            #endregion

            #region Addr4
            string m_addr4;
            [XmlElement("Addr4")]
            public string Addr4
            {
                get { return m_addr4; }
                set { m_addr4 = value; }
            }
            #endregion

            #region City
            string m_city;
            [XmlElement("City")]
            public string City
            {
                get { return m_city; }
                set { m_city = value; }
            }
            #endregion

            #region State
            string m_state;
            [XmlElement("State")]
            public string State
            {
                get { return m_state; }
                set { m_state = value; }
            }
            #endregion

            #region PostalCode
            string m_postalCode;
            [XmlElement("PostalCode")]
            public string PostalCode
            {
                get { return m_postalCode; }
                set { m_postalCode = value; }
            }
            #endregion

            #region Country
            string m_country;
            [XmlElement("Country")]
            public string Country
            {
                get { return m_country; }
                set { m_country = value; }
            }
            #endregion
        }

        #endregion

        #region LinkedTransactionRet

        [XmlRoot("LinkedTxn")]
        public class LinkedTransactionRet : QBResponseItem
        {
            #region TxnID

            private int m_txnID;
            [XmlElement("TxnID")]
            public int TxnID
            {
                get { return m_txnID; }
                set { m_txnID = value; }
            }

            #endregion

            #region TxnType

            private string m_txnType;
            [XmlElement("TxnType")]
            public string TxnType
            {
                get { return m_txnType; }
                set { m_txnType = value; }
            }

            #endregion

            #region TxnDate

            private DateTime m_txnDate;
            [XmlElement("TxnDate")]
            public DateTime TxnDate
            {
                get { return m_txnDate; }
                set { m_txnDate = value; }
            }

            #endregion

            #region RefNumber

            string m_refNumber;
            [XmlElement("RefNumber")]
            public string RefNumber
            {
                get { return m_refNumber; }
                set { m_refNumber = value; }
            }

            #endregion

            #region Amount

            private decimal m_amount;
            [XmlElement("Amount")]
            public decimal Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion

            #region TxnLineDetails

            private XmlElement[] m_txnLineDetails;
            [XmlAnyElement("TxnLineDetail")]
            public XmlElement[] TxnLineDetails
            {
                get { return m_txnLineDetails; }
                set { m_txnLineDetails = value; }
            }

            #endregion        
        }

        #endregion

        #region TxnLineDetailRet

        [XmlRoot("TxnLineDetail")]
        public class TxnLineDetailRet : QBResponseItem
        {
            #region TxnLineID

            private int m_txnLineID;
            [XmlElement("TxnLineID")]
            public int TxnLineID
            {
                get { return m_txnLineID; }
                set { m_txnLineID = value; }
            }

            #endregion

            #region Amount

            private decimal m_amount;
            [XmlElement("Amount")]
            public decimal Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion
        }

        #endregion
        
        #region InvoiceLineRet
        
        [XmlRoot("InvoiceLineRet")]
        public class InvoiceLineRet : QBResponseItem
        {
            #region TxnLineID
            
            int m_txnLineID;
            [XmlElement("TxnLineID")]
            public int TxnLineID
            {
                get { return m_txnLineID; }
                set { m_txnLineID = value; }
            }
            
            #endregion

            #region ItemRef

            QBResponseItem m_itemRef;
            [XmlElement("ItemRef")]
            public QBResponseItem ItemRef
            {
                get { return m_itemRef; }
                set { m_itemRef = value; }
            }
            
            #endregion

            #region Description
            
            string m_description;
            [XmlElement("Desc")]
            public string Description
            {
                get { return m_description; }
                set { m_description = value; }
            }
            
            #endregion

            #region Quantity
            
            decimal? m_quantity;
            [XmlElement("Quantity")]
            public decimal? Quantity
            {
                get { return m_quantity; }
                set { m_quantity = value; }
            }
            
            #endregion

            #region Rate
            
            decimal? m_rate;
            [XmlElement("Rate")]
            public decimal? Rate
            {
                get { return m_rate; }
                set { m_rate = value; }
            }
            
            #endregion

            #region RatePercent

            decimal? m_ratePercent;
            [XmlElement("RatePercent")]
            public decimal? RatePercent
            {
                get { return m_ratePercent; }
                set { m_ratePercent = value; }
            }
            
            #endregion           

            #region Amount

            private decimal? m_amount;
            [XmlElement("Amount")]
            public decimal? Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion

            #region ServiceDate
            
            DateTime? m_serviceDate;
            [XmlElement("ServiceDate")]
            public DateTime? ServiceDate
            {
                get { return m_serviceDate; }
                set { m_serviceDate = value; }
            }
            
            #endregion

            #region IsTaxable

            bool? m_isTaxable;
            [XmlElement("IsTaxable")]
            public bool? IsTaxable
            {
                get { return m_isTaxable; }
                set { m_isTaxable = value; }
            }

            #endregion
        }
        
        #endregion

        #region DiscountLine

        public class DiscountLine : QBResponseItem
        {
            #region Amount

            private decimal? m_amount;
            [XmlElement("Amount")]
            public decimal? Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion

            #region RatePercent

            private decimal? m_ratePercent;
            [XmlElement("RatePercent")]
            public decimal? RatePercent
            {
                get { return m_ratePercent; }
                set { m_ratePercent = value; }
            }

            #endregion

            #region IsTaxable

            bool? m_isTaxable;
            [XmlElement("IsTaxable")]
            public bool? IsTaxable
            {
                get { return m_isTaxable; }
                set { m_isTaxable = value; }
            }

            #endregion

            #region AccountRef

            private QBResponseItem m_accountRef;
            [XmlElement("AccountRef")]
            public QBResponseItem AccountRef
            {
                get { return m_accountRef; }
                set { m_accountRef = value; }
            }

            #endregion
        }

        #endregion

        #region SalesTaxLine

        public class SalesTaxLine : QBResponseItem
        {
            #region Amount

            private decimal? m_amount;
            [XmlElement("Amount")]
            public decimal? Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion

            #region RatePercent

            private decimal? m_ratePercent;
            [XmlElement("RatePercent")]
            public decimal? RatePercent
            {
                get { return m_ratePercent; }
                set { m_ratePercent = value; }
            }

            #endregion            

            #region AccountRef

            private QBResponseItem m_accountRef;
            [XmlElement("AccountRef")]
            public QBResponseItem AccountRef
            {
                get { return m_accountRef; }
                set { m_accountRef = value; }
            }

            #endregion
        }

        #endregion

        #region ShippingLine

        public class ShippingLine : QBResponseItem
        {
            #region Amount

            private decimal m_amount;
            [XmlElement("Amount")]
            public decimal Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion            

            #region AccountRef

            private QBResponseItem m_accountRef;
            [XmlElement("AccountRef")]
            public QBResponseItem AccountRef
            {
                get { return m_accountRef; }
                set { m_accountRef = value; }
            }

            #endregion
        }

        #endregion
    }
}
