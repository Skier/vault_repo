using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CreateProjectScope
{
    public class CreateProjectScopeModel : IModel
    {
        #region ProjectBillPay

        private ProjectConstructionScope m_projectScope;
        public ProjectConstructionScope ProjectScope
        {
            get { return m_projectScope; }
            set { m_projectScope = value; }
        }

        #endregion

        #region IsNewProjectBillPay

        public bool IsNewProjectBillPay
        {
            get { return ProjectScope.ProjectId == 0; }
        }

        #endregion

        #region BasedOn

        private ProjectConstructionScope m_basedOn;
        public ProjectConstructionScope BasedOn
        {
            get { return m_basedOn; }
            set { m_basedOn = value; }
        }

        #endregion

        #region ScopeTypes

        private List<ProjectConstructionScopeType> m_scopeTypes;
        public List<ProjectConstructionScopeType> ScopeTypes
        {
            get { return m_scopeTypes; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_scopeTypes = ProjectConstructionScopeType.Find();

            if (m_projectScope == null)
            {
                m_projectScope = new ProjectConstructionScope();
                m_projectScope.IsVoided = false;
            }

            if (m_basedOn != null)
            {
                m_projectScope.JobType = m_basedOn.JobType;
                m_projectScope.ScopeDate = m_basedOn.ScopeDate;
                m_projectScope.ScopeType = m_basedOn.ScopeType;
                m_projectScope.Amount = m_basedOn.Amount;
                m_projectScope.Notes = m_basedOn.JobType;
                m_projectScope.IsVoided = false;
            }
        }

        #endregion
    }
}
