using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;


namespace MobileTech.Windows.UI.CustomerOperations.Menu
{
	public partial class CustomerOperationsMenuView : BaseForm
    {
        #region Constructor

        public CustomerOperationsMenuView()
		{
			InitializeComponent();
        }

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;


            Text = Resources.Title3000;

            m_mbContinue.Text = CommonResources.BtnContinue;
            m_mbInvoice.Text = Resources.Invoice;
            m_mbInventory.Text = Resources.Inventory;
            m_mbInformation.Text = Resources.Information;
            m_mbOrder.Text = Resources.Order;
            m_mbMerchandising.Text = Resources.Merchandising;
            m_mbReviewVisits.Text = Resources.ReviewVisits;
            m_mbARCollection.Text = Resources.ARCollection;
        }

        #endregion

        #region Event Handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (m_model.CommonData.OdometerReading == 0)
            {
                App.Execute(CommandName.Odometer,
                    m_model.CommonData,
                    false);
            }
        }

        #endregion

        #region OnInvoiceClick

        private void OnInvoiceClick(object sender, EventArgs e)
        {

            App.Execute(CommandName.InvoiceItemEntry, 
                m_model.CommonData);

            Destroy();
        }

        #endregion

        #region OnContinueClick

        private void OnContinueClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.CustomerOperationsCommit, 
                m_model.CommonData);

            Destroy();
        }

        #endregion

        #region OnCancel
        protected override bool OnCancel()
        {
            bool cancel = OnCancel(CommonResources.MsgAllChangesWillBeLostExit);

            if (!cancel)
            {
                m_model.Rollback();

                App.Execute(CommandName.CustomerSelection);
            }

            return cancel;
        }
        #endregion

        #endregion

        #region BindData
        public override void BindData(object data)
        {
            if (!(data is CustomerOperationsCommonData))
                throw new MobileTechInvalidModelExeption();

            try
            {
                m_model.Init(data as CustomerOperationsCommonData);
            }
            catch (Exception ex)
            {
                m_model.Rollback();

                throw ex;
            }
        }

        #endregion
        
        #region InitModel

        CustomerOperationsMenuModel m_model;
        public override void InitModel()
        {
            m_model = new CustomerOperationsMenuModel();
        }

        #endregion
    }
}