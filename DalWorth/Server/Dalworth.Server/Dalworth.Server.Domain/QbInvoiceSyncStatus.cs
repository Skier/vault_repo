using System;
  
namespace Dalworth.Server.Domain
{
    public enum QbInvoiceSyncStatusEnum
    {
        NeedSync = 1,
        Synchronizing =3, 
        Synchronized = 4
    }

    public partial class QbInvoiceSyncStatus
    {
        public QbInvoiceSyncStatus() { }

        #region GetText

        public static string GetText(QbInvoiceSyncStatusEnum syncStatus)
        {
            if (syncStatus == QbInvoiceSyncStatusEnum.NeedSync)
                return "Need Sync";
            return syncStatus.ToString();            
        }

        #endregion
    }
}
      