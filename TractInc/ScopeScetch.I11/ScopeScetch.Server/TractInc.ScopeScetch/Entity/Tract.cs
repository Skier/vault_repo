namespace TractInc.ScopeScetch.Entity
{
    public class Tract
    {
        #region Fields

        public int TractId;
        public int Easting;
        public int Northing;
        public string Description;
        public int CreatedBy;
        public bool IsDeleted;
        public string Uid;
        
        public int DocId;
        public double CalledAC;
        public int UnitId;

        public string UnitName;
        public Document ParentDocument;

        public TractCall[] Calls;
        public TractTextObject[] TextObjects;

        #endregion

        #region Constructors

        public Tract() {
        }

        public Tract(int tractId, int easting, int northing, string description, 
                     int createdBy, bool isDeleted, string uid, int docId, double calledAC, int unitId) {
            TractId = tractId;
            Easting = easting;
            Northing = northing;
            Description = description;
            CreatedBy = createdBy;
            Uid = uid;
            IsDeleted = isDeleted;
            DocId = docId;
            CalledAC = calledAC;
            UnitId = unitId;
                     }

        #endregion
    }
}
