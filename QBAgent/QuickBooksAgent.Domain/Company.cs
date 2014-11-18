using System;

namespace QuickBooksAgent.Domain
{
    public partial class Company : ICounterField
    {
        public Company(){}

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_companyId;
            }
            set
            {
                m_companyId = value;
            }
        }

        public string CounterName
        {
            get { return "Company"; }
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return CompanyName;
        }

        #endregion
    }
}
      