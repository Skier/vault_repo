using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using TractInc.Server.Data;

namespace TractInc.Server.SDK
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
                    throw new TractIncException("Invalid configuration file");
                
                m_trace = Boolean.Parse(xmlElement.GetAttribute("Trace"));
            }
            

            public void Save(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new TractIncException("Invalid configuration file");
                
                xmlElement.SetAttribute("Trace", m_trace.ToString());
            }
        }


        public const string FILE_PATH = @"\TractInc.Server.Win32.xml";
        
        #region Fields
        //Global Variables

        private static bool m_initDB = false;
        private static string m_language = String.Empty;
        private static string m_connectionString = String.Empty;

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

        #region LoadGlobalConfiguration
        public static void LoadGlobalConfiguration()
        {
            // Configuration
            XmlDocument xmlConfig = OpenXmlDocument();

            App.Load(xmlConfig);

            try
            {
                m_initDB = bool.Parse(xmlConfig["application"]["settings"].GetAttribute("InitDB"));                
                m_language = xmlConfig["application"]["settings"].GetAttribute("Language").ToLower();
                m_connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                //This loads the connection string with variables for CE
                //It appends to the full connection string for CE in the program startup
            }
            catch (Exception ex)
            {
                throw new TractIncException(ex);
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
                throw new TractIncException("Invalid configuration file");

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
