using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace TractInc.TrueTract.Data
{
    internal class GroupItemsDataMapper
    {

        #region Constants

        private const string SQL_CREATE = @"
            INSERT INTO [GroupItems] ( GroupId, DocumentId, DrawingId )
                 VALUES ( @GroupId, @DocumentId, @DrawingId )

            select scope_identity();
        ";

        private const string SQL_DELETE = @"
            DELETE [UserGroup]
             WHERE GroupID = @GroupId 
        ";

        private const string SQL_AND_DOCUMENT_ID = "@ and DocumentId = @DocumentId";
        private const string SQL_AND_DRAWING_ID = "@ and DrawingId = @DrawingId";

        #endregion

        #region Methods
        
        public int Create(SqlTransaction tran, int groupId, int? documentId, int? drawingId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@DocumentId", 
                                           (documentId != null) ? (object) documentId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@DrawingId", 
                                           (drawingId != null) ? (object) drawingId : SqlInt32.Null));

            return int.Parse(SQLHelper.ExecuteScalar(
                                 tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        }

        public void Delete(SqlTransaction tran, int groupId, int? documentId, int? drawingId)
        {
            string sqlQuery = SQL_DELETE;

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            
            if (null != documentId)
            {
                sqlQuery += SQL_AND_DOCUMENT_ID;
                paramList.Add(new SqlParameter("@DocumentId", 
                                               (documentId != null) ? (object) documentId : SqlString.Null));
            } else if (null != drawingId)
            {
                sqlQuery += SQL_AND_DRAWING_ID;
                paramList.Add(new SqlParameter("@DrawingId", 
                                               (drawingId != null) ? (object) drawingId : SqlString.Null));
            }

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlQuery, paramList.ToArray());
        }

        #endregion

    }
}
