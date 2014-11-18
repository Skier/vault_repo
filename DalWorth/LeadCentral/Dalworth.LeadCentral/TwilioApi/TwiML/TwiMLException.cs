using System;

namespace Twilio.TwiML
{
    public class TwiMLException : Exception 
    {
        public TwiMLException(string message) 
            : base (message)
        {
        }
    
        public TwiMLException(Exception exception)
            : base(exception.Message)
        {
        }
    }
}

