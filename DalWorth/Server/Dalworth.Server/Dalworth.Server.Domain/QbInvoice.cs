using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class QbInvoice 
    {
        #region QbInvoiceLines

        private BindingList<QbInvoiceLine> m_qbInvoiceLines = new BindingList<QbInvoiceLine>();
        public BindingList<QbInvoiceLine> QbInvoiceLines
        {
            get { return m_qbInvoiceLines; }
            set { m_qbInvoiceLines = value; }
        }

        #endregion

        #region QbCustomer

        private QbCustomer m_qbCustomer;
        public QbCustomer QbCustomer
        {
            get { return m_qbCustomer; }
            set { m_qbCustomer = value; }
        }

        #endregion 

        #region QbProject 

        private QbCustomer m_qbProject;
        public QbCustomer QbProject
        {
            get { return m_qbProject; }
        }

        #endregion

        #region Constructor

        public QbInvoice()
        {

        }

        #endregion 

        #region IQbTransaction

        public string TransactionType
        {
            get { return "Invoice"; }
        }

        #endregion

        #region Find

        #region FindByPRojectId

        private const string SqlFindByProjectId =
            @"  select qbInvoice.*
                from qbInvoice
                join qbCustomer on qbCustomer.id = qbInvoice.qbCustomerId
                where qbCustomer.projectid = ?ProjectId";

        public static List<QbInvoice> FindByProjectId(int projectId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProjectId, connection))
            {
                List<QbInvoice> rv = new List<QbInvoice>();
                Database.PutParameter(dbCommand, "?ProjectId", projectId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {   
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }
                }

                return rv;
            }
        }

        #endregion 

        private const string SqlFindByTxnId = SqlSelectAll +
            @" where TxnId = ?TxnId";
        public static QbInvoice FindByTxnId(string txnId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTxnId, connection))
            {
                Database.PutParameter(dbCommand, "?TxnId", txnId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return Load(dataReader);
                    }
                }

                throw new DataNotFoundException("TxnId = " + txnId);
            }
        }

        public static List<QbInvoice> Find(QbCustomer qbCustomer, IDbConnection connection)
        {
            string sql = SqlSelectAll;

            if (qbCustomer.SubLevel == 0)
                sql += @" where qbcustomerid in (select id from qbcustomer where customerid = ?CustomerId )";
            else
                sql += @" where qbcustomerid = ?QbCustomerId";
            
            
            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                if (qbCustomer.SubLevel == 0)
                    Database.PutParameter(dbCommand, "?CustomerId", qbCustomer.CustomerId);
                else
                    Database.PutParameter(dbCommand, "?QbCustomerId", qbCustomer.ID);
                
                List<QbInvoice> rv = new List<QbInvoice>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }
                }

                return rv;
            }
        }

        #endregion 

        #region Create

        public static List<QbInvoice> Create(Visit visit, IDbConnection connection)
        {
            List<QbInvoice> result = new List<QbInvoice>();

            List<Project> projects = Project.FindByVisitId(visit.ID, connection);
            List<Task> visitTasks = Task.FindByVisit(visit, connection);
            Customer customer = Customer.FindByPrimaryKey(visit.CustomerId.Value, connection);

            foreach (Project project in projects)
            {
                List<Task> projectTasks = visitTasks.FindAll(delegate(Task t)
                    {
                        return t.ProjectId == project.ID;
                    });
               
                if (!IsInvoiceRequired(project, projectTasks))
                    continue;

                QbInvoice invoice = CreateInvoice(customer, project, projectTasks, connection);

                if (invoice != null)
                    result.Add(invoice);
            }

            return result;
        }

        private static QbInvoice CreateInvoice(Customer customer, Project project, List<Task> projectTasks, IDbConnection connection)
        {
            QbInvoice invoice = new QbInvoice();
            QbCustomer qbCustomer;
            QbCustomer qbProject;
            ProjectType projectType = ProjectType.FindByPrimaryKey(project.ProjectTypeId, null);

            FindOrCreateQbCustomer(customer, project, out qbCustomer, out qbProject, connection);

            invoice.m_qbCustomer = qbCustomer;
            invoice.m_qbProject = qbProject;
            invoice.CreatedDate = DateTime.Now;
            invoice.TxnDate = GetTxnDate(project, projectTasks);
            invoice.RefNumber = GenerateRefNumber(qbProject);
            invoice.IsPending = true;
            invoice.QbCustomerId = qbProject.ID;
            invoice.IsVoid = false;
            invoice.QbSalesRepRefListId = qbProject.QbSalesRepListId;
            invoice.QbClassListId = projectType.QbClassListId;

            invoice.BillingAddressAddr1 = qbProject.BillingAddressAddr1;
            invoice.BillingAddressAddr2 = qbProject.BillingAddressAddr2;
            invoice.BillingAddressCity = qbProject.BillingAddressCity;
            invoice.BillingAddresState = qbProject.BillingAddressState;
            invoice.m_billingAddresPostalCode = qbProject.BillingAddressPostalCode;

            invoice.ShipAddressAddr1 = qbProject.ShippingAddressAddr1;
            invoice.ShipAddressAddr2 = qbProject.ShippingAddressAddr2;
            invoice.ShipAddressCity = qbProject.ShippingAddressCity;
            invoice.ShipAddressState = qbProject.ShippingAddressState;
            invoice.ShipAddressPostalCode = qbProject.ShippingAddressPostalCode;
            
            List<QbAccount> qbAccounts = QbAccount.FindByProjectType(project.ProjectType, true, connection);
            invoice.QbAccountListId = qbAccounts.Count > 0 ? qbAccounts[0].ListId : null;

            foreach (Task task in projectTasks)
            {
                List<QbInvoiceLine> existingLines = QbInvoiceLine.FindByProject(project, null);
                QbInvoiceLine existingLine = existingLines.Find(delegate(QbInvoiceLine invoiceline) 
                    { return invoiceline.TaskId == task.ID; });

                if (existingLine == null)
                    AddInvoiceLines(project.ProjectType, invoice, task);
            }

            if (invoice.QbInvoiceLines.Count > 0)
            {
                SetTaxItem(invoice, project.ProjectType);
                invoice.CalculateTotals();

                Insert(invoice, connection);
                foreach (QbInvoiceLine line in invoice.QbInvoiceLines)
                {
                    line.QbInvoiceId = invoice.ID;
                    QbInvoiceLine.Insert(line, connection);
                }

                QbSyncRequest.Insert(
                    new QbSyncRequest(-1, DateTime.Now, (int) QbSyncActionEnum.InvoiceAdd, qbProject.ID, invoice.ID),
                    connection);

                return invoice;
            }

            return null;
        }

        private static string GenerateRefNumber(QbCustomer qbProject)
        {
            string refNumber;

            List<QbInvoice> existingProjectInvoices = QbInvoice.FindByProjectId(qbProject.ProjectId.Value, null);

            if (existingProjectInvoices.Count == 0)
                refNumber = qbProject.ProjectId.ToString();
            else
            {
                for (int i = 1; true; i++)
                {
                    refNumber = qbProject.ProjectId.ToString() + "-" + i;
                    QbInvoice tempInvoice = existingProjectInvoices.Find(
                        delegate(QbInvoice temp)
                        {
                            return temp.RefNumber == refNumber;
                        }
                        );

                    if (tempInvoice == null)
                        break;
                }
            }

            return refNumber;
        }

        private static DateTime? GetTxnDate(Project project, List<Task> projectTasks)
        {
            if (project.ProjectType == ProjectTypeEnum.Miscellaneous)
            {
                return projectTasks[0].FindInvoiceDate(null);
            }

            if (project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                Task task = projectTasks.Find(delegate(Task t)
                                           {
                                               return t.TaskType == TaskTypeEnum.RugDelivery && t.TaskFailTypeId == null;
                                           });
                return task.FindInvoiceDate(null);
            }

            if(project.ProjectType == ProjectTypeEnum.Deflood)
            {

                Task monitoringTask = projectTasks.Find(delegate(Task t)
                                           {
                                               return t.TaskType == TaskTypeEnum.Monitoring && t.TaskFailTypeId == null;
                                           });

                if (monitoringTask.ClosedAmount > 0)
                    return monitoringTask.FindInvoiceDate(null);

                Task defloodTask = projectTasks.Find(delegate(Task t)
                                           {
                                               return t.TaskType == TaskTypeEnum.Deflood && t.TaskFailTypeId == null;
                                           });
                return defloodTask.FindInvoiceDate(null);
            }

            throw new InvalidOperationException("Invoice Txn Date not available");
        }

        private static Boolean IsInvoiceRequired(Project project, List<Task> tasks)
        {
            
            if (project.ProjectType == ProjectTypeEnum.Deflood)
            {
                Task helpTask = tasks.Find(delegate(Task t)
                {
                    return t.ProjectId == project.ID
                        && t.TaskType == TaskTypeEnum.Help;
                });

                Task defloodTask = tasks.Find(delegate(Task t)
                {
                    return t.ProjectId == project.ID
                        && t.TaskType == TaskTypeEnum.Deflood;
                });

                Task monitoringTask = tasks.Find(delegate(Task t)
                {
                    return t.ProjectId == project.ID
                        && t.TaskType == TaskTypeEnum.Monitoring;
                });

                Task rugPickupTask = tasks.Find(delegate(Task t)
                {
                    return t.ProjectId == project.ID
                        && t.TaskType == TaskTypeEnum.RugPickup;
                });

                if (helpTask != null || defloodTask == null || monitoringTask == null)
                    return false;

                if (defloodTask.TaskFailTypeId != null)
                    return false;

                /*if ((defloodTask.ClosedAmount == 0 && monitoringTask.ClosedAmount == 0)
                    && (rugPickupTask == null || rugPickupTask.TaskFailTypeId != null) )
                    return false;*/

                if( monitoringTask.ClosedAmount == 0
                    && !Task.IsFirstMonitoring(monitoringTask)
                    &&(rugPickupTask == null || rugPickupTask.TaskFailTypeId != null))
                    return false;
            }
            else if (project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                Task rugDeliveryTask = tasks.Find(delegate(Task t)
                {
                    return t.ProjectId == project.ID
                        && t.TaskType == TaskTypeEnum.RugDelivery;
                });

                if (rugDeliveryTask == null || rugDeliveryTask.TaskFailTypeId != null)
                    return false;
            }
            else if (project.ProjectType == ProjectTypeEnum.Miscellaneous)
            {
                Task task = tasks.Find(delegate(Task t) { return t.ClosedAmount > 0; });
                if (task == null)
                    return false;
            }
            else
            {
                return false;
            }

            return true; 
        }

        private static void FindOrCreateQbCustomer(Customer customer, Project project, 
            out QbCustomer qbCustomer, out QbCustomer qbProject, IDbConnection connection)
        {
            Address address = Address.FindByPrimaryKey(customer.AddressId.Value);
            qbProject = null;

            try
            {
                qbCustomer = QbCustomer.FindParent(customer.ID, null);
            }
            catch (DataNotFoundException ex)
            {
                qbCustomer = new QbCustomer();
            }

            if (qbCustomer.ID != -1)
            {
                try
                {
                    qbProject = QbCustomer.FindByProjectId(project.ID, null);
                }
                catch (DataNotFoundException ex)
                {}
            }

            if (qbProject == null)
            {
                qbProject = new QbCustomer();
            }

            qbCustomer.Fill(customer, address);
            qbProject.Fill(customer, address, project);

            if (string.IsNullOrEmpty(qbProject.QbSalesRepListId) && !string.IsNullOrEmpty(qbCustomer.QbSalesRepListId))
                qbProject.QbSalesRepListId = qbCustomer.QbSalesRepListId;

            if (string.IsNullOrEmpty(qbProject.QbCustomerTypeListId) && !string.IsNullOrEmpty(qbCustomer.QbCustomerTypeListId))
                qbProject.QbCustomerTypeListId = qbCustomer.QbCustomerTypeListId;

            if (qbCustomer.ID == -1)
            {
                QbCustomer.Insert(qbCustomer, connection);
                QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int)QbSyncActionEnum.CustomerAdd, qbCustomer.ID, null));
            }
            else
            {
                QbCustomer.Update(qbCustomer, connection);
                QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int)QbSyncActionEnum.CustomerMod, qbCustomer.ID, null));
            }

            if (qbProject.ID == -1)
            {
                QbCustomer.Insert(qbProject, connection);
                QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int)QbSyncActionEnum.JobAdd, qbProject.ID, null));
            }
            else
            {
                QbCustomer.Update(qbProject, connection);
                QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int)QbSyncActionEnum.JobMod, qbProject.ID, null));
            }
        }

        private static void SetTaxItem(QbInvoice invoice, ProjectTypeEnum projectType)
        {     
            switch (projectType)
            {
                case ProjectTypeEnum.RugCleaning:
                    invoice.ItemSalesTaxRef = QbItemRugCleaningCatalog.QbItemSalesTax.ListId;
                    break;
                case ProjectTypeEnum.Deflood:
                    invoice.ItemSalesTaxRef = QbItemDefloodCatalog.QbItemSalesTax.ListId;
                    break;
            }
        }

        private static void AddInvoiceLines(ProjectTypeEnum projectType, QbInvoice invoice, Task task)
        {
            if (projectType == ProjectTypeEnum.RugCleaning)
            {
                if (task.TaskType == TaskTypeEnum.RugDelivery)
                    AddRugCleaningInvoiceLines(projectType, invoice, task);
            }

            if (projectType == ProjectTypeEnum.Deflood)
            {
                if ((task.TaskType == TaskTypeEnum.Deflood || 
                    (task.TaskType == TaskTypeEnum.Monitoring && task.ClosedAmount > 0))
                    )
                {   
                    AddDefloodInvoiceLines(invoice, task);
                }
                if (task.TaskType == TaskTypeEnum.RugPickup)
                    AddRugCleaningInvoiceLines(projectType, invoice, task);
            }

            if(projectType == ProjectTypeEnum.Miscellaneous)
            {
                AddMiscInvoiceLines(invoice, task);
            }
        }

        private static void AddMiscInvoiceLines(QbInvoice invoice, Task miscTask)
        {
            if (miscTask.IsRugCleaningDepartment)
            {
                QbInvoiceLine invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemRugCleaning.ListId, -1, null,
                miscTask.Description, 1, null,
                0, 0, null, miscTask.ClosedAmount,
                QbItemRugCleaningCatalog.QbItemRugCleaning.SalesTaxCodeRefListId, miscTask.ID, null);
                invoice.QbInvoiceLines.Add(invoiceLine);
            }
            else
            {
                AddDefloodInvoiceLines(invoice, miscTask);
            }
        }

        private static void AddDefloodInvoiceLines(QbInvoice invoice, Task defloodTask)
        {
            QbInvoiceLine invoiceLine = new QbInvoiceLine(
                -1, 
                QbItemDefloodCatalog.QbItemDefloodRevenue.ListId, 
                invoice.ID,null,"", 1,  null, defloodTask.ClosedAmount,0,
                null, defloodTask.ClosedAmount, 
                QbItemDefloodCatalog.QbItemDefloodRevenue.SalesTaxCodeRefListId, 
                defloodTask.ID, null);
            invoice.QbInvoiceLines.Add(invoiceLine);
        }

        private static void AddRugCleaningInvoiceLines(ProjectTypeEnum projectType, QbInvoice invoice, Task rugTask)
        {
            List<Item> items = Item.FindByTask(rugTask);

            decimal totalCleaningCost = 0;

            foreach (Item item in items)
            {
                ItemRecalcWrapper wrapper = new ItemRecalcWrapper(item);

                QbInvoiceLine invoiceLine;
                if (projectType == ProjectTypeEnum.Deflood)
                {
                    invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemRugCleaningFlood.ListId, -1, null,
                    QbItemRugCleaningCatalog.QbItemRugCleaningFlood.Description + " " + wrapper.Description, wrapper.SquareFootage, null,
                    QbItemRugCleaningCatalog.QbItemRugCleaningFlood.Price, 0, null, wrapper.CleanCost,
                    QbItemRugCleaningCatalog.QbItemRugCleaningFlood.SalesTaxCodeRefListId, rugTask.ID, item.ID);
                }
                else
                {
                    invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemRugCleaning.ListId, -1, null,
                    QbItemRugCleaningCatalog.QbItemRugCleaning.Description + " " + wrapper.Description, wrapper.SquareFootage, null,
                    QbItemRugCleaningCatalog.QbItemRugCleaning.Price, 0, null, wrapper.CleanCost,
                    QbItemRugCleaningCatalog.QbItemRugCleaning.SalesTaxCodeRefListId, rugTask.ID, item.ID);
                }

                invoice.QbInvoiceLines.Add(invoiceLine);

                totalCleaningCost += wrapper.CleanCost;

                if (item.IsMothRepelApplied)
                {
                    invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemMoth.ListId, -1, null,
                    QbItemRugCleaningCatalog.QbItemMoth.Description + " " + wrapper.Description, wrapper.SquareFootage, null,
                    QbItemRugCleaningCatalog.QbItemMoth.Price,0, null, wrapper.MothRepelCost,
                    QbItemRugCleaningCatalog.QbItemMoth.SalesTaxCodeRefListId, rugTask.ID, item.ID);
                    invoice.QbInvoiceLines.Add(invoiceLine);
                }

                if (item.IsPaddingApplied)
                {
                    invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemRugCleaningPad.ListId, -1, null,
                    QbItemRugCleaningCatalog.QbItemRugCleaningPad.Description + " " + wrapper.Description, wrapper.SquareFootage, null,
                    QbItemRugCleaningCatalog.QbItemRugCleaningPad.Price,0, null, wrapper.PaddingCost,
                    QbItemRugCleaningCatalog.QbItemRugCleaningPad.SalesTaxCodeRefListId, rugTask.ID, item.ID);
                    invoice.QbInvoiceLines.Add(invoiceLine);
                }

                if (item.IsProtectorApplied)
                {
                    invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemProtectant.ListId, -1, null,
                    QbItemRugCleaningCatalog.QbItemProtectant.Description + " " + wrapper.Description, wrapper.SquareFootage, null,
                    QbItemRugCleaningCatalog.QbItemProtectant.Price, 0, null, wrapper.ProtectorCost,
                    QbItemRugCleaningCatalog.QbItemProtectant.SalesTaxCodeRefListId, rugTask.ID, item.ID);
                    invoice.QbInvoiceLines.Add(invoiceLine);
                }

                if (item.IsRapApplied)
                {
                    invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemWrap.ListId, -1, null,
                    QbItemRugCleaningCatalog.QbItemWrap.Description + " " + wrapper.Description, 1, null,
                    QbItemRugCleaningCatalog.QbItemWrap.Price, 0, null, wrapper.RapCost,
                    QbItemRugCleaningCatalog.QbItemWrap.SalesTaxCodeRefListId, rugTask.ID, item.ID);
                    invoice.QbInvoiceLines.Add(invoiceLine);
                }
            }

            if (totalCleaningCost > QbItemRugCleaningCatalog.MinimumCharge)
            {
                if (rugTask.DiscountPercentage > 0)
                {
                    decimal discount = (-1 * totalCleaningCost * rugTask.DiscountPercentage) / 100;

                    QbInvoiceLine invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemDiscount.ListId, -1, null,
                        "Discount " + rugTask.DiscountPercentage.ToString() + "%", 0,
                        null, discount, 0, null, discount, QbItemRugCleaningCatalog.QbItemDiscount.SalesTaxCodeRefListId, rugTask.ID, null);
                    invoice.QbInvoiceLines.Add(invoiceLine);
                }
            }
            else
            {
                decimal minimumChargeAmount = QbItemRugCleaningCatalog.MinimumCharge - totalCleaningCost;

                QbInvoiceLine invoiceLine = new QbInvoiceLine(-1, QbItemRugCleaningCatalog.QbItemMinimumCharge.ListId, -1, null, 
                    "Minimum charge", 1, null, minimumChargeAmount,0, null, minimumChargeAmount,
                    QbItemRugCleaningCatalog.QbItemMinimumCharge.SalesTaxCodeRefListId, rugTask.ID, null);
                invoice.QbInvoiceLines.Add(invoiceLine);
            }
        }

        #endregion 

        public void CalculateTotals()
        {
            QbItem salesTaxItem = null;

            if (ItemSalesTaxRef != null)
            {
                salesTaxItem = QbItem.FindByPrimaryKey(ItemSalesTaxRef);
            }

            List<QbSalesTaxCode> salesTaxCodes = QbSalesTaxCode.Find();

            TaxAmount = 0;
            TotalAmount = 0;
            SubTotalAmount = 0;

            foreach (QbInvoiceLine line in QbInvoiceLines)
            {
                if (line.QbSalesTaxCodeListId != null && salesTaxItem != null)
                {
                    QbSalesTaxCode taxCode = salesTaxCodes.Find(delegate(QbSalesTaxCode taxCode1)
                        { return taxCode1.ListId == line.QbSalesTaxCodeListId; });

                    if (taxCode != null && taxCode.IsTaxable)
                        TaxAmount += (salesTaxItem.TaxRate * line.Amount) / 100;
                }

                SubTotalAmount += line.Amount;
            }

            TotalAmount += SubTotalAmount + TaxAmount;
        }

        #region DeleteDeep

        public static void DeleteDeep(int qbInvoiceId, IDbConnection connection)
        {
            List<QbInvoiceLine> lines = QbInvoiceLine.FindByInvoiceId(qbInvoiceId, connection);
            foreach (QbInvoiceLine line in lines)
            {
                QbInvoiceLine.Delete(line, connection);
            }

            QbInvoice invoice = new QbInvoice(qbInvoiceId);
            QbInvoice.Delete(invoice, connection);
        }

        #endregion 

        public void UpdateQbFields(QbInvoice qbInvoice)
        {
            TxnID = qbInvoice.TxnID;
            TimeCreatedInQb = qbInvoice.TimeCreatedInQb;
            TimeModifiedInQb = qbInvoice.TimeModifiedInQb;
            EditSequence = qbInvoice.EditSequence;
            TxnNumber = qbInvoice.TxnNumber;
            QbClassListId = qbInvoice.QbClassListId;
            QbAccountListId = qbInvoice.QbAccountListId;
            QbTemplateListId = qbInvoice.QbTemplateListId;
            TxnDate = qbInvoice.TxnDate;
            RefNumber = qbInvoice.RefNumber;
            BillingAddressAddr1 = qbInvoice.BillingAddressAddr1;
            BillingAddressAddr2 = qbInvoice.BillingAddressAddr2;
            BillingAddressAddr3 = qbInvoice.BillingAddressAddr3;
            BillingAddressAddr4 = qbInvoice.BillingAddressAddr4;
            BillingAddressAddr5 = qbInvoice.BillingAddressAddr5;
            BillingAddressCity = qbInvoice.BillingAddressCity;
            BillingAddressCountry = qbInvoice.BillingAddressCountry;
            BillingAddresPostalCode = qbInvoice.BillingAddresPostalCode;
            BillingAddressNote = qbInvoice.BillingAddressNote;
            BillingAddresState = qbInvoice.BillingAddresState;

            ShipAddressAddr1 = qbInvoice.ShipAddressAddr1;
            ShipAddressAddr2 = qbInvoice.ShipAddressAddr2;
            ShipAddressAddr3 = qbInvoice.ShipAddressAddr3;
            ShipAddressAddr4 = qbInvoice.ShipAddressAddr4;
            ShipAddressAddr5 = qbInvoice.ShipAddressAddr5;
            ShipAddressCity = qbInvoice.ShipAddressCity;
            ShipAddressCountry = qbInvoice.ShipAddressCountry;
            ShipAddressNote = qbInvoice.ShipAddressNote;
            ShipAddressPostalCode = qbInvoice.ShipAddressPostalCode;
            ShipAddressState = qbInvoice.ShipAddressState;

            QbInvoiceTermListId = qbInvoice.QbInvoiceTermListId;
            QbSalesRepRefListId = qbInvoice.QbSalesRepRefListId;
            ItemSalesTaxRef = qbInvoice.ItemSalesTaxRef;
            TotalAmount = qbInvoice.TotalAmount;
            TaxAmount = qbInvoice.TaxAmount;
            SubTotalAmount = qbInvoice.SubTotalAmount;
            IsPending = qbInvoice.IsPending;
        }
    }

    public enum QbTransactionTypeEnum
    {
        Invoice,
        Payment,
        CreditMemo
    }

    public class QbTransaction
    {
        private readonly QbInvoice m_qbInvoice;
        private readonly QbPayment m_qbPayment;
        private readonly QbCreditMemo m_qbCreditMemo;
        private readonly QbAccount m_qbAccount;

        #region QbTransaction

        public QbTransaction(QbInvoice qbInvoice, QbAccount qbAccount)
        {
            m_qbInvoice = qbInvoice;
            m_qbAccount = qbAccount;
        }

        public QbTransaction(QbPayment qbPayment, QbAccount qbAccount)
        {
            m_qbPayment = qbPayment;
            m_qbAccount = qbAccount;
        }

        public QbTransaction(QbCreditMemo qbCreditMemo, QbAccount qbAccount)
        {
            m_qbCreditMemo = qbCreditMemo;
            m_qbAccount = qbAccount;
        }

        #endregion

        #region QbInvoice

        public QbInvoice QbInvoice
        {
            get { return m_qbInvoice; }
        }

        #endregion

        #region QbPayment

        public QbPayment QbPayment
        {
            get { return m_qbPayment; }
        }

        #endregion

        #region QbCreditMemo

        public QbCreditMemo QbCreditMemo
        {
            get { return m_qbCreditMemo; }
        }

        #endregion


        #region Type

        public QbTransactionTypeEnum Type
        {
            get
            {
                if (m_qbInvoice != null)
                    return QbTransactionTypeEnum.Invoice;
                if (m_qbPayment != null)
                    return QbTransactionTypeEnum.Payment;
                if (m_qbCreditMemo != null)
                    return QbTransactionTypeEnum.CreditMemo;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        public string TypeText 
        {
            get
            {
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return "Credit Memo";
                return Type.ToString();
            }
        }

        #endregion

        #region Number

        public string Number
        {
            get
            {
                if (Type == QbTransactionTypeEnum.Invoice)
                    return m_qbInvoice.RefNumber;
                if (Type == QbTransactionTypeEnum.Payment)
                    return m_qbPayment.RefNumber;
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return m_qbCreditMemo.RefNumber;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        #endregion

        #region CreatedDate

        public DateTime? CreatedDate
        {
            get
            {
                if (Type == QbTransactionTypeEnum.Invoice)
                    return m_qbInvoice.CreatedDate;
                if (Type == QbTransactionTypeEnum.Payment)
                    return m_qbPayment.TimeCreatedInQb;
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return m_qbCreditMemo.TimeCreatedInQb;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        #endregion

        #region TxnDate

        public DateTime? TxnDate
        {
            get
            {
                if (Type == QbTransactionTypeEnum.Invoice)
                    return m_qbInvoice.TxnDate;
                if (Type == QbTransactionTypeEnum.Payment)
                    return m_qbPayment.TxnDate;
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return m_qbCreditMemo.TxnDate;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        #endregion

        #region AccountName

        public string AccountName
        {
            get { return m_qbAccount.FullName; }
        }

        #endregion

        #region TotalAmount

        public decimal TotalAmount
        {
            get
            {
                if (Type == QbTransactionTypeEnum.Invoice)
                    return m_qbInvoice.TotalAmount;
                if (Type == QbTransactionTypeEnum.Payment)
                    return m_qbPayment.TotalAmount;
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return m_qbCreditMemo.TotalAmount;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        #endregion        

        #region IsPending

        public bool IsPending
        {
            get
            {
                if (Type == QbTransactionTypeEnum.Invoice)
                    return m_qbInvoice.IsPending;
                if (Type == QbTransactionTypeEnum.Payment)
                    return false;
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return m_qbCreditMemo.IsPending;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        #endregion        

        #region Notes

        public string Notes
        {
            get
            {
                if (Type == QbTransactionTypeEnum.Invoice)
                    return m_qbInvoice.Memo;
                if (Type == QbTransactionTypeEnum.Payment)
                    return m_qbPayment.Memo;
                if (Type == QbTransactionTypeEnum.CreditMemo)
                    return m_qbCreditMemo.Memo;
                throw new DalworthException("Unknown QbTransaction type");
            }
        }

        #endregion

        #region Balance

        private decimal m_balance;
        public decimal Balance
        {
            get { return m_balance; }
            set { m_balance = value; }
        }

        #endregion

        #region FindBy

        private const string SqlInvoiceFindBy =
            @"select i.*, a.*
                from qbInvoice i
                inner join QbAccount a on a.Listid = i.QbAccountListId
                inner join QbCustomer qc on qc.Id = i.QbCustomerId
                    where i.isVoid=false and qc.{0}";

        private const string SqlPaymentFindBy =
            @"select p.*, a.*
                from qbPayment p
                inner join QbAccount a on a.Listid = p.QbAccountListId
                inner join QbCustomer qc on qc.Id = p.QbCustomerId
                    where qc.{0}";

        private const string SqlCreditMemoFindBy =
            @"select m.*, a.*
                from qbcreditmemo m
                inner join QbAccount a on a.Listid = m.QbAccountListId
                inner join QbCustomer qc on qc.Id = m.QbCustomerId
                    where qc.{0}";

        public static List<QbTransaction> FindBy(CustomerProjectWrapper wrapper)
        {
            List<QbTransaction> result = new List<QbTransaction>();

            string sqlQueryIvoice;
            if (wrapper.IsCustomer)
                sqlQueryIvoice = string.Format(SqlInvoiceFindBy, "CustomerId = " + wrapper.Customer.ID);
            else
                sqlQueryIvoice = string.Format(SqlInvoiceFindBy, "ProjectId = " + wrapper.ProjectId);

            string sqlQueryPayment;
            if (wrapper.IsCustomer)
                sqlQueryPayment = string.Format(SqlPaymentFindBy, "CustomerId = " + wrapper.Customer.ID);
            else
                sqlQueryPayment = string.Format(SqlPaymentFindBy, "ProjectId = " + wrapper.ProjectId);

            string sqlQueryCreditMemo;
            if (wrapper.IsCustomer)
                sqlQueryCreditMemo = string.Format(SqlCreditMemoFindBy, "CustomerId = " + wrapper.Customer.ID);
            else
                sqlQueryCreditMemo = string.Format(SqlCreditMemoFindBy, "ProjectId = " + wrapper.ProjectId);


            using (IDbCommand dbCommand = Database.PrepareCommand(sqlQueryIvoice))
            {                
                using(IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        result.Add(new QbTransaction(QbInvoice.Load(dataReader),
                            QbAccount.Load(dataReader, QbInvoice.FieldsCount)));
                    }
                }
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(sqlQueryPayment))
            {                
                using(IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        result.Add(new QbTransaction(QbPayment.Load(dataReader),
                            QbAccount.Load(dataReader, QbPayment.FieldsCount)));
                    }
                }
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(sqlQueryCreditMemo))
            {                
                using(IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        result.Add(new QbTransaction(QbCreditMemo.Load(dataReader),
                            QbAccount.Load(dataReader, QbCreditMemo.FieldsCount)));
                    }
                }
            }
            
            result.Sort(delegate(QbTransaction transaction1, QbTransaction transaction2)
                            {
                                if (transaction1.CreatedDate.HasValue && transaction2.CreatedDate.HasValue)
                                    return -transaction1.CreatedDate.Value.CompareTo(transaction2.CreatedDate.Value);

                                if (!transaction1.CreatedDate.HasValue && !transaction2.CreatedDate.HasValue)
                                    return 0;

                                if (!transaction1.CreatedDate.HasValue && transaction2.CreatedDate.HasValue)
                                    return 1;
                                return -1;
                            });

            return result;
        }

        #endregion         
    }
}
      