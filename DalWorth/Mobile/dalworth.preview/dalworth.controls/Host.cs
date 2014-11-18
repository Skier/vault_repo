//#define WINCE
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace MobileTech
{
    public delegate void CultureChangeHandler(CultureInfo cultureInfo);

	// Singletone
	public class Host
	{

        private CultureInfo m_cultureInfo;


        static private bool m_vc;
        static private bool m_sync;
        static public bool Sync
        {
            get { return m_sync; }
            set { m_sync = value; }
        }
        static public bool VC
        {
            get { return m_vc; }
            set { m_vc = value; }
        }

		private Host()
		{			

            m_cultureInfo = AvailableCultures[0];
		}




		private static Host m_instance;
        private const String ThreadSlotName = "Host";
		public static Host Instance
		{
			get 
			{
				if (m_instance == null)
				{
                    LocalDataStoreSlot slot = Thread.GetNamedDataSlot(ThreadSlotName);

					if (!(Thread.GetData(slot) is Host))
					{
						m_instance = new Host();
					
						Thread.SetData(slot, m_instance);
					}
					else
						m_instance = (Host)Thread.GetData(slot);

				}

				return m_instance;
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

	}


}
