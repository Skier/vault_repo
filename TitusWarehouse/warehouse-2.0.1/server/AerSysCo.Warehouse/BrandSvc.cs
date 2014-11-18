/*
 * 
 */
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
public class BrandSvc 
{
    public static List<Brand> GetAll(SqlTransaction tran) {
        return Select(tran, 0, null);
    }                       

    public static Brand FindById(SqlTransaction tran,int id) {
        Logger.ASSERT(0 != id);
        List<Brand> result = Select(tran, id, null);
        Logger.ASSERT(1 >= result.Count);
        if ( 0 == result.Count ) {
            string message = string.Format("No brand with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static Brand FindByCode(SqlTransaction tran,string code) {
        Logger.ASSERT(null != code);
        List<Brand> result = Select(tran, 0, code);
        Logger.ASSERT(1>=result.Count);
        if ( 0 == result.Count ) {
            string message = string.Format("No brand with code {0} ", code);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static Brand FindByCodeSoft(SqlTransaction tran,string code) {
        Logger.ASSERT(null != code);
        List<Brand> result = Select(tran, 0, code);
        Logger.ASSERT(1>=result.Count);
        if ( 0 == result.Count ) {
             return null;
        }
        return result[0];
    }


    private static List<Brand> Select(SqlTransaction tran, int id, string code) {
        String sql = " select BrandId, code, BrandName, ImageURLPrefix  "
                   + "   from Brand "
                   + "  where 1=1 ";
        List<SqlParameter> parms = new List<SqlParameter>();
        if ( 0 != id ) {
             sql += " and BrandId = @id ";
             SqlParameter param = new SqlParameter("@id", id);
             parms.Add(param);
        }
        if ( null != code ) {
             sql += " and code = @code ";
             SqlParameter param = new SqlParameter("@code", code);
             parms.Add(param);
        }

        List<Brand> result = new List<Brand>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
            while( rdr.Read() ) {
                Brand b = new Brand();
                b.brandId = rdr.GetInt32(rdr.GetOrdinal("BrandId"));
                b.code = rdr.GetString(rdr.GetOrdinal("code"));
                b.brandName = rdr.GetString(rdr.GetOrdinal("BrandName"));
                b.imageURLprefix = rdr.IsDBNull(rdr.GetOrdinal("ImageURLPrefix")) ? "http://" : rdr.GetString(rdr.GetOrdinal("ImageURLPrefix"));
                result.Add(b);
            }
        }
          
        return result;
    }

};
};