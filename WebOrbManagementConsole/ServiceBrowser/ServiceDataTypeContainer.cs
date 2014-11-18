using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.ServiceBrowser
{
    public abstract class ServiceDataTypeContainer:ServiceNode
    {

        ServiceDataType m_dataType;

        public ServiceDataType DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }

    }
}
