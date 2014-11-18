using System;
using System.Data;
using System.Data.SqlClient;

namespace TractInc.ScopeScetch.Data
{
    internal class SyncLogDataMapper
    {
        #region Constants

        private const string SQL_CREATE_DIRTY_RECORD = @"
            INSERT INTO [SyncLog] (UserId, DeviceId, IsDirty, SyncTimeStamp) 
                           VALUES ({0}, '{1}', {2}, @@DBTS);
        ";

        private const string SQL_CREATE_INIT_RECORD = @"
            INSERT INTO [SyncLog] (UserId, DeviceId, IsDirty, SyncTimeStamp) 
                           VALUES ({0}, '{1}', 0, 0);
        ";
        
        private const string SQL_CLEAR_DIRTY_FLAG = @"
            UPDATE [SyncLog] 
               set IsDirty = 0
             WHERE UserId = {0}
               and DeviceId = '{1}'
               and IsDirty = 1
        ";
        
        private const string SQL_DELETE_DIRTY_SYNC_RECORDS = @"
            DELETE [SyncLog] WHERE UserId = {0} and DeviceId = '{1}' and IsDirty = 1
        ";

        private const string SQL_SELECT_DEVICE_SYNC_RECORDS_COUNT = @"
            SELECT count(*) FROM [SyncLog] WHERE UserId = {0} and DeviceId = '{1}'
        ";

        #endregion

        #region Fields

        private int userId;
        private string deviceId;

        #endregion

        #region Constructors

        public SyncLogDataMapper(int userId, string deviceId) {
            this.userId = userId;
            this.deviceId = deviceId;
        }

        #endregion

        #region Methods

        public void CreateDirtyRecord(SqlTransaction tran) {
            string sql = String.Format(SQL_CREATE_DIRTY_RECORD, userId, deviceId, 1);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        public void CreateInitRecord(SqlTransaction tran) {
            string sql = String.Format(SQL_CREATE_INIT_RECORD, userId, deviceId);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        public void ClearDirtyFlag(SqlTransaction tran) {
            string sql = String.Format(SQL_CLEAR_DIRTY_FLAG, userId, deviceId);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
        
        public void DeleteDirtySyncRecords(SqlTransaction tran) {
            string sql = String.Format(SQL_DELETE_DIRTY_SYNC_RECORDS, userId, deviceId);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
        
        public bool IsInitSyncExists(SqlTransaction tran) {
            string sql = String.Format(SQL_SELECT_DEVICE_SYNC_RECORDS_COUNT, userId, deviceId);

            int recCount = int.Parse(
                SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return recCount > 0;
        }

        #endregion
    }
}
