using System;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.Expense.Domain
{
    public partial class RateByAssignmentDataMapper :_RateByAssignmentDataMapper
    {
              public RateByAssignmentDataMapper()
              {}
              public RateByAssignmentDataMapper(TractIncRAIDDb database):base(database)
              {}

        private const String SqlSelectByBillItemId = @"
            Select *
              From RateByAssignment r
                   inner join BillItem bi on bi.AssetAssignmentId = r.AssetAssignmentId and bi.BillItemTypeId = r.BillItemTypeId
             Where bi.BillItemId = @BillItemId
";

        public RateByAssignment getRateByBillItemId( int billItemId)
        {
            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByBillItemId))
                {

                    sqlCommand.Parameters.AddWithValue("@BillItemId", billItemId);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                            return doLoad(dataReader);
                    }
                }
            }

            return null;

        }

    }
}
        