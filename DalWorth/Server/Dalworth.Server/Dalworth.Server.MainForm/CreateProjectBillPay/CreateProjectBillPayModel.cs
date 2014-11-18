using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CreateProjectBillPay
{
    public class CreateProjectBillPayModel : IModel
    {
        #region ProjectBillPay

        private ProjectConstructionBillPay m_projectBillPay;
        public ProjectConstructionBillPay ProjectBillPay
        {
            get { return m_projectBillPay; }
            set { m_projectBillPay = value; }
        }

        #endregion

        #region IsNewProjectBillPay

        public bool IsNewProjectBillPay
        {
            get { return ProjectBillPay.ProjectId == 0; }
        }

        #endregion

        #region BillPayTypes

        private List<ProjectConstructionBillPayType> m_billPayTypes;
        public List<ProjectConstructionBillPayType> BillPayTypes
        {
            get { return m_billPayTypes; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_billPayTypes = ProjectConstructionBillPayType.Find();

            if (m_projectBillPay == null)
            {
                m_projectBillPay = new ProjectConstructionBillPay();
                m_projectBillPay.IsVoided = false;
            }
        }

        #endregion
    }
}
