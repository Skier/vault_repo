using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ParticipantDataMapper
    {

        #region Constants

        private const string SQL_GET_BY_DOCID = @"
            SELECT 
                ParticipantID,
                DocID,
                AsNamed,
                FirstName,
                MiddleName,
                LastName,
                IsSeller
              FROM [Participant]
             WHERE DocID = @DocID ";

        private const string SQL_CREATE = @"
            INSERT INTO [Participant] (
                DocID,
                AsNamed,
                FirstName,
                MiddleName,
                LastName,
                IsSeller
            ) 
            VALUES ( 
                @DocID,
                @AsNamed,
                @FirstName,
                @MiddleName,
                @LastName,
                @IsSeller
            )

            select scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Participant] set 
                DocID = @DocID,
                AsNamed = @AsNamed,
                FirstName = @FirstName,
                MiddleName = @MiddleName,
                LastName = @LastName,
                IsSeller = @IsSeller
             WHERE ParticipantID = @ParticipantID
        ";
        #endregion

        #region Methods
        
        public List<ParticipantInfo> GetParticipantsByDocId(SqlTransaction tran, int docId)
        {
            List<ParticipantInfo> result = new List<ParticipantInfo>();

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", docId));

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_GET_BY_DOCID, paramList.ToArray()))
            {
                while(dataReader.Read())
                {
                    ParticipantInfo participantInfo = new ParticipantInfo();
                    
                    participantInfo.ParticipantID = dataReader.GetInt32(0);
                    participantInfo.DocID = dataReader.GetInt32(1);
                    participantInfo.AsNamed = dataReader.GetString(2);
                    participantInfo.FirstName = dataReader.GetString(3);
                    participantInfo.MiddleName = dataReader.GetString(4);
                    participantInfo.LastName = dataReader.GetString(5);
                    participantInfo.IsSeler = dataReader.GetSqlBoolean(6).IsTrue;

                    result.Add(participantInfo);
                }
            }

            return result;
        }

        public ParticipantInfo Create(SqlTransaction tran, ParticipantInfo participantInfo) {
            
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", participantInfo.DocID));
            paramList.Add(new SqlParameter("@AsNamed", (null != participantInfo.AsNamed) ? participantInfo.AsNamed : SqlString.Null));
            paramList.Add(new SqlParameter("@FirstName", (null != participantInfo.FirstName) ? participantInfo.FirstName : SqlString.Null));
            paramList.Add(new SqlParameter("@MiddleName", (null != participantInfo.MiddleName) ? participantInfo.MiddleName : SqlString.Null));
            paramList.Add(new SqlParameter("@LastName", (null != participantInfo.LastName) ? participantInfo.LastName : SqlString.Null));
            paramList.Add(new SqlParameter("@IsSeller", participantInfo.IsSeler));

            participantInfo.ParticipantID = 
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

            return participantInfo;
        }

        public void Update(SqlTransaction tran, ParticipantInfo participantInfo) {
            
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", participantInfo.DocID));
            paramList.Add(new SqlParameter("@AsNamed", (null != participantInfo.AsNamed) ? participantInfo.AsNamed : SqlString.Null));
            paramList.Add(new SqlParameter("@FirstName", (null != participantInfo.FirstName) ? participantInfo.FirstName : SqlString.Null));
            paramList.Add(new SqlParameter("@MiddleName", (null != participantInfo.MiddleName) ? participantInfo.MiddleName : SqlString.Null));
            paramList.Add(new SqlParameter("@LastName", (null != participantInfo.LastName) ? participantInfo.LastName : SqlString.Null));
            paramList.Add(new SqlParameter("@IsSeller", participantInfo.IsSeler));
            paramList.Add(new SqlParameter("@ParticipantID", participantInfo.ParticipantID));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }
        
        #endregion

    }
}
