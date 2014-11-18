using System;
using System.Configuration;
using System.Data;
using System.IO;
using Dalworth.Common.Data;
using MySql.Data.MySqlClient;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class CustomerService
    {
        public static IDbConnection GetConnection(Customer customer)
        {
            var connString = String.Empty;

            connString += ("server=" + ConfigurationManager.AppSettings["DB_SERVER"] + ";");
            connString += ("user id=" + customer.DbLogin + ";");
            if (customer.DbPassword.Length == 36)
                connString += ("password=" + customer.DbPassword + ";");
            else
                connString += ("password=" + Cryptographer.Cryptographer.Decrypt(customer.DbPassword) + ";");
            connString += ("database=" + customer.DbName + ";");
            connString += "pooling=true;";

            IDbConnection connection = new MySqlConnection(connString);
            connection.Open();
            
            return connection;
        }

        public static Customer CreateServmanCustomer(string realmId, string appDbId, bool isQbo)
        {
            var customer = new Customer
                               {
                                   RealmId = realmId,
                                   AppDbId = appDbId,
                                   IsQBO = isQbo,
                                   CreationDate = DateTime.Now,
                                   DbName = ConfigurationManager.AppSettings["DB_PREFIX"] + realmId,
                                   DbLogin = ConfigurationManager.AppSettings["USER_PREFIX"] + realmId,
                                   DbPassword = Cryptographer.Cryptographer.Encrypt(Guid.NewGuid().ToString())
                               };
            customer.Name = customer.Email = customer.Phone = customer.Description = String.Empty;

            CreateUserDatabase(customer);

            var dbTemplateFileName = ConfigurationManager.AppSettings["DB_TEMPLATE"];
            BuildDatabase(customer, dbTemplateFileName);

            Customer.Insert(customer);

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

        private const String SqlCreateUser = "CREATE USER ?UserLogin IDENTIFIED BY ?UserPassword; ";
        private static void CreateUser(string login, string password)
        {
            var userExists = false;

            const string sqlSelectUser = @"
SELECT `User` FROM `mysql`.`user` WHERE `User`= ?UserLogin ;
";

            using (var dbCommand = Database.PrepareCommand(sqlSelectUser, null))
            {
                Database.PutParameter(dbCommand, "?UserLogin", login);
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        userExists = true;
                }
            }
            
            const string sqlDropUser = @"
DROP USER ?UserLogin ;
";
            
            if(userExists)
            {
                using (var dbCommand = Database.PrepareCommand(sqlDropUser, null))
                {
                    Database.PutParameter(dbCommand, "?UserLogin", login);
                    dbCommand.ExecuteNonQuery();
                }
            }

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

        public static void CreateUserDatabase(Customer servmanCustomer)
        {
            CreateDatabase(servmanCustomer.DbName);
            CreateUser(servmanCustomer.DbLogin, Cryptographer.Cryptographer.Decrypt(servmanCustomer.DbPassword));
            GrantUser(servmanCustomer.DbLogin, servmanCustomer.DbName);
        }

        private static void BuildDatabase(Customer servmanCustomer, string dbTemplateFileName)
        {
            using (var reader = new StreamReader(dbTemplateFileName))
            {
                using (var connection = new MySqlConnection(Common.SDK.Configuration.ConnectionString))
                {
                    connection.Open();

                    using (var dbCommand = Database.PrepareCommand(String.Format("USE `{0}`", servmanCustomer.DbName), connection))
                    {
                        dbCommand.ExecuteNonQuery();
                    }

                    try
                    {
                        var statement = "";
                        while (true)
                        {
                            if (statement.EndsWith(";"))
                                statement = "";

                            var line = reader.ReadLine();

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

        public static Customer FindByRealmId(string realmId)
        {
            Common.SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return Customer.GetByRealmId(realmId, null);
        }

        public static Customer GetByTicketId(string ticketId)
        {
            Common.SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return Customer.GetByTicketId(ticketId);
        }

        public static QbmsTransaction SaveQbmsTransaction(QbmsTransaction qbmsTransaction)
        {
            Common.SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            return QbmsTransaction.Save(qbmsTransaction);
        }

        public static void SetOAuthConnection(bool isInited)
        {
            var customer = ContextHelper.GetCurrentCustomer();
            customer.IsOAuthInited = isInited;
            Customer.Save(customer);
        }
    }
}
