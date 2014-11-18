using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

public class BillItem
{

    private static BillItem c_BillItem = new BillItem();

    public static BillItem GetInstance()
    {
        return c_BillItem;
    }

    private BillItem()
    {
    }

    private const string SQL_UPDATE = @"
        update  [BillItem]
        set     [BillItemTypeId]        = @BillItemTypeId,
                [BillId]                = @BillId,
                [AssetAssignmentId]     = @AssetAssignmentId,
                [BillingDate]           = @BillingDate,
                [Qty]                   = @Qty,
                [BillRate]              = @BillRate,
                [Status]                = @Status,
                [BillItemCompositionId] = @BillItemCompositionId
        where   [BillItemId] = @BillItemId";

    public void Update(SqlTransaction tran, BillItemDataObject billItemInfo)
    {
        DbParameter[] parms = new DbParameter[9] {
            new SqlParameter("@BillItemId",             billItemInfo.BillItemId),
            new SqlParameter("@BillItemTypeId",         billItemInfo.BillItemTypeId),
            new SqlParameter("@BillId",                 billItemInfo.BillId),
            new SqlParameter("@AssetAssignmentId",      billItemInfo.AssetAssignmentId),
            new SqlParameter("@BillingDate",            billItemInfo.BillingDate),
            new SqlParameter("@Qty",                    billItemInfo.Qty),
            new SqlParameter("@BillRate",               billItemInfo.BillRate),
            new SqlParameter("@Status",                 billItemInfo.Status),
            new SqlParameter("@BillItemCompositionId",  ((billItemInfo.BillItemCompositionId == 0)? SqlInt32.Null: billItemInfo.BillItemCompositionId))
        };

        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
    }

    private const string SQL_INSERT = @"
        insert  into [BillItem]
              ( [BillItemTypeId],
                [BillId],
                [AssetAssignmentId],
                [BillingDate],
                [Qty],
                [BillRate],
                [Status],
                [BillItemCompositionId] )
        values( @BillItemTypeId,
                @BillId,
                @AssetAssignmentId,
                @BillingDate,
                @Qty,
                @BillRate,
                @Status,
                @BillItemCompositionId );
        select  cast(scope_identity() as int)";

    public void Insert(SqlTransaction tran, BillItemDataObject billItemInfo)
    {
        DbParameter[] parms = new DbParameter[8] {
            new SqlParameter("@BillItemTypeId",         billItemInfo.BillItemTypeId),
            new SqlParameter("@BillId",                 billItemInfo.BillId),
            new SqlParameter("@AssetAssignmentId",      billItemInfo.AssetAssignmentId),
            new SqlParameter("@BillingDate",            billItemInfo.BillingDate),
            new SqlParameter("@Qty",                    billItemInfo.Qty),
            new SqlParameter("@BillRate",               billItemInfo.BillRate),
            new SqlParameter("@Status",                 billItemInfo.Status),
            new SqlParameter("@BillItemCompositionId",  (0 == billItemInfo.BillItemCompositionId)? DBNull.Value: (object)billItemInfo.BillItemCompositionId),
        };

        billItemInfo.BillItemId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
    }

    private const string SQL_REMOVE = @"
        delete from [BillItem]
        where  [BillItemId] = @BillItemId";

    public void Remove(SqlTransaction tran, BillItemDataObject billItemInfo)
    {
        DbParameter[] parms = new DbParameter[1] {
            new SqlParameter("@BillItemId", billItemInfo.BillItemId)
        };

        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
    }

    private const string SQL_REMOVE_BY_COMPOSITION_ID = @"
        delete from [BillItem]
        where  [BillItemCompositionId] = @BillItemCompositionId";

    public void Remove(SqlTransaction tran, int compositionId)
    {
        DbParameter[] parms = new DbParameter[1] {
            new SqlParameter("@BillItemCompositionId", compositionId)
        };

        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE_BY_COMPOSITION_ID, parms);
    }

    private const string SQL_SELECT_BY_BILL_ID = @"
        select  [BillItemId],
                [BillItemTypeId],
                [BillId],
                [AssetAssignmentId],
                [BillingDate],
                [Qty],
                [BillRate],
                [Status],
                [BillItemCompositionId]
        from    [BillItem]
        where   [BillId] = @BillId
        order by [BillItemTypeId] asc, [AssetAssignmentId] asc";

    public List<BillItemDataObject> GetBillItems(int billId, bool loadOnlyItems)
    {
        using (SqlConnection conn = SqlHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            return GetBillItems(tran, billId, loadOnlyItems);
        }
    }

    public List<BillItemDataObject> GetBillItems(SqlTransaction tran, int billId, bool loadOnlyItems)
    {
        List<BillItemDataObject> result = new List<BillItemDataObject>();

        DbParameter billIdParam = new SqlParameter("@BillId", billId);
        DbParameter[] parms = new DbParameter[1] { billIdParam };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_BILL_ID, parms))
        {
            while (dataReader.Read())
            {
                BillItemDataObject billItemInfo = new BillItemDataObject();

                billItemInfo.BillItemId = (int)dataReader.GetValue(0);
                billItemInfo.BillItemTypeId = (int)dataReader.GetValue(1);
                billItemInfo.BillId = (int)dataReader.GetValue(2);
                billItemInfo.AssetAssignmentId = (int)dataReader.GetValue(3);
                billItemInfo.BillingDate = (String)dataReader.GetValue(4);
                billItemInfo.Qty = (int)dataReader.GetValue(5);
                billItemInfo.BillRate = dataReader.GetDecimal(6);
                billItemInfo.Status = (String)dataReader.GetValue(7);
                if (!dataReader.IsDBNull(8))
                {
                    billItemInfo.BillItemCompositionId = (int)dataReader.GetValue(8);
                }
                else
                {
                    billItemInfo.BillItemCompositionId = 0;
                }

                if (!loadOnlyItems)
                {
                    billItemInfo.WorkLogInfo = WorkLog.GetInstance().getWorkLog(tran, billItemInfo.BillItemId);
                    if (null != billItemInfo.WorkLogInfo)
                    {
                        billItemInfo.WorkLogInfo.BillItemInfo = billItemInfo;
                    }

                    billItemInfo.AttachmentInfo = BillItemAttachment.GetInstance().GetAttachment(tran, billItemInfo.BillItemId);
                    if (null != billItemInfo.AttachmentInfo)
                    {
                        billItemInfo.AttachmentInfo.BillItemInfo = billItemInfo;
                    }

                    billItemInfo.Notes = Note.GetInstance().GetNotes(tran, billItemInfo.BillItemId, NoteDataObject.BILL_ITEM_NOTE_TYPE);
                }

                result.Add(billItemInfo);
            }
        }

        return result;
    }

    private const string SQL_SELECT_BY_ID = @"
        select  [BillItemId],
                [BillItemTypeId],
                [BillId],
                [AssetAssignmentId],
                [BillingDate],
                [Qty],
                [BillRate],
                [Status],
                [BillItemCompositionId]
        from    [BillItem]
        where   [BillItemId] = @BillItemId";

    public BillItemDataObject GetBillItem(SqlTransaction tran,  int billItemId)
    {
        BillItemDataObject result = null;

        DbParameter billItemIdParam = new SqlParameter("@BillItemId", billItemId);
        DbParameter[] parms = new DbParameter[1] { billItemIdParam };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
        {
            if (dataReader.Read())
            {
                result = new BillItemDataObject();

                result.BillItemId = (int)dataReader.GetValue(0);
                result.BillItemTypeId = (int)dataReader.GetValue(1);
                result.BillId = (int)dataReader.GetValue(2);
                result.AssetAssignmentId = (int)dataReader.GetValue(3);
                result.BillingDate = (String)dataReader.GetValue(4);
                result.Qty = (int)dataReader.GetValue(5);
                result.BillRate = dataReader.GetDecimal(6);
                result.Status = (String)dataReader.GetValue(7);

                if (!dataReader.IsDBNull(8))
                {
                    result.BillItemCompositionId = (int)dataReader.GetValue(8);
                }
                else
                {
                    result.BillItemCompositionId = 0;
                }
            }

            return result;
        }
    }

    private const String SQL_SELECT_FOR_INVOICE = @"
        select  b.[BillItemId],
                b.[BillItemTypeId],
                b.[BillId],
                b.[AssetAssignmentId],
                b.[BillingDate],
                b.[Qty],
                b.[BillRate],
                b.[Status],
                b.[BillItemCompositionId]
        from    [BillItem] b
                inner join [AssetAssignment] aa
                        on aa.[AssetAssignmentId] = b.[AssetAssignmentId]
                inner join [Afe]
                        on afe.[AFE] = aa.[AFE]
                 left join [InvoiceItem] i
                        on b.[BillItemId] = i.[BillItemId]
                       and i.[Status] <> 'VOID'
        where   b.[Status] = 'CONFIRMED' 
        and     i.[BillItemId] is null
        and     afe.[ClientId] = @ClientId
        and     cast(substring(b.[BillingDate], 7, 4) as int) = @Year
        and     cast(substring(b.[BillingDate], 1, 2) as int) = @Month";

    public List<BillItemDataObject> GetBillItemsForInvoice(SqlTransaction tran,
            int year, int month, bool isFirstPart, int clientId, int assetId)
    {
        List<BillItemDataObject> result = new List<BillItemDataObject>();
        string query = SQL_SELECT_FOR_INVOICE;

        if (isFirstPart)
        {
            query += " and cast(substring(b.[BillingDate], 4, 2) as int) < 16 ";
        }
        else
        {
            query += " and cast(substring(b.[BillingDate], 4, 2) as int) > 15 ";
        }

        if (assetId != 0)
        {
            query += " and aa.[AssetId] = ";
            query += assetId.ToString();
        }

        DbParameter[] parms = new DbParameter[3] {
            new SqlParameter("@ClientId", clientId),
            new SqlParameter("@Year", year),
            new SqlParameter("@Month", month)
        };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, query, parms))
        {
            while (dataReader.Read())
            {
                BillItemDataObject billItemInfo = new BillItemDataObject();

                billItemInfo.BillItemId = (int)dataReader.GetValue(0);
                billItemInfo.BillItemTypeId = (int)dataReader.GetValue(1);
                billItemInfo.BillId = (int)dataReader.GetValue(2);
                billItemInfo.AssetAssignmentId = (int)dataReader.GetValue(3);
                billItemInfo.BillingDate = (String)dataReader.GetValue(4);
                billItemInfo.Qty = (int)dataReader.GetValue(5);
                billItemInfo.BillRate = dataReader.GetDecimal(6);
                billItemInfo.Status = (String)dataReader.GetValue(7);
                if (!dataReader.IsDBNull(8))
                {
                    billItemInfo.BillItemCompositionId = (int)dataReader.GetValue(8);
                }
                else
                {
                    billItemInfo.BillItemCompositionId = 0;
                }

                result.Add(billItemInfo);
            }
        }

        return result;
    }

}

}
