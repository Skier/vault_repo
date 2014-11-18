using System;
using System.Collections.Generic;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Data;

namespace Dalworth.Server.QuickBooks
{
    public delegate bool CanProceeed();

    public delegate void ReportStatus(string action, string status, string description);

    public class QbSyncException : Exception
    {
        public QbSyncException(string message)
            : base(message)
        {
        }
    }

    public class QbSync 
    {
        private QbGateway m_qbGateway;
        private QBSyncLog m_qbSyncLog;

        private DateTime m_syncLastRunDate = DateTime.MinValue;
        private ReportStatus m_reportStatus = null;

        private bool m_isConnected = false;

        public QbSync()
        {
            
        }

        public QbSync(ReportStatus reportStatus)
        {
            m_reportStatus = reportStatus;

            m_qbSyncLog = new QBSyncLog(-1, DateTime.Now);
            QBSyncLog.Insert(m_qbSyncLog); 
        }
       
        public void Connect()
        {
            Host.QuickBooksDebug("QbSync:Execute", "Connecting to Qb File " + Configuration.QuickBooksCompanyFile + " ....");
            m_qbGateway = new QbGateway(Configuration.QuickBooksCompanyFile, m_reportStatus);
            m_qbGateway.ConnectToQB();
            Host.QuickBooksDebug("QbSync:Execute", "Connected to Qb File " + Configuration.QuickBooksCompanyFile);
            ReportStatus("Connect", "OK", "Connected to " + Configuration.QuickBooksCompanyFile);
            m_isConnected = true;
        }

        public void Disconnect ()
        {
            if (m_qbGateway != null && m_isConnected)
            {
                m_isConnected = false;
                Host.QuickBooksDebug("QbSync:Execute", "Disconnecting from Qb File");
                m_qbGateway.DisconnectFromQB();
                Host.QuickBooksDebug("QbSync:Execute", "Disconnected from Qb File");
                ReportStatus("Disconnect", "OK", "Disconnected from " + Configuration.QuickBooksCompanyFile);
            }
        }

        public List<QbCustomer> FindSimilarCustomers(QbCustomer basedOnQbCustomer)
        {
            List<QbCustomer> similarCustomers = m_qbGateway.FindSimilarCustomers(basedOnQbCustomer);
            similarCustomers.RemoveAll(delegate(QbCustomer qbCustomer) { return qbCustomer.FullName.Contains(":");});
            return similarCustomers;
        }

        public void RefreshBaseData()
        {
            Host.QuickBooksDebug("QbSync:RefreshBaseData", "RefreshBaseData Started");

            m_qbGateway.RefreshCustomerTypes();
            ReportStatus("Refresh Customer Types", "OK", "");
           
            m_qbGateway.RefreshClasses();
            ReportStatus("Refresh Classes", "OK", "");

            m_qbGateway.RefreshSalesTaxCodes();
            ReportStatus("Refresh Tax Codes", "OK", "");

            m_qbGateway.RefreshTemplates();
            ReportStatus("Refresh Templates", "OK", "");

            m_qbGateway.RefreshInvoiceTerms();
            ReportStatus("Refresh Invoice Iterms", "OK", "");

            m_qbGateway.RefreshSalesReps();
            ReportStatus("Refresh Sales Reps", "OK", "");

            m_qbGateway.RefreshAccounts();
            ReportStatus("Refresh Accounts", "OK", "");

            m_qbGateway.RefreshItems();
            ReportStatus("Refresh Items", "OK", "");

            m_qbGateway.RefreshQbPaymentMethods();
            ReportStatus("Refresh Payment Methods", "OK", "");

            Host.QuickBooksDebug("QbSync:RefreshBaseData", "Refresh Data DONE");
        }

        public void FindCustomerListIds (string name)
        {
            List<QbCustomer> customersInQb = m_qbGateway.FindQbCustomers(name, null);

            List<string> listIds = new List<string>();
            foreach (QbCustomer customerInQb in customersInQb)
            {
                if (customerInQb.Name.ToUpper() == name.ToUpper().Trim())
                {
                    listIds.Add(customerInQb.ListId);
                }
            }
            if(listIds.Count == 0)
            {
                Host.QuickBooksDebug("QbSync:FindCustomerListIds","NOT FOUND!");
            }
            else
            {
                foreach (string listId in listIds)
                {
                    Host.QuickBooksDebug("QbSync:FindCustomerListIds", listId);
                }    
            }
        }

        public void FindMissingCustomers(int daysMissing)
        {
            Host.QuickBooksDebug("QbSync:FindMissingCustomers", "started");
            m_qbGateway.SyncMissingCustomers(daysMissing, m_qbSyncLog);
        }

        public void SyncronizeData()
        {
            if (m_syncLastRunDate == DateTime.MinValue)
            {
                m_syncLastRunDate = DateTime.Now.AddDays(-3);
            }

            Host.QuickBooksDebug("QbSync:SyncronizeData", "**** Sync customers started");

            m_qbGateway.SyncQbCustomers(DateTime.Now.AddDays(-3), m_qbSyncLog);
            m_qbGateway.SyncInvoices(DateTime.Now.AddDays(-3), m_qbSyncLog);
            m_qbGateway.SyncPayments(DateTime.Now.AddDays(-3), m_qbSyncLog);
            m_qbGateway.SyncCreditMemos(DateTime.Now.AddDays(-3), m_qbSyncLog);

            Host.QuickBooksDebug("QbSync:SyncronizeData", "COMPLETED");
        }

        public static void VoidInvoice(QbSyncRequest syncRequest)
        {
            QbInvoice invoice = QbInvoice.FindByPrimaryKey(syncRequest.QbInvoiceId.Value, null);

            Host.QuickBooksDebug("QbSync:VoidInvoice", "Void invoice " + invoice.ID);

            if (!string.IsNullOrEmpty(invoice.EditSequence))
            {
                Host.QuickBooksDebug("QbSync:VoidInvoice", "Invoice already syncronized, Ignore");
                return;
            }

            List<QbInvoiceLine> invoiceLines = QbInvoiceLine.FindByInvoiceId(invoice.ID, null);

            foreach (QbInvoiceLine line in invoiceLines)
            {
                QbInvoiceLine.Delete(line);
            }
            QbInvoice.Delete(invoice);
        }

        public void UpdateCustomer (QbSyncRequest syncRequest, ref QbCustomer qbCustomer)
        {            
            string qbXmlRequest = null, qbXmlResponse = null;

            QbCustomer parentQbCustomer = null;

            if (qbCustomer.SubLevel == 1)
                parentQbCustomer = QbCustomer.FindParent(qbCustomer.CustomerId, null);

            try
            {
                m_qbGateway.ModifyQbCustomer(ref qbCustomer, parentQbCustomer, out qbXmlRequest, out qbXmlResponse);
                QbCustomer.Update(qbCustomer);

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = true;
                detail.QbCustomerId = qbCustomer.ID;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = qbXmlRequest;
                detail.QbXmlResponse = qbXmlResponse;
                QbSyncLogDetail.Insert(detail);
            }
            catch (Exception ex)
            {
                Host.Trace("QbSync:UpdateCustomer", ex.ToString());

                QbSyncLogDetail detail = new QbSyncLogDetail();
                    detail.CompletedDate = DateTime.Now;
                    detail.IsSuccess = false;
                    detail.QbCustomerId = qbCustomer.ID;
                    detail.QbSyncActionId = qbCustomer.SubLevel == 0 ? (int)QbSyncActionEnum.CustomerMod : (int)QbSyncActionEnum.JobMod; ;
                    detail.QbSyncLogId = m_qbSyncLog.ID;
                    detail.QbXmlRequest = qbXmlRequest;
                    detail.QbXmlResponse = qbXmlResponse;
                    detail.ErrorMessage = ex.ToString();
                    QbSyncLogDetail.Insert(detail);
                    throw ex;
            }
        }

        public void AddCustomer (QbSyncRequest syncRequest, ref QbCustomer qbCustomer)
        {
            string xmlQbRequest = null, xmlQbResponse = null;
            try
            {
                m_qbGateway.CreateQbCustomer(ref qbCustomer, null, out xmlQbRequest, out xmlQbResponse);
                QbCustomer.Update(qbCustomer);
                Host.QuickBooksDebug("QbSync:AddCustomer", qbCustomer.ID.ToString());

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = true;
                detail.QbCustomerId = qbCustomer.ID;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = xmlQbRequest;
                detail.QbXmlResponse = xmlQbResponse;
                QbSyncLogDetail.Insert(detail);
        
            }
            catch (Exception ex)
            {
                Host.Trace("QbSync:AddCustomer", ex.ToString());

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = false;
                detail.QbCustomerId = syncRequest.QbCustomerId;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = xmlQbRequest;
                detail.QbXmlResponse = xmlQbResponse;
                detail.ErrorMessage = ex.ToString();
                QbSyncLogDetail.Insert(detail);
                throw ex;
            }
        }

        public void AddJob(QbSyncRequest syncRequest, ref QbCustomer qbProject)
        {
            string qbXmlRequest = null, qbXmlResponse = null;
            try
            {
                Host.QuickBooksDebug("QbSync:AddJob", "Adding job" + qbProject.ID);

                QbCustomer qbCustomer = QbCustomer.FindParent(qbProject.CustomerId, null);
              
                if (!qbCustomer.IsActive || string.IsNullOrEmpty(qbCustomer.ListId))
                    throw new QbSyncException("Failed to add a job. Customer is not active or not synchronized");

                if (qbProject.ProjectInsurance != null)
                    ProjectInsurance.Update(qbProject.ProjectInsurance);

                m_qbGateway.CreateQbCustomer(ref qbProject, qbCustomer, out qbXmlRequest, out qbXmlResponse);

                QbCustomer.Update(qbProject);

                List<QbInvoice> invoices = QbInvoice.FindByProjectId(qbProject.ProjectId.Value, null);
                foreach (QbInvoice invoice in invoices)
                {
                    invoice.QbSalesRepRefListId = qbProject.QbSalesRepListId;

                    invoice.BillingAddressAddr1 = qbProject.BillingAddressAddr1;
                    invoice.BillingAddressAddr2 = qbProject.BillingAddressAddr2;
                    invoice.BillingAddressCity = qbProject.BillingAddressCity;
                    invoice.BillingAddresState = qbProject.BillingAddressState;
                    invoice.BillingAddresPostalCode = qbProject.BillingAddressPostalCode;

                    invoice.ShipAddressAddr1 = qbProject.ShippingAddressAddr1;
                    invoice.ShipAddressAddr2 = qbProject.ShippingAddressAddr2;
                    invoice.ShipAddressCity = qbProject.ShippingAddressCity;
                    invoice.ShipAddressState = qbProject.ShippingAddressState;
                    invoice.ShipAddressPostalCode = qbProject.ShippingAddressPostalCode;

                    QbInvoice.Update(invoice);
                }

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = true;
                detail.QbCustomerId = qbProject.ID;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = qbXmlRequest;
                detail.QbXmlResponse = qbXmlResponse;
                QbSyncLogDetail.Insert(detail);
            }
            catch (QbException ex)
            {
                Host.Trace("QbSync:AddJob", ex.ToString());

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = false;
                detail.QbCustomerId = syncRequest.QbCustomerId;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = qbXmlRequest;
                detail.QbXmlResponse = qbXmlResponse;
                detail.ErrorMessage = ex.ToString();
                QbSyncLogDetail.Insert(detail);
                throw ex;
            }
            catch (Exception ex)
            {
                Host.Trace("QbSync:AddJob", ex.ToString());

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = false;
                detail.QbCustomerId = syncRequest.QbCustomerId;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = qbXmlRequest;
                detail.QbXmlResponse = qbXmlResponse;
                detail.ErrorMessage = ex.ToString();
                QbSyncLogDetail.Insert(detail);
                throw ex;   
            }
        }

        public void AddInvoice(QbSyncRequest syncRequest, QbInvoice qbInvoice)
        {
            string qbXmlRequest = null, qbXmlResponse = null;
            try
            {
                qbInvoice.QbCustomer = QbCustomer.FindByPrimaryKey(qbInvoice.QbCustomerId);

                Host.QuickBooksDebug("AddInvoice", qbInvoice.ID.ToString());

                List<QbInvoiceLine> lines = QbInvoiceLine.FindByInvoiceId(qbInvoice.ID, null);
                foreach (QbInvoiceLine line in lines)
                {
                    qbInvoice.QbInvoiceLines.Add(line);
                }

                if (!qbInvoice.IsPending || !string.IsNullOrEmpty(qbInvoice.TxnID)
                    || qbInvoice.QbInvoiceLines.Count == 0)
                    throw new QbSyncException("Failed to add invoice. Invoice is incorrect" + "Invoice #" + qbInvoice.RefNumber);

                QbCustomer qbProject = QbCustomer.FindByPrimaryKey(qbInvoice.QbCustomerId, null);

                if (qbInvoice.IsVoid)
                {
                    Host.QuickBooksDebug("AddInvoice", "Invoice voided");

                    QbSyncLogDetail detail1 = new QbSyncLogDetail();
                    detail1.CompletedDate = DateTime.Now;
                    detail1.IsSuccess = true;
                    detail1.QbCustomerId = qbProject.ID;
                    detail1.QbSyncActionId = syncRequest.QbSyncActionId;
                    detail1.QbSyncLogId = m_qbSyncLog.ID;
                    detail1.QbXmlRequest = "Invoice Voided already";
                    detail1.QbXmlResponse = string.Empty;
                    detail1.QbInvoiceId = qbInvoice.ID;
                    QbSyncLogDetail.Insert(detail1);

                    throw new QbSyncException("Invoice voided Invoice #" + qbInvoice.RefNumber + " Project " + qbProject.FullName);
                }

                if (!qbProject.IsActive || string.IsNullOrEmpty(qbProject.ListId))
                {
                    throw new QbSyncException("Failed to add invoice. QbProject incorrect");
                }

                QbCustomer qbCustomer = QbCustomer.FindParent(qbProject.CustomerId, null);
                if (!qbCustomer.IsActive || string.IsNullOrEmpty(qbCustomer.ListId))
                {
                    throw new QbSyncException("Failed to add invoice. QbCustomer incorrect");
                }

                m_qbGateway.CreateQbInvoice(qbInvoice, qbProject, out qbXmlRequest, out qbXmlResponse);
                
                foreach (QbInvoiceLine line in qbInvoice.QbInvoiceLines)
                {
                    QbInvoiceLine.Update(line);
                }
                qbInvoice.IsPending = false;
                QbInvoice.Update(qbInvoice);

                ReportStatus("AddInvoice", "OK", qbInvoice.RefNumber + " Project " + qbProject.FullName);

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = true;
                detail.QbCustomerId = qbProject.ID;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = qbXmlRequest;
                detail.QbXmlResponse = qbXmlResponse;
                detail.QbInvoiceId = qbInvoice.ID;
                QbSyncLogDetail.Insert(detail);
            }
            catch (Exception ex)
            {
                Host.Trace("AddInvoice", ex.ToString());

                QbSyncLogDetail detail = new QbSyncLogDetail();
                detail.CompletedDate = DateTime.Now;
                detail.IsSuccess = false;
                detail.QbCustomerId = syncRequest.QbCustomerId;
                detail.QbSyncActionId = syncRequest.QbSyncActionId;
                detail.QbSyncLogId = m_qbSyncLog.ID;
                detail.QbXmlRequest = qbXmlRequest;
                detail.QbXmlResponse = qbXmlResponse;
                detail.ErrorMessage = ex.ToString();
                detail.QbInvoiceId = syncRequest.QbInvoiceId;
                QbSyncLogDetail.Insert(detail);
                throw ex;
            }
        }

        private void ReportStatus (string action, string status, string description)
        {
            if (m_reportStatus != null)
                m_reportStatus(action, status, description);
        }
    }
}
