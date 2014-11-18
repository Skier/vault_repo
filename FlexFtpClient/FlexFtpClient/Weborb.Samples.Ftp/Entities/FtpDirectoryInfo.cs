using System.Collections.Generic;

namespace Weborb.Samples.Ftp.Entities
{
    public class FtpDirectoryInfo : List<FtpFileInfo>
    {
        public FtpDirectoryInfo() { }

        public FtpDirectoryInfo(string dir, string path)
        {
            foreach (string line in dir.Replace("\n", "").Split(System.Convert.ToChar('\r')))
            {
                if (line != "")
                {
                    Add(new FtpFileInfo(line, path));
                }
            }
        }

        public FtpDirectoryInfo GetFiles()
        {
            FtpDirectoryInfo result = new FtpDirectoryInfo();
            foreach (FtpFileInfo fi in this)
            {
                if (fi.FileType == FtpFileInfo.DirectoryEntryTypes.File
                    && fi.Filename.Trim() != ""
                    && fi.Filename != "."
                    && fi.Filename != ".."
                    )
                {
                    result.Add(fi);
                }
            }
            return result;
        }

        public FtpDirectoryInfo GetDirectories()
        {
            FtpDirectoryInfo result = new FtpDirectoryInfo();
            foreach (FtpFileInfo fi in this)
            {
                if (fi.FileType == FtpFileInfo.DirectoryEntryTypes.Directory
                    && fi.Filename.Trim() != ""
                    && fi.Filename != "."
                    && fi.Filename != ".."
                    )
                {
                    result.Add(fi);
                }
            }
            return result;
        }

        public bool FileExists(string filename)
        {
            foreach (FtpFileInfo ftpfile in this)
            {
                if (ftpfile.Filename == filename)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
