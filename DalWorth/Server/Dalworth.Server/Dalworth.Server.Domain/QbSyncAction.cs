using System;
 
namespace Dalworth.Server.Domain
{
    public enum QbSyncActionEnum
    {
        CustomerAdd=1,
        JobAdd=2,
        InvoiceAdd=3,
        CustomerMod=5,
        JobMod=6,
        InvoiceVoid=7,
        PaymentAdd=8,
        PaymentMod =9,
        CreditMemoAdd = 10,
        CreditMemoMod = 11,

        SkipCustomer = 100, 
        SkipJob = 101,
        SkipInvoice = 102,
        DontSyncCustomer = 103,
        DontSyncInvoice = 104,
        DontSyncJob = 105
    }

    public partial class QbSyncAction
    {
        public QbSyncAction()
        {

        }
    }
}
      