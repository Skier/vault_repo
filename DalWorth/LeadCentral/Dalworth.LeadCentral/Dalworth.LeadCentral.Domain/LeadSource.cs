using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class LeadSource
    {
        public User RelatedUser { get; set; }

        public LeadSource()
        {
        }

        private const String SqlSelectByUserId = "Select * From LeadSource Where UserId = ?UserId; ";

        public static LeadSource GetByUserId(int userId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectByTrackingPhoneId = @"
SELECT ls.* From LeadSource ls 
    Inner Join LeadSourceTrackingPhone lsp On ls.Id = lsp.LeadSourceId
 WHERE lsp.TrackingPhoneId = ?TrackingPhoneId; ";

        public static List<LeadSource> GetByTrackingPhoneId(int trackingPhoneId, IDbConnection connection)
        {
            var result = new List<LeadSource>();
            using (var dbCommand = Database.PrepareCommand(SqlSelectByTrackingPhoneId, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", trackingPhoneId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        private const String SqlSelectBySimplePhoneNumber = @"
SELECT source.* From LeadSource source 
    Inner Join LeadSourcePhone phone On source.Id = phone.LeadSourceId
 WHERE phone.SimplePhoneNumber = ?SimplePhoneNumber
   AND phone.IsRemoved = 0 ; ";

        public static List<LeadSource> GetBySimplePhoneNumber(string simplePhoneNumber, IDbConnection connection)
        {
            var result = new List<LeadSource>();
            using (var dbCommand = Database.PrepareCommand(SqlSelectBySimplePhoneNumber, connection))
            {
                Database.PutParameter(dbCommand, "?SimplePhoneNumber", simplePhoneNumber);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        public string GetNotificationEmail(IDbConnection connection)
        {
            if (UserId == null)
                return null;

            var relatedUser = User.FindByPrimaryKey(UserId.Value, connection);

            if (relatedUser.IsActive && !string.IsNullOrEmpty(relatedUser.Email))
                return relatedUser.Email;

            return null;
        }
    }
}
      