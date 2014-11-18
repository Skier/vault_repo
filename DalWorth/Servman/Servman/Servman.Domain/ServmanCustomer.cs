using System;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class ServmanCustomer
    {
        public ServmanCustomer()
        {
        }

        #region FindByRealmId

        private const String SqlSelectByRealmId = "SELECT * FROM ServmanCustomer WHERE RealmId = ?RealmId ";

        public static ServmanCustomer FindByRealmId(string realmId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByRealmId, connection))
            {
                Database.PutParameter(dbCommand, "?RealmId", realmId);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region CreateUserDatabase

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
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlCreateUser, null))
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

        public static void CreateUserDatabase(ServmanCustomer user)
        {
            CreateDatabase(user.DbName);
            CreateUser(user.Login, user.Password);
            GrantUser(user.Login, user.DbName);
        }

        #endregion
    }
}
      