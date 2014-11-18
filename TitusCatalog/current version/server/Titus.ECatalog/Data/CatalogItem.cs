using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Titus.ECatalog.Entity;
using Titus.ECatalog.Util;

namespace Titus.ECatalog.Data
{

    public class CatalogItem
    {

        private static CatalogItem c_CatalogItem = new CatalogItem();

        public static CatalogItem GetInstance()
        {
            return c_CatalogItem;
        }

        private CatalogItem()
        {
        }

        private const string SQL_SELECT_ALL = @"
        select  distinct Cat3Id, Name, ModelId, Description
        from    EC_TTS_Menu_ModelItems";

        public List<CatalogItemDataObject> FindAll(SqlTransaction tran)
        {
            List<CatalogItemDataObject> result = new List<CatalogItemDataObject>();

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, new DbParameter[0] { }))
            {
                while (dataReader.Read())
                {
                    CatalogItemDataObject itemInfo = new CatalogItemDataObject();

                    itemInfo.ParentId = (int)dataReader.GetInt64(0);
                    itemInfo.Name = (string)dataReader.GetValue(1);
                    itemInfo.ModelId = (int)dataReader.GetInt64(2);
                    itemInfo.Description = (string)dataReader.GetValue(3);

                    result.Add(itemInfo);
                }
            }

            return result;
        }

    }

}
