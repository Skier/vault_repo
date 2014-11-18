using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Xml;
using System.Text;
//using MobileTech.ServiceLayer.VCHost;
using System.Diagnostics;


namespace MobileTech.Windows.UI.TComm
{
    class HttpComm
    {
        #region Private Data
        // Form size
        //private Size realSize;
        // Http request/response
        private HttpWebRequest m_req;
        private HttpWebResponse m_resp;
        private FileStream m_fs;
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        // Data buffer for stream operations
        private byte[] dataBuffer;
        private const int DataBlockSize = 65536;

        // Configuration
        //MGSprivate Utils utils = new Utils();
        private string _updateUrl;
        private string _localFile;
        #endregion

        public bool Download(string packageFile)
        {
            string appDir = Path.GetDirectoryName(Configuration.AppNameFullPath);
            string tempPath = String.Format(Host.Configuration.GetString(ConfigurationKey.HttpUpdateTemp),appDir);
            _localFile = tempPath + @"\" + packageFile;
            _updateUrl = Host.Configuration.GetString(ConfigurationKey.HttpUpdateUrl) + packageFile;
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            try
            {
                // Create asynchronous web request
                m_req = (HttpWebRequest)HttpWebRequest.Create(_updateUrl);
                // Create a New 'NetworkCredential' object.
                NetworkCredential networkCredential = new NetworkCredential(Host.Configuration.GetString(ConfigurationKey.VCLogin),
                                                     Host.Configuration.GetString(ConfigurationKey.VCPassword));
                m_req.Credentials = networkCredential;
                m_req.Timeout = 10000000;


                m_req.BeginGetResponse(new AsyncCallback(ResponseReceived), null);
                allDone.WaitOne();
            }
            catch (Exception)// ex)
            {
                //MessageBox.Show(ex.Message);
            }
            if (m_req.HaveResponse)
            {
                //MessageBox.Show("Received response");
            }
            m_req = null;
            //Application.DoEvents();

            return true;
        }

        void ResponseReceived(IAsyncResult res)
        {
            // Try getting the web response. If there was an error (404 or other),
            // web exception will be thrown hete
            try
            {
                m_resp = (HttpWebResponse)m_req.EndGetResponse(res);
            }
            catch (Exception)// ex)
            {
                //MessageBox.Show(ex.Message);
                return;
            }
            dataBuffer = new byte[DataBlockSize];
            // Prepare the propgres bar
            //maxVal = (int)m_resp.ContentLength;
            //pbProgress.Invoke(new EventHandler(SetProgressMax));
            m_fs = new FileStream(_localFile, FileMode.Create);

            // Start reading from network stream asynchronously
            m_resp.GetResponseStream().BeginRead(dataBuffer, 0, DataBlockSize, new AsyncCallback(OnDataRead), this);
            allDone.Set();
        }

        // Asynchronous network stream reading
        void OnDataRead(IAsyncResult res)
        {
            // Get bytecount of the received chunk
            int nBytes = m_resp.GetResponseStream().EndRead(res);
            // Dump it to the output stream
            m_fs.Write(dataBuffer, 0, nBytes);

            if (nBytes > 0)
            {
                // If read was successful, continue reading asynchronously as there is more data
                m_resp.GetResponseStream().BeginRead(dataBuffer, 0, DataBlockSize, new AsyncCallback(OnDataRead), this);
            }
            else
            {
                // Otherwise close the stream and proceed with installation
                m_fs.Close();
                m_fs = null;
                //this.Invoke(new EventHandler(this.AllDone));
            }
        }
    }
}
