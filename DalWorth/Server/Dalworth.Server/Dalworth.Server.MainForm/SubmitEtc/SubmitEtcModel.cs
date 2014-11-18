using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.SubmitEtc
{
    public class SubmitEtcModel
    {
        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

    }
}
