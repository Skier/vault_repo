using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;

namespace SmartSchedule.Win32.WcfClient
{
    public class WcfClient : IDisposable
    {
        private WcfClient() { }

        #region ClientDate

        private static DateTime m_clientDate;
        public static DateTime ClientDate
        {
            get { return m_clientDate; }
        }

        #endregion

        #region AllDatesInBucket

        private static bool m_allDatesInBucket;
        public static bool AllDatesInBucket
        {
            get { return m_allDatesInBucket; }
        }

        #endregion

        public static void SetClientProperties(DateTime date)
        {
            SetClientProperties(date, m_allDatesInBucket);
        }

        public static void SetClientProperties(bool allDatesInBucket)
        {
            SetClientProperties(m_clientDate, allDatesInBucket);
        }

        public static void SetClientProperties(DateTime date, bool allDatesInBucket)
        {
            WcfServiceClient.WcfServiceClient instance = Instance; //make sure instance initialized
            m_clientDate = date.Date;
            m_allDatesInBucket = allDatesInBucket;
            m_instance.Subscribe(WcfSubscriberTypeEnum.Dashboard, m_clientDate, User.Current, m_allDatesInBucket);            
        }

        #region Instance

        private static WcfServiceClient.WcfServiceClient m_instance;
        public static WcfServiceClient.WcfServiceClient Instance
        {
            get
            {
                if (m_instance == null || m_instance.State != CommunicationState.Opened)
                {
                    DisposeInstance();

                    InstanceContext context = new InstanceContext(new WcfCallbackListener());
                    m_instance = new WcfServiceClient.WcfServiceClient(context);
                    m_instance.Subscribe(WcfSubscriberTypeEnum.Dashboard, m_clientDate, User.Current, m_allDatesInBucket);
                }

                return m_instance;
            }
        }

        #endregion

        private static void DisposeInstance()
        {
            if (m_instance != null)
            {
                if (m_instance.State == CommunicationState.Opened)
                {
                    try
                    {
                        m_instance.Unsubscribe();
                    }
                    catch(Exception){}
                }

                try
                {
                    m_instance.Close();
                }
                catch (Exception){}
            }                            
        }

        public void Dispose()
        {
            DisposeInstance();
        }
    }
}
