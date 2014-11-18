using System;
using System.IO;
using Weborb.Samples.Ftp.Entities;

namespace Weborb.Samples.Ftp
{
    internal class UploadProcess
    {
        private String processId = null;
        private FileInfo file = null;
        private String storageDir;
        private FtpClient ftpClient;
        private ProcessStatus status = null;
        private FtpConnectionInfo connectionInfo;
        
        public UploadProcess(String storageDir, String processId, FileInfo file, FtpConnectionInfo connectionInfo)
        {
            this.storageDir = storageDir;
            this.processId = processId;
            this.file = file;
            this.connectionInfo = connectionInfo;
            
            Uri ftpUri = new Uri(connectionInfo.Host);
            ftpClient = new FtpClient(ftpUri,processId, connectionInfo.User, connectionInfo.Password);   
            
            ProcessStatusHandler processStatusHandler = new ProcessStatusHandler(OnStatusChanged);
            ftpClient.OnProcessedBytesChanged += processStatusHandler;

            status = new ProcessStatus(processId, ProcessStatus.UPLOADING, 0, 0, null);
            status.ProcessId = processId;
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
       
        public  void Execute(Object obj)
        {
            try
            {
                ftpClient.Login();
                ftpClient.ChangeDir(ftpClient.RootPath.TrimEnd('/') + connectionInfo.CurrentDir);
                
                lock(status)
                {
                    status.TotalBytes = (int)file.Length;
                    status.State = ProcessStatus.UPLOADING;
                }

                ftpClient.Upload(file);

                lock (status)
                {
                    status.State = ProcessStatus.UPLOAD_COMPLETED;
                }
                
                ftpClient.Close();
            }
            catch (Exception ex)
            {
                lock (status)
                {
                    status.ExceptionMessage = "Upload error. " + ex.Message;
                    status.State = ProcessStatus.ERROR;
                }
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

            if (currentStatus == ProcessStatus.UPLOADING)
            {

                ftpClient.Abort();

                lock (status)
                {
                    status.State = ProcessStatus.TERMINATED;
                }
            }
            
            DeleteTempDir();
        }

        private void OnStatusChanged(int processedBytes)
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
                dir.Delete(true);
            }
        }

        private String GetTempDir()
        {
            return Path.Combine(storageDir, processId);
        }

    }

}
