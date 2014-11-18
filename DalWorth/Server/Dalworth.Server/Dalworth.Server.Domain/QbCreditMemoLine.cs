using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class QbCreditMemoLine
    {
        public QbCreditMemoLine(){}

        #region QbItem

        private QbItem m_qbItem;
        public QbItem QbItem
        {
            get { return m_qbItem; }
            set { m_qbItem = value; }
        }

        #endregion

        #region QbSalesTaxCode

        private QbSalesTaxCode m_qbSalesTaxCode;
        public QbSalesTaxCode QbSalesTaxCode
        {
            get { return m_qbSalesTaxCode; }
            set { m_qbSalesTaxCode = value; }
        }

        #endregion

        #region QbItemName

        public string QbItemName
        {
            get { return m_qbItem.FullName; }
        }

        #endregion

        #region QbSalesTaxCodeName

        public string QbSalesTaxCodeName
        {
            get { return m_qbSalesTaxCode.Name; }
        }

        #endregion

        #region FindByQbInvoiceId

        private const string SqlFindByQbCreditMemoId =
            @"select li.*, i.*, t.*  from qbcreditmemoline li
                inner join QbItem i on i.ListId = li.QbItemListId
                inner join qbsalestaxcode t on t.ListId = li.QbSalesTaxCodeListId
            where QbCreditMemoTxnID = ?QbCreditMemoTxnID";

        public static List<QbCreditMemoLine> FindByCreditMemoId(string qbCreditMemoTxnId)
        {
            List<QbCreditMemoLine> rv = new List<QbCreditMemoLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQbCreditMemoId))
            {
                Database.PutParameter(dbCommand, "?QbCreditMemoTxnID", qbCreditMemoTxnId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        QbCreditMemoLine line = Load(dataReader);
                        line.QbItem = QbItem.Load(dataReader, FieldsCount);
                        line.QbSalesTaxCode = QbSalesTaxCode.Load(dataReader, FieldsCount + QbItem.FieldsCount);
                        rv.Add(line);
                    }
                }
            }

            return rv;
        }

        #endregion

        #region DeleteByCreditMemoTxnId

        private const String SqlDeleteByCreditMemoTxnId = "Delete From QbCreditMemoLine "
          + " Where "
          + " QbCreditMemoTxnId = ?QbCreditMemoTxnId "
        ;
        public static void DeleteByCreditMemoTxnId(string qbCreditMemoTxnID, IDbConnection connection)
        {

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByCreditMemoTxnId, connection))
            {

                Database.PutParameter(dbCommand, "?QbCreditMemoTxnId", qbCreditMemoTxnID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion 
    }
}
      