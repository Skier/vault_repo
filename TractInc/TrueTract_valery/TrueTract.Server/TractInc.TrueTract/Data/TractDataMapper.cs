using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class TractDataMapper
    {
        #region Constants

        private const string SQL_UPDATE = @"
            UPDATE [TT_Tract] set 
                Easting = @Easting,
                Northing = @Northing,
                RefName = @RefName,
                CreatedBy = @CreatedBy,
                DocId = @DocId, 
                CalledAC = @CalledAC, 
                UnitId = @UnitId,
                UniqueId = @UniqueId
             WHERE TractId = @TractId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_Tract] ( Easting, Northing, RefName, CreatedBy, DocId, CalledAC, UnitId, UniqueId ) 
                 VALUES ( @Easting, @Northing, @RefName, @CreatedBy, @DocId, @CalledAC, @UnitId, @UniqueId )

            SELECT scope_identity();
        ";

        private const string SQL_SELECT = @"
            SELECT [TT_Tract].TractId, Easting, Northing, RefName, CreatedBy, DocId, CalledAC, UnitId, UniqueId
              FROM [TT_Tract]
        ";

        private const string SQL_SELECT_BY_DOC_ID = SQL_SELECT + @" 
            WHERE DocId = @DocId";
        
        private const string SQL_SELECT_BY_TRACT_ID = SQL_SELECT + @" 
            WHERE TractId = @TractId";
        
        private const string SQL_SELECT_USER_DRAWINGS = SQL_SELECT + @" 
            WHERE CreatedBy = @CreatedBy and DocId is Null";
        
        private const string SQL_AND_REF_NAME_LIKE = @"
              AND RefName like '%{0}%'
        ";

        private const string SQL_DELETE = @"
            DELETE FROM [TT_Tract] WHERE TractId = @TractId
        ";

        private const string SQL_DELETE_BY_DOC_ID = @"
            DELETE FROM [TT_Tract] WHERE DocId = @DocId
        ";

        #endregion

        #region Fields

        private TractCallDataMapper m_tractCallDM;
        private TractTODataMapper m_tractTODM;
        private DocDataMapper m_docDM;
        
        #endregion

        #region Methods

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

        public List<TractInfo> GetUserDrawings(SqlTransaction tran, int userId, 
                                               TractsFilterInfo filter, bool getRelevant)
        {
            string sqlQuery = SQL_SELECT_USER_DRAWINGS;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);

            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Add( new SqlParameter("CreatedBy", userId));

            return Select(tran, sqlQuery, paramList, getRelevant);
        }
        
        public void Update(SqlTransaction tran, TractInfo tractInfo, int userId)
        {
            string sqlQuery = SQL_UPDATE;

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", tractInfo.TractId));
            paramList.Add(new SqlParameter("@Easting", tractInfo.Easting));
            paramList.Add(new SqlParameter("@Northing", tractInfo.Northing));
            paramList.Add(new SqlParameter("@RefName", tractInfo.RefName));
            paramList.Add(new SqlParameter("@CreatedBy", tractInfo.CreatedBy));
            paramList.Add(new SqlParameter("@DocId", (0 != tractInfo.DocId) ? tractInfo.DocId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@CalledAC", tractInfo.CalledAC));
            paramList.Add(new SqlParameter("@UnitId", tractInfo.UnitId));
            paramList.Add(new SqlParameter("@UniqueId", (null != tractInfo.UniqueId) ? tractInfo.UniqueId : SqlString.Null));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlQuery, paramList.ToArray());

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
        }

        public void Create(SqlTransaction tran, TractInfo tractInfo, int userId) {
            
            string sqlQuery = SQL_CREATE;

            if (tractInfo.UniqueId == null || tractInfo.UniqueId.Length == 0)
                tractInfo.UniqueId = Guid.NewGuid().ToString();
            
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Easting", tractInfo.Easting));
            paramList.Add(new SqlParameter("@Northing", tractInfo.Northing));
            paramList.Add(new SqlParameter("@RefName", tractInfo.RefName));
            paramList.Add(new SqlParameter("@CreatedBy", tractInfo.CreatedBy));
            paramList.Add(new SqlParameter("@DocId", (tractInfo.DocId > 0) ? tractInfo.DocId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@CalledAC", tractInfo.CalledAC));
            paramList.Add(new SqlParameter("@UnitId", tractInfo.UnitId));
            paramList.Add(new SqlParameter("@UniqueId", (null != tractInfo.UniqueId) ? tractInfo.UniqueId : SqlString.Null));

            tractInfo.TractId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sqlQuery, paramList.ToArray()).ToString());

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
        }

        public void Delete(SqlTransaction tran, int tractId)
        {
            string sqlQuery = SQL_DELETE;

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", tractId));
            
            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlQuery, paramList.ToArray());
        }

        public void DeleteByDocId(SqlTransaction tran, int docId)
        {
            string sqlQuery = SQL_DELETE_BY_DOC_ID;

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocId", docId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlQuery, paramList.ToArray());
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
                        tractInfo.DocId = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5);
                        tractInfo.CalledAC = rdr.GetSqlDecimal(6).ToDouble();
                        tractInfo.UnitId = rdr.GetInt32(7);
                        tractInfo.UniqueId = rdr.IsDBNull(8) ? null : rdr.GetString(8);

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

        private string GetFilterQueryString(TractsFilterInfo filter)
        {
            StringBuilder result = new StringBuilder();

            if (filter.refName != null) {
                result.Append(string.Format(SQL_AND_REF_NAME_LIKE, filter.refName));
            }

            return result.ToString();
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
