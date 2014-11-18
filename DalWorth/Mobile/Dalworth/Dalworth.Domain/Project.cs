using System;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Project : ICounterField
    {
        public Project(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Project"; }        
        }

        #endregion        
    }
}
      