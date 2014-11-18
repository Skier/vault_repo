using System;
  
namespace Dalworth.Server.Domain
{
    public enum ProjectStatusEnum
    {
        Open = 1,
        Completed = 2,
    }

    public partial class ProjectStatus
    {
        public ProjectStatus(){}

        #region GetText

        public static string GetText(ProjectStatusEnum projectStatus)
        {
            if (projectStatus == ProjectStatusEnum.Open)
                return "Open";
            else if (projectStatus == ProjectStatusEnum.Completed)
                return "Closed";
            return string.Empty;
        }

        #endregion
    }
}
      