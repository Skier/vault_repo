using System;
using System.IO;
using System.Web;

namespace Weborb.Samples.Email.Entities
{
    public class FileInfo  
    {
        public string Name;
        public string Url;
        
        [NonSerialized] public MemoryStream Content;
        [NonSerialized] public int Id;

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
        
        public long Size {
            get {
                return (null != Content) ? Content.Length : 0;
            }
        }
        
    }
}
