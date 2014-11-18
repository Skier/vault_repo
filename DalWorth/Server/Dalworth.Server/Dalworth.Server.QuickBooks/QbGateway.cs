using System;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;

using QBXMLRP2Lib;
using Dalworth.Server.Domain;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.QuickBooks
{
    public delegate int SelectSimilarCustomer(QbCustomer newCustomer, List<QbCustomer> similarCustomers);

    public class QbException : Exception
    {
        private int m_errorCode;
        public int ErrorCode
        {
            get {return m_errorCode;}
        }

        public QbException(string message):base(message) { }

        public QbException (int errorCode, string message): base(message)
        {
            m_errorCode = errorCode;
        }
    }

    public class QbEntityQueryResponse
    {
        #region ListId

        private string m_listId;
        public string ListId
        {
            get { return m_listId; }
            set { m_listId = value;}
        }

        #endregion 

        #region FirstName

        private string m_firstName;
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value != null ? value.ToUpper() : null; }
        }

        #endregion

        #region LastName

        private string m_lastName;
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value != null ? value.ToUpper() : null; }
        }

        #endregion 

        public QbSalesRepTypeEnum QbSalesRepType;
    }

    public class QbGateway
    {
        #region Private Properties 

        private ReportStatus m_reportStatus;
        private string m_ticket;
        private RequestProcessor2 m_requestProcessor;
      
        private static string m_appID = Configuration.QuickBooksAppID;
        private static string m_appName = Configuration.QuickBooksAppName;
        private string m_companyFile;
        private QBFileMode m_mode = QBFileMode.qbFileOpenDoNotCare;

        #endregion 

        #region Constructor

        public QbGateway(string companyFile, ReportStatus reportStatus)
        {
            m_companyFile = companyFile;
            m_reportStatus = reportStatus;
        }

        #endregion

        #region ProjectInsurance

        public void AddProjectInsurance(ProjectInsurance projectInsurance, QbCustomer qbCustomer)
        {
            string request = BuildProjectInsuranceRqXml(projectInsurance, qbCustomer);
            Host.QuickBooksDebug("AddProjectInsurance", request);

            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            Host.QuickBooksDebug("AddProjectInsurance", response);

            ParseDataExtAddRs(response);
        }

        private void ParseDataExtAddRs(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList dataExtAddRs = responseXmlDoc.GetElementsByTagName("DataExtAddRs");

            foreach (XmlNode responseNode in dataExtAddRs)
            {
                IsQbResponseAvailable(responseNode);
            }
        }

        private string BuildProjectInsuranceRqXml(ProjectInsurance projectInsurance, QbCustomer qbCustomer)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "1", !string.IsNullOrEmpty(projectInsurance.Company), qbCustomer.ListId, "Insurance", projectInsurance.Company);
            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "2", !string.IsNullOrEmpty(projectInsurance.Address1), qbCustomer.ListId, "Address", projectInsurance.Address1);
            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "3", !string.IsNullOrEmpty(projectInsurance.Address2), qbCustomer.ListId, "City, State, Zip", projectInsurance.Address2);
            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "4", !string.IsNullOrEmpty(projectInsurance.Contact), qbCustomer.ListId, "Contact", projectInsurance.Contact);
            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "5", !string.IsNullOrEmpty(projectInsurance.Phone), qbCustomer.ListId, "Phone", projectInsurance.Phone);
            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "6", !string.IsNullOrEmpty(projectInsurance.Fax), qbCustomer.ListId, "Fax", projectInsurance.Fax);
            BuildDataExtAdd(xmlDoc, qbXMLMsgsRq, "7", !string.IsNullOrEmpty(projectInsurance.ClaimNumber), qbCustomer.ListId, "Claim#", projectInsurance.ClaimNumber);

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private void BuildDataExtAdd(XmlDocument xmlDoc, XmlElement qbXMLMsgsRq, string requestID, bool condition, string listID, string dataExtName, string dataExtValue)
        {
            if (condition)
            {
                XmlElement dataExtAddRq = xmlDoc.CreateElement("DataExtAddRq");
                qbXMLMsgsRq.AppendChild(dataExtAddRq);
                dataExtAddRq.SetAttribute("requestID", requestID);

                XmlElement dataExtAdd = xmlDoc.CreateElement("DataExtAdd");
                dataExtAddRq.AppendChild(dataExtAdd);

                AddValue(xmlDoc, dataExtAdd, true, "OwnerID", "0");
                AddValue(xmlDoc, dataExtAdd, true, "DataExtName", dataExtName);
                AddValue(xmlDoc, dataExtAdd, true, "ListDataExtType", "Customer");

                XmlElement listObjRef = xmlDoc.CreateElement("ListObjRef");
                dataExtAdd.AppendChild(listObjRef);
                AddValue(xmlDoc, listObjRef, true, "ListID", listID);

                AddValue(xmlDoc, dataExtAdd, true, "DataExtValue", dataExtValue);
            }
        }

        #endregion

        #region QbAccount

        public void RefreshAccounts()
        {
            List<QbAccount> accountsInQuickBooks = FindAccountsInQb();
            List<QbAccount> accountsInDb = QbAccount.Find();
            
            foreach (QbAccount accountInQb in accountsInQuickBooks)
            {
                QbAccount accountInDb = accountsInDb.Find(delegate(QbAccount dbAccount) { return dbAccount.ListId == accountInQb.ListId; });

                if (accountInDb == null)
                    QbAccount.Insert(accountInQb);
                else if (accountInQb.EditSequence != accountInDb.EditSequence)
                    QbAccount.Update(accountInQb);
            }
        }

        private List<QbAccount> FindAccountsInQb()
        {
            string request = BuildGetAllQueryRqXml("AccountQueryRq");
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseAccountQueryResponse(response);
        }

        private List<QbAccount> ParseAccountQueryResponse(string xml)
        {
            List<QbAccount> result = new List<QbAccount>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList accountQueryRsList = responseXmlDoc.GetElementsByTagName("AccountQueryRs");
            if (accountQueryRsList.Count != 1) 
                throw new QbException("Invalid response, missing AccountQueryRs");

            XmlNode responseNode = accountQueryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList accountRetList = responseNode.SelectNodes("//AccountRet");
            if (accountRetList != null)
            {
                for (int i = 0; i < accountRetList.Count; i++)
                {
                    QbAccount account = new QbAccount();
                    XmlNode accountNode = accountRetList.Item(i);

                    account.ListId = accountNode.SelectSingleNode("./ListID").InnerText;
                    account.FullName = accountNode.SelectSingleNode("./FullName").InnerText;
                    account.AccountType = accountNode.SelectSingleNode("./AccountType").InnerText;
                    account.TimeCreated = DateTime.Parse(accountNode.SelectSingleNode("./TimeCreated").InnerText);
                    account.TimeModified = DateTime.Parse(accountNode.SelectSingleNode("./TimeModified").InnerText);
                    account.EditSequence = accountNode.SelectSingleNode("./EditSequence").InnerText;

                    result.Add(account);
                }
            }

            return result;
        }

        #endregion 

        #region QbItem

        public void RefreshItems()
        {
            List<QbItem> itemsInQb = FindQbItems(null);
            List<QbItem> itemsInDb = QbItem.Find();

            foreach (QbItem itemInQb in itemsInQb)
            {
                string fullname = itemInQb.FullName;

                QbItem itemInDb = itemsInDb.Find(delegate (QbItem item){return item.ListId == itemInQb.ListId;});

                if (itemInDb == null)
                    QbItem.Insert(itemInQb);
                else if (itemInDb.EditSequence != itemInQb.EditSequence)
                    QbItem.Update(itemInQb);
            }
        }

        private List<QbItem> FindQbItems(string ListId)
        {
            string request = BuildItemQueryRqXML(1, null);
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseItemQueryResponse(response);
        }

        private string BuildItemQueryRqXML(int requestId, string listId)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement ItemQueryRq = xmlDoc.CreateElement("ItemQueryRq");
            qbXMLMsgsRq.AppendChild(ItemQueryRq);
            if (listId != null)
            {
                XmlElement fullNameElement = xmlDoc.CreateElement("ListId");
                ItemQueryRq.AppendChild(fullNameElement).InnerText = listId;
            }

            ItemQueryRq.SetAttribute("requestID", requestId.ToString());
            xml = xmlDoc.OuterXml;
            return xml;
        }

        private List<QbItem> ParseItemQueryResponse(string xml)
        {
            List<QbItem> result = new List<QbItem>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList itemQueryRsList = responseXmlDoc.GetElementsByTagName("ItemQueryRs");
            if (itemQueryRsList.Count != 1) 
                throw new QbException("Invalid response, missing ItemQueryRs");

            XmlNode responseNode = itemQueryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList itemServiceRetList = responseNode.SelectNodes("//ItemServiceRet");

            XmlNodeList itemSalesTaxItemRetList = responseNode.SelectNodes("//ItemSalesTaxRet");

            XmlNodeList itemDiscountRetList = responseNode.SelectNodes("//ItemDiscountRet");

            XmlNodeList itemOtherCharges = responseNode.SelectNodes("//ItemOtherChargeRet");

            foreach (XmlNode itemNode in itemOtherCharges)
            {
                QbItem item = new QbItem();
                item.ListId = itemNode.SelectSingleNode("./ListID").InnerText;
                item.TimeCreated = DateTime.Parse(itemNode.SelectSingleNode("./TimeCreated").InnerText);
                item.TimeModified = DateTime.Parse(itemNode.SelectSingleNode("./TimeModified").InnerText);
                item.EditSequence = itemNode.SelectSingleNode("./EditSequence").InnerText;
                item.Name = itemNode.SelectSingleNode("./Name").InnerText;
                item.FullName = itemNode.SelectSingleNode("./FullName").InnerText;
                item.IsActive = Convert.ToBoolean(itemNode.SelectSingleNode("./IsActive ").InnerText);
                item.QbItemTypeId = (int)QbItemTypeEnum.OtherCharge;
                result.Add(item);
            }

            foreach (XmlNode itemNode in itemServiceRetList)
            {
                QbItem item = new QbItem();
               
                item.ListId = itemNode.SelectSingleNode("./ListID").InnerText;
                item.TimeCreated = DateTime.Parse(itemNode.SelectSingleNode("./TimeCreated").InnerText);
                item.TimeModified = DateTime.Parse(itemNode.SelectSingleNode("./TimeModified").InnerText);
                item.EditSequence = itemNode.SelectSingleNode("./EditSequence").InnerText;
                item.Name = itemNode.SelectSingleNode("./Name").InnerText;
                item.FullName = itemNode.SelectSingleNode("./FullName").InnerText;
                item.IsActive = Convert.ToBoolean(itemNode.SelectSingleNode("./IsActive ").InnerText);
                item.QbItemTypeId = (int) QbItemTypeEnum.Service;
                item.AccountRefListId = itemNode.SelectSingleNode("./SalesOrPurchase/AccountRef/ListID").InnerText;
                item.SalesTaxCodeRefListId = itemNode.SelectSingleNode("./SalesTaxCodeRef/ListID").InnerText;
                item.Description = itemNode.SelectSingleNode("./SalesOrPurchase/Desc").InnerText;

                XmlNode priceNode = itemNode.SelectSingleNode("./SalesOrPurchase/Price");
                if (priceNode != null)
                    item.Price = Convert.ToDecimal(priceNode.InnerText);

                result.Add(item);
            }

            for (int i = 0; i < itemSalesTaxItemRetList.Count; i++)
            {
                QbItem item = new QbItem();
                XmlNode itemNode = itemSalesTaxItemRetList.Item(i);

                item.ListId = itemNode.SelectSingleNode("./ListID").InnerText;
                item.TimeCreated = DateTime.Parse(itemNode.SelectSingleNode("./TimeCreated").InnerText);
                item.TimeModified = DateTime.Parse(itemNode.SelectSingleNode("./TimeModified").InnerText);
                item.EditSequence = itemNode.SelectSingleNode("./EditSequence").InnerText;
                item.Name = itemNode.SelectSingleNode("./Name").InnerText;
                item.FullName = item.Name;
                item.Description = itemNode.SelectSingleNode("./ItemDesc").InnerText;
                item.QbItemTypeId = (int)QbItemTypeEnum.Tax;
                item.TaxRate = Convert.ToDecimal(itemNode.SelectSingleNode("./TaxRate").InnerText);

                result.Add(item);
            }

            foreach (XmlNode itemNode in itemDiscountRetList)
            {
                QbItem item = new QbItem();

                item.ListId = itemNode.SelectSingleNode("./ListID").InnerText;
                item.TimeCreated = DateTime.Parse(itemNode.SelectSingleNode("./TimeCreated").InnerText);
                item.TimeModified = DateTime.Parse(itemNode.SelectSingleNode("./TimeModified").InnerText);
                item.EditSequence = itemNode.SelectSingleNode("./EditSequence").InnerText;
                item.Name = itemNode.SelectSingleNode("./Name").InnerText;
                item.FullName = itemNode.SelectSingleNode("./FullName").InnerText;
                item.IsActive = Convert.ToBoolean(itemNode.SelectSingleNode("./IsActive ").InnerText);
                item.QbItemTypeId = (int)QbItemTypeEnum.Discount;
                item.AccountRefListId = itemNode.SelectSingleNode("./AccountRef/ListID").InnerText;
                item.SalesTaxCodeRefListId = itemNode.SelectSingleNode("./SalesTaxCodeRef/ListID").InnerText;
                
                result.Add(item);
            }

            return result;
        }

        #endregion

        #region QbClass

        public void RefreshClasses()
        {
            List<QbClass> classListInQb = FindQbClass();
            List<QbClass> classListInDb = QbClass.Find();

            foreach (QbClass classInQb in classListInQb)
            {
                int idx = classListInDb.FindIndex(delegate(QbClass tempQbClass) { return tempQbClass.ListId == classInQb.ListId; });

                if (idx >= 0)
                {
                    QbClass classInDb = classListInDb[idx];
                    classListInDb.RemoveAt(idx);
                    if (classInDb.EditSequence != classInQb.EditSequence)
                        QbClass.Update(classInQb);
                }
                else
                {
                    QbClass.Insert(classInQb);
                }
            }

            foreach (QbClass classInDb in classListInDb)
            {
                classInDb.IsActive = false;
                QbClass.Update(classInDb);
            }
        }

        private List<QbClass> FindQbClass()
        {
            string request = BuildGetAllQueryRqXml("ClassQueryRq");
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseClassQueryResponse(response);
        }

        private List<QbClass> ParseClassQueryResponse(string xml)
        {
            List<QbClass> result = new List<QbClass>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("ClassQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing ClassQueryRs");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList classRetList = responseNode.SelectNodes("//ClassRet");

            foreach (XmlNode classRetNode in classRetList)
            {
                QbClass qbClass = new QbClass();

                qbClass.ListId = classRetNode.SelectSingleNode("./ListID").InnerText;
                qbClass.FullName = classRetNode.SelectSingleNode("./FullName").InnerText;
                qbClass.IsActive = Convert.ToBoolean(classRetNode.SelectSingleNode("./IsActive ").InnerText);
                qbClass.Name = classRetNode.SelectSingleNode("./Name").InnerText;
                qbClass.SubLevel = Convert.ToInt32(classRetNode.SelectSingleNode("./Sublevel").InnerText);
                qbClass.TimeCreated = DateTime.Parse(classRetNode.SelectSingleNode("./TimeCreated").InnerText);
                qbClass.TimeModified = DateTime.Parse(classRetNode.SelectSingleNode("./TimeModified").InnerText);
                qbClass.EditSequence = classRetNode.SelectSingleNode("./EditSequence").InnerText;

                XmlNode parentRefNode = classRetNode.SelectSingleNode("./ParentRef");
                if (parentRefNode != null)
                {
                    qbClass.ParentClassListID = parentRefNode.SelectSingleNode("./ListID").InnerText;
                }

                result.Add(qbClass);
            }

            return result;
        }

        #endregion

        #region QbSalesTaxCode

        public void RefreshSalesTaxCodes()
        {
            List<QbSalesTaxCode> salesTaxCodesInQb = FindQbSalesTaxCode();
            List<QbSalesTaxCode> salesTaxCodesInDb = QbSalesTaxCode.Find();

            foreach (QbSalesTaxCode salesTaxCodeInQb in salesTaxCodesInQb)
            {
                QbSalesTaxCode salesTaxCodeInDb = salesTaxCodesInDb.Find(delegate(QbSalesTaxCode tempSalesTaxCode)
                    { return tempSalesTaxCode.ListId == salesTaxCodeInQb.ListId; });

                if (salesTaxCodeInDb == null)
                    QbSalesTaxCode.Insert(salesTaxCodeInQb);
                else if (salesTaxCodeInDb.EditSequence != salesTaxCodeInQb.EditSequence)
                    QbSalesTaxCode.Update(salesTaxCodeInQb);
            }
        }

        private List<QbSalesTaxCode> FindQbSalesTaxCode()
        {
            string request = BuildGetAllQueryRqXml("SalesTaxCodeQueryRq");
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseSalesTaxCodeQueryResponse(response);
        }

        private List<QbSalesTaxCode> ParseSalesTaxCodeQueryResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            List<QbSalesTaxCode> result = new List<QbSalesTaxCode>();

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("SalesTaxCodeQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing SalesTaxCodeQueryRs");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList salesTaxCodeRetList = responseNode.SelectNodes("//SalesTaxCodeRet");
            foreach (XmlNode salesTaxCodeRetNode in salesTaxCodeRetList)
            {
                QbSalesTaxCode qbSalesTaxCode = new QbSalesTaxCode();

                qbSalesTaxCode.ListId = salesTaxCodeRetNode.SelectSingleNode("./ListID").InnerText;
                qbSalesTaxCode.Description = salesTaxCodeRetNode.SelectSingleNode("./Desc").InnerText;
                qbSalesTaxCode.IsActive = Convert.ToBoolean(salesTaxCodeRetNode.SelectSingleNode("./IsActive ").InnerText);
                qbSalesTaxCode.IsTaxable = Convert.ToBoolean(salesTaxCodeRetNode.SelectSingleNode("./IsTaxable  ").InnerText);
                qbSalesTaxCode.Name = salesTaxCodeRetNode.SelectSingleNode("./Name").InnerText;
                qbSalesTaxCode.TimeCreated = DateTime.Parse(salesTaxCodeRetNode.SelectSingleNode("./TimeCreated").InnerText);
                qbSalesTaxCode.TimeModified = DateTime.Parse(salesTaxCodeRetNode.SelectSingleNode("./TimeModified").InnerText);
                qbSalesTaxCode.EditSequence = salesTaxCodeRetNode.SelectSingleNode("./EditSequence").InnerText;

                result.Add(qbSalesTaxCode);
            }

            return result;
        }

        #endregion

        #region QbTemplate

        public void RefreshTemplates ()
        {
            List<QbTemplate> templatesInQb = FindQbTemplate();
            List<QbTemplate> templatesInDb = QbTemplate.Find();

            foreach (QbTemplate templateInQb in templatesInQb)
            {
                QbTemplate templateInDb = templatesInDb.Find(delegate(QbTemplate tempQbTemplate)
                    { return tempQbTemplate.ListId == templateInQb.ListId; });

                if (templateInDb == null)
                    QbTemplate.Insert(templateInQb);
                else if (templateInQb.EditSequence != templateInDb.EditSequence)
                    QbTemplate.Update(templateInQb);
            }
        }

        private List<QbTemplate> FindQbTemplate()
        {
            string request = BuildGetAllQueryRqXml("TemplateQueryRq", false);
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseTemplateQueryResponse(response);
        }

        private List<QbTemplate> ParseTemplateQueryResponse(string xml)
        {
            List<QbTemplate> result = new List<QbTemplate>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("TemplateQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing TemplateQueryRs");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList retList = responseNode.SelectNodes("//TemplateRet");

            foreach (XmlNode node in retList)
            {
                QbTemplate template = new QbTemplate();

                template.IsActive = Convert.ToBoolean(node.SelectSingleNode("./IsActive ").InnerText);
                template.ListId = node.SelectSingleNode("./ListID").InnerText;
                template.Name = node.SelectSingleNode("./Name").InnerText;
                template.TimeCreated = DateTime.Parse(node.SelectSingleNode("./TimeCreated").InnerText);
                template.TimeModified = DateTime.Parse(node.SelectSingleNode("./TimeModified").InnerText);
                template.EditSequence = node.SelectSingleNode("./EditSequence").InnerText;
                result.Add(template);
            }

            return result;
        }

        #endregion

        #region QbInvoiceTerm

        public void RefreshInvoiceTerms()
        {
            List<QbInvoiceTerm> invoiceTermsInQb = FindQbInvoiceTerm();
            List<QbInvoiceTerm> invoiceTermsInDb = QbInvoiceTerm.Find();

            foreach (QbInvoiceTerm invoiceTermInQb in invoiceTermsInQb)
            {
                QbInvoiceTerm invoiceTermInDb = invoiceTermsInDb.Find(delegate(QbInvoiceTerm tempInvoiceTerm)
                    { return tempInvoiceTerm.ListId == invoiceTermInQb.ListId; });

                if (invoiceTermInDb == null)
                    QbInvoiceTerm.Insert(invoiceTermInQb);
                else if (invoiceTermInQb.EditSequence != invoiceTermInDb.EditSequence)
                    QbInvoiceTerm.Update(invoiceTermInQb);
            }
        }

        private List<QbInvoiceTerm> FindQbInvoiceTerm()
        {

            string request = BuildGetAllQueryRqXml("TermsQueryRq");
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseTermQueryResponse(response);
        }

        private List<QbInvoiceTerm> ParseTermQueryResponse(string xml)
        {
            List<QbInvoiceTerm> result = new List<QbInvoiceTerm>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("TermsQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing TermsQueryRs");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList retList = responseNode.SelectNodes("//StandardTermsRet");

            foreach (XmlNode node in retList)
            {
                QbInvoiceTerm term = new QbInvoiceTerm();

                term.IsActive = Convert.ToBoolean(node.SelectSingleNode("./IsActive ").InnerText);
                term.IsDateDriven = false;
                term.ListId = node.SelectSingleNode("./ListID").InnerText;
                term.Name = node.SelectSingleNode("./Name").InnerText;
                term.TimeCreated = DateTime.Parse(node.SelectSingleNode("./TimeCreated").InnerText);
                term.TimeModified = DateTime.Parse(node.SelectSingleNode("./TimeModified").InnerText);
                term.EditSequence = node.SelectSingleNode("./EditSequence").InnerText;
                term.StdDueDays = Convert.ToInt32(node.SelectSingleNode("./StdDueDays").InnerText);
                term.StdDiscountDays = Convert.ToInt32(node.SelectSingleNode("./StdDiscountDays").InnerText);
                term.DiscountPct = Convert.ToDecimal(node.SelectSingleNode("./DiscountPct ").InnerText);

                result.Add(term);
            }

           retList = responseNode.SelectNodes("//DateDrivenTermsRet");

            foreach (XmlNode node in retList)
            {
                QbInvoiceTerm term = new QbInvoiceTerm();

                term.IsActive = Convert.ToBoolean(node.SelectSingleNode("./IsActive ").InnerText);
                term.IsDateDriven = true;
                term.ListId = node.SelectSingleNode("./ListID").InnerText;
                term.Name = node.SelectSingleNode("./Name").InnerText;
                term.TimeCreated = DateTime.Parse(node.SelectSingleNode("./TimeCreated").InnerText);
                term.TimeModified = DateTime.Parse(node.SelectSingleNode("./TimeModified").InnerText);
                term.EditSequence = node.SelectSingleNode("./EditSequence").InnerText;
                term.DiscountPct = Convert.ToDecimal(node.SelectSingleNode("./DiscountPct ").InnerText);
                term.DayOfMonthDue = Convert.ToInt32(node.SelectSingleNode("./DayOfMonthDue").InnerText);
                term.DueNextMonthDays = Convert.ToInt32(node.SelectSingleNode("./DueNextMonthDays").InnerText);
                term.DiscountDayOfMonth = Convert.ToInt32(node.SelectSingleNode("./DiscountDayOfMonth").InnerText);

                result.Add(term);
            }

            return result;
        }

        #endregion 

        #region Entity Query

        private List<QbEntityQueryResponse> FindQbEntities(List<string> fullNames)
        {
            string qbXmlRequest = BuildEntityQueryRqXml(fullNames);
            string qbXmlResponse = m_requestProcessor.ProcessRequest(m_ticket, qbXmlRequest);
            return ParseEntityQueryResponse(qbXmlResponse);
        }

        private string BuildEntityQueryRqXml(List<string> fullNames)
        {
            string xml = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement entityQueryRq = xmlDoc.CreateElement("EntityQueryRq");
            qbXMLMsgsRq.AppendChild(entityQueryRq);

            foreach (string fullName in fullNames)
            {
                AddValue(xmlDoc, entityQueryRq, true, "FullName", fullName);  
            }

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private List<QbEntityQueryResponse> ParseEntityQueryResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            List<QbEntityQueryResponse> result = new List<QbEntityQueryResponse>();

            XmlNodeList customerQueryRsList = responseXmlDoc.GetElementsByTagName("EntityQueryRs");
            if (customerQueryRsList.Count != 1)
                throw new QbException("Invalid response, missing EntityQueryRs");

            XmlNode responseNode = customerQueryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList employeeRetList = responseNode.SelectNodes("//EmployeeRet");

            XmlNodeList otherRetList = responseNode.SelectNodes("//OtherNameRet");

            XmlNodeList vendorRetList = responseNode.SelectNodes("//VendorRet");

            foreach (XmlNode retNode in employeeRetList)
            {
                QbEntityQueryResponse response = new QbEntityQueryResponse();
                response.QbSalesRepType = QbSalesRepTypeEnum.Employee;
                response.ListId = retNode.SelectSingleNode("./ListID").InnerText;
                response.FirstName = GetOptionalSingleNodeText(retNode, "./FirstName");
                response.LastName =  GetOptionalSingleNodeText(retNode, "./LastName");
                result.Add(response);
            }

            foreach (XmlNode retNode in otherRetList)
            {
                QbEntityQueryResponse response = new QbEntityQueryResponse();
                response.QbSalesRepType = QbSalesRepTypeEnum.Other;
                response.ListId = retNode.SelectSingleNode("./ListID").InnerText;
                response.FirstName = GetOptionalSingleNodeText(retNode, "./FirstName");
                response.LastName = GetOptionalSingleNodeText(retNode, "./LastName");
                result.Add(response);
            }

            foreach (XmlNode retNode in vendorRetList)
            {
                QbEntityQueryResponse response = new QbEntityQueryResponse();
                response.QbSalesRepType = QbSalesRepTypeEnum.Vendor;
                response.ListId = retNode.SelectSingleNode("./ListID").InnerText;
                response.FirstName = GetOptionalSingleNodeText(retNode, "./FirstName");
                response.LastName = GetOptionalSingleNodeText(retNode, "./LastName");
                result.Add(response);
            }

            return result;
        }

        #endregion

        #region QbSalesRep

        public void RefreshSalesReps()
        {
            List<QbSalesRep> salesRepsInQb = FindQbSalesRep();

            List<string> fullNames = new List<string>();
            foreach (QbSalesRep qbSalesRep in salesRepsInQb)
            {
                fullNames.Add(qbSalesRep.FullName);
            }

            List<QbEntityQueryResponse> entityQueryResponses =  FindQbEntities(fullNames);

            foreach (QbSalesRep qbSalesRep in salesRepsInQb)
            {
                QbEntityQueryResponse entityQueryResponse = entityQueryResponses.Find(
                    delegate(QbEntityQueryResponse temp) { return temp.ListId == qbSalesRep.RefListId; }
                    );

                if (entityQueryResponse != null)
                {
                    qbSalesRep.QbSalesRepTypeId = (int)entityQueryResponse.QbSalesRepType;
                    qbSalesRep.FirstName = entityQueryResponse.FirstName;
                    qbSalesRep.LastName = entityQueryResponse.LastName;
                }
            }


            List<QbSalesRep> salesRepsInDb = QbSalesRep.Find();

            foreach (QbSalesRep salesRepInQb in salesRepsInQb)
            {
                int idx = salesRepsInDb.FindIndex(delegate(QbSalesRep tempQbSalesRep)
                    { return tempQbSalesRep.ListId == salesRepInQb.ListId; });

                if(idx >=0)
                {
                    salesRepInQb.EmployeeId = salesRepsInDb[idx].EmployeeId;
                    salesRepInQb.QbCustomerTypeListId = salesRepsInDb[idx].QbCustomerTypeListId;
                    QbSalesRep.Update(salesRepInQb);
                    salesRepsInDb.RemoveAt(idx);
                }
                else
                    QbSalesRep.Insert(salesRepInQb);    
            }

            foreach (QbSalesRep salesRep in salesRepsInDb)
            {
                salesRep.IsActive = false;
                QbSalesRep.Update(salesRep);
            }
        }

        private List<QbSalesRep> FindQbSalesRep()
        {
            string request = BuildGetAllQueryRqXml("SalesRepQueryRq");
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseSalesRepQueryResponse(response);
        }

        private List<QbSalesRep> ParseSalesRepQueryResponse(string xml)
        {
            List<QbSalesRep> result = new List<QbSalesRep>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("SalesRepQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing SalesRepQueryRs ");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList retList = responseNode.SelectNodes("//SalesRepRet");

            foreach (XmlNode node in retList)
            {
                QbSalesRep salesRep = new QbSalesRep();

                salesRep.ListId = node.SelectSingleNode("./ListID").InnerText;
                salesRep.TimeCreated = DateTime.Parse(node.SelectSingleNode("./TimeCreated").InnerText);
                salesRep.TimeModified = DateTime.Parse(node.SelectSingleNode("./TimeModified").InnerText);
                salesRep.EditSequence = node.SelectSingleNode("./EditSequence").InnerText;
                salesRep.Initial = node.SelectSingleNode("./Initial").InnerText;
                salesRep.IsActive = Convert.ToBoolean(node.SelectSingleNode("./IsActive ").InnerText);

                XmlNode entityRefListId = node.SelectSingleNode("./SalesRepEntityRef/ListID");
                if (entityRefListId != null)
                    salesRep.RefListId = entityRefListId.InnerText;

                XmlNode entityRefFullName = node.SelectSingleNode("./SalesRepEntityRef/FullName");
                if (entityRefFullName != null)
                    salesRep.FullName = entityRefFullName.InnerText;

                result.Add(salesRep);
            }

            return result;
        }

        #endregion

        #region QbCustomer

        public void SyncMissingCustomers(int daysMissing, QBSyncLog log)
        {
            Host.QuickBooksDebug("QbSync:SyncQbCustomers", "Started");
            ReportStatus("Sync Customers", "STARTED", "Started Syncronization of customers");

            List<QbCustomer> modifiedQbCustomers = FindModifiedQbCustomers(DateTime.Now.AddDays(-daysMissing));

            foreach (QbCustomer modifiedCustomerInQb in modifiedQbCustomers)
            {
                QbCustomer qbCustomer;
                
                try
                {
                    qbCustomer = QbCustomer.FindByListId(modifiedCustomerInQb.ListId, null);
                }
                catch (DataNotFoundException)
                {
                    Host.QuickBooksDebug("QbSync:SyncronizeData", "Customer :" + modifiedCustomerInQb.FullName + " Not found in the system.  List ID:" + modifiedCustomerInQb.ListId);
                    continue;
                }
            }
        }

        public void SyncQbCustomers(DateTime fromModifiedDate, QBSyncLog log)
        {
            Host.QuickBooksDebug("QbSync:SyncQbCustomers", "Started");
            ReportStatus("Sync Customers", "STARTED", "Started Syncronization of customers");

            List<QbCustomer> modifiedQbCustomers = FindModifiedQbCustomers(fromModifiedDate);

            foreach (QbCustomer modifiedCustomerInQb in modifiedQbCustomers)
            {
                Host.QuickBooksDebug("QbSync:SyncronizeData", "Process Customer :" + modifiedCustomerInQb.FullName);

                QbCustomer qbCustomer;
                
                try
                {
                    qbCustomer = QbCustomer.FindByListId(modifiedCustomerInQb.ListId, null);
                }
                catch (DataNotFoundException)
                {
                    Host.QuickBooksDebug("QbSync:SyncronizeData", "Customer :" + modifiedCustomerInQb.FullName + " Not found in the system");
                    continue;
                }

                try
                {
                    if (string.IsNullOrEmpty(qbCustomer.ListId))
                    {
                        Host.QuickBooksDebug("QbSync:SyncronizeData", "Customer :" + modifiedCustomerInQb.FullName + " Not added yet");
                        continue;
                    }

                    if (qbCustomer.EditSequence != modifiedCustomerInQb.EditSequence || qbCustomer.Balance != modifiedCustomerInQb.Balance)
                    {
                        Host.Trace("QbSync:SyncronizeData", "Updating customer:" + qbCustomer.FullName + " balance " + qbCustomer.Balance);
                        modifiedCustomerInQb.InitializeNonQbFields(qbCustomer);
                        QbCustomer.Update(modifiedCustomerInQb);

                        if (modifiedCustomerInQb.ProjectInsurance != null && modifiedCustomerInQb.ProjectId.HasValue) 
                        {
                            ProjectInsurance projectInsuranceInDb = null;
                            modifiedCustomerInQb.ProjectInsurance.ProjectId = qbCustomer.ProjectId.Value;
                            try
                            {
                                projectInsuranceInDb = ProjectInsurance.FindByPrimaryKey(modifiedCustomerInQb.ProjectId.Value, null);
                                ProjectInsurance.Update(modifiedCustomerInQb.ProjectInsurance);
                            }
                            catch (DataNotFoundException){ }

                            if (projectInsuranceInDb == null)
                                ProjectInsurance.Insert(modifiedCustomerInQb.ProjectInsurance);
                        }

                        ReportStatus("Updated Customer", "OK", "Customer: " + qbCustomer.FullName + " Balance" + qbCustomer.Balance);

                        QbSyncLogDetail detail = new QbSyncLogDetail();
                        detail.CompletedDate = DateTime.Now;
                        detail.IsSuccess = true;
                        detail.QbCustomerId = qbCustomer.ID;
                        detail.QbSyncActionId = (int)QbSyncActionEnum.CustomerMod;
                        detail.QbSyncLogId = log.ID;
                        QbSyncLogDetail.Insert(detail);
                    }
                }
                catch (Exception ex)
                {

                    ReportStatus("Updated Customer", "Fail", "Customer: " + qbCustomer.FullName + " Balance" + qbCustomer.Balance);

                    QbSyncLogDetail detail = new QbSyncLogDetail();
                    detail.CompletedDate = DateTime.Now;
                    detail.IsSuccess = false;
                    detail.QbCustomerId = qbCustomer.ID;
                    detail.QbSyncActionId = (int)QbSyncActionEnum.CustomerMod;
                    detail.QbSyncLogId = log.ID;
                    detail.ErrorMessage = ex.ToString().Substring(0,1024);
                    QbSyncLogDetail.Insert(detail);
                    throw ex;
                }
            }

            ReportStatus("Sync Customers", "COMPLETED", "Completed Syncronization of customers");
        }

        public List<QbCustomer> FindQbCustomers(string name, string[] listIds)
        {
            string request = string.Empty;

            if (!string.IsNullOrEmpty(name))
                request = BuildCustomerQueryRqXml(null, null, name);
            else if (listIds != null && listIds.Length > 0)
                request = BuildCustomerQueryRqXml(listIds, null, null);
            else
                request = BuildCustomerQueryRqXml(null, null, null);
 
            Host.QuickBooksDebug("FindQbCustomerByListId", request);
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            Host.QuickBooksDebug("FindModifiedQbCustomers", response);
            List<QbCustomer> qbCustomers = ParseCustomerQueryResponse(response);
            return qbCustomers;
        }

        public List<QbCustomer> FindModifiedQbCustomers(DateTime fromModifiedDate)
        {
            string request = BuildCustomerQueryRqXml(null, fromModifiedDate);
            Host.QuickBooksDebug("FindModifiedQbCustomers", request);

           string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            Host.QuickBooksDebug("FindModifiedQbCustomers", response);

            List<QbCustomer> qbCustomers = ParseCustomerQueryResponse(response);

            return qbCustomers;
        }

        public void CreateQbCustomer (ref QbCustomer qbCustomer, QbCustomer parentQbCustomer, out string qbXmlRequest, out string qbXmlResponse)
        {
            Host.QuickBooksDebug("QbGateway:CreateQbCustomer", "Start:   qbCustomer:" + qbCustomer.Name + " id=" + qbCustomer.ID);

            string qbSalesRepListId = qbCustomer.QbSalesRepListId;

            qbXmlRequest = BuildCustomerAddRqXml(qbCustomer, parentQbCustomer);
            Host.QuickBooksDebug("QbGateway:CreateQbCustomer", qbXmlRequest);

            qbXmlResponse = m_requestProcessor.ProcessRequest(m_ticket, qbXmlRequest);
            Host.QuickBooksDebug("QbGateway:CreateQbCustomer", qbXmlResponse);

            QbCustomer qbCustomerResponse = ParseCustomerAddResponse(qbXmlResponse);
            qbCustomerResponse.InitializeNonQbFields(qbCustomer);
            qbCustomer = qbCustomerResponse;

            if(string.IsNullOrEmpty(qbCustomer.QbSalesRepListId))
            {
                // QB does not always save qbSalesRepListId.  Need to preserve it in our db
                qbCustomer.QbSalesRepListId = qbSalesRepListId;
            }

            if (qbCustomer.ProjectId.HasValue)
            {
                try
                {
                    qbCustomer.ProjectInsurance = ProjectInsurance.FindByPrimaryKey(qbCustomer.ProjectId.Value);
                    AddProjectInsurance(qbCustomer.ProjectInsurance, qbCustomer);
                }
                catch (DataNotFoundException ex)
                { }
            }
        }

        public List<QbCustomer> FindSimilarCustomers (QbCustomer basedOnQbCustomer)
        {
            string qbxmlRequestCustomer = BuildCustomerQueryRqXml(null, null, basedOnQbCustomer.Name);
            string qbXmlResponseCustomer = m_requestProcessor.ProcessRequest(m_ticket, qbxmlRequestCustomer);
            List<QbCustomer> similarQbCustomers = ParseCustomerQueryResponse(qbXmlResponseCustomer);

            return similarQbCustomers;
        }

        public void ModifyQbCustomer(ref QbCustomer qbCustomer, QbCustomer parentQbCustomer, out string qbXmlRequest, out string qbXmlResponse)
        {
            qbXmlRequest = BuildCustomerModRqXml(qbCustomer, parentQbCustomer);
            qbXmlResponse = m_requestProcessor.ProcessRequest(m_ticket, qbXmlRequest);

            QbCustomer qbCustomerResponse = ParseCustomerModResponse(qbXmlResponse);
            qbCustomerResponse.InitializeNonQbFields(qbCustomer);
            qbCustomer = qbCustomerResponse;
        }

        private string BuildCustomerQueryRqXml(string[] listIds, DateTime? fromModifiedDate)
        {
            return BuildCustomerQueryRqXml(listIds, fromModifiedDate, null);
        }

        private string BuildCustomerQueryRqXml(string[] listIds, DateTime? fromModifiedDate, string nameFilter)
        {
            string xml = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement customerQueryRq = xmlDoc.CreateElement("CustomerQueryRq");
            qbXMLMsgsRq.AppendChild(customerQueryRq);
            customerQueryRq.SetAttribute("requestID", "1");

            if (listIds != null && listIds.Length > 0)
            {
                foreach (string listId in listIds)
                {
                    AddValue(xmlDoc, customerQueryRq, true, "ListID", listId);
                }
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                XmlElement nameFilterElement = xmlDoc.CreateElement("NameFilter");
                customerQueryRq.AppendChild(nameFilterElement);

                AddValue(xmlDoc, nameFilterElement, true, "MatchCriterion", "Contains");
                AddValue(xmlDoc, nameFilterElement, true, "Name", nameFilter);
            }

            if (fromModifiedDate.HasValue)
                AddValue(xmlDoc, customerQueryRq, true, "FromModifiedDate", fromModifiedDate.Value.ToString("yyyy-MM-ddTHH:mm:ss-06:00"));

            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "ListID");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "TimeCreated");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "TimeModified");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "EditSequence");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "Name");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "FullName");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "IsActive");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "Sublevel");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "FirstName");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "LastName");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "BillAddress");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "BillAddressBlock");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "BillAddressBlock");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "ShipAddress");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "ShipAddressBlock");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "Phone");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "AltPhone");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "Email");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "Balance");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "TotalBalance");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "TotalBalance");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "SalesTaxCodeRef");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "ItemSalesTaxRef");
            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "JobStatus");

            AddValue(xmlDoc, customerQueryRq, true, "IncludeRetElement", "DataExtRet");
            AddValue(xmlDoc, customerQueryRq, true, "OwnerID", "0");

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private string BuildCustomerModRqXml(QbCustomer qbCustomer, QbCustomer parentQbCustomer)
        {
            string xml = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement customerAddRq = xmlDoc.CreateElement("CustomerModRq");
            qbXMLMsgsRq.AppendChild(customerAddRq);
            customerAddRq.SetAttribute("requestID", "1");

            XmlElement customerMod = xmlDoc.CreateElement("CustomerMod");
            customerAddRq.AppendChild(customerMod);

            BuildCustomerRqXml(xmlDoc,qbCustomer, parentQbCustomer, customerMod);

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private string BuildCustomerAddRqXml(QbCustomer qbCustomer, QbCustomer parentQbCustomer)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement customerAddRq = xmlDoc.CreateElement("CustomerAddRq");
            qbXMLMsgsRq.AppendChild(customerAddRq);
            customerAddRq.SetAttribute("requestID", "1");

            XmlElement customerAdd = xmlDoc.CreateElement("CustomerAdd");
            customerAddRq.AppendChild(customerAdd);

            BuildCustomerRqXml(xmlDoc,qbCustomer,parentQbCustomer, customerAdd);
                
            xml = xmlDoc.OuterXml;
            return xml;
        }

        private void BuildCustomerRqXml(XmlDocument xmlDoc, QbCustomer qbCustomer, QbCustomer parentQbCustomer,  XmlElement customerCommand)
        {
            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.ListId), "ListID", qbCustomer.ListId);
            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.ListId), "EditSequence", qbCustomer.EditSequence);
            
            AddValue(xmlDoc, customerCommand, true, "Name", qbCustomer.Name);
            AddValue(xmlDoc, customerCommand, true, "IsActive", qbCustomer.IsActive ? "true" : "false");

            if (parentQbCustomer != null)
                AddRef(xmlDoc, customerCommand, "ParentRef", parentQbCustomer.ListId);

            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.FirstName), "FirstName", qbCustomer.FirstName);
            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.LastName), "LastName", qbCustomer.LastName);
            
            if (!string.IsNullOrEmpty(qbCustomer.BillingAddressAddr1))
            {
                XmlElement address = xmlDoc.CreateElement("BillAddress");
                customerCommand.AppendChild(address);

                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.FirstLastName), "Addr1", qbCustomer.FirstLastName);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressAddr1), "Addr2", qbCustomer.BillingAddressAddr1);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressAddr2), "Addr3", qbCustomer.BillingAddressAddr2);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressCity), "City", qbCustomer.BillingAddressCity);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressState), "State", qbCustomer.BillingAddressState);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressPostalCode), "PostalCode", qbCustomer.BillingAddressPostalCode);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressCountry), "Country", qbCustomer.BillingAddressCountry);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.BillingAddressNote), "Note", qbCustomer.BillingAddressNote);
            }

            if (!string.IsNullOrEmpty(qbCustomer.ShippingAddressAddr1))
            {
                XmlElement address = xmlDoc.CreateElement("ShipAddress");
                customerCommand.AppendChild(address);

                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.FirstLastName), "Addr1", qbCustomer.FirstLastName);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressAddr1), "Addr2", qbCustomer.ShippingAddressAddr1);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressAddr2), "Addr3", qbCustomer.ShippingAddressAddr2);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressCity), "City", qbCustomer.ShippingAddressCity);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressState), "State", qbCustomer.ShippingAddressState);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressPostalCode), "PostalCode", qbCustomer.ShippingAddressPostalCode);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressCountry), "Country", qbCustomer.ShippingAddressCountry);
                AddValue(xmlDoc, address, !string.IsNullOrEmpty(qbCustomer.ShippingAddressNote), "Note", qbCustomer.ShippingAddressNote);
            }

            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.Phone1), "Phone", qbCustomer.Phone1);
            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.Phone2), "AltPhone", qbCustomer.Phone2);
            AddValue(xmlDoc, customerCommand, !string.IsNullOrEmpty(qbCustomer.Email), "Email", qbCustomer.Email);

            if (!string.IsNullOrEmpty(qbCustomer.QbCustomerTypeListId))
                AddRef(xmlDoc, customerCommand, "CustomerTypeRef", qbCustomer.QbCustomerTypeListId);
            if (!string.IsNullOrEmpty(qbCustomer.QbSalesRepListId))
                AddRef(xmlDoc, customerCommand, "SalesRepRef", qbCustomer.QbSalesRepListId);
        }

        private QbCustomer  ParseCustomerAddResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList customerAddRsList = responseXmlDoc.GetElementsByTagName("CustomerAddRs");
            if (customerAddRsList.Count != 1)
                throw new QbException("Invalid response, missing CustomerAddRs");

            XmlNode responseNode = customerAddRsList.Item(0);

            IsQbResponseAvailable(responseNode);

            List<QbCustomer> customers = ParseCustomerRet(responseNode);

            if (customers.Count == 0)
                throw new QbException("No QbCustomer found");

            return customers[0];
        }

        private QbCustomer ParseCustomerModResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList customerAddRsList = responseXmlDoc.GetElementsByTagName("CustomerModRs");
            if (customerAddRsList.Count != 1)
                throw new QbException("Invalid response, missing CustomerModRs");

            XmlNode responseNode = customerAddRsList.Item(0);

            IsQbResponseAvailable(responseNode);

            List<QbCustomer> customers = ParseCustomerRet(responseNode);
            if (customers.Count == 0)
                throw new QbException("No QbCustomer found");

            return customers[0];
        }

        private List<QbCustomer> ParseCustomerQueryResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList customerAddRsList = responseXmlDoc.GetElementsByTagName("CustomerQueryRs");
            if (customerAddRsList.Count != 1)
                throw new QbException("Invalid response, missing CustomerQueryRs");

            XmlNode responseNode = customerAddRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return new List<QbCustomer>();

            List<QbCustomer> customers = ParseCustomerRet(responseNode);

            return customers;
        }

        private List<QbCustomer> ParseCustomerRet(XmlNode responseNode)
        {
            XmlNodeList customerRetRetList = responseNode.SelectNodes("//CustomerRet");

            List<QbCustomer> customers = new List<QbCustomer>();

            foreach (XmlNode customerNode in customerRetRetList)
            {
                QbCustomer qbCustomer = new QbCustomer();

                qbCustomer.ListId = customerNode.SelectSingleNode("./ListID").InnerText;
                qbCustomer.TimeCreated = DateTime.Parse(customerNode.SelectSingleNode("./TimeCreated").InnerText);
                qbCustomer.TimeModified = DateTime.Parse(customerNode.SelectSingleNode("./TimeModified").InnerText);
                qbCustomer.EditSequence = customerNode.SelectSingleNode("./EditSequence").InnerText;
                qbCustomer.Name = customerNode.SelectSingleNode("./Name").InnerText;
                qbCustomer.FullName = customerNode.SelectSingleNode("./FullName").InnerText;
                qbCustomer.IsActive = Convert.ToBoolean(customerNode.SelectSingleNode("./IsActive ").InnerText);
                qbCustomer.SubLevel = Convert.ToInt32(customerNode.SelectSingleNode("./Sublevel").InnerText);

                qbCustomer.CompanyName = GetOptionalSingleNodeText(customerNode, "./CompanyName");
                qbCustomer.FirstName = GetOptionalSingleNodeText(customerNode, "./FirstName");
                qbCustomer.LastName = GetOptionalSingleNodeText(customerNode, "./LastName");
                qbCustomer.BillingAddressAddr1 = GetOptionalSingleNodeText(customerNode, "./BillAddress/Addr2");
                qbCustomer.BillingAddressAddr2 = GetOptionalSingleNodeText(customerNode, "./BillAddress/Addr3");
                qbCustomer.BillingAddressCity = GetOptionalSingleNodeText(customerNode, "./BillAddress/City");
                qbCustomer.BillingAddressState = GetOptionalSingleNodeText(customerNode, "./BillAddress/State");
                qbCustomer.BillingAddressPostalCode = GetOptionalSingleNodeText(customerNode, "./BillAddress/PostalCode");

                qbCustomer.ShippingAddressAddr1 = GetOptionalSingleNodeText(customerNode, "./ShipAddress/Addr2");
                qbCustomer.ShippingAddressAddr2 = GetOptionalSingleNodeText(customerNode, "./ShipAddress/Addr3");
                qbCustomer.ShippingAddressCity = GetOptionalSingleNodeText(customerNode, "./ShipAddress/City");
                qbCustomer.ShippingAddressState = GetOptionalSingleNodeText(customerNode, "./ShipAddress/State");
                qbCustomer.ShippingAddressPostalCode = GetOptionalSingleNodeText(customerNode, "./ShipAddress/PostalCode");

                qbCustomer.Phone1 = GetOptionalSingleNodeText(customerNode, "./Phone");
                if(qbCustomer.Phone1 != null)
                    qbCustomer.Phone1 = Regex.Replace(qbCustomer.Phone1, "[^0-9]", "");

                qbCustomer.Phone2 = GetOptionalSingleNodeText(customerNode, "./AltPhone");
                if (qbCustomer.Phone2 != null)
                    qbCustomer.Phone2 = Regex.Replace(qbCustomer.Phone2, "[^0-9]", "");

                qbCustomer.Email = GetOptionalSingleNodeText(customerNode, "./Email");

                qbCustomer.Balance = Convert.ToDecimal(GetOptionalSingleNodeText(customerNode, "./TotalBalance"));
                if (qbCustomer.Balance == 0)
                    qbCustomer.Balance = Convert.ToDecimal(GetOptionalSingleNodeText(customerNode, "./BalanceRemaining"));

                qbCustomer.QbCustomerTypeListId = GetOptionalSingleNodeText(customerNode, "./CustomerTypeRef/ListID");
                qbCustomer.QbSalesRepListId = GetOptionalSingleNodeText(customerNode, "./SalesRepRef/ListID");

                XmlNodeList dataExtRetList = customerNode.SelectNodes("./DataExtRet");
                if (dataExtRetList.Count > 0)
                {
                    var projectInsurance = new ProjectInsurance();

                    foreach (XmlNode dataExtList in dataExtRetList)
                    {
                        var dataExtName = dataExtList.SelectSingleNode("./DataExtName").InnerText;
                        var dataExtValue = dataExtList.SelectSingleNode("./DataExtValue").InnerText.Trim();
                        
                        switch (dataExtName)
                        {
                            case "Insurance":
                                projectInsurance.Company = dataExtValue;
                                break;
                            case "Address":
                                projectInsurance.Address1 = dataExtValue;
                                break;
                            case "City, State, Zip":
                                projectInsurance.Address2 = dataExtValue;
                                break;
                            case "Contact":
                                projectInsurance.Contact = dataExtValue;
                                break;
                            case "Phone":
                                projectInsurance.Phone = dataExtValue;
                                break;
                            case "Fax":
                                projectInsurance.Fax = dataExtValue;
                                break;
                            case "Claim#":
                                projectInsurance.ClaimNumber = dataExtValue;
                                break;
                        }
                    }

                    if (projectInsurance.IsFilled())
                    {
                        qbCustomer.ProjectInsurance = projectInsurance;
                    }
                }
                customers.Add(qbCustomer);
            }   

            return customers;
        }

        #endregion 

        #region QbCustomerType

        public void RefreshCustomerTypes()
        {
            List<QbCustomerType> customerTypeListInQb = FindQbCustomerTypes();
            List<QbCustomerType> customerTypeListInDb = QbCustomerType.Find();

            foreach (QbCustomerType customerTypeInQb in customerTypeListInQb)
            {
                QbCustomerType customerTypeInDb = customerTypeListInDb.Find(
                    delegate(QbCustomerType tempQbCustomerType)
                    { return tempQbCustomerType.ListId == customerTypeInQb.ListId; });

                if (customerTypeInDb == null)
                    QbCustomerType.Insert(customerTypeInQb);

                else
                {
                    if (customerTypeInDb.EditSequence != customerTypeInQb.EditSequence)
                        QbCustomerType.Update(customerTypeInQb);

                    customerTypeListInDb.Remove(customerTypeInDb);
                }
            }

            foreach (QbCustomerType customerTypeInDb in customerTypeListInDb)
            {
                customerTypeInDb.IsActive = false;
                QbCustomerType.Update(customerTypeInDb);
            }

        }

        private List<QbCustomerType> FindQbCustomerTypes()
        {
            string request = BuildGetAllQueryRqXml("CustomerTypeQueryRq");
            string response = m_requestProcessor.ProcessRequest(m_ticket, request);
            return ParseCustomerTypeQueryResponse(response);
        }

        private List<QbCustomerType> ParseCustomerTypeQueryResponse(string xml)
        {
            List<QbCustomerType> result = new List<QbCustomerType>();

            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("CustomerTypeQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing ClassQueryRs");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList retList = responseNode.SelectNodes("//CustomerTypeRet");

            foreach (XmlNode classRetNode in retList)
            {
                QbCustomerType qbCustomerType = new QbCustomerType();

                qbCustomerType.ListId = classRetNode.SelectSingleNode("./ListID").InnerText;
                qbCustomerType.TimeCreated = DateTime.Parse(classRetNode.SelectSingleNode("./TimeCreated").InnerText);
                qbCustomerType.TimeModified = DateTime.Parse(classRetNode.SelectSingleNode("./TimeModified").InnerText);
                qbCustomerType.EditSequence = classRetNode.SelectSingleNode("./EditSequence").InnerText;

                qbCustomerType.FullName = classRetNode.SelectSingleNode("./FullName").InnerText;
                qbCustomerType.Name = classRetNode.SelectSingleNode("./Name").InnerText;
                qbCustomerType.IsActive = Convert.ToBoolean(classRetNode.SelectSingleNode("./IsActive ").InnerText);
                qbCustomerType.SubLevel = Convert.ToInt32(classRetNode.SelectSingleNode("./Sublevel").InnerText);

                XmlNode parentRefNode = classRetNode.SelectSingleNode("./ParentRef");
                if (parentRefNode != null)
                {
                    qbCustomerType.ParentRefListId = parentRefNode.SelectSingleNode("./ListID").InnerText;
                }

                result.Add(qbCustomerType);
            }

            return result;
        }

        #endregion

        #region QbInvoice

        public void SyncInvoices(DateTime fromModifiedDate, QBSyncLog log)
        {
            Host.QuickBooksDebug("QbSync:SyncInvoices", "Started");
            ReportStatus("Sync Invoices", "STARTED", "Started Syncronization of invoices");

            List<QbInvoice> qbInvoices = FindQbInvoices(fromModifiedDate);

            foreach (QbInvoice invoiceInQb in qbInvoices)
            {
                Host.QuickBooksDebug("QbSync:SyncExisitngInvoices", "Processing invoice TxnID" + invoiceInQb.TxnID + "Customer " + invoiceInQb.CustomerRefListId);
                QbInvoice invoiceInDb;

                QbCustomer qbProject;
                QbCustomer qbCustomer;

                try
                {
                    qbProject = QbCustomer.FindByListId(invoiceInQb.CustomerRefListId, null);
                    qbCustomer = QbCustomer.FindParent(qbProject.CustomerId, null);

                }
                catch (DataNotFoundException)
                {
                    Host.QuickBooksDebug("QbSync:SyncInvoices", "Project " + invoiceInQb.CustomerRefListId + " not found in db");
                    continue;
                }

                try
                {
                    invoiceInDb = QbInvoice.FindByTxnId(invoiceInQb.TxnID, null);
                    Host.QuickBooksDebug("QbSync:SyncInvoices", "Processing invoice TxnID" + invoiceInQb.TxnID + " found in db");
                }
                catch (DataNotFoundException)
                {
                    invoiceInQb.QbCustomerId = qbProject.ID;
                    invoiceInQb.CreatedDate = DateTime.Now;
                    invoiceInQb.CalculateTotals();
                    QbInvoice.Insert(invoiceInQb);
                    foreach (QbInvoiceLine line in invoiceInQb.QbInvoiceLines)
                    {
                        line.QbInvoiceId = invoiceInQb.ID;
                        QbInvoiceLine.Insert(line);
                    }

                    ReportStatus("New Invoice", "OK", "Customer " + qbCustomer.FullName + " created new invoice " + invoiceInQb.RefNumber + " amount " + invoiceInQb.SubTotalAmount.ToString("c") );

                    QbSyncLogDetail detail = new QbSyncLogDetail();
                    detail.CompletedDate = DateTime.Now;
                    detail.IsSuccess = true;
                    detail.QbSyncActionId = (int)QbSyncActionEnum.InvoiceAdd;
                    detail.QbSyncLogId = log.ID;
                    detail.QbInvoiceId = invoiceInQb.ID;
                    QbSyncLogDetail.Insert(detail);

                    continue;
                }

                if (invoiceInDb.EditSequence == invoiceInQb.EditSequence)
                    continue;

                invoiceInQb.CalculateTotals();
                invoiceInDb.UpdateQbFields(invoiceInQb);
                QbInvoice.Update(invoiceInDb, null);

                ReportStatus("Update Invoice", "OK", "Customer " + qbCustomer.FullName + " created new invoice " + invoiceInQb.RefNumber + " amount " + invoiceInQb.SubTotalAmount.ToString("c"));

                QbSyncLogDetail qbSyncLogDetail = new QbSyncLogDetail();
                qbSyncLogDetail.CompletedDate = DateTime.Now;
                qbSyncLogDetail.IsSuccess = true;

                qbSyncLogDetail.QbSyncActionId = (int)QbSyncActionEnum.InvoiceAdd;
                qbSyncLogDetail.QbSyncLogId = log.ID;
                qbSyncLogDetail.QbInvoiceId = invoiceInDb.ID;
                QbSyncLogDetail.Insert(qbSyncLogDetail);

                List<QbInvoiceLine> invoiceLinesInDb = QbInvoiceLine.FindByInvoiceId(invoiceInDb.ID, null);

                foreach (QbInvoiceLine invoiceLineInQb in invoiceInQb.QbInvoiceLines)
                {
                    Host.QuickBooksDebug("QbSync:SyncInvoices", "invoiceLineInQb.TxnLineID " + invoiceLineInQb.TxnLineID);

                    QbInvoiceLine invoiceLineInDb = invoiceLinesInDb.Find(delegate(QbInvoiceLine tempLine)
                        { return tempLine.TxnLineID == invoiceLineInQb.TxnLineID; });

                    if (invoiceLineInDb == null)
                    {
                        Host.QuickBooksDebug("QbSync:SyncInvoices", "Line Found in Not Found " + invoiceLineInQb.TxnLineID);
                        invoiceLineInQb.QbInvoiceId = invoiceInDb.ID;
                        QbInvoiceLine.Insert(invoiceLineInQb);
                        continue;
                    }
                    
                    invoiceLinesInDb.Remove(invoiceLineInDb);

                    if (!invoiceLineInDb.QbEquals(invoiceLineInQb))
                    {
                        invoiceLineInDb.FillQbFields(invoiceLineInQb);
                        QbInvoiceLine.Update(invoiceLineInDb);
                        Host.QuickBooksDebug("QbSync:SyncInvoices", "Line Updated" + invoiceLineInDb.TxnLineID);
                    }
                }

                foreach (QbInvoiceLine invoiceLine in invoiceLinesInDb)
                {
                    QbInvoiceLine.Delete(invoiceLine);
                }
            }

            ReportStatus("Sync Invoices", "COMPLETED", "Completed Syncronization of invoices");
        }

        public void CreateQbInvoice(QbInvoice qbInvoice, QbCustomer qbProject, out string qbXmlRequest, out string qbXmlResponse)
        {
            qbXmlRequest = BuildInvoiceAddRqXML(qbInvoice, qbProject);
            qbXmlResponse = m_requestProcessor.ProcessRequest(m_ticket, qbXmlRequest);
            ParseInvoiceAddResponse(qbXmlResponse, qbInvoice);
        }

        public List<QbInvoice> FindQbInvoices(DateTime fromModifiedDate)
        {

            string xmlRequest = BuildInvoiceQueryRqXml(fromModifiedDate);
            Host.QuickBooksDebug("QbSync:FindQbInvoices", xmlRequest);

            string response = m_requestProcessor.ProcessRequest(m_ticket, xmlRequest);
            Host.QuickBooksDebug("QbSync:FindQbInvoices", response);

            List<QbInvoice> result = ParseInvoiceQueryResponse(response);

            return result;
        }

        private string BuildInvoiceQueryRqXml(DateTime fromModifiedDate)
        {
            string xml = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement invoiceQueryRq = xmlDoc.CreateElement("InvoiceQueryRq");
            qbXMLMsgsRq.AppendChild(invoiceQueryRq);

            XmlElement modifiedDateRangeFilterElement = xmlDoc.CreateElement("ModifiedDateRangeFilter");
            invoiceQueryRq.AppendChild(modifiedDateRangeFilterElement);

            AddValue(xmlDoc, modifiedDateRangeFilterElement, true, "FromModifiedDate", fromModifiedDate.ToString("yyyy-MM-ddTHH:mm:ss-06:00"));

            XmlElement includeLineItemsElement = xmlDoc.CreateElement("IncludeLineItems");
            invoiceQueryRq.AppendChild(includeLineItemsElement);
            includeLineItemsElement.InnerText = "true";

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private string BuildInvoiceAddRqXML(QbInvoice qbInvoice, QbCustomer qbProject)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement invoiceAddRq = xmlDoc.CreateElement("InvoiceAddRq");
            qbXMLMsgsRq.AppendChild(invoiceAddRq);
            invoiceAddRq.SetAttribute("requestID", "1");

            XmlElement invoiceAdd = xmlDoc.CreateElement("InvoiceAdd");
            invoiceAddRq.AppendChild(invoiceAdd);

            AddRef(xmlDoc, invoiceAdd, "CustomerRef", qbProject.ListId);
 
            if (qbInvoice.QbClassListId != null)
                AddRef(xmlDoc, invoiceAdd, "ClassRef", qbInvoice.QbClassListId);

            if (qbInvoice.QbAccountListId != null)
                AddRef(xmlDoc, invoiceAdd, "ARAccountRef", qbInvoice.QbAccountListId);
                
            if (qbInvoice.QbTemplateListId != null)
                AddRef(xmlDoc, invoiceAdd, "TemplateRef", qbInvoice.QbTemplateListId);

            AddValue(xmlDoc, invoiceAdd, qbInvoice.TxnDate != null, "TxnDate", string.Format("{0:yyyy-MM-dd}", qbInvoice.TxnDate));
            AddValue(xmlDoc, invoiceAdd, qbInvoice.RefNumber != null, "RefNumber", qbInvoice.RefNumber);

            if (qbInvoice.BillingAddressAddr1 != null)
            {
                XmlElement billAddress = xmlDoc.CreateElement("BillAddress");
                invoiceAdd.AppendChild(billAddress);

                AddValue(xmlDoc, billAddress, true, "Addr1", qbInvoice.QbCustomer.FirstLastName);
                AddValue(xmlDoc, billAddress, true, "Addr2", qbInvoice.BillingAddressAddr1);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddressAddr2 != null, "Addr3", qbInvoice.BillingAddressAddr2);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddressCity != null, "City", qbInvoice.BillingAddressCity);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddresState != null, "State", qbInvoice.BillingAddresState);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddresPostalCode != null, "PostalCode", qbInvoice.BillingAddresPostalCode);
            }

            if (qbInvoice.ShipAddressAddr1 != null)
            {
                XmlElement address = xmlDoc.CreateElement("ShipAddress");
                invoiceAdd.AppendChild(address);

                AddValue(xmlDoc, address, true, "Addr1", qbInvoice.QbCustomer.FirstLastName);
                AddValue(xmlDoc, address, true, "Addr2", qbInvoice.ShipAddressAddr1);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressAddr2 != null, "Addr3", qbInvoice.ShipAddressAddr2);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressCity != null, "City", qbInvoice.ShipAddressCity);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressState != null, "State", qbInvoice.ShipAddressState);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressPostalCode != null, "PostalCode", qbInvoice.ShipAddressPostalCode);
            }

            AddValue(xmlDoc, invoiceAdd, true, "IsPending", qbInvoice.IsPending.ToString().ToLower());

            if (qbInvoice.QbInvoiceTermListId != null)
                AddRef(xmlDoc, invoiceAdd, "TermsRef", qbInvoice.QbInvoiceTermListId);
            
            if (qbInvoice.QbSalesRepRefListId != null)
                AddRef(xmlDoc, invoiceAdd, "SalesRepRef", qbInvoice.QbSalesRepRefListId);

            if (qbInvoice.ItemSalesTaxRef != null)
                AddRef(xmlDoc, invoiceAdd, "ItemSalesTaxRef",qbInvoice.ItemSalesTaxRef);

            foreach (QbInvoiceLine line in qbInvoice.QbInvoiceLines)
            {
                QbItem qbItem = QbItem.FindByPrimaryKey(line.QbItemListId);

                XmlElement invoiceLineAdd = xmlDoc.CreateElement("InvoiceLineAdd");
                invoiceAdd.AppendChild(invoiceLineAdd);

                AddRef(xmlDoc,invoiceLineAdd, "ItemRef", line.QbItemListId);
                AddValue(xmlDoc, invoiceLineAdd, line.Description !=null, "Desc", line.Description);

                if (qbItem.QbItemTypeId != (int) QbItemTypeEnum.Discount)
                    AddValue(xmlDoc, invoiceLineAdd, true, "Quantity", line.Quantity.ToString());

                AddValue(xmlDoc, invoiceLineAdd, true, "Rate", line.Rate.ToString("F2"));
                AddRef(xmlDoc, invoiceLineAdd, "ClassRef", line.QbClassListId);
                AddValue(xmlDoc, invoiceLineAdd, true, "Amount", line.Amount.ToString("F2"));
                AddRef(xmlDoc, invoiceLineAdd, "SalesTaxCodeRef", line.QbSalesTaxCodeListId);
                AddRef(xmlDoc, invoiceLineAdd, "OverrideItemAccountRef", qbItem.AccountRefListId);
                AddValue(xmlDoc, invoiceLineAdd, true, "Other1", line.ID.ToString());
            }

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private List<QbInvoice> ParseInvoiceQueryResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            List<QbInvoice> result = new List<QbInvoice>();

            XmlNodeList customerQueryRsList = responseXmlDoc.GetElementsByTagName("InvoiceQueryRs");
            if (customerQueryRsList.Count != 1)
                throw new QbException("Invalid response, missing InvoiceQueryRs");

            XmlNode responseNode = customerQueryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList InvoiceRetList = responseNode.SelectNodes("//InvoiceRet");

            foreach (XmlNode invoiceRetNode in InvoiceRetList)
            {
                QbInvoice qbInvoice = new QbInvoice();
                FillInvoiceRet(invoiceRetNode, qbInvoice);
                result.Add(qbInvoice);
            }

            return result;
        }

        private void ParseInvoiceAddResponse(string xml, QbInvoice qbInvoice)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList customerAddRsList = responseXmlDoc.GetElementsByTagName("InvoiceAddRs");
            if (customerAddRsList.Count != 1)
                throw new QbException("Invalid response, missing InvoiceAddRs");

            XmlNode responseNode = customerAddRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
              return;

            XmlNodeList InvoiceRetList = responseNode.SelectNodes("//InvoiceRet");

            if (InvoiceRetList.Count == 0)
                throw new QbException("Missing InvoiceRet");

            XmlNode invoiceRetNode = InvoiceRetList[0];

            FillInvoiceRet(invoiceRetNode, qbInvoice);
        }

        private string BuildInvoiceModRqXML(QbInvoice qbInvoice, QbCustomer qbProject)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement invoiceAddRq = xmlDoc.CreateElement("InvoiceAddRq");
            qbXMLMsgsRq.AppendChild(invoiceAddRq);
            invoiceAddRq.SetAttribute("requestID", "1");

            XmlElement invoiceAdd = xmlDoc.CreateElement("InvoiceAdd");
            invoiceAddRq.AppendChild(invoiceAdd);

            AddRef(xmlDoc, invoiceAdd, "CustomerRef", qbProject.ListId);

            if (qbInvoice.QbClassListId != null)
                AddRef(xmlDoc, invoiceAdd, "ClassRef", qbInvoice.QbClassListId);

            if (qbInvoice.QbAccountListId != null)
                AddRef(xmlDoc, invoiceAdd, "ARAccountRef", qbInvoice.QbAccountListId);

            if (qbInvoice.QbTemplateListId != null)
                AddRef(xmlDoc, invoiceAdd, "TemplateRef", qbInvoice.QbTemplateListId);

            AddValue(xmlDoc, invoiceAdd, qbInvoice.TxnDate != null, "TxnDate", string.Format("{0:yyyy-MM-dd}", qbInvoice.TxnDate));
            AddValue(xmlDoc, invoiceAdd, qbInvoice.RefNumber != null, "RefNumber", qbInvoice.RefNumber);

            if (qbInvoice.BillingAddressAddr1 != null)
            {
                XmlElement billAddress = xmlDoc.CreateElement("BillAddress");
                invoiceAdd.AppendChild(billAddress);

                AddValue(xmlDoc, billAddress, true, "Addr1", qbInvoice.BillingAddressAddr1);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddressAddr2 != null, "Addr2", qbInvoice.BillingAddressAddr2);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddressCity != null, "City", qbInvoice.BillingAddressCity);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddresState != null, "State", qbInvoice.BillingAddresState);
                AddValue(xmlDoc, billAddress, qbInvoice.BillingAddresPostalCode != null, "PostalCode", qbInvoice.BillingAddresPostalCode);
            }

            if (qbInvoice.ShipAddressAddr1 != null)
            {
                XmlElement address = xmlDoc.CreateElement("ShipAddress");
                invoiceAdd.AppendChild(address);

                AddValue(xmlDoc, address, true, "Addr1", qbInvoice.ShipAddressAddr1);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressAddr2 != null, "Addr2", qbInvoice.ShipAddressAddr2);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressCity != null, "City", qbInvoice.ShipAddressCity);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressState != null, "State", qbInvoice.ShipAddressState);
                AddValue(xmlDoc, address, qbInvoice.ShipAddressPostalCode != null, "PostalCode", qbInvoice.ShipAddressPostalCode);
            }

            if (qbInvoice.QbInvoiceTermListId != null)
                AddRef(xmlDoc, invoiceAdd, "TermsRef", qbInvoice.QbInvoiceTermListId);

            if (qbInvoice.QbSalesRepRefListId != null)
                AddRef(xmlDoc, invoiceAdd, "SalesRepRef", qbInvoice.QbSalesRepRefListId);

            if (qbInvoice.ItemSalesTaxRef != null)
                AddRef(xmlDoc, invoiceAdd, "ItemSalesTaxRef", qbInvoice.ItemSalesTaxRef);

            List<QbInvoiceLine> invoiceLines = QbInvoiceLine.FindByInvoiceId(qbInvoice.ID, null);
            foreach (QbInvoiceLine line in invoiceLines)
            {
                QbItem qbItem = QbItem.FindByPrimaryKey(line.QbItemListId);

                XmlElement invoiceLineAdd = xmlDoc.CreateElement("InvoiceLineAdd");
                invoiceAdd.AppendChild(invoiceLineAdd);

                AddRef(xmlDoc, invoiceLineAdd, "ItemRef", line.QbItemListId);
                AddValue(xmlDoc, invoiceLineAdd, line.Description != null, "Desc", line.Description);
                AddValue(xmlDoc, invoiceLineAdd, true, "Quantity", line.Quantity.ToString());
                AddValue(xmlDoc, invoiceLineAdd, true, "Rate", line.Rate.ToString("F2"));
                AddRef(xmlDoc, invoiceLineAdd, "ClassRef", line.QbClassListId);
                AddValue(xmlDoc, invoiceLineAdd, true, "Amount", line.Amount.ToString("F2"));
                AddRef(xmlDoc, invoiceLineAdd, "SalesTaxCodeRef", line.QbSalesTaxCodeListId);
                AddRef(xmlDoc, invoiceLineAdd, "OverrideItemAccountRef", qbItem.AccountRefListId);
            }

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private void FillInvoiceRet(XmlNode invoiceRetNode, QbInvoice qbInvoice)
        {
            qbInvoice.TxnID = invoiceRetNode.SelectSingleNode("./TxnID").InnerText;

            Host.QuickBooksDebug("QbInvoiceSync:FillInvoiceRet", "Processing invoice TxnID=" + qbInvoice.TxnID);

            qbInvoice.TimeCreatedInQb = DateTime.Parse(invoiceRetNode.SelectSingleNode("./TimeCreated").InnerText);
            qbInvoice.TimeModifiedInQb = DateTime.Parse(invoiceRetNode.SelectSingleNode("./TimeModified").InnerText);
            qbInvoice.TxnNumber = Convert.ToInt32(invoiceRetNode.SelectSingleNode("./TxnNumber").InnerText);
            qbInvoice.CustomerRefListId = invoiceRetNode.SelectSingleNode("./CustomerRef/ListID").InnerText;
            qbInvoice.EditSequence = invoiceRetNode.SelectSingleNode("./EditSequence").InnerText;

            if (invoiceRetNode.SelectSingleNode("./ClassRef/ListID") != null)
                qbInvoice.QbClassListId = invoiceRetNode.SelectSingleNode("./ClassRef/ListID").InnerText;

            if (invoiceRetNode.SelectSingleNode("./ARAccountRef/ListID") != null)
                qbInvoice.QbAccountListId = invoiceRetNode.SelectSingleNode("./ARAccountRef/ListID").InnerText;

            if (invoiceRetNode.SelectSingleNode("./TemplateRef/ListID") != null)
                qbInvoice.QbTemplateListId = invoiceRetNode.SelectSingleNode("./TemplateRef/ListID").InnerText;

            qbInvoice.TxnDate = DateTime.Parse(invoiceRetNode.SelectSingleNode("./TxnDate").InnerText);

            if (invoiceRetNode.SelectSingleNode("./RefNumber") != null)
                qbInvoice.RefNumber = invoiceRetNode.SelectSingleNode("./RefNumber").InnerText;

            if (invoiceRetNode.SelectSingleNode("./IsPending") != null)
                qbInvoice.IsPending = Convert.ToBoolean(invoiceRetNode.SelectSingleNode("./IsPending").InnerText);

            if (invoiceRetNode.SelectSingleNode("./ItemSalesTaxRef/ListID") != null)
                qbInvoice.ItemSalesTaxRef = invoiceRetNode.SelectSingleNode("./ItemSalesTaxRef/ListID").InnerText;
            
            XmlNode billingAddressNode = invoiceRetNode.SelectSingleNode("./BillAddress");
            if (billingAddressNode != null)
            {
                qbInvoice.BillingAddressAddr1 = billingAddressNode.SelectSingleNode("./Addr2") != null ? billingAddressNode.SelectSingleNode("./Addr2").InnerText : null;
                qbInvoice.BillingAddressAddr2 = billingAddressNode.SelectSingleNode("./Addr3") != null ? billingAddressNode.SelectSingleNode("./Addr3").InnerText : null;
                qbInvoice.BillingAddressAddr4 = billingAddressNode.SelectSingleNode("./Addr4") != null ? billingAddressNode.SelectSingleNode("./Addr4").InnerText : null;
                qbInvoice.BillingAddressAddr5 = billingAddressNode.SelectSingleNode("./Addr5") != null ? billingAddressNode.SelectSingleNode("./Addr5").InnerText : null;
                qbInvoice.BillingAddressCity = billingAddressNode.SelectSingleNode("./City") != null ? billingAddressNode.SelectSingleNode("./City").InnerText : null;
                qbInvoice.BillingAddresState = billingAddressNode.SelectSingleNode("./State") != null ? billingAddressNode.SelectSingleNode("./State").InnerText : null;
                qbInvoice.BillingAddresPostalCode = billingAddressNode.SelectSingleNode("./PostalCode") != null ? billingAddressNode.SelectSingleNode("./PostalCode").InnerText : null;
                qbInvoice.BillingAddressCountry = billingAddressNode.SelectSingleNode("./Country") != null ? billingAddressNode.SelectSingleNode("./Country").InnerText:null;
                qbInvoice.BillingAddressNote = billingAddressNode.SelectSingleNode("./Note") != null ? billingAddressNode.SelectSingleNode("./Note").InnerText : null;
            }
            
            XmlNode shippingAddressNode = invoiceRetNode.SelectSingleNode("./ShipAddress");
            if (shippingAddressNode != null)
            {
                qbInvoice.ShipAddressAddr1 = billingAddressNode.SelectSingleNode("./Addr2") != null ? billingAddressNode.SelectSingleNode("./Addr2").InnerText : null;
                qbInvoice.ShipAddressAddr2 = billingAddressNode.SelectSingleNode("./Addr3") != null ? billingAddressNode.SelectSingleNode("./Addr3").InnerText : null;
                qbInvoice.ShipAddressAddr3 = billingAddressNode.SelectSingleNode("./Addr4") != null ? billingAddressNode.SelectSingleNode("./Addr4").InnerText : null;
                qbInvoice.ShipAddressAddr4 = billingAddressNode.SelectSingleNode("./Addr5") != null ? billingAddressNode.SelectSingleNode("./Addr5").InnerText : null;
                qbInvoice.ShipAddressCity = billingAddressNode.SelectSingleNode("./City") != null ? billingAddressNode.SelectSingleNode("./City").InnerText : null;
                qbInvoice.ShipAddressState = billingAddressNode.SelectSingleNode("./State") != null ? billingAddressNode.SelectSingleNode("./State").InnerText : null;
                qbInvoice.ShipAddressPostalCode = billingAddressNode.SelectSingleNode("./PostalCode") != null ? billingAddressNode.SelectSingleNode("./PostalCode").InnerText : null;
                qbInvoice.ShipAddressCountry = billingAddressNode.SelectSingleNode("./Country") != null ? billingAddressNode.SelectSingleNode("./Country").InnerText : null;
                qbInvoice.ShipAddressNote = billingAddressNode.SelectSingleNode("./Note") != null ? billingAddressNode.SelectSingleNode("./Note").InnerText : null; 
            }
  
            qbInvoice.QbSalesRepRefListId = invoiceRetNode.SelectSingleNode("./SalesRepRef/ListID") != null? invoiceRetNode.SelectSingleNode("./SalesRepRef/ListID").InnerText : null;

            XmlNodeList invoiceLinesNodeList = invoiceRetNode.SelectNodes("./InvoiceLineRet");

            foreach (XmlNode invoiceLineNode in invoiceLinesNodeList)
            {
                if (invoiceLineNode.SelectSingleNode("./ItemRef/ListID") == null)
                    continue;

                QbInvoiceLine qbInvoiceLine = null;

                if (invoiceLineNode.SelectSingleNode("./Other1") != null)
                {
                    int lineId = Convert.ToInt32(invoiceLineNode.SelectSingleNode("./Other1").InnerText);
                    if (lineId > 0)
                    {

                        foreach (QbInvoiceLine invoiceLine in qbInvoice.QbInvoiceLines)
                        {
                            if (lineId == invoiceLine.ID)
                            {
                                Host.QuickBooksDebug("QbInvoiceSync:FillInvoiceRet", "Found invoiceLine.ID" + invoiceLine.ID);
                                qbInvoiceLine = invoiceLine;
                                break;
                            }
                        }
                    }
                }

                if (qbInvoiceLine == null)
                {
                    Host.QuickBooksDebug("QbInvoiceSync:FillInvoiceRet", "Item Not found, creating new one");
                    qbInvoiceLine = new QbInvoiceLine();
                    qbInvoice.QbInvoiceLines.Add(qbInvoiceLine);
                }

                qbInvoiceLine.TxnLineID = invoiceLineNode.SelectSingleNode("./TxnLineID").InnerText;
                Host.QuickBooksDebug("QbInvoiceSync:FillInvoiceRet", "qbInvoiceLine.TxnLineID = " + qbInvoiceLine.TxnLineID);
                Host.QuickBooksDebug("QbInvoiceSync:FillInvoiceRet", "qbInvoiceLine.ID = " + qbInvoiceLine.ID);

                qbInvoiceLine.QbItemListId = invoiceLineNode.SelectSingleNode("./ItemRef/ListID").InnerText;

                if (invoiceLineNode.SelectSingleNode("./Desc") != null)
                    qbInvoiceLine.Description = invoiceLineNode.SelectSingleNode("./Desc").InnerText;

                if (invoiceLineNode.SelectSingleNode("./Quantity") != null)
                    qbInvoiceLine.Quantity = Convert.ToDecimal(invoiceLineNode.SelectSingleNode("./Quantity").InnerText);

                if (invoiceLineNode.SelectSingleNode("./UnitOfMeasure") != null)
                    qbInvoiceLine.UnitOfMeasure = invoiceLineNode.SelectSingleNode("./UnitOfMeasure").InnerText;

                if (invoiceLineNode.SelectSingleNode("./Rate") != null)
                    qbInvoiceLine.Rate = Convert.ToDecimal(invoiceLineNode.SelectSingleNode("./Rate").InnerText);

                if (invoiceLineNode.SelectSingleNode("./Amount") != null)
                    qbInvoiceLine.Amount = Convert.ToDecimal(invoiceLineNode.SelectSingleNode("./Amount").InnerText);

                if (invoiceLineNode.SelectSingleNode("./ClassRef/ListID") != null)
                    qbInvoiceLine.QbClassListId = invoiceLineNode.SelectSingleNode("./ClassRef/ListID").InnerText;

                if (invoiceLineNode.SelectSingleNode("./SalesTaxCodeRef/ListID") != null)
                    qbInvoiceLine.QbSalesTaxCodeListId = invoiceLineNode.SelectSingleNode("./SalesTaxCodeRef/ListID").InnerText;
            }
        }

        #endregion

        #region QbPaymentMethod

        public void RefreshQbPaymentMethods()
        {
            List<QbPaymentMethod> paymentMethodsInQb = FindPaymentMethodsInQb();
            List<QbPaymentMethod> paymentMethodsInDb = QbPaymentMethod.Find();

            foreach (QbPaymentMethod paymentMethodInQb in paymentMethodsInQb)
            {
                QbPaymentMethod paymentMethodInDb = paymentMethodsInDb.Find(delegate(QbPaymentMethod tempPaymentMethod)
                    { return tempPaymentMethod.ListId == paymentMethodInQb.ListId; });

                if (paymentMethodInDb == null)
                    QbPaymentMethod.Insert(paymentMethodInQb);
                else if (paymentMethodInQb.EditSequence != paymentMethodInDb.EditSequence)
                    QbPaymentMethod.Update(paymentMethodInQb);
            }
        }

        private List<QbPaymentMethod> FindPaymentMethodsInQb()
        {
           string request = BuildGetAllQueryRqXml("PaymentMethodQueryRq");
           string response = m_requestProcessor.ProcessRequest(m_ticket, request);
           return ParsePaymentMethodQueryResponse(response);
        }

        private List<QbPaymentMethod> ParsePaymentMethodQueryResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            List<QbPaymentMethod> result = new List<QbPaymentMethod>();

            XmlNodeList queryRsList = responseXmlDoc.GetElementsByTagName("PaymentMethodQueryRs");
            if (queryRsList.Count != 1)
                throw new QbException("Invalid response, missing PaymentMethodQueryRs");

            XmlNode responseNode = queryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList paymentMethodRetList = responseNode.SelectNodes("//PaymentMethodRet");
            foreach (XmlNode paymentMethodRetNode in paymentMethodRetList)
            {
                QbPaymentMethod qbPaymentMethod = new QbPaymentMethod();

                qbPaymentMethod.ListId = paymentMethodRetNode.SelectSingleNode("./ListID").InnerText;
                qbPaymentMethod.TimeCreated = DateTime.Parse(paymentMethodRetNode.SelectSingleNode("./TimeCreated").InnerText);
                qbPaymentMethod.EditSequence = paymentMethodRetNode.SelectSingleNode("./EditSequence").InnerText;
                qbPaymentMethod.TimeModified = DateTime.Parse(paymentMethodRetNode.SelectSingleNode("./TimeModified").InnerText);
                qbPaymentMethod.Name = paymentMethodRetNode.SelectSingleNode("./Name").InnerText;
                qbPaymentMethod.IsActive = Convert.ToBoolean(paymentMethodRetNode.SelectSingleNode("./IsActive ").InnerText);

                result.Add(qbPaymentMethod);
            }

            return result;
        }
        
        #endregion 

        #region QbPayment

        public void SyncPayments(DateTime fromModifiedDate, QBSyncLog log)
        {
            Host.QuickBooksDebug("QbGateway:SyncPayments", "Started");
            ReportStatus("Sync Payments", "STARTED", "Started Syncronization of payments");

            List<QbPayment> qbPayments = FindQbPayments(fromModifiedDate);

            foreach (QbPayment paymentInQb in qbPayments)
            {
                QbCustomer qbProject;
                QbCustomer qbCustomer;
                try
                {
                    qbProject = QbCustomer.FindByListId(paymentInQb.CustomerRefListId, null);
                    qbCustomer = QbCustomer.FindParent(qbProject.CustomerId, null);
                }
                catch (DataNotFoundException)
                {
                    continue;
                }

                QbPayment paymentInDb;

                try
                {
                    paymentInDb = QbPayment.FindByPrimaryKey(paymentInQb.TxnID);
                }
                catch (DataNotFoundException)
                {
                    
                    paymentInQb.QbCustomerId = qbProject.ID;
                    QbPayment.Insert(paymentInQb);

                    QbSyncLogDetail detail = new QbSyncLogDetail();
                    detail.CompletedDate = DateTime.Now;
                    detail.IsSuccess = true;
                    detail.QbSyncActionId = (int)QbSyncActionEnum.PaymentAdd;
                    detail.QbSyncLogId = log.ID;
                    detail.TxnID = paymentInQb.TxnID;
                    QbSyncLogDetail.Insert(detail);

                    ReportStatus("New Payment", "OK",
                                 "Customer " + qbCustomer.FullName + " Payment " + paymentInQb.RefNumber + " Amount " +
                                 paymentInQb.TotalAmount.ToString("c"));

                    Host.QuickBooksDebug("QbGateway:SyncPayments", "Created new payment paymentInQb.TxnID " + paymentInQb.TxnID + " QbProjectId " + qbProject.ID);
                    continue;
                }

                if (paymentInQb.EditSequence == paymentInDb.EditSequence)
                    continue;

                paymentInQb.QbCustomerId = paymentInDb.QbCustomerId;
                QbPayment.Update(paymentInQb);

                ReportStatus("Update Payment", "OK",
                                 "Customer " + qbCustomer.FullName + " Payment " + paymentInQb.RefNumber + " Amount " +
                                 paymentInQb.TotalAmount.ToString("c"));


                Host.QuickBooksDebug("QbGateway:SyncPayments", "Updateing payment paymentInQb.TxnID " + paymentInQb.TxnID);

                QbSyncLogDetail qbSyncLogDetail = new QbSyncLogDetail();
                qbSyncLogDetail.CompletedDate = DateTime.Now;
                qbSyncLogDetail.IsSuccess = true;
                qbSyncLogDetail.QbSyncActionId = (int)QbSyncActionEnum.PaymentMod;
                qbSyncLogDetail.QbSyncLogId = log.ID;
                qbSyncLogDetail.TxnID = paymentInQb.TxnID;
                QbSyncLogDetail.Insert(qbSyncLogDetail);
            }

            ReportStatus("Sync Payments", "COMPLETED", "Completed Syncronization of invoices");
        }

        public List<QbPayment> FindQbPayments(DateTime fromModifiedDate)
        {
            string xmlRequest = BuildPaymentQueryRqXml(fromModifiedDate);
            Host.QuickBooksDebug("NetServmanSync", xmlRequest);

            string response = m_requestProcessor.ProcessRequest(m_ticket, xmlRequest);
            Host.QuickBooksDebug("NetServmanSync", response);

            List<QbPayment> result = ParsePaymentQueryResponse(response);

            return result;
        }

        private string BuildPaymentQueryRqXml(DateTime fromModifiedDate)
        {
            string xml = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement paymentQueryRq = xmlDoc.CreateElement("ReceivePaymentQueryRq");
            qbXMLMsgsRq.AppendChild(paymentQueryRq);

            XmlElement modifiedDateRangeFilterElement = xmlDoc.CreateElement("ModifiedDateRangeFilter");
            paymentQueryRq.AppendChild(modifiedDateRangeFilterElement);

            AddValue(xmlDoc, modifiedDateRangeFilterElement, true, "FromModifiedDate", fromModifiedDate.ToString("yyyy-MM-ddTHH:mm:ss-06:00"));

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private List<QbPayment> ParsePaymentQueryResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            List<QbPayment> result = new List<QbPayment>();

            XmlNodeList customerQueryRsList = responseXmlDoc.GetElementsByTagName("ReceivePaymentQueryRs");
            if (customerQueryRsList.Count != 1)
                throw new QbException("Invalid response, missing ReceivePaymentQueryRs");

            XmlNode responseNode = customerQueryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList paymentRetList = responseNode.SelectNodes("//ReceivePaymentRet");

            foreach (XmlNode paymentRetNode in paymentRetList)
            {
                QbPayment qbPayment = new QbPayment();
                FillPaymentRet(paymentRetNode, qbPayment);
                result.Add(qbPayment);
            }

            return result;
        }

        private void FillPaymentRet(XmlNode paymentRetNode, QbPayment qbPayment)
        {
            qbPayment.TxnID = paymentRetNode.SelectSingleNode("./TxnID").InnerText;
            qbPayment.TimeCreatedInQb = DateTime.Parse(paymentRetNode.SelectSingleNode("./TimeCreated").InnerText);
            qbPayment.TimeModifiedInQb = DateTime.Parse(paymentRetNode.SelectSingleNode("./TimeModified").InnerText);
            qbPayment.EditSequence = paymentRetNode.SelectSingleNode("./EditSequence").InnerText;
            qbPayment.TxnNumber = Convert.ToInt32(paymentRetNode.SelectSingleNode("./TxnNumber").InnerText);
            qbPayment.CustomerRefListId = paymentRetNode.SelectSingleNode("./CustomerRef/ListID").InnerText;
            qbPayment.QbAccountListId = paymentRetNode.SelectSingleNode("./ARAccountRef/ListID").InnerText;
            qbPayment.QbPaymentMethodListId = paymentRetNode.SelectSingleNode("./PaymentMethodRef/ListID").InnerText;
            

            if (paymentRetNode.SelectSingleNode("./RefNumber") != null)
                qbPayment.RefNumber = paymentRetNode.SelectSingleNode("./RefNumber").InnerText;

            qbPayment.TotalAmount = Convert.ToDecimal(paymentRetNode.SelectSingleNode("./TotalAmount").InnerText);
            qbPayment.TxnDate = DateTime.Parse(paymentRetNode.SelectSingleNode("./TxnDate").InnerText);
        }

        #endregion 

        #region QbCreditMemo

        public void SyncCreditMemos(DateTime fromModifiedDate, QBSyncLog log)
        {
            ReportStatus("Sync Credit Memos", "STARTED", "Started Synchronization of Credit Memos");
            List<QbCreditMemo> creditMemosInQb = FindQbCreditMemos(fromModifiedDate);

            foreach (QbCreditMemo creditMemoInQb in creditMemosInQb)
            {
                QbCustomer qbProject;
                QbCustomer qbCustomer;

                try
                {
                    qbProject = QbCustomer.FindByListId(creditMemoInQb.CustomerRefListId, null);
                    qbCustomer = QbCustomer.FindParent(qbProject.CustomerId, null);
                    Host.QuickBooksDebug("QbGateway:SyncCreditMemos", "Found new credit memos for qb project id" + qbProject.ID);
                }
                catch (DataNotFoundException)
                {
                    continue;
                }

                QbCreditMemo creditMemoInDb;

                try
                {
                    creditMemoInDb = QbCreditMemo.FindByPrimaryKey(creditMemoInQb.TxnID);
                }
                catch (DataNotFoundException)
                {
                    Host.QuickBooksDebug("QbGateway:SyncCreditMemos", " Adding new creditMemoInQb.TxnID " + creditMemoInQb.TxnID);

                    creditMemoInQb.CalculateTotals();
                    creditMemoInQb.QbCustomerId = qbProject.ID;
                    QbCreditMemo.Insert(creditMemoInQb);

                    foreach (QbCreditMemoLine line in creditMemoInQb.QbCreditMemoLines)
                    {
                        line.QbCreditMemoTxnID = creditMemoInQb.TxnID;
                        QbCreditMemoLine.Insert(line);
                    }

                    QbSyncLogDetail detail = new QbSyncLogDetail();
                    detail.CompletedDate = DateTime.Now;
                    detail.IsSuccess = true;
                    detail.QbSyncActionId = (int)QbSyncActionEnum.CreditMemoAdd;
                    detail.QbSyncLogId = log.ID;
                    detail.TxnID = creditMemoInQb.TxnID;
                    QbSyncLogDetail.Insert(detail);
                    ReportStatus("New Credit Memo", "OK",
                                "Customer " + qbCustomer.FullName + " credit memo " +
                                creditMemoInQb.RefNumber + " Amount " +
                                creditMemoInQb.TotalAmount.ToString("c"));

                    continue;
                }

                if (creditMemoInDb.EditSequence == creditMemoInQb.EditSequence)
                    continue;

                Host.QuickBooksDebug("QbGateway:SyncCreditMemos", " Modifying creditMemoInQb.TxnID " + creditMemoInQb.TxnID);
                
                creditMemoInQb.CalculateTotals();
                creditMemoInQb.QbCustomerId = creditMemoInDb.QbCustomerId;
                QbCreditMemo.Update(creditMemoInQb, null);

                ReportStatus("Update Credit Memo", "OK",
                                "Customer " + qbCustomer.FullName + " credit memo " +
                                creditMemoInQb.RefNumber + " Amount " +
                                creditMemoInQb.TotalAmount.ToString("c"));

                QbCreditMemoLine.DeleteByCreditMemoTxnId(creditMemoInQb.TxnID, null);

                foreach (QbCreditMemoLine line in creditMemoInQb.QbCreditMemoLines)
                {
                    line.QbCreditMemoTxnID = creditMemoInQb.TxnID;
                    QbCreditMemoLine.Insert(line);
                }

                QbSyncLogDetail detail1 = new QbSyncLogDetail();
                detail1.CompletedDate = DateTime.Now;
                detail1.IsSuccess = true;
                detail1.QbSyncActionId = (int)QbSyncActionEnum.CreditMemoMod;
                detail1.QbSyncLogId = log.ID;
                detail1.TxnID = creditMemoInQb.TxnID;
                QbSyncLogDetail.Insert(detail1);
            }

            ReportStatus("Sync Credit Memos", "COMPLETED", "Started Synchronization of Credit Memos");
        }

        public List<QbCreditMemo> FindQbCreditMemos(DateTime fromModifiedDate)
        {
            string xmlRequest = BuildCreditMemoQueryRqXml(fromModifiedDate);
            Host.QuickBooksDebug("QbGateway:FindQbCreditMemos", xmlRequest);

            string response = m_requestProcessor.ProcessRequest(m_ticket, xmlRequest);
            Host.QuickBooksDebug("QbGateway:FindQbCreditMemos", response);

            List<QbCreditMemo> result = ParseCreditMemoQueryResonse(response);

            return result;
        }

        private string BuildCreditMemoQueryRqXml(DateTime fromModifiedDate)
        {
            string xml = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement creditMemoQueryRq = xmlDoc.CreateElement("CreditMemoQueryRq");
            qbXMLMsgsRq.AppendChild(creditMemoQueryRq);

            XmlElement modifiedDateRangeFilterElement = xmlDoc.CreateElement("ModifiedDateRangeFilter");
            creditMemoQueryRq.AppendChild(modifiedDateRangeFilterElement);

            AddValue(xmlDoc, modifiedDateRangeFilterElement, true, "FromModifiedDate", fromModifiedDate.ToString("yyyy-MM-ddTHH:mm:ss-06:00"));
            AddValue(xmlDoc, creditMemoQueryRq, true, "IncludeLineItems", "true");

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private List<QbCreditMemo> ParseCreditMemoQueryResonse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            List<QbCreditMemo> result = new List<QbCreditMemo>();

            XmlNodeList customerQueryRsList = responseXmlDoc.GetElementsByTagName("CreditMemoQueryRs");
            if (customerQueryRsList.Count != 1)
                throw new QbException("Invalid response, missing ReceivePaymentQueryRs");

            XmlNode responseNode = customerQueryRsList.Item(0);

            if (!IsQbResponseAvailable(responseNode))
                return result;

            XmlNodeList creditMemoRetList = responseNode.SelectNodes("//CreditMemoRet");

            foreach (XmlNode retNode in creditMemoRetList)
            {
                QbCreditMemo qbCreditMemo = new QbCreditMemo();
                FillCreditMemoRet(retNode, qbCreditMemo);
                result.Add(qbCreditMemo);
            }

            return result;
        }

        private void FillCreditMemoRet(XmlNode retNode, QbCreditMemo qbCreditMemo)
        {
            qbCreditMemo.TxnID = retNode.SelectSingleNode("./TxnID").InnerText;
            qbCreditMemo.TimeCreatedInQb = DateTime.Parse(retNode.SelectSingleNode("./TimeCreated").InnerText);
            qbCreditMemo.TimeModifiedInQb = DateTime.Parse(retNode.SelectSingleNode("./TimeModified").InnerText);
            qbCreditMemo.TxnNumber = Convert.ToInt32(retNode.SelectSingleNode("./TxnNumber").InnerText);
            qbCreditMemo.CustomerRefListId = retNode.SelectSingleNode("./CustomerRef/ListID").InnerText;
            qbCreditMemo.EditSequence = retNode.SelectSingleNode("./EditSequence").InnerText;

            if (retNode.SelectSingleNode("./ClassRef/ListID") != null)
                qbCreditMemo.QbClassListId = retNode.SelectSingleNode("./ClassRef/ListID").InnerText;

            if (retNode.SelectSingleNode("./ARAccountRef/ListID") != null)
                qbCreditMemo.QbAccountListId = retNode.SelectSingleNode("./ARAccountRef/ListID").InnerText;

            if (retNode.SelectSingleNode("./TemplateRef/ListID") != null)
                qbCreditMemo.QbTemplateListId = retNode.SelectSingleNode("./TemplateRef/ListID").InnerText;

            qbCreditMemo.TxnDate = DateTime.Parse(retNode.SelectSingleNode("./TxnDate").InnerText);

            if (retNode.SelectSingleNode("./RefNumber") != null)
                qbCreditMemo.RefNumber = retNode.SelectSingleNode("./RefNumber").InnerText;

            if (retNode.SelectSingleNode("./IsPending") != null)
                    qbCreditMemo.IsPending = Convert.ToBoolean(retNode.SelectSingleNode("./IsPending").InnerText);

            if (retNode.SelectSingleNode("./ItemSalesTaxRef/ListID") != null)
                qbCreditMemo.ItemSalesTaxRef = retNode.SelectSingleNode("./ItemSalesTaxRef/ListID").InnerText;

            qbCreditMemo.SalesRepRefListId = retNode.SelectSingleNode("./SalesRepRef/ListID") != null ? retNode.SelectSingleNode("./SalesRepRef/ListID").InnerText : null;

            XmlNodeList linesNodeList = retNode.SelectNodes("./CreditMemoLineRet");

            foreach (XmlNode lineNode in linesNodeList)
            {
                QbCreditMemoLine line = new QbCreditMemoLine();
                qbCreditMemo.QbCreditMemoLines.Add(line);

                line.TxnLineID = lineNode.SelectSingleNode("./TxnLineID").InnerText;
                line.QbItemListId = lineNode.SelectSingleNode("./ItemRef/ListID").InnerText;

                if (lineNode.SelectSingleNode("./Desc") != null)
                    line.Description = lineNode.SelectSingleNode("./Desc").InnerText;

                if (lineNode.SelectSingleNode("./Quantity") != null)
                    line.Quantity = Convert.ToDecimal(lineNode.SelectSingleNode("./Quantity").InnerText);

                if (lineNode.SelectSingleNode("./Rate") != null)
                    line.Rate = Convert.ToDecimal(lineNode.SelectSingleNode("./Rate").InnerText);

                if (lineNode.SelectSingleNode("./Amount") != null)
                    line.Amount = Convert.ToDecimal(lineNode.SelectSingleNode("./Amount").InnerText);
                if (lineNode.SelectSingleNode("./SalesTaxCodeRef/ListID") != null)
                    line.QbSalesTaxCodeListId = lineNode.SelectSingleNode("./SalesTaxCodeRef/ListID").InnerText;
            }
        }
      
        #endregion

        #region TxnVoid

        public void VoidTransaction(string txnVoidType, string txnId, out string qbXmlRequest, out string qbXmlResponse)
        {
            qbXmlRequest = BuildTxnVoidRqXML(txnVoidType, txnId);
            qbXmlResponse = m_requestProcessor.ProcessRequest(m_ticket, qbXmlRequest);
            ParseTxnVoidResponse(qbXmlResponse);
        }

        private string BuildTxnVoidRqXML(string txnVoidType, string txnId)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

            XmlElement txnVoidRq = xmlDoc.CreateElement("TxnVoidRq");
            qbXMLMsgsRq.AppendChild(txnVoidRq);

            AddValue(xmlDoc, txnVoidRq, true, "TxnVoidType", txnVoidType);
            AddValue(xmlDoc, txnVoidRq, true, "TxnID", txnId);

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private void ParseTxnVoidResponse(string xml)
        {
            XmlDocument responseXmlDoc = new XmlDocument();
            responseXmlDoc.LoadXml(xml);

            XmlNodeList rsList = responseXmlDoc.GetElementsByTagName("TxnVoidRs");
            if (rsList.Count != 1)
                throw new QbException("Invalid response");

            XmlNode responseNode = rsList.Item(0);

            IsQbResponseAvailable(responseNode);
        }
        #endregion

        #region QB Helpers

        public void ConnectToQB()
        {
            m_requestProcessor = new RequestProcessor2Class();
            m_requestProcessor.OpenConnection(m_appID, m_appName);
            m_ticket = m_requestProcessor.BeginSession(m_companyFile, m_mode);
        }

        public void DisconnectFromQB()
        {
            if (m_ticket != null)
            {
                m_requestProcessor.EndSession(m_ticket);
                m_ticket = null;
                m_requestProcessor.CloseConnection();
            }
        }

        private XmlElement BuildRqEnvelope(XmlDocument doc)
        {
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));

            doc.AppendChild(doc.CreateProcessingInstruction("qbxml", "version=\"8.0\""));
            XmlElement qbXML = doc.CreateElement("QBXML");
            doc.AppendChild(qbXML);
            XmlElement qbXMLMsgsRq = doc.CreateElement("QBXMLMsgsRq");
            qbXML.AppendChild(qbXMLMsgsRq);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            return qbXMLMsgsRq;
        }

        private bool IsQbResponseAvailable(XmlNode responseNode)
        {
            XmlAttributeCollection rsAttributes = responseNode.Attributes;
            string statusCode = rsAttributes.GetNamedItem("statusCode").Value;
            string statusSeverity = rsAttributes.GetNamedItem("statusSeverity").Value;
            string statusMessage = rsAttributes.GetNamedItem("statusMessage").Value;

            int intErrorCode = int.Parse(statusCode);
            if (intErrorCode == 1 && statusSeverity == "Info")
            {
                return false;
            }

            if (intErrorCode != 0)
                throw new QbException(intErrorCode, statusMessage);

            return true;
        }

        private string BuildGetAllQueryRqXml(string requestName)
        {
            return BuildGetAllQueryRqXml(requestName, true);
        }

        private string BuildGetAllQueryRqXml(string requestName, bool needActiveStatus)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = BuildRqEnvelope(xmlDoc);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement queryRq = xmlDoc.CreateElement(requestName);
            qbXMLMsgsRq.AppendChild(queryRq);
            queryRq.SetAttribute("requestID", "1");

            if (needActiveStatus)
                AddValue(xmlDoc, queryRq, true, "ActiveStatus", "All");

            xml = xmlDoc.OuterXml;
            return xml;
        }

        private void AddRef(XmlDocument xmlDoc, XmlElement appendToXmlElement, string refName, string listId )
        {
            XmlElement refElement = xmlDoc.CreateElement(refName);
            appendToXmlElement.AppendChild(refElement);

            XmlElement temp = xmlDoc.CreateElement("ListID");
            refElement.AppendChild(temp);
            temp.InnerText = listId;
        }

        private void AddValue(XmlDocument xmlDoc, XmlElement appendToXmlElement, bool condition, string elementName, string value)
        {
            if (condition)
            {
                XmlElement temp = xmlDoc.CreateElement(elementName);
                appendToXmlElement.AppendChild(temp);
                temp.InnerText = value;
            }
        }

        private string GetOptionalSingleNodeText(XmlNode node, string nodeName)
        {
            if (node.SelectSingleNode(nodeName) != null)
                return node.SelectSingleNode(nodeName).InnerText;
            else
                return null;
        }

        #endregion

        #region Report Status

        private void ReportStatus(string action, string status, string description)
        {
            if (m_reportStatus != null)
                m_reportStatus(action, status, description);
        }

        #endregion
    }
}
