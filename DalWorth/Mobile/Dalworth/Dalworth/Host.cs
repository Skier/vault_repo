using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Dalworth.SDK;

namespace Dalworth
{
    public delegate void EventInfoEventHandler(IEventInfo eventInfo);
    public delegate void CultureChangeHandler(CultureInfo cultureInfo);

    // Singletone
    public class Host
    {
        Configuration m_config;

        private CultureInfo m_cultureInfo;

        public event EventInfoEventHandler Events;

        private Host()
        {
            m_config = Configuration.Instance;

            m_cultureInfo = AvailableCultures[0];
        }


        public void AddEvent(IEventInfo eventInfo)
        {
#if DEBUG
            Debug.WriteLine(eventInfo);
#endif

            if (Events != null)
            {
                Events.Invoke(eventInfo);
            }
        }

        public static Configuration Configuration
        {
            get
            {
                return Instance.m_config;
            }
        }

        private static Host s_instance;        
        public static Host Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new Host();
                }

                return s_instance;
            }
        }


        public static CultureInfo[] AvailableCultures
        {
            get
            {
                CultureInfo[] cultures = new CultureInfo[4];

                cultures[0] = new CultureInfo("en");
                cultures[1] = new CultureInfo("ru");
                cultures[2] = new CultureInfo("es");
                cultures[3] = new CultureInfo("fr");

                return cultures;
            }
        }


        public event CultureChangeHandler CultureChange;

        public CultureInfo Culture
        {
            get
            {
                return m_cultureInfo;
            }
            set
            {
                m_cultureInfo = value;

                if (Instance.CultureChange != null)
                    Instance.CultureChange.Invoke(m_cultureInfo);
            }
        }


        static String s_appPath = String.Empty;

        public static String GetPath()
        {
            if (s_appPath == String.Empty)
                s_appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

            return s_appPath;
        }

        public static String GetPath(String path)
        {

            return String.Format("{0}\\{1}", GetPath(), path);
        }


        public static void SetPath(String path)
        {
            s_appPath = path;
        }

        TextWriter m_log;

        public static TextWriter LogFileWriter
        {
            get { return Instance.m_log; }
            set { Instance.m_log = value; }
        }

        public static void WriteToLogFile(String logger, String text)
        {
            LogFileWriter.WriteLine(
                String.Format("{0},{1},{2}", DateTime.Now.ToString(),
                logger,
                text));
        }

        public static void Trace(String logger, String text)
        {
            Debug.WriteLine(logger + "   " + text);
            
            if (Configuration.App.Trace)            
                WriteToLogFile(logger, text);            
        }
    }

    public enum FileLoggerEnum
    {
        DatabaseEngine,
        Core,
        Loader,
        Debug,
        DatabaseLogReflection,
        MainMenu
    }
}
