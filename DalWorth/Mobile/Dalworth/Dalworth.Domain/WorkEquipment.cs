using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class WorkEquipment : ICounterField
    {
        public WorkEquipment(){ }

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "WorkEquipment"; }
        }

        #endregion        

        #region EquipmentType

        public EquipmentTypeEnum EquipmentType
        {
            get { return (EquipmentTypeEnum)m_equipmentTypeId; }
            set { m_equipmentTypeId = (int)value; }
        }

        #endregion

        #region FindBy Work

        private const string SqlFindByWork =
            @"SELECT *
            FROM WorkEquipment
                WHERE WorkId = @WorkId";

        public static List<WorkEquipment> FindBy(Work work)
        {
            List<WorkEquipment> workEquipmentList = new List<WorkEquipment>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWork))
            {
                Database.PutParameter(dbCommand, "@WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        workEquipmentList.Add(Load(dataReader));
                    }
                }
            }
            return workEquipmentList;
        }

        #endregion        
    }
}
      