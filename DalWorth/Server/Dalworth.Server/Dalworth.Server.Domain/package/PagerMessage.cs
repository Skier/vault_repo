using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Dalworth.Server.Domain.Sync;
using Dalworth.Server.SDK;
using Dalworth.Server.Servman.Domain;

namespace Dalworth.Server.Domain.package
{
    public class PagerMessage
    {
        private const int DEFAULT_MESSAGE_LENGTH = 140;
        private const int MAX_MESSAGE_LENGTH = 254;
        public const string BLOCK_SEPARATOR = "{*block*}";

        #region Send

        public static void Send(string servmanTruckId, string message, IDbConnection connection)
        {
            truck truck = Servman.Domain.truck.FindByPrimaryKey(servmanTruckId, connection);
            page_svc service = page_svc.FindByPrimaryKey(truck.serv_id, connection);

            int messageLength;
            if (truck.max_chars > MAX_MESSAGE_LENGTH)
                messageLength = MAX_MESSAGE_LENGTH;
            else if (truck.max_chars == 0)
                messageLength = DEFAULT_MESSAGE_LENGTH;
            else
                messageLength = truck.max_chars;

            List<string> dividedMessages = DivideText(message.Replace("\r\n", " "), messageLength);
            int currentMessagePartNumber = 1;

            foreach (string dividedMessage in dividedMessages)
            {
                pages messageRecord = new pages();
                messageRecord.phone = string.Empty;
                messageRecord.max_baud = 0;
                messageRecord.d_start = DateTime.Now.Date;
                messageRecord.t_start = DateTime.Now.Hour.ToString("00")
                    + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                messageRecord.d_end = Utils.SERVMAN_NULL_DATE;
                messageRecord.t_end = string.Empty;
                messageRecord.pager_num = truck.pager_num;
                messageRecord.response = string.Empty;
                messageRecord.message = dividedMessage;
                messageRecord.response = string.Empty;
                messageRecord.station = 0;
                messageRecord.seq_num = currentMessagePartNumber;
                messageRecord.ticket_num = string.Empty;
                messageRecord.count = 0;
                messageRecord.station = 0;
                messageRecord.email_dom = service.email_dom;
                pages.Insert(messageRecord, connection);

                currentMessagePartNumber++;
            }

        }

        #endregion

        #region DivideText

        private static List<string> DivideText(string text, int maxBlockLength)
        {
            List<string> result = new List<string>();

            while(text != string.Empty)
            {
                string currentBlock;
                if (text.Contains(BLOCK_SEPARATOR))
                {
                    currentBlock = text.Substring(0, text.IndexOf(BLOCK_SEPARATOR));
                    if (currentBlock.Length + BLOCK_SEPARATOR.Length == text.Length)
                        text = string.Empty;
                    else
                        text = text.Substring(currentBlock.Length + BLOCK_SEPARATOR.Length);
                }                    
                else
                {
                    currentBlock = text;
                    text = string.Empty;
                }

                int remainedLength = maxBlockLength;
                if (result.Count > 0)
                    remainedLength = maxBlockLength - result[result.Count - 1].Length;


                if (result.Count > 0 && currentBlock.Length < remainedLength) //check if current block fits in remainder
                {
                    result[result.Count - 1] += " " + currentBlock;                    
                    continue;
                }
                    
                if (currentBlock.Length > maxBlockLength)                
                    result.AddRange(Utils.DivideText(currentBlock, maxBlockLength));
                else
                    result.Add(currentBlock);                                
            }

            return result;
        }

        #endregion

        #region SendViaWebService

        public static void SendViaWebService(string servmanTruckId, string message)
        {
            DalworthSyncService service = new DalworthSyncService();
            service.SendMessage(Configuration.ConnectionKey, servmanTruckId, message);
        }

        #endregion
    }
}
