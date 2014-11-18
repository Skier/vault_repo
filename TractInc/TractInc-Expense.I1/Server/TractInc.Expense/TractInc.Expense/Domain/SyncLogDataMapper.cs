using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Weborb.Data.Management;
//using Weborb.Data.Management.MSSql;

namespace TractInc.Expense.Domain
{
public partial class SyncLogDataMapper
{

    private const string SqlMaxTimestamp = @"select max(SyncTimeStamp) from synclog where deviceid = @deviceid and assetid = @assetId and syncTimeStamp < @maxTimeStamp";

    public System.DateTime findMaxTimeStamp(string deviceid, int assetid, DateTime maxTimeStamp) {      
        using (Database database = new Database()) {
            using (SqlCommand sqlCommand = new SqlCommand(SqlMaxTimestamp, database.Connection)) {
                sqlCommand.Parameters.Add("@deviceid", deviceid);
                sqlCommand.Parameters.Add("@assetId", assetid);
                sqlCommand.Parameters.Add("@maxTimeStamp", maxTimeStamp);
             
                using (IDataReader dataReader = sqlCommand.ExecuteReader()) {
                    if ( dataReader.Read() ) {
                        return dataReader.GetDateTime(0);
                    } else {
                        return System.DateTime.MinValue;
                    }
                }
            }
        }        
    }

}
}
      