using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Dalworth.Common.SDK;

namespace Dalworth.Common
{
    public delegate void EventInfoEventHandler(IEventInfo eventInfo);
    public delegate void CultureChangeHandler(CultureInfo cultureInfo);

    // Singletone
    public class Host
    {
        readonly Configuration Config;

        private CultureInfo CurrentCulture;

        public event EventInfoEventHandler Events;

        private Host()
        {
            Config = Configuration.Instance;

            CurrentCulture = AvailableCultures[0];
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
                return Instance.Config;
            }
        }

        private static Host CurrentInstance;        
        public static Host Instance
        {
            get { return CurrentInstance ?? (CurrentInstance = new Host()); }
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
                return CurrentCulture;
            }
            set
            {
                CurrentCulture = value;

                if (Instance.CultureChange != null)
                    Instance.CultureChange.Invoke(CurrentCulture);
            }
        }


        static String AppPath = String.Empty;

        public static String GetPath()
        {
            if (AppPath == String.Empty)
                AppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

            return AppPath;
        }

        public static String GetPath(String path)
        {

            return String.Format("{0}\\{1}", GetPath(), path);
        }


        public static void SetPath(String path)
        {
            AppPath = path;
        }

        TextWriter Log;

        public static TextWriter LogFileWriter
        {
            get { return Instance.Log; }
            set { Instance.Log = value; }
        }

        public static void WriteToLogFile(String logger, String text)
        {
            try
            {
                LogFileWriter.WriteLine(
                    String.Format("{0},{1},{2}", DateTime.Now,
                                  logger,
                                  text));
                LogFileWriter.Flush();
            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }
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
