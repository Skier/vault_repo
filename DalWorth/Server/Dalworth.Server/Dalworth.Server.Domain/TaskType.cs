using System;
  
namespace Dalworth.Server.Domain
{
    public enum TaskTypeEnum
    {
        RugPickup = 1,
        RugDelivery = 2,
        Unknown = 3,
        Deflood = 4,
        Monitoring = 5,
        Miscellaneous = 6,
        Help = 7
    }

    public partial class TaskType
    {
        public TaskType(){ }

        #region GetText

        public static string GetText(TaskTypeEnum taskType)
        {
            if (taskType == TaskTypeEnum.RugDelivery)
                return "Rug Delivery";
            if (taskType == TaskTypeEnum.RugPickup)
                return "Rug Pickup";
            return taskType.ToString();

        }

        #endregion
    }
}
      