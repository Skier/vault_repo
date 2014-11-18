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
        public DocAttachmentInfo[] Attachments;

        #endregion

        #region Constructors

        public DocumentInfo() {
        }

        public DocumentInfo(
                        int docID,
                        bool isPublic,
                        int docTypeId,
                        string volume,
                        string page,
                        string documentNo,
                        int county,
                        int state,
                        int dateFiledYear,
                        int dateFiledMonth,
                        int dateFiledDay,
                        int dateSignedYear,
                        int dateSignedMonth,
                        int dateSignedDay,
                        string researchNote,
                        string imageLink ) {
            DocID = docID;
            IsPublic = isPublic;
            DocTypeId = docTypeId;
            Volume = volume;
            Page = page;
            DocumentNo = documentNo;
            County = county;
            State = state;
            DateFiledYear = dateFiledYear;
            DateFiledMonth = dateFiledMonth;
            DateFiledDay = dateFiledDay;
            DateSignedYear = dateSignedYear;
            DateSignedMonth = dateSignedMonth;
            DateSignedDay = dateSignedDay;
            ResearchNote = researchNote;
            ImageLink = imageLink;
                        }

        #endregion
        
        public DateTime DateFilled {
            get {
                DateTime result = DateTime.MinValue;

                try {
                    result = new DateTime(DateFiledYear, DateFiledMonth, DateFiledDay);
                } catch {}
                
                return result;
            }
        }

        public DateTime DateSigned {
            get {
                DateTime result = DateTime.MinValue;
                
                try {
                    return new DateTime(DateSignedYear, DateSignedMonth, DateSignedDay);
                } catch {}
                
                return result;
            }
        }
        
    }
}
