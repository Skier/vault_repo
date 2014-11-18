using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Service.WcfServiceClient;

namespace SmartSchedule.Service.WcfClient
{
    public class WcfClient : IDisposable
    {
        private WcfClient() { }
        
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
                    m_instance.Subscribe(WcfSubscriberTypeEnum.Service, DateTime.MinValue.Date);
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

    [CallbackBehavior(UseSynchronizationContext = false)]
    public class WcfCallbackListener : IWcfServiceCallback
    {
        public void OnViewModelChanged(CallbackInfo info)
        {
            
        }

        public void OnOptimizationRequested(Schedule schedule)
        {
            
        }
    }

}
