
using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class User
    {
        public const string UserRoleAdministrator = "Administrator";
        public const string UserRoleStaff = "Staff";
        public const string UserRoleBusinessPartner = "BusinessPartner";
        public const string UserRoleAccountant = "Accountant";

        public List<ActivityLog> Activities { get; set; }
        public BusinessPartner RelatedBusinessPartner { get; set; }

        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(ScreenName) ? string.Format("{0} {1}", FirstName, LastName) : ScreenName;
            }
        }

        public string PartnerName{
            get
            {
                return RelatedBusinessPartner != null ? RelatedBusinessPartner.PartnerName : string.Empty;
            }
        }

        public string StatusStr
        {
            get
            {
                return IsActive ? "Active" : "Inactive";
            }
        }

        public User()
        {
        }

        private const String SqlSelectByQbUserId = "Select * From User Where QbUserId = ?UserId; ";

        public static User FindByQbUserId(string userId, IDbConnection connection)
        {

            User result = null;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByQbUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        result = Load(dataReader);
                }
            }

            return result;
        }

        private const String SqlSelectActive = @"
SELECT * 
  FROM User 
 WHERE IsActive = 1 
  ORDER BY ScreenName
";

        public static List<User> GetAllActive(IDbConnection connection)
        {
            var result = new List<User>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectActive, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            foreach (var user in result)
            {
                if (user.BusinessPartnerId != null)
                {
                    user.RelatedBusinessPartner = BusinessPartner.FindByPrimaryKey(user.BusinessPartnerId.Value,
                                                                                   connection);
                }
            }

            return result;
        }

        private const String SqlSelectAllUsers = @"
SELECT * 
  FROM User 
  ORDER BY ScreenName
";

        public static List<User> GetAll(IDbConnection connection)
        {
            var result = new List<User>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectAllUsers, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            foreach (var user in result)
            {
                if (user.BusinessPartnerId != null)
                {
                    user.RelatedBusinessPartner = BusinessPartner.FindByPrimaryKey(user.BusinessPartnerId.Value,
                                                                                   connection);
                }
            }

            return result;
        }

        public bool IsBusinessPartner
        {
            get { return QbRoleName == UserRoleEnum.BusinessPartner.ToString(); }
        }

        public bool IsAdmin()
        {
            return QbRoleName == UserRoleEnum.Administrator.ToString();
        }

        public bool IsAccounter()
        {
            return QbRoleName == UserRoleEnum.Accountant.ToString();
        }

        private const String SqlSelectByBusinessPartnerId = @"
        SELECT * 
          FROM User 
         WHERE BusinessPartnerId = ?BusinessPartnerId
           AND QbRoleName = 'BusinessPartner' and isActive
          ORDER BY ScreenName
        ";

        public static List<User> FindActiveByBusinessPartnerId(int id, IDbConnection connection)
        {
            var result = new List<User>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByBusinessPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", id);
                
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        private const string SqlSelectByFilter = @"
SELECT *
  FROM User
 WHERE 1=1
";

        public static List<User> LoadUsers(UserFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectByFilter, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<User>();

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }

                return result;
            }
        }

        private static IDbCommand BuildFilterCommand(string sql, UserFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.PartnerId > 0)
                    Database.PutParameter(dbCommand, "?BusinessPartnerId", filter.PartnerId);
            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, UserFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.PartnerId > 0)
                    result += String.Format(" AND BusinessPartnerId = ?BusinessPartnerId ");

                if (!filter.ShowInactive)
                    result += String.Format(" AND IsActive = 1 ");
            }

            result += " ORDER BY ScreenName ";

            return result;
        }

    }

    public class UserFilter
    {
        public int PartnerId;
        public bool ShowInactive;
    }

    public enum UserRoleEnum
    {
        Administrator,
        Staff,
        BusinessPartner,
        Accountant
    }
}
      