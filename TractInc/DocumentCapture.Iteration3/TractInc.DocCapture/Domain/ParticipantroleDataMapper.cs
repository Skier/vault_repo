using System;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class ParticipantroleDataMapper
    {

        private String SqlGetRoleByDocType = @"
            Select DocRoleID,RoleName,DocTypeID,IsSeller
              From participantrole
             Where DocTypeID = @DocTypeID
               And IsSeller = @IsSeller ";

        public Participantrole getRoleByDocType(int docTypeId, Boolean isSeller)
        {
            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetRoleByDocType))
                {

                    sqlCommand.Parameters.AddWithValue("@DocTypeID", docTypeId);

                    if (isSeller)
                        sqlCommand.Parameters.AddWithValue("@IsSeller", 1);
                    else
                        sqlCommand.Parameters.AddWithValue("@IsSeller", 0);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            return doLoad(dataReader);
                        }
                        else
                        {
                            Participantrole role = new Participantrole();
                            role.DocTypeID = docTypeId;
                            role.RoleName = isSeller ? "Seller" : "Buyer";
                            role.IsSeller = isSeller;
                            return create(role);
                        }
                    }
                }
            }
        }
    }
}
      