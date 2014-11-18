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

    public class SubCategory
    {

        private static SubCategory c_SubCategory = new SubCategory();

        public static SubCategory GetInstance()
        {
            return c_SubCategory;
        }

        private SubCategory()
        {
        }

        private const string SQL_SELECT_BY_CATEGORY = @"
            select  [Id],
                    [CategoryId],
                    [Sort],
                    [Name],
                    [Description]
            from    [v_SubCategory]
            where   [CategoryId] = @CategoryId";

        public List<SubCategoryDataObject> FindByCategory(SqlTransaction tran, long categoryId)
        {
            List<SubCategoryDataObject> result = new List<SubCategoryDataObject>();

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_CATEGORY,
                    new DbParameter[1] { new SqlParameter("@CategoryId", categoryId) }))
            {
                while (dataReader.Read())
                {
                    SubCategoryDataObject subCategoryInfo = new SubCategoryDataObject();

                    subCategoryInfo.SubCategoryId = (long)dataReader.GetValue(0);
                    subCategoryInfo.CategoryId = (long)dataReader.GetValue(1);
                    subCategoryInfo.Sort = (int)dataReader.GetValue(2);
                    subCategoryInfo.Name = (string)dataReader.GetValue(3);
                    subCategoryInfo.Description = (string)dataReader.GetValue(4);

                    result.Add(subCategoryInfo);
                }
            }

            return result;
        }

    }

}
