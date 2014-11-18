using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dalworth.Server.Web.Partner.Models
{
    public class CallDetail
    {
        public CallDetail(CallLogItem call)
        {
            Call = call;
        }

        public CallLogItem Call { get; set; }
        public string BackLinkQueryString { get; set; }
    }
}