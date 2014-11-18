using System;
using System.Xml.Serialization;

namespace Dalworth.Server.Domain
{
    public enum EquipmentStatusEnum
    {
        InService = 1,
        Retired = 2,
        Broken = 3
    }

    public partial class EquipmentStatus
    {
        public EquipmentStatus(){ }

        #region GetText

        public static string GetText(EquipmentStatusEnum equipmentStatus)
        {
            if (equipmentStatus == EquipmentStatusEnum.InService)
                return "In Service";
            if (equipmentStatus == EquipmentStatusEnum.Retired)
                return "Retired";
            if (equipmentStatus == EquipmentStatusEnum.Broken)
                return "Broken";

            return string.Empty;

        }

        #endregion
    }
}
      