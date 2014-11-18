using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class Project
    {

        private static Project c_Project = new Project();

        public static Project GetInstance()
        {
            return c_Project;
        }

        private Project()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [SubAFE],
                    [AFE],
                    [SubAFEStatus],
                    [ShortName],
                    [Deleted],
                    [Temporary]
            from    [SubAFE]";

        public List<ProjectDataObject> GetProjects(SqlTransaction tran)
        {
            List<ProjectDataObject> result = new List<ProjectDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    ProjectDataObject projectInfo = new ProjectDataObject();

                    projectInfo.SubAFE = (string)dataReader.GetValue(0);
                    projectInfo.AFE = (string)dataReader.GetValue(1);
                    projectInfo.SubAFEStatus = (string)dataReader.GetValue(2);
                    projectInfo.ShortName = (string)dataReader.GetValue(3);
                    projectInfo.Deleted = (bool)dataReader.GetValue(4);
                    projectInfo.Temporary = (bool)dataReader.GetValue(5);

                    result.Add(projectInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [SubAFE]
              ( [SubAFE],
                [AFE],
                [SubAFEStatus],
                [ShortName],
                [Deleted],
                [Temporary])
        values( @SubAFE,
                @AFE,
                @SubAFEStatus,
                @ShortName,
                @Deleted,
                @Temporary)";

        public void Insert(SqlTransaction tran, ProjectDataObject projectInfo)
        {
            DbParameter[] parms = new DbParameter[6] {
                new SqlParameter("@SubAFE",       projectInfo.SubAFE),
                new SqlParameter("@AFE",          projectInfo.AFE),
                new SqlParameter("@SubAFEStatus", projectInfo.SubAFEStatus),
                new SqlParameter("@ShortName",    projectInfo.ShortName),
                new SqlParameter("@Deleted",      projectInfo.Deleted),
                new SqlParameter("@Temporary",    projectInfo.Temporary)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [SubAFE]
        set     [AFE]          = @AFE,
                [SubAFEStatus] = @SubAFEStatus,
                [ShortName]    = @ShortName,
                [Deleted]      = @Deleted,
                [Temporary]    = @Temporary
        where   [SubAFE]       = @SubAFE";

        public void Update(SqlTransaction tran, ProjectDataObject projectInfo)
        {
            DbParameter[] parms = new DbParameter[6] {
                new SqlParameter("@SubAFE",       projectInfo.SubAFE),
                new SqlParameter("@AFE",          projectInfo.AFE),
                new SqlParameter("@SubAFEStatus", projectInfo.SubAFEStatus),
                new SqlParameter("@ShortName",    projectInfo.ShortName),
                new SqlParameter("@Deleted",      projectInfo.Deleted),
                new SqlParameter("@Temporary",    projectInfo.Temporary)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        update  [SubAFE]
        set     [Deleted] = 1
        where   [SubAFE] = @SubAFE";

        public void Remove(SqlTransaction tran, string project)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@SubAFE", project)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_CAN_REMOVE_PROJECT = @"
        select  *
        from    [BillItem] bi
                inner join [AssetAssignment] aa
                        on bi.[AssetAssignmentId] = aa.[AssetAssignmentId]
        where   bi.[Status] <> 'CONFIRMED'
        and     aa.[SubAFE] = @Project";

        public bool CanRemoveProject(SqlTransaction tran, string project)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@Project", project) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_CAN_REMOVE_PROJECT, parms))
            {
                if (dataReader.Read())
                {
                    return false;
                }
            }

            return true;
        }

    }

}
