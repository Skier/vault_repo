using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.ServiceBrowser
{
    public class ServiceDataTypeField:ServiceDataTypeContainer
    {
        public ServiceDataTypeField() { }
        public ServiceDataTypeField(String name, ServiceDataType dataType, ServiceDataType parent)
        {
            Name = name;
            Parent = parent;
            DataType = dataType;
        }
    }
}
