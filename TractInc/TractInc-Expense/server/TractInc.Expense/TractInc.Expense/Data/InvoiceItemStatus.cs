using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class InvoiceItemStatus
    {

        private static InvoiceItemStatus c_InvoiceItemStatus = new InvoiceItemStatus();

        public static InvoiceItemStatus GetInstance()
        {
            return c_InvoiceItemStatus;
        }

        private InvoiceItemStatus()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [Status]
            from    [InvoiceItemStatus]";

        public List<InvoiceItemStatusDataObject> GetInvoiceItemStatuses(SqlTransaction tran)
        {
            List<InvoiceItemStatusDataObject> result = new List<InvoiceItemStatusDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    InvoiceItemStatusDataObject invoiceItemStatusInfo = new InvoiceItemStatusDataObject();

                    invoiceItemStatusInfo.Status = (string)dataReader.GetValue(0);

                    result.Add(invoiceItemStatusInfo);
                }
            }

            return result;
        }

        public List<InvoiceItemStatusDataObject> GetInvoiceItemStatuses()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    List<InvoiceItemStatusDataObject> result = GetInvoiceItemStatuses(tran);

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
