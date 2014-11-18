using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class Service
    {
        public Service(){ }

        #region Services

        private static Dictionary<int, Service> m_services;
        public static Dictionary<int, Service> Services
        {
            get
            {
                if (m_services == null)
                {
                    List<Service> services = Find();
                    m_services = new Dictionary<int, Service>();
                    foreach (Service service in services)
                        m_services.Add(service.ID, service);
                }

                return m_services;
            }
        }

        public static Service GetService(int id)
        {
            return Services[id];
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
      