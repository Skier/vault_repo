using System.Collections.Generic;

namespace Twilio.TwiML
{
    public class TwiMLResponse : Verb 
    {
        public TwiMLResponse()
            : base(VResponse, null)
        {
            AllowedVerbs = new List<string>
                                    {
                                        VGather,
                                        VRecord,
                                        VDial,
                                        VSay,
                                        VPlay,
                                        VRedirect,
                                        VHangup,
                                        VSms
                                    };
        }
    }
}

