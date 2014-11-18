using System.IO;
using System.Web;

namespace Weborb.Samples.Email.Entities
{
    public class FileInfo  
    {
        #region Fields

        private string n_name;
        private string n_url;
        private string n_text;
        private MemoryStream m_content;

        #endregion

        #region Constructors

        public FileInfo() {
        }

        public FileInfo(string name, string url, MemoryStream content) {
            Name = name;
            Url = url;
            Content = content;
        }

        public FileInfo(HttpPostedFile file) {
            Name = file.FileName;
            Content = new MemoryStream();

            byte[] bytes = new byte[4096];

            int i;
            while ((i = file.InputStream.Read(bytes, 0, bytes.Length)) != 0) {
                Content.Write(bytes, 0, i);
            }
        }
        
        #endregion

        #region Properties

        public string Name {
            get { return n_name; }
            set { n_name = value; }
        }

        public string Url {
            get { return n_url; }
            set { n_url = value; }
        }

        public string Text {
            get { return n_text; }
            set { n_text = value; }
        }

        internal MemoryStream Content {
            get { return m_content; }
            set { m_content = value; }
        }

        public long Size {
            get {
                return (null != Content) ? Content.Length : 0;
            }
        }

        #endregion
        
    }
}
