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

    public class Category
    {

        private static Category c_Category = new Category();

        public static Category GetInstance()
        {
            return c_Category;
        }

        private Category()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [Id],
                    [Sort],
                    [Name],
                    [Description],
                    [Color1],
                    [Color2],
                    [Color3],
                    [Color4]
            from    [v_Category]";

        public List<CategoryDataObject> FindAll(SqlTransaction tran)
        {
            List<CategoryDataObject> result = new List<CategoryDataObject>();

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, new DbParameter[0]))
            {
                while (dataReader.Read())
                {
                    CategoryDataObject categoryInfo = new CategoryDataObject();

                    categoryInfo.CategoryId = (long)dataReader.GetValue(0);
                    categoryInfo.Sort = (int)dataReader.GetValue(1);
                    categoryInfo.Name = (string)dataReader.GetValue(2);
                    categoryInfo.Description = (string)dataReader.GetValue(3);
                    categoryInfo.Color1 = (string)dataReader.GetValue(4);
                    categoryInfo.Color2 = (string)dataReader.GetValue(5);
                    categoryInfo.Color3 = (string)dataReader.GetValue(6);
                    categoryInfo.Color4 = (string)dataReader.GetValue(7);

                    result.Add(categoryInfo);
                }
            }

            return result;
        }

    }

}
