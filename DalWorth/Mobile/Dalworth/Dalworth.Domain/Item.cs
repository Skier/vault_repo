using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Item : ICounterField
    {
        public Item() { }

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Item"; }
        }

        #endregion

        #region ItemShape

        public ItemShapeEnum ItemShape
        {
            get { return (ItemShapeEnum)m_itemShapeId; }
            set{ m_itemShapeId = (int)value; }
        }

        #endregion

        #region GetDeliveryItems

        private const string SqlGetDeliveryItems =
            @"select i.* from WorkDetail wd
		    inner join Visit v on wd.VisitId = v.ID
                    inner join Task t on t.VisitId = v.ID
                    inner join TaskItemDelivery tid on tid.TaskId = t.ID                
                inner join Item i on i.ID = tid.ItemId
                where t.TaskTypeId = 2 and wd.WorkId = @WorkId";

        public static List<Item> GetDeliveryItems(Work work)
        {
            List<Item> items = new List<Item>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetDeliveryItems))
            {
                Database.PutParameter(dbCommand, "@WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        items.Add(Load(dataReader));
                    }
                }
            }

            return items;
        }

        #endregion

        #region Name

        public string Name
        {
            get
            {
                if (ItemShape == ItemShapeEnum.Rectangle)
                {
                    return "Rect, " + Width
                           + "x" + Height
                           + ", " + (Width * Height).ToString("0.00")
                           + "SF";
                }
                else if (ItemShape == ItemShapeEnum.Round)
                {
                    return "Round, D" + Diameter
                           + ", " + ((decimal.ToDouble(Diameter * Diameter) * Math.PI) / 4).ToString("0.00")
                           + "SF";
                }
                return string.Empty;
                
            }
        }

        #endregion

        #region FindByServerId

        private const string SqlFindByServerId =
            @"SELECT *
            FROM Item
                WHERE ServerId = @ServerId";

        public static Item FindByServerId(int serverId, IDbTransaction transaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServerId, transaction))
            {
                Database.PutParameter(dbCommand, "@ServerId", serverId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("Item not found by ServerId");
        }

        #endregion
    }
}
