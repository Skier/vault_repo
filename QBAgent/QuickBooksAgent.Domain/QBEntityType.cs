using System;

namespace QuickBooksAgent.Domain
{
    public partial class QBEntityType
    {
        public static QBEntityType Employee = new QBEntityType(0, "Employee");
        public static QBEntityType Vendor = new QBEntityType(1, "Vendor");
        public static QBEntityType Customer = new QBEntityType(2, "Customer");
        
        public QBEntityType(){}

        public static bool operator ==(QBEntityType arg1, QBEntityType arg2)
        {
            return arg1.Equals(arg2);
        }
        
        public static bool operator !=(QBEntityType arg1, QBEntityType arg2)
        {
            return !arg1.Equals(arg2);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is QBEntityType)
                return (obj as QBEntityType).QBEntityTypeId == QBEntityTypeId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
      