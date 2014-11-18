using System;

namespace Weborb.Samples.Ftp.Entities
{
    
    public class FtpConnectionInfo
    {
        
        private String host;
        private String user;
        private String password;
        private String currentDir;
        private String connectionId;

        public FtpConnectionInfo()
        {
            host = String.Empty;
            user = String.Empty;
            password = String.Empty;
            currentDir = String.Empty;
            connectionId = String.Empty;
        }

        public FtpConnectionInfo(String host, String user, String password, String currentDir, String connectionId) 
        {
            this.host = host;
            this.user = user;
            this.password = password;
            this.currentDir = currentDir;
            this.connectionId = connectionId;
        }

        public String Host
        {
            get { return host; }
            set { host = value.StartsWith("ftp://") ? value : "ftp://" + value; }
        }

        public String User
        {
            get { return user; }
            set { user = (0 == value.Length) ? "Anonymous" : value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public String CurrentDir
        {
            get { return currentDir; }
            set { currentDir = value; }
        }

        public String ConnectionId
        {
            get { return connectionId; }
            set { connectionId = value; }
        }
    
    }

}
