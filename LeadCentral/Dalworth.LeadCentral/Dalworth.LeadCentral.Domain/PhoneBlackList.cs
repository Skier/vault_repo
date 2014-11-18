using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;
using Dalworth.Common.SDK;


namespace Dalworth.LeadCentral.Domain
{
    public partial class PhoneBlackList
    {
        public PhoneBlackList()
        {
        }

        private const string SqlSelectByCallerPhone = @"
SELECT * 
  FROM PhoneBlackList
 WHERE PhoneDigits = ?CallerPhone 
";

        public static List<PhoneBlackList> GetByCallerPhone(string phone, IDbConnection connection)
        {
            var result = new List<PhoneBlackList>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByCallerPhone, connection))
            {
                Database.PutParameter(dbCommand, "?CallerPhone", StringUtil.ExtractLastSevenDigits(phone));

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
      