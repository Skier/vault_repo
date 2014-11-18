using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class ParticipantreservationDataMapper
    {

        private const String SqlGetByParticipantId = @"
    Select DocReservationID, ParticipantID, Details
      From participantreservation
     Where ParticipantID = @ParticipantID ";

        public List<Participantreservation> getByParticipantId(int participantID)
        {
            List<Participantreservation> result = new List<Participantreservation>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetByParticipantId))
                {
                    sqlCommand.Parameters.AddWithValue("@ParticipantID", participantID);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            result.Add(doLoad(dataReader));
                        }
                    }
                }
            }

            return result;
        }
        
    }
}
      