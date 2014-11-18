using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class ProjectStatus
    {

        private static ProjectStatus c_ProjectStatus = new ProjectStatus();

        public static ProjectStatus GetInstance()
        {
            return c_ProjectStatus;
        }

        private ProjectStatus()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [SubAFEStatus]
            from    [SubAFEStatus]";

        public List<ProjectStatusDataObject> GetProjectStatuses(SqlTransaction tran)
        {
            List<ProjectStatusDataObject> result = new List<ProjectStatusDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    ProjectStatusDataObject projectStatusInfo = new ProjectStatusDataObject();

                    projectStatusInfo.Status = (string)dataReader.GetValue(0);

                    result.Add(projectStatusInfo);
                }
            }

            return result;
        }

    }

}
