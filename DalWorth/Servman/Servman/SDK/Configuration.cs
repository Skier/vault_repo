using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Servman.Data;

namespace Servman.SDK
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


        public const string FILE_PATH = @"\Dalworth.LeadCentral.Win32.xml";
        
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

        private static int m_syncInterval = 0;
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

        public static int SyncInterval
        {
            get { return m_syncInterval; }
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

        #region LoadGlobalConfiguration
        public static void LoadGlobalConfiguration()
        {
            // Configuration
            XmlDocument xmlConfig = OpenXmlDocument();

            App.Load(xmlConfig);

            try
            {
                m_connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                m_servmanConnectionString = xmlConfig["application"]["database"].GetAttribute("ServmanConnectionString");
                m_webServiceUrl = xmlConfig["application"]["settings"].GetAttribute("WebServiceUrl");
                if (xmlConfig["application"]["sync"] != null)
                {
                    m_connectionKeyEnc = xmlConfig["application"]["sync"].GetAttribute("ConnectionKeyEnc");

                    string syncInterval = xmlConfig["application"]["sync"].GetAttribute("SyncInterval");
                    if (syncInterval != null && syncInterval != string.Empty)
                        m_syncInterval = int.Parse(syncInterval);

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
