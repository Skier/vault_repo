using System;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Address : ICounterField
    {
        public Address(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Address"; }
        }

        #endregion        
    }
}
      