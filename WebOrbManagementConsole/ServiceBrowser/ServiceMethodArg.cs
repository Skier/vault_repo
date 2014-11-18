using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.ServiceBrowser
{
    public class ServiceMethodArg : ServiceDataTypeContainer
    {
        public ServiceMethodArg() { }
        public ServiceMethodArg( String name )
        {
            Name = name;
        }
    }
}
