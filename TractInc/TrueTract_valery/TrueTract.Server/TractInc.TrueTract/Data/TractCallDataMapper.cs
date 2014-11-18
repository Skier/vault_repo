using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class TractCallDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_TRACT_ID = @"
            SELECT TractCallId, TractId, Type, Params, [Order], CreatedByMouse
              FROM [TT_TractCall]
             WHERE TractId = @TractId
        ";

        private const string SQL_DELETE_BY_TRACT_ID = @"
            DELETE [TT_TractCall] WHERE TractId = @TractId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_TractCall] (TractId, Type, Params, [Order], CreatedByMouse)
            VALUES (@TractId, @Type, @Params, @Order, @CreatedByMouse)

            SELECT scope_identity();
        ";

        #endregion

        #region Methods

        public List<TractCallInfo> GetTractCalls(SqlTransaction tran, int tractId) {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", tractId));
            
            List<TractCallInfo> result = new List<TractCallInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_TRACT_ID, paramList.ToArray()))
            {
                while (dataReader.Read()) {
                    TractCallInfo call = new TractCallInfo();
                    
                    call.TractCallId = dataReader.GetInt32(0);
                    call.TractId = dataReader.GetInt32(1);
                    call.CallType = dataReader.GetString(2);
                    call.CallDBValue = dataReader.GetString(3);
                    call.CallOrder = dataReader.GetInt32(4);
                    call.CreatedByMouse = dataReader.GetSqlBoolean(5).IsTrue;
                    
                    result.Add(call);
                }
            }
            
            return result;
        }
        
        public void Create(SqlTransaction tran, TractCallInfo call) {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", call.TractId));
            paramList.Add(new SqlParameter("@Type", call.CallType));
            paramList.Add(new SqlParameter("@Params", call.CallDBValue));
            paramList.Add(new SqlParameter("@Order", call.CallOrder));
            paramList.Add(new SqlParameter("@CreatedByMouse", call.CreatedByMouse));
            
            call.TractCallId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        }

        public void DeleteByTractId(SqlTransaction tran, int tractId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", tractId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE_BY_TRACT_ID, paramList.ToArray());
        }

        #endregion
        
    }
}
