using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Service.Sync.WcfServiceClient;

namespace SmartSchedule.Service.Sync
{
    [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CallbackListener : IWcfServiceCallback
    {
        public void OnOptimizationRequested(Schedule schedule){}
        private void OnOptimizerIterationDone() { }
        public void OnViewModelChanged(CallbackInfo info) { }

        #region ForceSync

        public void ForceSync(SyncTypeEnum syncType)
        {
            try
            {
                SyncWindowsService.DoImport();
                Host.Trace("SmartScheduleServmanSync", "Ticket Import Done");
            }
            catch (Exception ex)
            {
                Host.Trace("SmartScheduleServmanSync", "Unhandled exception: " + ex.Message + ex.StackTrace);
            }
        }

        #endregion


    }
}
