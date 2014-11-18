using System;
using System.Text.RegularExpressions;

namespace Weborb.Samples.Ftp.Entities
{
    public class FtpFile
    {

        private String name = String.Empty;
        private Int32 size = 0;
        private DateTime fileDate = DateTime.Now;
        private String ext = String.Empty;
        private String permission = String.Empty;
        private FtpDirectory directory;
        private Boolean isDirectory;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public Int32 Size
        {
            get { return size; }
            set { size = value; }
        }

        public DateTime FileDate
        {
            get { return fileDate; }
            set { fileDate = value; }
        }

        public String Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        public String Permission
        {
            get { return permission; }
            set { permission = value; }
        }

        public FtpDirectory Directory
        {
            get { return directory; }
            set { directory = value; }
        }

        public Boolean IsDirectory
        {
            get { return isDirectory; }
            set { isDirectory = value; }
        }

        public FtpFile() { }

        public FtpFile(string line)
        {
            Match match = GetMatch(line);
            if (match != null)
            {
                name = match.Groups["name"].Value;

                Int32.TryParse(match.Groups["size"].Value, out size);

                permission = match.Groups["permission"].Value;

                string dir = match.Groups["dir"].Value;
                if (dir != "" && dir != "-")
                {
                    isDirectory = true;
                } else {
                    isDirectory = false;
                }

                int i = name.LastIndexOf(".");
                if (i >= 0 && i < (name.Length - 1))
                {
                    ext = name.Substring(i + 1);
                }
                else
                {
                    ext = string.Empty;
                }

                try
                {
                    fileDate = DateTime.Parse(match.Groups["timestamp"].Value);
                }
                catch (Exception)
                {
                    fileDate = Convert.ToDateTime(null);
                }
            }
        }

        private static string[] ParseFormats = new string[] { 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)", 
            "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)"
            };

        private Match GetMatch(string line)
        {
            Regex regex;
            Match match;
            
            for (int i = 0; i < ParseFormats.Length; i++)
            {
                regex = new Regex(ParseFormats[i]);
                match = regex.Match(line);
                if (match.Success)
                {
                    return match;
                }
            }
            return null;
        }

    }

}
