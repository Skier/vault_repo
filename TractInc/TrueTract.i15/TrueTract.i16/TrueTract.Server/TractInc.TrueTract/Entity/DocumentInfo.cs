using System;

namespace TractInc.TrueTract.Entity
{
    public class DocumentInfo
    {
    
        #region Fields

        public int DocID;
        public bool IsPublic;
        public int DocTypeId;
        public string Volume;
        public string Page;
        public string DocumentNo;
        public int County;
        public int State;
        public int DateFiledYear;
        public int DateFiledMonth;
        public int DateFiledDay;
        public int DateSignedYear;
        public int DateSignedMonth;
        public int DateSignedDay;
        public string ResearchNote;
        public string ImageLink;
        public DateTime Created;
        public int CreatedBy;
        public string DocBranchUid;
        public bool IsActive;

        public int TractsCount;
        public double TractsAcres;

        public string CreatedByName;

        //These fields are filling on the client to minimize database access
        public string StateName;
        public string CountyName;
        public string DocumentTypeName;
        public string SellerRoleName;
        public string BuyerRoleName;
        //---

        public TractInfo[] TractList;
        public ParticipantInfo Buyer;
        public ParticipantInfo Seller;

        #endregion

        #region Constructors

        public DocumentInfo() {
        }

        #endregion

        #region Properties

        public DateTime Filed {
            get {
                DateTime result = DateTime.MinValue;

                try {
                    result = new DateTime(DateFiledYear, DateFiledMonth, DateFiledDay);
                } catch {}
                
                return result;
            }
            set
            {
                DateFiledDay = value.Day;
                DateFiledMonth = value.Month;
                DateFiledYear = value.Year;
            }
        }

        public DateTime Signed {
            get {
                DateTime result = DateTime.MinValue;
                
                try {
                    return new DateTime(DateSignedYear, DateSignedMonth, DateSignedDay);
                } catch {}
                
                return result;
            }
            set
            {
                DateSignedDay = value.Day;
                DateSignedMonth = value.Month;
                DateSignedYear = value.Year;
            }
        }

        #endregion
        
    }
}
