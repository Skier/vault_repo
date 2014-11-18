using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace QuickBooksAgent.QBSDK
{

    public delegate void QBRequestProcessRequest(XmlWriter xmlWriter);


    public class QBRequest
    {
        public event QBRequestProcessRequest Process;

        internal void ProcessRequest(XmlWriter xmlWriter)
        {

            if (Process != null)
                Process.Invoke(xmlWriter);
        }
    }
}
