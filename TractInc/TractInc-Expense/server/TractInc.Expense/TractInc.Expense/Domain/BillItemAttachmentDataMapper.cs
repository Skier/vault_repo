
          namespace TractInc.Expense.Domain
          {
              using System;
              using System.Collections.Generic;
              using System.Text;
              using System.Data;
              using System.Data.SqlClient;
              using Weborb.Data.Management;
              using TractInc.Expense.Entity;

              public class BillItemAttachmentDataMapper : _BillItemAttachmentDataMapper
            {
              public BillItemAttachmentDataMapper()
              {}
              public BillItemAttachmentDataMapper(TractIncRAIDDb database):base(database)
              {}

              private const string SQL_REMOVE = @"
                DELETE FROM [BillItemAttachment] WHERE BillItemId = {0}";

              public void Remove(SqlTransaction tran, int billItemId)
              {
                  string sql = String.Format(
                      SQL_REMOVE,
                      billItemId);

                  SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
              }

              private const string SQL_INSERT = @"
                INSERT INTO [BillItemAttachment] (BillItemId, FileName, OriginalFileName) VALUES
                ({0}, '{1}', '{2}')";

              public void Insert(SqlTransaction tran, BillItemAttachmentDataObject attachmentInfo)
              {
                  string sql = String.Format(
                      SQL_INSERT,
                      attachmentInfo.BillItemId,
                      attachmentInfo.FileName,
                      attachmentInfo.OriginalFileName);

                  SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
              }

              private const string SQL_SELECT_BY_BILL_ITEM_ID = @"
            SELECT *
            FROM BillItemAttachment
            where BillItemId = @BillItemId";

              public List<BillItemAttachment> getAttachments(int billItemId)
              {
                  List<BillItemAttachment> result = new List<BillItemAttachment>();

                  using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
                  {
                      using (SqlCommand sqlCommand = Database.CreateCommand(SQL_SELECT_BY_BILL_ITEM_ID))
                      {
                          sqlCommand.Parameters.AddWithValue("@BillItemId", billItemId);

                          using (IDataReader dataReader = sqlCommand.ExecuteReader())
                          {
                              while (dataReader.Read())
                              {
                                  BillItemAttachment attachment = doLoad(dataReader);

                                  result.Add(attachment);
                              }
                          }
                      }
                  }

                  return result;
              }

          }
        }
      