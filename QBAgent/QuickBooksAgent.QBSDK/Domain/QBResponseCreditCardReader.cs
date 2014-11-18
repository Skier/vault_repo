using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public abstract class QBResponseCreditCardReader : QBResponseReader<CreditCard>
    {
        #region Convert

        protected override CreditCard Convert(object item)
        {
            CreditCardRet cardRet = (CreditCardRet)item;

            CreditCard card = new CreditCard(
                null,
                cardRet.CreditCardType,
                null,
                null,
                0,
                cardRet.QuickBooksTxnId,
                cardRet.EditSequence,
                cardRet.TimeCreated,
                cardRet.TimeModified,
                cardRet.TxnNumber,
                cardRet.TxnDate,
                null,
                cardRet.RefNumber,
                cardRet.Amount,
                cardRet.Memo);

            card.Account = new Account();
            card.Account.QuickBooksListId = cardRet.AccountRef.ListId;

            if (cardRet.PayeeEntityRef != null)
            {
                card.PayeeQBEntityId = cardRet.PayeeEntityRef.ListId;
            }

            if (cardRet.ExpenceLines != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ExpenceLineRet));
                card.ExpenceLines = new List<CreditCardExpenceLine>();

                foreach (XmlElement expenceLineElement in cardRet.ExpenceLines)
                {
                    ExpenceLineRet expenceLineRet =
                        (ExpenceLineRet)serializer.Deserialize(new XmlNodeReader(expenceLineElement));

                    CreditCardExpenceLine cardExpenceLine = new CreditCardExpenceLine(
                        null,
                        null,
                        null,
                        0,
                        expenceLineRet.TxnLineID,
                        expenceLineRet.Amount,
                        expenceLineRet.Memo);

                    if (expenceLineRet.AccountRef != null)
                    {
                        cardExpenceLine.Account = new Account();
                        cardExpenceLine.Account.QuickBooksListId = expenceLineRet.AccountRef.ListId;
                    }

                    if (expenceLineRet.CustomerRef != null)
                    {
                        cardExpenceLine.Customer = new Customer();
                        cardExpenceLine.Customer.QuickBooksListId = expenceLineRet.CustomerRef.ListId;
                    }


                    card.ExpenceLines.Add(cardExpenceLine);
                }
            }

            return card;
        }

        #endregion
    }

    #region CreditCardRet

    public class CreditCardRet : QBResponseItem
    {
        #region QuickBooksTxnId

        int m_quickBooksTxnId;
        [XmlElement("TxnID")]
        public int QuickBooksTxnId
        {
            get { return m_quickBooksTxnId; }
            set { m_quickBooksTxnId = value; }
        }

        #endregion

        #region TxnNumber

        private int? m_txnNumber;
        [XmlElement("TxnNumber")]
        public int? TxnNumber
        {
            get { return m_txnNumber; }
            set { m_txnNumber = value; }
        }

        #endregion

        #region AccountRef

        private QBResponseItem m_accountRef;
        [XmlElement("AccountRef")]
        public QBResponseItem AccountRef
        {
            get { return m_accountRef; }
            set { m_accountRef = value; }
        }

        #endregion

        #region PayeeEntityRef

        private QBResponseItem m_payeeEntityRef;
        [XmlElement("PayeeEntityRef")]
        public QBResponseItem PayeeEntityRef
        {
            get { return m_payeeEntityRef; }
            set { m_payeeEntityRef = value; }
        }

        #endregion

        #region RefNumber

        private string m_refNumber;
        [XmlElement("RefNumber")]
        public string RefNumber
        {
            get { return m_refNumber; }
            set { m_refNumber = value; }
        }

        #endregion

        #region TxnDate

        private DateTime m_txnDate;
        [XmlElement("TxnDate")]
        public DateTime TxnDate
        {
            get { return m_txnDate; }
            set { m_txnDate = value; }
        }

        #endregion

        #region Amount

        private decimal m_amount;
        [XmlElement("Amount")]
        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

        #endregion

        #region Memo

        private string m_memo;
        [XmlElement("Memo")]
        public string Memo
        {
            get { return m_memo; }
            set { m_memo = value; }
        }

        #endregion

        #region ExpenceLines

        private XmlElement[] m_expenceLines;
        [XmlAnyElement("ExpenseLineRet")]
        public XmlElement[] ExpenceLines
        {
            get { return m_expenceLines; }
            set { m_expenceLines = value; }
        }

        #endregion

        #region CreditCardType

        private CreditCardType m_creditCardType;
        public CreditCardType CreditCardType
        {
            get { return m_creditCardType; }
            set { m_creditCardType = value; }
        }

        #endregion
    }

    #endregion

    #region ExpenceLine
    [XmlRoot("ExpenseLineRet")]
    public class ExpenceLineRet : QBResponseItem
    {
        #region TxnLineID

        private int m_txnLineID;
        [XmlElement("TxnLineID")]
        public int TxnLineID
        {
            get { return m_txnLineID; }
            set { m_txnLineID = value; }
        }

        #endregion

        #region AccountRef

        private QBResponseItem m_accountRef;
        [XmlElement("AccountRef")]
        public QBResponseItem AccountRef
        {
            get { return m_accountRef; }
            set { m_accountRef = value; }
        }

        #endregion

        #region Amount

        private decimal m_amount;
        [XmlElement("Amount")]
        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

        #endregion

        #region Memo

        private string m_memo;
        [XmlElement("Memo")]
        public string Memo
        {
            get { return m_memo; }
            set { m_memo = value; }
        }

        #endregion

        #region CustomerRef

        private QBResponseItem m_customerRef;
        [XmlElement("CustomerRef")]
        public QBResponseItem CustomerRef
        {
            get { return m_customerRef; }
            set { m_customerRef = value; }
        }

        #endregion
    }

    #endregion    
}
