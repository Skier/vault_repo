using System;


namespace MobileTech.Domain
{
    public enum InventoryTransactionTypeEnum
    {
        Load = 1,
        Unload = 2,
        Transfer = 3
    }

    public partial class InventoryTransactionType
    {
        public InventoryTransactionType()
        {

        }
    }
}
