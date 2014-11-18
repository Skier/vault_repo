using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class InvoiceStatus
    {

        private static InvoiceStatus c_InvoiceStatus = new InvoiceStatus();

        public static InvoiceStatus GetInstance()
        {
            return c_InvoiceStatus;
        }

        private InvoiceStatus()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [Status]
            from    [InvoiceStatus]";

        public List<InvoiceStatusDataObject> GetInvoiceStatuses(SqlTransaction tran)
        {
            List<InvoiceStatusDataObject> result = new List<InvoiceStatusDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    InvoiceStatusDataObject invoiceStatusInfo = new InvoiceStatusDataObject();

                    invoiceStatusInfo.Status = (string)dataReader.GetValue(0);

                    result.Add(invoiceStatusInfo);
                }
            }

            return result;
        }

        public List<InvoiceStatusDataObject> GetInvoiceStatuses()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    List<InvoiceStatusDataObject> result = GetInvoiceStatuses(tran);

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
