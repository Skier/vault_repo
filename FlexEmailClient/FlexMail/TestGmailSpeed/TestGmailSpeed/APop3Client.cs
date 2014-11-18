using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace TestGmailSpeed
{
    abstract class APop3Client
    {
        protected SslStream stream;
        protected TcpClient pop3Client;

        public bool IsConnected = false;

        public void Connect1(string host, int port, string userName, string userPassword)
        {
            pop3Client = new TcpClient(host, port);
            pop3Client.ReceiveTimeout = pop3Client.SendTimeout = 20000;

            stream = new SslStream(pop3Client.GetStream());
            stream.AuthenticateAsClient(host);

            string response = ReadSingleLineResponse();
            if (!IsOkResponse(response))
            {
                throw new Exception("Server not ready to start AUTHORIZATION.\nMessage: " + response);
            }

            SendCommand("USER " + userName);
            response = ReadSingleLineResponse();

            if (!IsOkResponse(response))
            {
                throw new Exception("Server doesn't accept username.\nMessage: " + response);
            }

            SendCommand("PASS " + userPassword);
            response = ReadSingleLineResponse();

            if (!IsOkResponse(response))
            {
                throw new Exception("Server doesn't accept user password.\nMessage: " + response);
            }
            this.IsConnected = true;

        }

        virtual public void Disconnect1()
        {

            if (pop3Client != null)
            {
                if (pop3Client.Connected)
                {
                    SendCommand("QUIT");
                    string response = ReadSingleLineResponse();

                }

                stream.Close();
                stream = null;

                pop3Client.Close();
                pop3Client = null;

            }


        }

        

        public int GetMessagesCount1()
        {
            SendCommand("STAT");
            String response = ReadSingleLineResponse();

            string[] responseParts = response.Split(' ');
            return int.Parse(responseParts[1]);

        }

        public string GetMessage1(int messageNumber)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            SendCommand(string.Format("TOP {0} 0", messageNumber));


            byte[] buffer = new byte[3072];

            byte[] last5 = new byte[5];

           
            int totalRead = 0;
            int lastRead = 0;

            do
            {
                Array.Clear(buffer, 0, 3072);
                lastRead = stream.Read(buffer, 0, 3072);
                if (lastRead > 5)
                {
                    last5[0] = buffer[lastRead - 5];
                    last5[1] = buffer[lastRead - 4];
                    last5[2] = buffer[lastRead - 3];
                    last5[3] = buffer[lastRead - 2];
                    last5[4] = buffer[lastRead - 1];
                    
                }
                // TODO add more code in case it is less then 5 

            } while (!(last5[0] == 0x0D && last5[1] == 0x0a && last5[2] == 0x2E && last5[3] == 0x0D && last5[4] == 0x0a) && lastRead > 0);
           
            return messageNumber.ToString();
        }

        private string ReadSingleLineResponse()
        {
            byte[] buffer = new byte[512];

            int totalRead = 0;
            int lastRead = 0;

            do
            {
                lastRead = stream.Read(buffer, totalRead, 512 - totalRead);
                totalRead += lastRead;

            } while (!(buffer[totalRead - 2] == 0x0D && buffer[totalRead - 1] == 0x0A) && totalRead <= 512);


            return ASCIIEncoding.ASCII.GetString(buffer, 0, totalRead);
        }



        public void Connect(string host, int port) {
            pop3Client = new TcpClient(host, port);
            pop3Client.ReceiveTimeout = pop3Client.SendTimeout = 20000;

            stream = new SslStream(pop3Client.GetStream());

            stream.AuthenticateAsClient(host);
        }
        
        virtual public void Disconnect () {

            if (pop3Client != null) {
                
                if (pop3Client.Connected) {
                    ExecuteCommand("QUIT");
                }
                
                stream.Close();
                stream = null;

                pop3Client.Close();
                pop3Client = null;
                
            }
        }
        
        public int GetMessagesCount () {

            string response = ExecuteCommand("STAT");
            
            if (!IsOkResponse(response)) {
                throw new Exception("negative response for retieving stats");
            }

            string[] responseParts = response.Split(' ');
            return int.Parse(responseParts[1]);
        }
        
        public string GetMessage(int messageNumber) {
            
            string response = ExecuteCommand(string.Format("TOP {0} 0", messageNumber));

            if (!IsOkResponse(response)) {
                throw new Exception(string.Format("negative response for email (ID: {0}) request", messageNumber));
            }

            return ReadContent();

        }
        
        virtual public void Login (string userName, string userPassword) {
            
            string response = ReadLine();

            if (!IsOkResponse(response)) {
                throw new Exception("Server not ready to start AUTHORIZATION.\nMessage: " + response);
            }
            
            response = ExecuteCommand("USER " + userName);

            if (!IsOkResponse(response)) {
                throw new Exception("Server doesn't accept username.\nMessage: " + response);
            }

            response = ExecuteCommand("PASS " + userPassword);

            if (!IsOkResponse(response)) {
                throw new Exception("Server doesn't accept user password.\nMessage: " + response);
            }            
        }
        
        private string ExecuteCommand(string command) {
            
            byte[] bytes = Encoding.ASCII.GetBytes(command + Environment.NewLine);
            
            if (stream.CanWrite) {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }

            return ReadLine();
            
        }

        private void SendCommand(string command)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(command + Environment.NewLine);

            if (stream.CanWrite)
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

       
        
        private bool IsOkResponse(string response) {
            return response.StartsWith("+OK");
        }
        
        abstract protected string ReadLine ();
        abstract protected string ReadContent ();
    }
}
