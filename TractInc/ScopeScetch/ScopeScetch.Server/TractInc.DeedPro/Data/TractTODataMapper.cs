using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TractInc.DeedPro.Entity;

namespace TractInc.DeedPro.Data
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

        public List<TractTextObjectInfo> GetTextObjects(SqlTransaction tran, int tractId) {
            string sql = String.Format(SQL_SELECT_BY_TRACT_ID, tractId);
            
            List<TractTextObjectInfo> result = new List<TractTextObjectInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null )) {
                while (dataReader.Read()) {
                    TractTextObjectInfo to = new TractTextObjectInfo();
                    
                    to.TractTextObjectId = dataReader.GetInt32(0);
                    to.TractId = dataReader.GetInt32(1);
                    to.Text = dataReader.GetString(2);
                    to.Easting = dataReader.GetSqlDecimal(3).ToDouble();
                    to.Northing = dataReader.GetSqlDecimal(4).ToDouble();
                    to.Rotation = dataReader.GetSqlDecimal(5).ToDouble();
                    
                    result.Add(to);
                }
            }
            
            return result;
        }
        
        public void Create(SqlTransaction tran, TractTextObjectInfo textObjectInfo) {
            CultureInfo intlCI = new CultureInfo( "en-US", false );
            
            string sql = String.Format(SQL_CREATE, 
                    textObjectInfo.TractId, textObjectInfo.Text, 
                    textObjectInfo.Easting.ToString(intlCI.NumberFormat), 
                    textObjectInfo.Northing.ToString(intlCI.NumberFormat), 
                    textObjectInfo.Rotation.ToString(intlCI.NumberFormat));
            
            textObjectInfo.TractTextObjectId = int.Parse( 
                SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());
        }

        public void Backup(SqlTransaction tran, int backupTractId, int tractId)
        {
            string sql = String.Format(SQL_BACKUP, backupTractId, tractId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        public void DeleteByTractId(SqlTransaction tran, int tractId)
        {
            string sql = String.Format(SQL_DELETE_BY_TRACT_ID, tractId);
            
            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        #endregion
        
    }
}
