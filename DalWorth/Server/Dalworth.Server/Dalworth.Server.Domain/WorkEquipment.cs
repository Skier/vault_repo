using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class WorkEquipment 
    {
        public WorkEquipment(){ }

        #region EquipmentType

        [XmlIgnore]
        public EquipmentType EquipmentType
        {
            get
            {
                return EquipmentType.Instance.GetType(EquipmentTypeId);
            }

            set
            {
                EquipmentTypeId = value.ID;
            }
        }

        #endregion

        #region EquipmentTypeText

        [XmlIgnore]
        public string EquipmentTypeText
        {
            get
            {
                return EquipmentType.Type;
            }
        }

        #endregion

        #region FindBy Work

        private const string SqlFindByWork =
            @"SELECT *
            FROM WorkEquipment
                WHERE WorkId = ?WorkId
            order by EquipmentTypeId";

        public static List<WorkEquipment> FindBy(Work work, IDbConnection connection)
        {
            List<WorkEquipment> workEquipmentList = new List<WorkEquipment>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWork, connection))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);

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

        public static List<WorkEquipment> FindBy(Work work)
        {
            return FindBy(work, null);
        }

        #endregion                

        #region DeleteByWork

        private const string SqlDeleteByWork =
            @"DELETE FROM WorkEquipment
                WHERE WorkId = ?WorkId";

        public static void DeleteByWork(Work work)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByWork))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region GetEstimate

        public static List<WorkEquipment> GetEstimate(List<int> visitIds)
        {
            //currently we do not calculate equipment estimate
            List<WorkEquipment> result = new List<WorkEquipment>();
            List<EquipmentType> equipmentTypes = EquipmentType.Find();
            foreach (EquipmentType type in equipmentTypes)
                result.Add(new WorkEquipment(0, 0, type.ID, 0));
            
            return result;
        }

        #endregion
    }
}
      