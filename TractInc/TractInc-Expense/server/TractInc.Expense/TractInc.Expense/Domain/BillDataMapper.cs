
          namespace TractInc.Expense.Domain
          {
              using System;
              using System.Collections.Generic;
              using System.Text;
              using System.Data;
              using System.Data.SqlClient;
              using System.Globalization;
              using TractInc.Expense.Entity;
              using Weborb.Data.Management;

              public class BillDataMapper : _BillDataMapper
            {
              public BillDataMapper()
              {}
              public BillDataMapper(TractIncRAIDDb database):base(database)
              {}

                  private BillItemDataMapper m_billItemDM;


          private const string SQL_UPDATE = @"
            UPDATE [Bill] set
                Status = '{0}', 
                TotalDailyBill = {1}, 
                DailyBillAmt = {2},
                OtherBillAmt = {3},
                TotalBillAmt = {4}
            WHERE BillId = {5}
          ";

          public void Update(SqlTransaction tran, BillDataObject billInfo)
          {
              IFormatProvider culture = new CultureInfo("en-US", true);
              string sql = String.Format(
                  culture,
                  SQL_UPDATE,
                  billInfo.Status,
                  billInfo.TotalDailyBill,
                  billInfo.DailyBillAmt,
                  billInfo.OtherBillAmt,
                  billInfo.TotalBillAmt,
                  billInfo.BillId);

              SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
          }

                public List<BillItem> GetBill(int billId)
                {
                    Bill bill = findByPrimaryKey(billId);

                    if (null == bill.relatedBillItem)
                    {
                        bill.relatedBillItem = new List<BillItem>();
                    }

                    List<BillItem> items = BillItemDM.getBillItems(billId);

                    foreach (BillItem item in items)
                    {
                        item.RelatedBill = bill;
                        bill.addRelatedBillItemItem(item);
                    }
                    return bill.relatedBillItem;
                }

                  protected BillItemDataMapper BillItemDM
                  {
                      get
                      {
                          if (null == m_billItemDM)
                          {
                              m_billItemDM = new BillItemDataMapper();
                          }
                          return m_billItemDM;
                      }
                  }

            }
        }
      