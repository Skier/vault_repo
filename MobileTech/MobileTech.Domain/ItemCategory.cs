using System;
using System.Collections.Generic;
using System.Data;
using MobileTech.Data;


namespace MobileTech.Domain
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {

        }

        #region Finders

        const String SqlFindByTypeSortByName = "Select ItemCategoryId,ItemTypeId,Name,Description from ItemCategory Where ItemTypeId = @ItemTypeId Order By Name";
        const String SqlFindByTypeSortById = "Select ItemCategoryId,ItemTypeId,Name,Description from ItemCategory Where ItemTypeId = @ItemTypeId Order By ItemCategoryId";

        public static List<ItemCategory> Find(ItemFieldName sortField, ItemTypeEnum type)
        {
            IDbCommand dbCommand = Database.PrepareCommand(sortField == ItemFieldName.Name ? 
            SqlFindByTypeSortByName : SqlFindByTypeSortById);

            Database.PutParameter(dbCommand, "@ItemTypeId", (int)type);

            List<ItemCategory> rv = new List<ItemCategory>();

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    rv.Add(Load(dataReader));
                }
            }

            return rv;
        }


        const String SqlFindCountByType = "Select Count(*) from ItemCategory where ItemTypeId=@ItemTypeId";

        public static int Count(ItemTypeEnum type)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindCountByType);

            Database.PutParameter(dbCommand, "@ItemTypeId", (int)type);

            Object o = dbCommand.ExecuteScalar();

            int rv = 0;

            if (o is long)
                rv = (int)(long)o;
            else if (o is int)
                rv = (int)o;

            return rv;

        }

        #endregion

        #region Service

        public static int ProductCategoryCount
        {
            get
            {
                return Counter.GetValue("itemcategory_product");
            }
        }

        public static int EquipmentCategoryCount
        {
            get
            {
                return Counter.GetValue("itemcategory_equipment");
            }
        }

        public static void UpdateCounter()
        {
            Counter.Update("itemcategory_product", Count(ItemTypeEnum.Product));
            Counter.Update("itemcategory_equipment", Count(ItemTypeEnum.Equipment));
        }
        #endregion
    }
}
