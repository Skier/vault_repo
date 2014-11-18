using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Mapsco
    {
        public Mapsco(){ }

        #region FindMapsco

        private const string SqlFindMapsco =
            @"select * from mapsco
                where UpperLeftLatitude >= ?Latitude and UpperLeftLongitude <= ?Longitude
                  and LowerRightLatitude <= ?Latitude and LowerRightLongitude >= ?Longitude
                limit 1;";


        public static Mapsco FindMapsco(float latitude, float longitude)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindMapsco))
            {
                Database.PutParameter(dbCommand, "?Latitude", latitude);
                Database.PutParameter(dbCommand, "?Longitude", longitude);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        return Load(dataReader);
                    }
                }
            }
            throw new DataNotFoundException("Mapsco not found");
        }

        #endregion
    }
}
      