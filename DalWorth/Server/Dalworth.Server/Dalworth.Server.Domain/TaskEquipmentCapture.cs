using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class TaskEquipmentCapture
    {
        public TaskEquipmentCapture(){ }

        #region FindBy Task

        private const string SqlFindByTask =
            @"SELECT *
            FROM TaskEquipmentCapture
                WHERE TaskId = ?TaskId";

        public static List<TaskEquipmentCapture> FindBy(Task task, IDbConnection connection)
        {
            List<TaskEquipmentCapture> captures = new List<TaskEquipmentCapture>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTask, connection))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        captures.Add(Load(dataReader));
                    }
                }
            }
            return captures;
        }

        public static List<TaskEquipmentCapture> FindBy(Task task)
        {
            return FindBy(task, null);
        }

        #endregion        

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"SELECT *
            FROM TaskEquipmentCapture
                WHERE TaskId in (select TaskId from VisitTask where VisitId = ?VisitId)";

        public static List<TaskEquipmentCapture> FindBy(Visit visit, IDbConnection connection)
        {
            List<TaskEquipmentCapture> captures = new List<TaskEquipmentCapture>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit, connection))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        captures.Add(Load(dataReader));
                    }
                }
            }
            return captures;
        }

        public static List<TaskEquipmentCapture> FindBy(Visit visit)
        {
            return FindBy(visit, null);
        }

        #endregion        
    }
}
      