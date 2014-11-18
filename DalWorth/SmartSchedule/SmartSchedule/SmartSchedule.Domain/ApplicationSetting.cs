using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SmartSchedule.Domain
{
    public enum ApplicationSyncState
    {
        StartDayDone,
        PartialSyncDone,
        SyncDone
    }

    public partial class ApplicationSetting
    {
        public ApplicationSetting(){ }

        #region GetSyncState

        public static ApplicationSyncState GetSyncState(ApplicationSetting setting)
        {
            if (setting == null)
                return ApplicationSyncState.StartDayDone;
            if (setting.Note == ApplicationSyncState.PartialSyncDone.ToString())
                return ApplicationSyncState.PartialSyncDone;
            return ApplicationSyncState.SyncDone;
        }

        public static ApplicationSyncState GetSyncState()
        {
            return GetSyncState(FindApplicationSetting());
        }

        #endregion

        #region FindApplicationSetting

        public static ApplicationSetting FindApplicationSetting()
        {
            List<ApplicationSetting> settings = Find();
            Debug.Assert(settings.Count <= 1, "Multiple app settings found");

            if (settings.Count == 1)
                return settings[0];
            return null;
        }

        #endregion
    }
}
      