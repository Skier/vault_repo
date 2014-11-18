using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;

namespace Dalworth.Windows.StartDay.EnterEquipment
{
    public class EnterEquipmentModel : StartDayBaseModel, IModel
    {
        #region WorkEquipment

        private List<WorkEquipment> m_workEquipment;
        public List<WorkEquipment> WorkEquipment
        {
            get { return m_workEquipment; }
            set { m_workEquipment = value; }
        }

        #endregion

        private Dictionary<EquipmentTypeEnum, List<string>> m_equipmentMap;
        private Dictionary<string, Equipment> m_equipmentNumberMap;

        #region Init

        public void Init()
        {                 
            m_workEquipment = Domain.WorkEquipment.FindBy(StartDayModel.Work);

            List<Equipment> equipmentList = Equipment.Find();
            m_equipmentMap = new Dictionary<EquipmentTypeEnum, List<string>>();
            m_equipmentNumberMap = new Dictionary<string, Equipment>();

            foreach (Equipment equipment in equipmentList)
            {                
                if (!m_equipmentMap.ContainsKey(equipment.EquipmentType))
                    m_equipmentMap.Add(equipment.EquipmentType, new List<string>());

                m_equipmentMap[equipment.EquipmentType].Add(equipment.SerialNumber);

                m_equipmentNumberMap.Add(equipment.SerialNumber, equipment);
            }
        }

        #endregion

        #region IsEquipmentExist

        public bool IsEquipmentExist(EquipmentTypeEnum equipmentType, string serialNumber)
        {
            return m_equipmentMap[equipmentType].Contains(serialNumber);
        }

        #endregion

        #region SaveEnteredEquipment

        public void SaveCapturedEquipment(Dictionary<int, Dictionary<int, List<string>>> serialNumbers)
        {
            StartDayModel.CapturedEquipment = new List<WorkTransactionEquipment>();

            for (int i = 0; i < m_workEquipment.Count; i++)
            {
                EquipmentTypeEnum equipmentType = m_workEquipment[i].EquipmentType;

                foreach (List<string> list in serialNumbers[i].Values)
                {
                    foreach (string s in list)
                    {
                        if (s == string.Empty)
                            continue;
                        
                        WorkTransactionEquipment equipment 
                            = new WorkTransactionEquipment(0, 0, m_equipmentNumberMap[s].ID, false, true);
                                                
                        StartDayModel.CapturedEquipment.Add(equipment);
                    }
                }                
            }

            int x = 0;
        }

        #endregion
    }
}
