namespace TractInc.TrueTract.Entity
{
    public class TractInfo
    {
        #region Fields

        public int TractId;
        public int Easting;
        public int Northing;
        public string RefName;
        public int CreatedBy;
        public bool IsDeleted;
        
        public int DocId;
        public double CalledAC;
        public int UnitId;

        public string UnitName;
        public DocumentInfo ParentDocument;

        public TractCallInfo[] Calls;
        public TractTextObjectInfo[] TextObjects;

        #endregion

        #region Constructors

        public TractInfo() {
        }

        public TractInfo(int tractId, int easting, int northing, string refName, 
                     int createdBy, bool isDeleted, int docId, double calledAC, int unitId) {
            TractId = tractId;
            Easting = easting;
            Northing = northing;
            RefName = refName;
            CreatedBy = createdBy;
            IsDeleted = isDeleted;
            DocId = docId;
            CalledAC = calledAC;
            UnitId = unitId;
        }

        #endregion
    }
}
