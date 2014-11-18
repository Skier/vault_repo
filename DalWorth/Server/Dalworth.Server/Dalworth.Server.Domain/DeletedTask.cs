using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class DeletedTask
    {
        public DeletedTask(){ }

        #region FindTasksToDeleteInServman

        private const string SqlFindTasksToDeleteInServman =
            @"SELECT *
            FROM DeletedTask
                WHERE LastSyncDate is null";

        public static List<DeletedTask> FindTasksToDeleteInServman()
        {
            List<DeletedTask> deletedTasks = new List<DeletedTask>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTasksToDeleteInServman))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        deletedTasks.Add(Load(dataReader));
                }
            }

            return deletedTasks;
        }

        #endregion        
    }
}
      