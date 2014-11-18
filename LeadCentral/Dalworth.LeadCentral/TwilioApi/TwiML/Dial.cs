using System.Collections.Generic;

namespace Twilio.TwiML
{
    public class Dial : Verb 
    {
        public Dial()
            : base(VDial, null)
        {
            AllowedVerbs = new List<string> {VNumber, VConference};
        }

        public Dial(string number)
            : base(VDial, number)
        {
            AllowedVerbs = new List<string> {VNumber, VConference};
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

        public bool HangupOnStar
        {
            set { Set("hangupOnStar", value ? "true" : "false"); }
        }

        public int TimeLimit
        {
            set { Set("timeLimit", value.ToString()); }
        }

        public string Callerid
        {
            set { Set("callerid", value); }
        }
    }
}

