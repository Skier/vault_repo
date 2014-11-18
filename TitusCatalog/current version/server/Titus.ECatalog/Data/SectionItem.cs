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

    public class SectionItem
    {

        private static SectionItem c_SectionItem = new SectionItem();

        public static SectionItem GetInstance()
        {
            return c_SectionItem;
        }

        private SectionItem()
        {
        }

        private const string SQL_INSERT = @"
        insert  into [EC_SectionItem]
              ( [SectionPageId],
                [ModelId],
                [CatalogLevel] )
        values( @SectionPageId,
                @ModelId,
                @CatalogLevel );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, SectionItemDataObject itemInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@SectionPageId", itemInfo.SectionPageId),
                new SqlParameter("@ModelId", itemInfo.ModelId),
                new SqlParameter("@CatalogLevel", itemInfo.CatalogLevel)
            };

            SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [EC_SectionItem]
        set     [SectionPageId] = @SectionPageId,
                [CatalogLevel] = @CatalogLevel
        where   [SectionItemId] = @SectionItemId";

        public void Update(SqlTransaction tran, SectionItemDataObject itemInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@SectionItemId", itemInfo.SectionItemId),
                new SqlParameter("@SectionPageId", itemInfo.SectionPageId),
                new SqlParameter("@CatalogLevel", itemInfo.CatalogLevel)
            };

            SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_SELECT_BY_MODEL_AND_LEVEL = @"
        select  [SectionItemId],
                [SectionPageId],
                [ModelId],
                [CatalogLevel]
        from    [EC_SectionItem]
        where   [ModelId] = @ModelId
        and     [CatalogLevel] = @CatalogLevel";

        public SectionItemDataObject FindById(SqlTransaction tran, int modelId, int catalogLevel)
        {
            SectionItemDataObject result = null;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_AND_LEVEL,
                    new DbParameter[2] {
                        new SqlParameter("ModelId", modelId),
                        new SqlParameter("CatalogLevel", catalogLevel)
                    }))
            {
                if (dataReader.Read())
                {
                    result = new SectionItemDataObject();
                    result.SectionItemId = (int)dataReader.GetValue(0);
                    result.SectionPageId = (int)dataReader.GetValue(1);
                    result.ModelId = (int)dataReader.GetValue(2);
                    result.CatalogLevel = (int)dataReader.GetValue(3);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_MODEL = @"
        select  [SectionItemId],
                [SectionPageId],
                [ModelId],
                [CatalogLevel]
        from    [EC_SectionItem]
        where   [ModelId] = @ModelId";

        public List<SectionItemDataObject> FindByModelId(SqlTransaction tran, int modelId)
        {
            List<SectionItemDataObject> result = new List<SectionItemDataObject>();

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL,
                    new DbParameter[1] {
                        new SqlParameter("ModelId", modelId)
                    }))
            {
                while (dataReader.Read())
                {
                    SectionItemDataObject itemInfo = new SectionItemDataObject();
                    itemInfo.SectionItemId = (int)dataReader.GetValue(0);
                    itemInfo.SectionPageId = (int)dataReader.GetValue(1);
                    itemInfo.ModelId = (int)dataReader.GetValue(2);
                    itemInfo.CatalogLevel = (int)dataReader.GetValue(3);
                    result.Add(itemInfo);
                }
            }

            return result;
        }

        private const string SQL_GET_ITEM_LOCATION = @"
        select  s.[SectionPrefix], sp.[SectionPageNumber], s.[StartPageNumber] + sp.[SectionPageNumber] - 2
        from    [EC_SectionItem] si
                inner join [EC_SectionPage] sp
                        on si.[SectionPageId] = sp.[SectionPageId]
                inner join [EC_Section] s
                        on sp.[SectionId] = s.[SectionId]
        where   [ModelId] = @ModelId
        and     [CatalogLevel] = @CatalogLevel";

        public CatalogLocationDataObject GetItemLocation(SqlTransaction tran, int modelId, int catalogLevel)
        {
            CatalogLocationDataObject result = null;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_GET_ITEM_LOCATION,
                    new DbParameter[2] {
                        new SqlParameter("ModelId", modelId),
                        new SqlParameter("CatalogLevel", catalogLevel)
                    }))
            {
                if (dataReader.Read())
                {
                    result = new CatalogLocationDataObject();
                    result.Section = (string)dataReader.GetValue(0);
                    result.SectionPage = (int)dataReader.GetValue(1);
                    result.CatalogPage = (int)dataReader.GetValue(2);
                }
            }

            return result;
        }

    }

}
