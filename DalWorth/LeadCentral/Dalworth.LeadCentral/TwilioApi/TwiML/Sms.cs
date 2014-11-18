namespace Twilio.TwiML
{
    public class Sms : TwiML.Verb 
    {
        public Sms(string message)
            : base(VSms, message)
        {
            AllowedVerbs = null;
        }

        public string To
        {
            set { Set("to", value); }
        }

        public string From
        {
            set { Set("from", value); }
        }

        public string Method
        {
            set { Set("method", value); }
        }

        public string Action
        {
            set { Set("action", value); }
        }

        public string StatusCallback
        {
            set { Set("statusCallback", value); }
        }
    }
}
