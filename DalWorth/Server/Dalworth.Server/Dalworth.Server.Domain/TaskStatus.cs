using System;
  
namespace Dalworth.Server.Domain
{
    public enum TaskStatusEnum
    {
        NotCompleted = 1,
        Completed = 2,
        RugDeliveryCreated = 3,
        InProcess = 4
    }

    public partial class TaskStatus
    {
        public TaskStatus(){}

        #region GetText

        public static string GetText(TaskStatusEnum taskStatus)
        {
            if (taskStatus == TaskStatusEnum.NotCompleted)
                return "Not Started";
            if (taskStatus == TaskStatusEnum.Completed)
                return "Completed";
            if (taskStatus == TaskStatusEnum.RugDeliveryCreated)
                return "Completed";
            if (taskStatus == TaskStatusEnum.InProcess)
                return "In Process";

            return string.Empty;

        }

        #endregion
    }
}
      