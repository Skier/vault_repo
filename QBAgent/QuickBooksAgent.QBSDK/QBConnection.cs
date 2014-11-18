using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace QuickBooksAgent.QBSDK
{
    public enum QBConnectionType
    { 
        Secure,
        Unsecure
    }

    public enum QBConnectionStateEnum
    {
        Closed,        
        Connecting,
        PerformingData,
        Uploading,
        WaitingResponse,
        Downloading,
    }

    public enum QBCommandTypeEnum
    {
        Add,
        Update,
        Delete,
        Query,
        Unknown
    }

    public delegate void QBConnectionProgressHandler(int bytesProcessed, int bytesLeft);
    public delegate void QBConnectionStateChangedHandler(QBConnectionStateEnum previosState, QBConnectionStateEnum currentState, QBConnectionStateEnum prevLatestState);

    public delegate void QBConnectionUserLoginInfoHandler(out String login, out String password);

    public class QBExpiredSessionKeyException:QuickBooksAgentException
    {
        public QBExpiredSessionKeyException(String message):base(message)
        { }
    }

    public class QBConnection
    {
        const int PACKET_SIZE = 10000;
        const int CONNECTION_TRIES_COUNT = 4;

        #region Events
        public event QBConnectionProgressHandler DownloadProgress;
        public event QBConnectionProgressHandler UploadProgress;
        public event QBConnectionStateChangedHandler StateChanged;
        #endregion

        #region Fields

        QBConnectionTicket m_connectionTicket;

        public QBConnectionTicket ConnectionTicket
        {
            get { return m_connectionTicket; }
        }

        QBSessionTicket m_sessionTicket;

        public QBSessionTicket SessionTicket
        {
            get { return m_sessionTicket; }
        }

        QBConnectionType m_type;

        public QBConnectionType Type
        {
            get { return m_type; }
        }

        String m_appLogin;

        public String AppLogin
        {
            get { return m_appLogin; }
            set { m_appLogin = value; }
        }

        #region Url
        String m_url;
        public String Url
        {
            get { return m_url; }
        }
        #endregion

        #region State
        QBConnectionStateEnum m_state = QBConnectionStateEnum.Closed;

        public QBConnectionStateEnum State
        {
            get { return m_state; }
            private set 
            {
                if (m_state != value)
                {
                    QBConnectionStateEnum oldState = m_state;

                    if (m_prevLatestState < oldState)
                        m_prevLatestState = oldState;
                    
                    
                    m_state = value;
                    
                    if (StateChanged != null)
                        StateChanged.Invoke(oldState, m_state, m_prevLatestState);
                }
            }
        }
        #endregion

        #endregion

       
        #region Constructor
        public QBConnection(String appLogin,
            QBConnectionTicket connectionTicket)
        {
            m_appLogin = appLogin;
            m_connectionTicket = connectionTicket;
            m_url = Configuration.Instance.QBConnectionURLTicked;
            m_type = QBConnectionType.Unsecure;
        }

        public QBConnection(String appLogin,
            QBSessionTicket sessionTicket)
        {
            m_appLogin = appLogin;
            m_sessionTicket = sessionTicket;
            m_connectionTicket = sessionTicket.ConnectionTicket;
            m_url = Configuration.Instance.QBConnectionURLTicked;
            m_type = QBConnectionType.Secure;
        }
        #endregion


        private QBConnectionStateEnum m_prevLatestState;
        
        
        #region Send
        
        public QBResponse Send(QBRequest request)
        {   
            m_prevLatestState = QBConnectionStateEnum.Closed;
            m_state = QBConnectionStateEnum.Closed;
            
            for (int i = 1; i <= CONNECTION_TRIES_COUNT; i++)
            {
                try
                {
                    return SendRequest(request);
                }
                catch (Exception ex)
                {
                    if (i == CONNECTION_TRIES_COUNT)
                        throw ex;
                }
            }
            
            return null;
        }
        
        
        private QBResponse SendRequest(QBRequest request)
        {
            
            Host.Trace("QBConnection:Send", "Server URL: " + Url);
            
            State = QBConnectionStateEnum.Connecting;

            System.Net.ServicePointManager.CertificatePolicy = new QBCertificatePolicy();

            try
            {
                if (Type == QBConnectionType.Secure
                    && m_sessionTicket.IsExpired)
                {
                    throw new QBExpiredSessionKeyException("Session Key Is Expired");
                }
                
                
                
                HttpWebRequest webRequest = null;
                webRequest = (HttpWebRequest)HttpWebRequest.Create(Url);

                webRequest.KeepAlive = false;

                using (Stream bufferStream = new MemoryStream())
                {
                    State = QBConnectionStateEnum.PerformingData;

                    XmlTextWriter xmlWriter = new XmlTextWriter(
                           bufferStream, Encoding.UTF8);

                    #region Performing XML Request

                    xmlWriter.WriteStartDocument();

                    //xmlWriter.WriteRaw("<!DOCTYPE QBXML PUBLIC '-//INTUIT//DTD QBXML QB0 5.0//EN' 'http://apps.quickbooks.com/dtds/qbxmlops50.dtd'>");
                    xmlWriter.WriteRaw("<?qbxml version=\"5.0\"?>");

                    #region QBXML
                    xmlWriter.WriteStartElement("QBXML");
                    #region SignonMsgsRq
                    xmlWriter.WriteStartElement("SignonMsgsRq");
                    if (Type == QBConnectionType.Unsecure)
                    {
                        #region SignonDesktopRq
                        xmlWriter.WriteStartElement("SignonDesktopRq");
                        #region ClientDateTime
                        xmlWriter.WriteStartElement("ClientDateTime");
                        xmlWriter.WriteString(DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region ApplicationLogin
                        xmlWriter.WriteStartElement("ApplicationLogin");
                        xmlWriter.WriteString(Configuration.QuickBooks.AppLogin);
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region ConnectionTicket
                        xmlWriter.WriteStartElement("ConnectionTicket");
                        xmlWriter.WriteString("XXX-XXX-XXXXXXXXXXXXXXXXXXXXXX");
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region Language
                        xmlWriter.WriteStartElement("Language");
                        xmlWriter.WriteString("English");
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region AppID
                        xmlWriter.WriteStartElement("AppID");
                        xmlWriter.WriteString(m_connectionTicket.AppCode);
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region AppVer
                        xmlWriter.WriteStartElement("AppVer");
                        xmlWriter.WriteString("1.0");
                        xmlWriter.WriteEndElement();
                        #endregion
                        xmlWriter.WriteEndElement();
                        #endregion
                    }
                    else
                    {
                        #region SignonTicketRq
                        xmlWriter.WriteStartElement("SignonTicketRq");
                        #region ClientDateTime
                        xmlWriter.WriteStartElement("ClientDateTime");
                        xmlWriter.WriteString(DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region ConnectionTicket
                        xmlWriter.WriteStartElement("SessionTicket");
                        xmlWriter.WriteString(m_sessionTicket.Ticket);
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region Language
                        xmlWriter.WriteStartElement("Language");
                        xmlWriter.WriteString("English");
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region AppID
                        xmlWriter.WriteStartElement("AppID");
                        xmlWriter.WriteString(m_connectionTicket.AppCode);
                        xmlWriter.WriteEndElement();
                        #endregion
                        #region AppVer
                        xmlWriter.WriteStartElement("AppVer");
                        xmlWriter.WriteString("1.0");
                        xmlWriter.WriteEndElement();
                        #endregion
                        xmlWriter.WriteEndElement();
                        #endregion
                    }
                    xmlWriter.WriteEndElement();
                    #endregion
                    request.ProcessRequest(xmlWriter);
                    xmlWriter.WriteEndElement();
                    #endregion

                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();                    
                    #endregion

                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/x-qbxml";
                    
                    UploadData(webRequest, bufferStream);
                }


                if (Type == QBConnectionType.Secure)
                    m_sessionTicket.UpdateUsageDate();

                DownloadData(webRequest);


                return new QBResponse();
            }
            finally
            {
                //State = QBConnectionStateEnum.Closed;
            }                

        }
        #endregion
                
        #region UploadData
        private void UploadData(WebRequest webRequest,Stream stream)
        {
            State = QBConnectionStateEnum.Uploading;

            stream.Position = 0;

            StreamReader streamReader = new StreamReader(stream);

            int totalBytesForSend = 0;
            
            string data = streamReader.ReadToEnd();

            using (FileStream fileStream = new FileStream(Host.GetPath("request.xml"), FileMode.Create))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.Write(data);

                streamWriter.Close();
            }

            data = data.Replace("XXX-XXX-XXXXXXXXXXXXXXXXXXXXXX", m_connectionTicket.Ticket);

            char[] dataToSend = data.ToCharArray();
            totalBytesForSend = dataToSend.Length;
            
            webRequest.ContentLength = totalBytesForSend;

            using (Stream webRequestStream = webRequest.GetRequestStream())
            {                
                StreamWriter streamWriter = new StreamWriter(webRequestStream);

                int bytesSent = 0;

                while (bytesSent < totalBytesForSend)
                {
                    int bytesToWrite =
                        bytesSent + PACKET_SIZE < totalBytesForSend
                        ? PACKET_SIZE
                        : totalBytesForSend - bytesSent;


                    streamWriter.Write(dataToSend,
                        bytesSent,
                        bytesToWrite);

                    bytesSent += bytesToWrite;

                    if (UploadProgress != null)
                        UploadProgress.Invoke(bytesSent, totalBytesForSend - bytesSent);
                }

                streamWriter.Close();

                webRequestStream.Close();
            }
        }
        #endregion

        #region DownloadData

        private void DownloadData(WebRequest webRequest)
        {
            State = QBConnectionStateEnum.WaitingResponse;

            WebResponse webResponse = webRequest.GetResponse();            

            FileStream returnStream = new FileStream(Host.GetPath("response.xml"), FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(returnStream);

            using (Stream stream = webResponse.GetResponseStream())
            {

                State = QBConnectionStateEnum.Downloading;

                int bytesDownloaded = 0;

                using (StreamReader streamReader = new StreamReader(stream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        char[] buffer = new char[PACKET_SIZE];
                        
                        int readed =
                            streamReader.ReadBlock(buffer, 0, buffer.Length);

                        bytesDownloaded += readed;

                        if (DownloadProgress != null)
                            DownloadProgress.Invoke(bytesDownloaded, ((int)webResponse.ContentLength) - bytesDownloaded);

                        streamWriter.Write(buffer, 0, readed);
                    }
                }

                streamWriter.Flush();
                stream.Close();
            }

            returnStream.Close();
            webResponse.Close();
        }
        #endregion
    }
}
