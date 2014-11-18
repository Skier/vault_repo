using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using Weborb.Samples.Ftp.Entities;

namespace Weborb.Samples.Ftp
{

    public delegate void ProcessStatusHandler(int bytesProcessed);

    internal class FtpClient
    {
        private class ServerResponse
        {
            private int resultCode;
            private string message;

            public ServerResponse(int resultCode, string message)
            {
                this.resultCode = resultCode;
                this.message = message;
            }

            public int ResultCode
            {
                get
                {
                    return resultCode;
                }
            }

            public string Message
            {
                get
                {
                    return message;
                }
            }
        }

        private static int BUFFER_SIZE = 8192;

        private String server;
        private String remotePath = ".";
        private String username;
        private String password;


        private String rootPath = "/";
        private String processId = null;

        private int port = 21;
        private int bytes = 0;
      
        private bool isLoggedIn = false;
        private bool binMode = false;

        private Byte[] buffer = new Byte[BUFFER_SIZE];
        private Socket clientSocket = null;

        private long abortFlag = 0;

        private EventWaitHandle abortEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        
        public event ProcessStatusHandler OnProcessedBytesChanged;
        

        public FtpClient(Uri uri, string processId, string userName, string password)
        {
            server = uri.Host;
            port = uri.Port;
            username = userName;
            this.password = password;
            this.processId = processId;
        }

        public String RootPath
        {
            get { return rootPath; }

        }

        public String ProcessId
        {
            get { return processId; }
          
        }

        public Boolean LoggedIn
        {
            get
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    return isLoggedIn;
                } 
                else
                {
                    isLoggedIn = false;
                    return isLoggedIn;
                }
            }
        }

        public bool BinaryMode
        {
            get { return binMode; }
            
            set
            {
                if (binMode == value)
                    return;

                ServerResponse response;
                if (value)
                   response =  sendCommand("TYPE I");
                else
                   response = sendCommand("TYPE A");

               if (response.ResultCode != 200)
                   throw new FtpException(response.Message.Substring(4));
            }
        }

        public void Abort()
        {
            Interlocked.Exchange(ref abortFlag, 1);
            
            abortEvent.WaitOne();
        }

        public bool IsAborting()
        {
            long value = Interlocked.Read(ref abortFlag);
            if (value == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Login()
        {
            if (LoggedIn)
                return;

            IPAddress addr;
            IPEndPoint ep;

            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                addr = Dns.GetHostEntry(server).AddressList[0];
                ep = new IPEndPoint(addr, port);
                clientSocket.Connect(ep);
            }
            catch (Exception ex)
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    clientSocket.Close();
                }
                throw new FtpException("Couldn't connect to remote server", ex);
            }

            ServerResponse response = readResponse();
            if (response.ResultCode != 220)
            {
                Close();
                throw new FtpException(response.Message);
            }

            response = sendCommand("USER " + username);
            if (!(response.ResultCode == 331 || response.ResultCode == 230))
            {
                Cleanup();
                throw new FtpException(response.Message.Substring(4));
            }

            if (response.ResultCode != 230)
            {
                response = sendCommand("PASS " + password);
                if (!(response.ResultCode == 230 || response.ResultCode == 202))
                {
                    Cleanup();
                    throw new FtpException(response.Message.Substring(4));
                }
            }

            isLoggedIn = true;

            ChangeDir(remotePath);
            rootPath = remotePath;

        }

        public void Close()
        {
            if (clientSocket != null && isLoggedIn)
            {
                sendCommand("QUIT");
            }
            Cleanup();
        }

        public String[] GetFileList()
        {
            return GetFileList("*");
        }

        public String[] GetFileList(String mask)
        {
            
            Login();

            Socket cSocket = null;
            string message = "";
            ServerResponse response;
            try
            {
                cSocket = createDataSocket();
                response = sendCommand("LIST " + mask);

                if (!(response.ResultCode == 150 || response.ResultCode == 125 || response.ResultCode == 226))
                    throw new FtpException(response.Message.Substring(4));

                while ((bytes = cSocket.Receive(buffer, BUFFER_SIZE, 0)) > 0 && !IsAborting())
                    message += Encoding.Default.GetString(buffer, 0, bytes);
            }
            finally
            {
                if (cSocket != null && cSocket.Connected)
                    cSocket.Close();
            }

            if (IsAborting())
            {
                abortEvent.Set();
                return new String[] { };
            }

            String[] msg = message.Replace("\r", "").TrimEnd('\n').Split('\n');

            if (message.IndexOf("No such file or directory") != -1)
                msg = new String[] { };

            if (response.ResultCode == 226)
                return msg;

            response = readResponse();

            if (response.ResultCode != 226)
                msg = new String[] { };

            return msg;
            
           
        }

        public long GetFileSize(String fileName)
        {
            if (IsAborting())
            {
                abortEvent.Set();
                return 0;
            }

            Login();

            long size;

            ServerResponse response = sendCommand("SIZE " + fileName);
            if (response.ResultCode == 213)
                size = long.Parse(response.Message.Substring(4));
            else
                throw new FtpException(response.Message.Substring(4));

            return size;
        }

        public long GetDirSize(String dirName)
        {

            long size = 0;
            ChangeDir(dirName);

            FtpDirectory dir = new FtpDirectory(GetFileList());

            foreach (FtpFile subDir in dir.Directories)
                if (!IsAborting())
                    size += GetDirSize(subDir.Name);

            foreach (FtpFile ftpFile in dir.Files)
                if (!IsAborting())
                    size += ftpFile.Size;

            ChangeDir("..");

            return size;
        }

        public void DownloadDir(string dirName, string localDir)
        {
            if (IsAborting())
            {
                abortEvent.Set();
                return;
            }

            if (!Directory.Exists(localDir))
                Directory.CreateDirectory(localDir);

            ChangeDir(dirName);

            FtpDirectory ftpDirectory = new FtpDirectory(GetFileList());

            foreach (FtpFile ftpFile in ftpDirectory.Files)
            {
                if (IsAborting()) {
                    abortEvent.Set();
                    return;
                }
                
                Download(ftpFile.Name, Path.Combine(localDir, ftpFile.Name));
            }

            foreach (FtpFile subDir in ftpDirectory.Directories)
            {
                
                if (IsAborting()) {
                    abortEvent.Set();
                    return;
                }
                
                DownloadDir(dirName + "/" + subDir.Name, Path.Combine(localDir, subDir.Name));
            }

        }

        public void Download(String remFileName, String locFileName)
        {

            Login();

            BinaryMode = true;

            if (locFileName.Equals(""))
                locFileName = remFileName;

            FileStream output;

            if (!File.Exists(locFileName))
                output = File.Create(locFileName);
            else
                output = new FileStream(locFileName, FileMode.Open);

            Socket cSocket = null;

            ServerResponse resp;
            using (output)
            {
                try
                {
                    
                    cSocket = createDataSocket();

                    resp = sendCommand("RETR " + remFileName);

                    if (resp.ResultCode != 150 && resp.ResultCode != 125 && resp.ResultCode != 226)
                        throw new FtpException(resp.Message.Substring(4));

                    do
                    {
                        bytes = cSocket.Receive(buffer, BUFFER_SIZE, 0);
                        output.Write(buffer, 0, bytes);
                        onProcessed(bytes);
                    } while (bytes > 0 && !IsAborting());

                    output.Flush();

                }
                finally
                {
                    output.Close();
                    if (null != cSocket && cSocket.Connected) cSocket.Close();
                }
            }
            
            if (IsAborting()) {
                abortEvent.Set();
                return;
            }

            if (resp.ResultCode == 226)
                return;

            resp = readResponse();
            if (resp.ResultCode != 226 && resp.ResultCode != 250)
                throw new FtpException(resp.Message.Substring(4));
        }
        
        public void Upload(FileInfo file)
        {
            Upload(file.FullName);
        }

        public void Upload(String fileName)
        {
            ServerResponse response;
            if (IsAborting())
            {
                abortEvent.Set();
                return;
            }

            Login();

            Socket cSocket;

            using (FileStream input = new FileStream(fileName, FileMode.Open))
            {

                cSocket = createDataSocket();
                try
                {

                    response = sendCommand("STOR " + Path.GetFileName(fileName));
                    if (response.ResultCode != 125 && response.ResultCode != 150 && response.ResultCode != 226)
                        throw new FtpException(response.Message.Substring(4));

                    while (((bytes = input.Read(buffer, 0, buffer.Length)) > 0) && !IsAborting())
                    {
                        cSocket.Send(buffer, bytes, 0);
                        onProcessed(bytes);
                    }

                    input.Close();

                }
                finally
                {
                    if (null != cSocket && cSocket.Connected) cSocket.Close();
                }

            }

            if (IsAborting())
            {
                abortEvent.Set();
                return;
            }

            if (response!= null &&  response.ResultCode == 226)
                return;

            response = readResponse();
            if (response.ResultCode != 226 && response.ResultCode != 250)
                throw new FtpException(response.Message.Substring(4));
        }

        public void DeleteFile(String fileName)
        {
            Login();

            ServerResponse response = sendCommand("DELE " + fileName);
            if (response.ResultCode != 250)
                throw new FtpException(response.Message.Substring(4));
        }

        public void DeleteDirectory(String dirName)
        {
            ChangeDir(dirName);

            FtpDirectory ftpDirectory = new FtpDirectory(GetFileList());

            foreach (FtpFile subDir in ftpDirectory.Directories)
                DeleteDirectory(subDir.Name);

            foreach (FtpFile file in ftpDirectory.Files)
                DeleteFile(file.Name);

            ChangeDir("..");
            RemoveDir(dirName);
        }

        public void RenameFile(String oldFileName, String newFileName, bool overwrite)
        {
            Login();

            ServerResponse response = sendCommand("RNFR " + oldFileName);
            if (response.ResultCode != 350)
                throw new FtpException(response.Message.Substring(4));
            if (!overwrite && GetFileList(newFileName).Length > 0)
                throw new FtpException("File already exists");

            response = sendCommand("RNTO " + newFileName);
            if (response.ResultCode != 250)
                throw new FtpException(response.Message.Substring(4));
        }

        public void MakeDir(String dirName)
        {
            Login();

            ServerResponse response = sendCommand("MKD " + dirName);
            if (response.ResultCode != 250 && response.ResultCode != 257)
                throw new FtpException(response.Message.Substring(4));
        }

        public void RemoveDir(String dirName)
        {
            Login();

            ServerResponse response  = sendCommand("RMD " + dirName);
            if (response.ResultCode != 250)
                throw new FtpException(response.Message.Substring(4));
        }

        public void ChangeDir(String dirName)
        {
            if (dirName == null || dirName.Length == 0 || IsAborting())
                return;

            Login();

            ServerResponse response = sendCommand("CWD " + dirName);
            if (response.ResultCode != 250)
                throw new FtpException(response.Message.Substring(4));

            remotePath = GetWorkDir();
        }

        public String GetWorkDir()
        {
            Login();

            ServerResponse response = sendCommand("PWD");
            if (response.ResultCode != 257)
                throw new FtpException(response.Message.Substring(4));

            return response.Message.Split('"')[1];
        }

        public void NoOp()
        {
            Login();

            ServerResponse response = sendCommand("NOOP");
            if (response.ResultCode != 200)
                throw new FtpException(response.Message.Substring(4));
        }

        private ServerResponse readResponse()
        {
            int resultCode = 0;
            string result;

            result = readLine();
            if (result.Length > 3)
                resultCode = int.Parse(result.Substring(0, 3));
            else
                result = null;

            return new ServerResponse(resultCode, result);
        }

        private String readLine()
        {
            string message = "";
            
            while (true)
            {
                bytes = clientSocket.Receive(buffer, BUFFER_SIZE, 0);
                message += Encoding.Default.GetString(buffer, 0, bytes);
                if (bytes < BUFFER_SIZE)
                    break;
            }

            String[] msg = message.Replace("\r", "").Split('\n');

            if (msg.Length > 2)
                message = msg[msg.Length - 2];
            else
                message = msg[0];

            if (message.Length > 4 && !message.Substring(3, 1).Equals(" "))
                return readLine();

            return message;
        }

        private ServerResponse sendCommand(String command)
        {
            Byte[] cmdBytes = Encoding.Default.GetBytes((command + "\r\n").ToCharArray());
            clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
            return readResponse();
        }

        private Socket createDataSocket()
        {
            ServerResponse response = sendCommand("PASV");

            if (response.ResultCode != 227)
                throw new FtpException(response.Message.Substring(4));

            int index1 = response.Message.IndexOf('(');
            int index2 = response.Message.IndexOf(')');

            String ipData = response.Message.Substring(index1 + 1, index2 - index1 - 1);

            int[] parts = new int[6];

            int len = ipData.Length;
            int partCount = 0;
            String buf = "";

            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = char.Parse(ipData.Substring(i, 1));

                if (char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                    throw new FtpException("Malformed PASV result: " + response.Message);

                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = int.Parse(buf);
                        buf = "";
                    }
                    catch (Exception ex)
                    {
                        throw new FtpException("Malformed PASV result (not supported?): " + response.Message, ex);
                    }
                }
            }

            String ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];

            port = (parts[4] << 8) + parts[5];

            Socket socket = null;
            IPEndPoint ep;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                socket.Connect(ep);
            }
            catch (Exception ex)
            {
                if (socket != null && socket.Connected)
                    socket.Close();
                throw new FtpException("Can't connect to remote server", ex);
            }

            return socket;
        }

        private void Cleanup()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }
            isLoggedIn = false;
        }

        private void onProcessed(int bytesProcessed)
        {
            if (OnProcessedBytesChanged != null)
                OnProcessedBytesChanged(bytesProcessed);
        }

        ~FtpClient()
        {
            Cleanup();
        }

    }

}
