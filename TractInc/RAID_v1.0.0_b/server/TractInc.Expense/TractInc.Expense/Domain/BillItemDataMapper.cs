using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.Expense.Domain
{
    public partial class BillItemDataMapper :_BillItemDataMapper
    {

              

              public BillItemDataMapper()
              {}
              public BillItemDataMapper(TractIncRAIDDb database):base(database)
              {}

        private String SqlSelectBillItemsToCreateInvoice = @"
            Select *
              From [BillItem] b 
             inner join AssetAssignment aa on aa.AssetAssignmentId = b.AssetAssignmentId
             inner join Afe on afe.AFE = aa.AFE
             left join InvoiceItem i on b.BillItemId = i.BillItemId And i.Status <> 'VOID'
             Where b.Status = 'CONFIRMED' 
               And i.BillItemId is null
               And Afe.ClientId = @ClientId
               And cast(substring(b.BillingDate, 7, 4) as int) = @Year
               And cast(substring(b.BillingDate, 1, 2) as int) = @Month
               ";

        public List<BillItem> getToCreateInvoice(int year, int month, bool isFirstPart, int clientId, int assetId)
        {
            List<BillItem> result = new List<BillItem>();

            if (isFirstPart)
                SqlSelectBillItemsToCreateInvoice += " And cast(substring(b.BillingDate, 4, 2) as int) < 16 ";
            else
                SqlSelectBillItemsToCreateInvoice += " And cast(substring(b.BillingDate, 4, 2) as int) > 15 ";

            if (assetId != 0) {
                SqlSelectBillItemsToCreateInvoice += " And aa.AssetId = ";
                SqlSelectBillItemsToCreateInvoice += assetId.ToString();
            }
            
            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlSelectBillItemsToCreateInvoice))
                {

                    sqlCommand.Parameters.AddWithValue("@ClientId", clientId);
                    sqlCommand.Parameters.AddWithValue("@Year", year);
                    sqlCommand.Parameters.AddWithValue("@Month", month);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            result.Add(doLoad(dataReader));
                        }
                    }
                }
            }

            return result;
        }

    }
}
        