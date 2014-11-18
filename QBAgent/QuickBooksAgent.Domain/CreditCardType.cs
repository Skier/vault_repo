namespace QuickBooksAgent.Domain
{
    public partial class CreditCardType
    {
        public static CreditCardType Credit = new CreditCardType(0, "Credit");
        public static CreditCardType Charge = new CreditCardType(1, "Charge");
        
        
        public CreditCardType() {}        

        public static bool operator ==(CreditCardType arg1, CreditCardType arg2)
        {
            return arg1.Equals(arg2);
        }

        public static bool operator !=(CreditCardType arg1, CreditCardType arg2)
        {
            return !arg1.Equals(arg2);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is CreditCardType)
                return (obj as CreditCardType).CreditCardTypeId == CreditCardTypeId;

            return false;
        }

        public override int GetHashCode()
        {
            return CreditCardTypeId.GetHashCode();
        }        
    }
}
      