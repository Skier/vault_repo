using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.Inventory.Load.Commit
{
    public partial class InventoryLoadCommitView : BaseForm
    {
        #region Constructor

        public InventoryLoadCommitView()
        {
            InitializeComponent();
        }
        #endregion

        #region Model

        InventoryLoadCommitModel m_model;
        public override void InitModel()
        {
            m_model = new InventoryLoadCommitModel();

            m_model.Init();
        }

        protected override void CleanUp()
        {
            if (m_model != null)
            {
                m_model.CleanUp();

                m_model = null;
            }
        }
        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_mbBack.Text = CommonResources.BtnBack;
            m_mbFinal.Text = CommonResources.BtnFinal;
        }

        #endregion

        #region Event handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (m_model == null)
                return;


            m_lbInfo.Text = !m_model.IsTransactionExists ?
                Resources.NoTransaction : Resources.PressFinishToCommit;
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            App.Execute(CommandName.InventoryLoad);

            return base.OnCancel();
        }
        #endregion

        #region OnBackClick

        private void OnBackClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryLoad);

            Destroy();
        }

        #endregion

        #region OnFinalClick

        private void OnFinalClick(object sender, EventArgs e)
        {
            try
            {
                if (m_model.IsTransactionExists)
                {
                    m_model.AssignDocumentNumber();

                    m_model.Commit();
                }
                else
                    m_model.Rollback();

                App.Execute(CommandName.InventoryMenu,true,true);

                Destroy();
            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                    new MobileTechException(Resources.EnableToCommitTransaction,
                    ex));
            }
        }

        #endregion

        #endregion
    }
}