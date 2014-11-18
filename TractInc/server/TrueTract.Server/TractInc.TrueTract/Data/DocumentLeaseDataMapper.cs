using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocumentLeaseDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_DOCUMENT = @"
            SELECT [TT_DocumentLease].*
              FROM [TT_DocumentLease]
             WHERE [DocId] = @DocId        
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_DocumentLease] (
                DocId,
                LCN,
                Term,
                Royalty,
                EffectiveDate,
                Acreage,
                AliasGrantee,
                ExpirationDate,
                HBP)
            VALUES (
                @DocId,
                @LCN,
                @Term,
                @Royalty,
                @EffectiveDate,
                @Acreage,
                @AliasGrantee,
                @ExpirationDate,
                @HBP)

            SELECT scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [TT_DocumentLease] SET 
                LCN = @LCN,
                Term = @Term,
                Royalty = @Royalty,
                EffectiveDate = @EffectiveDate,
                Acreage = @Acreage,
                AliasGrantee = @AliasGrantee,
                ExpirationDate = @ExpirationDate,
                HBP = @HBP
             WHERE DocLeaseId = @DocLeaseId
        ";

        private const string SQL_DELETE = @"
            DELETE [TT_DocumentLease] WHERE DocLeaseId = @DocLeaseId
        ";

        #endregion

        #region Methods

        public DocumentLeaseInfo GetByDocument(SqlTransaction tran, int documentId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocId", documentId));

            List<DocumentLeaseInfo> result = Select(tran, SQL_SELECT_BY_DOCUMENT, paramList);
            if ( 1 == result.Count ) {
                return result[0];
            } else {
                return null;
            }
        }

        private List<DocumentLeaseInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
        {
            List<DocumentLeaseInfo> result = new List<DocumentLeaseInfo>();
            
            using (SqlDataReader dataReader = (null != tran) ?
                SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()) :
                SQLHelper.ExecuteReader(CommandType.Text, sql, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    DocumentLeaseInfo file = new DocumentLeaseInfo();
                    file.DocLeaseId = dataReader.GetInt32(dataReader.GetOrdinal("DocLeaseId"));
                    file.DocId = dataReader.GetInt32(dataReader.GetOrdinal("DocId"));
                    file.LCN = dataReader.IsDBNull(dataReader.GetOrdinal("LCN"))
                               ? null
                               : dataReader.GetString(dataReader.GetOrdinal("LCN"));
                    file.Term = dataReader.IsDBNull(dataReader.GetOrdinal("Term"))
                               ? 0
                               : dataReader.GetInt32(dataReader.GetOrdinal("Term"));
                    file.Royalty = dataReader.IsDBNull(dataReader.GetOrdinal("Royalty"))
                               ? 0.0
                               : dataReader.GetSqlDecimal(dataReader.GetOrdinal("Royalty")).ToDouble();
                    file.EffectiveDate = dataReader.IsDBNull(dataReader.GetOrdinal("EffectiveDate"))
                               ? DateTime.MinValue
                               : dataReader.GetDateTime(dataReader.GetOrdinal("EffectiveDate"));
                    file.Acreage = dataReader.IsDBNull(dataReader.GetOrdinal("Acreage"))
                               ? 0.000
                               : dataReader.GetSqlDecimal(dataReader.GetOrdinal("Acreage")).ToDouble();
                    file.AliasGrantee = dataReader.IsDBNull(dataReader.GetOrdinal("AliasGrantee"))
                               ? null
                               : dataReader.GetString(dataReader.GetOrdinal("AliasGrantee"));
                    file.ExpirationDate = dataReader.IsDBNull(dataReader.GetOrdinal("ExpirationDate"))
                               ? DateTime.MinValue
                               : dataReader.GetDateTime(dataReader.GetOrdinal("ExpirationDate"));
                    file.HBP = dataReader.IsDBNull(dataReader.GetOrdinal("HBP"))
                               ? false
                               : dataReader.GetSqlBoolean(dataReader.GetOrdinal("HBP")).IsTrue;
                    result.Add(file);
                }
            }
            
            return result;
        }

        public DocumentLeaseInfo Create(SqlTransaction tran, DocumentLeaseInfo file)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocId", file.DocId));
            paramList.Add(new SqlParameter("@LCN", null != file.LCN ? file.LCN : SqlString.Null));
            paramList.Add(new SqlParameter("@Term", 0 != file.Term ? file.Term : SqlInt32.Null));
            paramList.Add(new SqlParameter("@Royalty", 0 != file.Royalty ? file.Royalty : SqlDouble.Null));
            paramList.Add(new SqlParameter("@EffectiveDate", DateTime.MinValue != file.EffectiveDate ? file.EffectiveDate : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@Acreage", 0 != file.Acreage ? file.Acreage : SqlDouble.Null));
            paramList.Add(new SqlParameter("@AliasGrantee", null != file.AliasGrantee ? file.AliasGrantee : SqlString.Null));
            paramList.Add(new SqlParameter("@ExpirationDate", DateTime.MinValue != file.ExpirationDate ? file.ExpirationDate : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@HBP", file.HBP));

            file.DocLeaseId = int.Parse(
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
            
            return file;
        }

        public DocumentLeaseInfo Update(SqlTransaction tran, DocumentLeaseInfo file)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@LCN", null != file.LCN ? file.LCN : SqlString.Null));
            paramList.Add(new SqlParameter("@Term", 0 != file.Term ? file.Term : SqlInt32.Null));
            paramList.Add(new SqlParameter("@Royalty", 0 != file.Royalty ? file.Royalty : SqlDouble.Null));
            paramList.Add(new SqlParameter("@EffectiveDate", DateTime.MinValue != file.EffectiveDate ? file.EffectiveDate : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@Acreage", 0 != file.Acreage ? file.Acreage : SqlDouble.Null));
            paramList.Add(new SqlParameter("@AliasGrantee", null != file.AliasGrantee ? file.AliasGrantee : SqlString.Null));
            paramList.Add(new SqlParameter("@ExpirationDate", DateTime.MinValue != file.ExpirationDate ? file.ExpirationDate : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@HBP", file.HBP));

            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            return file;
        }

        public void Delete(SqlTransaction tran, DocumentLeaseInfo file)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocLeaseId", file.DocLeaseId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        #endregion

    }
}
