using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class TaskItemRequirement
    {
        public TaskItemRequirement(){ }

        #region FindBy Task

        private const string SqlFindByTask =
            @"SELECT *
            FROM TaskItemRequirement
                WHERE TaskId = ?TaskId";

        public static List<TaskItemRequirement> FindBy(Task task, IDbConnection connection)
        {
            List<TaskItemRequirement> itemRequirements = new List<TaskItemRequirement>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTask, connection))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        itemRequirements.Add(Load(dataReader));
                    }
                }
            }
            return itemRequirements;
        }

        public static List<TaskItemRequirement> FindBy(Task task)
        {
            return FindBy(task, null);
        }

        #endregion        
    }
}
      