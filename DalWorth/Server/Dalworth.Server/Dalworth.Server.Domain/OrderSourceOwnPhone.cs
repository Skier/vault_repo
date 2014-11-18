using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;


namespace Dalworth.Server.Domain
{
    public partial class OrderSourceOwnPhone
    {
        public OrderSourceOwnPhone(){}

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
            @"SELECT * FROM ordersourceownphone
                where OrderSourceId = ?OrderSourceId";

        public static List<OrderSourceOwnPhone> FindByOrderSource(int orderSourceId, IDbConnection connection)
        {
            List<OrderSourceOwnPhone> result = new List<OrderSourceOwnPhone>();

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

        #region FindBy PhoneNumber

        private const string SqlFindByPhoneNumber =
            @"SELECT * FROM ordersourceownphone
                where Number = ?Number";

        public static OrderSourceOwnPhone FindByPhoneNumber(string phoneNumber, IDbConnection connection)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
                phoneNumber = string.Join(null, System.Text.RegularExpressions.Regex.Split(phoneNumber, "[^\\d]"));

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPhoneNumber, connection))
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
      