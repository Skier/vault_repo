using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

  
namespace Dalworth.Server.Domain
{
    public partial class QbCreditMemo
    {
        #region Constructor

        public QbCreditMemo()
        {
        }

        #endregion 

        #region CustomerRefListId

        private string m_customerRefListId;
        public string CustomerRefListId
        {
            get { return m_customerRefListId; }
            set { m_customerRefListId = value; }
        }

        #endregion

        #region QbCreditMemoLines

        private BindingList<QbCreditMemoLine> m_qbCreditMemoLines = new BindingList<QbCreditMemoLine>();
        public BindingList<QbCreditMemoLine> QbCreditMemoLines
        {
            get { return m_qbCreditMemoLines; }
            set { m_qbCreditMemoLines = value; }
        }

        #endregion

        #region CalculateTotals

        public void CalculateTotals()
        {
            QbItem salesTaxItem = null;

            if (ItemSalesTaxRef != null)
            {
                salesTaxItem = QbItem.FindByPrimaryKey(ItemSalesTaxRef);
            }

            List<QbSalesTaxCode> salesTaxCodes = QbSalesTaxCode.Find();

            TaxAmount = 0;
            TotalAmount = 0;
            SubTotalAmount = 0;

            foreach (QbCreditMemoLine line in QbCreditMemoLines)
            {
                if (line.QbSalesTaxCodeListId != null && salesTaxItem != null)
                {
                    QbSalesTaxCode taxCode = salesTaxCodes.Find(delegate(QbSalesTaxCode taxCode1)
                    { return taxCode1.ListId == line.QbSalesTaxCodeListId; });

                    if (taxCode != null && taxCode.IsTaxable)
                        TaxAmount += (salesTaxItem.TaxRate * line.Amount) / 100;
                }

                SubTotalAmount += line.Amount;
            }
            TotalAmount += SubTotalAmount + TaxAmount;
        }

        #endregion 

    }
}
      