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
using Microsoft.WindowsMobile.PocketOutlook;
using QuickBooksAgent.Data;

namespace QuickBooksAgent
{

    public class DatePeriod
    {
        private int? m_daysCount;
        private string m_description;

        public int? DaysCount
        {
            get { return m_daysCount; }
            set { m_daysCount = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public DatePeriod(int? daysCount, string description)
        {
            m_daysCount = daysCount;
            m_description = description;
        }

        public DatePeriod(int? daysCount) : this(daysCount, string.Empty) { }

        public override int GetHashCode()
        {
            return m_daysCount.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            DatePeriod datePeriod = obj as DatePeriod;
            if (datePeriod == null) return false;
            if (!Equals(m_daysCount, datePeriod.m_daysCount)) return false;
            return true;
        }
    }    
    
    public class Configuration
    {

        QuickBooksConfiguration m_quickBooks = new QuickBooksConfiguration();
        ApplicationConfiguration m_app = new ApplicationConfiguration();

        public static ApplicationConfiguration App
        {
            get
            {
                return Instance.m_app;
            }
        }

        public static QuickBooksConfiguration QuickBooks
        {
            get
            {
                return Instance.m_quickBooks;
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

            String m_license;
            [XmlAttribute]
            public String License
            {
                get { return m_license; }
                set { m_license = value; }
            }
                                    
            private string m_firstName;
            [XmlAttribute]
            public string FirstName
            {
                get { return m_firstName; }
                set { m_firstName = value; }
            }

            private string m_lastName;
            [XmlAttribute]
            public string LastName
            {
                get { return m_lastName; }
                set { m_lastName = value; }
            }

            private string m_company;
            [XmlAttribute]
            public string Company
            {
                get { return m_company; }
                set { m_company = value; }
            }
            
            private string m_userType;
            [XmlAttribute]
            public string UserType
            {
                get { return m_userType; }
                set { m_userType = value; }
            }

            private int m_userId;
            [XmlAttribute]
            public int UserId
            {
                get { return m_userId; }
                set { m_userId = value; }
            }            

            private bool m_useUserIdentification;
            [XmlAttribute]
            public bool UseUserIdentification
            {
                get { return m_useUserIdentification; }
                set { m_useUserIdentification = value; }
            }

            private int? m_emailSettingsType;
            [XmlAttribute]
            public int? EmailSettingsType
            {
                get { return m_emailSettingsType; }
                set { m_emailSettingsType = value; }
            }

            private string m_outlookAccount;
            [XmlAttribute]
            public string OutlookAccount
            {
                get { return m_outlookAccount; }
                set { m_outlookAccount = value; }
            }

            private string m_smtpServer;
            [XmlAttribute]
            public string SmtpServer
            {
                get { return m_smtpServer; }
                set { m_smtpServer = value; }
            }

            private int? m_smtpPort;
            [XmlAttribute]
            public int? SmtpPort
            {
                get { return m_smtpPort; }
                set { m_smtpPort = value; }
            }
            
            private string m_emailFrom;
            [XmlAttribute]
            public string EmailFrom
            {
                get { return m_emailFrom; }
                set { m_emailFrom = value; }
            }


            private string GenerateLicense(string firstName, string lastName, string company)
            {
                MD5 md5 = MD5.Create();
                string s = string.Format("G$G%*@94HE5{0}&312[{1}]4hS3h!4g%{2}*30^41", firstName.ToLower(), 
                    lastName.ToLower(), company.ToLower());
                byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                Int64 value = BitConverter.ToInt64(result, 0);

                if (value < 0)
                    value *= -1;
                string valueString = value.ToString();

                if (valueString.Length < 19)
                {
                    for (int i = valueString.Length; i < 19; i++)
                        valueString += '0';
                }
                else if (valueString.Length > 19)
                {
                    valueString = valueString.Substring(0, 19);
                }

                valueString = valueString.Insert(5, "-");
                valueString = valueString.Insert(11, "-");
                valueString = valueString.Insert(17, "-");

                return valueString;                
            }
            
            public bool IsLicenseValid(string license, string firstName, string lastName, string company)
            {
                return (license == GenerateLicense(firstName, lastName, company));
            }
            
            public bool IsLicensed()
            {
                if (m_license != string.Empty && IsLicenseValid(m_license, m_firstName, m_lastName, m_company))
                    return true;
                else
                    return false;
            }
            
            public void Load(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new QuickBooksAgentException("Invalid configuration file");


                m_license = xmlElement.GetAttribute("License");
                m_firstName = xmlElement.GetAttribute("FirstName");
                m_lastName = xmlElement.GetAttribute("LastName");
                m_company = xmlElement.GetAttribute("Company");
                
                m_trace = Boolean.Parse(xmlElement.GetAttribute("Trace"));

                xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application/UserIdentification");
                if (xmlElement == null)
                    throw new QuickBooksAgentException("Invalid configuration file");

                m_userType = xmlElement.GetAttribute("UserType");
                
                if (xmlElement.GetAttribute("UserId") != null && xmlElement.GetAttribute("UserId") != string.Empty)
                    m_userId = int.Parse(xmlElement.GetAttribute("UserId"));

                if (xmlElement.GetAttribute("Use") != null && xmlElement.GetAttribute("Use") != string.Empty)
                    m_useUserIdentification = bool.Parse(xmlElement.GetAttribute("Use"));

                //email
                string emailSettingsType = xmlElement.GetAttribute("EmailSettingsType");
                try
                {
                    m_emailSettingsType = int.Parse(emailSettingsType);
                }
                catch (Exception)
                {
                    m_emailSettingsType = null;
                }

                if (m_emailSettingsType < 1 || m_emailSettingsType > 2)
                    m_emailSettingsType = null;

                m_outlookAccount = xmlElement.GetAttribute("OutlookAccount");
                m_smtpServer = xmlElement.GetAttribute("SmtpServer");
                
                string smtpPort = xmlElement.GetAttribute("SmtpPort");
                try
                {
                    m_smtpPort = int.Parse(smtpPort);
                }
                catch (Exception)
                {
                    m_smtpPort = null;
                }

                if (m_smtpPort < 0 || m_smtpPort > 65535)
                    m_smtpPort = null;

                m_emailFrom = xmlElement.GetAttribute("EmailFrom");
            }
            
            /// <summary>
            /// Returns string.empty if email settings are ok, or error message otherwise
            /// </summary>
            /// <returns></returns>
            public string CheckEmailSettings()
            {
                if (m_emailSettingsType == null)
                    return "E-mail settings are not set. Please check 'Setup -> Application'";
                if (m_emailSettingsType == 1)
                {
                    OutlookSession session = new OutlookSession();
                    foreach (EmailAccount account in session.EmailAccounts)
                    {
                        if (account.Name == m_outlookAccount)
                            return string.Empty;
                    }
                    session.Dispose();

                    if (m_outlookAccount == string.Empty)
                    {
                        return
                            "You haven't chosen Outlook account for sending mail. Please check E-Mail settings (Setup -> Application)";
                    } else
                    {
                        return
                            string.Format("Outlook account '{0}' doesn't exist. Please use another one", m_outlookAccount);
                    }

                } else if (m_emailSettingsType == 2)
                {
                    if (m_smtpServer == string.Empty)
                        return "You haven't specified SMTP server. Please check E-Mail settings (Setup -> Application)";
                    
                    if (m_smtpPort == null)
                    {
                        return "You haven't specified SMTP server port or it has incorrect value. Please check E-Mail settings (Setup -> Application)";
                    } 
                    
                    if (m_emailFrom == string.Empty)
                        return "You haven't specified 'Email From'. Please check E-Mail settings (Setup -> Application)";
                    else
                    {
                        Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                        if (!regex.IsMatch(m_emailFrom))
                        {
                            return "'Email From' setting has invalid email address. Please check E-Mail settings (Setup -> Application)";
                        }                        
                    }
                }

                return string.Empty;
            }

            public void Save(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application");

                if (xmlElement == null)
                    throw new QuickBooksAgentException("Invalid configuration file");

                xmlElement.SetAttribute("License", m_license);
                xmlElement.SetAttribute("FirstName", m_firstName);
                xmlElement.SetAttribute("LastName", m_lastName);
                xmlElement.SetAttribute("Company", m_company);
                
                xmlElement.SetAttribute("Trace", m_trace.ToString());

                xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application/UserIdentification");

                if (xmlElement == null)
                    throw new QuickBooksAgentException("Invalid configuration file");

                xmlElement.SetAttribute("UserType", UserType);
                xmlElement.SetAttribute("UserId", UserId.ToString());
                xmlElement.SetAttribute("Use", UseUserIdentification.ToString());                
                
                xmlElement.SetAttribute("EmailSettingsType", m_emailSettingsType.HasValue? m_emailSettingsType.ToString() : string.Empty);
                xmlElement.SetAttribute("OutlookAccount", m_outlookAccount);
                xmlElement.SetAttribute("SmtpServer", m_smtpServer);
                xmlElement.SetAttribute("SmtpPort", m_smtpPort.HasValue? m_smtpPort.ToString() : string.Empty);
                xmlElement.SetAttribute("EmailFrom", m_emailFrom);
            }
        }

        [XmlRoot("QuickBooks")]
        public class QuickBooksConfiguration
        {
            String m_appCode;
            [XmlAttribute]
            public String AppCode
            {
                get { return m_appCode; }
                set { m_appCode = value; }
            }

            String m_appLogin;
            [XmlAttribute]
            public String AppLogin
            {
                get { return m_appLogin; }
                set { m_appLogin = value; }
            }

            DateTime? m_lastSyncDate;
            [XmlAttribute]
            public DateTime? LastSyncDate
            {
                get { return m_lastSyncDate; }
                set { m_lastSyncDate = value; }
            }

            int? m_transactionFreshnessDays;
            [XmlAttribute]
            public int? TransactionFreshnessDays
            {
                get { return m_transactionFreshnessDays; }
                set { m_transactionFreshnessDays = value; }
            }

            int? m_transactionFreshnessDaysLastSync;
            [XmlAttribute]
            public int? TransactionFreshnessDaysLastSync
            {
                get { return m_transactionFreshnessDaysLastSync; }
                set { m_transactionFreshnessDaysLastSync = value; }
            }

            private DateTime m_currentSyncTime;
            public DateTime CurrentSyncTime
            {
                get { return m_currentSyncTime; }
                set { m_currentSyncTime = value; }
            }

            private bool m_isNewlyCreatedDB;
            public bool IsNewlyCreatedDB
            {
                get { return m_isNewlyCreatedDB; }
                set { m_isNewlyCreatedDB = value; }
            }
            
            private DateTime? m_webAccessLastTime;
            public DateTime? WebAccessLastTime
            {
                get { return m_webAccessLastTime; }
                set { m_webAccessLastTime = value; }
            }

            public DateTime? EntityLastSyncDate
            {
                get
                {
                    if (m_isNewlyCreatedDB)
                        return null;
                    return LastSyncDate;
                }
            }
            
            
            public DateTime? TransactionLastSyncDate
            {
                get
                {
                    if (IsNewlyCreatedDB || !m_lastSyncDate.HasValue)
                    {
                        if (m_transactionFreshnessDays == null)
                            return null;
                        else
                            return m_currentSyncTime.AddDays(-m_transactionFreshnessDays.Value);
                    }
                    
                    if (m_transactionFreshnessDays == null && m_transactionFreshnessDaysLastSync == null)
                        return m_lastSyncDate;
                    else if (m_transactionFreshnessDays != null && m_transactionFreshnessDaysLastSync == null)
                    {
                        if (m_lastSyncDate < m_currentSyncTime.AddDays(-m_transactionFreshnessDays.Value))
                            return m_currentSyncTime.AddDays(-m_transactionFreshnessDays.Value);
                        else
                            return m_lastSyncDate;                            
                    } 
                    else if (m_transactionFreshnessDays == null && m_transactionFreshnessDaysLastSync != null)
                    {
                        return null;
                    }
                    else //Both values exist
                    {
                        if (m_transactionFreshnessDays == m_transactionFreshnessDaysLastSync)
                        {
                            return m_lastSyncDate;
                        } else if (m_transactionFreshnessDays < m_transactionFreshnessDaysLastSync)
                        {
                            if (m_lastSyncDate < m_currentSyncTime.AddDays(-m_transactionFreshnessDays.Value))
                                return m_currentSyncTime.AddDays(-m_transactionFreshnessDays.Value);
                            else
                                return m_lastSyncDate;                            
                            
                        } else
                        {
                            return m_currentSyncTime.AddDays(-m_transactionFreshnessDays.Value);
                        }
                    }
                                            
                }
            }
            
            private String m_connectionTicket;
            [XmlElement]
            public String ConnectionTicket
            {
                get { return m_connectionTicket; }
                set { m_connectionTicket = value; }
            }


            String m_defaultLogin;
            [XmlElement]
            public String DefaultLogin
            {
                get { return m_defaultLogin; }
                set { m_defaultLogin = value; }
            }

            public void Load(XmlDocument xmlDocument)
            {                
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application/QuickBooks");                
                
                if (xmlElement == null)
                    throw new QuickBooksAgentException("Invalid configuration file");

                AppCode = xmlElement.GetAttribute("AppCode");
                AppLogin = xmlElement.GetAttribute("AppLogin");                

                if (xmlElement.GetAttribute("LastSyncDate") != string.Empty)
                    LastSyncDate = DateTime.Parse(xmlElement.GetAttribute("LastSyncDate"), QBDataType.USCulture);
                else
                    LastSyncDate = null;

                if (xmlElement.GetAttribute("TransactionFreshnessDays") != string.Empty)
                    TransactionFreshnessDays = int.Parse(xmlElement.GetAttribute("TransactionFreshnessDays"));
                else
                    TransactionFreshnessDays = null;

                if (xmlElement.GetAttribute("TransactionFreshnessDaysLastSync") != string.Empty)
                    TransactionFreshnessDaysLastSync = int.Parse(xmlElement.GetAttribute("TransactionFreshnessDaysLastSync"));
                else
                    TransactionFreshnessDaysLastSync = null;

                ConnectionTicket = xmlElement.SelectSingleNode("ConnectionTicket").InnerText;

                DefaultLogin = xmlElement.SelectSingleNode("DefaultLogin").InnerText;

            }

            public void Save(XmlDocument xmlDocument)
            {
                XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application/QuickBooks");

                if(xmlElement == null)
                    throw new QuickBooksAgentException("Invalid configuration file");

                xmlElement.SetAttribute("AppCode", AppCode);
                xmlElement.SetAttribute("AppLogin", AppLogin);                
                
                if (LastSyncDate.HasValue)
                    xmlElement.SetAttribute("LastSyncDate", LastSyncDate.Value.ToString("yyyy-MM-ddThh:mm:ss"));
                else
                    xmlElement.SetAttribute("LastSyncDate", string.Empty);

                if (TransactionFreshnessDays.HasValue)
                    xmlElement.SetAttribute("TransactionFreshnessDays", TransactionFreshnessDays.ToString());
                else
                    xmlElement.SetAttribute("TransactionFreshnessDays", string.Empty);

                if (TransactionFreshnessDaysLastSync.HasValue)
                    xmlElement.SetAttribute("TransactionFreshnessDaysLastSync", TransactionFreshnessDaysLastSync.ToString());
                else
                    xmlElement.SetAttribute("TransactionFreshnessDaysLastSync", string.Empty);
                
                xmlElement.SelectSingleNode("ConnectionTicket").InnerText = ConnectionTicket;
                xmlElement.SelectSingleNode("DefaultLogin").InnerText = DefaultLogin;
            }
        }

        public const String URL_TICKET = "https://apps.quickbooks.com/j/AppGateway";

#if WINCE
        public const string FILE_PATH = @"\Q-Agent.WinCE.xml";
#else
        public const string FILE_PATH = @"\Q-Agent.Win32.xml";
#endif
        #region Fields
        //Global Variables

        private static bool m_initDB = false;
        private static string m_language = String.Empty;
        private static string m_appNameFullPath = String.Empty;
        private static string m_connectionString = String.Empty;

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

        #region LoadGlobalConfiguration
        public static void LoadGlobalConfiguration()
        {
            // Configuration
            XmlDocument xmlConfig = OpenXmlDocument();

            App.Load(xmlConfig);
            QuickBooks.Load(xmlConfig);

            try
            {
                m_initDB = bool.Parse(xmlConfig["application"]["settings"].GetAttribute("InitDB"));                
                m_language = xmlConfig["application"]["settings"].GetAttribute("Language").ToLower();
                //This loads the connection string with variables for CE
                //It appends to the full connection string for CE in the program startup
                m_connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                m_masterConnectionString = xmlConfig["application"]["database"].GetAttribute("MasterConnectionString");
            }
            catch (Exception ex)
            {
                throw new QuickBooksAgentException(ex);
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


        public String QBConnectionURLTicked
        {
            get
            {
                return URL_TICKET;
            }
        }

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
            QuickBooks.Save(xmlDocument);

            XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode(@"/application/settings");

            if (xmlElement == null)
                throw new QuickBooksAgentException("Invalid configuration file");

            xmlElement.SetAttribute("InitDB", m_initDB.ToString());

            xmlDocument.Save(Host.GetPath(FILE_PATH));
        }

        public static void RemoveConfigReadOnlyAttribute()
        {
            FileInfo fileInfo = new FileInfo(Host.GetPath(FILE_PATH));
            
            if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                fileInfo.Attributes -= System.IO.FileAttributes.ReadOnly;
        }
    }
}
