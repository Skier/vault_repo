using System;

namespace MobileTech.Domain
{
    public enum BusinessTransactionStatusEnum
    {
        Created  = 1,
        Commited = 2
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    public partial class BusinessTransactionStatus
    {
        public BusinessTransactionStatus()
        {

        }

        public static BusinessTransactionStatus Find(BusinessTransactionStatusEnum status)
        {
            return FindByPrimaryKey((int)status);
        }
    }
}
