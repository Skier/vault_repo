using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class custmast
    {
        public custmast(){ }

        #region ZipParsed

        public int? ZipParsed
        {
            get
            {
                if (zip == null || zip.Trim() == string.Empty)
                    return null;

                try
                {
                    return int.Parse(zip.Trim());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #endregion

        #region FindLatestCustomerId

        private const string SqlFindLatestCustomerId =
            @"select top 1 * from [custmast] 
                order by Cust_id desc";

        private static string FindLatestCustomerId()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestCustomerId, ConnectionKeyEnum.Servman))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader).cust_id;
                }
            }

            throw new DataNotFoundException("000000");

        }

        #endregion        

        #region FindCustomers

        private const string SqlFindCustomersLatestId =
            @"select top 10000 * from [custmast] 
                    where cust_id > ?
              ORDER BY Cust_id";


        public static List<custmast> FindCustomers(string latestImportedCustomerId)
        {
            List<custmast> result = new List<custmast>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindCustomersLatestId, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@cust_id", latestImportedCustomerId == string.Empty ? "000000" : latestImportedCustomerId);

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

        private const string SqlFindCustomersDateModified =
            @"select * from [custmast] 
                    where (l_contact > ? or l_addr_chg > ?)";


        public static List<custmast> FindCustomers(DateTime lastImportDate)
        {
            List<custmast> result = new List<custmast>();
            
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindCustomersDateModified, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@l_contact", lastImportDate.Date);
                Database.PutParameter(dbCommand, "@l_addr_chg", lastImportDate.Date);

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

        #region FindByPrimaryKey

        private const String SqlFindByPrimaryKey =
            @"Select * from [custmast]
                where cust_id = ?";

        public static custmast FindByPrimaryKey(string cust_id, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPrimaryKey, connection))
            {
                Database.PutParameter(dbCommand, "@cust_id", cust_id);

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
      