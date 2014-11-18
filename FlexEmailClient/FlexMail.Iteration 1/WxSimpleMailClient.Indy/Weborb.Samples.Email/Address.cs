using System;
using System.Collections;
using System.Data;
using Weborb.Samples.Email.Entities;

namespace Weborb.Samples.Email
{
    public class Address
    {
        static public int Create(AddressInfo addressInfo)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                // Check for the addressInfo existing.
                string sql = string.Format(DataAccessBrigde.SQL_SELECT_ADDRESS_BY_ACCOUNTID_AND_EMAIL, 
                                           addressInfo.AccountId, addressInfo.Email);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql))
                    if (rdr.Read())
                        return rdr.GetInt32(0);

                // Insert new addressInfo to the db.
                sql = string.Format(DataAccessBrigde.SQL_INSERT_ADDRESS, 
                                    addressInfo.AccountId, addressInfo.Email);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);

                // Get addressInfo primary key.
                int id;
                sql = string.Format(DataAccessBrigde.SQL_SELECT_ADDRESS_BY_ACCOUNTID_AND_EMAIL, 
                                    addressInfo.AccountId, addressInfo.Email);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(connection, CommandType.Text, sql))
                    if (rdr.Read())
                        id = rdr.GetInt32(0);
                    else
                        throw new Exception("Reading address id failed.");

                return id;
            }
        }

        static public AddressInfo RetreiveById(int addressId)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_SELECT_ADDRESS_BY_ID, addressId);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql))
                    if (rdr.Read())
                        return new AddressInfo(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetString(2));

                throw new Exception("Address not found.");
            }
        }

        static public AddressInfo[] RetreiveAllByAccountId(int accountId)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                ArrayList container = new ArrayList();

                // Retreive addresses from db.
                string sql = string.Format(DataAccessBrigde.SQL_SELECT_ALL_ADDRESSES_BY_ACCOUNTID, 
                                           accountId);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql))
                    while (rdr.Read())
                        container.Add(new AddressInfo(rdr.GetInt32(0), rdr.GetInt32(1), 
                                                      rdr.GetString(2)));

                // Cast result.
                AddressInfo[] addresses = new AddressInfo[container.Count];
                container.CopyTo(addresses);

                return addresses;
            }
        }

        static public void Delete(int addressId)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_DELETE_ADDRESS, addressId);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }
    }
}
