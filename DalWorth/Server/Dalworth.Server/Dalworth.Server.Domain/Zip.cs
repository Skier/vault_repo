using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Zip
    {
        public Zip(){ }

        #region FindArea

        private const string SqlArea =
            @"select a.* from Zip z
                inner join Area a on a.ID = z.AreaId
	                where z.ZipCode = ?ZipCode";

        public static Area FindArea(string zip)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlArea))
            {
                Database.PutParameter(dbCommand, "?ZipCode", zip);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Area.Load(dataReader);
                }
            }

            return null;
        }

        #endregion        

        #region FindZip

        private const string SqlFindZip =
            @"select * from Zip
	             where ZipCode = ?ZipCode";

        public static List<Zip> FindZip(string zip)
        {
            List<Zip> result = new List<Zip>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindZip))
            {
                Database.PutParameter(dbCommand, "?ZipCode", zip);

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
      