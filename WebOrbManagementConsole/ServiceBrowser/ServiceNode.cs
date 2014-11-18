using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.ServiceBrowser
{
    public class ServiceNode
    {
        private List<ServiceNode> m_items = new List<ServiceNode>();
        private String m_name = String.Empty;
        ServiceNode m_parent;

        public ServiceNode Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        public List<ServiceNode> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        internal String getFullName()
        {
            ServiceNode serviceNode = Parent;
            String fullName = Name;

            while( serviceNode != null )
            {
                fullName = String.Format( "{0}.{1}", serviceNode.Name, fullName );
                serviceNode = serviceNode.Parent;
            }

            return fullName;
        }

        internal static void Sort(List<ServiceNode> serviceNodeList)
        {

            serviceNodeList.Sort(delegate(ServiceNode l, ServiceNode r) 
            {
                if (!l.GetType().Equals(r.GetType()))
                {
                    if (l.IsNamespace())
                        return -1;
                    else
                        return 0;
                }
                    
                return l.Name.CompareTo(r.Name); 

            });


            foreach (ServiceNode serviceNode in serviceNodeList)
            {
                if(!serviceNode.IsService())
                    Sort(serviceNode.Items);
            }
        }

        internal bool IsService()
        {
            return this is Service;
        }

        internal bool IsNamespace()
        {
            return this is ServiceNamespace;
        }
    }
}
