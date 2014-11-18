using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using Weborb.Util.Logging;

namespace Weborb.Samples.Email.Pop3
{

    #region Helper classses

    /// <summary>
    /// Combines Email ID with Email UID for one email
    /// The POP3 server assigns to each message a unique Email UID, which will not change for the life time
    /// of the message and no other message should use the same.
    /// 
    /// Exceptions:
    /// Throws Pop3Exception if there is a serious communication problem with the POP3 server, otherwise
    /// 
    /// </summary>
    public struct EmailUid
    {
        /// <summary>
        /// used in POP3 commands to indicate which message (only valid in the present session)
        /// </summary>
        public int EmailId;

        /// <summary>
        /// Uid is always the same for a message, regardless of session
        /// </summary>
        public string Uid;

        /// <summary>
        /// constructor
        /// </summary>
        public EmailUid(int EmailId, string Uid) {
            this.EmailId = EmailId;
            this.Uid = Uid;
        }
    }


    /// <summary>
    /// If anything goes wrong within Pop3MailClient, a Pop3Exception is raised
    /// </summary>
    public class Pop3Exception : ApplicationException
    {
        /// <summary>
        /// Pop3 exception with no further explanation
        /// </summary>
        public Pop3Exception() {
        }

        /// <summary>
        /// Pop3 exception with further explanation
        /// </summary>
        public Pop3Exception(string ErrorMessage) : base(ErrorMessage) {
        }
    }


    /// <summary>
    /// A pop 3 connection goes through the following states:
    /// </summary>
    public enum Pop3ConnectionStateEnum
    {
        /// <summary>
        /// undefined
        /// </summary>
        None = 0,
        /// <summary>
        /// not connected yet to POP3 server
        /// </summary>
        Disconnected,
        /// <summary>
        /// TCP connection has been opened and the POP3 server has sent the greeting. POP3 server expects user name and password
        /// </summary>
        Authorization,
        /// <summary>
        /// client has identified itself successfully with the POP3, server has locked all messages 
        /// </summary>
        Connected,
        /// <summary>
        /// QUIT command was sent, the server has deleted messages marked for deletion and released the resources
        /// </summary>
        Closed
    }

    #endregion

    /// <summary>
    /// Pop3MailClient Class provides access to emails on a POP3 Server
    /// </summary>

    public class Pop3MailClient : IDisposable {
        
        #region Constants

        private const string NEGATIVE_RESPONSE = "Negative server response for request ({0}). Response is {1}";
        private const string WRONG_RESPONSE = "Server return response in wrong format";
        private const string UIDL_REQUEST = "Get Unique Identifier List";
        private const string DELETE_REQUEST = "Delete message (Message number is \"{0}\")";
        
        private const string TOP_REQUEST = "Get Message Header";
        private const string RETR_REQUEST = "Get Full Message.";
        private const string QUIT_REQUEST = "Disconnect.";
        
        private const string RESPONSE_OK="+OK";

        #endregion
       
        #region Fields

        private string _popServer;
        private int _port;
        private int _readTimeout = 30000;
        private int _sendTimeout = 30000;
        private string _username;
        private string _password;
        private TcpClient _clientSocket;
        private StreamReader _reader;
        private StreamWriter _writer;
        private Stream _stream;
        private bool _useSSL;
        private Pop3ConnectionStateEnum _connectionState = Pop3ConnectionStateEnum.Disconnected;
        
        #endregion

        #region Properties

        public string PopServer {
            get { return _popServer; }
        }

        public int Port {
            get { return _port; }
        }

        /// <summary>
        /// Should SSL be used for connection with POP3 server ?
        /// </summary>
        public bool UseSSL {
            get { return _useSSL; }
        }

        /// <summary>
        /// Read timeout for the connection to the SMTP server in milliseconds.
        /// The default value is 60000 milliseconds.
        /// </summary>
        public int ReadTimeout {
            get { return _readTimeout; }
            set { _readTimeout = value; }
        }

        /// <summary>
        /// Send timeout for the connection to the SMTP server in milliseconds.
        /// The default value is 60000 milliseconds.
        /// </summary>
        public int SendTimeOut {
            get{return _sendTimeout;}
            set{_sendTimeout=value;}
        }
        
        /// <summary>
        /// Get owner name of mailbox on POP3 server
        /// </summary>
        public string Username {
            get { return _username; }
        }

        /// <summary>
        /// Get password for mailbox on POP3 server
        /// </summary>
        public string Password {
            get { return _password; }
        }

        /// <summary>
        /// Get connection status with POP3 server
        /// </summary>
        public Pop3ConnectionStateEnum ConnectionState {
            get { return _connectionState; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Make POP3 client ready to connect to POP3 server
        /// </summary>
        /// <param name="popServer"><example>pop.gmail.com</example></param>
        /// <param name="port"><example>995</example></param>
        /// <param name="useSSL">True: SSL is used for connection to POP3 server</param>
        /// <param name="username"><example>abc@gmail.com</example></param>
        /// <param name="password">Secret</param>
        public Pop3MailClient(string popServer, int port, bool useSSL, string username, string password) {
            _popServer = popServer;
            _port = port;
            _useSSL = useSSL;
            _username = username;
            _password = password;
        }

        ~Pop3MailClient() {
            Disconnect();
        }
        
        /// <summary>
        /// Connect to POP3 server
        /// </summary>
        public void Connect() {
            
            if (_connectionState != Pop3ConnectionStateEnum.Disconnected &&
                _connectionState != Pop3ConnectionStateEnum.Closed) {
                
                CallWarning("connect", string.Format(
                    "Connect command received, but connection state is: {0}", _connectionState.ToString()));
                
            } else {
                
                //establish TCP connection
                try {
                    CallTrace(string.Format("   Connect at port {0}", _port));
                    
                    _clientSocket = new TcpClient(_popServer, _port);
                    
                    _clientSocket.ReceiveTimeout = _readTimeout;
                    _clientSocket.SendTimeout = _sendTimeout;
                    
                    _clientSocket.ReceiveBufferSize = _clientSocket.SendBufferSize = 1024;
                    
                } catch (Exception ex) {
                    throw new Pop3Exception(string.Format(
                        "Connection to server {0}, port {1} failed.{2}Runtime Error: {3}",
                        _popServer, _port, Environment.NewLine, ex.ToString()));
                }

                if (_useSSL) {
                    
                    //get SSL stream
                    try {
                        
                        CallTrace("   Get SSL connection");
                        _stream = new SslStream(_clientSocket.GetStream(), false);
                        
                    } catch (Exception ex) {
                        throw new Pop3Exception(string.Format(
                            "Server {0} found, but cannot get SSL data stream.{1}Runtime Error: {2}",
                            _popServer, Environment.NewLine, ex.ToString()));
                    }

                    //perform SSL authentication
                    try {
                        
                        CallTrace("   Get SSL authentication");
                        ((SslStream) _stream).AuthenticateAsClient(_popServer);
                        
                    } catch (Exception ex) {
                        throw new Pop3Exception(string.Format(
                            "Server {0} found, but problem with SSL Authentication.{1}Runtime Error: {2}",
                            _popServer, Environment.NewLine, ex.ToString()));
                    }
                    
                } else {
                    
                    //create a stream to POP3 server without using SSL
                    try {
                        
                        CallTrace("   Get connection without SSL");
                        _stream = _clientSocket.GetStream();
                        
                    } catch (Exception ex) {
                        throw new Pop3Exception(string.Format(
                            "Server {0} found, but cannot get data stream (without SSL).{1}Runtime Error: {2}",
                            _popServer, Environment.NewLine, ex.ToString()));
                    }
                }

                _stream.ReadTimeout = _clientSocket.ReceiveTimeout;
                _stream.WriteTimeout = _clientSocket.SendTimeout;
                
                //get stream for reading from pop server
                try {
                    
                    _reader = new StreamReader(_stream, Encoding.Default, true);
                    _writer = new StreamWriter(_stream);
                    
                } catch (Exception ex) {
                    throw new Pop3Exception(string.Format(
                        "Server {0} found, but cannot read from {1}stream.{2}Runtime Error: {3}", 
                        _popServer, _useSSL ? "SSL " : "", Environment.NewLine, ex.Message));
                }

                //ready for authorisation
                string response;
                if (!readSingleLine(out response)) {
                    throw new Pop3Exception(string.Format(
                        "Server not ready to start AUTHORIZATION.\nMessage: {0}", response));
                }
                
                setPop3ConnectionState(Pop3ConnectionStateEnum.Authorization);

                //send user name
                if (!executeCommand("USER " + _username, out response)) {
                    throw new Pop3Exception(string.Format(
                        "Server {0} doesn't accept username '{1}'.{2}Message: {3}",
                        _popServer, _username, Environment.NewLine, response));
                }

                //send password
                if (!executeCommand("PASS " + _password, out response)) {
                    throw new Pop3Exception(string.Format(
                        "Server {0} doesn't accept password for user '{1}'.{2}Message: {3}",
                        _popServer, _username, Environment.NewLine, response));
                }

                setPop3ConnectionState(Pop3ConnectionStateEnum.Connected);
            }
        }

        /// <summary>
        /// Disconnect from POP3 Server
        /// </summary>
        public void Disconnect() {
            
            if (_connectionState == Pop3ConnectionStateEnum.Disconnected ||
                _connectionState == Pop3ConnectionStateEnum.Closed) {
                CallWarning("disconnect", "Disconnect received, but was already disconnected.");
                return;
            } 

            //ask server to end session and possibly to remove emails marked for deletion
            try {
                string response;
                
                if (executeCommand("QUIT", out response)) {
                    //server says everything is ok
                    setPop3ConnectionState(Pop3ConnectionStateEnum.Closed);
                } else {
                    //server says there is a problem
                    CallWarning("Disconnect", string.Format(NEGATIVE_RESPONSE, QUIT_REQUEST, response));
                    
                    setPop3ConnectionState(Pop3ConnectionStateEnum.Disconnected);
                }
                
            } catch {}
            finally {
                if (_reader != null) {
                    _reader.Close();
                    _reader = null;
                }
                
                if (_writer != null) {
                    _writer.Close();
                    _writer = null;
                }
                
                if (_stream != null) {
                    _stream.Close();
                    _stream = null;
                }

                if (_clientSocket != null) {
			        _clientSocket.Close();                    
    		        _clientSocket=null;
                }
                
            }

        }

        /// <summary>
        /// Delete message from server.
        /// The POP3 server marks the message as deleted.  Any future
        /// reference to the message-number associated with the message
        /// in a POP3 command generates an error.  The POP3 server does
        /// not actually delete the message until the POP3 session
        /// enters the UPDATE state.
        /// </summary>
        /// <param name="msg_number"></param>
        /// <returns></returns>
        public void DeleteEmail(int msg_number) {
            EnsureState(Pop3ConnectionStateEnum.Connected);
            string response;
            
            if (!executeCommand("DELE " + msg_number, out response)) {
                string requestDescription = string.Format(DELETE_REQUEST, msg_number);
                throw new Pop3Exception(string.Format(NEGATIVE_RESPONSE, requestDescription, response));
            }
        }

        public Hashtable GetUniqueEmailIdMap() {
            EnsureState(Pop3ConnectionStateEnum.Connected);
            Hashtable result = new Hashtable();

            //get server response status line
            string response;
            if (!executeCommand("UIDL", out response)) {
                throw new Pop3Exception(string.Format(NEGATIVE_RESPONSE, UIDL_REQUEST, response));
            }

            string[] responseLines = ReceiveContent().Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            int EmailNumber;
            for (int i = 0; i < responseLines.Length; i++) {
                string[] responseSplit = responseLines[i].Split(' ');
                
                if (responseSplit.Length < 2) {
                    throw new Pop3Exception(WRONG_RESPONSE);
                } else if (!int.TryParse(responseSplit[0], out EmailNumber)) {
                    throw new Pop3Exception(WRONG_RESPONSE);
                } else {
                    result.Add(responseSplit[1], EmailNumber);
                }
            }
            
            return result;
        }
        
        /// <summary>
        /// Get a list with the unique IDs of all Email available in mailbox.
        /// 
        /// Explanation:
        /// EmailIds for the same email can change between sessions, whereas the unique Email Id
        /// never changes for an email.
        /// </summary>
        public List<EmailUid> GetUniqueEmailIdList() {
            EnsureState(Pop3ConnectionStateEnum.Connected);
            
            List<EmailUid> result = new List<EmailUid>();

            string response;

            if (!executeCommand("UIDL", out response)) {
                throw new Pop3Exception(String.Format(NEGATIVE_RESPONSE, UIDL_REQUEST, response));
            }

            string responseContent = ReceiveContent();
            int EmailId;
            
            string[] responseLines = responseContent.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < responseLines.Length; i++) {
                
                string[] responseSplit = responseLines[i].Split(' ');
                
                if (responseSplit.Length < 2) {
                    throw new Pop3Exception(WRONG_RESPONSE);
                } else if (!int.TryParse(responseSplit[0], out EmailId)) {
                    throw new Pop3Exception(WRONG_RESPONSE);
                } else {
                    result.Add(new EmailUid(EmailId, responseSplit[1]));
                }
                
            }
            
            return result;
        }

        public RxMailMessage GetEmail(int MessageNo) {

            EnsureState(Pop3ConnectionStateEnum.Connected);
            
            string response;
            
            if (!executeCommand(string.Format("RETR {0}", MessageNo), out response)) {
                throw new Pop3Exception(string.Format(NEGATIVE_RESPONSE, RETR_REQUEST , response));
            }
            
            return MimeParser.Parse(ReceiveContent(), false);
        }
        
        public RxMailMessage GetEmailHeader(int MessageNo) {

            //Log.log(LoggingConstants.DEBUG, "BF: ------Started GetEmailHeader");
            DateTime startTime = DateTime.Now;

            EnsureState(Pop3ConnectionStateEnum.Connected);
            
            string response;

           // DateTime startExecuteCommand = DateTime.Now;
            if (!executeCommand(string.Format("TOP {0} 0", MessageNo), out response)) {
                throw new Pop3Exception(string.Format(NEGATIVE_RESPONSE, TOP_REQUEST , response));
            }
           // DateTime stopExecuteCommand = DateTime.Now;

           // DateTime startReceiveContent = DateTime.Now;
            string content = ReceiveContent();
           // DateTime stopReceiveContent = DateTime.Now;

           // DateTime startMimeParser = DateTime.Now;
            RxMailMessage message = MimeParser.Parse(content, true);
           // DateTime stopMimeParser = DateTime.Now;

           // DateTime stopTime = DateTime.Now;

            //Log.log(LoggingConstants.DEBUG, "BF: ----------ExecuteCommand took:" + (stopExecuteCommand - startExecuteCommand).TotalMilliseconds.ToString());
            //Log.log(LoggingConstants.DEBUG, "BF: ----------ReceiveContent took:" + (stopReceiveContent - startReceiveContent).TotalMilliseconds.ToString());
            //Log.log(LoggingConstants.DEBUG, "BF: ----------MimeParser took:" + (stopMimeParser - startMimeParser).TotalMilliseconds.ToString());
            //Log.log(LoggingConstants.DEBUG, "BF: ------Stopped GetEmailHeader took:" + (stopTime - startTime).TotalMilliseconds.ToString());
            return message;
        }
        
        /// <summary>
        /// Get the sizes of all the messages
        /// </summary>
        public List<int> GetEmailSizeList() {
            EnsureState(Pop3ConnectionStateEnum.Connected);
            List<int> result = new List<int>();

            string response;
            if (executeCommand("LIST", out response)) {

                string responseContent = ReceiveContent();
                string[] responseLines = responseContent.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < responseLines.Length; i++) {

                    int size;
                    if (int.TryParse(responseLines[i].Split(' ')[1], out size)) {
                        result.Add(size);
                    } else {
                        throw new Pop3Exception(WRONG_RESPONSE);
                    }
                    
                }
                
            }

            return result;
        }
        
        /// <summary>
        /// Sends an 'empty' command to the POP3 server. Server has to respond with +OK
        /// </summary>
        /// <returns>true: server responds as expected</returns>
        public void NOOP() {
            
            EnsureState(Pop3ConnectionStateEnum.Connected);
            
            string response;
            if (!executeCommand("NOOP", out response)) {
                throw new Pop3Exception(string.Format(NEGATIVE_RESPONSE, "NOOP", response));
            }
            
        }

        /// <summary>
        /// Unmark any emails from deletion. The server only deletes email really
        /// once the connection is properly closed.
        /// </summary>
        /// <returns>true: emails are unmarked from deletion</returns>
        public bool UndeleteAllEmails() {
            EnsureState(Pop3ConnectionStateEnum.Connected);
            string response;
            return executeCommand("RSET", out response);
        }

        /// <summary>
        /// Get mailbox statistics
        /// </summary>
        public bool GetMailboxStats(out int NumberOfMails, out int MailboxSize) {
            EnsureState(Pop3ConnectionStateEnum.Connected);

            //interpret response
            string response;
            NumberOfMails = 0;
            MailboxSize = 0;
            if (executeCommand("STAT", out response)) {
                //got a positive response
                string[] responseParts = response.Split(' ');
                if (responseParts.Length < 2) {
                    //response format wrong
                    throw new Pop3Exception("Server " + _popServer + " sends illegally formatted response." +
                                            "\nExpected format: +OK int int" +
                                            "\nReceived response: " + response);
                }
                NumberOfMails = int.Parse(responseParts[1]);
                MailboxSize = int.Parse(responseParts[2]);
                return true;
            }
            return false;
        }

        public void Dispose() {
            if (_connectionState == Pop3ConnectionStateEnum.Connected) {
                Disconnect();
            }
        }
        
        #endregion

        #region Helper Methods

		private bool IsOkResponse(string strResponse) {
			return (strResponse.Substring(0, 3) == RESPONSE_OK);
		}
        
        private bool executeCommand(string strCommand, out string response) {
            bool result = false;
            response = null;
            
            if( _writer.BaseStream.CanWrite ) {
                
                _writer.WriteLine(strCommand);
                _writer.Flush();
                
                response = _reader.ReadLine();
                
                result = IsOkResponse(response);
            }
            
            return result;
        }

        private string ReceiveContent() {
            
            StringBuilder builder = new StringBuilder();
            
            string strResponse = _reader.ReadLine();
            
            while (strResponse != ".") 
            {
                builder.Append(strResponse + Environment.NewLine);
                
                strResponse = _reader.ReadLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// read single line response from POP3 server. 
        /// <example>Example server response: +OK asdfkjahsf</example>
        /// </summary>
        /// <param name="response">response from POP3 server</param>
        /// <returns>true: positive response</returns>
        private bool readSingleLine(out string response) {
            response = null;
            
            try {
                response = _reader.ReadLine();
            } catch {}

            if (response == null) {
                throw new Pop3Exception("Server " + _popServer + " has not responded, timeout has occurred.");
            }

            return IsOkResponse(response);
        }

        private void setPop3ConnectionState(Pop3ConnectionStateEnum State) {
            _connectionState = State;
        }

        /// <summary>
        /// throw exception if POP3 connection is not in the required state
        /// </summary>
        private void EnsureState(Pop3ConnectionStateEnum requiredState) {
            if (_connectionState != requiredState) {
                
                // wrong connection state
                throw new Pop3Exception( string.Format(
                    "Wrong connection state. Expected : {0}, but detected {1}.",
                    requiredState.ToString(), _connectionState.ToString()));
            }
        }

        private void CallWarning(string methodName, string warningText) {
            
            CallTrace(string.Format("!! method:{0}, warning:{1}", methodName, warningText));
            
        }
        
        private void CallTrace(string text) {
            
            Weborb.Util.Logging.Log.log(
                Weborb.Util.Logging.LoggingConstants.DEBUG, 
                string.Format("{0} {1}", DateTime.Now.ToString("hh:mm:ss"), text) );
            
        }

        #endregion

    }
}