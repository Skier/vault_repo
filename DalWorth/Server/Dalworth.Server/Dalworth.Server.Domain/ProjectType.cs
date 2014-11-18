using System;
  
namespace Dalworth.Server.Domain
{
    public enum ProjectTypeEnum
    {
        NotSpecified =      0,
        RugCleaning =       1,
        Deflood =           2,
        Miscellaneous =     3,
        Construction =      4,
        Content =           5,
        BasementSystems =   6
    }

    public partial class ProjectType
    {
        public ProjectType(){ }

        #region GetText

        public static string GetText(ProjectTypeEnum projectType)
        {
            if (projectType == ProjectTypeEnum.RugCleaning)
                return "Rug Cleaning";
            return projectType.ToString();
        }

        #endregion
    }
}
      