using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.ServiceBrowser
{
    class ServiceNodeMap
    {
        List<ServiceNode> m_items = new List<ServiceNode>();

        public List<ServiceNode> Items
        {
            get
            {
                return m_items;
            }
        }

        private Dictionary<String, ServiceNamespace> m_namespaces = new Dictionary<string, ServiceNamespace>();

        public ServiceNamespace getNamespace( String fullNamespace )
        {
            if( fullNamespace == null )
                return null;

            String[] parts = fullNamespace.Split( '.' );

            ServiceNamespace lastNamespacePart = null;
            String dynamicNs = String.Empty;

            foreach( String nsPart in parts )
            {
                if( !String.Empty.Equals( dynamicNs ) )
                    dynamicNs = String.Format( "{0}.{1}", dynamicNs, nsPart );
                else
                    dynamicNs = nsPart;

                ServiceNamespace serviceNamespace = null;

                if( m_namespaces.ContainsKey( dynamicNs ) )
                    serviceNamespace = m_namespaces[ dynamicNs ];
                else
                {
                    serviceNamespace = new ServiceNamespace( nsPart, lastNamespacePart );

                    if( lastNamespacePart != null )
                        lastNamespacePart.Items.Add( serviceNamespace );
                    else
                        m_items.Add( serviceNamespace );

                    m_namespaces.Add( serviceNamespace.getFullName(), serviceNamespace );
                }

                lastNamespacePart = serviceNamespace;
            }

            if( lastNamespacePart == null )
                lastNamespacePart = new ServiceNamespace( fullNamespace, null );

            return lastNamespacePart;
        }
    }
}
