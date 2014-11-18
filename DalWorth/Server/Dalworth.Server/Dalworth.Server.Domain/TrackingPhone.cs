using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class TrackingPhone
    {
        public TrackingPhone(){}

        #region NumberText

        public string NumberText
        {
            get
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(Number));
                }
                catch (FormatException)
                {
                    return Number;
                }
            }
        }

        #endregion

        #region FindBy OrderSource

        private const string SqlFindByOrderSource =
            @"SELECT tp.* FROM trackingphone tp
                inner join trackingphoneordersource tpos on tpos.TrackingPhoneId = tp.ID
            where tpos.OrderSourceId = ?OrderSourceId";

        public static List<TrackingPhone> FindByOrderSource(int orderSourceId, IDbConnection connection)
        {
            List<TrackingPhone> result = new List<TrackingPhone>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByOrderSource, connection))
            {
                Database.PutParameter(dbCommand, "?OrderSourceId", orderSourceId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindBy Phone

        private const string SqlFindByPhone =
            @"SELECT * FROM TrackingPhone 
                where Number = ?Number";

        public static TrackingPhone FindByPhone(string phoneNumber, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPhone, connection))
            {
                Database.PutParameter(dbCommand, "?Number", phoneNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion
    }
}
      