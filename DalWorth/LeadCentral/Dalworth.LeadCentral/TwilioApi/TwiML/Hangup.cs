namespace Twilio.TwiML
{
    public class Hangup : Verb 
    {
        public Hangup()
            : base(VHangup, null)
        {
            AllowedVerbs = null;
        }
    }
}
