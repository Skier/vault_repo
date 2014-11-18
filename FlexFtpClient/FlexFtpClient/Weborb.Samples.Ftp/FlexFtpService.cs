using System.Collections.Generic;
using System;
using System.IO;
using Weborb.Activation;
using Weborb.Samples.Ftp.Entities;

namespace Weborb.Samples.Ftp
{
    [SessionActivation()]
    public class FlexFtpService
    {
        private FtpClientPool ftpClientPool;

        public FlexFtpService()
        {
            ftpClientPool = new FtpClientPool();
            CleanUpStorage();
        }

        public String ConnectToFtp(FtpConnectionInfo connectionInfo)
        {
            connectionInfo.ConnectionId = Guid.NewGuid().ToString();
            FtpClient ftpClient = ftpClientPool.GetFtp(connectionInfo);
            return ftpClient.ProcessId;
        }

        public void CloseConnection(String connectionId)
        {
            FtpClient ftpClient = ftpClientPool.GetExistingFtp(connectionId);

            if (ftpClient != null)
                ftpClientPool.ReleaseFtp(ftpClient);
        }

        public List<FtpFile> GetFtpDirectory(FtpConnectionInfo connectionInfo)
        {
            String[] files;

            FtpClient ftpClient = ftpClientPool.GetFtp(connectionInfo);
            files = ftpClient.GetFileList();

            FtpDirectory ftpDirectory = new FtpDirectory(files);
            List<FtpFile> returnList = new List<FtpFile>();

            
            foreach (FtpFile ftpDir in ftpDirectory.Directories)
                returnList.Add(new FtpDirectory(ftpDir));

            foreach (FtpFile ftpFile in ftpDirectory.Files)
                returnList.Add(ftpFile);

            return returnList;
        }

        public void DeleteFtpFiles(FtpFile[] files, FtpConnectionInfo connectionInfo)
        {
            FtpClient ftpClient = ftpClientPool.GetFtp(connectionInfo);
            
            foreach (FtpFile file in files)
            {
                if (file.IsDirectory)
                    ftpClient.DeleteDirectory(file.Name);
                else
                    ftpClient.DeleteFile(file.Name);
            }
        }

        public void RenameFtpFile(FtpFile file, String target, FtpConnectionInfo connectionInfo)
        {
            if (target == String.Empty)
                return;

            FtpClient ftpClient = ftpClientPool.GetFtp(connectionInfo);
            ftpClient.RenameFile(file.Name, target, true);
        }

        public void MoveFtpFiles(FtpFile[] files, String targetDir, FtpConnectionInfo connectionInfo)
        {
            if (targetDir == String.Empty)
                return;
            
            FtpClient ftpClient = ftpClientPool.GetFtp(connectionInfo);
            
            foreach (FtpFile file in files)
            {
                ftpClient.RenameFile(file.Name, ftpClient.RootPath.TrimEnd('/') + AdjustDir(targetDir) + file.Name, true);
            }
        }

        public void CreateFtpDirectory(String dirname, FtpConnectionInfo connectionInfo)
        {
            FtpClient ftpClient = ftpClientPool.GetFtp(connectionInfo);
            ftpClient.MakeDir(dirname);
        }

        private static String AdjustDir(String path)
        {
            return path + ((path.EndsWith("/")) ? "" : "/");
        }

        private static void CleanUpStorage()
        {
            if (Directory.Exists(Uploader.BaseDir))
            {
                try
                {
                    DirectoryInfo storage = new DirectoryInfo(Uploader.BaseDir);
                    
                    foreach (DirectoryInfo directoryInfo in storage.GetDirectories())
                    {
                        if (directoryInfo.CreationTime.Date < DateTime.Today)
                            directoryInfo.Delete(true);
                    }
                }
                catch { }
            }
        }

    }

}
