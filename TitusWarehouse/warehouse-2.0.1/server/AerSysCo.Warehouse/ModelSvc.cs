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
public class ModelSvc : TraceableSvc
{
    public static List<Model> GetByBrandId(SqlTransaction tran, int brandId) {
        Logger.ASSERT( 0 != brandId );
        return Select(tran, brandId, 0, 0, null);
    } 

    public static Model FindById(SqlTransaction tran, int id) {
        Logger.ASSERT( 0 != id );
        List<Model> result = Select(tran, 0, 0, id, null);
        Logger.ASSERT( 1 >= result.Count);
        if ( 0 == result.Count ) {
            string message = string.Format("No Model with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    } 

    public static Model FindByBrandAndName(SqlTransaction tran, int brandId, string name) {
        Logger.ASSERT(0 != brandId);
        Logger.ASSERT(null != name);
        List<Model> result = Select(tran, brandId, 0, 0, name);
        Logger.ASSERT( 1 >= result.Count);
        if ( 0 == result.Count ) {
            return null;
        }
        return result[0];
    } 

    public static List<Model> GetByCategoryOnly(SqlTransaction tran, int categoryId) {
        Logger.ASSERT(0 != categoryId);
        return Select(tran, 0, categoryId, 0, null);
    }

    public static Model Insert(SqlTransaction tran, Model model) {
        string sql = 
            " insert into Model (BrandId, CategoryId, ModelName, IsActive, DateCreated, CreatedByUser, LastUpdateDate ) "
           +" values(@BrandId, @CategoryId, @ModelName, @IsActive, @DateCreated, @CreatedByUser, @LastUpdateDate ) ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@BrandId", model.brandId));
        parms.Add(new SqlParameter("@CategoryId", model.categoryId));
        parms.Add(new SqlParameter("@ModelName", model.modelName ));
        parms.Add(new SqlParameter("@IsActive", model.isActive));
        parms.Add(new SqlParameter("@DateCreated", model.dateCreated));
        parms.Add(new SqlParameter("@CreatedByUser", model.createdByUser));
        parms.Add(new SqlParameter("@LastUpdateDate", model.lastUpdateDate));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, parms.ToArray());
        model.modelId = SQLHelper.GetIdentity(tran);

        Logger.GetAppLogger().Debug("ModelSvc: created " + model.modelName);
        return model;
    }

    private static List<Model> Select(SqlTransaction tran, int brandId, int categoryId, int id, string name) {
        string sql = "select ModelId, BrandId, CategoryId, ModelName, IsActive, DateCreated, CreatedByUser, LastUpdateDate " 
                   + "  from Model " 
                   + " where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != brandId ) {
            sql += " and BrandId = @brandId ";
            SqlParameter param = new SqlParameter("@brandId", brandId);
            parms.Add(param);
        }
        if ( 0 != categoryId ) {
            sql += " and CategoryId = @categoryId ";
            SqlParameter param = new SqlParameter("@categoryId", categoryId);
            parms.Add(param);
        }
        if ( 0 != id ) {
            sql += " and ModelId = @modelId ";
            SqlParameter param = new SqlParameter("@modelId", id);
            parms.Add(param);
        }
        if ( null != name ) {
            sql += " and ModelName = @ModelName ";
            SqlParameter param = new SqlParameter("@ModelName", name);
            parms.Add(param);
        }
        List<Model> result = new List<Model>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            while( rdr.Read() ) {
                Model m = new Model();
                m.brandId = rdr.GetInt32(rdr.GetOrdinal("BrandId"));
                m.categoryId = rdr.GetInt32(rdr.GetOrdinal("CategoryId"));
                m.modelId = rdr.GetInt32(rdr.GetOrdinal("ModelId"));
                m.modelName = rdr.GetString(rdr.GetOrdinal("ModelName"));
                m.isActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
                TraceableSvc.FromReader(rdr, m);
                result.Add(m);
            }
        }
        return result;
    }
};
};