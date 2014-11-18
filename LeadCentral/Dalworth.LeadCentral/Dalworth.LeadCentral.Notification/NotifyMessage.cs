using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dalworth.LeadCentral.Notification
{
    public class NotifyMessage
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string HtmlBody { get; set; }
    }
}
