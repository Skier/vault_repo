using System;
  
namespace Dalworth.Domain
{
    public enum TaskStatusEnum
    {
        NotCompleted = 1,
        Completed = 2
    }

    public partial class TaskStatus
    {
        public TaskStatus(){ }

        #region GetText

        public static string GetText(TaskStatusEnum status)
        {
            if (status == TaskStatusEnum.Completed)
                return "Completed";
            else if (status == TaskStatusEnum.NotCompleted)
                return "Not Completed";
            return string.Empty;
        }

        #endregion
    }
}
      