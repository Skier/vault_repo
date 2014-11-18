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
using Dalworth.Data;

namespace Dalworth.SDK
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


        public const string FILE_PATH = @"\Dalworth.WinCE.xml";
        
        #region Fields
        //Global Variables

        private static bool m_initDB = false;
        private static string m_language = String.Empty;
        private static string m_connectionString = String.Empty;
        private static string m_webServiceUrl = String.Empty;

        private static string m_connectionKeyEnc = string.Empty;
        private static string m_connectionKey = string.Empty;
        private static int m_sendAttemptPeriod = 0;
        private static int m_sendAttemptFailThreshold = 0;
        private static int m_checkIngoingVisitPeriod = 0;
        private static int m_checkIngoingVisitFailThreshold = 0;
        private static int m_gpsTrackPeriod = 0;
        private static string m_dispatcherPhoneNumber = string.Empty;

        private static string m_dbFullPath;
        private static string m_dbLogFullPath;
        private static string m_masterConnectionString = String.Empty;

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

        private static void InitClientCertificate()
        {
            StreamReader reader = new StreamReader(Host.GetPath(@"\Certificates\client.cer"));
            BinaryReader binaryReader = new BinaryReader(reader.BaseStream);
            byte[] data = binaryReader.ReadBytes(1000000);
            binaryReader.Close();
            m_clientCertificate = new X509Certificate(data);
        }

        #region LoadGlobalConfiguration
        public static void LoadGlobalConfiguration()
        {
            // Configuration
            XmlDocument xmlConfig = OpenXmlDocument();

            App.Load(xmlConfig);

            try
            {
                InitClientCertificate();

                m_initDB = bool.Parse(xmlConfig["application"]["settings"].GetAttribute("InitDB"));                
                m_language = xmlConfig["application"]["settings"].GetAttribute("Language").ToLower();
                //This loads the connection string with variables for CE
                //It appends to the full connection string for CE in the program startup
                m_connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                m_masterConnectionString = xmlConfig["application"]["database"].GetAttribute("MasterConnectionString");
                m_webServiceUrl = xmlConfig["application"]["settings"].GetAttribute("WebServiceUrl");

                m_connectionKeyEnc = xmlConfig["application"]["sync"].GetAttribute("ConnectionKeyEnc");
                m_sendAttemptPeriod = int.Parse(xmlConfig["application"]["sync"].GetAttribute("SendAttemptPeriod"));
                m_sendAttemptFailThreshold = int.Parse(xmlConfig["application"]["sync"].GetAttribute("SendAttemptFailThreshold"));
                m_checkIngoingVisitPeriod = int.Parse(xmlConfig["application"]["sync"].GetAttribute("CheckIngoingVisitPeriod"));
                m_checkIngoingVisitFailThreshold = int.Parse(xmlConfig["application"]["sync"].GetAttribute("CheckIngoingVisitFailThreshold"));
                m_gpsTrackPeriod = int.Parse(xmlConfig["application"]["sync"].GetAttribute("GpsTrackPeriod"));
                m_dispatcherPhoneNumber = xmlConfig["application"]["other"].GetAttribute("DispatcherPhoneNumber");
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

        #region MasterConnectionString
        public static String MasterConnectionString
        {
            get
            {
                // Load settings
                return m_masterConnectionString;
            }
            set
            {
                // Saving settings
                m_masterConnectionString = value;
            }
        }
        #endregion

        #region Language
        public static string Language
        {
            get
            {
                // Load settings
                return m_language;
            }
            set
            {
                // Saving settings
                m_language = value;
            }
        }
        #endregion  
     
        #region InitDB
        public static bool InitDB
        {
            get
            {
                return m_initDB;
            }
            set
            {
                m_initDB = value;
            }
        }
        #endregion

        #region WebServiceUrl

        public static string WebServiceUrl
        {
            get { return m_webServiceUrl; }
            set { m_webServiceUrl = value; }
        }

        #endregion

        #region CurrentTechnician

        private static int m_currentTechnicianId;
        public static int CurrentTechnicianId
        {
            get { return m_currentTechnicianId; }
            set { m_currentTechnicianId = value; }
        }

        #endregion

        #region ConnectionKeyEnc

        public static string ConnectionKeyEnc
        {
            get { return m_connectionKeyEnc; }
        }

        #endregion

        #region ConnectionKey

        public static string ConnectionKey
        {
            get { return m_connectionKey; }
            set { m_connectionKey = value; }
        }

        #endregion

        #region SendAttemptPeriod

        public static int SendAttemptPeriod
        {
            get { return m_sendAttemptPeriod; }
        }

        #endregion

        #region SendAttemptFailThreshold

        public static int SendAttemptFailThreshold
        {
            get { return m_sendAttemptFailThreshold; }
        }

        #endregion

        #region checkIngoingVisitPeriod

        public static int checkIngoingVisitPeriod
        {
            get { return m_checkIngoingVisitPeriod; }
        }

        #endregion

        #region checkIngoingVisitFailThreshold

        public static int checkIngoingVisitFailThreshold
        {
            get { return m_checkIngoingVisitFailThreshold; }
        }

        #endregion

        #region GpsTrackPeriod

        public static int GpsTrackPeriod
        {
            get { return m_gpsTrackPeriod; }
        }

        #endregion

        #region DispatcherPhoneNumber

        public static string DispatcherPhoneNumber
        {
            get { return m_dispatcherPhoneNumber; }
        }

        #endregion

        #region ClientCertificate

        private static X509Certificate m_clientCertificate;
        public static X509Certificate ClientCertificate
        {
            get { return m_clientCertificate; }
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

            xmlElement.SetAttribute("InitDB", m_initDB.ToString());

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
