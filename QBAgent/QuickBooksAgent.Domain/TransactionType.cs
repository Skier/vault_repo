using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class TransactionType
    {
        public static TransactionType ARRefundCreditCard    = new TransactionType(0 , "ARRefundCreditCard");
        public static TransactionType Bill                  = new TransactionType(1 , "Bill"); 
        public static TransactionType BillPaymentCheck      = new TransactionType(2 , "BillPaymentCheck");
        public static TransactionType BillPaymentCreditCard = new TransactionType(3 , "BillPaymentCreditCard");
        public static TransactionType BuildAssembly         = new TransactionType(4 , "BuildAssembly");
        public static TransactionType Charge                = new TransactionType(5 , "Charge");      
        public static TransactionType Check                 = new TransactionType(6 , "Check");
        public static TransactionType CreditCardCharge      = new TransactionType(7 , "CreditCardCharge");
        public static TransactionType CreditCardCredit      = new TransactionType(8 , "CreditCardCredit");
        public static TransactionType CreditMemo            = new TransactionType(9 , "CreditMemo");
        public static TransactionType Deposit               = new TransactionType(10, "Deposit");         
        public static TransactionType Estimate              = new TransactionType(11, "Estimate");            
        public static TransactionType InventoryAdjustment   = new TransactionType(12, "InventoryAdjustment");
        public static TransactionType Invoice               = new TransactionType(13, "Invoice");
        public static TransactionType ItemReceipt           = new TransactionType(14, "ItemReceipt");          
        public static TransactionType JournalEntry          = new TransactionType(15, "JournalEntry");         
        public static TransactionType LiabilityAdjustment   = new TransactionType(16, "LiabilityAdjustment");  
        public static TransactionType Paycheck              = new TransactionType(17, "Paycheck");             
        public static TransactionType PayrollLiabilityCheck = new TransactionType(18, "PayrollLiabilityCheck");
        public static TransactionType PurchaseOrder         = new TransactionType(19, "PurchaseOrder");        
        public static TransactionType ReceivePayment        = new TransactionType(20, "ReceivePayment");       
        public static TransactionType SalesOrder            = new TransactionType(21, "SalesOrder");           
        public static TransactionType SalesReceipt          = new TransactionType(22, "SalesReceipt");         
        public static TransactionType SalesTaxPaymentCheck  = new TransactionType(23, "SalesTaxPaymentCheck");
        public static TransactionType Transfer              = new TransactionType(24, "Transfer");             
        public static TransactionType VendorCredit          = new TransactionType(25, "VendorCredit");
        public static TransactionType YTDAdjustment         = new TransactionType(26, "YTDAdjustment");
        public static TransactionType Payment               = new TransactionType(27, "Payment");
        
        public TransactionType() {}

        #region Equals & HashCode

        public static bool operator ==(TransactionType arg1, TransactionType arg2)
        {
            return arg1.Equals(arg2);
        }

        public static bool operator !=(TransactionType arg1, TransactionType arg2)
        {
            return !arg1.Equals(arg2);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is TransactionType)
                return (obj as TransactionType).TransactionTypeId == TransactionTypeId;

            return false;
        }

        public override int GetHashCode()
        {
            return TransactionTypeId.GetHashCode();
        }

        #endregion

        #region FindBy Description

        private const string SqlFindDescription =
            @"SELECT TransactionTypeId, TransactionTypeDescription 
            FROM TransactionType
                WHERE TransactionTypeDescription = @TransactionTypeDescription";

        public static TransactionType FindBy(string description)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindDescription))
            {
                Database.PutParameter(dbCommand, "@TransactionTypeDescription", description);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("TransactionType " + description + " not found");
                }
            }
        }

        #endregion                
        
    }
}
      