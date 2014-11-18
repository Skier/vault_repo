using System;


namespace MobileTech.Domain
{
    public enum BusinessTransactionTypeEnum
    {
        Customer = 1,
        Period = 2,
        Inventory = 3
    }

    public partial class BusinessTransactionType
    {
        public BusinessTransactionType()
        {

        }

        public static BusinessTransactionType Find(BusinessTransactionTypeEnum type)
        {
            return FindByPrimaryKey((int)type);
        }
    }
}
