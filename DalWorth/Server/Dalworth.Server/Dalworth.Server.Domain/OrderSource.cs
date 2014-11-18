using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class OrderSource
    {
        public OrderSource(){}

        #region FindByTransNum

        private const String SqlFindByAdvertisingSource =
            @"select os.* from OrderSourceAdvertisingSource osas
                inner join OrderSource os on os.ID = osas.OrderSourceId
                where osas.AdvertisingSourceId = ?AdvertisingSourceId";

        public static OrderSource FindByAdSource(int advertisingSourceId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByAdvertisingSource, connection))
            {
                Database.PutParameter(dbCommand, "?AdvertisingSourceId", advertisingSourceId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion
    }
}
      