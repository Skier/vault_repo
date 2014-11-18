using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class DigiumLogItem
    {
        public DigiumLogItem()
        {
        }

        #region Import

        public static void Import()
        {
            try
            {
                Database.Begin();
                List<DigiumLogItem> importedCalls = DigiumRequestProcessor.ImportCallLog();

                SetCallSources(importedCalls);
                Transaction.Import();
                DigiumRequestProcessor.MatchTransactionsToCalls();
                Order.SetNewOrdersSources();

                Host.Trace("Digium", "Processing voice files");
                Dictionary<string, string> fileNamesMap = ProcessVoiceFiles();
                Database.Commit();

                Host.Trace("Digium", "Moving and encoding voice files");
                foreach (KeyValuePair<string, string> mapPair in fileNamesMap)
                {
                    string newDirectory = mapPair.Value.Substring(0, mapPair.Value.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(newDirectory))
                        Directory.CreateDirectory(newDirectory);

                    if (mapPair.Key.EndsWith(".wav") && !mapPair.Value.EndsWith(".wav"))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = true;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.FileName = Configuration.DigiumLameFolder + "lame.exe";
                        startInfo.Arguments = mapPair.Key + " " + mapPair.Value;

                        using (Process lameProcess = Process.Start(startInfo))
                        {
                            lameProcess.WaitForExit();

                            if (lameProcess.ExitCode != 0)
                            {
                                Host.Trace("Digium", string.Format("Unable to convert to MP3 {0}. Exit code is: {1}",
                                    mapPair.Key, lameProcess.ExitCode));
                                continue;
                            }
                        }

                        File.Delete(mapPair.Key);                        
                    }
                    else
                        File.Move(mapPair.Key, mapPair.Value);                        
                }
                                    
                Host.Trace("Digium", "DONE");
            }
            catch (CustomerNotReadyException)
            {
                Database.Rollback();                
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region FindBy CallId

        private const String SqlFindByCallId =
            @"Select * from DigiumLogItem
                where CallId = ?CallId
              order by TimeCreated desc
              limit 1";

        public static DigiumLogItem FindBy(string callId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCallId))
            {
                Database.PutParameter(dbCommand, "?CallId", callId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion        

        #region FindBy TimeAndNumbers

        private const String SqlFindByTimeAndNumbers =
            @"Select * from DigiumLogItem
                where {0} 
                    and CallerIdNumber = ?CallerIdNumber 
                    and Extension = ?Extension";

        public static DigiumLogItem FindByOriginalTime(DateTime timeStart, DateTime timeEnd, string phoneFrom, string phoneTo)
        {
            bool isOutgoingCall = phoneFrom.Length <= 3;
            string externalPhone = isOutgoingCall ? phoneTo : phoneFrom;
            string extension = isOutgoingCall ? phoneFrom : phoneTo;

            if (isOutgoingCall && externalPhone.Length > 10)
                externalPhone = externalPhone.Remove(0, externalPhone.Length - 10);

            if (externalPhone == "unknown")
                externalPhone = "Unknown <?>";
            if (extension == "unknown")
                extension = "Unknown <?>";

            string query = string.Format(SqlFindByTimeAndNumbers, isOutgoingCall ?
                "TimeCreatedOriginal >= ?TimeIntervalStart and TimeCreatedOriginal <= ?TimeIntervalEnd"
                : "TimeTalkStartedOriginal >= ?TimeIntervalStart and TimeTalkStartedOriginal <= ?TimeIntervalEnd");

            using (IDbCommand dbCommand = Database.PrepareCommand(query))
            {
                Database.PutParameter(dbCommand, "?TimeIntervalStart", timeStart);
                Database.PutParameter(dbCommand, "?TimeIntervalEnd", timeEnd);
                Database.PutParameter(dbCommand, "?CallerIdNumber", externalPhone);
                Database.PutParameter(dbCommand, "?Extension", extension);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region FindLastImportedCall

        private const String SqlFindLastImportedCall =
            @"SELECT * FROM DigiumLogItem
                order by TimeCreated desc
                limit 1";

        public static DigiumLogItem FindLastImportedCall()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLastImportedCall))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region SetCallSources

        private static void SetCallSources(List<DigiumLogItem> logItems)
        {
            Dictionary<string, List<int>> ownPhoneMap = GetOwnPhoneToOrderSourceMap();
            Dictionary<string, List<int>> trackingPhoneMap = GetTrackingPhoneToOrderSourceMap();

            Host.Trace("Digium", "Setting call sources for imported calls");
            foreach (DigiumLogItem logItem in logItems)
            {
                List<int> possibleOrderSources = new List<int>();

                if (ownPhoneMap.ContainsKey(logItem.CallerIdNumber))
                    possibleOrderSources.AddRange(ownPhoneMap[logItem.CallerIdNumber]);

                if (possibleOrderSources.Count == 1)
                {
                    logItem.CallSourceId = possibleOrderSources[0];
                    Update(logItem);
                    continue;
                }

                if (trackingPhoneMap.ContainsKey(logItem.IncomingDid))
                {
                    if (possibleOrderSources.Count == 0)
                    {
                        logItem.CallSourceId = trackingPhoneMap[logItem.IncomingDid][0];
                        Update(logItem);
                        continue;
                    }

                    foreach (int possibleOrderSourceId in trackingPhoneMap[logItem.IncomingDid])
                    {
                        if (possibleOrderSources.Contains(possibleOrderSourceId))
                        {
                            logItem.CallSourceId = possibleOrderSourceId;
                            break;
                        }
                    }

                    if (logItem.CallSourceId.HasValue)
                    {
                        Update(logItem);
                        continue;
                    }

                    logItem.CallSourceId = trackingPhoneMap[logItem.IncomingDid][0];
                }
            }
        }

        private static Dictionary<string, List<int>> GetOwnPhoneToOrderSourceMap()
        {
            List<OrderSourceOwnPhone> ownPhones = OrderSourceOwnPhone.Find();
            Dictionary<string, List<int>> ownPhoneToOrderSourceMap = new Dictionary<string, List<int>>();
            foreach (OrderSourceOwnPhone ownPhone in ownPhones)
            {
                if (!ownPhoneToOrderSourceMap.ContainsKey(ownPhone.Number))
                    ownPhoneToOrderSourceMap.Add(ownPhone.Number, new List<int>());
                ownPhoneToOrderSourceMap[ownPhone.Number].Add(ownPhone.OrderSourceId);
            }

            return ownPhoneToOrderSourceMap;
        }

        private static Dictionary<string, List<int>> GetTrackingPhoneToOrderSourceMap()
        {
            Dictionary<string, List<int>> trackingPhoneToOrderSourceMap = new Dictionary<string, List<int>>();

            List<TrackingPhone> trackingPhones = TrackingPhone.Find();
            Dictionary<int, TrackingPhone> trackingPhoneMap = new Dictionary<int, TrackingPhone>();
            foreach (TrackingPhone trackingPhone in trackingPhones)
                trackingPhoneMap.Add(trackingPhone.ID, trackingPhone);
            List<TrackingPhoneOrderSource> trackingPhoneOrderSources = TrackingPhoneOrderSource.Find();
            foreach (TrackingPhoneOrderSource trackingPhoneOrderSource in trackingPhoneOrderSources)
            {
                string trackingPhone = trackingPhoneMap[trackingPhoneOrderSource.TrackingPhoneId].Number;

                if (!trackingPhoneToOrderSourceMap.ContainsKey(trackingPhone))
                    trackingPhoneToOrderSourceMap.Add(trackingPhone, new List<int>());
                trackingPhoneToOrderSourceMap[trackingPhone].Add(trackingPhoneOrderSource.OrderSourceId);
            }

            return trackingPhoneToOrderSourceMap;
        }

        #endregion

        #region FindByExtension
        private const String SqlFindByExtension =
            @"Select * from DigiumLogItem
                where Extension = ?Extension 
                    and TimeTalkStarted > ?TimeTalkStartBegin and TimeTalkStarted < ?TimeTalkStartEnd
              order by TimeCreated";

        public static List<DigiumLogItem> FindByExtension(string extension, DateTime timeTalkStartBegin, DateTime timeTalkStartEnd)
        {
            List<DigiumLogItem> result = new List<DigiumLogItem>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByExtension))
            {
                Database.PutParameter(dbCommand, "?Extension", extension);
                Database.PutParameter(dbCommand, "?TimeTalkStartBegin", timeTalkStartBegin);
                Database.PutParameter(dbCommand, "?TimeTalkStartEnd", timeTalkStartEnd);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var digiumLogItem = Load(dataReader);
                        result.Add(digiumLogItem);
                    }
                }
            }

            return result;
        }

        #endregion

        #region GetTimeDistanceToTransaction

        public double GetTimeDistanceToTransaction(DateTime transactionTimeStart, DateTime transactionTimeEnd)
        {
            return Math.Abs(transactionTimeStart.Subtract(TimeTalkStarted).TotalSeconds)
                   + Math.Abs(transactionTimeEnd.Subtract(TimeTalkStarted.AddSeconds(DurationSec)).TotalSeconds);
        }

        #endregion

        #region ProcessVoiceFiles

        private static Dictionary<string, string> ProcessVoiceFiles()
        {            
            List<string> newFiles = new List<string>(Directory.GetFiles(Configuration.DigiumIncomingVoiceFilesFolder1));
            newFiles.AddRange(Directory.GetFiles(Configuration.DigiumIncomingVoiceFilesFolder2));
            Dictionary<string, string> oldToNewFileNameMap = new Dictionary<string, string>();

            foreach (var fullFileName in newFiles)
            {
                string fileName = fullFileName.Substring(fullFileName.LastIndexOf('\\') + 1);
                string guidString = "-" + Guid.NewGuid();
                string newFileName = string.Empty;

                if (fileName.EndsWith(".wav"))
                    newFileName = fileName.Replace(".wav", string.Empty) + guidString + ".mp3";
                else if (fileName.EndsWith(".mp3"))
                    newFileName = fileName.Replace(".mp3", string.Empty) + guidString + ".mp3";
                else if (fileName.EndsWith(".xml"))
                    newFileName = fileName.Replace(".xml", string.Empty) + guidString + ".xml";
                else
                    newFileName = fileName;

                if (!fileName.EndsWith(".wav") && !fileName.EndsWith(".mp3") && !fileName.EndsWith(".xml"))
                {
                    oldToNewFileNameMap.Add(fullFileName, Configuration.DigiumOutgoingVoiceFilesFolder + "unknown\\" + fileName);
                    continue;
                }
                
                string[] splittedName = fileName.Split('-', '_', '.');
                if (splittedName.Length < 9)
                {
                    oldToNewFileNameMap.Add(fullFileName, Configuration.DigiumOutgoingVoiceFilesFolder + "unknown\\" + fileName);
                    Host.Trace("Digium", string.Format("ERROR! File {0} ignored. Enexpected pattern", fileName));
                    continue;
                }
                    
                DateTime date;
                try
                {
                    date = new DateTime(int.Parse(splittedName[0]), int.Parse(splittedName[1]), 
                        int.Parse(splittedName[2]), int.Parse(splittedName[3]), int.Parse(splittedName[4]), 
                        int.Parse(splittedName[5]));
                }
                catch (Exception)
                {
                    oldToNewFileNameMap.Add(fullFileName, Configuration.DigiumOutgoingVoiceFilesFolder + "unknown\\" + fileName);
                    Host.Trace("Digium", string.Format("ERROR! File {0} ignored. Unable to parse DateTime", fileName));
                    continue;
                }

                DigiumLogItem item = FindByOriginalTime(date.AddSeconds(-1), date.AddSeconds(1), splittedName[6], splittedName[7]);
                string newFullFileName = string.Format("{0}{1}-{2}-{3}\\{4}", 
                    Configuration.DigiumOutgoingVoiceFilesFolder, splittedName[0], 
                    splittedName[1], splittedName[2], newFileName);

                if (item == null)
                {
                    if (splittedName[6].Length <= 3 && splittedName[7].Length <= 3)
                        oldToNewFileNameMap.Add(fullFileName, newFullFileName);
                    else if (date.AddMinutes(Configuration.DigiumOutdatedVoiceFileMin) < DateTime.Now)
                    {
                        Host.Trace("Digium", string.Format("ERROR! File {0} ignored. Corresponding DigiumLogItem not found", fileName));
                        oldToNewFileNameMap.Add(fullFileName, Configuration.DigiumOutgoingVoiceFilesFolder + "unmatched\\" + fileName);
                    }
                        
                    continue;
                }

                if (newFileName.EndsWith(".mp3"))
                {
                    item.VoiceFileName = newFileName;
                    Update(item);                    
                }
                oldToNewFileNameMap.Add(fullFileName, newFullFileName);
            }

            return oldToNewFileNameMap;
        }

        #endregion

        #region TimeTalkEnd

        public DateTime TimeTalkEnd
        {
            get { return TimeTalkStarted.AddSeconds(DurationSec); }
        }

        #endregion

        #region Match Ranking

        public bool IsStartedDuringCall { get; set; }
        public bool IsCustomerPhoneMatch { get; set; }
        public bool IsCallerIdMatch { get; set; }
        public bool IsManualCallerIdMatch { get; set; }

        public int MatchRank
        {
            get
            {
                var rank = 0;

                if (!IsIncoming)
                {
                    if (IsCustomerPhoneMatch)
                        rank = 1;
                }
                else
                {
                    if (IsCallerIdMatch)
                        rank += 2;

                    if (IsManualCallerIdMatch)
                        rank += 4;

                    if (IsCustomerPhoneMatch)
                        rank += 8;

                    if (IsStartedDuringCall)
                        rank += 16;
                }
                
                return rank;
            }
        }        

        #endregion
    }
}
    