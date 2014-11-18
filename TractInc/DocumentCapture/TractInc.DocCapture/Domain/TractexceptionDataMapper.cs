using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class TractexceptionDataMapper
    {

        private const String SqlGetByTractID = @"
    Select TractExceptionsID, TractID, RefName, CalledAC 
      From tractexception 
     Where TractID = @TractID ";

        public List<Tractexception> getByTractID(int tractID)
        {
            List<Tractexception> result = new List<Tractexception>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetByTractID))
                {
                    sqlCommand.Parameters.AddWithValue("@TractID", tractID);

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
      