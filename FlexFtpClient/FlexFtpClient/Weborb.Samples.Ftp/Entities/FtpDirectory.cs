using System;
using System.Collections.Generic;

namespace Weborb.Samples.Ftp.Entities
{
    
    public class FtpDirectory : FtpFile
    {

        private List<FtpFile> directories = new List<FtpFile>();
        private List<FtpFile> files = new List<FtpFile>();

        

        public FtpDirectory() { }
        
        public FtpDirectory(FtpFile ftpFile)
        {
            Name = ftpFile.Name;
            FileDate = ftpFile.FileDate;
            Size = 0;
            FileDate = ftpFile.FileDate;
            Ext = String.Empty;
            Permission = ftpFile.Permission;
            IsDirectory = true;
        }

        public FtpDirectory(String[] entries)
        {
            foreach (String line in entries)
            {
                FtpFile ftpFile = new FtpFile(line);
                if (ftpFile.Name != "")
                {
                    if (ftpFile.IsDirectory )
                    {
                        if (ftpFile.Name != "."
                        && ftpFile.Name != "..")
                        {
                            directories.Add(ftpFile);
                        }
                    }
                    else
                    {
                        files.Add(ftpFile);
                    }
                  
                }
            }
        }

        public List<FtpFile> Files
        {
            get
            {
                return files;
            }
        }

        public List<FtpFile> Directories
        {
            get
            {
                return directories;
            }
        }

    }

}
