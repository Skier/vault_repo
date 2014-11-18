using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class BusinessPartner
    {
        public BusinessPartner()
        {

        }

        public static string BusinessPartnerRoleName = "BusinessPartner";

        public User RelatedUser { set; get; }

        private const String SqlSelectByUserId = "Select * From BusinessPartner Where UserId = ?UserId; ";
        
        public static BusinessPartner GetByUserId(int userId, IDbConnection connection)
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

        private const String SqlSelectByPhoneNumbers = @"
Select bp.* From BusinessPartner bp 
    Inner Join PhoneToBusinessPartner pbp On bp.Id = pbp.BusinessPartnerId
    Inner Join phone p On pbp.PhoneId = p.Id
Where p.Number = ?PhoneFrom 
   Or p.Number = ?PhoneTo; ";

        public static List<BusinessPartner> GetByPhoneNumbers(string phoneFrom, string phoneTo, IDbConnection connection)
        {
            var result = new List<BusinessPartner>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPhoneNumbers, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneFrom", phoneFrom);
                Database.PutParameter(dbCommand, "?PhoneTo", phoneTo);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        private const String SqlSelectByCompanyPhoneId = @"
SELECT bp.* From BusinessPartner bp 
    Inner Join PhoneToBusinessPartner pbp On bp.Id = pbp.BusinessPartnerId
 WHERE pbp.PhoneId = ?PhoneId; ";

        public static List<BusinessPartner> GetByCompanyPhoneId(int phoneId, IDbConnection connection)
        {
            var result = new List<BusinessPartner>();
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
SELECT bp.* From BusinessPartner bp 
    Inner Join PhoneToBusinessPartner pbp On bp.Id = pbp.BusinessPartnerId
    Inner Join phone p On pbp.PhoneId = p.Id
WHERE p.Number = ?PhoneNo 
  AND (pbp.IsIncoming IS NULL OR pbp.IsIncoming = 0); ";

        public static List<BusinessPartner> GetByOwnPhoneNumbers(string phoneNo, IDbConnection connection)
        {
            var result = new List<BusinessPartner>();
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
SELECT bp.* From BusinessPartner bp 
    Inner Join PhoneToBusinessPartner pbp On bp.Id = pbp.BusinessPartnerId
    Inner Join phone p On pbp.PhoneId = p.Id
WHERE p.Number = ?PhoneNo 
  AND (pbp.IsIncoming = 1); ";

        public static List<BusinessPartner> GetByCompanyPhoneNumbers(string phoneNo, IDbConnection connection)
        {
            var result = new List<BusinessPartner>();
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
      