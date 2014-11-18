using System;
using System.Collections.Generic;

namespace Dalworth.Server.Domain
{
    public partial class EquipmentType
    {
        private EquipmentType() {}

        //Equipment types cache
        private Dictionary<int, EquipmentType> m_equipmentTypes;

        private static EquipmentType m_instance;
        public static EquipmentType Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new EquipmentType();
                return m_instance;
            }
        }

        #region GetType

        public EquipmentType GetType(int equipmentTypeId)
        {
            if (m_equipmentTypes == null)
            {
                List<EquipmentType> equipmentTypes = Find();
                m_equipmentTypes = new Dictionary<int, EquipmentType>();
                foreach (EquipmentType type in equipmentTypes)
                {
                    m_equipmentTypes.Add(type.ID, type);
                }
            }

            return m_equipmentTypes[equipmentTypeId];            
        }

        #endregion

        #region Equals & GetHashCode

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            EquipmentType equipmentType = obj as EquipmentType;
            if (equipmentType == null) return false;
            return m_iD == equipmentType.m_iD;
        }

        public override int GetHashCode()
        {
            return m_iD;
        }

        #endregion
    }
}
      