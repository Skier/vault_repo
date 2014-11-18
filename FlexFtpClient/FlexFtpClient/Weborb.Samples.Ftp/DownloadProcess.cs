using System;
using System.IO;
using System.Configuration;
using Weborb.Samples.Ftp.Entities;

namespace Weborb.Samples.Ftp
{
    internal class DownloadProcess
    {
        private const String MAX_DOWNLOAD_SIZE_KEY = "maxFileSize";

        private static int maxAllowedSize;

        private FtpConnectionInfo connectionInfo;
        private FtpClient ftpClient = null;
        private ZipHelper zipHelper = null;
        private ProcessStatus status = null;
        private String storageDir;
        private FtpFile file = null;

        public readonly String Id;
        
        static DownloadProcess()
        {
            Int32.TryParse(ConfigurationManager.AppSettings[MAX_DOWNLOAD_SIZE_KEY], out maxAllowedSize);
        }

        public DownloadProcess(String storageDir, FtpFile file, FtpConnectionInfo connectionInfo)
        {
            this.storageDir = storageDir;
            this.connectionInfo = connectionInfo;
            this.file = file;
            Id = Guid.NewGuid().ToString();
            
            Uri ftpUri = new Uri(connectionInfo.Host);

            ftpClient = new FtpClient(ftpUri, Id, connectionInfo.User, connectionInfo.Password);

            ProcessStatusHandler processStatusHandler = new ProcessStatusHandler(OnProcessedBytesChanged);
            ftpClient.OnProcessedBytesChanged += processStatusHandler;

            status = new ProcessStatus(Id, ProcessStatus.DOWNLOADING, file.Size, 0, null);
        }

        public String FileName
        {
            get
            {
                return file.Name;
            }
        }
        public ProcessStatus Status
        {
            get
            {
                ProcessStatus result;
                lock (status)
                {
                    result = status.Clone();
                }
                return result;
            }
        }

        public void Execute(Object obj)
        {
            try
            {
                ftpClient.Login();
                ftpClient.ChangeDir(ftpClient.RootPath.TrimEnd(('/')) + connectionInfo.CurrentDir);
                
                if (file.IsDirectory)
                {
                    lock (status)
                    {
                        status.State = ProcessStatus.CHECKING_DIRECTORY_SIZE;
                    }

                    file.Size = (int)ftpClient.GetDirSize(file.Name);
                }
                
                if (file.Size > maxAllowedSize)
                {
                    throw new Exception("Cannot download files larger than " + file.Size.ToString());
                } 
                else
                {
                    lock (status)
                    {
                        status.TotalBytes = file.Size;
                    }
                }

                if (!ftpClient.IsAborting())
                {
                    lock (status)
                    {
                        status.State = ProcessStatus.DOWNLOADING;
                    }
                    if (file.IsDirectory)
                    {
                        ftpClient.DownloadDir(ftpClient.RootPath.TrimEnd(('/')) + connectionInfo.CurrentDir + file.Name, Path.Combine(GetTempDir(), file.Name));
                    }
                    else
                    {
                        ftpClient.Download(file.Name, Path.Combine(GetTempDir(), file.Name));
                    }
                }

                if (!ftpClient.IsAborting())
                {
                    if (file.IsDirectory)
                    {
                        lock (status)
                        {
                            status.ProcessedBytes = 0;
                            status.State = ProcessStatus.COMPRESSING;
                        }
                        zipHelper = new ZipHelper();
                        
                        zipHelper.CompressDirectory(GetTempDir(), file.Name);
                        
                        file.Name += ZipHelper.ZIP_FILE_EXT;
                    }
                }

                lock (status)
                {
                    if (status.State != ProcessStatus.TERMINATED)
                    {
                        status.State = ProcessStatus.DOWNLOAD_COMPLETED;
                    }
                }
            }
            catch (Exception ex)
            {
                lock(status)
                {
                    status.State = ProcessStatus.ERROR;
                    status.ExceptionMessage = "Download error. " + ex.Message;
                }
            }
            finally
            {
                ftpClient.Close();
            }
        }

        public void Abort()
        {
            String currentStatus;

            lock (status)
            {
                currentStatus = status.State.Clone().ToString();
            }

            if (currentStatus == ProcessStatus.DOWNLOADING || currentStatus == ProcessStatus.CHECKING_DIRECTORY_SIZE)
            {
                ftpClient.Abort();

                lock (status)
                {
                    status.State = ProcessStatus.TERMINATED;
                }
            }

            if (currentStatus == ProcessStatus.COMPRESSING)
            {
                zipHelper.Abort();
                
                lock (status)
                {
                    status.State = ProcessStatus.TERMINATED;
                }
            }

            DeleteTempDir();
        }
        
        private void OnProcessedBytesChanged(int processedBytes)
        {
            lock(status)
            {
                status.ProcessedBytes += processedBytes;
            }
        }

        private void DeleteTempDir()
        {
            string tempDir = GetTempDir();

            if (Directory.Exists(tempDir))
            {
                DirectoryInfo dir = new DirectoryInfo(tempDir);

                try
                {
                    dir.Delete(true);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot delete temporary directory. \r\n" + ex.Message, ex);
                }
            }
        }

        private String GetTempDir()
        {
            String tempDir = Path.Combine(storageDir, Id);
         
            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);
            
            return tempDir;
        }

    }
}
