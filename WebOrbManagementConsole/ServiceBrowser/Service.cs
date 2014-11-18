using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Weborb.Management.ServiceBrowser
{
    public class Service : ServiceNode
    {
        public Service() { }

        public Service( ServiceNamespace parent, String name )
        {
            Name = name;
            Parent = parent;
        }
    }
}
