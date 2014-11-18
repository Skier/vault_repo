using System;
using System.Collections.Generic;

namespace TractInc.TrueTract.Entity
{
    public class TractInfo
    {
        private const string XML_TEMPLATE = @"<tract id=""{0}"" easting=""{1}"" northing=""{2}"" refName=""{3}"" calledAC=""{4}""/>";

        #region Fields

        public int TractId;
        public int Easting;
        public int Northing;
        public string RefName;
        public int CreatedBy;
//        public bool IsDeleted;
        
        public int DocId;
        public double CalledAC;
        public int UnitId;

        public string UniqueId;

        public string UnitName;
        public DocumentInfo ParentDocument;

        public List<TractCallInfo> Calls;
        public List<TractTextObjectInfo> TextObjects;

        #endregion

        #region Constructors

        public TractInfo() {
        }

        public TractInfo(int tractId, int easting, int northing, string refName, 
                     int createdBy, int docId, double calledAC, int unitId) {
            TractId = tractId;
            Easting = easting;
            Northing = northing;
            RefName = refName;
            CreatedBy = createdBy;
            DocId = docId;
            CalledAC = calledAC;
            UnitId = unitId;
        }

        #endregion

        public string toXml()
        {
            return String.Format(XML_TEMPLATE,
                                 TractId,
                                 Easting,
                                 Northing,
                                 XmlString.validate(RefName),
                                 CalledAC);
        }

        public string toSearchString()
        {
            return RefName;
        }

    }
}
