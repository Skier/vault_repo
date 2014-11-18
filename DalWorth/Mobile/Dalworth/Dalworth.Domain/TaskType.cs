using System;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public enum TaskTypeEnum
    {
        RugPickup = 1,
        RugDelivery = 2,
        Unknown = 3
    }

    public partial class TaskType
    {
        public TaskType(){ }

        #region GetText

        public static string GetText(TaskTypeEnum taskType)
        {
            if (taskType == TaskTypeEnum.RugDelivery)
                return "Rug Delivery";
            else if (taskType == TaskTypeEnum.RugPickup)
                return "Rug Pickup";
            else if (taskType == TaskTypeEnum.Unknown)
                return "Unknown";

            throw new DalworthException("No text representation for " + taskType);
        }

        #endregion
    }
}
      