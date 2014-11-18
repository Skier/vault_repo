namespace Twilio.TwiML
{
    public class Pause : Verb 
    {

        public Pause()
            : base(VPause, null)
        {
            AllowedVerbs = null;
        }

        public int Length
        {
            set { Set("length", value.ToString()); }
        }
    }
}

