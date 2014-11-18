namespace TractInc.ScopeScetch.Entity
{
    public class TractTextObject
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

        public TractTextObject() {
        }

        public TractTextObject(int tractId, string text, double easting, double northing, double rotation)
        {
            TractId = tractId;
            Text = text;
            Easting = easting;
            Northing = northing;
            Rotation = rotation;
        }

        #endregion

        #region Methods

        public TractTextObject copy()
        {
            TractTextObject textObjectCopy = new TractTextObject();

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
