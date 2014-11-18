using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class ParticipantentitypartyDataMapper
    {

        private const String SqlGetByParticipantId = @"
    Select ParticipantEntityPartyID, ParticipantID, fName, mName, lName, SSN 
      From participantentityparty 
     Where ParticipantID = @ParticipantID";

        public List<Participantentityparty> getByParticipantId(int participantId)
        {
            List<Participantentityparty> result = new List<Participantentityparty>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetByParticipantId))
                {
                    sqlCommand.Parameters.AddWithValue("@ParticipantID", participantId);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(doLoad(dataReader));
                    }
                }
            }

            return result;
        }
    }
}
      