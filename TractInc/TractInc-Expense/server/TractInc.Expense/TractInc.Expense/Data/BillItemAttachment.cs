using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class BillItemAttachment
    {

        private static BillItemAttachment c_Attachment = new BillItemAttachment();

        public static BillItemAttachment GetInstance()
        {
            return c_Attachment;
        }

        private BillItemAttachment()
        {
        }

        private const string SQL_INSERT = @"
        insert  into [BillItemAttachment]
              ( [BillItemId],
                [FileName],
                [OriginalFileName] )
        values( @BillItemId,
                @FileName,
                @OriginalFileName );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, BillItemAttachmentDataObject attachmentInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@BillItemId", attachmentInfo.BillItemId),
                new SqlParameter("@FileName", attachmentInfo.FileName),
                new SqlParameter("@OriginalFileName", attachmentInfo.OriginalFileName)
            };

            attachmentInfo.BillItemAttachmentId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [BillItemAttachment]
        where   BillItemId = @BillItemId";

        public void Remove(SqlTransaction tran, int billItemId)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@BillItemId", billItemId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_SELECT_BY_BILL_ITEM_ID = @"
        select  [BillItemAttachmentId],
                [BillItemId],
                [FileName],
                [OriginalFileName]
        from    [BillItemAttachment]
        where   [BillItemId] = @BillItemId";

        public BillItemAttachmentDataObject GetAttachment(SqlTransaction tran, int billItemId)
        {
            BillItemAttachmentDataObject result = null;

            DbParameter billItemIdParam = new SqlParameter("@BillItemId", billItemId);
            DbParameter[] parms = new DbParameter[1] { billItemIdParam };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_BILL_ITEM_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new BillItemAttachmentDataObject();

                    result.BillItemAttachmentId = (int)dataReader.GetValue(0);
                    result.BillItemId = (int)dataReader.GetValue(1);
                    result.FileName = (string)dataReader.GetValue(2);
                    result.OriginalFileName = (string)dataReader.GetValue(3);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_COMPOSITION_ID = @"
        select  bia.[BillItemAttachmentId],
                bia.[BillItemId],
                bia.[FileName],
                bia.[OriginalFileName]
        from    [BillItemAttachment] bia
                inner join [BillItem] bi
                        on bi.[BillItemId] = bia.[BillItemId]
        where   bi.[BillItemCompositionId] = @BillItemCompositionId";

        public BillItemAttachmentDataObject GetCompositionAttachment(SqlTransaction tran, int compositionId)
        {
            BillItemAttachmentDataObject result = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@BillItemCompositionId", compositionId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_COMPOSITION_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new BillItemAttachmentDataObject();

                    result.BillItemAttachmentId = (int)dataReader.GetValue(0);
                    result.BillItemId = (int)dataReader.GetValue(1);
                    result.FileName = (string)dataReader.GetValue(2);
                    result.OriginalFileName = (string)dataReader.GetValue(3);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_BILL_ID = @"
        select  bia.[BillItemAttachmentId],
                bia.[BillItemId],
                bia.[FileName],
                bia.[OriginalFileName]
        from    [BillItemAttachment] bia
                inner join [BillItem] bi
                        on bi.[BillItemId] = bia.[BillItemId]
        where   bi.[BillId] = @BillId";

        public List<BillItemAttachmentDataObject> GetBillAttachments(SqlTransaction tran, int billId)
        {
            List<BillItemAttachmentDataObject> result = new List<BillItemAttachmentDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@BillId", billId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_BILL_ID, parms))
            {
                while (dataReader.Read())
                {
                    BillItemAttachmentDataObject attachmentInfo = new BillItemAttachmentDataObject();

                    attachmentInfo.BillItemAttachmentId = (int)dataReader.GetValue(0);
                    attachmentInfo.BillItemId = (int)dataReader.GetValue(1);
                    attachmentInfo.FileName = (string)dataReader.GetValue(2);
                    attachmentInfo.OriginalFileName = (string)dataReader.GetValue(3);

                    attachmentInfo.BillItemInfo = BillItem.GetInstance().GetBillItem(tran, attachmentInfo.BillItemId);
                    if (0 != attachmentInfo.BillItemInfo.BillItemCompositionId)
                    {
                        attachmentInfo.CompositionInfo = BillItemComposition.GetInstance().GetBillComposition(tran, attachmentInfo.BillItemInfo.BillItemCompositionId);
                    }

                    result.Add(attachmentInfo);
                }
            }

            return result;
        }

    }

}
