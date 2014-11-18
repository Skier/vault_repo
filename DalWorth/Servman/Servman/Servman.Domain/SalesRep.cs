using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class SalesRep
    {
        public SalesRep()
        {
        }

        public static string SalesRepRoleName = "SalesRep";

        public User RelatedUser { set; get; }

        private const String SqlSelectByUserId = "Select * From SalesRep Where UserId = ?UserId; ";

        public static SalesRep GetByUserId(int userId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectByCompanyPhoneId = @"
SELECT sr.* From SalesRep sr 
    Inner Join PhoneToSalesRep psr On sr.Id = psr.SalesRepId
 WHERE psr.PhoneId = ?PhoneId; ";

        public static List<SalesRep> GetByCompanyPhoneId(int phoneId, IDbConnection connection)
        {
            var result = new List<SalesRep>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCompanyPhoneId, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneId", phoneId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        private const String SqlSelectByOwnPhoneNumbers = @"
SELECT sr.* From Salesrep sr 
    Inner Join PhoneToSalesRep psr On sr.Id = psr.SalesRepId
    Inner Join phone p On psr.PhoneId = p.Id
WHERE p.Number = ?PhoneNo 
  AND (psr.IsIncoming IS NULL OR psr.IsIncoming = 0); ";

        public static List<SalesRep> GetByOwnPhoneNumbers(string phoneNo, IDbConnection connection)
        {
            var result = new List<SalesRep>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByOwnPhoneNumbers, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneNo", phoneNo);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        private const String SqlSelectByCompanyPhoneNumbers = @"
SELECT sr.* From Salesrep sr 
    Inner Join PhoneToSalesRep psr On sr.Id = psr.SalesRepId
    Inner Join phone p On psr.PhoneId = p.Id
WHERE p.Number = ?PhoneNo 
  AND (psr.IsIncoming = 1); ";

        public static List<SalesRep> GetByCompanyPhoneNumbers(string phoneNo, IDbConnection connection)
        {
            var result = new List<SalesRep>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCompanyPhoneNumbers, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneNo", phoneNo);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

    }
}
      