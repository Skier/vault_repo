using System;
  
namespace Dalworth.Server.Domain
{
    public enum WorkTransactionPaymentTypeEnum
    {
        CreditCard = 1,
        BankCheck = 2,
        Cash = 3
    }

    public partial class WorkTransactionPaymentType
    {
        public WorkTransactionPaymentType(){}
    }
}
      