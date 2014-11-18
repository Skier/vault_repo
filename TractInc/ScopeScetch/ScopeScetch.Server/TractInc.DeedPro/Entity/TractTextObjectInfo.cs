namespace TractInc.DeedPro.Entity
{
    public class TractTextObjectInfo
    {
        #region Fields

        public int TractTextObjectId;
        public int TractId;
        public string Text;
        public double Easting;
        public double Northing;
        public double Rotation;

        #endregion

        #region Constructors

        public TractTextObjectInfo() {
        }

        public TractTextObjectInfo(int tractId, string text, double easting, double northing, double rotation)
        {
            TractId = tractId;
            Text = text;
            Easting = easting;
            Northing = northing;
            Rotation = rotation;
        }

        #endregion

        #region Methods

        public TractTextObjectInfo copy()
        {
            TractTextObjectInfo textObjectCopy = new TractTextObjectInfo();

            textObjectCopy.TractId = TractId;
            textObjectCopy.Text = Text;
            textObjectCopy.Easting = Easting;
            textObjectCopy.Northing = Northing;
            textObjectCopy.Rotation = Rotation;

            return textObjectCopy;
        }

        #endregion
    }
}
