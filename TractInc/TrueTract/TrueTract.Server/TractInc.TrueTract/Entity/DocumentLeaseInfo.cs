using System;

namespace TractInc.TrueTract.Entity
{
    public class DocumentLeaseInfo
    {
        public int DocLeaseId;
        public int DocId;
        public string LCN;
        public int Term;
        public double Royalty;
        public DateTime EffectiveDate;
        public double Acreage;
        public string AliasGrantee;
        public DateTime ExpirationDate;
        public bool HBP;
    }
}
