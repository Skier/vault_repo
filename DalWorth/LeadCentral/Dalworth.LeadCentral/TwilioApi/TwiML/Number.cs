namespace Twilio.TwiML
{
    public class Number : Verb 
    {
        public Number(string number)
            : base(VNumber, number)
        {
            AllowedVerbs = null;
        }

        public string SendDigits
        {
            set { Set("sendDigits", value); }
        }

        public string Url
        {
            set { Set("url", value); }
        }
    }
}
