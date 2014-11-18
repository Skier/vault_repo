using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Weborb.Activation;
using Weborb.Samples.Ftp.Entities;

namespace Weborb.Samples.Ftp
{
    [SessionActivation()]
    public class DownloadService
    {

        private Hashtable processes;
        private String storageDir;
        
        public DownloadService()
        {
            processes = new Hashtable();
            storageDir = Uploader.BaseDir;
            CleanUpStorage();
           
        }

        public string StartDownload(FtpFile file, FtpConnectionInfo connectionInfo)
        {

            DownloadProcess process = new DownloadProcess(storageDir, file, connectionInfo);

            lock (processes.SyncRoot)
            {
                processes.Add(process.Id, process);
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(process.Execute));

            return process.Id;
        }

        public void AbortDownload(string downloadUid)
        {
            DownloadProcess process;
            
            lock (processes.SyncRoot)
            {
                
                process = (DownloadProcess)processes[downloadUid];
                if (process != null)
                {
                    processes.Remove(downloadUid);
                }
            }

            if (process != null)
            {
                process.Abort();
            }
            
        }

        public List<ProcessStatus> GetDownloadStatuses(String[] downloadUids)
        {
            List<ProcessStatus> result = new List<ProcessStatus>();

            lock (processes.SyncRoot)
            {
                foreach (String downloadUid in downloadUids)
                {
                    DownloadProcess process = (DownloadProcess)processes[downloadUid];
                    if (process != null)
                    {
                        ProcessStatus status = process.Status;
                        result.Add(status);
                    } 
                }
            }

            return result;
        }

        public String GetFileUrl(String downloadUid)
        {
            DownloadProcess process = GetProcessByUID(downloadUid);

            String baseUrl = Uploader.BaseUrl;
            
            return AdjustDir(baseUrl) + AdjustDir(downloadUid) + process.FileName;
        }

        private DownloadProcess GetProcessByUID(string downloadUid)
        {
            lock (processes.SyncRoot)
            {
                DownloadProcess process = (DownloadProcess)processes[downloadUid];
                if (process == null)
                {
                    throw new InvalidOperationException("Object state is invalid.");
                }

                return process;
            }
        }

        private void CleanUpStorage()
        {
            if (Directory.Exists(storageDir))
            {
                try
                {
                    DirectoryInfo storage = new DirectoryInfo(storageDir);

                    foreach (DirectoryInfo directory in storage.GetDirectories())
                    {
                        if (directory.CreationTime.Date < DateTime.Today)
                            directory.Delete(true);
                    }
                }
                catch { }
            }
        }

        private String AdjustDir(String path)
        {
            return path + ((path.EndsWith("/")) ? "" : "/").ToString();
        }
    }
}
