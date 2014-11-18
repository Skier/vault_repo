namespace Twilio.TwiML
{
    public class Say : Verb 
    {
        public Say(string body)
            : base(VSay, body)
        {
            AllowedVerbs = null;
        }

        public int Loop
        {
            set { Set("loop", value.ToString()); }
        }

        public string Language
        {
            set { Set("language", value); }
        }

        public string Voice
        {
            set { Set("voice", value); }
        }
    }
}

