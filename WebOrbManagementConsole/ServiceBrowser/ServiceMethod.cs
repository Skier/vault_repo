using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Weborb.Management.ServiceBrowser
{
    public class ServiceMethod : ServiceNode
    {
        public ServiceMethod() { }
        public ServiceMethod( String name )
        {
            Name = name;
        }

        ServiceDataType m_returnDataType;

        public ServiceDataType ReturnDataType
        {
            get { return m_returnDataType; }
            set { m_returnDataType = value; }
        }

        private bool m_called;

        internal bool Called
        {
            get { return m_called; }
            set { m_called = value; }
        }
    }
}
