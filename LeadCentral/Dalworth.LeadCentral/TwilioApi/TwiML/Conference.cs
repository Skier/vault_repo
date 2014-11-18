namespace Twilio.TwiML
{
    public class Conference : Verb 
    {
        public Conference(string name) : base(VConference , name)
        {
            AllowedVerbs = null;
        }
     
        private void SetBoolean(string attr, bool boolean)
        {
            Set(attr, boolean ? "true" : "false");
        }

        public bool Muted
        {
            set { SetBoolean("muted", value); }
        }

        public bool Beep
        {
            set { SetBoolean("beep", value); }
        }

        public bool StartConferenceOnEnter
        {
            set { SetBoolean("startConferenceOnEnter", value); }
        }

        public bool EndConferenceOnExit
        {
            set { SetBoolean("endConferenceOnExit", value); }
        }

        public string WaitMethod
        {
            set { Set("waitMethod", value); }
        }

        public string WaitUrl
        {
            set { Set("waitUrl", value); }
        }
    }
}

