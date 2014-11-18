using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class WorkflowDetail
    {
        public static string VoiceMailWelcomeMessageKey = "Welcome message";
        public static string VoiceMailWelcomeMessageDefaultValue = "Welcome to Dalworth restoration.";
        public static string VoiceMailMessageKey = "Message";
        public static string VoiceMailMessageDefaultValue = "Please leave your message after the tone.";
        public static string RedirectPhoneNumberKey = "Redirect phone number";
        public static string RedirectPhoneNumberDefaultValue = "+12147851062";
        public static string QueueWelcomeMessageFirstKey = "Welcome message 1";
        public static string QueueWelcomeMessageFirstDefaultValue = "Welcome to Dalworth restoration.";
        public static string QueueWelcomeMessageSecondKey = "Welcome message 2";
        public static string QueueWelcomeMessageSecondDefaultValue = "Please wait for available dispatcher.";
        public static string QueueSoundUrlKey = "Sound URL";
        public static string QueueSoundUrlDefaultValue = "chopin.mp3";

        public WorkflowDetail()
        {
            
        }

        private const String SqlSelectByCallWorkflowId = @"
SELECT * FROM WorkflowDetail 
 WHERE CallWorkflowId = ?CallWorkflowId; ";

        public static List<WorkflowDetail> GetByWorkflowId(int workflowId, IDbConnection connection)
        {
            var result = new List<WorkflowDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCallWorkflowId, connection))
            {
                Database.PutParameter(dbCommand, "?CallWorkflowId", workflowId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }
    }
}
      