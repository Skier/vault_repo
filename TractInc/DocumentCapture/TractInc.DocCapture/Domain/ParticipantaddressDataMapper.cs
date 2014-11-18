using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class ParticipantaddressDataMapper
    {
        
        private const String SqlGetByParticipantId = @"
    Select AddressID, ParticipantlID, AddressTypeID, Line1, Line2, City, State, Zip, Incareof 
      From participantaddress 
     Where ParticipantlID = @ParticipantlID ";

        public List<Participantaddress> getByParticipantId(int participantId)
        {
            List<Participantaddress> result = new List<Participantaddress>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetByParticipantId))
                {
                    sqlCommand.Parameters.AddWithValue("@ParticipantlID", participantId);

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
      