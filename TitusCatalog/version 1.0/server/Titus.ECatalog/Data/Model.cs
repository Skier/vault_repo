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

    public class Model
    {

        private static Model c_Model = new Model();

        public static Model GetInstance()
        {
            return c_Model;
        }

        private Model()
        {
        }

        private const string SQL_INSERT = @"
        insert  into [DocumentPageModel]
              ( [DocumentPageId],
                [ModelId] )
        values( @DocumentPageId,
                @ModelId );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, ModelDataObject modelInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@DocumentPageId", modelInfo.PageId),
                new SqlParameter("@ModelId", modelInfo.ModelId)
            };

            modelInfo.ModelPageId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [DocumentPageModel]
        set     [DocumentPageId] = @DocumentPageId
        where   [DocumentPageModelId] = @DocumentPageModelId";

        public void Update(SqlTransaction tran, ModelDataObject modelInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@DocumentPageModelId", modelInfo.ModelPageId),
                new SqlParameter("@DocumentPageId", modelInfo.PageId)
            };
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_SELECT_BY_ID = @"
        select  [DocumentPageModelId],
                [DocumentPageId],
                [ModelId]
        from    [DocumentPageModel]
        where   [ModelId] = @ModelId";

        public int FindById(SqlTransaction tran, int modelId)
        {
            int result = 0;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID,
                    new DbParameter[1] { new SqlParameter("ModelId", modelId) }))
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
