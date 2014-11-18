using System;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class WorkTransactionEquipment : ICounterField
    {
        public WorkTransactionEquipment(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "WorkTransactionEquipment"; }
        }

        #endregion        
    }
}
      