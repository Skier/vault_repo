using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TractInc.TrueTract.Data
{
    internal class GroupItemDataMapper
    {

        #region Constants

        private const string SQL_SELECT_COUNT = @"
            SELECT count(*) 
              FROM [TT_GroupItem] 
             WHERE GroupId = @GroupId
               and DocBranchUid = @DocBranchUid
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_GroupItem] ( GroupId, DocBranchUid )
                 VALUES ( @GroupId, @DocBranchUid )

            select scope_identity();
        ";

        private const string SQL_DELETE = @"
            DELETE [TT_GroupItem]
             WHERE GroupID = @GroupId 
        ";

        private const string SQL_AND_DOC_BRANCH_UID = " and DocBranchUid = @DocBranchUid";

        #endregion

        #region Methods
        
        public bool IsGroupContains(SqlTransaction tran, int groupId, string docBranchUid)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@DocBranchUid", docBranchUid));

            return int.Parse(SQLHelper.ExecuteScalar(
                 tran, CommandType.Text, SQL_SELECT_COUNT, paramList.ToArray()).ToString()) > 0;
        }

        public int Create(SqlTransaction tran, int groupId, string docBranchUid)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@DocBranchUid", docBranchUid));

            return int.Parse(SQLHelper.ExecuteScalar(
                                 tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        }

        public void Delete(SqlTransaction tran, int groupId, string docBranchUid)
        {
            string sqlQuery = SQL_DELETE;

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            
            if (null != docBranchUid)
            {
                sqlQuery += SQL_AND_DOC_BRANCH_UID;

                paramList.Add(new SqlParameter("@DocBranchUid", docBranchUid));
            }

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlQuery, paramList.ToArray());
        }

        #endregion

    }
}
