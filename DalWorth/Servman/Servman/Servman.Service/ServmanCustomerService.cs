using System;
using System.Configuration;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Servman.Data;
using Servman.Domain;

namespace Servman.Service
{
    public class ServmanCustomerService
    {
        public static IDbConnection GetConnection(ServmanCustomer servmanCustomer)
        {
            string connString = String.Empty;

            connString += ("server=" + ConfigurationManager.AppSettings["DB_SERVER"] + ";");
            connString += ("user id=" + servmanCustomer.Login + ";");
            connString += ("password=" + servmanCustomer.Password + ";");
            connString += ("database=" + servmanCustomer.DbName + ";");
            connString += "pooling=true;";

            IDbConnection connection = new MySqlConnection(connString);
            connection.Open();
            return connection;
        }

        public static ServmanCustomer CreateServmanCustomer(string realmId, string appDbId)
        {
            var customer = new ServmanCustomer
                               {
                                   RealmId = realmId,
                                   AppDbId = appDbId,
                                   CreationDate = DateTime.Now,
                                   LastLoginDate = DateTime.Now,
                                   DbName = ConfigurationManager.AppSettings["DB_PREFIX"] + realmId,
                                   Login = ConfigurationManager.AppSettings["USER_PREFIX"] + realmId,
                                   Password = Guid.NewGuid().ToString()
                               };
            customer.Name = customer.Email = customer.Phone = customer.Description = String.Empty;

            ServmanCustomer.CreateUserDatabase(customer);

            string dbTemplateFileName = ConfigurationManager.AppSettings["DB_TEMPLATE"];
            BuildDatabase(customer, dbTemplateFileName);

            ServmanCustomer.Insert(customer);

            return customer;
        }

        private static void BuildDatabase(ServmanCustomer servmanCustomer, string dbTemplateFileName)
        {
            using (var reader = new StreamReader(dbTemplateFileName))
            {
                using (var connection = new MySqlConnection(SDK.Configuration.ConnectionString))
                {
                    connection.Open();

                    using (
                        var dbCommand = Database.PrepareCommand(String.Format("USE `{0}`", servmanCustomer.DbName),
                                                                       connection))
                    {
                        dbCommand.ExecuteNonQuery();
                    }

                    try
                    {
                        string statement = "";
                        string line;
                        while (true)
                        {
                            if (statement.EndsWith(";"))
                                statement = "";

                            line = reader.ReadLine();

                            if (line == null)
                                break;

                            line = line.TrimEnd();
                            if (line == "")
                                continue;
                            if (line.StartsWith("--"))
                                continue;

                            statement += line;

                            if (!statement.EndsWith(";"))
                                continue;

                            if (statement.StartsWith("/*!"))
                            {
                                if (statement.EndsWith("*/;"))
                                {
                                    int start = statement.IndexOf(" ");
                                    if (start == -1)
                                        continue;
                                    statement = statement.Substring(start + 1);
                                    statement = statement.Remove(statement.Length - 4) + ";";
                                }
                                else continue;
                            }

                            using (IDbCommand dbCommand = Database.PrepareCommand(statement, connection))
                            {
                                dbCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static ServmanCustomer FindByRealmId(string realmId)
        {
            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return ServmanCustomer.FindByRealmId(realmId, null);
        }
    }
}
