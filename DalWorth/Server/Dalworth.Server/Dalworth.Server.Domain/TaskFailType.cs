using System;
  
namespace Dalworth.Server.Domain
{
    public enum TaskFailTypeEnum
    {
        MustReturn = 1,
        Cancel = 3
    }

    public partial class TaskFailType
    {
        public TaskFailType() {}

        #region GetText

        public static string GetText(TaskFailTypeEnum taskFailType)
        {
            if (taskFailType == TaskFailTypeEnum.MustReturn)
                return "Failed Must Return";
            else if (taskFailType == TaskFailTypeEnum.Cancel)
                return "Cancelled";
            return string.Empty;
        }

        #endregion
    }
}
      