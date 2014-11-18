using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;
using AerSysCo.Warehouse.WebReference;

namespace AerSysCo.Warehouse
{

public class ShippingTypeSvc {
    public static List<ShippingType> GetAll(SqlTransaction tran) {
        return Select(tran, 0);
    }

    public static ShippingType FindById(SqlTransaction tran, int id) {
        Logger.ASSERT(0!=id);
        List<ShippingType> result = Select(tran, id);
        if ( 0 == result.Count ) {
            string message = string.Format("No ShippingType with id {0} ", id);
            Logger.Error(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);

        } else if ( 1 != result.Count ){
            string message = string.Format("Found {0} ShippingTypes with id {1} ", result.Count, id);
            Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null); 
            throw new ApplicationException(message);
        }
        return result[0];
    }

    public static ShippingType FromReader(SqlDataReader rdr) {
        ShippingType st = new ShippingType();
        st.shippingTypeId = rdr.GetInt32(rdr.GetOrdinal("ShippingTypeId"));
        st.shippingType = rdr.GetString(rdr.GetOrdinal("ShippingType"));
        st.shippingCode = rdr.GetString(rdr.GetOrdinal("ShippingCode"));
        return st;
    }

    public static ShippingType FromFedEx(SqlTransaction tran, ServiceType srvType) {
        switch (srvType) {
            case ServiceType.FEDEX_2_DAY:
                return FindById(tran, 2);
            case ServiceType.FEDEX_3_DAY_FREIGHT: 
                return FindById(tran, 3);
            case ServiceType.FEDEX_GROUND: 
                return FindById(tran, 4);
            case ServiceType.STANDARD_OVERNIGHT: 
                return FindById(tran, 1);
            case ServiceType.INTERNATIONAL_PRIORITY: 
                return FindById(tran, 5);
            case ServiceType.INTERNATIONAL_ECONOMY:
                return FindById(tran, 6);
            default: 
                return null;
        } 
    }

    private static List<ShippingType> Select(SqlTransaction tran, int id) {
        string sql = "select ShippingTypeId, ShippingType, ShippingCode " 
                   + "  from ShippingType "
                   + " where 1=1 ";
       ArrayList parms = new ArrayList();
       if ( 0 != id ) {
           sql += " and ShippingTypeId = @id ";
           SqlParameter param = new SqlParameter("@id", id);
           parms.Add(param);
       }
       List<ShippingType> result = new List<ShippingType>();
       using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, ( SqlParameter[])parms.ToArray(typeof(SqlParameter))) ) {
           while( rdr.Read() ) {
               result.Add(FromReader(rdr));
           }
       }
       return result;

    }
};

};