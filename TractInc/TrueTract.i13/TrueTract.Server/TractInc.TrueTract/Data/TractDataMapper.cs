using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class TractDataMapper
    {
        #region Constants

        private const string SQL_UPDATE = @"
            UPDATE [Tract] set 
                Easting = @Easting,
                Northing = @Northing,
                RefName = @RefName,
                CreatedBy = @CreatedBy,
                IsDeleted = @IsDeleted,
                DocId = @DocId, 
                CalledAC = @CalledAC, 
                UnitId = @UnitId
             WHERE TractId = @TractId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [Tract] ( Easting, Northing, RefName, CreatedBy, IsDeleted, DocId, CalledAC, UnitId ) 
                 VALUES ( @Easting, @Northing, @RefName, @CreatedBy, @IsDeleted, @DocId, @CalledAC, @UnitId )

            select scope_identity();
        ";

        private const string SQL_SELECT = @"
            SELECT TractId, Easting, Northing, RefName, CreatedBy, IsDeleted, DocId, CalledAC, UnitId
              FROM [Tract]
             WHERE 1 = 1
        ";
        
        private const string SQL_AND_TRACT_ID = " and TractId = @TractId";
        private const string SQL_AND_DOCUMENT_ID = " and DocId = @DocId";
        
        private const string SQL_SELECT_TRACT_LIST = @"
            SELECT [Tract].TractId, [Tract].RefName, [Tract].DocId
              FROM [Tract]
        ";

        private const string SQL_WHERE_DOCUMENT_ID = " WHERE DocId = @DocId";
        private const string SQL_WHERE_DOCUMENT_ID_IS_NULL = " WHERE DocId is Null";
        private const string SQL_JOIN_USER_HISTORY = @" INNER JOIN [UserTractHistory] on [UserTractHistory].TractId = [Tract].TractId
             WHERE [UserTractHistory].UserId = @UserId
        ";

        private const string SQL_INSERT_USER_TRACT_HISTORY = @"
            DELETE FROM [UserTractHistory] where UserId = @UserId and TractId = @TractId

            INSERT INTO [UserTractHistory] (UserId, TractId, AccessDate)
                 VALUES (@UserId, @TractId, GetDate())
        ";

        private const string SQL_BACKUP = @"
            INSERT INTO [TractBackup] (Easting, Northing, RefName, CreatedBy, IsDeleted, DocId, CalledAC, UnitId) 
                 SELECT Easting, Northing, RefName, CreatedBy, IsDeleted, DocId, CalledAC, UnitId 
                   FROM [Tract]  
                  WHERE TractId = @TractId

            select scope_identity();
        ";

        private const string SQL_INSERT_TO_BACKUP_LOG = @"
            INSERT INTO [HistoryLog] ( SourceTableId, BackupTableId, SourceItemId, BackupItemId, ItemVersion, LogDate, UserId, Description) 
                 VALUES ( object_id(N'[Tract]'), object_id(N'[TractBackup]'), @SourceItemId, @BackupItemId, @ItemVersion, getdate(), @UserId, @Description)
        ";

        private const string SQL_GET_CURRENT_VERSION = @"
            SELECT 'currentVersion' = CASE WHEN MAX(ItemVersion) IS NULL THEN 0 ELSE MAX(ItemVersion) END
              FROM [HistoryLog]
             WHERE SourceTableId = object_id(N'[Tract]')
               AND SourceItemId = @SourceItemId
        ";

        #endregion

        #region Fields

        private TractCallDataMapper m_tractCallDM;
        private TractTODataMapper m_tractTODM;
        private DocDataMapper m_docDM;
        
        #endregion

        #region Methods

        public List<TractListInfo> GetDrawingList(SqlTransaction tran) {
            string sqlQuery = SQL_SELECT_TRACT_LIST + SQL_WHERE_DOCUMENT_ID_IS_NULL;
            return SelectTractList(tran, sqlQuery, new List<SqlParameter>());
        }

        public List<TractListInfo> GetTractList(SqlTransaction tran, int docId) {
            string sqlQuery = SQL_SELECT_TRACT_LIST + SQL_WHERE_DOCUMENT_ID;
            
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Add( new SqlParameter("@DocId", docId));
            
            return SelectTractList(tran, sqlQuery, paramList);
        }

        public List<TractListInfo> GetRecentTractList(SqlTransaction tran, int userId) {
            string sqlQuery = SQL_SELECT_TRACT_LIST + SQL_JOIN_USER_HISTORY;
            
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Add( new SqlParameter("@UserId", userId));
            
            return SelectTractList(tran, sqlQuery, paramList);
        }
        
        private List<TractListInfo> SelectTractList(SqlTransaction tran, string sqlQuery, List<SqlParameter> paramList) {
            List<TractListInfo> tractList = new List<TractListInfo>();

            try {
                using (SqlDataReader rdr = (tran == null 
                           ? SQLHelper.ExecuteReader(CommandType.Text, sqlQuery, paramList.ToArray()) 
                           : SQLHelper.ExecuteReader(tran, CommandType.Text, sqlQuery, paramList.ToArray()))) 
                {
                    while (rdr.Read()) {
                        int tractId = rdr.GetInt32(0);
                        string referenceName = rdr.GetString(1);
                        int docId = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2);

                        tractList.Add(new TractListInfo(tractId, referenceName, docId));
                    }
                }
            } catch (SqlException ex) {
                throw new Exception ("Cannot read Tract table", ex);
            }
            
            return tractList;
        }
        
        public TractInfo GetByTractId(SqlTransaction tran, int tractId, bool getRelevant)
        {
            List<TractInfo> tractList = Select(tran, tractId, int.MinValue, getRelevant);
            
            if (tractList.Count == 1) {
                return tractList[0];
            } else if (tractList.Count == 0) {
                return null;
            } else {
                throw new Exception(string.Format("Duplicate Tract found with TractId [{0}]", tractId));
            }
        }
        
        public List<TractInfo> GetByDocId(SqlTransaction tran, int docId, bool getRelevant)
        {
            return Select(tran, int.MinValue, docId, getRelevant);
        }

        private List<TractInfo> Select(SqlTransaction tran, int tractId, int documentId, bool getRelevant) {
            
            string sqlQuery = SQL_SELECT;
            List<SqlParameter> paramList = new List<SqlParameter>();

            if ( tractId != int.MinValue) {
                sqlQuery += SQL_AND_TRACT_ID;
                
                paramList.Add( new SqlParameter("@TractId", tractId));
            }

            if ( documentId != int.MinValue) {
                sqlQuery += SQL_AND_DOCUMENT_ID;
                
                paramList.Add( new SqlParameter("@DocId", documentId));
            }

            List<TractInfo> tractList = new List<TractInfo>();
            
            try {
                using (SqlDataReader rdr = (tran == null 
                           ? SQLHelper.ExecuteReader(CommandType.Text, sqlQuery, paramList.ToArray()) 
                           : SQLHelper.ExecuteReader(tran, CommandType.Text, sqlQuery, paramList.ToArray()))) 
                {
                    while (rdr.Read()) {
                        TractInfo tractInfo = new TractInfo();
                        
                        tractInfo.TractId = rdr.GetInt32(0);
                        tractInfo.Easting = rdr.GetInt32(1);
                        tractInfo.Northing = rdr.GetInt32(2);
                        tractInfo.RefName = rdr.GetString(3);
                        tractInfo.CreatedBy = rdr.GetInt32(4);
                        tractInfo.IsDeleted = rdr.GetSqlBoolean(5).IsTrue;
                        tractInfo.DocId = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6);
                        tractInfo.CalledAC = rdr.GetSqlDecimal(7).ToDouble();
                        tractInfo.UnitId = rdr.GetInt32(8);
                        
                        tractList.Add(tractInfo);
                    }
                }
            } catch (SqlException ex) {
                throw new Exception ("Cannot read Tract table", ex);
            }

            if (getRelevant)
            {
                try {
                    foreach (TractInfo tract in tractList)
                    {
                        tract.Calls = TractCallDM.GetTractCalls(tran, tract.TractId).ToArray();
                        tract.TextObjects = TractTODM.GetTextObjects(tran, tract.TractId).ToArray();
                    }
                } catch (SqlException ex) {
                    throw new Exception ("Cannot read Tract relevant tables", ex);
                }
            }
                
            return tractList;
        }
        
        public void Update(SqlTransaction tran, TractInfo tractInfo, int userId)
        {
            Backup(tran, tractInfo, userId);

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Easting", tractInfo.Easting));
            paramList.Add(new SqlParameter("@Northing", tractInfo.Northing));
            paramList.Add(new SqlParameter("@RefName", tractInfo.RefName));
            paramList.Add(new SqlParameter("@CreatedBy", tractInfo.CreatedBy));
            paramList.Add(new SqlParameter("@IsDeleted", tractInfo.IsDeleted));
            paramList.Add(new SqlParameter("@DocId", (tractInfo.DocId > 0) ? tractInfo.DocId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@CalledAC", tractInfo.CalledAC));
            paramList.Add(new SqlParameter("@UnitId", tractInfo.UnitId));
            paramList.Add(new SqlParameter("@TractId", tractInfo.TractId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            TractCallDM.DeleteByTractId(tran, tractInfo.TractId);
            TractTODM.DeleteByTractId(tran, tractInfo.TractId);
            
            foreach (TractCallInfo call in tractInfo.Calls) {
                call.TractId = tractInfo.TractId;
                TractCallDM.Create(tran, call);
            }
            
            foreach (TractTextObjectInfo to in tractInfo.TextObjects) {
                to.TractId = tractInfo.TractId;
                TractTODM.Create(tran, to);
            }
            
            if (null != tractInfo.ParentDocument) {
                if (tractInfo.ParentDocument.DocID == 0) {
                    DocDM.Create(tran, tractInfo.ParentDocument, userId);
                } else {
                    DocDM.Update(tran, tractInfo.ParentDocument, userId);
                }
            }

            StoreUserTractActivity(tran, userId, tractInfo.TractId);
        }

        public void Create(SqlTransaction tran, TractInfo tractInfo, int userId) {
            
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Easting", tractInfo.Easting));
            paramList.Add(new SqlParameter("@Northing", tractInfo.Northing));
            paramList.Add(new SqlParameter("@RefName", tractInfo.RefName));
            paramList.Add(new SqlParameter("@CreatedBy", tractInfo.CreatedBy));
            paramList.Add(new SqlParameter("@IsDeleted", tractInfo.IsDeleted));
            paramList.Add(new SqlParameter("@DocId", (tractInfo.DocId > 0) ? tractInfo.DocId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@CalledAC", tractInfo.CalledAC));
            paramList.Add(new SqlParameter("@UnitId", tractInfo.UnitId));

            tractInfo.TractId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

            if (null != tractInfo.Calls)
            {
                foreach (TractCallInfo call in tractInfo.Calls)
                {
                    call.TractId = tractInfo.TractId;
                    TractCallDM.Create(tran, call);
                }
            }

            if (null != tractInfo.TextObjects)
            {
                foreach (TractTextObjectInfo to in tractInfo.TextObjects)
                {
                    to.TractId = tractInfo.TractId;
                    TractTODM.Create(tran, to);
                }
            }

            StoreHistory(tran, tractInfo.TractId, 0, userId, "CREATE");
            StoreUserTractActivity(tran, userId, tractInfo.TractId);
        }

        private void StoreUserTractActivity(SqlTransaction tran, int userId, int tractID) {
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@TractId", tractID));

            //store tract userHistory
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT_USER_TRACT_HISTORY, paramList.ToArray());
        }

        private void Backup(SqlTransaction tran, TractInfo tractInfo, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", tractInfo.TractId));

            int backupTractId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_BACKUP, paramList.ToArray()).ToString());

            TractCallDM.Backup(tran, backupTractId, tractInfo.TractId);
            TractTODM.Backup(tran, backupTractId, tractInfo.TractId);

            StoreHistory(tran, tractInfo.TractId, backupTractId, userId, "MODIFY");
            
        }

        private void StoreHistory(SqlTransaction tran, int tractId, int backupTractId, int userId, string info)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@SourceItemId", tractId));

            int currentVersion = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_GET_CURRENT_VERSION, paramList.ToArray()).ToString());

            currentVersion++;

            paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@SourceItemId", tractId));
            paramList.Add(new SqlParameter("@BackupItemId", backupTractId));
            paramList.Add(new SqlParameter("@ItemVersion", currentVersion));
            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@Description", info));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_INSERT_TO_BACKUP_LOG, paramList.ToArray());

        }

        #endregion

        #region Properties

        private DocDataMapper DocDM {
            get {
                if (null == m_docDM) {
                    m_docDM = new DocDataMapper();
                }
                
                return m_docDM;
            }
        }
        
        private TractCallDataMapper TractCallDM {
            get {
                if (null == m_tractCallDM) {
                    m_tractCallDM = new TractCallDataMapper();
                }
                
                return m_tractCallDM;
            }
        }

        private TractTODataMapper TractTODM {
            get {
                if (null == m_tractTODM) {
                    m_tractTODM = new TractTODataMapper();
                }
                
                return m_tractTODM;
            }
        }

        #endregion
    }
}
