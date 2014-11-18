using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ArriveVisit
{
    public class ArriveVisitModel : IModel
    {
        #region WorkDetail

        private WorkDetail m_workDetail;
        public WorkDetail WorkDetail
        {
            get { return m_workDetail; }
            set { m_workDetail = value; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_work = Work.FindByPrimaryKey(m_workDetail.WorkId);
        }

        #endregion
    }
}
