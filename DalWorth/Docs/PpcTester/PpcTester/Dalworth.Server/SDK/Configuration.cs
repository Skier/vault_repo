using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Dalworth.Server.Data;

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
                                               
            public void Load(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new DalworthException("Invalid configuration file");
                
                m_trace = Boolean.Parse(xmlElement.GetAttribute("Trace"));
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
            set { m_smtpDisplayName = value; }
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

        #endregion

        #region LoadGlobalConfiguration
        public static void LoadGlobalConfiguration()
        {
            // Configuration
            XmlDocument xmlConfig = OpenXmlDocument();

            App.Load(xmlConfig);

            try
            {
                InitClientCertificate();
                m_connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                m_servmanConnectionString = xmlConfig["application"]["database"].GetAttribute("ServmanConnectionString");
                m_webServiceUrl = xmlConfig["application"]["settings"].GetAttribute("WebServiceUrl");
                if (xmlConfig["application"]["sync"] != null)
                {
                    m_connectionKeyEnc = xmlConfig["application"]["sync"].GetAttribute("ConnectionKeyEnc");

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
                    
                if (xmlConfig["application"]["misc"] != null)
                {
                    XmlElement miscElement = xmlConfig["application"]["misc"];
                    m_lastLogin = miscElement.GetAttribute("LastLogin");
                    m_customerSearchDelay = miscElement.GetAttribute("CustomerSearchDelay");
                    m_libertyMutualAdsourceIds = miscElement.GetAttribute("LibertyMutualAdsourceIds");
                    m_stateFarmAdsourceIds = miscElement.GetAttribute("StateFarmAdsourceIds");
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
                    m_smtpFeedbackAddress = emailElement.GetAttribute("FeedbackAddress");
                    m_smtpFeedbackDisplayName = emailElement.GetAttribute("FeedbackDisplayName");
                }
                    
                if (xmlConfig["application"]["emailTemplates"] != null)
                {
                     XmlElement templatesElement = xmlConfig["application"]["emailTemplates"];
                     m_emailTemplatesBaseDirectory = templatesElement.GetAttribute("BaseDirectory");
                     m_emailTemplatesRugCleaningCompletedTemplate = templatesElement.GetAttribute("RugCleaningCompletedTemplate");
                     m_emailTemplatesDefloodCompletedTemplate = templatesElement.GetAttribute("DefloodCompletedTemplate");
                     m_emailTemplatesLeadReceivedTemplate = templatesElement.GetAttribute("LeadReceivedTemplate");
                     m_emailTemplatesProjectFeedbackReceivedTemplate = templatesElement.GetAttribute("ProjectFeedbackReceivedTemplate");
                     m_emailTemplatesRugCleaningRemiderTemplate = templatesElement.GetAttribute("RugCleaningReminderTemplate");
                     m_emailTemplatesRugCleaningRemiderFeedbackOffer = templatesElement.GetAttribute("RugCleaningReminderFeedbackOffer");

                }
                //TODO:
                if (m_connectionKeyEnc != string.Empty)
                    m_connectionKey = Crypto.Decrypt("dqa", m_connectionKeyEnc);

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
}
