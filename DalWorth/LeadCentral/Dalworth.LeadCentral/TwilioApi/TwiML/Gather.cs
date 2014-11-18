using System.Collections.Generic;

namespace Twilio.TwiML
{
    public class Gather : Verb {

        public Gather()
            : base(VGather, null)
        {
            AllowedVerbs = new List<string> {VSay, VPlay, VPause};
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

        public int NumDigits
        {
            set { Set("numDigits", value.ToString()); }
        }
    }
}

