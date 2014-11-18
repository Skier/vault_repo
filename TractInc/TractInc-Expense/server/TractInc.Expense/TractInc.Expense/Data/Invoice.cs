using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class Invoice
    {

        private static Invoice c_Invoice = new Invoice();

        public static Invoice GetInstance()
        {
            return c_Invoice;
        }

        private Invoice()
        {
        }

        private const string SQL_SELECT_CURRENT = @"
            select  [InvoiceId],
                    [InvoiceNumber],
                    [ClientId],
                    [ClientName],
                    [ClientAddress],
                    [ClientActive],
                    [Status],
                    [StartDate],
                    [TotalDailyAmt],
                    [DailyInvoiceAmt],
                    [OtherInvoiceAmt],
                    [TotalInvoiceAmt],
                    year([StartDate]) as [StartYear],
                    month([StartDate]) as [StartMonth],
                    day([StartDate]) as [StartDay]
            from    [Invoice]
            order   by StartYear desc, StartMonth desc, StartDay desc";

        public List<InvoiceDataObject> GetInvoices(SqlTransaction tran)
        {
            List<InvoiceDataObject> result = new List<InvoiceDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CURRENT, parms))
            {
                while (dataReader.Read())
                {
                    InvoiceDataObject invoiceInfo = new InvoiceDataObject();

                    invoiceInfo.InvoiceId = (int)dataReader.GetValue(0);
                    invoiceInfo.InvoiceNumber = (String)dataReader.GetValue(1);

                    if ("" == invoiceInfo.InvoiceNumber)
                    {
                        Remove(tran, invoiceInfo.InvoiceId);
                        continue;
                    }

                    invoiceInfo.ClientId = (int)dataReader.GetValue(2);
                    invoiceInfo.ClientName = (string)dataReader.GetValue(3);
                    invoiceInfo.ClientAddress = (string)dataReader.GetValue(4);
                    invoiceInfo.ClientActive = (bool)dataReader.GetValue(5);
                    invoiceInfo.Status = (string)dataReader.GetValue(6);
                    invoiceInfo.StartDate = (string)dataReader.GetValue(7);
                    invoiceInfo.TotalDailyAmt = (int)dataReader.GetValue(8);
                    invoiceInfo.DailyInvoiceAmt = (decimal)dataReader.GetValue(9);
                    invoiceInfo.OtherInvoiceAmt = (decimal)dataReader.GetValue(10);
                    invoiceInfo.TotalInvoiceAmt = (decimal)dataReader.GetValue(11);

                    List<int> assets = GetInvoiceAssets(tran, invoiceInfo.InvoiceId);
                    if (1 == assets.Count)
                    {
                        invoiceInfo.Landman = Asset.GetInstance().GetAsset(tran, (int)assets[0]).BusinessName;
                    }

                    result.Add(invoiceInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_INVOICE_ASSETS = @"
            select  distinct aa.[AssetId]
            from    [Invoice] i
                    inner join [InvoiceItem] ii
                            on ii.[InvoiceId] = i.[InvoiceId]
                    inner join [AssetAssignment] aa
                            on aa.[AssetAssignmentId] = ii.[AssetAssignmentId]
            where   i.[InvoiceId] = @InvoiceId";

        private List<int> GetInvoiceAssets(SqlTransaction tran, int invoiceId)
        {
            List<int> result = new List<int>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@InvoiceId", invoiceId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_INVOICE_ASSETS, parms))
            {
                while (dataReader.Read())
                {
                    int assetId = (int)dataReader.GetValue(0);

                    result.Add(assetId);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_ID = @"
            select  i.[InvoiceId],
                    i.[InvoiceNumber],
                    i.[ClientId],
                    i.[ClientName],
                    i.[ClientAddress],
                    i.[ClientActive],
                    i.[Status],
                    i.[StartDate],
                    i.[TotalDailyAmt],
                    i.[DailyInvoiceAmt],
                    i.[OtherInvoiceAmt],
                    i.[TotalInvoiceAmt]
            from    [Invoice] i
            where   i.[InvoiceId] = @InvoiceId";

        public InvoiceDataObject GetInvoice(SqlTransaction tran, int invoiceId)
        {
            InvoiceDataObject invoiceInfo = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@InvoiceId", invoiceId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    invoiceInfo = new InvoiceDataObject();

                    invoiceInfo.InvoiceId = (int)dataReader.GetValue(0);
                    invoiceInfo.InvoiceNumber = (String)dataReader.GetValue(1);
                    invoiceInfo.ClientId = (int)dataReader.GetValue(2);
                    invoiceInfo.ClientName = (string)dataReader.GetValue(3);
                    invoiceInfo.ClientAddress = (string)dataReader.GetValue(4);
                    invoiceInfo.ClientActive = (bool)dataReader.GetValue(5);
                    invoiceInfo.Status = (string)dataReader.GetValue(6);
                    invoiceInfo.StartDate = (string)dataReader.GetValue(7);
                    invoiceInfo.TotalDailyAmt = (int)dataReader.GetValue(8);
                    invoiceInfo.DailyInvoiceAmt = (decimal)dataReader.GetValue(9);
                    invoiceInfo.OtherInvoiceAmt = (decimal)dataReader.GetValue(10);
                    invoiceInfo.TotalInvoiceAmt = (decimal)dataReader.GetValue(11);

                    invoiceInfo.InvoiceItems = InvoiceItem.GetInstance().GetInvoiceItems(tran, invoiceId);

                    invoiceInfo.Assignments = AssetAssignment.GetInstance().GetAssignmentsForInvoice(tran, invoiceId);

                    int assetId = 0;

                    foreach (InvoiceItemDataObject itemInfo in invoiceInfo.InvoiceItems)
                    {
                        if (0 == assetId)
                        {
                            assetId = itemInfo.AssetId;
                        }

                        if (assetId != itemInfo.AssetId)
                        {
                            assetId = 0;
                            break;
                        }
                    }

                    if (0 != assetId)
                    {
                        invoiceInfo.Landman = Asset.GetInstance().GetAsset(tran, assetId).BusinessName;
                    }
                }
            }

            return invoiceInfo;
        }

        private const string SQL_INSERT = @"
        insert  into [Invoice]
              ( [InvoiceNumber],
                [ClientId],
                [ClientName],
                [ClientAddress],
                [ClientActive],
                [Status],
                [StartDate],
                [TotalDailyAmt],
                [DailyInvoiceAmt],
                [OtherInvoiceAmt],
                [TotalInvoiceAmt] )
        values( @InvoiceNumber,
                @ClientId,
                @ClientName,
                @ClientAddress,
                @ClientActive,
                @Status,
                @StartDate,
                @TotalDailyAmt,
                @DailyInvoiceAmt,
                @OtherInvoiceAmt,
                @TotalInvoiceAmt);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, InvoiceDataObject invoiceInfo)
        {
            DbParameter[] parms = new DbParameter[11] {
                new SqlParameter("@InvoiceNumber",   invoiceInfo.InvoiceNumber),
                new SqlParameter("@ClientId",        invoiceInfo.ClientId),
                new SqlParameter("@ClientName",      invoiceInfo.ClientName),
                new SqlParameter("@ClientAddress",   invoiceInfo.ClientAddress),
                new SqlParameter("@ClientActive",    invoiceInfo.ClientActive),
                new SqlParameter("@Status",          invoiceInfo.Status),
                new SqlParameter("@StartDate",       invoiceInfo.StartDate),
                new SqlParameter("@TotalDailyAmt",   invoiceInfo.TotalDailyAmt),
                new SqlParameter("@DailyInvoiceAmt", invoiceInfo.DailyInvoiceAmt),
                new SqlParameter("@OtherInvoiceAmt", invoiceInfo.OtherInvoiceAmt),
                new SqlParameter("@TotalInvoiceAmt", invoiceInfo.TotalInvoiceAmt)
            };

            invoiceInfo.InvoiceId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [Invoice]
        where   [InvoiceId] = @InvoiceId";

        public void Remove(SqlTransaction tran, int invoiceId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@InvoiceId", invoiceId) };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_UPDATE = @"
        update  [Invoice]
        set     [InvoiceNumber]         = @InvoiceNumber,
                [Status]                = @Status,
                [TotalDailyAmt]         = @TotalDailyAmt,
                [DailyInvoiceAmt]       = @DailyInvoiceAmt,
                [OtherInvoiceAmt]       = @OtherInvoiceAmt,
                [TotalInvoiceAmt]       = @TotalInvoiceAmt
        where   [InvoiceId] = @InvoiceId";

        public void Update(SqlTransaction tran, InvoiceDataObject invoiceInfo)
        {
            DbParameter[] parms = new DbParameter[7] {
                new SqlParameter("@InvoiceNumber",          invoiceInfo.InvoiceNumber),
                new SqlParameter("@Status",                 invoiceInfo.Status),
                new SqlParameter("@TotalDailyAmt",          invoiceInfo.TotalDailyAmt),
                new SqlParameter("@DailyInvoiceAmt",        invoiceInfo.DailyInvoiceAmt),
                new SqlParameter("@OtherInvoiceAmt",        invoiceInfo.OtherInvoiceAmt),
                new SqlParameter("@TotalInvoiceAmt",        invoiceInfo.TotalInvoiceAmt),
                new SqlParameter("@InvoiceId",              invoiceInfo.InvoiceId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_CHECK_INVOICE_NUMBER = @"
        select  *
        from    Invoice
        where   InvoiceNumber = @InvoiceNumber
        and     Status <> 'VOID'
        and     InvoiceId <> @InvoiceId";

        public bool CheckInvoiceNumber(SqlTransaction tran, int invoiceId, String invoiceNumber)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@InvoiceId",     invoiceId),
                new SqlParameter("@InvoiceNumber", invoiceNumber)
            };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_CHECK_INVOICE_NUMBER, parms))
            {
                if (dataReader.Read())
                {
                    return true;
                }
            }

            return false;
        }

    }

}
