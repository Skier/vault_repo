using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class InvoiceItem
    {

        private static InvoiceItem c_InvoiceItem = new InvoiceItem();
        
        public static InvoiceItem GetInstance()
        {
            return c_InvoiceItem;
        }

        private InvoiceItem()
        {
        }

        private const string SQL_SELECT_BY_INVOICE_ID = @"
        select  ii.[InvoiceItemId],
                ii.[InvoiceItemTypeId],
                ii.[InvoiceId],
                ii.[BillItemId],
                ii.[AssetAssignmentId],
                ii.[InvoiceDate],
                ii.[Qty],
                ii.[InvoiceRate],
                ii.[Status],
                ii.[IsSelected],
                aa.[AssetId]
        from    [InvoiceItem] ii
                inner join [AssetAssignment] aa
                        on aa.[AssetAssignmentId] = ii.[AssetAssignmentId]
        where   ii.[InvoiceId] = @InvoiceId
        order by ii.[InvoiceItemTypeId] asc, ii.[AssetAssignmentId] asc";

        public List<InvoiceItemDataObject> GetInvoiceItems(SqlTransaction tran, int invoiceId)
        {
            List<InvoiceItemDataObject> result = new List<InvoiceItemDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@InvoiceId", invoiceId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_INVOICE_ID, parms))
            {
                while (dataReader.Read())
                {
                    InvoiceItemDataObject invoiceItemInfo = new InvoiceItemDataObject();

                    invoiceItemInfo.InvoiceItemId = (int)dataReader.GetValue(0);
                    invoiceItemInfo.InvoiceItemTypeId = (int)dataReader.GetValue(1);
                    invoiceItemInfo.InvoiceId = (int)dataReader.GetValue(2);
                    if (dataReader.IsDBNull(3))
                    {
                        invoiceItemInfo.BillItemId = 0;
                    }
                    else
                    {
                        invoiceItemInfo.BillItemId = (int)dataReader.GetValue(3);
                    }
                    invoiceItemInfo.AssetAssignmentId = (int)dataReader.GetValue(4);
                    invoiceItemInfo.InvoiceDate = (String)dataReader.GetValue(5);
                    invoiceItemInfo.Qty = (int)dataReader.GetValue(6);
                    invoiceItemInfo.InvoiceRate = dataReader.GetDecimal(7);
                    invoiceItemInfo.Status = (String)dataReader.GetValue(8);
                    invoiceItemInfo.IsSelected = (bool)dataReader.GetValue(9);
                    invoiceItemInfo.AssetId = (int)dataReader.GetValue(10);

                    result.Add(invoiceItemInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [InvoiceItem]
              ( [InvoiceItemTypeId],
                [InvoiceId],
                [BillItemId],
                [AssetAssignmentId],
                [InvoiceDate],
                [Qty],
                [InvoiceRate],
                [Status],
                [IsSelected] )
        values( @InvoiceItemTypeId,
                @InvoiceId,
                @BillItemId,
                @AssetAssignmentId,
                @InvoiceDate,
                @Qty,
                @InvoiceRate,
                @Status,
                @IsSelected );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, InvoiceItemDataObject invoiceItemInfo)
        {
            DbParameter[] parms = new DbParameter[9] {
                new SqlParameter("@InvoiceItemTypeId",      invoiceItemInfo.InvoiceItemTypeId),
                new SqlParameter("@InvoiceId",              invoiceItemInfo.InvoiceId),
                new SqlParameter("@BillItemId",             (0 == invoiceItemInfo.BillItemId)? DBNull.Value: (object)invoiceItemInfo.BillItemId),
                new SqlParameter("@AssetAssignmentId",      invoiceItemInfo.AssetAssignmentId),
                new SqlParameter("@InvoiceDate",            invoiceItemInfo.InvoiceDate),
                new SqlParameter("@Qty",                    invoiceItemInfo.Qty),
                new SqlParameter("@InvoiceRate",            invoiceItemInfo.InvoiceRate),
                new SqlParameter("@Status",                 invoiceItemInfo.Status),
                new SqlParameter("@IsSelected",             invoiceItemInfo.IsSelected),
            };

            invoiceItemInfo.InvoiceItemId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [InvoiceItem]
        set     [InvoiceItemTypeId] = @InvoiceItemTypeId,
                [InvoiceId]         = @InvoiceId,
                [BillItemId]        = @BillItemId,
                [AssetAssignmentId] = @AssetAssignmentId,
                [InvoiceDate]       = @InvoiceDate,
                [Qty]               = @Qty,
                [InvoiceRate]       = @InvoiceRate,
                [Status]            = @Status,
                [IsSelected]        = @IsSelected
        where   [InvoiceItemId]     = @InvoiceItemId";

        public void Update(SqlTransaction tran, InvoiceItemDataObject invoiceItemInfo)
        {
            DbParameter[] parms = new DbParameter[10] {
                new SqlParameter("@InvoiceItemId",     invoiceItemInfo.InvoiceItemId),
                new SqlParameter("@InvoiceItemTypeId", invoiceItemInfo.InvoiceItemTypeId),
                new SqlParameter("@InvoiceId",         invoiceItemInfo.InvoiceId),
                new SqlParameter("@BillItemId",        (0 == invoiceItemInfo.BillItemId)? DBNull.Value: (object)invoiceItemInfo.BillItemId),
                new SqlParameter("@AssetAssignmentId", invoiceItemInfo.AssetAssignmentId),
                new SqlParameter("@InvoiceDate",       invoiceItemInfo.InvoiceDate),
                new SqlParameter("@Qty",               invoiceItemInfo.Qty),
                new SqlParameter("@InvoiceRate",       invoiceItemInfo.InvoiceRate),
                new SqlParameter("@Status",            invoiceItemInfo.Status),
                new SqlParameter("@IsSelected",        invoiceItemInfo.IsSelected),
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [InvoiceItem]
        where   [InvoiceItemId] = @InvoiceItemId";

        public void Remove(SqlTransaction tran, int invoiceItemId)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@InvoiceItemId", invoiceItemId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

    }

}
