using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;

namespace Dalworth.Server.Domain.package
{
    public class ProjectPackage
    {
        #region Project

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
            set { m_project = value; }
        }

        #endregion

        #region Visits

        private List<Visit> m_visits;
        public List<Visit> Visits
        {
            get { return m_visits; }
            set { m_visits = value; }
        }

        #endregion        
    }
}
