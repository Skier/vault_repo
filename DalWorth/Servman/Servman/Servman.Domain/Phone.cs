using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class Phone
    {
        public Phone()
        {
            
        }

        private const String SqlSelectByPhoneNumber = @"
SELECT * 
  FROM Phone
 WHERE Number = ?PhoneNumber; ";

        public static Phone GetByPhoneNumber(string phoneNumber, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPhoneNumber, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneNumber", phoneNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectActive = @"
SELECT * 
  FROM Phone
 WHERE TwilioId IS NOT NULL
   AND IsRemoved IS NULL OR IsRemoved = 0 ; ";

        public static List<Phone> GetActivePhoneNumbers(IDbConnection connection)
        {
            var result = new List<Phone>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectActive, connection))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        private const String SqlSelectByBusinessPartnerId = @"
SELECT * 
  FROM Phone p
    INNER JOIN PhoneToBusinessPartner pbp ON p.Id = pbp.PhoneId
 WHERE pbp.BusinessPartnerId = ?BusinessPartnerId ; ";

        public static List<Phone> GetByBusinessPartnerId(int businessPartnerId, IDbConnection connection)
        {
            var result = new List<Phone>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByBusinessPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        private const String SqlSelectBySalesRepId = @"
SELECT * 
  FROM Phone p
    INNER JOIN PhoneToSalesRep psr ON p.Id = psr.PhoneId
 WHERE psr.SalesRepId = ?SalesRepId ; ";

        public static List<Phone> GetBySalesRepId(int salesRepId, IDbConnection connection)
        {
            var result = new List<Phone>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectBySalesRepId, connection))
            {
                Database.PutParameter(dbCommand, "?SalesRepId", salesRepId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
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
