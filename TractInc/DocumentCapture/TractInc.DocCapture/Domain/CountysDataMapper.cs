
        using System;
        using System.Collections.Generic;
        using System.Data;
        using System.Data.SqlClient;
        using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{
    public partial class CountysDataMapper
    {
        private const String SqlGetByStateId = @"Select
    OBJECTID,STATE_ID,NAME
    From [Countys] 
    Where STATE_ID = @StateId ";

        public List<Countys> getByStateId(int stateId)
        {
            List<Countys> result = new List<Countys>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetByStateId))
                {
                    sqlCommand.Parameters.AddWithValue("@StateId", stateId);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Countys county = new Countys();
                            county.OBJECTID = dataReader.GetInt32(0);
                            county.STATE_ID = dataReader.GetInt32(1);

                            if (!dataReader.IsDBNull(2))
                                county.NAME = dataReader.GetString(2);

                            result.Add(county);
                        }
                    }
                }
            }

            return result;
        }
    }
}
      