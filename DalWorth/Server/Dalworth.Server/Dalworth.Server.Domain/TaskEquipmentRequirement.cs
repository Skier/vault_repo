using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class TaskEquipmentRequirement
    {
        public TaskEquipmentRequirement(){}

        #region EquipmentType

        [XmlIgnore]
        public EquipmentType EquipmentType
        {
            get
            {
                if (EquipmentTypeId.HasValue)
                    return EquipmentType.Instance.GetType(EquipmentTypeId.Value);
                return null;
            }

            set
            {
                if (value == null)
                    EquipmentTypeId = null;
                else
                    EquipmentTypeId = value.ID;
            }
        }

        #endregion

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"SELECT *
            FROM TaskEquipmentRequirement
                WHERE TaskId in (select TaskId from VisitTask where VisitId = ?VisitId)";

        public static List<TaskEquipmentRequirement> FindBy(Visit visit)
        {
            List<TaskEquipmentRequirement> taskEquipmentRequirements = new List<TaskEquipmentRequirement>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        taskEquipmentRequirements.Add(Load(dataReader));
                    }
                }
            }
            return taskEquipmentRequirements;
        }

        #endregion        
    }
}
      