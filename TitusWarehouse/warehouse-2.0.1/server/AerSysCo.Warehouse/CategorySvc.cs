using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{
public class CategorySvc
{
    public static List<Category> GetByBrand(SqlTransaction tran, int brandId) {
        Logger.ASSERT(0 != brandId );
        return Select(tran, 0, 0, brandId, null);
    } 
    public static List<Category> GetChildren(SqlTransaction tran, int parentId, int brandId) {
        Logger.ASSERT(0 != parentId || 0 != brandId );
        return Select(tran, 0, parentId, brandId, null);
    } 

    public static Category FindByName(SqlTransaction tran, string name, int parentId, int brandId) {
        Logger.ASSERT(0!= brandId);
        Logger.ASSERT(null!= name);
        List<Category> result = Select(tran, 0, parentId, brandId, name);
        Logger.ASSERT(1>=result.Count);
        if ( 0 == result.Count ) {
            return null;
        }
        return result[0];
    } 

    public static bool IsEmpty(SqlTransaction tran, int categoryId) {
        string sql = 
           " select CategoryId  "
          +"   from Category "
          +"  where CategoryId = @CategoryId "
          +"    and   ( exists ( select 1 from Category c where c.ParentCategoryId = @CategoryId ) "
          +"       or ( exists ( select 1 from Model m where m.CategoryId  = @CategoryId ) ) ) ";

        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@CategoryId", categoryId ));
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            if ( rdr.Read() ) {
                return false;
            } else {
                return true;
            }
        }
    }

    public static void Delete(SqlTransaction tran, int categoryId ) {
        string sql = " delete from Category where CategoryId = @CategoryId ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@CategoryId", categoryId ));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());

        Logger.GetAppLogger().Debug("CategorySvc: deleted " + categoryId);
    }

    public static Category Insert(SqlTransaction tran, Category cat) {
        string sql = 
            " insert into Category (BrandId, ParentCategoryId, Name, CreatedByUser, DateCreated, LastUpdateDate ) "
           +" values( @BrandId, @ParentCategoryId, @Name, @CreatedByUser, @DateCreated, @LastUpdateDate ) ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@BrandId", cat.BrandId ));
        if ( 0 == cat.ParentCategoryId ) {
            parms.Add(new SqlParameter("@ParentCategoryId", DBNull.Value));
        } else {
            parms.Add(new SqlParameter("@ParentCategoryId", cat.ParentCategoryId));
        }
        parms.Add(new SqlParameter("@Name", cat.Name ));
        parms.Add(new SqlParameter("@CreatedByUser", cat.createdByUser));
        parms.Add(new SqlParameter("@DateCreated", cat.dateCreated));
        parms.Add(new SqlParameter("@LastUpdateDate", cat.lastUpdateDate ));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        cat.CategoryId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("CategorySvc: created " + cat.Name);
        return cat;
    }

    private static List<Category> Select(SqlTransaction tran, int id, int parentId, int brandId, string name) {
        string sql = "select CategoryId, BrandId, ParentCategoryId, Name, CreatedByUser, "
                   + "       DateCreated, LastUpdateDate " 
                   + "  from Category " 
                   + " where  1=1 ";
        ArrayList parms = new ArrayList();
        if ( 0 != id ) {
            sql += " and CategoryId = @id ";
            SqlParameter param = new SqlParameter("@id", id);
            parms.Add(param);
        } else {
            if ( null != name ) {
                sql += " and BrandId = @brandId ";
                parms.Add( new SqlParameter("@brandId", brandId));
                if ( 0 != parentId ) {
                    sql += " and  ParentCategoryId = @parentId ";
                    parms.Add( new SqlParameter("@parentId", parentId));
                } else {
                    sql += " and  ParentCategoryId  is null ";
                }
                sql += " and Name = @name ";
                parms.Add( new SqlParameter("@name", name));
            } else {
                if ( 0 != parentId ) {
                    sql += " and  ParentCategoryId = @parentId ";
                    SqlParameter param = new SqlParameter("@parentId", parentId);
                    parms.Add(param);
                } else {
                    sql += " and BrandId = @brandId ";
                    SqlParameter param = new SqlParameter("@brandId", brandId);
                    parms.Add(param);
                }
            }
        }
        List<Category> result = new List<Category>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, ( SqlParameter[])parms.ToArray(typeof(SqlParameter))) ) {
            while( rdr.Read() ) {
                Category c = new Category();
                c.CategoryId = rdr.GetInt32(rdr.GetOrdinal("CategoryId"));
                c.BrandId = rdr.GetInt32(rdr.GetOrdinal("BrandId"));
                c.ParentCategoryId = !rdr.IsDBNull(rdr.GetOrdinal("ParentCategoryId")) 
                        ? rdr.GetInt32(rdr.GetOrdinal("ParentCategoryId"))
                        : 0;
                c.Name = rdr.GetString(rdr.GetOrdinal("Name"));
                TraceableSvc.FromReader(rdr, c);
                result.Add(c);
            }
        }
        return result;
    }

};
};
