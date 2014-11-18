using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch.Data
{
    internal class TractDataMapper
    {
        #region Constants

        private const string SQL_SELECT_CHANGED_TRACTS_FOR_USER = @"
            SELECT TractId, Easting, Northing, Description, CreatedBy, IsDeleted, Uid, DocId, CalledAC, UnitId
              FROM [Tract]
             WHERE timestamp > 
	            (SELECT max (SyncTimeStamp) 
                   FROM [SyncLog] WHERE UserId = {0} and deviceId = '{1}' and IsDirty = 0)
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Tract] set 
                Easting = {0},
                Northing = {1},
                Description = '{2}',
                CreatedBy = {3},
                IsDeleted = {4},
                Uid = '{5}', 
                DocId = {6}, 
                CalledAC = {7}, 
                UnitId = {8}
             WHERE TractId = {9}
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [Tract] ( Easting, Northing, Description, CreatedBy, IsDeleted, Uid, DocId, CalledAC, UnitId ) 
                 VALUES ( {0}, {1}, '{2}', {3}, {4}, '{5}', {6}, {7}, {8} )

            select scope_identity();
        ";

        private const string SQL_SELECT_TRACTS_BY_DOCID = @"
            SELECT TractId, Easting, Northing, Description, CreatedBy, IsDeleted, Uid, DocId, CalledAC, UnitId
              FROM [Tract]
             WHERE DocId = {0}
        ";

        private const string SQL_SELECT_TRACT_REFERENCE_NAMES_LIST = @"
            SELECT Uid, Description
              FROM [Tract]
        ";

        private const string SQL_BACKUP = @"
            INSERT INTO [TractBackup] (Easting, Northing, Description, CreatedBy, IsDeleted, Uid, DocId, CalledAC, UnitId) 
                 SELECT Easting, Northing, Description, CreatedBy, IsDeleted, Uid, DocId, CalledAC, UnitId 
                   FROM [Tract]  
                  WHERE TractId = {0}

            select scope_identity();
        ";

        private const string SQL_INSERT_TO_HISTORY_LOG = @"
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

        public List<TractListInfo> GetReferenceNamesList(SqlTransaction tran) {
            List<TractListInfo> result = new List<TractListInfo>();

            string sql = SQL_SELECT_TRACT_REFERENCE_NAMES_LIST;
            
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null )){
                while (dataReader.Read()) {
                    string uid = dataReader.GetGuid(0).ToString();
                    string referenceName = dataReader.GetString(1);

                    result.Add(new TractListInfo(uid, referenceName));
                }
            }
            
            return result;
        }

        public List<Tract> GetModifiedTractsForUser(SqlTransaction tran, int userId, string deviceId, 
                                                    bool getRelevant) 
        {
            string sql = String.Format(SQL_SELECT_CHANGED_TRACTS_FOR_USER, userId, deviceId);
            
            List<Tract> tractList = new List<Tract>();
            
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null )) {
                while (dataReader.Read()) {
                    Tract tract = new Tract();
                    
                    tract.TractId = dataReader.GetInt32(0);
                    tract.Easting = dataReader.GetInt32(1);
                    tract.Northing = dataReader.GetInt32(2);
                    tract.Description = dataReader.GetString(3);
                    tract.CreatedBy = dataReader.GetInt32(4);
                    tract.IsDeleted = dataReader.GetSqlBoolean(5).IsTrue;
                    tract.Uid = dataReader.GetGuid(6).ToString();
                    tract.DocId = dataReader.IsDBNull(7) ? 0 : dataReader.GetInt32(7);
                    tract.CalledAC = dataReader.GetSqlDecimal(8).ToDouble();
                    tract.UnitId = dataReader.GetInt32(9);
                    
                    tractList.Add(tract);
                }
            }

            foreach (Tract tract in tractList) {
                if (tract.DocId > 0) {
                    tract.ParentDocument = DocDM.GetDocumentById(tran, tract.DocId, false);
                }

                if (getRelevant) {
                    tract.Calls = TractCallDM.GetTractCalls(tran, tract.TractId).ToArray();
                    tract.TextObjects = TractTODM.GetTextObjects(tran, tract.TractId).ToArray();
                }
            }
            
            return tractList;
        }

        public List<Tract> SelectTractsByDocId(SqlTransaction tran, int docId,
                                                    bool getRelevant)
        {
            string sql = String.Format(SQL_SELECT_TRACTS_BY_DOCID, docId);

            List<Tract> tractList = new List<Tract>();

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    Tract tract = new Tract();

                    tract.TractId = dataReader.GetInt32(0);
                    tract.Easting = dataReader.GetInt32(1);
                    tract.Northing = dataReader.GetInt32(2);
                    tract.Description = dataReader.GetString(3);
                    tract.CreatedBy = dataReader.GetInt32(4);
                    tract.IsDeleted = dataReader.GetSqlBoolean(5).IsTrue;
                    tract.Uid = dataReader.GetGuid(6).ToString();
                    tract.DocId = dataReader.IsDBNull(7) ? 0 : dataReader.GetInt32(7);
                    tract.CalledAC = dataReader.GetSqlDecimal(8).ToDouble();
                    tract.UnitId = dataReader.GetInt32(9);

                    tractList.Add(tract);
                }
            }

            if (getRelevant)
            {
                foreach (Tract tract in tractList)
                {
                    tract.Calls = TractCallDM.GetTractCalls(tran, tract.TractId).ToArray();
                    tract.TextObjects = TractTODM.GetTextObjects(tran, tract.TractId).ToArray();
                }
            }

            return tractList;
        }

        public void Update(SqlTransaction tran, Tract tract, int userId)
        {
            Backup(tran, tract, userId);
            
            string sql = String.Format(SQL_UPDATE,
                                       tract.Easting, tract.Northing, tract.Description,
                                       tract.CreatedBy, tract.IsDeleted ? 1 : 0, tract.Uid, 
                                       (tract.DocId > 0) ? tract.DocId.ToString() : "null", tract.CalledAC, tract.UnitId,
                                       tract.TractId);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);

            TractCallDM.DeleteByTractId(tran, tract.TractId);
            TractTODM.DeleteByTractId(tran, tract.TractId);
            
            foreach (TractCall call in tract.Calls) {
                call.TractId = tract.TractId;
                TractCallDM.Create(tran, call);
            }
            
            foreach (TractTextObject to in tract.TextObjects) {
                to.TractId = tract.TractId;
                TractTODM.Create(tran, to);
            }
            
            if (null != tract.ParentDocument) {
                if (tract.ParentDocument.DocID == 0) {
                    DocDM.Create(tran, tract.ParentDocument, userId);
                } else {
                    DocDM.Update(tran, tract.ParentDocument, userId);
                }
                
            }
        }

        public void Create(SqlTransaction tran, Tract tract, int userId) {
            
            CultureInfo intlCI = new CultureInfo("en-US", false);

            string sql = String.Format(SQL_CREATE,
                                       tract.Easting.ToString(intlCI), tract.Northing.ToString(intlCI),
                                       tract.Description, tract.CreatedBy, tract.IsDeleted ? 1 : 0, tract.Uid, 
                                       (tract.DocId > 0) ? tract.DocId.ToString() : "null", tract.CalledAC, tract.UnitId);

            tract.TractId = int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            if (null != tract.Calls)
            {
                foreach (TractCall call in tract.Calls)
                {
                    call.TractId = tract.TractId;
                    TractCallDM.Create(tran, call);
                }
            }

            if (null != tract.TextObjects)
            {
                foreach (TractTextObject to in tract.TextObjects)
                {
                    to.TractId = tract.TractId;
                    TractTODM.Create(tran, to);
                }
            }

            StoreHistory(tran, tract.TractId, 0, userId, "CREATE");

        }

        public void Backup(SqlTransaction tran, Tract tract, int userId)
        {
            string sql = String.Format(SQL_BACKUP, 
                                       tract.TractId);

            int backupTractId = int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            TractCallDM.Backup(tran, backupTractId, tract.TractId);
            TractTODM.Backup(tran, backupTractId, tract.TractId);

            StoreHistory(tran, tract.TractId, backupTractId, userId, "MODIFY");
            
        }

        public void StoreHistory(SqlTransaction tran, int tractId, int backupTractId, int userId, string info)
        {
            string sql = String.Format(SQL_GET_CURRENT_VERSION, tractId);

            int currentVersion = int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            currentVersion++;

            sql = String.Format(SQL_INSERT_TO_HISTORY_LOG, tractId, backupTractId, currentVersion, userId, info);

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);

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
