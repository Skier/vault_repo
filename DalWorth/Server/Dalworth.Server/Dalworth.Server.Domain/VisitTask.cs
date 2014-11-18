using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class VisitTask
    {
        public VisitTask(){}

        #region DeleteBy Visit

        private const string SqlDeleteByVisit =
            @"delete
            FROM VisitTask
                WHERE VisitId = ?VisitId";

        public static void DeleteBy(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"SELECT * 
            FROM VisitTask
                WHERE VisitId = ?VisitId";

        public static List<VisitTask> FindBy(Visit visit)
        {
            List<VisitTask> result = new List<VisitTask>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;

        }

        #endregion

        #region DeleteBy Task

        private const string SqlDeleteByTask =
            @"delete
            FROM VisitTask
                WHERE TaskId = ?TaskId";

        public static void DeleteBy(Task task)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByTask))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
      