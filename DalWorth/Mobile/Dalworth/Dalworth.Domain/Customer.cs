using System;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Customer : ICounterField
    {
        public Customer(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Customer"; }
        }

        #endregion        
    }
}
      