using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.WcfServiceClient;

namespace SmartSchedule.Win32.WcfClient
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class WcfCallbackListener : IWcfServiceCallback
    {
        private static WcfCallbackListenerData m_data = new WcfCallbackListenerData();
        
        public static bool HasPendingChanges()
        {
            lock (m_data)
            {
                return m_data.PendingChanges.Count > 0;
            }                        
        }

        public static bool IsSequenceValid()
        {
            lock (m_data)
            {
                return !m_data.LastProcessedCallbackId.HasValue
                    || m_data.PendingChanges.Peek().Id == m_data.LastProcessedCallbackId + 1;
            }
        }

        public static void Reset(long lastProcessedCallbackId)
        {
            lock (m_data)
            {
                m_data.LastProcessedCallbackId = lastProcessedCallbackId;
                m_data.PendingChanges.Clear();
            }            
        }

        public static CallbackInfo Read()
        {
            lock (m_data)
            {
                CallbackInfo result = m_data.PendingChanges.Dequeue();
                m_data.LastProcessedCallbackId = result.Id;
                return result;
            }
        }

        public void OnViewModelChanged(CallbackInfo info)
        {
            lock (m_data)
            {
                m_data.PendingChanges.Enqueue(info);
            }            
        }

        public void OnOptimizationRequested(Schedule schedule)
        {
            
        }

        public void ForceSync(SyncTypeEnum syncType)
        {
            
        }

        private class WcfCallbackListenerData
        {
            private long? m_lastProcessedCallbackId;
            private Queue<CallbackInfo> m_pendingChanges;

            public WcfCallbackListenerData()
            {
                m_pendingChanges = new Queue<CallbackInfo>();
            }

            public Queue<CallbackInfo> PendingChanges
            {
                get { return m_pendingChanges; }
            }

            public long? LastProcessedCallbackId
            {
                get { return m_lastProcessedCallbackId; }
                set { m_lastProcessedCallbackId = value; }
            }
        }
    }
}
