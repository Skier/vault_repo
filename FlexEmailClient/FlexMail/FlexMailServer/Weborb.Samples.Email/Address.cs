using System.Collections;
using System.Data;
using Weborb.Samples.Email.Entities;

namespace Weborb.Samples.Email
{
    public class Address
    {

        public static void Create (int accountId, EmailAddressInfo[] addressList) {
            Hashtable contactsMap = new Hashtable();
            
            foreach (EmailAddressInfo addressInfo in addressList) {
                contactsMap[addressInfo.DisplayValue] = null;
            }
            
            string[] addresses = new string[contactsMap.Count];
            contactsMap.Keys.CopyTo(addresses, 0);
            
            Create(accountId, addresses);
        }
        
        public static void Create (int accountId, string[] adressesList) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();

                foreach (string address in adressesList) {

                    // Check for the address existing.
                    string sql = string.Format(DataAccessBrigde.SQL_SELECT_ADDRESS_BY_ACCOUNTID_AND_EMAIL,
                                               accountId, address);
                    using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                        connection, CommandType.Text, sql)) {
                        if (rdr.Read()) {
                            continue;
                        }
                    }
                    
                    // Insert new addressInfo to the db.
                    sql = string.Format(DataAccessBrigde.SQL_INSERT_ADDRESS, accountId, address);
                    DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
                }
            }
        }
        
        public static AddressInfo[] RetreiveAllByAccountId(int accountId) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                ArrayList container = new ArrayList();

                string sql = string.Format(DataAccessBrigde.SQL_SELECT_ALL_ADDRESSES_BY_ACCOUNTID,
                                           accountId);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql)) {
                    while (rdr.Read()) {
                        container.Add(new AddressInfo(rdr.GetInt32(0), rdr.GetInt32(1),
                                                      rdr.GetString(2)));
                    }
                }

                AddressInfo[] addresses = new AddressInfo[container.Count];
                container.CopyTo(addresses);

                return addresses;
            }
        }

        public static void Delete(int addressId) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_DELETE_ADDRESS, addressId);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }
    }
}