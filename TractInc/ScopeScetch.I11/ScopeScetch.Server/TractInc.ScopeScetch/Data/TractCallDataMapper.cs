using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch.Data
{
    internal class TractCallDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_TRACT_ID = @"
            SELECT TractCallId, TractId, CallType, CallDBValue, CallOrder, CreatedByMouse
              FROM [TractCalls]
             WHERE TractId = {0}
        ";

        private const string SQL_DELETE_BY_TRACT_ID = @"
            DELETE [TractCalls] WHERE TractId = {0}
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TractCalls] (TractId, CallType, CallDBValue, CallOrder, CreatedByMouse)
            VALUES ({0}, '{1}', '{2}', {3}, {4})

            SELECT scope_identity();
        ";

        private const string SQL_BACKUP = @"
            INSERT INTO [TractCallsBackup] (TractId, CallType, CallDBValue, CallOrder, CreatedByMouse)
                 SELECT {0}, CallType, CallDBValue, CallOrder, CreatedByMouse
                   FROM [TractCalls]
                  WHERE TractId = {1}
        ";

        #endregion

        #region Methods

        public List<TractCall> GetTractCalls(SqlTransaction tran, int tractId) {
            string sql = String.Format(SQL_SELECT_BY_TRACT_ID, tractId);
            
            List<TractCall> result = new List<TractCall>();
            
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null )) {
                while (dataReader.Read()) {
                    TractCall call = new TractCall();
                    
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
        
        public void Create(SqlTransaction tran, TractCall call) {
            string sql = String.Format(SQL_CREATE, 
                call.TractId, call.CallType, call.CallDBValue, call.CallOrder, call.CreatedByMouse ? 1 : 0);
            
            call.TractCallId = int.Parse( SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());
        }

        public void Backup(SqlTransaction tran, int backupTractId, int tractId)
        {
            string sql = String.Format(SQL_BACKUP, backupTractId, tractId);

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        public void DeleteByTractId(SqlTransaction tran, int tractId)
        {
            string sql = String.Format(SQL_DELETE_BY_TRACT_ID, tractId);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        #endregion
        
    }
}
