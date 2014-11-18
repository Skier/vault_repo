using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Dalworth.LeadCentral.Data;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class ServmanCustomerService
    {
        public static IDbConnection GetConnection(ServmanCustomer servmanCustomer)
        {
            var connString = String.Empty;

            connString += ("server=" + ConfigurationManager.AppSettings["DB_SERVER"] + ";");
            connString += ("user id=" + servmanCustomer.Login + ";");
            if (servmanCustomer.Password.Length == 36)
                connString += ("password=" + servmanCustomer.Password + ";");
            else
                connString += ("password=" + Cryptographer.Cryptographer.Decrypt(servmanCustomer.Password) + ";");
            connString += ("database=" + servmanCustomer.DbName + ";");
            connString += "pooling=true;";

            IDbConnection connection = new MySqlConnection(connString);
            connection.Open();
            
            return connection;
        }

        public static ServmanCustomer CreateServmanCustomer(string realmId, string appDbId, bool isQbo)
        {
            var customer = new ServmanCustomer
                               {
                                   RealmId = realmId,
                                   AppDbId = appDbId,
                                   IsQBO = isQbo,
                                   CreationDate = DateTime.Now,
                                   LastLoginDate = DateTime.Now,
                                   DbName = ConfigurationManager.AppSettings["DB_PREFIX"] + realmId,
                                   Login = ConfigurationManager.AppSettings["USER_PREFIX"] + realmId,
                                   Password = Cryptographer.Cryptographer.Encrypt(Guid.NewGuid().ToString())
                               };
            customer.Name = customer.Email = customer.Phone = customer.Description = String.Empty;

            CreateUserDatabase(customer);

            string dbTemplateFileName = ConfigurationManager.AppSettings["DB_TEMPLATE"];
            BuildDatabase(customer, dbTemplateFileName);

            ServmanCustomer.Insert(customer);

            return customer;
        }

        private const String SqlCreateDatabase = "CREATE DATABASE IF NOT EXISTS `{0}`;";
        private static void CreateDatabase(string dbName)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(String.Format(SqlCreateDatabase, dbName), null))
            {
                dbCommand.ExecuteNonQuery();
            }
        }

        private const String SqlGrantUsage = "GRANT USAGE ON *.* TO ?UserLogin; ";
        private const String SqlDropUser = "DROP USER ?UserLogin; ";
        private const String SqlCreateUser = "CREATE USER ?UserLogin IDENTIFIED BY ?UserPassword; ";
        private static void CreateUser(string login, string password)
        {
/*
            using (var dbCommand = Database.PrepareCommand(SqlGrantUsage, null))
            {
                Database.PutParameter(dbCommand, "?UserLogin", login);
                dbCommand.ExecuteNonQuery();
            }

            using (var dbCommand = Database.PrepareCommand(SqlDropUser, null))
            {
                Database.PutParameter(dbCommand, "?UserLogin", login);
                dbCommand.ExecuteNonQuery();
            }
*/

            using (var dbCommand = Database.PrepareCommand(SqlCreateUser, null))
            {
                Database.PutParameter(dbCommand, "?UserLogin", login);
                Database.PutParameter(dbCommand, "?UserPassword", password);
                dbCommand.ExecuteNonQuery();
            }
        }

        private const String SqlGrantUser = "GRANT ALL ON `{0}`.* TO ?UserLogin; ";
        private static void GrantUser(string login, string dbName)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(String.Format(SqlGrantUser, dbName), null))
            {
                Database.PutParameter(dbCommand, "?UserLogin", login);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void CreateUserDatabase(ServmanCustomer servmanCustomer)
        {
            CreateDatabase(servmanCustomer.DbName);
            CreateUser(servmanCustomer.Login, Cryptographer.Cryptographer.Decrypt(servmanCustomer.Password));
            GrantUser(servmanCustomer.Login, servmanCustomer.DbName);
        }

        private static void BuildDatabase(ServmanCustomer servmanCustomer, string dbTemplateFileName)
        {
            using (var reader = new StreamReader(dbTemplateFileName))
            {
                using (var connection = new MySqlConnection(SDK.Configuration.ConnectionString))
                {
                    connection.Open();

                    using (var dbCommand = Database.PrepareCommand(String.Format("USE `{0}`", servmanCustomer.DbName), connection))
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

        public static ServmanCustomer GetByTicketId(string ticketId)
        {
            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return ServmanCustomer.GetByTicketId(ticketId);
        }

        public static QbmsTransaction SaveQbmsTransaction(QbmsTransaction qbmsTransaction)
        {
            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return QbmsTransaction.Save(qbmsTransaction);
        }

    }
}
