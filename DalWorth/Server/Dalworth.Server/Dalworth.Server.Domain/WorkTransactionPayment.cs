using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WorkTransactionPayment
    {
        public WorkTransactionPayment(){ }

        #region CreditCardType

        [XmlIgnore]
        public CreditCardTypeEnum? CreditCardType
        {
            get { return (CreditCardTypeEnum?) m_creditCardTypeId; }
            set { m_creditCardTypeId = (int?) value; }
        }

        #endregion

        #region CreditCardCVV2Type

        [XmlIgnore]
        public CreditCardCVV2TypeEnum? CreditCardCVV2Type
        {
            get { return (CreditCardCVV2TypeEnum?)m_creditCardCVV2TypeId; }
            set { m_creditCardCVV2TypeId = (int?)value; }
        }

        #endregion

        #region BankCheckAccountType

        [XmlIgnore]
        public BankCheckAccountTypeEnum? BankCheckAccountType
        {
            get { return (BankCheckAccountTypeEnum?)m_bankCheckAccountTypeId; }
            set { m_bankCheckAccountTypeId = (int?)value; }
        }

        #endregion

        #region WorkTransactionPaymentType

        [XmlIgnore]
        public WorkTransactionPaymentTypeEnum WorkTransactionPaymentType
        {
            get { return (WorkTransactionPaymentTypeEnum)m_workTransactionPaymentTypeId; }
            set { m_workTransactionPaymentTypeId = (int)value; }
        }

        #endregion

        #region DeleteBy WorkTransaction

        private const string SqlDeleteByWorkTransaction =
            @"DELETE FROM WorkTransactionPayment
                where WorkTransactionId = ?WorkTransactionId";

        public static void DeleteBy(WorkTransaction workTransaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", workTransaction.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region FindBy WorkTransaction

        private const string SqlFindByWorkTransaction =
            @"SELECT *
            FROM WorkTransactionPayment
                WHERE WorkTransactionId = ?WorkTransactionId";

        public static List<WorkTransactionPayment> FindBy(WorkTransaction workTransaction)
        {
            List<WorkTransactionPayment> result = new List<WorkTransactionPayment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", workTransaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion
    }
}
      