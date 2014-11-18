using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using TractInc.Expense.Entity;

namespace TractInc.Expense
{

    public class UserService
    {
        #region ConfigurationSettings

        private const string SMTP_SERVER_KEY = "smtp";
        private const string SMTP_PORT_KEY = "port";
        private const string SMTP_USER_KEY = "username";
        private const string SMTP_PASSWORD_KEY = "password";
        private const string EMAIL_FROM_KEY = "from";
        private const string EMAIL_SUBJECT_KEY = "subject";
        private const int DEFAULT_SMTP_PORT = 25;
        private const string DEFAULT_EMAIL_FROM = "admin@truetract.com";
        private const string DEFAULT_EMAIL_SUBJECT = "Password recovery";
        private const string EMAIL_BODY_TEMPLATE = @"
==============================

login: {0}
password: {1}

==============================
";
        
        #endregion

        #region Methods

        public UserDataObject Login(string login, string password)
        {
            UserDataObject userInfo;
            
            using (SqlConnection conn = SqlHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();

                userInfo = TractInc.Expense.Data.User.GetInstance().GetUserByLogin(tran, login);

                if (null != userInfo)
                {
                    if (userInfo.Password != password)
                    {
                        userInfo.HackingAttempts++;
                        TractInc.Expense.Data.User.GetInstance().Update(tran, userInfo);

                        throw new Exception("Invalid password");
                    }

                    if (!userInfo.IsActive)
                    {
                        throw new Exception("User is inactive.");
                    }

                } else {
                    throw new Exception("User not found");
                }

                if (userInfo.HackingAttempts > 0)
                {
                    userInfo.HackingAttempts = 0;
                    TractInc.Expense.Data.User.GetInstance().Update(tran, userInfo);
                }
                
                tran.Commit();
            }

            return userInfo;
        }
        
        public bool RestorePassword(String login)
        {

            UserDataObject userInfo;

            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                userInfo = TractInc.Expense.Data.User.GetInstance().GetUserByLogin(tran, login);
                tran.Commit();
            }

            if (null == userInfo)
            {
                return false;
            }

            SmtpClient smtpClient = new SmtpClient();

            string host = ConfigurationManager.AppSettings[SMTP_SERVER_KEY];
            if (null == host || host.Length == 0)
            {
                throw new ConfigurationErrorsException("SMTP hostname not found");
            }
            smtpClient.Host = host;

            string port_str = ConfigurationManager.AppSettings[SMTP_PORT_KEY];
            if (null == port_str || port_str.Length == 0)
            {
                smtpClient.Port = DEFAULT_SMTP_PORT;
            }
            else
            {
                smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings[SMTP_PORT_KEY]);
            }

            string username = ConfigurationManager.AppSettings[SMTP_USER_KEY];
            string password = ConfigurationManager.AppSettings[SMTP_PASSWORD_KEY];
            if (username != null && password != null)
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
            }

            MailAddress to = new MailAddress(userInfo.Email);
            MailAddress from;

            if (ConfigurationManager.AppSettings[EMAIL_FROM_KEY] == null)
            {
                from = new MailAddress(DEFAULT_EMAIL_FROM);
            }
            else
            {
                from = new MailAddress(ConfigurationManager.AppSettings[EMAIL_FROM_KEY]);
            }

            MailMessage message = new MailMessage(from, to);

            string subject = ConfigurationManager.AppSettings[EMAIL_SUBJECT_KEY];
            if (subject != null && subject != String.Empty)
            {
                message.Subject = subject;
            }
            else
            {
                message.Subject = DEFAULT_EMAIL_SUBJECT;
            }

            message.IsBodyHtml = false;

            message.Body = String.Format(EMAIL_BODY_TEMPLATE, userInfo.Login, userInfo.Password);

            smtpClient.Send(message);

            return true;
        }

        public BillDataObject GetBill(int billId)
        {   
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    BillDataObject result = TractInc.Expense.Data.Bill.GetInstance().GetBill(tran, billId, false);

                    tran.Commit();

                    return result;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

        public List<BillDataObject> GetLoadedBills(List<BillDataObject> billsToLoad)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                foreach (BillDataObject billInfo in billsToLoad)
                {
                    billInfo.Assign(TractInc.Expense.Data.Bill.GetInstance().GetBill(tran, billInfo.BillId, false));
                }

                tran.Commit();

                return billsToLoad;
            }
        }

        public List<BillDataObject> GetBills(int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                List<BillDataObject> result = TractInc.Expense.Data.Bill.GetInstance().GetBills(tran, assetId);

                tran.Commit();

                return result;
            }
        }

        public BillDataObject StoreBillItems(BillDataObject bill, List<BillItemDataObject> billItems)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                BillDataObject result = TractInc.Expense.Data.Bill.GetInstance().StoreBillItems(tran, bill, billItems);

                tran.Commit();

                return result;
            }
        }

        public BillItemCompositionDataObject StoreComposition(BillItemCompositionDataObject compositionInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                BillItemCompositionDataObject result = TractInc.Expense.Data.BillItemComposition.GetInstance().StoreComposition(tran, compositionInfo);

                tran.Commit();

                return result;
            }
        }

        public List<BillItemCompositionDataObject> StoreCompositions(List<BillItemCompositionDataObject> compositions)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                List<BillItemCompositionDataObject> result = TractInc.Expense.Data.BillItemComposition.GetInstance().StoreCompositions(tran, compositions);

                tran.Commit();

                return result;
            }
        }

        public BillDataObject RemoveComposition(BillItemCompositionDataObject compositionInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.BillItemComposition.GetInstance().Remove(tran, compositionInfo.BillItemCompositionId);

                BillDataObject billInfo = TractInc.Expense.Data.Bill.GetInstance().UpdateBillStatus(tran, compositionInfo.BillId);

                tran.Commit();

                return billInfo;
            }
        }

        public BillDataObject UpdateBillStatus(int billId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                BillDataObject billInfo = TractInc.Expense.Data.Bill.GetInstance().UpdateBillStatus(tran, billId);

                tran.Commit();

                return billInfo;
            }
        }

        public void ApproveBills(IList<BillSubmitDataObject> bills)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                foreach (BillSubmitDataObject billSubmitInfo in bills)
                {
                    BillDataObject billInfo = TractInc.Expense.Data.Bill.GetInstance().GetBill(tran, billSubmitInfo.BillId, true);
                    billInfo.Status = billSubmitInfo.Status;

                    if (null != billSubmitInfo.Notes)
                    {
                        foreach (NoteDataObject noteInfo in billSubmitInfo.Notes)
                        {
                            if (0 == noteInfo.NoteId)
                            {
                                TractInc.Expense.Data.Note.GetInstance().Insert(tran, noteInfo);
                            }
                        }
                    }

                    if (null != billSubmitInfo.BillItems)
                    {
                        foreach (BillItemDataObject itemInfo in billSubmitInfo.BillItems)
                        {
                            TractInc.Expense.Data.BillItem.GetInstance().Update(tran, itemInfo);
                        }
                    }

                    TractInc.Expense.Data.Bill.GetInstance().Update(tran, billInfo);
                }

                tran.Commit();
            }
        }

        public void SendRejectionNotify(int billId, bool isManagerRejection)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                String body = "Bill item notes history:\n" + "-----------------------\n\n";

                BillDataObject billInfo = TractInc.Expense.Data.Bill.GetInstance().GetBill(tran, billId, false);

                List<AssetAssignmentDataObject> assignmentsList = TractInc.Expense.Data.AssetAssignment.GetInstance().GetAssignments(tran, billInfo.AssetId);
                Hashtable assignmentsHash = new Hashtable();
                foreach (AssetAssignmentDataObject assignmentInfo in assignmentsList)
                {
                    assignmentsHash.Add(assignmentInfo.AssetAssignmentId, assignmentInfo);
                }

                List<BillItemTypeDataObject> itemTypesList = TractInc.Expense.Data.BillItemType.GetInstance().GetBillItemTypes(tran);
                Hashtable itemTypesHash = new Hashtable();
                foreach (BillItemTypeDataObject itemTypeInfo in itemTypesList)
                {
                    itemTypesHash.Add(itemTypeInfo.BillItemTypeId, itemTypeInfo);
                }

                int assetId = billInfo.AssetId;
                if (isManagerRejection)
                {
                    AssetDataObject assetInfo = TractInc.Expense.Data.Asset.GetInstance().GetAsset(tran, billInfo.AssetId);
                    assetId = assetInfo.ChiefAssetId;
                }

                UserDataObject userInfo = TractInc.Expense.Data.User.GetInstance().GetUserByAsset(tran, assetId);

                foreach (BillItemDataObject itemInfo in billInfo.BillItems)
                {
                    if ((BillItemDataObject.BILL_ITEM_STATUS_REJECTED == itemInfo.Status)
                            || (BillItemDataObject.BILL_ITEM_STATUS_DECLINED == itemInfo.Status))
                    {
                        body += "Bill item type: " + ((BillItemTypeDataObject)itemTypesHash[itemInfo.BillItemTypeId]).Name
                            + ", AFE: " + ((AssetAssignmentDataObject)assignmentsHash[itemInfo.AssetAssignmentId]).AFE
                            + ", Project: " + ((AssetAssignmentDataObject)assignmentsHash[itemInfo.AssetAssignmentId]).SubAFE + "\n";

                        int counter = 1;
                        foreach (NoteDataObject noteInfo in itemInfo.Notes)
                        {
                            body += counter.ToString() + ". " + noteInfo.Posted.ToString() + " " + noteInfo.NoteText + "\n";
                            counter++;
                        }

                        body += "\n";
                    }
                }

                body += "\n";

                body += "Bill notes history:\n" + "------------------\n";
                body += "Bill start date: " + billInfo.StartDate + "\n\n";
                foreach (NoteDataObject noteInfo in billInfo.Notes)
                {
                    body += noteInfo.Posted.ToString() + " " + noteInfo.NoteText + "\n";
                }

                SendEmail(userInfo.Email, "Bill rejected", body);

                tran.Commit();
            }
        }

        public bool SendEmail(String email, String subject, String body)
        {
            SmtpClient smtpClient = new SmtpClient();

            string host = ConfigurationManager.AppSettings[SMTP_SERVER_KEY];
            if (null == host || host.Length == 0)
            {
                throw new ConfigurationErrorsException("SMTP hostname not found");
            }
            smtpClient.Host = host;

            string port_str = ConfigurationManager.AppSettings[SMTP_PORT_KEY];
            if (null == port_str || port_str.Length == 0)
            {
                smtpClient.Port = DEFAULT_SMTP_PORT;
            }
            else
            {
                smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings[SMTP_PORT_KEY]);
            }

            string username = ConfigurationManager.AppSettings[SMTP_USER_KEY];
            string password = ConfigurationManager.AppSettings[SMTP_PASSWORD_KEY];
            if (username != null && password != null)
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
            }

            MailAddress to = new MailAddress(email);
            MailAddress from;

            if (ConfigurationManager.AppSettings[EMAIL_FROM_KEY] == null)
            {
                from = new MailAddress(DEFAULT_EMAIL_FROM);
            }
            else
            {
                from = new MailAddress(ConfigurationManager.AppSettings[EMAIL_FROM_KEY]);
            }

            MailMessage message = new MailMessage(from, to);

            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;

            smtpClient.Send(message);

            return true;
        }

        public void SubmitBills(IList<BillSubmitDataObject> bills)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    foreach (BillSubmitDataObject billSubmitInfo in bills)
                    {
                        BillDataObject billInfo = TractInc.Expense.Data.Bill.GetInstance().GetBill(tran, billSubmitInfo.BillId, true);
                        billInfo.DailyBillAmt = 0;
                        billInfo.OtherBillAmt = 0;
                        billInfo.TotalBillAmt = 0;
                        billInfo.TotalDailyBill = 0;

                        if ((BillDataObject.BILL_STATUS_CHANGED == billInfo.Status)
                                || (BillDataObject.BILL_STATUS_REJECTED == billInfo.Status))
                        {
                            billInfo.Status = BillDataObject.BILL_STATUS_CORRECTED;
                        }
                        else if (BillDataObject.BILL_STATUS_NEW == billInfo.Status)
                        {
                            billInfo.Status = BillDataObject.BILL_STATUS_SUBMITTED;
                        }

                        foreach (BillItemDataObject itemInfo in billInfo.BillItems)
                        {
                            decimal amount = (decimal)(itemInfo.Qty * itemInfo.BillRate);

                            if (1 == itemInfo.BillItemTypeId)
                            {
                                billInfo.TotalDailyBill += (int)itemInfo.Qty;
                                billInfo.DailyBillAmt += amount;
                            }
                            else
                            {
                                billInfo.OtherBillAmt += amount;
                            }

                            billInfo.TotalBillAmt += amount;

                            if ((BillItemDataObject.BILL_ITEM_STATUS_CHANGED == itemInfo.Status)
                                    || (BillItemDataObject.BILL_ITEM_STATUS_REJECTED == itemInfo.Status))
                            {
                                itemInfo.Status = BillItemDataObject.BILL_ITEM_STATUS_CORRECTED;
                            }
                            else if (BillItemDataObject.BILL_ITEM_STATUS_NEW == itemInfo.Status)
                            {
                                itemInfo.Status = BillItemDataObject.BILL_ITEM_STATUS_SUBMITTED;
                            }

                            TractInc.Expense.Data.BillItem.GetInstance().Update(tran, itemInfo);
                        }

                        TractInc.Expense.Data.Bill.GetInstance().Update(tran, billInfo);

                        if (null != billSubmitInfo.Notes)
                        {
                            foreach (NoteDataObject noteInfo in billSubmitInfo.Notes)
                            {
                                if (0 == noteInfo.NoteId)
                                {
                                    TractInc.Expense.Data.Note.GetInstance().Insert(tran, noteInfo);
                                }
                            }
                        }

                        if (null != billSubmitInfo.Attachments)
                        {
                            foreach (BillItemAttachmentDataObject attachmentInfo in billSubmitInfo.Attachments)
                            {
                                if (0 == attachmentInfo.BillItemAttachmentId)
                                {
                                    if (!attachmentInfo.IsDeleted)
                                    {
                                        TractInc.Expense.Data.BillItemAttachment.GetInstance().Insert(tran, attachmentInfo);
                                    }
                                }
                                else if (attachmentInfo.IsDeleted)
                                {
                                    TractInc.Expense.Data.BillItemAttachment.GetInstance().Remove(tran, attachmentInfo.BillItemId);
                                }
                            }
                        }
                    }

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (SqlException)
                    {
                    }
                    throw ex;
                }
            }
        }

        public List<NoteDataObject> StoreNotes(List<NoteDataObject> notes)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                foreach (NoteDataObject noteInfo in notes)
                {
                    if (0 != noteInfo.NoteId)
                    {
                        continue;
                    }

                    TractInc.Expense.Data.Note.GetInstance().Insert(tran, noteInfo);
                }

                tran.Commit();

                return notes;
            }
        }

        public List<BillItemAttachmentDataObject> GetBillAttachments(int billId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                List<BillItemAttachmentDataObject> result = TractInc.Expense.Data.BillItemAttachment.GetInstance().GetBillAttachments(tran, billId);

                tran.Commit();

                return result;
            }
        }

        public DictionariesDataObject GetDictionaries()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                DictionariesDataObject dictionariesInfo = new DictionariesDataObject();

                dictionariesInfo.BillItemStatuses = TractInc.Expense.Data.BillItemStatus.GetInstance().GetBillItemStatuses(tran);
                dictionariesInfo.BillStatuses = TractInc.Expense.Data.BillStatus.GetInstance().GetBillStatuses(tran);
                dictionariesInfo.InvoiceItemStatuses = TractInc.Expense.Data.InvoiceItemStatus.GetInstance().GetInvoiceItemStatuses(tran);
                dictionariesInfo.InvoiceStatuses = TractInc.Expense.Data.InvoiceStatus.GetInstance().GetInvoiceStatuses(tran);
                dictionariesInfo.BillItemTypes = TractInc.Expense.Data.BillItemType.GetInstance().GetBillItemTypes(tran);
                dictionariesInfo.InvoiceItemTypes = TractInc.Expense.Data.InvoiceItemType.GetInstance().GetInvoiceItemTypes(tran);
                dictionariesInfo.AssetTypes = TractInc.Expense.Data.AssetType.GetInstance().GetAssetTypes(tran);
                dictionariesInfo.AFEStatuses = TractInc.Expense.Data.AFEStatus.GetInstance().GetAFEStatuses(tran);
                dictionariesInfo.ProjectStatuses = TractInc.Expense.Data.ProjectStatus.GetInstance().GetProjectStatuses(tran);

                tran.Commit();

                return dictionariesInfo;
            }
        }

        public LandmanDataObject GetLandmanData(int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                LandmanDataObject landmanData = new LandmanDataObject();

                landmanData.Bills = TractInc.Expense.Data.Bill.GetInstance().GetBills(tran, assetId);
                landmanData.AssetAssignments = TractInc.Expense.Data.AssetAssignment.GetInstance().GetAssignments(tran, assetId);
                landmanData.Clients = TractInc.Expense.Data.Client.GetInstance().GetAssetClients(tran, assetId);

                tran.Commit();

                return landmanData;
            }
        }

        public List<InvoiceDataObject> GetInvoicesData()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                List<InvoiceDataObject> result = TractInc.Expense.Data.Invoice.GetInstance().GetInvoices(tran);

                tran.Commit();

                return result;
            }
        }

        public InvoiceDataObject GetInvoiceData(int invoiceId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                InvoiceDataObject result = TractInc.Expense.Data.Invoice.GetInstance().GetInvoice(tran, invoiceId);

                tran.Commit();

                return result;
            }
        }

        public ManagerDataObject GetManagerData()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                ManagerDataObject managerData = new ManagerDataObject();

                managerData.Bills = TractInc.Expense.Data.Bill.GetInstance().GetCurrentBills(tran);
                managerData.Clients = TractInc.Expense.Data.Client.GetInstance().GetCurrentClients(tran);
                managerData.Assets = TractInc.Expense.Data.Asset.GetInstance().GetCurrentAssets(tran);
                managerData.Assignments = TractInc.Expense.Data.AssetAssignment.GetInstance().GetCurrentAssignments(tran);

                tran.Commit();

                return managerData;
            }
        }

        public CrewChiefDataObject GetCrewChiefData(int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                CrewChiefDataObject crewChiefData = new CrewChiefDataObject();
                crewChiefData.Assets = TractInc.Expense.Data.Asset.GetInstance().GetCrewAssets(tran, assetId);
                crewChiefData.Bills = TractInc.Expense.Data.Bill.GetInstance().GetCrewBills(tran, assetId);
                crewChiefData.Clients = TractInc.Expense.Data.Client.GetInstance().GetCurrentClients(tran);
                crewChiefData.Assignments = TractInc.Expense.Data.AssetAssignment.GetInstance().GetCrewAssignments(tran, assetId);

                tran.Commit();

                return crewChiefData;
            }
        }

        public List<BillDataObject> GetOldCrewBills(int assetId, string startDate)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                List<BillDataObject> result = TractInc.Expense.Data.Bill.GetInstance().GetCrewBills(tran, assetId, startDate);

                tran.Commit();

                return result;
            }
        }

        public List<BillDataObject> GetOldBills(string startDate)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                List<BillDataObject> result = TractInc.Expense.Data.Bill.GetInstance().GetCrewBills(tran, 0, startDate);

                tran.Commit();

                return result;
            }
        }

        public LoginDataObject GetLoginData(int userId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                LoginDataObject loginInfo = new LoginDataObject();

                loginInfo.UserInfo = TractInc.Expense.Data.User.GetInstance().GetUser(tran, userId);
                loginInfo.UserRoleInfo = TractInc.Expense.Data.UserRole.GetInstance().GetUserRole(tran, userId);

                loginInfo.UserAssetInfo = TractInc.Expense.Data.UserAsset.GetInstance().GetUserAsset(tran, userId);
                if (null != loginInfo.UserAssetInfo)
                {
                    loginInfo.AssetInfo = TractInc.Expense.Data.Asset.GetInstance().GetAsset(tran, loginInfo.UserAssetInfo.AssetId);
                }

                loginInfo.AFEs = TractInc.Expense.Data.AFE.GetInstance().GetAFEs(tran);
                loginInfo.Projects = TractInc.Expense.Data.Project.GetInstance().GetProjects(tran);

                tran.Commit();

                return loginInfo;
            }
        }

        public MessagesDataObject CheckMessages(int userId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                MessagesDataObject messagesInfo = new MessagesDataObject();

                messagesInfo.InboxMessages = TractInc.Expense.Data.Message.GetInstance().GetMessagesByReceiver(tran, userId);
                messagesInfo.SentMessages = TractInc.Expense.Data.Message.GetInstance().GetMessagesBySender(tran, userId);
                messagesInfo.Users = TractInc.Expense.Data.User.GetInstance().GetUsers(tran);

                tran.Commit();

                return messagesInfo;
            }
        }

        public MessageDataObject PostMessage(MessageDataObject messageInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Message.GetInstance().Insert(tran, messageInfo);

                tran.Commit();

                return messageInfo;
            }
        }

        public MessageDataObject MarkAsRead(MessageDataObject messageInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Message.GetInstance().MarkAsRead(tran, messageInfo);

                tran.Commit();

                return messageInfo;
            }
        }

        public void RemoveMessage(int messageId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Message.GetInstance().Remove(tran, messageId);

                tran.Commit();
            }
        }

        public bool CanDeleteAssignment(int assignmentId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                bool result = TractInc.Expense.Data.AssetAssignment.GetInstance().CanDeleteAssignment(tran, assignmentId);

                tran.Commit();

                return result;
            }
        }

        public void DeleteAssignment(int assignmentId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.AssetAssignment.GetInstance().MarkDeleted(tran, assignmentId);
                
                tran.Commit();
            }
        }

        public AssetAssignmentDataObject StoreAssignment(AssetAssignmentDataObject assignmentInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (0 == assignmentInfo.AssetAssignmentId)
                {
                    TractInc.Expense.Data.AssetAssignment.GetInstance().Insert(tran, assignmentInfo);
                }

                foreach (RateByAssignmentDataObject assignmentRateInfo in assignmentInfo.Rates)
                {
                    assignmentRateInfo.AssetAssignmentId = assignmentInfo.AssetAssignmentId;

                    if (0 == assignmentRateInfo.RateByAssignmentId)
                    {
                        TractInc.Expense.Data.RateByAssignment.GetInstance().Insert(tran, assignmentRateInfo);
                    }
                    else if (0 == assignmentRateInfo.BillRate || 0 == assignmentRateInfo.InvoiceRate)
                    {
                        TractInc.Expense.Data.RateByAssignment.GetInstance().Remove(tran, assignmentRateInfo.RateByAssignmentId);
                        assignmentRateInfo.RateByAssignmentId = 0;
                    }
                    else
                    {
                        TractInc.Expense.Data.RateByAssignment.GetInstance().Update(tran, assignmentRateInfo);
                    }
                }

                tran.Commit();

                return assignmentInfo;
            }
        }

        public void RemoveInvoice(int invoiceId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Invoice.GetInstance().Remove(tran, invoiceId);

                tran.Commit();
            }
        }

        public InvoiceDataObject CreateInvoice(int year, int month, bool isFirstPart, int clientId)
        {
            return CreateInvoice(year, month, isFirstPart, clientId, 0);
        }

        public InvoiceDataObject CreateInvoice(int year, int month, bool isFirstPart, int clientId, int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                ClientDataObject clientInfo = TractInc.Expense.Data.Client.GetInstance().GetClient(tran, clientId);

                InvoiceDataObject invoiceInfo = new InvoiceDataObject();
                invoiceInfo.InvoiceNumber = "";
                invoiceInfo.ClientId = clientInfo.ClientId;
                invoiceInfo.ClientName = clientInfo.ClientName;
                invoiceInfo.ClientAddress = clientInfo.ClientAddress;
                invoiceInfo.Status = "NEW";
                invoiceInfo.StartDate = month.ToString() + "/" + (isFirstPart? "01": "16") + "/" + year.ToString();
                invoiceInfo.TotalDailyAmt = 0;
                invoiceInfo.DailyInvoiceAmt = 0;
                invoiceInfo.OtherInvoiceAmt = 0;
                invoiceInfo.TotalInvoiceAmt = 0;
                invoiceInfo.InvoiceItems = new List<InvoiceItemDataObject>();

                TractInc.Expense.Data.Invoice.GetInstance().Insert(tran, invoiceInfo);

                invoiceInfo.InvoiceItems = new List<InvoiceItemDataObject>();

                List<BillItemDataObject> billItems = TractInc.Expense.Data.BillItem.GetInstance().GetBillItemsForInvoice(tran, year, month, isFirstPart, clientId, assetId);

                foreach (BillItemDataObject billItemInfo in billItems)
                {
                    InvoiceItemDataObject invoiceItemInfo = new InvoiceItemDataObject();

                    invoiceItemInfo.BillItemId = billItemInfo.BillItemId;
                    invoiceItemInfo.InvoiceItemTypeId = billItemInfo.BillItemTypeId;
                    invoiceItemInfo.InvoiceDate = billItemInfo.BillingDate;
                    invoiceItemInfo.AssetAssignmentId = billItemInfo.AssetAssignmentId;
                    invoiceItemInfo.Qty = billItemInfo.Qty;
                    invoiceItemInfo.Status = BillItemDataObject.BILL_ITEM_STATUS_NEW;
                    invoiceItemInfo.IsSelected = true;

                    RateByAssignmentDataObject rateInfo = TractInc.Expense.Data.RateByAssignment.GetInstance().GetRateByBillItemId(tran, billItemInfo.BillItemId);

                    if (rateInfo == null)
                    {
                        invoiceItemInfo.InvoiceRate = billItemInfo.BillRate;
                    }
                    else
                    {
                        invoiceItemInfo.InvoiceRate = rateInfo.InvoiceRate;
                    }

                    invoiceItemInfo.InvoiceId = invoiceInfo.InvoiceId;

                    invoiceInfo.InvoiceItems.Add(invoiceItemInfo);

                    decimal amount = invoiceItemInfo.Qty * invoiceItemInfo.InvoiceRate;

                    if (invoiceItemInfo.InvoiceItemTypeId == 1)
                    {
                        invoiceInfo.TotalDailyAmt += (int)invoiceItemInfo.Qty;
                        invoiceInfo.DailyInvoiceAmt += amount;
                    }
                    else
                    {
                        invoiceInfo.OtherInvoiceAmt += amount;
                    }

                    invoiceInfo.TotalInvoiceAmt += amount;

                    TractInc.Expense.Data.InvoiceItem.GetInstance().Insert(tran, invoiceItemInfo);
                }

                TractInc.Expense.Data.Invoice.GetInstance().Update(tran, invoiceInfo);

                tran.Commit();

                return invoiceInfo;
            }
        }

        public ClientDataObject StoreClient(ClientDataObject clientInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (0 == clientInfo.ClientId)
                {
                    TractInc.Expense.Data.Client.GetInstance().Insert(tran, clientInfo);

                    if (null != clientInfo.DefaultRates)
                    {
                        foreach (DefaultInvoiceRateDataObject rateInfo in clientInfo.DefaultRates)
                        {
                            rateInfo.ClientId = clientInfo.ClientId;
                            TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Insert(tran, rateInfo);
                        }
                    }
                }
                else
                {
                    TractInc.Expense.Data.Client.GetInstance().Update(tran, clientInfo);

                    if (null != clientInfo.DefaultRates)
                    {
                        foreach (DefaultInvoiceRateDataObject rateInfo in clientInfo.DefaultRates)
                        {
                            TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Update(tran, rateInfo);
                        }
                    }
                }

                tran.Commit();
            }

            return clientInfo;
        }

        public bool CanRemoveClient(int clientId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                bool result = TractInc.Expense.Data.Client.GetInstance().CanRemoveClient(tran, clientId);

                tran.Commit();

                return result;
            }
        }

        public void RemoveClient(int clientId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Client.GetInstance().Remove(tran, clientId);

                tran.Commit();
            }
        }

        public void StoreAFE(AFEDataObject afeInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (afeInfo.IsNew)
                {
                    TractInc.Expense.Data.AFE.GetInstance().Insert(tran, afeInfo);
                    afeInfo.IsNew = false;
                }
                else
                {
                    TractInc.Expense.Data.AFE.GetInstance().Update(tran, afeInfo);
                }

                tran.Commit();
            }
        }

        public bool CanRemoveAFE(string afe)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                bool result = TractInc.Expense.Data.AFE.GetInstance().CanRemoveAFE(tran, afe);

                tran.Commit();

                return result;
            }
        }

        public void RemoveAFE(string afe)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.AFE.GetInstance().Remove(tran, afe);

                tran.Commit();
            }
        }

        public void StoreProject(ProjectDataObject projectInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (projectInfo.IsNew)
                {
                    TractInc.Expense.Data.Project.GetInstance().Insert(tran, projectInfo);
                    projectInfo.IsNew = false;
                }
                else
                {
                    TractInc.Expense.Data.Project.GetInstance().Update(tran, projectInfo);
                }

                tran.Commit();
            }
        }

        public bool CanRemoveProject(string project)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                bool result = TractInc.Expense.Data.Project.GetInstance().CanRemoveProject(tran, project);

                tran.Commit();

                return result;
            }
        }

        public void RemoveProject(string project)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Project.GetInstance().Remove(tran, project);

                tran.Commit();
            }
        }

        public AssetDataObject StoreAsset(AssetDataObject assetInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (0 == assetInfo.AssetId)
                {
                    TractInc.Expense.Data.Asset.GetInstance().Insert(tran, assetInfo);

                    if (0 == assetInfo.ChiefAssetId)
                    {
                        assetInfo.ChiefAssetId = assetInfo.AssetId;

                        TractInc.Expense.Data.Asset.GetInstance().Update(tran, assetInfo);
                    }

                    if (null != assetInfo.DefaultRates)
                    {
                        foreach (DefaultBillRateDataObject rateInfo in assetInfo.DefaultRates)
                        {
                            rateInfo.AssetId = assetInfo.AssetId;
                            TractInc.Expense.Data.DefaultBillRate.GetInstance().Insert(tran, rateInfo);
                        }
                    }

                    assetInfo.Bills = new List<BillDataObject>();
                    for (int i = 1; i < 13; i++) {
                        string startDate = "";
                        startDate = (i < 10) ? ("0" + i.ToString()) : i.ToString();
                        
                        BillDataObject bill1 = new BillDataObject();
                        bill1.AssetId = assetInfo.AssetId;
                        bill1.Status = BillDataObject.BILL_STATUS_NEW;
                        bill1.StartDate = startDate + "/01/" + DateTime.Now.Year.ToString();

                        assetInfo.Bills.Add(bill1);

                        TractInc.Expense.Data.Bill.GetInstance().Insert(tran, bill1);

                        BillDataObject bill2 = new BillDataObject();
                        bill2.AssetId = assetInfo.AssetId;
                        bill2.Status = BillDataObject.BILL_STATUS_NEW;
                        bill2.StartDate = startDate + "/16/" + DateTime.Now.Year.ToString();

                        assetInfo.Bills.Add(bill2);

                        TractInc.Expense.Data.Bill.GetInstance().Insert(tran, bill2);
                    }

                    TractInc.Expense.Data.User.GetInstance().Insert(tran, assetInfo.UserInfo);

                    UserAssetDataObject userAssetInfo = new UserAssetDataObject();
                    userAssetInfo.AssetId = assetInfo.AssetId;
                    userAssetInfo.UserId = assetInfo.UserInfo.UserId;

                    TractInc.Expense.Data.UserAsset.GetInstance().Insert(tran, userAssetInfo);

                    UserRoleDataObject userRoleInfo = new UserRoleDataObject();
                    userRoleInfo.RoleId = 2;
                    userRoleInfo.UserId = assetInfo.UserInfo.UserId;

                    TractInc.Expense.Data.UserRole.GetInstance().Insert(tran, userRoleInfo);
                }
                else
                {
                    TractInc.Expense.Data.Asset.GetInstance().Update(tran, assetInfo);

                    if (null != assetInfo.DefaultRates)
                    {
                        foreach (DefaultBillRateDataObject rateInfo in assetInfo.DefaultRates)
                        {
                            TractInc.Expense.Data.DefaultBillRate.GetInstance().Update(tran, rateInfo);
                        }
                    }

                    if (null != assetInfo.UserInfo)
                    {
                        TractInc.Expense.Data.User.GetInstance().Update(tran, assetInfo.UserInfo);
                    }
                }

                tran.Commit();
            }

            return assetInfo;
        }

        public bool CanRemoveAsset(int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                bool result = TractInc.Expense.Data.Asset.GetInstance().CanRemoveAsset(tran, assetId);

                tran.Commit();

                return result;
            }
        }

        public void RemoveAsset(int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                TractInc.Expense.Data.Asset.GetInstance().Remove(tran, assetId);

                tran.Commit();
            }
        }

        public DefaultBillRateDataObject StoreDefaultBillRate(DefaultBillRateDataObject rateInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (0 == rateInfo.DefaultBillRateId)
                {
                    if (0 < rateInfo.BillRate)
                    {
                        TractInc.Expense.Data.DefaultBillRate.GetInstance().Insert(tran, rateInfo);
                    }
                }
                else
                {
                    if (0 < rateInfo.BillRate)
                    {
                        TractInc.Expense.Data.DefaultBillRate.GetInstance().Update(tran, rateInfo);
                    }
                    else
                    {
                        TractInc.Expense.Data.DefaultBillRate.GetInstance().Remove(tran, rateInfo);
                    }
                }

                tran.Commit();
            }

            return rateInfo;
        }

        public DefaultInvoiceRateDataObject StoreDefaultInvoiceRate(DefaultInvoiceRateDataObject rateInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (0 == rateInfo.DefaultInvoiceRateId)
                {
                    if (0 < rateInfo.InvoiceRate)
                    {
                        TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Insert(tran, rateInfo);
                    }
                }
                else
                {
                    if (0 < rateInfo.InvoiceRate)
                    {
                        TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Update(tran, rateInfo);
                    }
                    else
                    {
                        TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Remove(tran, rateInfo);
                    }
                }

                tran.Commit();
            }

            return rateInfo;
        }

        #endregion

        public InvoiceDataObject StoreInvoice(InvoiceDataObject invoiceInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if ((InvoiceStatusDataObject.INVOICE_STATUS_VOID == invoiceInfo.Status)
                        || (InvoiceStatusDataObject.INVOICE_STATUS_PAID == invoiceInfo.Status)
                        || (InvoiceStatusDataObject.INVOICE_STATUS_SUBMITTED == invoiceInfo.Status))
                {
                    foreach (InvoiceItemDataObject itemInfo in invoiceInfo.InvoiceItems)
                    {
                        itemInfo.Status = invoiceInfo.Status;
                    }
                }

                if (0 == invoiceInfo.InvoiceId)
                {
                    TractInc.Expense.Data.Invoice.GetInstance().Insert(tran, invoiceInfo);

                    if (null != invoiceInfo.InvoiceItems)
                    {
                        foreach (InvoiceItemDataObject itemInfo in invoiceInfo.InvoiceItems)
                        {
                            itemInfo.InvoiceId = invoiceInfo.InvoiceId;
                            TractInc.Expense.Data.InvoiceItem.GetInstance().Insert(tran, itemInfo);
                        }
                    }
                }
                else
                {
                    TractInc.Expense.Data.Invoice.GetInstance().Update(tran, invoiceInfo);

                    if (null != invoiceInfo.InvoiceItems)
                    {
                        foreach (InvoiceItemDataObject itemInfo in invoiceInfo.InvoiceItems)
                        {
                            if (itemInfo.Deleted)
                            {
                                TractInc.Expense.Data.InvoiceItem.GetInstance().Remove(tran, itemInfo.InvoiceItemId);
                            }
                            else
                            {
                                if (0 == itemInfo.InvoiceItemId)
                                {
                                    TractInc.Expense.Data.InvoiceItem.GetInstance().Insert(tran, itemInfo);
                                }
                                else
                                {
                                    TractInc.Expense.Data.InvoiceItem.GetInstance().Update(tran, itemInfo);
                                }
                            }
                        }
                    }
                }

                tran.Commit();
            }

            return invoiceInfo;
        }

        public bool CheckInvoiceNumber(int invoiceId, String invoiceNumber)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                bool result = TractInc.Expense.Data.Invoice.GetInstance().CheckInvoiceNumber(tran, invoiceId, invoiceNumber);

                tran.Commit();

                return result;
            }
        }

        public void ImportData()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["ExpenseAppDir"] + "clients.csv");
                Hashtable clientsHash = new Hashtable();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> parsedLine = ParseLine(line);

                    ClientDataObject clientInfo = new ClientDataObject();
                    clientInfo.Active = true;
                    clientInfo.ClientAddress = "";
                    clientInfo.ClientName = parsedLine[1].Trim();
                    clientInfo.Deleted = false;

                    TractInc.Expense.Data.Client.GetInstance().Insert(tran, clientInfo);
                    clientsHash[parsedLine[0]] = clientInfo;

                    DefaultInvoiceRateDataObject invoiceRateInfo = new DefaultInvoiceRateDataObject();
                    invoiceRateInfo.ClientId = clientInfo.ClientId;
                    invoiceRateInfo.InvoiceItemTypeId = 1;
                    invoiceRateInfo.InvoiceRate = 62.5m;
                    TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Insert(tran, invoiceRateInfo);

                    invoiceRateInfo = new DefaultInvoiceRateDataObject();
                    invoiceRateInfo.ClientId = clientInfo.ClientId;
                    invoiceRateInfo.InvoiceItemTypeId = 2;
                    invoiceRateInfo.InvoiceRate = 0.5m;
                    TractInc.Expense.Data.DefaultInvoiceRate.GetInstance().Insert(tran, invoiceRateInfo);
                }

                reader.Close();

                reader = new StreamReader(ConfigurationManager.AppSettings["ExpenseAppDir"] + "afes.csv");

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> parsedLine = ParseLine(line);

                    AFEDataObject afeInfo = new AFEDataObject();
                    afeInfo.AFE = parsedLine[0];
                    afeInfo.AFEName = parsedLine[1];
                    afeInfo.AFEStatus = "ISSUED";
                    afeInfo.ClientId = ((ClientDataObject)clientsHash[parsedLine[2]]).ClientId;
                    afeInfo.Deleted = false;
                    afeInfo.IsNew = true;
                    TractInc.Expense.Data.AFE.GetInstance().Insert(tran, afeInfo);

                    ProjectDataObject projectInfo = new ProjectDataObject();
                    projectInfo.AFE = parsedLine[0];
                    projectInfo.SubAFE = parsedLine[0] + " - " + parsedLine[1];
                    projectInfo.SubAFEStatus = "ISSUED";
                    projectInfo.Temporary = false;
                    projectInfo.ShortName = parsedLine[0];
                    projectInfo.Deleted = false;
                    projectInfo.IsNew = true;
                    try
                    {
                        TractInc.Expense.Data.Project.GetInstance().Insert(tran, projectInfo);
                    }
                    catch (Exception)
                    {
                        throw new Exception(projectInfo.SubAFE);
                    }
                }

                reader.Close();

                tran.Commit();
            }
        }

        private List<String> ParseLine(string source)
        {
            List<String> result = new List<string>();

            if (0 == source.Length)
            {
                return result;
            }

            int currentIndex = 0;
            int currentState = 0;
            string currentValue = "";

            while (currentIndex < source.Length)
            {
                switch (currentState) {
                    case 0:
                        if ('"' == source[currentIndex])
                        {
                            currentState = 2;
                        }
                        else
                        {
                            currentState = 1;
                            currentValue += source[currentIndex];
                        }
                        break;
                    case 1:
                        if (',' == source[currentIndex])
                        {
                            currentState = 4;
                            result.Add(currentValue);
                            currentValue = "";
                        }else
                        {
                            currentState = 3;
                            currentValue += source[currentIndex];
                        }
                        break;
                    case 2:
                        currentState = 5;
                        currentValue += source[currentIndex];
                        break;
                    case 3:
                        if (',' == source[currentIndex])
                        {
                            currentState = 4;
                            result.Add(currentValue);
                            currentValue = "";
                        }
                        else
                        {
                            currentValue += source[currentIndex];
                        }
                        break;
                    case 4:
                        if ('"' == source[currentIndex])
                        {
                            currentState = 2;
                        }
                        else if (',' == source[currentIndex])
                        {
                            result.Add("");
                        }
                        else
                        {
                            currentState = 1;
                            currentValue += source[currentIndex];
                        }
                        break;
                    case 5:
                        if ('"' == source[currentIndex])
                        {
                            currentState = 6;
                        }
                        else
                        {
                            currentValue += source[currentIndex];
                        }
                        break;
                    case 6:
                        if ('"' == source[currentIndex])
                        {
                            currentState = 5;
                            currentValue += '"';
                        }
                        else if (',' == source[currentIndex])
                        {
                            currentState = 4;
                            result.Add(currentValue);
                            currentValue = "";
                        }
                        break;
                }

                currentIndex++;
            }

            if (0 < currentValue.Length)
            {
                result.Add(currentValue);
            }

            return result;
        }

    }

}
