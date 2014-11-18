using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class ParticipantDataMapper
    {
        private String SqlGetRootParticipant = @"
            Select ParticipantID, DocID, DocRoleID, AsNamed,
                   PhoneHome, PhoneOffice, PhoneCell, PhoneAlt,
                   EntityName, FirstName, MiddleName, LastName, ContactPosition,
                   TAXID, SSN,
                   ParentID, TypeID
              From [Participant] 
             Where DocID = @DocID
               AND DocRoleID = @DocRoleID
               AND ParentID = ParticipantID";

        public Participant getRootParticipant(int docId, int docRoleId)
        {
            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetRootParticipant))
                {

                    sqlCommand.Parameters.AddWithValue("@DocID", docId);
                    sqlCommand.Parameters.AddWithValue("@DocRoleID", docRoleId);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            return doLoad(dataReader);
                        }
                    }
                }
            }

            Participant participant = new Participant();
            participant.DocID = docId;
            participant.DocRoleID = docRoleId;

            participant.ParentID = save(participant).ParticipantID;
            
            return save(participant);
        }

        private String SqlGetDetailParticipants = @"
            Select ParticipantID, DocID, DocRoleID, AsNamed,
                   PhoneHome, PhoneOffice, PhoneCell, PhoneAlt,
                   EntityName, FirstName, MiddleName, LastName, ContactPosition,
                   TAXID, SSN,
                   ParentID, TypeID
              From participant 
             Where DocID = @DocID
               AND DocRoleID = @DocRoleID
               AND ParentID <> ParticipantID";

        public List<Participant> getDetailParticipants(int docId, int docRoleId)
        {
            List<Participant> result = new List<Participant>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetDetailParticipants))
                {

                    sqlCommand.Parameters.AddWithValue("@DocID", docId);
                    sqlCommand.Parameters.AddWithValue("@DocRoleID", docRoleId);

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
      