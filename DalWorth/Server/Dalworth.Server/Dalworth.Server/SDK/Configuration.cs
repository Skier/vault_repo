using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Dalworth.Server.SDK
{    
    public class Configuration
    {
        ApplicationConfiguration m_app = new ApplicationConfiguration();

        public static ApplicationConfiguration App
        {
            get
            {
                return Instance.m_app;
            }
        }

        [XmlRoot("Application")]
        public class ApplicationConfiguration
        {
            bool m_trace = true;
            [XmlAttribute]
            public bool Trace
            {
                get { return m_trace; }
                set { m_trace = value; }
            }

            public bool Debug { get; set; }
            
            public void Load(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new DalworthException("Invalid configuration file");
                
                m_trace = Boolean.Parse(xmlElement.GetAttribute("Trace"));

                string strDebug = xmlElement.GetAttribute("Debug");
                if (!string.IsNullOrEmpty(xmlElement.GetAttribute("Debug")))
                {
                    Debug = Boolean.Parse(strDebug);
                }
            }
            

            public void Save(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new DalworthException("Invalid configuration file");
                
                xmlElement.SetAttribute("Trace", m_trace.ToString());
            }
        }


        public const string FILE_PATH = @"Dalworth.Server.Win32.xml";
        
        #region Fields
        //Global Variables
        
        private static string m_connectionString = String.Empty;
        private static string m_servmanConnectionString = String.Empty;
        private static string m_webServiceUrl = String.Empty;

        private static string m_connectionKeyEnc = string.Empty;
        private static string m_connectionKey = string.Empty;
        private static string m_lastLogin = string.Empty;
        private static string m_customerSearchDelay = string.Empty;
        private static string m_libertyMutualAdsourceIds = string.Empty;
        private static string m_stateFarmAdsourceIds = string.Empty;

        private static bool m_logUserActions = false;
        private static bool m_flushEveryRecord = false;

        private static int m_ticketImportInterval = 0;
        private static int m_customerImportInterval = 0;
        private static int m_exportInterval = 0;
        private static DateTime m_workingTimeStart;
        private static DateTime m_workingTimeEnd;

        private static string m_dbFullPath;
        private static string m_dbLogFullPath;

        #endregion

        #region Constructor
        private Configuration()
        {

        }
        #endregion

        #region EventStoreLevel
        public static EventType EventStoreLevel
        {
            get
            {
                return EventType.Exception;
            }
        }
        #endregion        

        private static X509Certificate m_clientCertificate;
        public static X509Certificate ClientCertificate
        {
            get { return m_clientCertificate; }
        }

        public static string WebServiceUrl
        {
            get { return m_webServiceUrl; }
            set { m_webServiceUrl = value; }
        }

        private static int m_currentDispatchId;
        public static int CurrentDispatchId
        {
            get { return m_currentDispatchId; }
            set { m_currentDispatchId = value; }
        }

        public static string ConnectionKeyEnc
        {
            get { return m_connectionKeyEnc; }
        }

        public static int TicketImportInterval
        {
            get { return m_ticketImportInterval; }
        }

        public static int CustomerImportInterval
        {
            get { return m_customerImportInterval; }
        }

        public static int ExportInterval
        {
            get { return m_exportInterval; }
        }

        public static DateTime WorkingTimeStart
        {
            get { return m_workingTimeStart; }
        }

        public static DateTime WorkingTimeEnd
        {
            get { return m_workingTimeEnd; }
        }

        private static string m_digiumApiUrl;
        public static string DigiumApiUrl
        {
            get { return m_digiumApiUrl; }
        }

        private static string m_dalworthRestorationCustomerFeedbackUrl;
        public static string DalworthRestorationCustomerFeedbackUrl
        {
            get { return m_dalworthRestorationCustomerFeedbackUrl; }
        }

        public static DateTime? DigiumDateStart { get; set; }
        public static DateTime? DigiumDateEnd { get; set; }
       

        public static int DigiumCallImportRequeryMin { get; set; }
        public static int DigiumTransactionImportDelayMin { get; set; }
        public static int DigiumOutdatedVoiceFileMin { get; set; }

        public static string DigiumIncomingVoiceFilesFolder1 { get; set; }
        public static string DigiumIncomingVoiceFilesFolder2 { get; set; }
        public static string DigiumOutgoingVoiceFilesFolder { get; set; }
        public static string DigiumLameFolder { get; set; }

        private static string m_partnerSiteEmailFrom;
        public static string PartnerSiteEmailFrom
        {
            get { return m_partnerSiteEmailFrom; }
        }

        private static string m_partnerSiteUrl;
        public static string PartnerSiteUrl
        {
            get { return m_partnerSiteUrl; }
        }

        private static string m_digiumLogin;        
        public static string DigiumLogin
        {
            get { return m_digiumLogin; }
        }

        private static string m_digiumPassword;
        public static string DigiumPassword
        {
            get { return m_digiumPassword; }
        }

        public static string ConnectionKey
        {
            get { return m_connectionKey; }
            set { m_connectionKey = value; }
        }

        public static string LastLogin
        {
            get { return m_lastLogin; }
            set { m_lastLogin = value; }
        }

        public static string CustomerSearchDelay
        {
            get { return m_customerSearchDelay; }
        }

        public static string LibertyMutualAdsourceIds
        {
            get { return m_libertyMutualAdsourceIds; }
        }

        public static string StateFarmAdsourceIds
        {
            get { return m_stateFarmAdsourceIds; }
        }

        public static bool LogUserActions
        {
            get { return m_logUserActions; }
        }

        public static bool FlushEveryRecord
        {
            get { return m_flushEveryRecord; }
        }

        private static object m_mainFormController;
        public static object MainFormController
        {
            get { return m_mainFormController; }
            set { m_mainFormController = value; }
        }

        private static void InitClientCertificate()
        {
            StreamReader reader = new StreamReader(Host.GetPath(@"\Certificates\client.cer"));
            BinaryReader binaryReader = new BinaryReader(reader.BaseStream);
            byte[] data = binaryReader.ReadBytes(1000000);
            binaryReader.Close();
            m_clientCertificate = new X509Certificate(data);
        }

        #region Printing

        private static string m_visitPrinter;
        public static string VisitPrinter
        {
            get { return m_visitPrinter; }
            set { m_visitPrinter = value; }
        }

        private static string m_reportPrinter;
        public static string ReportPrinter
        {
            get { return m_reportPrinter; }
            set { m_reportPrinter = value; }
        }

        private static bool m_automatedVisitPrint;
        public static bool AutomatedVisitPrint
        {
            get { return m_automatedVisitPrint; }
            set { m_automatedVisitPrint = value; }
        }

        private static DateTime m_tomorrowVisitsPrintTime;
        public static DateTime TomorrowVisitsPrintTime
        {
            get { return m_tomorrowVisitsPrintTime; }
            set { m_tomorrowVisitsPrintTime = value; }
        }

        #endregion

        #region Email Smtp

        private static string m_smtpHost;
        public static string SmtpHost
        {
            get { return m_smtpHost; }
            set { m_smtpHost = value; }
        }

        private static int m_smtpPort;
        public static int SmtpPort
        {
            get { return m_smtpPort; }
            set { m_smtpPort = value; }
        }

        private static string m_smtpLogin;
        public static string SmtpLogin
        {
            get { return m_smtpLogin; }
            set { m_smtpLogin = value; }
        }

        private static string m_smtpPassword;
        public static string SmtpPassword
        {
            get { return m_smtpPassword; }
            set { m_smtpPassword = value; }
        }

        private static string m_smtpFromAddress;
        public static string SmtpFromAddress
        {
            get { return m_smtpFromAddress; }
            set { m_smtpFromAddress = value; }
        }

        private static string m_smtpDisplayName;
        public static string SmtpDisplayName
        {
            get { return m_smtpDisplayName; }
            set { m_smtpDisplayName = value; }
        }

        private static string m_smtpOnCallAddress;
        public static string SmtpOnCallAddress
        {
            get { return m_smtpOnCallAddress; }
            set { m_smtpOnCallAddress = value; }
        }

        private static string m_smtpOnCallDisplayName;
        public static string SmtpOnCallDisplayName
        {
            get { return m_smtpOnCallDisplayName; }
            set { m_smtpOnCallDisplayName = value; }
        }

        private static string m_smtpMarketingAddress;
        public static string SmtpMarketingAddress
        {
            get { return m_smtpMarketingAddress; }
            set { m_smtpMarketingAddress = value; }
        }

        private static string m_smtpMarketingDisplayName;
        public static string SmtpMarketingDisplayName
        {
            get { return m_smtpMarketingDisplayName; }
            set { m_smtpMarketingDisplayName = value; }
        }

        private static string m_smtpLateLeadsAddress;
        public static string SmtpLateLeadsAddress
        {
            get { return m_smtpLateLeadsAddress; }
            set { m_smtpLateLeadsAddress = value; }
        }

        private static string m_smtpLateLeadsDisplayName;
        public static string SmtpLateLeadsDisplayName
        {
            get { return m_smtpLateLeadsDisplayName; }
            set { m_smtpLateLeadsDisplayName = value; }
        }

        private static string m_smtpFeedbackAddress;
        public static string SmtpFeedbackAddress
        {
            get { return m_smtpFeedbackAddress; }
            set { m_smtpFeedbackAddress = value; }
        }

        private static string m_smtpFeedbackDisplayName;
        public static string SmtpFeedbackDisplayName
        {
            get { return m_smtpFeedbackDisplayName; }
            set { m_smtpFeedbackDisplayName = value; }
        }

        #endregion

        #region Quickbooks

        #region QuickBooksCompanyFile

        private static string m_qbCompanyFile;
        public static string QuickBooksCompanyFile
        {
            get { return m_qbCompanyFile; }
            set { m_qbCompanyFile = value; }
        }

        #endregion 

        #region QuickBooksAppName

        private static string m_qbAppName;
        public static string QuickBooksAppName
        {
            get { return m_qbAppName; }
            set { m_qbAppName = value; }
        }

        #endregion

        #region QuickBooksAppID

        private static string m_qbAppID;
        public static string QuickBooksAppID
        {
            get { return m_qbAppID; }
            set { m_qbAppID = value; }
        }

        #endregion


        #region QuickBooksLogLevel

        private static QuickbooksLogLevalEnum m_qbLogLevel;
        public static QuickbooksLogLevalEnum QuickBooksLogLevel
        {
            get { return m_qbLogLevel; }
            set { m_qbLogLevel = value; }
        }

        #endregion 

        #region QuickBooksItemRugCleaningCostListId

        private static string m_qbItemRugCleaningCostListId;
        public static string QuickBooksItemRugCleaningCostListId
        {
            get { return m_qbItemRugCleaningCostListId; }
            set { m_qbItemRugCleaningCostListId = value; }
        }

        #endregion 

        #region QuickBooksItemRugCleaningCostFloodListId

        private static string m_qbItemRugCleaningCostFloodListId;
        public static string QuickBooksItemRugCleaningCostFloodListId
        {
            get { return m_qbItemRugCleaningCostFloodListId; }
            set { m_qbItemRugCleaningCostFloodListId = value; }
        }

        #endregion

        #region QuickbooksItemRugCleaningPadListId

        private static string m_qbItemRugCleaningPadListId;
        public static string QuickBooksItemRugCleaningPadListId
        {
            get { return m_qbItemRugCleaningPadListId; }
            set { m_qbItemRugCleaningPadListId = value; }
        }

        #endregion

        #region QuickBooksItemRugCleaningProtectantListId

        private static string m_qbItemRugCleaningProtectantListId;
        public static string QuickBooksItemRugCleaningProtectantListId
        {
            get { return m_qbItemRugCleaningProtectantListId; }
            set { m_qbItemRugCleaningProtectantListId = value; }
        }

        #endregion

        #region QuickBooksItemRugCleaningRepairsListId

        private static string m_qbItemRugCleaningRepairsListId;
        public static string QuickBooksItemRugCleaningRepairsListId
        {
            get { return m_qbItemRugCleaningRepairsListId; }
            set { m_qbItemRugCleaningRepairsListId = value; }
        }

        #endregion

        #region  QuickBooksItemRugCleaningMothListId

        private static string m_qbItemRugCleaningMothListId;
        public static string QuickBooksItemRugCleaningMothListId
        {
            get { return m_qbItemRugCleaningMothListId; }
            set { m_qbItemRugCleaningMothListId = value; }
        }

        #endregion

        #region QuickBooksItemRugCleaningWrapListId

        private static string m_qbItemRugCleaningWrapListId;
        public static string QuickBooksItemRugCleaningWrapListId
        {
            get { return m_qbItemRugCleaningWrapListId; }
            set { m_qbItemRugCleaningWrapListId = value; }
        }

        #endregion

        #region QuickBooksItemRugCleaningRevenueListId

        private static string m_qbItemRugCleaningRevenueListId;
        public static string QuickBooksItemRugCleaningRevenueListId
        {
            get { return m_qbItemRugCleaningRevenueListId; }
            set { m_qbItemRugCleaningRevenueListId = value; }
        }

        #endregion 

        #region QuickBooksItemRugCleaningStorageListId

        private static string m_qbItemRugCleaningStorageListId;
        public static string QuickBooksItemRugCleaningStorageListId
        {
            get { return m_qbItemRugCleaningStorageListId; }
            set { m_qbItemRugCleaningStorageListId = value; }
        }

        #endregion

        #region QuickBooksItemRugCleaningDiscountListId

        private static string m_qbItemRugCleaningDiscountListId;
        public static string QuickBooksItemRugCleaningDiscountListId
        {
            get { return m_qbItemRugCleaningDiscountListId; }
            set { m_qbItemRugCleaningDiscountListId = value; }
        }

        #endregion

        #region QuickBooksItemRugCleaningMinimumChargeListId

        private static string m_qbItemRugCleaningMinimumChargeListId;
        public static string QuickBooksItemRugCleaningMinimumChargeListId
        {
            get { return m_qbItemRugCleaningMinimumChargeListId; }
            set { m_qbItemRugCleaningMinimumChargeListId = value; }
        }

        #endregion 

        #region QuickBooksItemTaxRateListId

        private static string m_qbItemTaxRateListId;
        public static string QuickBooksItemTaxRateListId
        {
            get { return m_qbItemTaxRateListId; }
            set { m_qbItemTaxRateListId = value; }
        }

        #endregion 

        #region QuickBooksItemDefloodRevenueListId

        private static string m_qbItemDefloodRevenueListId;
        public static string QuickBooksItemDefloodRevenueListId
        {
            get { return m_qbItemDefloodRevenueListId; }
            set { m_qbItemDefloodRevenueListId = value; }
        }

        #endregion

        #endregion

        #region EmailTemplates

        private static string m_emailTemplatesBaseDirectory;
        public static string EmailTemplatesBaseDirectory
        {
            get { return m_emailTemplatesBaseDirectory; }
            set { m_emailTemplatesBaseDirectory = value; }
        }

        private static string m_emailTemplatesRugCleaningCompletedTemplate;
        public static string EmailTemplatesRugCleaningCompletedTemplate
        {
            get { return m_emailTemplatesRugCleaningCompletedTemplate; }
            set { m_emailTemplatesRugCleaningCompletedTemplate = value; }
        }

        private static string m_emailTemplatesProjectFeedbackReceivedTemplate;
        public static string EmailTemplatesProjectFeedbackReceivedTemplate
        {
            get { return m_emailTemplatesProjectFeedbackReceivedTemplate; }
            set { m_emailTemplatesProjectFeedbackReceivedTemplate = value; }
        }

        private static string m_emailTemplatesProjectFeedbackProcessedTemplate;
        public static string EmailTemplatesProjectFeedbackProcessedTemplate
        {
            get { return m_emailTemplatesProjectFeedbackProcessedTemplate; }
            set { m_emailTemplatesProjectFeedbackProcessedTemplate = value; }
        }

        private static string m_emailTemplatesRugCleaningRemiderTemplate;
        public static string EmailTemplatesRugCleaningReminderTemplate
        {
            get { return m_emailTemplatesRugCleaningRemiderTemplate; }
            set { m_emailTemplatesRugCleaningRemiderTemplate = value; }
        }

        private static string m_emailTemplatesRugCleaningRemiderFeedbackOffer;
        public static string EmailTemplatesRugCleaningReminderFeedbackOffer
        {
            get { return m_emailTemplatesRugCleaningRemiderFeedbackOffer; }
            set { m_emailTemplatesRugCleaningRemiderFeedbackOffer = value; }
        }


        private static string m_emailTemplatesDefloodCompletedTemplate;
        public static string EmailTemplatesDefloodCompletedTemplate
        {
            get { return m_emailTemplatesDefloodCompletedTemplate; }
            set { m_emailTemplatesDefloodCompletedTemplate = value; }
        }

        private static string m_emailTemplatesLeadReceivedTemplate;
        public static string EmailTemplatesLeadReceivedTemplate
        {
            get { return m_emailTemplatesLeadReceivedTemplate; }
            set { m_emailTemplatesLeadReceivedTemplate = value; }
        }

        private static string m_emailTemplatesLeadReceivedMarketingTemplate;
        public static string EmailTemplatesLeadReceivedMarketingTemplate
        {
            get { return m_emailTemplatesLeadReceivedMarketingTemplate; }
            set { m_emailTemplatesLeadReceivedMarketingTemplate = value; }
        }

        public static string EmailTemplatesPartnerSiteInvitation { get; set; }
        public static string EmailTemplatesPartnerSitePasswordReminder { get; set; }

        #endregion

        #region LoadGlobalConfiguration

        public static void LoadGlobalConfiguration()
        {
            LoadGlobalConfiguration(true);
        }

        public static void LoadGlobalConfiguration(bool requireCertificate)
        {
            // Configuration
            XmlDocument xmlConfig = OpenXmlDocument();
            App.Load(xmlConfig);

            try
            {
                if (requireCertificate)
                    InitClientCertificate();

                if (xmlConfig["application"] == null)
                    return;

                if (xmlConfig["application"]["database"] != null)
                {
                    m_connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                    m_servmanConnectionString = xmlConfig["application"]["database"].GetAttribute("ServmanConnectionString");                    
                }

                if (xmlConfig["application"]["settings"] != null)
                    m_webServiceUrl = xmlConfig["application"]["settings"].GetAttribute("WebServiceUrl");

                if (xmlConfig["application"]["sync"] != null)
                {
                    m_connectionKeyEnc = xmlConfig["application"]["sync"].GetAttribute("ConnectionKeyEnc");
                    if (m_connectionKeyEnc != string.Empty)
                        m_connectionKey = Crypto.Decrypt("dqa", m_connectionKeyEnc);

                    string ticketImportInterval = xmlConfig["application"]["sync"].GetAttribute("TicketImportInterval");
                    if (ticketImportInterval != null && ticketImportInterval != string.Empty)
                        m_ticketImportInterval = int.Parse(ticketImportInterval);

                    string customerImportInterval = xmlConfig["application"]["sync"].GetAttribute("CustomerImportInterval");
                    if (customerImportInterval != null && customerImportInterval != string.Empty)
                        m_customerImportInterval = int.Parse(customerImportInterval);

                    string exportInterval = xmlConfig["application"]["sync"].GetAttribute("ExportInterval");
                    if (exportInterval != null && exportInterval != string.Empty)
                        m_exportInterval = int.Parse(exportInterval);

                    string workingTimeStart = xmlConfig["application"]["sync"].GetAttribute("WorkingTimeStart");
                    if (workingTimeStart != null && workingTimeStart != string.Empty)
                        m_workingTimeStart = DateTime.Parse(workingTimeStart);

                    string workingTimeEnd = xmlConfig["application"]["sync"].GetAttribute("WorkingTimeEnd");
                    if (workingTimeEnd != null && workingTimeEnd != string.Empty)
                        m_workingTimeEnd = DateTime.Parse(workingTimeEnd);                    
                }

                if (xmlConfig["application"]["Digium"] != null)
                {
                    m_digiumApiUrl = xmlConfig["application"]["Digium"].GetAttribute("DigiumApiUrl");
                    m_digiumLogin = xmlConfig["application"]["Digium"].GetAttribute("DigiumLogin");
                    m_digiumPassword = xmlConfig["application"]["Digium"].GetAttribute("DigiumPassword");
                    m_partnerSiteUrl = xmlConfig["application"]["Digium"].GetAttribute("PartnerSiteUrl");
                    m_partnerSiteEmailFrom = xmlConfig["application"]["Digium"].GetAttribute("PartnerSiteEmailFrom");

                    if (xmlConfig["application"]["Digium"].GetAttribute("DigiumDateStart") != string.Empty)
                        DigiumDateStart = DateTime.Parse(xmlConfig["application"]["Digium"].GetAttribute("DigiumDateStart"));
                    if (xmlConfig["application"]["Digium"].GetAttribute("DigiumDateEnd") != string.Empty)
                        DigiumDateEnd = DateTime.Parse(xmlConfig["application"]["Digium"].GetAttribute("DigiumDateEnd"));

                    DigiumIncomingVoiceFilesFolder1 = xmlConfig["application"]["Digium"].GetAttribute("DigiumIncomingVoiceFilesFolder1");
                    DigiumIncomingVoiceFilesFolder2 = xmlConfig["application"]["Digium"].GetAttribute("DigiumIncomingVoiceFilesFolder2");
                    DigiumOutgoingVoiceFilesFolder = xmlConfig["application"]["Digium"].GetAttribute("DigiumOutgoingVoiceFilesFolder");
                    DigiumLameFolder = xmlConfig["application"]["Digium"].GetAttribute("DigiumLameFolder");

                    if (xmlConfig["application"]["Digium"].GetAttribute("DigiumCallImportRequeryMin") != string.Empty)
                        DigiumCallImportRequeryMin = int.Parse(xmlConfig["application"]["Digium"].GetAttribute("DigiumCallImportRequeryMin"));
                    if (xmlConfig["application"]["Digium"].GetAttribute("DigiumTransactionImportDelayMin") != string.Empty)
                        DigiumTransactionImportDelayMin = int.Parse(xmlConfig["application"]["Digium"].GetAttribute("DigiumTransactionImportDelayMin"));
                    if (xmlConfig["application"]["Digium"].GetAttribute("DigiumOutdatedVoiceFileMin") != string.Empty)
                        DigiumOutdatedVoiceFileMin = int.Parse(xmlConfig["application"]["Digium"].GetAttribute("DigiumOutdatedVoiceFileMin"));
                }

                if (xmlConfig["application"]["publicWebSite"] != null)
                {
                    if (xmlConfig["application"]["publicWebSite"].GetAttribute("DalworthRestorationCustomerFeedbackUrl") != string.Empty)
                        m_dalworthRestorationCustomerFeedbackUrl =
                            xmlConfig["application"]["publicWebSite"].GetAttribute("DalworthRestorationCustomerFeedbackUrl");
                }

                if (xmlConfig["application"]["misc"] != null)
                {
                    XmlElement miscElement = xmlConfig["application"]["misc"];
                    m_lastLogin = miscElement.GetAttribute("LastLogin");
                    m_customerSearchDelay = miscElement.GetAttribute("CustomerSearchDelay");
                    m_libertyMutualAdsourceIds = miscElement.GetAttribute("LibertyMutualAdsourceIds");
                    m_stateFarmAdsourceIds = miscElement.GetAttribute("StateFarmAdsourceIds");
                }

                if (xmlConfig["application"]["log"] != null)
                {
                    string logUserActions = xmlConfig["application"]["log"].GetAttribute("LogUserActions");
                    if (!string.IsNullOrEmpty(logUserActions))
                        m_logUserActions = bool.Parse(logUserActions);

                    string flushEveryRecord = xmlConfig["application"]["log"].GetAttribute("FlushEveryRecord");
                    if (!string.IsNullOrEmpty(flushEveryRecord))
                        m_flushEveryRecord = bool.Parse(flushEveryRecord);                    
                }

                if (xmlConfig["application"]["printing"] != null)
                {                    
                    XmlElement printingElement = xmlConfig["application"]["printing"];
                    m_visitPrinter = printingElement.GetAttribute("VisitPrinter");
                    m_reportPrinter = printingElement.GetAttribute("ReportPrinter");
                    if (printingElement.GetAttribute("AutomatedVisitPrint") != string.Empty)
                        m_automatedVisitPrint = bool.Parse(printingElement.GetAttribute("AutomatedVisitPrint"));
                    if (printingElement.GetAttribute("TomorrowVisitsPrintTime") != string.Empty)
                        m_tomorrowVisitsPrintTime =
                            DateTime.Parse(printingElement.GetAttribute("TomorrowVisitsPrintTime"));
                }

                if (xmlConfig["application"]["smtp"] != null)
                {
                    XmlElement emailElement = xmlConfig["application"]["smtp"];
                    m_smtpHost = emailElement.GetAttribute("Host");
                    m_smtpPort = int.Parse(emailElement.GetAttribute("Port"));
                    m_smtpLogin = emailElement.GetAttribute("Login");
                    m_smtpPassword = emailElement.GetAttribute("Password");
                    m_smtpFromAddress = emailElement.GetAttribute("FromAddress");
                    m_smtpDisplayName = emailElement.GetAttribute("DisplayName");
                    m_smtpOnCallAddress = emailElement.GetAttribute("OnCallAddress");
                    m_smtpOnCallDisplayName = emailElement.GetAttribute("OnCallDisplayName");
                    m_smtpMarketingAddress = emailElement.GetAttribute("MarketingAddress");
                    m_smtpMarketingDisplayName = emailElement.GetAttribute("MarketingDisplayName");
                    m_smtpLateLeadsAddress = emailElement.GetAttribute("LateLeadsAddress");
                    m_smtpLateLeadsDisplayName = emailElement.GetAttribute("LateLeadsDisplayName");
                    m_smtpFeedbackAddress = emailElement.GetAttribute("FeedbackAddress");
                    m_smtpFeedbackDisplayName = emailElement.GetAttribute("FeedbackDisplayName");
                }

                if (xmlConfig["application"]["quickbooks"] != null)
                {
                    XmlElement qbElement = xmlConfig["application"]["quickbooks"];
                    m_qbAppName = qbElement.GetAttribute("appName");
                    m_qbAppID = qbElement.GetAttribute("appID");
                    m_qbCompanyFile = qbElement.GetAttribute("CompanyFile");
                    m_qbItemRugCleaningCostListId = qbElement.GetAttribute("ItemRugCleaningCostListId");
                    m_qbItemRugCleaningCostFloodListId = qbElement.GetAttribute("ItemRugCleaningCostFloodListId");
                    m_qbItemRugCleaningPadListId = qbElement.GetAttribute("ItemRugCleaningPadListId");
                    m_qbItemRugCleaningProtectantListId = qbElement.GetAttribute("ItemRugCleaningProtectantListId");
                    m_qbItemRugCleaningRepairsListId = qbElement.GetAttribute("ItemRugCleaningRepairsListId");
                    m_qbItemRugCleaningMothListId = qbElement.GetAttribute("ItemRugCleaningMothListId");
                    m_qbItemRugCleaningWrapListId = qbElement.GetAttribute("ItemRugCleaningWrapListId");
                    m_qbItemRugCleaningRevenueListId = qbElement.GetAttribute("ItemRugCleaningRevenueListId");
                    m_qbItemRugCleaningStorageListId = qbElement.GetAttribute("ItemRugCleaningStorageListId");
                    m_qbItemRugCleaningDiscountListId = qbElement.GetAttribute("ItemRugCleaningDiscountListId");
                    m_qbItemRugCleaningMinimumChargeListId = qbElement.GetAttribute("ItemRugCleaningMinimumChargeListId");
                    m_qbItemTaxRateListId = qbElement.GetAttribute("ItemTaxRateListId");
                    m_qbItemDefloodRevenueListId = qbElement.GetAttribute("ItemDefloodRevenueListId");

                    string qbLogLevel = qbElement.GetAttribute("LogLevel");
                    if (qbLogLevel == "DEBUG")
                        m_qbLogLevel = QuickbooksLogLevalEnum.Debug;
                    else 
                        m_qbLogLevel = QuickbooksLogLevalEnum.Error;
                }
                    
                if (xmlConfig["application"]["emailTemplates"] != null)
                {
                     XmlElement templatesElement = xmlConfig["application"]["emailTemplates"];
                     m_emailTemplatesBaseDirectory = templatesElement.GetAttribute("BaseDirectory");
                     m_emailTemplatesRugCleaningCompletedTemplate = templatesElement.GetAttribute("RugCleaningCompletedTemplate");
                     m_emailTemplatesDefloodCompletedTemplate = templatesElement.GetAttribute("DefloodCompletedTemplate");
                     m_emailTemplatesLeadReceivedTemplate = templatesElement.GetAttribute("LeadReceivedTemplate");
                     m_emailTemplatesLeadReceivedMarketingTemplate = templatesElement.GetAttribute("MarketingLeadReceivedTemplate");
                     m_emailTemplatesProjectFeedbackReceivedTemplate = templatesElement.GetAttribute("ProjectFeedbackReceivedTemplate");
                     m_emailTemplatesProjectFeedbackProcessedTemplate = templatesElement.GetAttribute("ProjectFeedbackProcessedTemplate");
                     m_emailTemplatesRugCleaningRemiderTemplate = templatesElement.GetAttribute("RugCleaningReminderTemplate");
                     m_emailTemplatesRugCleaningRemiderFeedbackOffer = templatesElement.GetAttribute("RugCleaningReminderFeedbackOffer");
                     EmailTemplatesPartnerSiteInvitation = templatesElement.GetAttribute("PartnerSiteInvitation");
                     EmailTemplatesPartnerSitePasswordReminder = templatesElement.GetAttribute("PartnerSitePasswordReminder");
                }

                //This loads the connection string with variables for CE
                //It appends to the full connection string for CE in the program startup
            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }
        }
        #endregion

        #region BitmapsDir

        public static string BitmapsDir()
        {
            return Host.GetPath(@"Bitmaps\");
        }

        #endregion

        #region Instance

        static Configuration s_configuration = new Configuration();

        public static Configuration Instance
        {
            get
            {
                return s_configuration;
            }
        }

        #endregion

        #region ConnectionString
        public static String ConnectionString
        {
            get
            {
                // Load settings
                return m_connectionString;
            }
            set
            {
                // Saving settings
                m_connectionString = value;
            }
        }
        #endregion

        #region ServmanConnectionString
        public static String ServmanConnectionString
        {
            get
            {
                // Load settings
                return m_servmanConnectionString;
            }
            set
            {
                // Saving settings
                m_servmanConnectionString = value;
            }
        }
        #endregion        

        #region Login & Password

        private static string m_login;        
        public static string Login
        {
            get { return m_login; }
            set { m_login = value; }
        }

        private static string m_password;
        public static string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        #endregion        

        #region AppNameFullPath
        public static string AppNameFullPath
        {
            get
            {
                // Load settings
                return Host.GetPath(String.Empty);
            }
            /*set
            {
                // Saving settings
                _appNameFullPath = value;
            }*/
        }
        #endregion

        #region DBFullPath
        public static string DBFullPath
        {
            get
            {
                // Load settings
                return m_dbFullPath;
            }
            set
            {
                // Saving settings
                m_dbFullPath = value;
            }
        }
        #endregion

        #region DBLogFullPath
        public static string DBLogFullPath
        {
            get
            {
                // Load settings
                return m_dbLogFullPath;
            }
            set
            {
                // Saving settings
                m_dbLogFullPath = value;
            }
        }
        #endregion


        public static XmlDocument OpenXmlDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(Host.GetPath(FILE_PATH));

            return xmlDocument;
        }

        public static void Save()
        {
            XmlDocument xmlDocument = OpenXmlDocument();

            App.Save(xmlDocument);

            XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application/settings");
            if (xmlElement == null)
                throw new DalworthException("Invalid configuration file");

            XmlElement miscNode = (XmlElement)xmlDocument.SelectSingleNode(@"/application/misc");
            if (miscNode != null)
            {
                miscNode.SetAttribute("LastLogin", m_lastLogin);
            }

            xmlDocument.Save(Host.GetPath(FILE_PATH));
        }

        public static void RemoveConfigReadOnlyAttribute()
        {
            FileInfo fileInfo = new FileInfo(Host.GetPath(FILE_PATH));
            
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                fileInfo.Attributes -= FileAttributes.ReadOnly;
        }
    }

    public enum QuickbooksLogLevalEnum
    {
        Debug, 
        Error
    }
}
