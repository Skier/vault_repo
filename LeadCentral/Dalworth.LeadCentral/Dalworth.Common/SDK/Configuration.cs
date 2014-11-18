using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Dalworth.Common.SDK
{    
    public class Configuration
    {
        public const string ConfigurationFilePath = @"\Dalworth.Common.Win32.xml";

        private readonly ApplicationConfiguration m_appConfiguration = new ApplicationConfiguration();
        private readonly SyncronizationConfiguration m_syncConfiguration = new SyncronizationConfiguration();
        private readonly LeadCentralCommonConfiguration m_leadCentralCommonConfiguration = new LeadCentralCommonConfiguration();

        #region ApplicationConfiguration

        [XmlRoot("Application")]
        public class ApplicationConfiguration
        {
            public ApplicationConfiguration()
            {
                Trace = true;
            }

            [XmlAttribute]
            public bool Trace { get; set; } 
                                               
            public void Load(XmlDocument xmlDocument)
            {
                var xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new DalworthException("Invalid configuration file");

                Trace = Boolean.Parse(xmlElement.GetAttribute("Trace"));
            }

            public void Save(XmlDocument xmlDocument)
            {
                var xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new DalworthException("Invalid configuration file");

                xmlElement.SetAttribute("Trace", Trace.ToString());
            }
        }

        #endregion

        #region SyncronizationConfiguration

        public class SyncronizationConfiguration
        {
            public string EmailTemplateDir { get; set; }
            public string EmailTemplateCreateLead { get; set; }
            public string EmailTemplateLowBalance { get; set; }
            public string EmailNotificationFrom { get; set; }
            public int SyncInterval { get; set; }
            public string CallDirectory { get; set; }
           
            public void Load(XmlElement xmlElement)
            {
                EmailTemplateDir = xmlElement.GetAttribute("EmailTemplateDir");
                EmailTemplateCreateLead = xmlElement.GetAttribute("EmailTemplateCreateLead");
                EmailTemplateLowBalance = xmlElement.GetAttribute("EmailTemplateLowBalance");
                EmailNotificationFrom = xmlElement.GetAttribute("EmailNotificationFrom");
                SyncInterval = int.Parse(xmlElement.GetAttribute("SyncInterval"));
                CallDirectory = xmlElement.GetAttribute("CallDirectory");
            }
        }

        #endregion

        #region LeadCentralCommonConfiguration

        public class LeadCentralCommonConfiguration
        {
            public string CallUrl { get; set;}

            public void Load(XmlElement xmlElement)
            {
                CallUrl = xmlElement.GetAttribute("CallUrl");
            }
        }

        #endregion

        #region Fields

        public static String ConnectionString { get; set; }

        public static ApplicationConfiguration App
        {
            get
            {
                return Instance.m_appConfiguration;
            }
        }

        public static SyncronizationConfiguration  Sync
        {
            get { return Instance.m_syncConfiguration; }
        }

        public static LeadCentralCommonConfiguration LeadCentralCommon
        {
            get { return Instance.m_leadCentralCommonConfiguration; }
        }
        
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
            var xmlConfig = OpenXmlDocument();

            App.Load(xmlConfig);

            try
            {
                ConnectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");

                var sectionElement = xmlConfig["application"]["sync"];
                if (sectionElement != null)
                    Sync.Load(sectionElement);

                sectionElement = xmlConfig["application"]["lead_central_common"];
                if (sectionElement != null)
                    LeadCentralCommon.Load(sectionElement);

            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }
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

        #region OpenXmlDocument

        private static XmlDocument OpenXmlDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(Host.GetPath(ConfigurationFilePath));

            return xmlDocument;
        }

        #endregion

        #region RemoveConfigReadOnlyAttribute

        public static void RemoveConfigReadOnlyAttribute()
        {
            var fileInfo = new FileInfo(Host.GetPath(ConfigurationFilePath));
            
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                fileInfo.Attributes -= FileAttributes.ReadOnly;
        }

        #endregion
    }
}
