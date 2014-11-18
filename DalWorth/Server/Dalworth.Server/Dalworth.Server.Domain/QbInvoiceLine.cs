using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  
  
namespace Dalworth.Server.Domain
{

    public partial class QbInvoiceLine
    {
        public QbInvoiceLine(){ }

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

        private const string SqlFindByQbInvoiceId =
            @"select il.*, i.*, t.*  from qbinvoiceline il
                inner join QbItem i on i.ListId = il.QbItemListId
                inner join qbsalestaxcode t on t.ListId = il.QbSalesTaxCodeListId
            where qbInvoiceid = ?QbInvoiceId";

        public static List<QbInvoiceLine> FindByInvoiceId(int qbInvoiceId, IDbConnection connection)
        {
            List<QbInvoiceLine> rv = new List<QbInvoiceLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQbInvoiceId, connection))
            {
                Database.PutParameter(dbCommand, "?QbInvoiceId", qbInvoiceId);                
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        QbInvoiceLine line = Load(dataReader);
                        line.QbItem = QbItem.Load(dataReader, FieldsCount);
                        line.QbSalesTaxCode = QbSalesTaxCode.Load(dataReader, FieldsCount + QbItem.FieldsCount);
                        rv.Add(line);
                    }
                }                
            }

            return rv;
        }

        #endregion

        #region FindByProjectId

        private const string SqlFindByProject =
            @"select qbinvoiceline.*
            from qbinvoiceline
            join task on qbinvoiceline.taskid = task.id
            where projectid = ?ProjectId";

        public static List<QbInvoiceLine> FindByProject(Project project, IDbConnection connection)
        {
            List<QbInvoiceLine> rv = new List<QbInvoiceLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProject, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectId", project.ID);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        QbInvoiceLine line = Load(dataReader);
                        rv.Add(line);
                    }
                }
            }

            return rv;
        }

        #endregion

        #region FindByTaskId

        private const string SqlFindByTaskId =
            SqlSelectAll + " where taskid = ?TaskId";

        public static List<QbInvoiceLine> FindByTaskId(int taskId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTaskId, connection))
            {
                Database.PutParameter(dbCommand, "?TaskId", taskId);
                List<QbInvoiceLine> rv = new List<QbInvoiceLine>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }

        }

        #endregion

        #region QbEquals

        public bool QbEquals(QbInvoiceLine line)
        {
            return this.TxnLineID == line.TxnLineID &&
                this.QbItemListId == line.QbItemListId &&
                this.Description == line.Description &&
                this.Quantity == line.Quantity &&
                this.UnitOfMeasure == line.UnitOfMeasure &&
                this.Rate == line.Rate &&
                this.QbClassListId == line.QbClassListId &&
                this.Amount == line.Amount &&
                this.QbSalesTaxCodeListId == line.QbSalesTaxCodeListId;
        }

        #endregion 

        #region FillQbFields

        public void FillQbFields(QbInvoiceLine line)
        {
            this.TxnLineID = line.TxnLineID;
            this.QbItemListId = line.QbItemListId;
            this.Description = line.Description;
            this.Quantity = line.Quantity;
            this.UnitOfMeasure = line.UnitOfMeasure;
            this.Rate = line.Rate;
            this.QbClassListId = line.QbClassListId;
            this.Amount = line.Amount;
            this.QbSalesTaxCodeListId = line.QbSalesTaxCodeListId;
        }

        #endregion

        #region ClearTaskIdInvoiceId

        private const string SqlUpdateInvoiceTaskId =
            "update qbinvoiceline set taskid = null, itemid = null where qbinvoiceid = ?QbInvoiceId";
            
        public static void ClearTaskIdInvoiceId(int qbInvoiceId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateInvoiceTaskId, connection))
            {
                Database.PutParameter(dbCommand, "?QbInvoiceId", qbInvoiceId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
      