using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain.WCF
{
    [DataContract]
    public class WcfServiceBusinessException
    {
        public WcfServiceBusinessException(string caption, string message)
        {
            Caption = caption;
            Message = message;
        }

        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
