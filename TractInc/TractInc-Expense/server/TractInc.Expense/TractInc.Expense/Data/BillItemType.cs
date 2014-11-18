using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class BillItemType
    {

        private static BillItemType c_BillItemType = new BillItemType();

        public static BillItemType GetInstance()
        {
            return c_BillItemType;
        }

        private BillItemType()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [BillItemTypeId],
                    [InvoiceItemTypeId],
                    [Name],
                    [IsCountable],
                    [IsPresetRate],
                    [IsSingle],
                    [IsAttachRequired],
                    [Deleted]
            from    [BillItemType]";

        public List<BillItemTypeDataObject> GetBillItemTypes(SqlTransaction tran)
        {
            List<BillItemTypeDataObject> result = new List<BillItemTypeDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    BillItemTypeDataObject billItemTypeInfo = new BillItemTypeDataObject();

                    billItemTypeInfo.BillItemTypeId = (int)dataReader.GetValue(0);
                    billItemTypeInfo.InvoiceItemTypeId = (int)dataReader.GetValue(1);
                    billItemTypeInfo.Name = (string)dataReader.GetValue(2);
                    billItemTypeInfo.IsCountable = (bool)dataReader.GetValue(3);
                    billItemTypeInfo.IsPresetRate = (bool)dataReader.GetValue(4);
                    billItemTypeInfo.IsSingle = (bool)dataReader.GetValue(5);
                    billItemTypeInfo.IsAttachRequired = (bool)dataReader.GetValue(6);
                    billItemTypeInfo.Deleted = (bool)dataReader.GetValue(7);

                    result.Add(billItemTypeInfo);
                }
            }

            return result;
        }

        public List<BillItemTypeDataObject> GetBillItemTypes()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    List<BillItemTypeDataObject> result = GetBillItemTypes(tran);

                    tran.Commit();

                    return result;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

    }

}
