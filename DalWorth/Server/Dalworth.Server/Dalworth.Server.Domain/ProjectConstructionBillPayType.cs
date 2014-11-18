using System;

namespace Dalworth.Server.Domain
{
    public enum ProjectConstructionBillPayTypeEnum
    {
        Invoice = 1,
        Payment = 2,
        Credit = 3
    }
    
    public partial class ProjectConstructionBillPayType
    {
        public ProjectConstructionBillPayType() { }

        #region GetText

        public static string GetText(ProjectConstructionBillPayTypeEnum billPayType)
        {
            return billPayType.ToString();
        }

        #endregion
    }
}
      