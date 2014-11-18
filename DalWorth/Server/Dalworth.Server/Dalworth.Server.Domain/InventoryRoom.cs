using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class InventoryRoom
    {
        public InventoryRoom(){}

        #region FindBy Area

        private const string SqlFindByArea =
            @"SELECT *
            FROM InventoryRoom
                WHERE AreaId = ?AreaId";

        public static List<InventoryRoom> FindBy(Area area)
        {
            List<InventoryRoom> result = new List<InventoryRoom>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByArea))
            {
                Database.PutParameter(dbCommand, "?AreaId", area.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }
            return result;
        }

        #endregion        
    }
}
      