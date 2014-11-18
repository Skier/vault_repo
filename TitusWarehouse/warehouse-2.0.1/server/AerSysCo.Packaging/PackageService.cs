using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;

namespace AerSysCo.Packaging
{

public class PackageService {
    /// <summary>
    /// If Lenght, Width or Height of a package is unknown, fill it with zero.
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="soppingCartId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static PackageSet[] Package(SqlTransaction tran, int shoppingCartShippmetId, PackageRequest[] request) {
        List<PackageSet> result = new List<PackageSet>();
        foreach (PackageRequest pack in request) {

            Logger.GetAppLogger().Debug(String.Format("SKU {0} count {1}", pack.SKU, pack.ItemCount));

            string sql = 
                "select ItemId, SKU, Description, Width, Length, Height, Weight, QtyIncrement, IsActive, DateCreated, CreatedByUser, LastUpdateDate "
               +"  from Item "
               +" where SKU = @SKU ";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@SKU", pack.SKU));

            using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()) ) {
                if( rdr.Read() ) { 
                    result.Add(new PackageSet(
                                   new Decimal(rdr.GetDouble(rdr.GetOrdinal("Weight"))), 
                                   Convert.ToInt32(rdr.GetDouble(rdr.GetOrdinal("Length"))),
                                   Convert.ToInt32(rdr.GetDouble(rdr.GetOrdinal("Width"))),
                                   Convert.ToInt32(rdr.GetDouble(rdr.GetOrdinal("Height")))));

                } else {
                    Logger.GetAppLogger().Error(String.Format("Item with SKU {0} not found", pack.SKU));
                } 
            }
           
       }
       return result.ToArray();
   }
}

}
