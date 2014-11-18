using System;
using System.Collections.Generic;
using Weborb.Samples.Ftp.Entities;

namespace Weborb.Samples.Ftp
{
    internal class FtpClientPool
    {
        private List<FtpClient> ftpClients;
        
        public FtpClientPool()
        {
            ftpClients = new List<FtpClient>();
        }

        public FtpClient GetFtp(FtpConnectionInfo connectionInfo)
        {
            FtpClient ftpClient = GetExistingFtp(connectionInfo.ConnectionId);
            if (ftpClient != null)
            {
                ftpClient.ChangeDir(ftpClient.RootPath.TrimEnd('/') + connectionInfo.CurrentDir);
                return ftpClient;
            }
            else
            {
               return CreateFtp(connectionInfo); 
            }            
        }

        public void ReleaseFtp(FtpClient ftp)
        {
            if (ftp != null)
            {
                lock (ftpClients)
                {
                    ftpClients.Remove(ftp);
                }
                ftp.Close();
            }
        }

        public void ClearAll()
        {
            lock (ftpClients)
            {
                foreach (FtpClient ftpConnection in ftpClients)
                {
                    ftpConnection.Close();
                }

                ftpClients.Clear();
            }
        }

        public FtpClient GetExistingFtp(String processId)
        {
            FtpClient result = null;
            
            lock (ftpClients)
            {
                for (int i = 0; i < ftpClients.Count; i++)
                {
                    FtpClient ftpConnection = ftpClients[i];
                    
                    if (ftpConnection.ProcessId == processId)
                    {
                        if (ftpConnection.LoggedIn)
                            result = ftpConnection;
                        else
                        {
                            ftpClients.Remove(ftpConnection);
                            ftpConnection.Close();
                        }
                        break;
                    }
                }
            }
            
            return result;
        }

        private FtpClient CreateFtp(FtpConnectionInfo connectionInfo)
        {
            try
            {
                Uri ftpUri = new Uri(connectionInfo.Host);
                FtpClient ftpClient = new FtpClient(ftpUri, connectionInfo.ConnectionId, connectionInfo.User, connectionInfo.Password);
                
                ftpClient.Login();
                ftpClient.ChangeDir(ftpClient.RootPath.TrimEnd('/') + connectionInfo.CurrentDir);
                lock (ftpClients)
                {
                    ftpClients.Add(ftpClient);
                }
                return ftpClient;
            }
            catch (Exception ex)
            {
                throw new Exception((null != ex) ? ex.Message : "Can not connect to Ftp server");
            }

        }

    }
}
