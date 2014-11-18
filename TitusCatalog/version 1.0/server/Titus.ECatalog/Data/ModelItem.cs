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

    public class ModelItem
    {

        private static ModelItem c_ModelItem = new ModelItem();

        public static ModelItem GetInstance()
        {
            return c_ModelItem;
        }

        private ModelItem()
        {
        }

/*         private const string SQL_SELECT_ALL = @"
        select  Cat1, CatId, Cat2, Cat2Id, Cat3, Cat3Id, Name, ModelId, MfgCode, Description, Image_Id
        from    EC_Menu_ModelItems"; */

        private const string SQL_SELECT_ALL = @"
        select  distinct Cat3Id, Name, ModelId, Description
        from    EC_Menu_ModelItems";

        public List<ModelItemDataObject> FindAll(SqlTransaction tran)
        {
            List<ModelItemDataObject> result = new List<ModelItemDataObject>();

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, new DbParameter[0] { }))
            {
                while (dataReader.Read())
                {
                    ModelItemDataObject itemInfo = new ModelItemDataObject();

                    /* itemInfo.Cat1 = (string)dataReader.GetValue(0);
                    itemInfo.CatId = (long)dataReader.GetValue(1);
                    itemInfo.Cat2 = (string)dataReader.GetValue(2);
                    itemInfo.Cat2Id = (long)dataReader.GetValue(3);
                    itemInfo.Cat3 = (string)dataReader.GetValue(4);
                    itemInfo.Cat3Id = (long)dataReader.GetValue(5);
                    itemInfo.Name = (string)dataReader.GetValue(6);
                    itemInfo.ModelId = (long)dataReader.GetValue(7);
                    itemInfo.Code = (string)dataReader.GetValue(8);
                    itemInfo.Description = (string)dataReader.GetValue(9);
                    itemInfo.ImageGUID = (string)dataReader.GetGuid(10).ToString(); */

                    itemInfo.Cat3Id = (long)dataReader.GetValue(0);
                    itemInfo.Name = (string)dataReader.GetValue(1);
                    itemInfo.ModelId = (long)dataReader.GetValue(2);
                    itemInfo.Description = (string)dataReader.GetValue(3);

                    result.Add(itemInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [DocumentPageModelItem]
              ( [DocumentPageId],
                [ModelItemId] )
        values( @DocumentPageId,
                @ModelItemId );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, int itemId, int pageId)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@DocumentPageId", pageId),
                new SqlParameter("@ModelItemId", itemId)
            };

            SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [DocumentPageModelItem]
        set     [DocumentPageId] = @DocumentPageId
        where   [DocumentPageModelItemId] = @DocumentPageModelItemId";

        public void Update(SqlTransaction tran, int id, int pageId)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@DocumentPageModelItemId", id),
                new SqlParameter("@DocumentPageId", pageId)
            };

            SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_SELECT_BY_ID = @"
        select  [DocumentPageModelItemId],
                [DocumentPageId],
                [ModelItemId]
        from    [DocumentPageModelItem]
        where   [ModelItemId] = @ModelItemId";

        public int FindById(SqlTransaction tran, int modelItemId)
        {
            int result = 0;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID,
                    new DbParameter[1] { new SqlParameter("ModelItemId", modelItemId) }))
            {
                if (dataReader.Read())
                {
                    result = (int)dataReader.GetValue(0);
                }
            }

            return result;
        }

    }

}
