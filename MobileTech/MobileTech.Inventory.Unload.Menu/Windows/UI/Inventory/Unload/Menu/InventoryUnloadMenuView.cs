using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileTech.Windows.UI.Inventory.Unload.Menu
{
    public partial class InventoryUnloadMenuView : BaseForm
    {
        #region Constructor

        public InventoryUnloadMenuView()
        {
            InitializeComponent();
        }

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_mbContinue.Text = CommonResources.BtnContinue;
            m_mbDamage.Text = CommonResources.DcDamage;
            m_mbEquipment.Text = CommonResources.DcEquipment;
            m_mbGood.Text = CommonResources.DcGood;

            Text = Resources.Title;
        }

        #endregion

        #region Event Handlers

        private void OnContinueClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryUnloadCommit);

            Destroy();
        }

        private void OnGoodClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryUnloadGood);

            Destroy();
        }

        private void OnEquipmentClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryUnloadEquipment);

            Destroy();
        }

        private void OnDamageClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryUnloadDamage);

            Destroy();
        }

        protected override bool OnCancel()
        {
            bool cancel = false;

            if (m_model.IsTransactionExists)
                cancel = base.OnCancel(CommonResources.MsgAllChangesWillBeLostExit);
            else
                cancel = false;

            if (!cancel)
            {
                m_model.Rollback();

                App.Execute(CommandName.InventoryMenu);
            }

            return cancel;
        }

        #endregion

        #region Model

        InventoryUnloadMenuModel m_model;

        public override void InitModel()
        {
            m_model = new InventoryUnloadMenuModel();

            m_model.Init();
        }

        protected override void CleanUp()
        {
            m_model.CleanUp();
            m_model = null;
        }

        #endregion
    }
}