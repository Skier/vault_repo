using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
  
namespace Dalworth.Server.Domain
{
    public partial class QbCustomerType
    {
        public QbCustomerType()
        {

        }

        #region FindActive

        private const string SqlSelectActive = SqlSelectAll + " where isActive = true order by fullname";
        public static List<QbCustomerType> FindActive(IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectActive, connection))
            {
                List<QbCustomerType> rv = new List<QbCustomerType>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }
        }

        #endregion

        #region CanBeSelected

        public static bool CanBeSelected(string listId)
        {
            if (string.IsNullOrEmpty(listId))
                return false;

            List<QbCustomerType> allActiveCustomerTypes = FindActive(null);

            QbCustomerType current = allActiveCustomerTypes.Find(delegate(QbCustomerType customerType)
                                                                     { return customerType.ListId == listId; });

            if (current == null)
                return false;

            if (current.SubLevel == 1)
            {
                QbCustomerType level2Child = allActiveCustomerTypes.Find(delegate(QbCustomerType customerType)
                                                    { return customerType.ParentRefListId == current.ListId
                                                      && customerType.SubLevel == 2;
                                                });
                if (level2Child != null)
                    return false;
            }

            if (current.SubLevel == 0)
            {
                QbCustomerType level1Child = allActiveCustomerTypes.Find(delegate(QbCustomerType customerType)
                                                    {
                                                        return customerType.ParentRefListId == current.ListId
                                                          && customerType.SubLevel == 1;
                                                    });
                if (level1Child != null)
                    return false;
            }

            return true;
        }
        #endregion
    }
}
      