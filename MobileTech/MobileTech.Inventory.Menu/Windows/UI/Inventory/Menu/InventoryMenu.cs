using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using MobileTech.Windows.UI.Password;
using MobileTech.Domain;


namespace MobileTech.Windows.UI.Inventory.Menu
{
    public partial class InventoryMenu : BaseForm
    {

        #region Constructor

        public InventoryMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title2000;

            m_mbLoad.Text = Resources.Load;
            m_mbLoadRequest.Text = Resources.LoadRequest;
            m_mbLoadTransfer.Text = Resources.LoadTransfer;
            m_mbPrintReport.Text = Resources.PrintReport;
            m_mbReturnToStock.Text = Resources.ReturnToStock;
            m_mbUnload.Text = Resources.Unload;
            m_mbExit.Text = CommonResources.BtnExit;
        }

        #endregion

        #region Events Handling

        private void OnExitClick(object sender, EventArgs e)
        {

            App.Execute(CommandName.MainMenu);

            Destroy();
        }

        private void OnLoadClick(object sender, EventArgs e)
        {
            PasswordModel p_model = new PasswordModel(PasswordFunctionality.Load);
            if (!p_model.PasswordPassed)
                App.Execute(CommandName.Password, p_model, false);

            if (p_model.PasswordPassed)
            {
                App.Execute(CommandName.InventoryLoad);
                Destroy();
            }
        }

        private void OnUnloadClick(object sender, EventArgs e)
        {
            PasswordModel p_model = new PasswordModel(PasswordFunctionality.Unload);
            if (!p_model.PasswordPassed)
                App.Execute(CommandName.Password, p_model, false);

            if (p_model.PasswordPassed)
            {
                App.Execute(CommandName.InventoryUnload);
                Destroy();
            }
        }


        protected override bool OnCancel()
        {
            App.Execute(CommandName.MainMenu);

            return base.OnCancel();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_mbLoad.Enabled = m_model.LoadAllow;
            m_mbUnload.Enabled = m_model.UnloadAllow;

            if (m_exitFocus)
#if WINCE
                m_mbExit.Focus();
#else
                m_mbExit.Select();
#endif
        }

        #endregion

        #region IView Members

        InventoryMenuModel m_model;
        bool m_exitFocus;
        public override void BindData(Object data)
        {
            if (data is InventoryMenuModel)
                m_model = (InventoryMenuModel)data;
            else if(data is Boolean)
                m_exitFocus = (Boolean)data;
            else
                throw new MobileTechInvalidModelExeption();

            
        }

        #endregion
    }
}