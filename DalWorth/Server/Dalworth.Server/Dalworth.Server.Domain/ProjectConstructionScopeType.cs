using System;
  
namespace Dalworth.Server.Domain
{
    public enum ProjectConstructionScopeTypeEnum
    {
        Scope = 1,
        ScopeEstimate = 2,
        ChangeOrder = 3
    }

    public partial class ProjectConstructionScopeType
    {
        public ProjectConstructionScopeType(){}

        #region GetText

        public static string GetText(ProjectConstructionScopeTypeEnum scopeType)
        {
            if (scopeType == ProjectConstructionScopeTypeEnum.ScopeEstimate)
                return "Scope Estimate";
            else if (scopeType == ProjectConstructionScopeTypeEnum.ChangeOrder)
                return "Change Order";
            else
                return scopeType.ToString();
        }

        #endregion
    }
}
      