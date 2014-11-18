using System;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Equipment : ICounterField
    {
        public Equipment(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Equipment"; }
        }

        #endregion        

        #region EquipmentType

        public EquipmentTypeEnum EquipmentType
        {
            get { return (EquipmentTypeEnum)m_equipmentTypeId; }
            set { m_equipmentTypeId = (int)value; }
        }

        #endregion
    }
}
      