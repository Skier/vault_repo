using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class OAuthConnection
    {
        public OAuthConnection()
        {
        }

        private const string SelectByServmanCustomerId = @"
SELECT * 
  FROM OAuthConnection 
 WHERE CustomerId = ?CustomerId AND IsActive = 1; ";

        public static OAuthConnection GetByCustomerId(int customerId)
        {
            using (var dbCommand = Database.PrepareCommand(SelectByServmanCustomerId))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }
    }
}
      