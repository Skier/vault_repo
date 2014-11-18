using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class InvoiceItemType
    {

        private static InvoiceItemType c_InvoiceItemType = new InvoiceItemType();

        public static InvoiceItemType GetInstance()
        {
            return c_InvoiceItemType;
        }

        private InvoiceItemType()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [InvoiceItemTypeId],
                    [Name],
                    [IsCountable],
                    [IsPresetRate],
                    [IsSingle],
                    [Deleted]
            from    [InvoiceItemType]";

        public List<InvoiceItemTypeDataObject> GetInvoiceItemTypes(SqlTransaction tran)
        {
            List<InvoiceItemTypeDataObject> result = new List<InvoiceItemTypeDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    InvoiceItemTypeDataObject invoiceItemTypeInfo = new InvoiceItemTypeDataObject();

                    invoiceItemTypeInfo.InvoiceItemTypeId = (int)dataReader.GetValue(0);
                    invoiceItemTypeInfo.Name = (string)dataReader.GetValue(1);
                    invoiceItemTypeInfo.IsCountable = (bool)dataReader.GetValue(2);
                    invoiceItemTypeInfo.IsPresetRate = (bool)dataReader.GetValue(3);
                    invoiceItemTypeInfo.IsSingle = (bool)dataReader.GetValue(4);
                    invoiceItemTypeInfo.Deleted = (bool)dataReader.GetValue(5);

                    result.Add(invoiceItemTypeInfo);
                }
            }

            return result;
        }

    }

}
