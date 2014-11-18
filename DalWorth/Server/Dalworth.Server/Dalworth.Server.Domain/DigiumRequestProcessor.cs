using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Dalworth.Server.SDK;
using File = CrystalDecisions.Enterprise.File;

namespace Dalworth.Server.Domain
{
    public class DigiumRequestProcessor
    {
        #region GetCallLogXml

        private const string RequestString = @"<request method='switchvox.callLogs.search'>
	            <parameters>
		            <start_date>{0}</start_date>
		            <end_date>{1}</end_date>
                    <items_per_page>500</items_per_page>
                    {2}
	            </parameters>
            </request>";

        public static List<XmlDocument> GetCallLogXml(DateTime startDate, DateTime endDate)
        {
            ServicePointManager.CertificatePolicy = new TrustAllPolicy();
            ASCIIEncoding encoding = new ASCIIEncoding();

            int totalPages = 1;
            int currentPage = 1;
            List<XmlDocument> results = new List<XmlDocument>();
            while (currentPage <= totalPages)
            {
                Host.Trace("Digium", string.Format("Importing digium Log page {0} from {1}", currentPage, totalPages));
                byte[] data = encoding.GetBytes(string.Format(RequestString,
                    startDate.ToString("u").Replace("Z", string.Empty),
                    endDate.ToString("u").Replace("Z", string.Empty), 
                    currentPage != 1 ? string.Format("<page_number>{0}</page_number>", currentPage) : string.Empty));

                HttpWebRequest myRequest =
                  (HttpWebRequest)WebRequest.Create(Configuration.DigiumApiUrl);
                myRequest.Method = "POST";
                myRequest.ContentType = "text/xml";
                myRequest.KeepAlive = false;
                myRequest.Credentials = new NetworkCredential(Configuration.DigiumLogin, Configuration.DigiumPassword);

                using (Stream newStream = myRequest.GetRequestStream())
                    newStream.Write(data, 0, data.Length);

                using (HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    Encoding encode = Encoding.GetEncoding("utf-8");

                    using (StreamReader readStream = new StreamReader(responseStream, encode))
                    {
                        
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(readStream.ReadToEnd());                        
                                                
                        //document.Save(string.Format(@"C:\Temp\Xml\{0}.xml", currentPage.ToString("000")));

                        XmlNodeList calls = document.GetElementsByTagName("calls");
                        if (calls.Count == 0 || calls[0].Attributes == null)
                            totalPages = 0;
                        else
                            totalPages = int.Parse(calls[0].Attributes["total_pages"].InnerText);
                        currentPage++;
                        results.Add(document);
                    }
                }                  
            }

            return results;
        }

        #endregion

        #region GetDigiumTimeShift

        private const string RequestStringTimeShift =
            @"<request method='switchvox.systemClock.getInfo'>
                <parameters>
                </parameters>
            </request>";


        private static TimeSpan GetDigiumTimeShift()
        {
            ServicePointManager.CertificatePolicy = new TrustAllPolicy();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] data = encoding.GetBytes(RequestStringTimeShift);

            HttpWebRequest myRequest =
                (HttpWebRequest)WebRequest.Create("https://192.168.200.10/xml");
            myRequest.Method = "POST";
            myRequest.ContentType = "text/xml";
            myRequest.KeepAlive = false;
            myRequest.Credentials = new NetworkCredential("admin", "timebomb7");

            DateTime requestStartTime = DateTime.Now;
            using (Stream newStream = myRequest.GetRequestStream())
                newStream.Write(data, 0, data.Length);
            
            using (HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");

                using (StreamReader readStream = new StreamReader(responseStream, encode))
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(readStream.ReadToEnd());

                    XmlNodeList calls = document.GetElementsByTagName("system");
                    DateTime digiumTime = DateTime.Parse(calls[0].Attributes["current_time"].InnerText);
                    Host.Trace("Digium", string.Format("Digium time {0}", digiumTime));
                    TimeSpan requestDuration = DateTime.Now.Subtract(requestStartTime);
                    Host.Trace("Digium", string.Format("Time adjustment accuracy {0}", requestDuration));
                    digiumTime = digiumTime.Subtract(requestDuration);
                    TimeSpan digiumTimeShift = DateTime.Now.Subtract(digiumTime);

                    Host.Trace("Digium", string.Format("Digium time shift {0}", digiumTimeShift));
                    return digiumTimeShift;
                }
            }                                          
        }

        #endregion

        #region ImportCallLog

        public static List<DigiumLogItem> ImportCallLog()
        {
            DateTime? start = null;
            DateTime? end = null;

            if (Configuration.DigiumDateStart.HasValue)
                start = Configuration.DigiumDateStart.Value.Date;
            if (Configuration.DigiumDateEnd.HasValue)
                end = Configuration.DigiumDateEnd.Value.AddHours(23).AddMinutes(59);
            
            if (start == null || end == null)
            {
                DigiumLogItem lastCall = DigiumLogItem.FindLastImportedCall();
                if (lastCall == null)
                    start = DateTime.MinValue;
                else
                    start = lastCall.TimeCreated;

                DateTime realtimeStart = DateTime.Now.AddMinutes(-Configuration.DigiumCallImportRequeryMin);

                if (start > realtimeStart)
                    start = realtimeStart;

                end = DateTime.Now;
            }

            TimeSpan digiumTimeShift = GetDigiumTimeShift();
            List<XmlDocument> callPages = GetCallLogXml(start.Value, end.Value);
            List<DigiumLogItem> result = new List<DigiumLogItem>();

            foreach (XmlDocument callPage in callPages)
            {
                XmlNodeList calls = callPage.SelectNodes(
                    "/response/result/calls/call");
                if (calls == null)
                    return result;

                foreach (XmlNode call in calls)
                {
                    XmlNodeList talks = call.SelectNodes(
                        "events/event[@type = 'TALKING']");
                    if (talks == null || talks.Count == 0)
                        continue;

                    if (call.Attributes == null)
                        continue;
                    if (call.Attributes["origination"].InnerText != "incoming" && call.Attributes["origination"].InnerText != "outgoing")
                        continue;
                    bool isIncoming = call.Attributes["origination"].InnerText == "incoming";                    

                    XmlNode incomingNode = call.SelectSingleNode("events/event[@type = 'INCOMING']");
                    if (isIncoming && incomingNode == null)
                        continue;

                    DateTime digiumTimeStart = DateTime.Parse(call.Attributes["start_time"].InnerText);
                    DateTime digiumTimeTalkStart = DateTime.Parse(talks[0].Attributes["start_time"].InnerText);

                    string callerIdNumber;
                    if (isIncoming)
                        callerIdNumber= call.Attributes["from_number"] != null ? call.Attributes["from_number"].InnerText : string.Empty;
                    else
                        callerIdNumber= call.Attributes["to_number"] != null ? call.Attributes["to_number"].InnerText : string.Empty;

                    string fromName = string.Empty;
                    if (isIncoming)
                        fromName = call.Attributes["from_name"] != null ? call.Attributes["from_name"].InnerText.Trim() : string.Empty;

                    string callExtension;
                    if (isIncoming)
                        callExtension = call.Attributes["to_number"].InnerText.Trim();
                    else
                        callExtension = call.Attributes["from_number"].InnerText.Trim();

                    DigiumLogItem firstTalkItem = new DigiumLogItem(0,
                            call.Attributes["id"].InnerText, isIncoming, 
                            digiumTimeStart.Add(digiumTimeShift), digiumTimeStart,
                            digiumTimeTalkStart.Add(digiumTimeShift), digiumTimeTalkStart,
                            int.Parse(call.Attributes["talk_duration"].InnerText),
                            callerIdNumber, fromName, callExtension,
                            string.Empty,
                            isIncoming ? incomingNode.Attributes["incoming_did"].InnerText : string.Empty,
                            talks.Count > 1, null, string.Empty);

                    if (!firstTalkItem.IsIncoming && firstTalkItem.CallerIdNumber.Length > 10)
                        firstTalkItem.CallerIdNumber = firstTalkItem.CallerIdNumber.Remove(0, firstTalkItem.CallerIdNumber.Length - 10);

                    if (firstTalkItem.CallerIdNumber.Length < 4)
                        continue;

                    if (firstTalkItem.Extension.Length > 5)
                        firstTalkItem.Extension = string.Empty;

                    if (DigiumLogItem.FindBy(firstTalkItem.CallId) != null)
                        continue;

                    if (talks.Count > 1)
                    {
                        for (int i = 0; i < talks.Count; i++)
                        {                            
                            string talkDescription = talks[i].Attributes["display"].InnerText;
                            string extension = string.Empty;

                            if (talkDescription.IndexOf("<") >= 0 && talkDescription.IndexOf(">") >= 0)
                            {
                                int startIndexExtension = talkDescription.IndexOf("<") + 1;
                                int endIndexExtension = talkDescription.IndexOf(">") - 1;
                                extension = talkDescription.Substring(startIndexExtension,
                                    endIndexExtension - startIndexExtension + 1);

                                if (extension.Length > 5)
                                    extension = string.Empty;
                            }

                            int munutes = 0;
                            if (talkDescription.IndexOf("minute") > 0)
                            {
                                int endIndex = talkDescription.IndexOf("minute") - 2;
                                int startIndex = endIndex - 1;
                                while (char.IsDigit(talkDescription[startIndex]))
                                    startIndex--;
                                munutes = int.Parse(talkDescription.Substring(startIndex, 
                                    endIndex - startIndex + 1));
                            }                            
                            
                            int seconds = 0;
                            if (talkDescription.IndexOf("second") > 0)
                            {
                                int endIndex = talkDescription.IndexOf("second") - 2;
                                int startIndex = endIndex - 1;
                                while (char.IsDigit(talkDescription[startIndex]))
                                    startIndex--;
                                seconds = int.Parse(talkDescription.Substring(startIndex, 
                                    endIndex - startIndex + 1));
                            }

                            DateTime timeTalkStart = DateTime.Parse(talks[i].Attributes["start_time"].InnerText);

                            DigiumLogItem logItem = new DigiumLogItem(0,
                                firstTalkItem.CallId, true, 
                                firstTalkItem.TimeCreated, firstTalkItem.TimeCreatedOriginal, 
                                timeTalkStart.Add(digiumTimeShift), timeTalkStart,
                                munutes * 60 + seconds, firstTalkItem.CallerIdNumber, 
                                firstTalkItem.CallerName, extension, string.Empty,
                                firstTalkItem.IncomingDid, i != talks.Count - 1, null, string.Empty);

                            if (!logItem.IsIncoming && logItem.CallerIdNumber.Length > 10)
                                logItem.CallerIdNumber = logItem.CallerIdNumber.Remove(0, logItem.CallerIdNumber.Length - 10);

                            Host.Trace("Digium", string.Format("New call imported, CallID = {0}", logItem.CallId));
                            DigiumLogItem.Insert(logItem);
                            result.Add(logItem);
                        }
                    }
                    else
                    {
                        DigiumLogItem.Insert(firstTalkItem);
                        Host.Trace("Digium", string.Format("New call imported, CallID = {0}", firstTalkItem.CallId));
                        result.Add(firstTalkItem);
                    }                        
                }
            }

            return result;
        }

        #endregion

        #region MatchTransactionsToCalls

        public static void MatchTransactionsToCalls(DateTime? fromDate = null, string rematchWorkflowId = null)
        {
            Host.Trace("Digium", "MatchTransactionsToCalls Matching transactions to calls");

            List<string> servmanWorkflowIds;
            if (rematchWorkflowId != null)
                servmanWorkflowIds = new List<string>()
                                         {
                                             rematchWorkflowId
                                         };
            else if (fromDate.HasValue)
                servmanWorkflowIds = Transaction.FindLatestServmanWorkflowIds(fromDate.Value);
            else
                servmanWorkflowIds = Transaction.FindLatestServmanWorkflowIds(
                    DateTime.Now.AddMinutes(-Configuration.DigiumTransactionImportDelayMin));

            Host.Trace("Digium", "MatchTransactionsToCalls Retrieved " + servmanWorkflowIds.Count + " workflow IDS");

            var i = 0;
            foreach (var workflowId in servmanWorkflowIds)
            {
                i++;
                try
                {
                    Host.Trace("Digium", "MatchTransactionsToCalls Processing workflowID: " + workflowId + "------------------" + i+ "/" + servmanWorkflowIds.Count + "-------------------", 
                        HostTraceLevelEnum.Debug);

                    List<Transaction> transactionsGroup = Transaction.FindByWorkflow(workflowId);
                    DigiumLogItem matchedCall = Transaction.FindMatchedCall(transactionsGroup);
                    if (matchedCall != null)
                    {
                        Host.Trace("Digium", "MatchTransactionsToCalls FOUND MATCH CALL workflowID:" + workflowId + " CALL " + matchedCall.ID, 
                            HostTraceLevelEnum.Debug);

                        foreach (Transaction transactionInGroup in transactionsGroup)
                        {
                            transactionInGroup.DigiumLogItemId = matchedCall.ID;
                            transactionInGroup.MatchCriteria = Transaction.GetEncodedMatchCriteria(matchedCall.IsCustomerPhoneMatch,
                                matchedCall.IsCallerIdMatch, matchedCall.IsManualCallerIdMatch, matchedCall.IsStartedDuringCall);
                            Transaction.Update(transactionInGroup);
                        }
                    }
                    else
                    {
                        Host.Trace("Digium", "MatchTransactionsToCalls NO MATCH FOUND CALL workflowID:" + workflowId, HostTraceLevelEnum.Debug);

                        foreach (Transaction transactionInGroup in transactionsGroup)
                        {
                            transactionInGroup.DigiumLogItemId = null;
                            transactionInGroup.MatchCriteria = null;
                            Transaction.Update(transactionInGroup);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Host.SendErrorEmail("Error Matching transaction, workflowid:" + workflowId + " " +ex);
                    Host.WriteToLogFile("digium", "Error Matching transaction, workflowid: " + workflowId + " " + ex);
                }
            }

            Host.Trace("Digium", "MatchTransactionsToCalls Completed.  Match " + i + " workflows");
        }

        #endregion
    }

    #region TrustAllPolicy

    public class TrustAllPolicy : ICertificatePolicy
    {
        #region ICertificatePolicy Members

        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, 
            WebRequest request, int certificateProblem)
        {
            //Due an Invalid Certificate used in the SIMEM site, we must return true to all invalid SSL Request  
            return true;
        }

        #endregion


        public static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, 
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //Due an Invalid Certificate used in the site, we must return true to all invalid SSL Request  
            return true;
        }
    }

    #endregion

}
