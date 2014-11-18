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
    public class UploadService
    {
        private Hashtable processes;
        private String storageDir;

        public UploadService()
        {
            processes = new Hashtable();
            storageDir = Uploader.BaseDir;
            CleanUpStorage();
        }

        public static String GetFileUploaderURL()
        {
            return Uploader.UploaderUrl;
        }

        public String StartUploadToFtp(String uploadUid, String fileName, FtpConnectionInfo connectionInfo)
        {
            String localFilename = GetFilePath(uploadUid, fileName);
            if (!File.Exists(localFilename))
                throw (new Exception("File " + localFilename + " not found"));
            FileInfo fileInfo = new FileInfo(localFilename);

            UploadProcess process = new UploadProcess(storageDir, uploadUid, fileInfo, connectionInfo);

            lock (processes.SyncRoot)
            {
                processes.Remove(uploadUid);
                processes.Add(uploadUid, process);
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(process.Execute));

            return uploadUid;
        }

        public List<ProcessStatus> GetUploadStatuses(String[] uploadUids)
        {

            List<ProcessStatus> result = new List<ProcessStatus>();
            
            foreach (String uploadUid in uploadUids)
            {
                lock (processes.SyncRoot)
                {   
                    UploadProcess process = (UploadProcess)processes[uploadUid];
                    if (process != null)
                    {
                        ProcessStatus status = process.Status;
                        result.Add(status);
                    }   
                }
            }

            return result;
        }

        public void AbortUpload(String uploadUid)
        {
            
            UploadProcess process;
            lock (processes.SyncRoot)
            {
                process = (UploadProcess)processes[uploadUid];
                if (process != null)
                {
                    processes.Remove(uploadUid); 
                }
            }

            if (process != null)
            {
                process.Abort();
            }
        }

        private void CleanUpStorage()
        {
            if (Directory.Exists(storageDir))
            {
                try
                {
                    DirectoryInfo storage = new DirectoryInfo(storageDir);
                    
                    foreach (DirectoryInfo di in storage.GetDirectories())
                        if (di.CreationTime.Date < DateTime.Today)
                            di.Delete(true);
                }
                catch { }
            }
        }

        private String GetFilePath(String subDir, String fileName)
        {
            String baseDir = Uploader.BaseDir;
            String path = Path.Combine(Path.Combine(baseDir, subDir), fileName);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }

    }

}
