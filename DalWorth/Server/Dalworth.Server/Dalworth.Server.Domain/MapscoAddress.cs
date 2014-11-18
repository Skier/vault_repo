using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class MapscoAddress
    {
        public MapscoAddress() { }

        #region StreetFull

        public string StreetFull
        {
            get
            {
                return Utils.JoinStrings(" ", Prefix, Street, Suffix);
            }
        }

        #endregion

        #region MapscoFull

        public string MapscoFull
        {
            get
            {
                return MapPage + MapLetter;
            }
        }

        #endregion


        #region AssignMapsco

        private const string SqlAssignMapsco =
            @"SELECT *
            FROM MapscoAddress
                WHERE 
                    Street = ?Street
                    and City = ?City
                    and State = ?State
                    and Zip = ?Zip
                    and BlockBegin <= ?Block and BlockEnd >= ?Block";

        public static void AssignMapsco(Address address)
        {
            if (address == null)
                return;

            if (address.Street == null || address.Street == string.Empty
                || address.City == null || address.City == string.Empty
                || address.State == null || address.State == string.Empty
                || address.Zip == null
                || address.Block == null || address.Block == string.Empty)
            {
                address.MapLetter = string.Empty;
                address.MapPage = string.Empty;
                return;
            }

            int block;
            if (!int.TryParse(address.Block, out block))
            {
                address.MapLetter = string.Empty;
                address.MapPage = string.Empty;
                return;
            }

            List<MapscoAddress> mapscos = new List<MapscoAddress>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlAssignMapsco))
            {
                Database.PutParameter(dbCommand, "?Street", address.Street);
                Database.PutParameter(dbCommand, "?City", address.City);
                Database.PutParameter(dbCommand, "?State", address.State);
                Database.PutParameter(dbCommand, "?Zip", address.Zip);
                Database.PutParameter(dbCommand, "?Block", block);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        mapscos.Add(Load(dataReader));
                }
            }


            if (mapscos.Count == 0)
            {
                address.MapLetter = string.Empty;
                address.MapPage = string.Empty;
                return;
            } 

            if (mapscos.Count == 1)
            {
                address.MapLetter = mapscos[0].MapLetter;
                address.MapPage = mapscos[0].MapPage;
                return;
            } 


            foreach (MapscoAddress mapsco in mapscos)
            {
                if (mapsco.Prefix.ToLower() == address.Prefix.ToLower() 
                    && mapsco.Suffix.ToLower() == address.Suffix.ToLower())
                {
                    address.MapLetter = mapsco.MapLetter;
                    address.MapPage = mapsco.MapPage;
                    return;
                }
            }

            foreach (MapscoAddress mapsco in mapscos)
            {
                if (mapsco.Suffix.ToLower() == address.Suffix.ToLower())
                {
                    address.MapLetter = mapsco.MapLetter;
                    address.MapPage = mapsco.MapPage;
                    return;
                }
            }

            foreach (MapscoAddress mapsco in mapscos)
            {
                if (mapsco.Prefix.ToLower() == address.Prefix.ToLower())
                {
                    address.MapLetter = mapsco.MapLetter;
                    address.MapPage = mapsco.MapPage;
                    return;
                }
            }

            address.MapLetter = mapscos[0].MapLetter;
            address.MapPage = mapscos[0].MapPage;
            return;
        }

        #endregion        

        #region FindCities

        private const string SqlFindCities =
            @"select City, count(*) as qty from MapscoAddress
                where zip = ?zip
                group by City
                order by qty desc";

        public static List<string> FindCities(string zip)
        {
            List<string> result = new List<string>();

            List<Domain.Zip> zips = Domain.Zip.FindZip(zip);
            foreach (Zip foundedZip in zips)
            {
                result.Add(foundedZip.City);
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindCities))
            {
                Database.PutParameter(dbCommand, "?zip", zip);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetString(0));
                }
            }

            return result;
        }

        #endregion        

        #region FindPossibleMapsco

        private const string SqlFindPossibleMapscoIgnoreBlock =
            @"select * from MapscoAddress
                where Street like ?Street
                    and City = ?City
                    and Zip = ?Zip
                order by Street
                limit 100";

        private const string SqlFindPossibleMapscoIgnoreZip =
            @"(select * from MapscoAddress
                where Street like ?Street
                    and City = ?City
                    and BlockBegin <= ?Block and BlockEnd >= ?Block
                order by Street
                limit 100)
                
              union

              (select * from MapscoAddress
                where Street like ?Street
                    and City = ?City                    
                order by Street, BlockBegin, BlockEnd
                limit 100)";


        public static List<MapscoAddress> FindPossibleMapsco(Address address)
        {
            List<MapscoAddress> result = new List<MapscoAddress>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPossibleMapscoIgnoreBlock))
            {
                Database.PutParameter(dbCommand, "?Street", address.Street + "%");
                Database.PutParameter(dbCommand, "?City", address.City);
                Database.PutParameter(dbCommand, "?Zip", address.Zip);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            if (result.Count > 0)
                return result;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPossibleMapscoIgnoreZip))
            {
                Database.PutParameter(dbCommand, "?Street", address.Street + "%");
                Database.PutParameter(dbCommand, "?City", address.City);
                Database.PutParameter(dbCommand, "?Block", address.Block);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }


            return result;
        }

        #endregion        

        #region FindPossibleMapscoByStreet

        private const string SqlFindPossibleMapscoByStreet =
            @"(select * from MapscoAddress
                where Street like ?Street
                    and City = ?City
                    and Zip = ?Zip
                    and BlockBegin <= ?Block and BlockEnd >= ?Block
                order by Street
                limit 100)
                
              union

              (select * from MapscoAddress
                where Street like ?Street
                    and City = ?City
                    and Zip = ?Zip                   
                order by Street, BlockBegin, BlockEnd
                limit 100)

              union

             (select * from MapscoAddress
                where Street like ?Street
                    and City = ?City
                    and Zip <> ?Zip
                    and BlockBegin <= ?Block and BlockEnd >= ?Block
                order by Street
                limit 100)

              union
                
              (select * from MapscoAddress
                where Street like ?Street
                    and City = ?City
                    and Zip <> ?Zip                   
                order by Street, BlockBegin, BlockEnd
                limit 100)";

        public static List<MapscoAddress> FindPossibleMapscoByStreet(Address address)
        {
            List<MapscoAddress> result = new List<MapscoAddress>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPossibleMapscoByStreet))
            {
                Database.PutParameter(dbCommand, "?Street", address.Street + "%");
                Database.PutParameter(dbCommand, "?City", address.City);
                Database.PutParameter(dbCommand, "?Zip", address.Zip);
                Database.PutParameter(dbCommand, "?Block", address.Block);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion        

        #region FindZip

        private const string SqlFindZip =
            @"select * from MapscoAddress
                where Street = ?Street
                    and City = ?City
                    and State = ?State
                    and BlockBegin <= ?Block and BlockEnd >= ?Block";

        public static string FindZip(Address address)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindZip))
            {
                Database.PutParameter(dbCommand, "?Street", address.Street.Trim());
                Database.PutParameter(dbCommand, "?City", address.City.Trim());
                Database.PutParameter(dbCommand, "?State", address.State);
                Database.PutParameter(dbCommand, "?Block", address.Block.Trim());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader).Zip;
                }
            }

            return string.Empty;
        }

        #endregion        
    }
}
      