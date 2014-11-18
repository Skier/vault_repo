using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Area
    {
        public Area(){ }

        #region FindBy ServmanId

        private const string SqlFindByServmanId =
            @"SELECT *
            FROM Area
                WHERE ServmanId = ?ServmanId";

        public static Area FindByServmanId(string servmanId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanId))
            {
                Database.PutParameter(dbCommand, "?ServmanId", servmanId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Area not found");
        }

        #endregion
    }
}
      