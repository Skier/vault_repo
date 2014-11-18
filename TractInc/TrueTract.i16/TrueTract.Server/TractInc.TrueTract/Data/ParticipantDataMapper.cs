using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
             WHERE DocID = {0} ";

        private const string SQL_CREATE = @"
            INSERT INTO [Participant] (
                DocID,
                AsNamed,
                FirstName,
                MiddleName,
                LastName,
                IsSeller
                ) 
            VALUES ( {0}, '{1}', '{2}', '{3}', '{4}', '{5}')

            select scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Participant] set 
                DocID = {0},
                AsNamed = '{1}',
                FirstName = '{2}',
                MiddleName = '{3}',
                LastName = '{4}',
                IsSeller = '{5}'
             WHERE ParticipantID = {6}
        ";
        #endregion

        #region Methods
        
        public List<ParticipantInfo> GetParticipantsByDocId(SqlTransaction tran, int docId)
        {
            List<ParticipantInfo> result = new List<ParticipantInfo>();

            string sql = String.Format(SQL_GET_BY_DOCID, docId);
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null))
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
            
            string sql = String.Format(SQL_CREATE, 
                participantInfo.DocID, participantInfo.AsNamed, participantInfo.FirstName, participantInfo.MiddleName, 
                participantInfo.LastName, participantInfo.IsSeler ? 1 : 0);

            participantInfo.ParticipantID = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return participantInfo;
        }

        public void Update(SqlTransaction tran, ParticipantInfo participantInfo) {
            string sql = String.Format(SQL_UPDATE,
                                       participantInfo.DocID,
                                       participantInfo.AsNamed,
                                       participantInfo.FirstName,
                                       participantInfo.MiddleName,
                                       participantInfo.LastName,
                                       participantInfo.IsSeler ? 1 : 0, participantInfo.ParticipantID);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
        
        #endregion

    }
}
