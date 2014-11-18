using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class ReportAttachment
    {

        private static ReportAttachment c_Attachment = new ReportAttachment();

        public static ReportAttachment GetInstance()
        {
            return c_Attachment;
        }

        private ReportAttachment()
        {
        }

        private const string SQL_SELECT_BY_INVOICE_ID = @"
        select  bia.[BillItemAttachmentId],
                bia.[BillItemId],
                bia.[FileName],
                bia.[OriginalFileName],
                bi.[BillItemCompositionId],
                aa.[AFE],
                aa.[SubAFE],
                a.[BusinessName]
        from    [BillItemAttachment] bia
                inner join [BillItem] bi
                        on bi.[BillItemId] = bia.[BillItemId]
                inner join [InvoiceItem] ii
                        on ii.[BillItemId] = bi.[BillItemId]
                inner join [AssetAssignment] aa
                        on aa.[AssetAssignmentId] = bi.[AssetAssignmentId]
                inner join [Asset] a
                        on a.[AssetId] = aa.[AssetId]
        where   ii.[InvoiceId] = @InvoiceId
        and     ii.[IsSelected] = 1
        order   by aa.[AFE] asc,
                aa.[SubAFE] asc,
                a.[BusinessName] asc";

        public List<ReportAttachmentDataObject> GetInvoiceAttachments(SqlTransaction tran, int invoiceId)
        {
            List<ReportAttachmentDataObject> result = new List<ReportAttachmentDataObject>();
            Hashtable attachmentsHash = new Hashtable();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@InvoiceId", invoiceId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_INVOICE_ID, parms))
            {
                while (dataReader.Read())
                {
                    ReportAttachmentDataObject attachmentInfo = new ReportAttachmentDataObject();

                    attachmentInfo.BillItemAttachmentId = (int)dataReader.GetValue(0);
                    attachmentInfo.BillItemId = (int)dataReader.GetValue(1);
                    attachmentInfo.FileName = (string)dataReader.GetValue(2);
                    attachmentInfo.OriginalFileName = (string)dataReader.GetValue(3);
                    attachmentInfo.AFE = (string)dataReader.GetValue(5);
                    attachmentInfo.Project = (string)dataReader.GetValue(6);
                    attachmentInfo.Landman = (string)dataReader.GetValue(7);

                    if (dataReader.IsDBNull(4))
                    {
                        attachmentInfo.BillItemCompositionId = 0;
                    }
                    else
                    {
                        attachmentInfo.BillItemCompositionId = (int)dataReader.GetValue(4);
                    }

                    if (0 == attachmentInfo.BillItemCompositionId)
                    {
                        result.Add(attachmentInfo);
                    }
                    else if (null == attachmentsHash[attachmentInfo.BillItemCompositionId])
                    {
                        result.Add(attachmentInfo);
                    }
                }
            }

            return result;
        }

    }

}
