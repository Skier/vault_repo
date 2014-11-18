using System;
  
namespace Dalworth.Server.Domain
{
    public enum ProjectConstructionProgressEnum
    {
        Lead = 1,
        Job = 2,
        Declined = 3,
        PaidInFull = 4
    }

    public partial class ProjectConstructionProgress
    {
        public ProjectConstructionProgress(){}

        #region GetText

        public static string GetText(ProjectConstructionProgressEnum progress)
        {
            if (progress == ProjectConstructionProgressEnum.PaidInFull)
                return "Paid In Full";
            else
                return progress.ToString();
        }

        #endregion
    }
}
