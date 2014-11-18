using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class BillItemComposition
    {

        private static BillItemComposition c_Composition = new BillItemComposition();

        public static BillItemComposition GetInstance()
        {
            return c_Composition;
        }

        private BillItemComposition()
        {
        }

        private const string SQL_INSERT = @"
        insert  into [BillItemComposition]
              ( [BillId],
                [BillItemTypeId],
                [Amount],
                [Description] )
        values( @BillId,
                @BillItemTypeId,
                @Amount,
                @Description );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, BillItemCompositionDataObject compositionInfo)
        {
            DbParameter[] parms = new DbParameter[4] {
                new SqlParameter("@BillId", compositionInfo.BillId),
                new SqlParameter("@BillItemTypeId", compositionInfo.BillItemTypeId),
                new SqlParameter("@Amount", compositionInfo.Amount),
                new SqlParameter("@Description", compositionInfo.Description)
            };

            compositionInfo.BillItemCompositionId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [BillItemComposition]
        set     [BillItemTypeId] = @BillItemTypeId,
                [Amount] = @Amount,
                [Description] = @Description
        where   [BillItemCompositionId] = @BillItemCompositionId";

        public void Update(SqlTransaction tran, BillItemCompositionDataObject compositionInfo)
        {
            DbParameter[] parms = new DbParameter[4] {
                new SqlParameter("@BillItemTypeId", compositionInfo.BillItemTypeId),
                new SqlParameter("@Amount", compositionInfo.Amount),
                new SqlParameter("@Description", compositionInfo.Description),
                new SqlParameter("@BillItemCompositionId", compositionInfo.BillItemCompositionId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        public List<BillItemCompositionDataObject> StoreCompositions(SqlTransaction tran, List<BillItemCompositionDataObject> compositions)
        {
            List<BillItemCompositionDataObject> updatedCompositions = new List<BillItemCompositionDataObject>();

            foreach (BillItemCompositionDataObject compositionInfo in compositions)
            {
                updatedCompositions.Add(StoreComposition(tran, compositionInfo));
            }

            tran.Commit();

            return updatedCompositions;
        }

        public BillItemCompositionDataObject StoreComposition(SqlTransaction tran, BillItemCompositionDataObject compositionInfo)
        {
            if (0 == compositionInfo.BillItemCompositionId)
            {
                Insert(tran, compositionInfo);

                foreach (BillItemDataObject itemInfo in compositionInfo.BillItems)
                {
                    itemInfo.BillItemCompositionId = compositionInfo.BillItemCompositionId;
                }
            }
            else
            {
                BillItem.GetInstance().Remove(tran, compositionInfo.BillItemCompositionId);

                foreach (BillItemDataObject itemInfo in compositionInfo.BillItems)
                {
                    if (BillItemDataObject.BILL_ITEM_STATUS_REJECTED == itemInfo.Status)
                    {
                        itemInfo.Status = BillItemDataObject.BILL_ITEM_STATUS_CHANGED;
                    }
                }

                Update(tran, compositionInfo);
            }

            if (null != compositionInfo.AttachmentInfo)
            {
                foreach (BillItemDataObject itemInfo in compositionInfo.BillItems)
                {
                    itemInfo.AttachmentInfo = new BillItemAttachmentDataObject();
                    itemInfo.AttachmentInfo.FileName = compositionInfo.AttachmentInfo.FileName;
                    itemInfo.AttachmentInfo.OriginalFileName = compositionInfo.AttachmentInfo.OriginalFileName;
                    itemInfo.AttachmentInfo.BillItemAttachmentId = compositionInfo.AttachmentInfo.BillItemAttachmentId;
                    itemInfo.AttachmentInfo.BillItemId = itemInfo.BillItemId;
                    itemInfo.AttachmentInfo.IsDeleted = compositionInfo.AttachmentInfo.IsDeleted;
                }
            }

            compositionInfo.BillInfo = Bill.GetInstance().StoreBillItems(tran, Bill.GetInstance().GetBill(tran, compositionInfo.BillId), compositionInfo.BillItems);

            if (null != compositionInfo.Notes)
            {
                foreach (NoteDataObject noteInfo in compositionInfo.Notes)
                {
                    if (0 == noteInfo.NoteId)
                    {
                        Note.GetInstance().Insert(tran, noteInfo);
                    }
                }
            }

            return compositionInfo;
        }

        private const string SQL_REMOVE = @"
        delete  from [BillItemComposition]
        where   BillItemCompositionId = @BillItemCompositionId";

        public void Remove(SqlTransaction tran, int compositionId)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@BillItemCompositionId", compositionId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_SELECT_BY_BILL_ID = @"
        select  [BillItemCompositionId],
                [BillId],
                [BillItemTypeId],
                [Amount],
                [Description]
        from    [BillItemComposition]
        where   [BillId] = @BillId";

        public List<BillItemCompositionDataObject> GetBillCompositions(SqlTransaction tran, int billId)
        {
            List<BillItemCompositionDataObject> result = new List<BillItemCompositionDataObject>();

            DbParameter billIdParam = new SqlParameter("@BillId", billId);
            DbParameter[] parms = new DbParameter[1] { billIdParam };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_BILL_ID, parms))
            {
                while (dataReader.Read())
                {
                    BillItemCompositionDataObject compositionInfo = new BillItemCompositionDataObject();

                    compositionInfo.BillItemCompositionId = (int)dataReader.GetValue(0);
                    compositionInfo.BillId = (int)dataReader.GetValue(1);
                    compositionInfo.BillItemTypeId = (int)dataReader.GetValue(2);
                    compositionInfo.Amount = (decimal)dataReader.GetValue(3);
                    compositionInfo.Description = (String)dataReader.GetValue(4);

                    compositionInfo.AttachmentInfo = BillItemAttachment.GetInstance().GetCompositionAttachment(tran, compositionInfo.BillItemCompositionId);

                    compositionInfo.Notes = Note.GetInstance().GetNotes(tran, compositionInfo.BillItemCompositionId, NoteDataObject.NOTE_TYPE_MULTIDAY_ITEM);

                    result.Add(compositionInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_ID = @"
        select  [BillItemCompositionId],
                [BillId],
                [BillItemTypeId],
                [Amount],
                [Description]
        from    [BillItemComposition]
        where   [BillItemCompositionId] = @BillItemCompositionId";

        public BillItemCompositionDataObject GetBillComposition(SqlTransaction tran, int compositionId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@BillItemCompositionId", compositionId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    BillItemCompositionDataObject compositionInfo = new BillItemCompositionDataObject();

                    compositionInfo.BillItemCompositionId = (int)dataReader.GetValue(0);
                    compositionInfo.BillId = (int)dataReader.GetValue(1);
                    compositionInfo.BillItemTypeId = (int)dataReader.GetValue(2);
                    compositionInfo.Amount = (decimal)dataReader.GetValue(3);
                    compositionInfo.Description = (String)dataReader.GetValue(4);

                    return compositionInfo;
                }
            }

            return null;
        }

    }

}
