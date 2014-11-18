using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Domain
{
    public partial class BillItemDataMapper :_BillItemDataMapper
    {

              

              public BillItemDataMapper()
              {}
              public BillItemDataMapper(TractIncRAIDDb database):base(database)
              {}

        private BillItemAttachmentDataMapper m_attachmentDM;

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

        private const string SQL_UPDATE = @"
            UPDATE [BillItem] set
                Status = '{0}'
            WHERE BillItemId = {1}
          ";

        public void Update(SqlTransaction tran, BillItemDataObject billItemInfo)
        {
            string sql = String.Format(
                SQL_UPDATE,
                billItemInfo.Status,
                billItemInfo.BillItemId);

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        private const string SQL_SELECT = @"
            SELECT [BillItemId]
            ,[BillItemTypeId]
            ,[BillId]
            ,[AssetAssignmentId]
            ,[BillingDate]
            ,[Qty]
            ,[BillRate]
            ,[Status]
            ,[Notes]
            ,[BillItemCompositionId]
            FROM BillItem
            where BillId = @BillId
            order by BillItemTypeId asc, AssetAssignmentId asc";

        public List<BillItem> getBillItems(int billId)
        {
            List<BillItem> result = new List<BillItem>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SQL_SELECT))
                {
                    sqlCommand.Parameters.AddWithValue("@BillId", billId);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            BillItem item = doLoad(dataReader);

                            if (null == item.relatedBillItemAttachment)
                            {
                                item.relatedBillItemAttachment = new List<BillItemAttachment>();
                            }

                            List<BillItemAttachment> attachments = AttachmentDM.getAttachments(item.BillItemId);
                            foreach (BillItemAttachment attachment in attachments)
                            {
                                item.addRelatedBillItemAttachmentItem(attachment);
                            }

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        protected BillItemAttachmentDataMapper AttachmentDM
        {
            get
            {
                if (null == m_attachmentDM)
                {
                    m_attachmentDM = new BillItemAttachmentDataMapper();
                }
                return m_attachmentDM;
            }
        }

    }
}
        