using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Van
    {
        public Van(){ }

        #region FindAvailableVans

        private const string SqlFindByWorkDate =
            @"select * from Van
	            where ID not in (
		            select VanId from Work
		            where StartDate >= ?StartDateStart
			            and StartDate < ?StartDateEnd
                        and VanId is not null
	                )";


        public static List<Van> FindAvailableVans(DateTime workDate)
        {
            List<Van> vans = new List<Van>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkDate))
            {
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(workDate.Year, workDate.Month, workDate.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(workDate.Year, workDate.Month, workDate.Day, 23, 59, 59, 999));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        vans.Add(Load(dataReader));
                    }
                }
            }
            return vans;
        }

        #endregion        

        #region IsVanAvailable

        private const string SqlIsVanAvailable =
            @"select * from Van
	            where ID not in (
		            select VanId from Work
		            where StartDate >= ?StartDateStart
			            and StartDate < ?StartDateEnd
                        and VanId is not null
	                )
                    and ID = ?VanId
                limit 1";


        public static bool IsVanAvailable(Van van, DateTime workDate)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsVanAvailable))
            {
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(workDate.Year, workDate.Month, workDate.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(workDate.Year, workDate.Month, workDate.Day, 23, 59, 59, 999));
                Database.PutParameter(dbCommand, "?VanId", van.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    return dataReader.Read();
                }
            }            
        }

        #endregion        

        #region FindBy Area

        private const string SqlFindByArea =
            @"SELECT *
            FROM Van
                WHERE AreaId = ?AreaId";

        public static List<Van> FindBy(Area area)
        {
            List<Van> result = new List<Van>();

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

        #region FindBy Servman TruckId

        private const string SqlFindByServmanTruckId =
            @"SELECT *
            FROM Van
                WHERE ServmanTruckId = ?ServmanTruckId";

        public static Van FindByServmanTruckId(string servmanTruckId)
        {            
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanTruckId))
            {
                Database.PutParameter(dbCommand, "?ServmanTruckId", servmanTruckId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Van not found by ServmanTruckId");
        }

        #endregion

        #region FindBy Servman TruckNum

        private const string SqlFindByServmanTruckNum =
            @"SELECT *
            FROM Van
                WHERE ServmanTruckNum = ?ServmanTruckNum";

        public static Van FindByServmanTruckNum(string servmanTruckNum)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanTruckNum))
            {
                Database.PutParameter(dbCommand, "?ServmanTruckNum", servmanTruckNum);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Van not found by ServmanTruckNum");
        }

        #endregion

    }
}
      