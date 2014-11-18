using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch.Data
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
        
        public List<Participant> GetParticipantsByDocId(SqlTransaction tran, int docId)
        {
            List<Participant> result = new List<Participant>();

            string sql = String.Format(SQL_GET_BY_DOCID, docId);
            
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                while(dataReader.Read())
                {
                    Participant participant = new Participant();
                    
                    participant.ParticipantID = dataReader.GetInt32(0);
                    participant.DocID = dataReader.GetInt32(1);
                    participant.AsNamed = dataReader.GetString(2);
                    participant.FirstName = dataReader.GetString(3);
                    participant.MiddleName = dataReader.GetString(4);
                    participant.LastName = dataReader.GetString(5);
                    participant.IsSeler = dataReader.GetSqlBoolean(6).IsTrue;

                    result.Add(participant);
                }
            }

            return result;
        }

        public Participant Create(SqlTransaction tran, Participant participant) {
            
            string sql = String.Format(SQL_CREATE, 
                participant.DocID, participant.AsNamed, participant.FirstName, participant.MiddleName, 
                participant.LastName, participant.IsSeler ? 1 : 0);

            participant.ParticipantID = int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return participant;
        }

        public void Update(SqlTransaction tran, Participant participant) {
            string sql = String.Format(SQL_UPDATE,
                                       participant.DocID,
                                       participant.AsNamed,
                                       participant.FirstName,
                                       participant.MiddleName,
                                       participant.LastName,
                                       participant.IsSeler ? 1 : 0, participant.ParticipantID);

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
        
        #endregion

    }
}
