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

public class WarehouseSvc {
    public static List<Entity.Warehouse> GetAll(SqlTransaction tran) {
        return Select(tran, 0, null, null, false);
    }

    public static List<Entity.Warehouse> GetAllActive(SqlTransaction tran) {
        return Select(tran, 0, null, null, true);
    }

    public static Entity.Warehouse FindById(SqlTransaction tran, int id) {
        Logger.ASSERT(0 != id);
        List<Entity.Warehouse> result = Select(tran, id, null, null, false);
        if ( 0 == result.Count ) {
            string message = string.Format("No warehouse with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);

        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} warehouses with id {1} ", result.Count, id);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static Entity.Warehouse FindByName(SqlTransaction tran, String name) {
        List<Entity.Warehouse> result = Select(tran, 0, name, null, false);
        if ( 0 == result.Count ) {
            string message = string.Format("No warehouse with name '{0}' ", name);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} warehouses with name '{1}' ", result.Count, name);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static Entity.Warehouse FindByCode(SqlTransaction tran, String code) {
        List<Entity.Warehouse> result = Select(tran, 0, null, code, false);
        if ( 0 == result.Count ) {
            string message = string.Format("No warehouse with code '{0}' ", code);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} warehouses with code '{1}' ", result.Count, code);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }


    private static List<Entity.Warehouse> Select(SqlTransaction tran, int id, String name, string code, bool activeOnly) {
        String sql = " select WarehouseId, WarehouseName, WarehouseCode, Address1, Address2, City, State, Zip, Country  "
                   + "   from Warehouse "
                   + "  where 1=1 ";
        ArrayList parms = new ArrayList();
        if ( 0 != id ) {
             sql += " and WarehouseId = @id ";
             parms.Add( new SqlParameter("@id", id));
        }
        if ( null != name && !("".Equals(name)) ) {
             sql += " and WarehouseName = @name ";
             parms.Add( new SqlParameter("@name", name) );
        }
        if ( null != code && !("".Equals(code)) ) {
             sql += " and WarehouseCode = @code ";
             parms.Add(  new SqlParameter("@code", code));
        }

        if ( activeOnly ) {
             sql += " and WarehouseCode not like '*%' ";
        }

        List<Entity.Warehouse> result = new List<Entity.Warehouse>();
        using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, ( SqlParameter[])parms.ToArray(typeof(SqlParameter))) ) {
            while( rdr.Read() ) {
                Entity.Warehouse w = new Entity.Warehouse();
                w.warehouseId = rdr.GetInt32(rdr.GetOrdinal("WarehouseId"));
                w.warehouseName = rdr.GetString(rdr.GetOrdinal("WarehouseName"));
                w.warehouseCode = rdr.GetString(rdr.GetOrdinal("WarehouseCode"));
                w.address1 = rdr.GetString(rdr.GetOrdinal("Address1"));
                w.address2 = rdr.GetString(rdr.GetOrdinal("Address2"));
                w.city = rdr.GetString(rdr.GetOrdinal("City"));
                w.country = rdr.GetString(rdr.GetOrdinal("Country"));
                w.zip = rdr.GetString(rdr.GetOrdinal("Zip"));
                w.state = rdr.GetString(rdr.GetOrdinal("State"));
                result.Add(w);
            }
        }
          
        return result;
    }
};

};