using System;

namespace TractInc.TrueTract.Entity
{
    public class DocumentInfo
    {
        private const string XML_TEMPLATE = @"<document itemtype=""document"" id=""{0}"" type=""{1}"" volume=""{2}"" page=""{3}"" docNo=""{4}"" state=""{5}"" county=""{6}"" researchNote=""{7}"" sellerRole=""{8}"" buyerRole=""{9}""><seller>{10}</seller><buyer>{11}</buyer><attachments>{12}</attachments><tracts>{13}</tracts></document>";

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
        public int PreviousVersion;

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

        public string DocTypeName;
        public DocumentAttachmentInfo[] Attachments;
        public TractInfo[] Tracts;
        public ParticipantInfo Buyer;
        public ParticipantInfo Seller;

        public DocumentReferenceInfo[] References;

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

        #region Methods
        
        public string toXml()
        {
            string seller = String.Empty;
            if (Seller != null)
                seller += Seller.toXml();

            string buyer = String.Empty;
            if (Buyer != null)
                buyer += Buyer.toXml();

            string attachments = String.Empty;
            foreach (DocumentAttachmentInfo attachment in Attachments)
            {
                attachments += attachment.toXml();
            }

            string tracts = String.Empty;
            foreach (TractInfo tract in Tracts)
            {
                tracts += tract.toXml();
            }

            return String.Format(XML_TEMPLATE,
                                 DocID,
                                 DocTypeName,
                                 XmlString.validate(Volume),
                                 XmlString.validate(Page),
                                 XmlString.validate(DocumentNo),
                                 XmlString.validate(StateName),
                                 XmlString.validate(CountyName),
                                 XmlString.validate(ResearchNote),
                                 XmlString.validate(SellerRoleName),
                                 XmlString.validate(BuyerRoleName),
                                 seller,
                                 buyer,
                                 attachments,
                                 tracts);
        }

        public string toSearchString()
        {
            string seller = String.Empty;
            if (Seller != null)
                seller += Seller.toSearchString();

            string buyer = String.Empty;
            if (Buyer != null)
                buyer += Buyer.toSearchString();

            string attachments = String.Empty;
            foreach (DocumentAttachmentInfo attachment in Attachments)
            {
                attachments += attachment.toSearchString();
            }

            string tracts = String.Empty;
            foreach (TractInfo tract in Tracts)
            {
                tracts += tract.toSearchString();
            }

            return DocTypeName + " " 
                   + Volume + " " 
                   + Page + " " 
                   + DocumentNo + " " 
                   + StateName + " " 
                   + CountyName + " " 
                   + ResearchNote + " " 
                   + SellerRoleName + " " 
                   + BuyerRoleName + " " 
                   + seller + " " 
                   + buyer + " " 
                   + attachments + " " 
                   + tracts;
        }

        #endregion
    }
}
