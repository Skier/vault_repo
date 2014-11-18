using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.LeadLookup
{
    public class LeadLookupModel
    {
        #region LeadWrappers

        private BindingList<LeadWrapper> m_leadWrappers;
        public BindingList<LeadWrapper> LeadWrappers
        {
            get { return m_leadWrappers; }
            set { m_leadWrappers = value; }
        }

        #endregion

        #region CustomerAndAddress

        private CustomerAndAddress m_customerAndAddress;
        public CustomerAndAddress CustomerAndAddress
        {
            get { return m_customerAndAddress; }
            set { m_customerAndAddress = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion
    }
}
