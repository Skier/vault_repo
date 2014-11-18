using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.ServiceLayer;
using System.Diagnostics;

namespace MobileTech.Windows.UI.Inventory.Unload.Commit
{
    public partial class InventoryUnloadCommitView : BaseForm
    {
        #region Constructor
        public InventoryUnloadCommitView()
        {
            InitializeComponent();
        }
        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_lbInfo.Text = !m_model.IsTransactionExists ?
                Resources.NoTransaction : Resources.PressFinishToCommit;

            m_mbBack.Text = CommonResources.BtnBack;
            m_mbFinal.Text = CommonResources.BtnFinal;

        }

        #endregion

        #region Model

        InventoryUnloadCommitModel m_model;
        public override void InitModel()
        {
            m_model = new InventoryUnloadCommitModel();

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

        #region Event handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(m_model != null);

            base.OnLoad(e);

        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            App.Execute(CommandName.InventoryUnload);

            return base.OnCancel();
        }
        #endregion

        #region OnBackClick

        private void OnBackClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.InventoryUnload);

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
                    new MobileTechException("Unload commit error",ex));
            }
        }

        #endregion

        #endregion
    }
}