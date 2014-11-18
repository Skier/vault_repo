using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;
using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.CustomerOperations.Invoice
{
    public partial class OversellWarningView : BaseForm
    {
        CustomerTransactionDetail m_detail;

        int m_cancelValue = 0;

        #region Constructors

        public OversellWarningView()
        {
            InitializeComponent();
        }

        public OversellWarningView(CustomerTransactionDetail detail,
            int cancelValue)
        {
            m_detail = detail;
            m_cancelValue = cancelValue;

            InitializeComponent();


            m_txtItemName.Text = m_detail.Item.Name;
            m_txtItemNumber.Text = m_detail.ItemNumber;
            m_txtQtyInStock.Text = m_detail.InventoryQuantity.ToString();
            m_txtTrxnQty.Text = m_detail.Quantity.ToString();

            m_txtTrxnQty.SelectAll();
            m_txtTrxnQty.Focus();

#if WIN32
            StartPosition = FormStartPosition.CenterParent;
            SizeGripStyle = SizeGripStyle.Hide;
#endif

        }

        #endregion

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            m_mbOk.Text = CommonResources.BtnDone;
            m_lbItemNumber.Text = Resources.ItemNumber;
            m_lbQtyInStock.Text = Resources.QtyInStock;
            m_lbTrxnQty.Text = Resources.TrxnQty;

            Text = Resources.Title9320;
        }

        private void OversellWarningView_Closing(object sender, CancelEventArgs e)
        {
            if (IsSelfClose)
                m_detail.Quantity = int.Parse(m_txtTrxnQty.Text);
            else
                m_detail.Quantity = m_cancelValue;
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            int test = -1;

            try
            {
                test = int.Parse(m_txtTrxnQty.Text);
                
                Destroy();
            }
            catch (Exception ex)
            {
                EventService.AddEvent(new MobileTechException(ex));
            }
        }

        private void OnQtyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                e.Handled = true;

                Destroy();
            }
        }

        private void OnQtyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;
        }
    }
}