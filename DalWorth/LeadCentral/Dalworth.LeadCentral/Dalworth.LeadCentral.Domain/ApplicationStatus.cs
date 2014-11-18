
using System;
using Dalworth.LeadCentral.Data;


namespace Dalworth.LeadCentral.Domain
{
    public enum BillingStatusEnum
    {
        Ok = 1, 
        Grace = 0
    }

    public partial class ApplicationStatus
    {
        public ApplicationStatus()
        {
        }

        private const String SqlSelectByUserId = @"
SELECT * FROM ApplicationStatus WHERE ServmanCustomerId = ?ServmanCustomerId; ";

        public static ApplicationStatus GetByServmanCustomerId(int servmanCustomerId)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByUserId))
            {
                Database.PutParameter(dbCommand, "?ServmanCustomerId", servmanCustomerId);

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
      