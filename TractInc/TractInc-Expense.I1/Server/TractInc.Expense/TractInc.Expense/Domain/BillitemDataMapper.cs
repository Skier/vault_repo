using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Weborb.Data.Management;

namespace TractInc.Expense.Domain
{
public partial class BillItemDataMapper
{
    private const String SqlSelectChanged = @"Select
            b.BillItemId, 
            b.BillId, 
            b.AssetAssignmentId,
            b.BillingDate,
            b.DayQty,
            b.BillRate,
            b.TotalHourlyBilling,
            b.Lodging,
            b.Meals,
            b.Phone,
            b.Copies,
            b.FilingFees,
            b.Status,
            b.BillItemGuid 
        From billitem b
            inner join assetassignment aa on b.assetassignmentid=aa.assetassignmentid
        where aa.assetId = @assetId and SyncTimeStamp > @syncTimeStamp";

  public List<BillItem> findChangedBillItems(int assetId, DateTime timestamp) {
      List<BillItem> result = new List<BillItem>();

      using (Database database = new Database()) {
          using (SqlCommand sqlCommand = new SqlCommand(SqlSelectChanged, database.Connection)) {
              sqlCommand.Parameters.Add("@assetId", assetId);
              sqlCommand.Parameters.Add("@syncTimeStamp", timestamp);

              using (IDataReader dataReader = sqlCommand.ExecuteReader()) {
                  while (dataReader.Read()) {
                      result.Add(doLoad(dataReader));
                  }
              }
          }
      }

      return result;
  }

}
}
      