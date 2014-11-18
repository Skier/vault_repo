using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.ServiceBrowser
{
    public class ServiceNamespace : ServiceNode
    {
        public static ServiceNamespace Empty = new ServiceNamespace();

        public ServiceNamespace() { }
        public ServiceNamespace( String name, ServiceNamespace parent )
        {
            Name = name;
            Parent = parent;
        }
    }
}
