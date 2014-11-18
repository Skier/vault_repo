using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class truck
    {
        public truck(){ }

        #region FindByPrimaryKey

        private const string SqlFindByPrimaryKey =
            @"select * from Truck where truck.truck_id = ?";

        public static truck FindByPrimaryKey(string truckId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPrimaryKey, connection))
            {
                Database.PutParameter(dbCommand, "@truck_id", truckId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }

            }

            throw new DataNotFoundException("truck not found, search by primary key");
        }

        #endregion

        #region FindNewAdSources

        private const string SqlFindNewTrucks =
            @"select * from truck 
                where Truck_id > ?
             order by Truck_id";

        public static List<truck> FindNewTrucks(string lastTruckId)
        {
            List<truck> result = new List<truck>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNewTrucks, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@LastTruckId", lastTruckId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion
    }
}
      