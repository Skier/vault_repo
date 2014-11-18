namespace Twilio.TwiML
{
    public class Redirect : TwiML.Verb 
    {
        public Redirect()
            : base(VRedirect, null)
        {
            AllowedVerbs = null;
        }

        public Redirect(string url)
            : base(VRedirect, url)
        {
            AllowedVerbs = null;
        }

        public string Method
        {
            set { Set("method", value); }
        }
    }
}

