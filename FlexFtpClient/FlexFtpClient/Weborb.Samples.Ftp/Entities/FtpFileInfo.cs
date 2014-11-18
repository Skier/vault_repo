using System;
using System.Text.RegularExpressions;

namespace Weborb.Samples.Ftp.Entities
{
    public class FtpFileInfo
    {
#region "Properties"

        private static string[] _ParseFormats = new string[] { 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)"
            };

        public enum DirectoryEntryTypes
        {
            File,
            Directory
        }

        private string _filename;
        public string Filename
        {
            get { return _filename; }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
        }

        private DirectoryEntryTypes _fileType;
        public DirectoryEntryTypes FileType
        {
            get { return _fileType; }
        }

        private int _size;
        public int Size
        {
            get { return _size; }
        }

        private DateTime _fileDateTime;
        public DateTime FileDateTime
        {
            get { return _fileDateTime; }
        }

        private string _permission;
        public string Permission
        {
            get { return _permission; }
        }
        
        public string Extension
        {
            get
            {
                int i = Filename.LastIndexOf(".");
                if (i >= 0 && i < (Filename.Length - 1))
                {
                    return Filename.Substring(i + 1);
                }
                else
                {
                    return "";
                }
            }
        }

#endregion

        public FtpFileInfo(string line, string path)
        {
            Match m = GetMatch(line);
            if (m != null)
            {
                _filename = m.Groups["name"].Value;
                _path = path;

                Int32.TryParse(m.Groups["size"].Value, out _size);

                _permission = m.Groups["permission"].Value;
                string _dir = m.Groups["dir"].Value;
                if (_dir != "" && _dir != "-") {
                    _fileType = DirectoryEntryTypes.Directory;
                } else {
                    _fileType = DirectoryEntryTypes.File;
                }

                try
                {
                    _fileDateTime = DateTime.Parse(m.Groups["timestamp"].Value);
                }
                catch (Exception)
                {
                    _fileDateTime = Convert.ToDateTime(null);
                }
            }
        }

        private Match GetMatch(string line)
        {
            Regex rx;
            Match m;
            for (int i = 0; i < _ParseFormats.Length; i++)
            {
                rx = new Regex(_ParseFormats[i]);
                m = rx.Match(line);
                if (m.Success)
                {
                    return m;
                }
            }
            return null;
        }
    }
}
