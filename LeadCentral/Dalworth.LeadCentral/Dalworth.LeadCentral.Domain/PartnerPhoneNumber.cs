using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class PartnerPhoneNumber
    {
        public PartnerPhoneNumber()
        {
        }

        private const string SqlSelectByPartnerId = @"
SELECT * 
  FROM PartnerPhoneNumber
 WHERE BusinessPartnerId = ?BusinessPartnerId
";

        public static List<PartnerPhoneNumber> GetByPartnerId(int businessPartnerId, IDbConnection connection)
        {
            var result = new List<PartnerPhoneNumber>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId);

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
    }
}
      