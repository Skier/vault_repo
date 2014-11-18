using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class TractTODataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_TRACT_ID = @"
            SELECT TractTextObjectId, TractId, [Text], Easting, Northing, Rotation
              FROM [TT_TractTextObject]
             WHERE TractId = @TractId
        ";

        private const string SQL_DELETE_BY_TRACT_ID = @"
            DELETE [TT_TractTextObject] WHERE TractId = @TractId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_TractTextObject] (TractId, [Text], Easting, Northing, Rotation)
            VALUES ( @TractId, @Text, @Easting, @Northing, @Rotation)

            SELECT scope_identity();
        ";

        #endregion

        #region Methods

        public List<TractTextObjectInfo> GetTextObjects(SqlTransaction tran, int tractId) {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", tractId));
            
            List<TractTextObjectInfo> result = new List<TractTextObjectInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_TRACT_ID, paramList.ToArray() )) {
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

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@TractId", textObjectInfo.TractId));
            paramList.Add(new SqlParameter("@Text", textObjectInfo.Text));
            paramList.Add(new SqlParameter("@Easting", textObjectInfo.Easting));
            paramList.Add(new SqlParameter("@Northing", textObjectInfo.Northing));
            paramList.Add(new SqlParameter("@Rotation", textObjectInfo.Rotation));

            textObjectInfo.TractTextObjectId = int.Parse( 
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
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
