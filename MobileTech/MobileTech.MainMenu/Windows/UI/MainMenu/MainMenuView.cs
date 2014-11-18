using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MobileTech.Domain;
using MobileTech.ServiceLayer;
using MobileTech.Windows.UI.Password;

namespace MobileTech.Windows.UI.MainMenu
{
	public partial class MainMenuView : BaseForm
    {

        #region Constructor

        public MainMenuView()
		{
			InitializeComponent();
		}
        #endregion

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title;

            m_mbInventory.Text = Resources.Inventory;
            m_mbCustomerService.Text = Resources.CustomerOperations;
            m_mbEndDay.Text = Resources.EndDay;
            m_mbInformation.Text = Resources.Information;
            m_mbReview.Text = Resources.Review;
            m_mbSetup.Text = Resources.Setup;
            m_mbStartDay.Text = Resources.StartDay;
            m_mbTransmitData.Text = Resources.TransmitData;
        }

        #region Event Handlers

        #region OnActivated
        private void OnActivated(object sender, EventArgs e)
        {
            m_mbStartDay.Enabled = m_model.IsCommandAllow(CommandName.StartDay);
            m_mbCustomerService.Enabled = m_model.IsCommandAllow(CommandName.CustomerOperations);
            m_mbEndDay.Enabled = m_model.IsCommandAllow(CommandName.EndDay);
            m_mbInformation.Enabled = m_model.IsCommandAllow(CommandName.Information);
            m_mbInventory.Enabled = m_model.IsCommandAllow(CommandName.InventoryMenu);
            m_mbReview.Enabled = false;
            m_mbSetup.Enabled = m_model.IsCommandAllow(CommandName.SetupMenu);
            m_mbTransmitData.Enabled = m_model.IsCommandAllow(CommandName.TComm);
        }

        #endregion

        #region OnStartDayClick

        private void OnStartDayClick(object sender, EventArgs e)
        {
            RouteStatusEnum routeStatus = Route.Current.Status;

            if (routeStatus == RouteStatusEnum.EOP_DONE)
            {
                if (DialogResult.No == MessageBox.Show(Resources.BypassTComm, "", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1))
                {
                    return;
                }
            }

            PasswordModel p_model = new PasswordModel(PasswordFunctionality.StartDay);
            if (!p_model.PasswordPassed)
                App.Execute(CommandName.Password, p_model, false);

            if (p_model.PasswordPassed)
            {
                App.Execute(CommandName.StartDay);
                Destroy();
            }
        }

        #endregion

        #region OnEndDayClick

        private void OnEndDayClick(object sender, EventArgs e)
        {
            PasswordModel p_model = new PasswordModel(PasswordFunctionality.EndDay);
            if (!p_model.PasswordPassed)
                App.Execute(CommandName.Password, p_model, false);

            if (p_model.PasswordPassed)
            {
                App.Execute(CommandName.EndDay);
                Destroy();
            }


        }

        #endregion

        #region OnCustomerServiceClick
        private void OnCustomerServiceClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.CustomerSelection);

            Destroy();
        }
       #endregion

        #region OnSetupClick
        private void OnSetupClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.SetupMenu);

            Destroy();
        }
        #endregion

        #region OnInventoryClick

        private void OnInventoryClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryMenu);
            Destroy();
        }

        #endregion

        #region OnTransmitDataClick
        
        private void OnTransmitDataClick(object sender, EventArgs e)
        {

            try
            {
               App.Execute(CommandName.TComm);
            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                    new MobileTechException(Resources.EnableToRunTComm,ex));

                return;
            }

            Destroy();
        }
        #endregion

        #endregion

        #region IView Members

        MainMenuModel m_model;

        public override void BindData(Object data)
        {
            if (!(data is MainMenuModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (MainMenuModel)data;
        }

        #endregion
    }
}