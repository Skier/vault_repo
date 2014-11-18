using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class TractDataMapper
    {
        #region Constants

        private const string SQL_UPDATE = @"
            UPDATE [Tract] set 
                Easting = {0},
                Northing = {1},
                RefName = '{2}',
                CreatedBy = {3},
                IsDeleted = {4},
                DocId = {5}, 
                CalledAC = {6}, 
                UnitId = {7}
             WHERE TractId = {8}
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [Tract] ( Easting, Northing, RefName, CreatedBy, IsDeleted, DocId, CalledAC, UnitId ) 
                 VALUES ( {0}, {1}, '{2}', {3}, {4}, {5}, {6}, {7} )

            select scope_identity();
        ";

        private const string SQL_SELECT = @"
            SELECT [Tract].TractId, Easting, Northing, RefName, CreatedBy, IsDeleted, DocId, CalledAC, UnitId
              FROM [Tract]
        ";

        private const string SQL_SELECT_BY_DOC_ID = SQL_SELECT + " WHERE DocId = @DocId";
        
        private const string SQL_SELECT_BY_TRACT_ID = SQL_SELECT + " WHERE TractId = @TractId";
        
        private const string SQL_SELECT_BY_GROUP_AND_USER = SQL_SELECT + @"
            INNER JOIN [GroupItems] on [GroupItems].DrawingId = [Tract].TractId
            INNER JOIN [GroupUsers] on [GroupUsers].GroupId = [GroupItems].GroupId
         where [GroupUsers].GroupId = @GroupId
           and [GroupUsers].UserId = @UserId
        ";

        private const string SQL_SELECT_USER_DRAWINGS = 
            SQL_SELECT + " WHERE CreatedBy = @CreatedBy and DocId is Null";

        private const string SQL_SELECT_USER_DRAWINGS_BY_REF_NAME = 
            SQL_SELECT + " WHERE CreatedBy = @CreatedBy and RefName like @RefName";
        
        private const string SQL_SELECT_USER_RECENT_DRAWINGS = SQL_SELECT + @"
                INNER JOIN [UserTractHistory] on [UserTractHistory].TractId = [Tract].TractId
             WHERE [UserTractHistory].UserId = @UserId
               and DocId is Null
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
                  WHERE TractId = {0}

            select scope_identity();
        ";

        private const string SQL_INSERT_TO_BACKUP_LOG = @"
            INSERT INTO [HistoryLog] ( SourceTableId, BackupTableId, SourceItemId, BackupItemId, ItemVersion, LogDate, UserId, Description) 
                 VALUES ( object_id(N'[Tract]'), object_id(N'[TractBackup]'), {0}, {1}, {2}, getdate(), {3}, '{4}')
        ";

        private const string SQL_GET_CURRENT_VERSION = @"
            SELECT 'currentVersion' = CASE WHEN MAX(ItemVersion) IS NULL THEN 0 ELSE MAX(ItemVersion) END
              FROM [HistoryLog]
             WHERE SourceTableId = object_id(N'[Tract]')
               AND SourceItemId = {0}
        ";

        #endregion

        #region Fields

        private TractCallDataMapper m_tractCallDM;
        private TractTODataMapper m_tractTODM;
        private DocDataMapper m_docDM;
        
        #endregion

        #region Methods

        public List<TractInfo> GetUserDrawings(SqlTransaction tran, int userId, bool getRelevant)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Add( new SqlParameter("CreatedBy", userId));
            
            return Select(tran, SQL_SELECT_USER_DRAWINGS, paramList, getRelevant);
        }

        public List<TractInfo> GetUserDrawingsByRefName(SqlTransaction tran, int userId, string refName, bool getRelevant)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add( new SqlParameter("CreatedBy", userId));
            paramList.Add( new SqlParameter("RefName", "%" + refName + "%"));

            return Select(tran, SQL_SELECT_USER_DRAWINGS_BY_REF_NAME, paramList, getRelevant);
        }

        public List<TractInfo> GetUserRecentDrawings(SqlTransaction tran, int userId, bool getRelevant) 
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Add( new SqlParameter("UserId", userId));
            
            return Select(tran, SQL_SELECT_USER_RECENT_DRAWINGS, paramList, getRelevant);
        }

        public List<TractInfo> GetTractsByDocId(SqlTransaction tran, int docId, bool getRelevant)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add( new SqlParameter("DocId", docId));

            return Select(tran, SQL_SELECT_BY_DOC_ID, paramList, getRelevant);
        }

        public TractInfo GetByTractId(SqlTransaction tran, int tractId, bool getRelevant)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add( new SqlParameter("TractId", tractId));

            List<TractInfo> tractList = Select(tran, SQL_SELECT_BY_TRACT_ID, paramList, getRelevant);

            if (tractList.Count == 1) {
                return tractList[0];
            } else if (tractList.Count == 0) {
                return null;
            } else {
                throw new Exception(string.Format("Duplicate Tract found with TractId [{0}]", tractId));
            }
        }
        
        public List<TractInfo> GetByGroupAndUser(SqlTransaction tran, int groupId, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@UserId", userId));

            return Select(tran, SQL_SELECT_BY_GROUP_AND_USER, paramList, false);
        }

        public void Update(SqlTransaction tran, TractInfo tractInfo, int userId)
        {
            Backup(tran, tractInfo, userId);
            
            string sql = String.Format(SQL_UPDATE,
                                       tractInfo.Easting, tractInfo.Northing, tractInfo.RefName,
                                       tractInfo.CreatedBy, tractInfo.IsDeleted ? 1 : 0,
                                       (tractInfo.DocId > 0) ? tractInfo.DocId.ToString() : "null", tractInfo.CalledAC, tractInfo.UnitId,
                                       tractInfo.TractId);
            
            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);

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
                    tractInfo.ParentDocument.CreatedBy = userId;
                    DocDM.Create(tran, tractInfo.ParentDocument);
                } else {
                    DocDM.Update(tran, tractInfo.ParentDocument, userId);
                }
            }

            StoreUserTractActivity(tran, userId, tractInfo.TractId);
        }

        public void Create(SqlTransaction tran, TractInfo tractInfo, int userId) {
            
            CultureInfo intlCI = new CultureInfo("en-US", false);

            string sql = String.Format(SQL_CREATE,
                                       tractInfo.Easting.ToString(intlCI), tractInfo.Northing.ToString(intlCI),
                                       tractInfo.RefName, tractInfo.CreatedBy, tractInfo.IsDeleted ? 1 : 0,
                                       (tractInfo.DocId > 0) ? tractInfo.DocId.ToString() : "null", tractInfo.CalledAC, tractInfo.UnitId);

            tractInfo.TractId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

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

        private List<TractInfo> Select(SqlTransaction tran, string sqlQuery, List<SqlParameter> paramList, 
                                       bool getRelevant) {
            
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
                        tract.Calls = TractCallDM.GetTractCalls(tran, tract.TractId);
                        tract.TextObjects = TractTODM.GetTextObjects(tran, tract.TractId);
                    }
                } catch (SqlException ex) {
                    throw new Exception ("Cannot read Tract relevant tables", ex);
                }
            }
                
            return tractList;
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
            string sql = String.Format(SQL_BACKUP, 
                                       tractInfo.TractId);

            int backupTractId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            TractCallDM.Backup(tran, backupTractId, tractInfo.TractId);
            TractTODM.Backup(tran, backupTractId, tractInfo.TractId);

            StoreHistory(tran, tractInfo.TractId, backupTractId, userId, "MODIFY");
            
        }

        private void StoreHistory(SqlTransaction tran, int tractId, int backupTractId, int userId, string info)
        {
            string sql = String.Format(SQL_GET_CURRENT_VERSION, tractId);

            int currentVersion = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            currentVersion++;

            sql = String.Format(SQL_INSERT_TO_BACKUP_LOG, tractId, backupTractId, currentVersion, userId, info);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);

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
