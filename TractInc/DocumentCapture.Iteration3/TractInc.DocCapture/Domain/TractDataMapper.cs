using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class TractDataMapper
    {

        private const String SqlGetByDocID = @"
    Select TractID, DocID, RefName, CalledAC, ScopePlotUrl 
      From tract 
     Where DocID = @DocID";

        public List<Tract> getByDocID(int docID)
        {
            List<Tract> result = new List<Tract>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetByDocID))
                {
                    sqlCommand.Parameters.AddWithValue("@DocID", docID);

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
