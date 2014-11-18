using System.ComponentModel;

namespace Dpi.Central.Web.Controls
{
    [DefaultProperty("Title")]
    public class Tab
    {
        #region Fields

        private string _title;
        private string _tag;

        #endregion

        #region Properties

        [Category("Behavior")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        [Category("Behavior")]
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        #endregion
    }
}