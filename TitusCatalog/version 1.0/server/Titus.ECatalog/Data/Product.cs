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

    public class Product
    {

        private static Product c_Product = new Product();

        public static Product GetInstance()
        {
            return c_Product;
        }

        private Product()
        {
        }

        private const string SQL_SELECT_BY_SUB_CATEGORY = @"
            select  [Id],
                    [CategoryId],
                    [Name],
                    [Description],
                    [Information],
                    [MarketingInfo]
            from    [v_Product]
            where   [CategoryId] = @SubCategoryId";

        public List<ProductDataObject> FindBySubCategory(SqlTransaction tran, long subCategoryId)
        {
            List<ProductDataObject> result = new List<ProductDataObject>();

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_SUB_CATEGORY,
                    new DbParameter[1] { new SqlParameter("@SubCategoryId", subCategoryId) }))
            {
                while (dataReader.Read())
                {
                    ProductDataObject productInfo = new ProductDataObject();

                    productInfo.ProductId = (long)dataReader.GetValue(0);
                    productInfo.SubCategoryId = (long)dataReader.GetValue(1);
                    productInfo.Name = (string)dataReader.GetValue(2);
                    productInfo.Description = (string)dataReader.GetValue(3);

                    if (!dataReader.IsDBNull(4)) {
                        productInfo.Information = (string)dataReader.GetValue(4);
                    }

                    if (!dataReader.IsDBNull(5)) {
                        productInfo.MarketingInfo = (string)dataReader.GetValue(5);
                    }

                    result.Add(productInfo);
                }
            }

            return result;
        }

    }

}
