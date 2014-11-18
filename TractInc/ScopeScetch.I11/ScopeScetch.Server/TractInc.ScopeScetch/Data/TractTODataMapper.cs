using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch.Data
{
    internal class TractTODataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_TRACT_ID = @"
            SELECT TractTextObjectId, TractId, [Text], Easting, Northing, Rotation
              FROM [TractTextObjects]
             WHERE TractId = {0}
        ";

        private const string SQL_DELETE_BY_TRACT_ID = @"
            DELETE [TractTextObjects] WHERE TractId = {0}
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TractTextObjects] (TractId, [Text], Easting, Northing, Rotation)
            VALUES ({0}, '{1}', {2}, {3}, {4})

            SELECT scope_identity();
        ";

        private const string SQL_BACKUP = @"
            INSERT INTO [TractTextObjectsBackup] (TractId, [Text], Easting, Northing, Rotation)
                 SELECT {0}, [Text], Easting, Northing, Rotation
                   FROM [TractTextObjects]
                  WHERE TractId = {1}
        ";

        #endregion

        #region Methods

        public List<TractTextObject> GetTextObjects(SqlTransaction tran, int tractId) {
            string sql = String.Format(SQL_SELECT_BY_TRACT_ID, tractId);
            
            List<TractTextObject> result = new List<TractTextObject>();
            
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null )) {
                while (dataReader.Read()) {
                    TractTextObject to = new TractTextObject();
                    
                    to.TractTextObjectId = dataReader.GetInt32(0);
                    to.TractId = dataReader.GetInt32(1);
                    to.Text = dataReader.GetString(2);
                    to.Easting = dataReader.GetSqlDecimal(3).ToDouble();
                    to.Northing = dataReader.GetSqlDecimal(4).ToDouble();
                    to.Rotation = dataReader.GetInt32(5);
                    
                    result.Add(to);
                }
            }
            
            return result;
        }
        
        public void Create(SqlTransaction tran, TractTextObject textObject) {
            CultureInfo intlCI = new CultureInfo( "en-US", false );
            
            string sql = String.Format(SQL_CREATE, 
                   textObject.TractId, textObject.Text, textObject.Easting.ToString(intlCI.NumberFormat), 
                   textObject.Northing.ToString(intlCI.NumberFormat), textObject.Rotation);
            
            textObject.TractTextObjectId = int.Parse( 
                SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());
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
