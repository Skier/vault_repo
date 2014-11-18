namespace Twilio.TwiML
{
    public class Record : TwiML.Verb 
    {
        public Record()
            : base(VRecord, null)
        {
            AllowedVerbs = null;
        }

        public string Action
        {
            set { Set("action", value); }
        }

        public string Method
        {
            set { Set("method", value); }
        }

        public int Timeout
        {
            set { Set("timeout", value.ToString()); }
        }

        public string FinishOnKey
        {
            set { Set("finishOnKey", value); }
        }

        public int MaxLength
        {
            set { Set("maxLength", value.ToString()); }
        }

        public bool Transcribe
        {
            set { Set("transcribe", value ? "true" : "false"); }
        }

        public string TranscribeCallback
        {
            set { Set("transcribeCallback", value); }
        }
    }

}