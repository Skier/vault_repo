using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using MobileTech.Data;

namespace MobileTech.Domain
{
    public enum ItemFieldName
    {
        Number,
        Name
    }

    public partial class Item
    {
        #region Constructors

        public Item()
        {

        }

        #endregion

        #region Extra fields

        public ItemTypeEnum Type
        {
            get
            {
                return (ItemTypeEnum)m_itemTypeId;
            }
            set
            {
                m_itemTypeId = (int)value;
            }
        }


        #endregion

        #region Finders
        /*public static void Search(String query, ItemFieldName field, int maxReturnCount, List<Item> returnList)
        {

            IDbCommand dbCommand = null;

            if (field == ItemFieldName.Name)
                dbCommand = Database.PrepareCommand(SqlSearchByName);
            else
                dbCommand = Database.PrepareCommand(SqlSearchByNumber);

            Database.PutParameter(dbCommand, "@Query", String.Format("{0}%", query));

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    returnList.Add(Load(dataReader));

                    if (maxReturnCount > 0 && returnList.Count == maxReturnCount)
                        break;
                }
            }

        }*/

        #region Count

        const String SqlSearchCount = "Select Count(*) from Item Where LocationId = @LocationId and RouteNumber = @RouteNumber and ItemCategoryId = @ItemCategoryId and ItemTypeId = @ItemTypeId";

        public static int Count(Route route,ItemCategory itemCategory)
        {
            int rv = 0;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSearchCount))
            {
                Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
                Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
                Database.PutParameter(dbCommand, "@ItemCategoryId", itemCategory.ItemCategoryId);
                Database.PutParameter(dbCommand, "@ItemTypeId", itemCategory.ItemTypeId);

                Object o = dbCommand.ExecuteScalar();

                if (o is long)
                    rv = (int)(long)o;
                else if (o is int)
                    rv = (int)o;
            }

            return rv;
        }

        public static int Count(ItemCategory itemCategory)
        {
            return Count(Route.Current, itemCategory);
        }
        #endregion

        #region Search Index

        #region Search with alphabetic indexation
        /* Search with alphabetic indexation
         * 
        const String SqlSearchIndexByNumber = "Select ItemNumberSortIndex from Item Where ItemNumber Like @Query Order By ItemNumberSortIndex";
        const String SqlSearchIndexByName = "Select NameSortIndex from Item Where IXName0 = @IX0 and Name Like @Query Order By NameSortIndex";
        const String SqlSearchIndexByNameIX0 = "Select NameSortIndex from Item Where IXName0 = @IX0 Order By NameSortIndex";

        public static int SearchIndex(ItemFieldName field, String query)
        {
            int rv = 0;

            IDbCommand dbCommand = null;


            if (field == ItemFieldName.Name && query.Length == 1)
                dbCommand = Database.PrepareCommand(SqlSearchIndexByNameIX0);
            else if (field == ItemFieldName.Name)
                dbCommand = Database.PrepareCommand(SqlSearchIndexByName);
            else
                dbCommand = Database.PrepareCommand(SqlSearchIndexByNumber);

            using(dbCommand)
            {
                if (field == ItemFieldName.Name)
                    Database.PutParameter(dbCommand, "@IX0", (int)query[0]);

                if (query.Length > 1 || field == ItemFieldName.Number)
                    Database.PutParameter(dbCommand, "@Query", String.Format("{0}%", query));

                using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if(dataReader.Read())
                    {
                        Object o = dataReader.GetValue(0);

                        if (o is long)
                            rv = (int)(long)o;
                        else if (o is int)
                            rv = (int)o;
                    }
                }
            }

            return rv;

        }*/

        #endregion

        const String SqlSearchIndexByNumber = "Select ItemNumberSortIndex from Item "+
            "Where ItemTypeId = @ItemTypeId and " +
            "ItemCategoryId = @ItemCategoryId and " +
            "ItemNumber Like @Query Order By ItemNumberSortIndex";

        const String SqlSearchIndexByName = "Select NameSortIndex from Item "+
            "Where ItemTypeId = @ItemTypeId and " +
            "ItemCategoryId = @ItemCategoryId and " +
            "Name Like @Query Order By NameSortIndex";

        public static int SearchIndex(ItemCategory itemCategory,
            ItemFieldName field, 
            String query)
        {
            int rv = 0;

            IDbCommand dbCommand = null;



            if (field == ItemFieldName.Name)
                dbCommand = Database.PrepareCommand(SqlSearchIndexByName);
            else
                dbCommand = Database.PrepareCommand(SqlSearchIndexByNumber);


            Database.PutParameter(dbCommand, "@ItemCategoryId", itemCategory.ItemCategoryId);
            Database.PutParameter(dbCommand, "@ItemTypeId", itemCategory.ItemTypeId);


            using (dbCommand)
            {


                Database.PutParameter(dbCommand, "@Query", String.Format("{0}%", query));

                using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (dataReader.Read())
                    {
                        Object o = dataReader.GetValue(0);

                        if (o is long)
                            rv = (int)(long)o;
                        else if (o is int)
                            rv = (int)o;
                    }
                }
            }

            return rv;

        }

        #endregion

        #region Search

        const String SqlSearchByName = "Select LocationId,RouteNumber,ItemNumber, "+
            "ItemCategoryId,ItemTypeId,Name,Description,NameSortIndex, " +
            "ItemNumberSortIndex from Item Where ItemTypeId = @ItemTypeId and " +
            "ItemCategoryId = @ItemCategoryId and NameSortIndex > @Skip "+
            "and NameSortIndex <= @Top Order By NameSortIndex";

        const String SqlSearchByNumber = "Select LocationId,RouteNumber,ItemNumber, "+
            "ItemCategoryId,ItemTypeId,Name,Description,NameSortIndex, "+
            "ItemNumberSortIndex from Item Where ItemTypeId = @ItemTypeId and " +
            "ItemCategoryId = @ItemCategoryId and ItemNumberSortIndex > @Skip "+
            "and ItemNumberSortIndex <= @Top Order By ItemNumberSortIndex";

        public static void Search(ItemCategory itemCategory, ItemFieldName field, int top, int skip, List<Item> returnList)
        {
            IDbCommand dbCommand = null;

            if (field == ItemFieldName.Name)
                dbCommand = Database.PrepareCommand(SqlSearchByName);
            else
                dbCommand = Database.PrepareCommand(SqlSearchByNumber);

            using (dbCommand)
            {
                Database.PutParameter(dbCommand, "@Top", skip + top);
                Database.PutParameter(dbCommand, "@Skip", skip);
                Database.PutParameter(dbCommand, "@ItemCategoryId", itemCategory.ItemCategoryId);
                Database.PutParameter(dbCommand, "@ItemTypeId", itemCategory.ItemTypeId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        returnList.Add(Load(dataReader));
                    }
                }
            }

        }

        #endregion

        #region FindBy

        /// <summary>
        /// Find item using current Route
        /// </summary>
        /// <param name="ItemNumber"></param>
        /// <returns></returns>
        public static Item FindBy(String itemNumber)
        {
            return FindByPrimaryKey(Route.Current.LocationId,
                Route.Current.RouteNumber, itemNumber);
        }

        #endregion

        #endregion

        #region Service

        public static int UpdateIndex()
        {
            List<Route> routeList = Route.Find();

            int affectedCount = 0;

            foreach (Route route in routeList)
            {
                affectedCount += UpdateIndex(route,ItemFieldName.Name);
                UpdateIndex(route,ItemFieldName.Number);
            }

            ItemCategory.UpdateCounter();

            return affectedCount;

        }

        /* Iteration 4
        const String SqlUpdateNameIndex = "Update Item Set NameSortIndex = @SortIndex, IXName0 = @IXName0 Where ItemNumber = @ItemNumber";
         */
        const String SqlUpdateNameIndex = "Update Item Set NameSortIndex = @SortIndex Where ItemNumber = @ItemNumber";
        const String SqlUpdateNumberIndex = "Update Item Set ItemNumberSortIndex = @SortIndex Where ItemNumber = @ItemNumber";

        const String SqlSelectIndexUpdateDataByName = "Select ItemNumber,Name,ItemCategoryId,ItemTypeId from Item Where LocationId = @LocationId and RouteNumber = @RouteNumber Order By ItemTypeId,ItemCategoryId,Name";
        const String SqlSelectIndexUpdateDataByNumber = "Select ItemNumber,Name,ItemCategoryId,ItemTypeId from Item Where LocationId = @LocationId and RouteNumber = @RouteNumber Order By ItemTypeId,ItemCategoryId,ItemNumber";

        class IndexData
        {
            public IndexData(String itemNumber, String name, int itemCategoryId, int itemTypeId)
            {
                ItemNumber = itemNumber;
                Name = name;
                ItemCategoryId = itemCategoryId;
                ItemTypeId = itemTypeId;
            }

            public String ItemNumber;
            public String Name;
            public int ItemCategoryId;
            public int ItemTypeId;
        }

        public static int UpdateIndex(Route route, ItemFieldName field)
        {
            List<IndexData> itemList = new List<IndexData>();

            int sortIndex = 0;
            int itemCategoryId = 0;
            int itemTypeId = 0;

            using (IDbCommand dbCommand = Database.PrepareCommand(field == ItemFieldName.Name ? SqlSelectIndexUpdateDataByName : SqlSelectIndexUpdateDataByNumber))
            {


                Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
                Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        itemList.Add(new IndexData(dataReader.GetString(0),
                            dataReader.GetString(1), 
                            dataReader.GetInt32(2),
                            dataReader.GetInt32(3)));
                    }
                }
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(field == ItemFieldName.Name ? SqlUpdateNameIndex : SqlUpdateNumberIndex))
            {

                foreach (IndexData indexData in itemList)
                {
                    // If next category id then begin new index
                    if (indexData.ItemCategoryId != itemCategoryId
                        || indexData.ItemTypeId != itemTypeId)
                        sortIndex = 0;

                    Database.PutParameter(dbCommand, "@ItemNumber", indexData.ItemNumber);
                    Database.PutParameter(dbCommand, "@SortIndex", ++sortIndex);
                    
                    /* Iteration 4
                    if (field == ItemFieldName.Name)
                    {
                        Database.PutParameter(dbCommand, "@IXName0",(int)indexData.Name[0] );
                    }*/

                    dbCommand.ExecuteNonQuery();
                    dbCommand.Parameters.Clear();

                    itemCategoryId = indexData.ItemCategoryId;
                    itemTypeId = indexData.ItemTypeId;
                }
            }

            return sortIndex;
        }
        #endregion
    }
}
