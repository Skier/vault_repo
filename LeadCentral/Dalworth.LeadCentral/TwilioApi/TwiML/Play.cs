namespace Twilio.TwiML
{
    public class Play : Verb 
    {
        public Play(string body)
            : base("Play", body)
        {
            AllowedVerbs = null;
        }

        public int Loop
        {
            set { Set("loop", value.ToString()); }
        }
    }

}
