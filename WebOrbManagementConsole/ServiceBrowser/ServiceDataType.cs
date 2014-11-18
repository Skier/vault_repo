using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Weborb.Management.ServiceBrowser
{
    public class ServiceDataType:ServiceNode
    {
        public ServiceDataType() { }

        public ServiceDataType(String name, ServiceNode parent)
        {
            this.Name = name;
            this.Parent = parent;
        }

        public bool IsComplexType()
        {
            return Items != null && Items.Count > 0 && !IsArray();
        }

        public bool IsArray()
        {
            return Name.Equals("Array");
        }

        public bool IsBoolean()
        {
            return Name.Equals("Boolean");
        }

        public bool IsGeneric()
        {
            return !IsArray() && !IsComplexType();
        }

        private ServiceDataType m_elementType;

        public ServiceDataType ElementType
        {
            get { return m_elementType; }
            set { m_elementType = value; }
        }


        private bool m_isHashTable;

        public bool IsHashTable
        {
            get { return m_isHashTable; }
            set { m_isHashTable = value; }
        }

    }
}
