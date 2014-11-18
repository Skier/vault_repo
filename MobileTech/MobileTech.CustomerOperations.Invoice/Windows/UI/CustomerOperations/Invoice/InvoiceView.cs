using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;

using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.CustomerOperations.Invoice
{
	public partial class InvoiceView : BaseForm
    {
        #region Constructor

        public InvoiceView()
		{
			InitializeComponent();
        }

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title3315;

            m_mbAdd.Text = CommonResources.BtnAddItem;
            m_mbDone.Text = CommonResources.BtnDone;

        }

        #endregion

        #region Event Handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InvoiceQuantityTableCellRenderer renderer = new InvoiceQuantityTableCellRenderer();

            m_table.AddColumn(new MobileTech.Windows.UI.Controls.TableColumn(
                1, 40, renderer));


            m_table.Focus();
            m_table.Select(0, 1);
            m_table.BeginEdit();
        }

        #endregion

        #region OnAddClick

        private void OnAddClick(object sender, EventArgs e)
        {
            int rowCount = m_model.GetRowCount();

            App.Execute(CommandName.SelectItem, m_model, false);

            if (rowCount < m_model.GetRowCount())
            {
                m_table.Select(0);
            }

        }

        #endregion

        #region OnDoneClick

        private void OnDoneClick(object sender, EventArgs e)
        {
            m_table.ApplyEdit();

            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    m_model.Save();
                }

                Destroy();
            }
            catch (MobileTechException ex)
            {
                EventService.AddEvent(ex);
            }
            catch (Exception ex)
            {
                EventService.AddEvent(new MobileTechException(ex));
            }
        }

        #endregion

        #region OnClosing

        private void OnClosing(object sender, CancelEventArgs e)
        {
            m_model.QuantityChanged -= new QuantityChangedHandler(OnQuantityChanged);
     
            App.Execute(CommandName.CustomerOperations, 
                m_model.CommonData);

            m_model = null;
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            return base.OnCancel(CommonResources.MsgAllChangesWillBeLostExit);
        }

        #endregion

        #region OnRowChange

        private void OnRowChange(int rowIndex)
        {
   
            m_lbTruck.Visible = rowIndex > -1;

            if (rowIndex == -1)
                return;

            CustomerTransactionDetail detail = 
                (CustomerTransactionDetail)
                m_table.Model.GetObjectAt(rowIndex,0);


            m_lbTruck.Text = String.Format("{0}: {1}",
                Resources.Truck,
                detail.InventoryQuantity);

 
            m_lbID.Text = String.Format("{0}: {1}",
                Resources.ItemNumber,
                detail.ItemNumber);

            m_lbTruck.ForeColor = detail.InventoryQuantity >= detail.Quantity
                ? Color.Black : Color.Red;

        }

        #endregion

        #region OnQuantityChanged

        void OnQuantityChanged(CustomerTransactionDetail item, int oldQuantity)
        {
            if (oldQuantity != item.Quantity
                && item.InventoryQuantity < item.Quantity)
            {
                OversellWarningView oversellWarningView
                    = new OversellWarningView(item, oldQuantity);

                oversellWarningView.ShowDialog();
            }
        }

        #endregion

        #endregion

        #region BindData

        public override void BindData(Object data)
        {
            if (!(data is CustomerOperationsCommonData))
                throw new MobileTechInvalidModelExeption();

            m_model.Init(data as CustomerOperationsCommonData);

            m_table.BindModel(m_model);

            m_model.QuantityChanged += new QuantityChangedHandler(OnQuantityChanged);
        }

        #endregion

        #region InitModel
        InvoiceModel m_model;

        public override void InitModel()
        {
            m_model = new InvoiceModel();
        }

        #endregion
    }
}