using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
using MobileTech.Domain;
using System.Windows.Forms;
using MobileTech.Windows.UI.Password;


namespace MobileTech.Windows.UI.Setup.Menu
{
    public partial class SetupMenu : BaseForm
    {
        #region Fields

        bool m_cancel = true;

        #endregion

        #region Constructors

        public SetupMenu()
        {
            InitializeComponent();
        }
        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title8000;

            m_mbRouteSetup.Text = Resources.RouteSetup;
            m_mbTCommSetup.Text = Resources.TCommSetup;
            m_mbPen.Text = Resources.Pen;
            m_mbAbout.Text = Resources.About;
            m_mbCustomerBalance.Text = Resources.CustomerBal;
            m_mbSystem.Text = Resources.SystemInfo;
            m_mbSwitch.Text = Resources.SwitchRoute;
            m_mbExit.Text = Resources.Exit8000;
        }

        #endregion

        #region Events Handling

        private void m_mbRouteSetup_Click(object sender, EventArgs e)
        {
            if ((File.Exists(Configuration.DBFullPath)) && (Route.IsContainsData()))
            {
                PasswordModel p_model = new PasswordModel(PasswordFunctionality.SetupRoute);
                if (!p_model.PasswordPassed)
                    App.Execute(CommandName.Password, p_model, false);

                if (p_model.PasswordPassed)
                {
                    if ((File.Exists(Configuration.DBFullPath)) && (Route.IsContainsData()))
                    {
                        App.Execute(CommandName.SetupRoute, null, false);
                        m_cancel = false;
                        m_mbExit.Focus();

                    }
                }
            }
            else
            {
                App.Execute(CommandName.SetupRoute, null, false);
                m_cancel = false;
                m_mbExit.Focus();
            }
        }
        private void OnExitClick(object sender, EventArgs e)
        {

            App.Execute(CommandName.MainMenu);

            m_cancel = false;

            WinAPI.CloseWindow(this);
        }


        private void OnClosing(object sender, CancelEventArgs e)
        {
            if(m_cancel)
                App.Execute(CommandName.MainMenu);
        }

        #endregion

        #region IView Members

        SetupMenuModel m_model;

        public override void BindData(Object data)
        {
            if (!(data is SetupMenuModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (SetupMenuModel)data;
        }

        #endregion
    }
}