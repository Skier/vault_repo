using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

public class Bill
{

    private static Bill c_Bill = new Bill();

    public static Bill GetInstance()
    {
        return c_Bill;
    }

    private Bill()
    {
    }

    private const string SQL_UPDATE = @"
        update  [Bill]
        set     [Status]                = @Status,
                [TotalDailyBill]        = @TotalDailyBill,
                [DailyBillAmt]          = @DailyBillAmt,
                [OtherBillAmt]          = @OtherBillAmt,
                [TotalBillAmt]          = @TotalBillAmt
        where   [BillId] = @BillId";

    public void Update(SqlTransaction tran, BillDataObject billInfo)
    {
        DbParameter[] parms = new DbParameter[6] {
            new SqlParameter("@Status",                 billInfo.Status),
            new SqlParameter("@TotalDailyBill",         billInfo.TotalDailyBill),
            new SqlParameter("@DailyBillAmt",           billInfo.DailyBillAmt),
            new SqlParameter("@OtherBillAmt",           billInfo.OtherBillAmt),
            new SqlParameter("@TotalBillAmt",           billInfo.TotalBillAmt),
            new SqlParameter("@BillId",                 billInfo.BillId)
        };

        SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
    }

    private const string SQL_SELECT_BY_ID = @"
            select  [BillId],
                    [Status],
                    [StartDate],
                    [AssetId],
                    [TotalDailyBill],
                    [DailyBillAmt],
                    [OtherBillAmt],
                    [TotalBillAmt]
            from    [Bill]
            where   BillId = @BillId";

    public BillDataObject GetBill(SqlTransaction tran, int billId, bool loadOnlyItems)
    {
        BillDataObject result = GetBill(tran, billId);

        if (null == result)
        {
            return null;
        }

        result.BillItems = BillItem.GetInstance().GetBillItems(tran, billId, loadOnlyItems);

        result.Compositions = BillItemComposition.GetInstance().GetBillCompositions(tran, billId);

        result.Users = User.GetInstance().GetUsers(tran);

        result.Notes = Note.GetInstance().GetNotes(tran, billId, NoteDataObject.BILL_NOTE_TYPE);

        result.AssetInfo = Asset.GetInstance().GetAsset(tran, result.AssetId);

        return result;
    }

    public BillDataObject GetBill(SqlTransaction tran, int billId)
    {
        BillDataObject result = null;

        DbParameter billIdParam = new SqlParameter("@BillId", billId);
        DbParameter[] parms = new DbParameter[1] { billIdParam };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
        {
            if (dataReader.Read())
            {
                result = new BillDataObject();

                result.BillId = (int)dataReader.GetValue(0);
                result.Status = (String)dataReader.GetValue(1);
                result.StartDate = (String)dataReader.GetValue(2);
                result.AssetId = (int)dataReader.GetValue(3);
                result.TotalDailyBill = (int)dataReader.GetValue(4);
                result.DailyBillAmt = dataReader.GetDecimal(5);
                result.OtherBillAmt = dataReader.GetDecimal(6);
                result.TotalBillAmt = dataReader.GetDecimal(7);
            }
        }

        return result;
    }

    private const string SQL_SELECT_BY_ASSET_ID = @"
            select  [BillId],
                    [Status],
                    [StartDate],
                    [AssetId],
                    [TotalDailyBill],
                    [DailyBillAmt],
                    [OtherBillAmt],
                    [TotalBillAmt],
                    year([StartDate]) as [StartYear],
                    month([StartDate]) as [StartMonth],
                    day([StartDate]) as [StartDay]
            from    [Bill]
            where   AssetId = @AssetId
            order   by StartYear desc, StartMonth desc, StartDay desc";

    public List<BillDataObject> GetBills(SqlTransaction tran, int assetId)
    {
        List<BillDataObject> result = new List<BillDataObject>();

        DbParameter assetIdParam = new SqlParameter("@AssetId", assetId);
        DbParameter[] parms = new DbParameter[1] { assetIdParam };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ASSET_ID, parms))
        {
            while (dataReader.Read())
            {
                BillDataObject billInfo = new BillDataObject();

                billInfo.BillId = (int)dataReader.GetValue(0);
                billInfo.Status = (String)dataReader.GetValue(1);
                billInfo.StartDate = (String)dataReader.GetValue(2);
                billInfo.AssetId = (int)dataReader.GetValue(3);
                billInfo.TotalDailyBill = (int)dataReader.GetValue(4);
                billInfo.DailyBillAmt = dataReader.GetDecimal(5);
                billInfo.OtherBillAmt = dataReader.GetDecimal(6);
                billInfo.TotalBillAmt = dataReader.GetDecimal(7);

                billInfo.AssetInfo = Asset.GetInstance().GetAsset(tran, assetId);

                billInfo.Notes = Note.GetInstance().GetNotes(tran, billInfo.BillId, NoteDataObject.BILL_NOTE_TYPE);

                result.Add(billInfo);
            }
        }

        return result;
    }

    public BillDataObject UpdateBillStatus(SqlTransaction tran, int billId)
    {
        BillDataObject billInfo = Bill.GetInstance().GetBill(tran, billId, true);

        return UpdateBillStatus(tran, billInfo);
    }

    public BillDataObject StoreBillItems(SqlTransaction tran, BillDataObject bill, List<BillItemDataObject> billItems)
    {
        foreach (BillItemDataObject billItemInfo in billItems)
        {
            if (0 == billItemInfo.BillItemId)
            {
                BillItem.GetInstance().Insert(tran, billItemInfo);
            }
            else if (billItemInfo.IsMarkedToRemove)
            {
                BillItem.GetInstance().Remove(tran, billItemInfo);
                continue;
            }
            else
            {
                if ((BillItemDataObject.BILL_ITEM_STATUS_REJECTED == billItemInfo.Status)
                        && (0 == billItemInfo.BillItemCompositionId))
                {
                    billItemInfo.Status = BillItemDataObject.BILL_ITEM_STATUS_CHANGED;
                }

                BillItem.GetInstance().Update(tran, billItemInfo);
            }

            if (null != billItemInfo.WorkLogInfo)
            {
                if (0 == billItemInfo.WorkLogInfo.WorkLogId)
                {
                    billItemInfo.WorkLogInfo.BillItemId = billItemInfo.BillItemId;
                    WorkLog.GetInstance().Insert(tran, billItemInfo.WorkLogInfo);
                }
                else
                {
                    WorkLog.GetInstance().Update(tran, billItemInfo.WorkLogInfo);
                }
            }

            if (null != billItemInfo.AttachmentInfo)
            {
                BillItemAttachment.GetInstance().Remove(tran, billItemInfo.AttachmentInfo.BillItemId);
                if (!billItemInfo.AttachmentInfo.IsDeleted)
                {
                    billItemInfo.AttachmentInfo.BillItemId = billItemInfo.BillItemId;
                    BillItemAttachment.GetInstance().Insert(tran, billItemInfo.AttachmentInfo);
                }
                else
                {
                    billItemInfo.AttachmentInfo = null;
                }
            }

            if (null != billItemInfo.Notes)
            {
                foreach (NoteDataObject noteInfo in billItemInfo.Notes)
                {
                    if (0 == noteInfo.NoteId)
                    {
                        noteInfo.RelatedItemId = billItemInfo.BillItemId;
                        Note.GetInstance().Insert(tran, noteInfo);
                    }
                }
            }
        }
                
        UpdateBillStatus(tran, bill);

        List<BillItemDataObject> updatedItemsList = new List<BillItemDataObject>();

        foreach(BillItemDataObject updatedItem in billItems)
        {
            if (updatedItem.IsMarkedToRemove)
            {
                continue;
            }

            updatedItemsList.Add(updatedItem);
        }

        bill.BillItems = updatedItemsList;

        return bill;
    }

    public BillDataObject UpdateBillStatus(SqlTransaction tran, BillDataObject billInfo)
    {
        billInfo.BillItems = BillItem.GetInstance().GetBillItems(tran, billInfo.BillId, true);

        bool hasChanged = false;
        bool hasRejected = false;
        bool hasOnlyConfirmed = true;

        billInfo.DailyBillAmt = 0;
        billInfo.OtherBillAmt = 0;
        billInfo.TotalBillAmt = 0;
        billInfo.TotalDailyBill = 0;

        foreach (BillItemDataObject item in billInfo.BillItems)
        {
            if (item.IsMarkedToRemove)
            {
                continue;
            }

            decimal amount = (decimal)(item.Qty * item.BillRate);

            if (1 == item.BillItemTypeId)
            {
                billInfo.TotalDailyBill += (int)item.Qty;
                billInfo.DailyBillAmt += amount;
            }
            else
            {
                billInfo.OtherBillAmt += amount;
            }

            billInfo.TotalBillAmt += amount;

            if (BillItemDataObject.BILL_ITEM_STATUS_CHANGED == item.Status)
            {
                hasChanged = true;
            }
            if (BillItemDataObject.BILL_ITEM_STATUS_REJECTED == item.Status)
            {
                hasRejected = true;
            }
            if ((BillItemDataObject.BILL_ITEM_STATUS_CONFIRMED != item.Status)
                    && (BillItemDataObject.BILL_ITEM_STATUS_APPROVED != item.Status))
            {
                hasOnlyConfirmed = false;
            }
        }

        if (!hasRejected && (BillDataObject.BILL_STATUS_NEW != billInfo.Status)
                && (hasChanged
                    || (hasOnlyConfirmed && BillDataObject.BILL_STATUS_CHANGED != billInfo.Status)
                    || (BillDataObject.BILL_STATUS_REJECTED == billInfo.Status)))
        {
            billInfo.Status = BillDataObject.BILL_STATUS_CHANGED;
        }

        Update(tran, billInfo);

        return billInfo;
    }

    private const string SQL_SELECT_CURRENT = @"
            select  [BillId],
                    [Status],
                    [StartDate],
                    [AssetId],
                    [TotalDailyBill],
                    [DailyBillAmt],
                    [OtherBillAmt],
                    [TotalBillAmt],
                    year([StartDate]) as [StartYear],
                    month([StartDate]) as [StartMonth],
                    day([StartDate]) as [StartDay]
            from    [Bill]
            where   Status <> 'CONFIRMED'
            order   by StartYear desc, StartMonth desc, StartDay desc";

    public List<BillDataObject> GetCurrentBills(SqlTransaction tran)
    {
        List<BillDataObject> result = new List<BillDataObject>();

        DbParameter[] parms = new DbParameter[0] { };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CURRENT, parms))
        {
            while (dataReader.Read())
            {
                BillDataObject billInfo = new BillDataObject();

                billInfo.BillId = (int)dataReader.GetValue(0);
                billInfo.Status = (String)dataReader.GetValue(1);
                billInfo.StartDate = (String)dataReader.GetValue(2);
                billInfo.AssetId = (int)dataReader.GetValue(3);
                billInfo.TotalDailyBill = (int)dataReader.GetValue(4);
                billInfo.DailyBillAmt = dataReader.GetDecimal(5);
                billInfo.OtherBillAmt = dataReader.GetDecimal(6);
                billInfo.TotalBillAmt = dataReader.GetDecimal(7);

                billInfo.Notes = Note.GetInstance().GetNotes(tran, billInfo.BillId, NoteDataObject.BILL_NOTE_TYPE);

                billInfo.AssetInfo = Asset.GetInstance().GetAsset(tran, billInfo.AssetId);

                result.Add(billInfo);
            }
        }

        return result;
    }

    private const string SQL_SELECT_CREW = @"
            select  b.[BillId],
                    b.[Status],
                    b.[StartDate],
                    b.[AssetId],
                    b.[TotalDailyBill],
                    b.[DailyBillAmt],
                    b.[OtherBillAmt],
                    b.[TotalBillAmt],
                    year([StartDate]) as [StartYear],
                    month([StartDate]) as [StartMonth],
                    day([StartDate]) as [StartDay]
            from    [Bill] b
                    inner join [Asset] a
                            on b.[AssetId] = a.[AssetId]
            where   b.[Status] <> 'CONFIRMED'
            and     a.[ChiefAssetId] = @ChiefAssetId
            order   by StartYear desc, StartMonth desc, StartDay desc";

    public List<BillDataObject> GetCrewBills(SqlTransaction tran, int chiefAssetId)
    {
        List<BillDataObject> result = new List<BillDataObject>();

        DbParameter[] parms = new DbParameter[1] {
            new SqlParameter("@ChiefAssetId", chiefAssetId)
        };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CREW, parms))
        {
            while (dataReader.Read())
            {
                BillDataObject billInfo = new BillDataObject();

                billInfo.BillId = (int)dataReader.GetValue(0);
                billInfo.Status = (String)dataReader.GetValue(1);
                billInfo.StartDate = (String)dataReader.GetValue(2);
                billInfo.AssetId = (int)dataReader.GetValue(3);
                billInfo.TotalDailyBill = (int)dataReader.GetValue(4);
                billInfo.DailyBillAmt = dataReader.GetDecimal(5);
                billInfo.OtherBillAmt = dataReader.GetDecimal(6);
                billInfo.TotalBillAmt = dataReader.GetDecimal(7);

                billInfo.Notes = Note.GetInstance().GetNotes(tran, billInfo.BillId, NoteDataObject.BILL_NOTE_TYPE);

                billInfo.AssetInfo = Asset.GetInstance().GetAsset(tran, billInfo.AssetId);

                result.Add(billInfo);
            }
        }

        return result;
    }

    private const string SQL_SELECT_CREW_OLD = @"
            select  b.[BillId],
                    b.[Status],
                    b.[StartDate],
                    b.[AssetId],
                    b.[TotalDailyBill],
                    b.[DailyBillAmt],
                    b.[OtherBillAmt],
                    b.[TotalBillAmt],
                    year([StartDate]) as [StartYear],
                    month([StartDate]) as [StartMonth],
                    day([StartDate]) as [StartDay]
            from    [Bill] b
                    inner join [Asset] a
                            on b.[AssetId] = a.[AssetId]
            where   b.[Status] = 'CONFIRMED'
            and     b.[StartDate] = @StartDate
            and     (a.[ChiefAssetId] = @ChiefAssetId
                    or @ChiefAssetId = 0)
            order   by StartYear desc, StartMonth desc, StartDay desc";

    public List<BillDataObject> GetCrewBills(SqlTransaction tran, int chiefAssetId, string startDate)
    {
        List<BillDataObject> result = new List<BillDataObject>();

        DbParameter[] parms = new DbParameter[2] {
            new SqlParameter("@ChiefAssetId", chiefAssetId),
            new SqlParameter("@StartDate", startDate)
        };

        using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CREW_OLD, parms))
        {
            while (dataReader.Read())
            {
                BillDataObject billInfo = new BillDataObject();

                billInfo.BillId = (int)dataReader.GetValue(0);
                billInfo.Status = (String)dataReader.GetValue(1);
                billInfo.StartDate = (String)dataReader.GetValue(2);
                billInfo.AssetId = (int)dataReader.GetValue(3);
                billInfo.TotalDailyBill = (int)dataReader.GetValue(4);
                billInfo.DailyBillAmt = dataReader.GetDecimal(5);
                billInfo.OtherBillAmt = dataReader.GetDecimal(6);
                billInfo.TotalBillAmt = dataReader.GetDecimal(7);

                billInfo.Notes = Note.GetInstance().GetNotes(tran, billInfo.BillId, NoteDataObject.BILL_NOTE_TYPE);

                billInfo.AssetInfo = Asset.GetInstance().GetAsset(tran, billInfo.AssetId);

                result.Add(billInfo);
            }
        }

        return result;
    }

    private const string SQL_INSERT = @"
        insert  into [Bill]
              ( [Status],
                [StartDate],
                [AssetId],
                [TotalDailyBill],
                [DailyBillAmt],
                [OtherBillAmt],
                [TotalBillAmt],
                [Notes])
        values( @Status,
                @StartDate,
                @AssetId,
                @TotalDailyBill,
                @DailyBillAmt,
                @OtherBillAmt,
                @TotalBillAmt,
                '');
        select  cast(scope_identity() as int)";

    public void Insert(SqlTransaction tran, BillDataObject billInfo)
    {
        DbParameter[] parms = new DbParameter[7] {
                new SqlParameter("@Status",         billInfo.Status),
                new SqlParameter("@StartDate",      billInfo.StartDate),
                new SqlParameter("@AssetId",        billInfo.AssetId),
                new SqlParameter("@TotalDailyBill", billInfo.TotalDailyBill),
                new SqlParameter("@DailyBillAmt",   billInfo.DailyBillAmt),
                new SqlParameter("@OtherBillAmt",   billInfo.OtherBillAmt),
                new SqlParameter("@TotalBillAmt",   billInfo.TotalBillAmt)
            };

        billInfo.BillId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
    }

}

}
