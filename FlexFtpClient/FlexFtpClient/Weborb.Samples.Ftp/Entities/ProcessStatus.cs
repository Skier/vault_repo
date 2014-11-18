using System;

namespace Weborb.Samples.Ftp.Entities
{
    public class ProcessStatus
    {
        internal const string INIT = "INIT";
        internal const string CHECKING_DIRECTORY_SIZE = "CHECKING_DIRECTORY_SIZE";
        internal const string COMPRESSING = "COMPRESSING";
        internal const string DOWNLOADING = "DOWNLOADING";
        internal const string DOWNLOAD_COMPLETED = "DOWNLOAD_COMPLETED";
        internal const string UPLOADING = "UPLOADING";
        internal const string UPLOAD_COMPLETED = "UPLOAD_COMPLETED";
        internal const string TERMINATED = "TERMINATED";
        internal const string ERROR = "ERROR";

        private String processId;
        private String state;
        private String description;
        private int totalBytes;
        private int processedBytes;
        private String exceptionMessage;

        public String ProcessId
        {
            get { return processId; }
            set { processId = value; }
        }
        
        public String State
        {
            get { return state; }
            set { state = value; }
        }

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }

        public int ProcessedBytes
        {
            get { return processedBytes; }
            set { processedBytes = value; }
        }

        public String ExceptionMessage
        {
            get { return exceptionMessage; }
            set { exceptionMessage = value; }
        }

        public ProcessStatus()
        {
        }

        public ProcessStatus(String processId, String state, int totalBytes, int processedBytes, String exceptionMessage)
        {
            this.processId = processId;
            this.state = state;
            this.totalBytes = totalBytes;
            this.processedBytes = processedBytes;
            this.exceptionMessage = exceptionMessage;
        }
        
        public ProcessStatus Clone()
        {
            return new ProcessStatus(processId, state, totalBytes, processedBytes, exceptionMessage);
        }

    }
}
