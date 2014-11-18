using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class DefaultInvoiceRate
    {

        private static DefaultInvoiceRate c_DefaultInvoiceRate = new DefaultInvoiceRate();

        public static DefaultInvoiceRate GetInstance()
        {
            return c_DefaultInvoiceRate;
        }

        private DefaultInvoiceRate()
        {
        }

        private const string SQL_SELECT_BY_CLIENT = @"
            select  [DefaultInvoiceRateId],
                    [ClientId],
                    [InvoiceItemTypeId],
                    [InvoiceRate]
            from    [DefaultInvoiceRate]
            where   [ClientId] = @ClientId";

        public List<DefaultInvoiceRateDataObject> GetClientRates(SqlTransaction tran, int clientId)
        {
            List<DefaultInvoiceRateDataObject> result = new List<DefaultInvoiceRateDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ClientId", clientId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_CLIENT, parms))
            {
                while (dataReader.Read())
                {
                    DefaultInvoiceRateDataObject clientRateInfo = new DefaultInvoiceRateDataObject();

                    clientRateInfo.DefaultInvoiceRateId = (int)dataReader.GetValue(0);
                    clientRateInfo.ClientId = (int)dataReader.GetValue(1);
                    clientRateInfo.InvoiceItemTypeId = (int)dataReader.GetValue(2);
                    clientRateInfo.InvoiceRate = (decimal)dataReader.GetValue(3);

                    result.Add(clientRateInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [DefaultInvoiceRate]
              ( [ClientId],
                [InvoiceItemTypeId],
                [InvoiceRate] )
        values( @ClientId,
                @InvoiceItemTypeId,
                @InvoiceRate );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, DefaultInvoiceRateDataObject rateInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@ClientId",          rateInfo.ClientId),
                new SqlParameter("@InvoiceItemTypeId", rateInfo.InvoiceItemTypeId),
                new SqlParameter("@InvoiceRate",       rateInfo.InvoiceRate)
            };

            rateInfo.DefaultInvoiceRateId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [DefaultInvoiceRate]
        set     [InvoiceRate] = @InvoiceRate
        where   [DefaultInvoiceRateId] = @DefaultInvoiceRateId";

        public void Update(SqlTransaction tran, DefaultInvoiceRateDataObject rateInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@DefaultInvoiceRateId", rateInfo.DefaultInvoiceRateId),
                new SqlParameter("@InvoiceRate",          rateInfo.InvoiceRate)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [DefaultInvoiceRate]
        where   [DefaultInvoiceRateId] = @DefaultInvoiceRateId";

        public void Remove(SqlTransaction tran, DefaultInvoiceRateDataObject rateInfo)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@DefaultInvoiceRateId", rateInfo.DefaultInvoiceRateId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);

            rateInfo.DefaultInvoiceRateId = 0;
        }

    }

}
