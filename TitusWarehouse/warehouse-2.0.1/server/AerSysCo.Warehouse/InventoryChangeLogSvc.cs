using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{
public class InventoryChangeLogSvc
{
    public static InventoryChangeLog Insert(SqlTransaction tx, InventoryChangeLog log) {
        string sql = 
           " insert into InventoryChangeLog (InventoryId, MacPacUpdateId, ChangeDate, Qty, Balance) "
          +" values (@InventoryId, @MacPacUpdateId, @ChangeDate, @Qty, @Balance) ";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add( new SqlParameter("@InventoryId", log.inventoryId));
        parms.Add( new SqlParameter("@MacPacUpdateId", log.macPacUpdateId));
        parms.Add( new SqlParameter("@ChangeDate", log.changeDate));
        parms.Add( new SqlParameter("@Qty", log.qty));
        parms.Add( new SqlParameter("@Balance", log.balance));

        SQLHelper.ExecuteNonQuery(tx, CommandType.Text, sql, parms.ToArray());
        log.inventoryChangeLogId = SQLHelper.GetIdentity(tx);
        return log;
    }
}
}
