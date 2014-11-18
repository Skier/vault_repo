using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using MobileTech.Data;
using System.IO;
using MobileTech.ServiceLayer;
using MobileTech.Domain;
using MobileTech.Windows.UI.Password;
#if WINCE
using Microsoft.WindowsCE.Forms;
using System.Reflection;
#endif

namespace MobileTech.Windows.UI.Forms
{
	public partial class MainView : BaseForm
    {
        #region Constructor

        public MainView()
		{
#if WIN32
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            MaximizeBox = false;
#endif
			InitializeComponent();
            LoadCultures();

 //           Host.Instance.CultureChange += new CultureChangeHandler(OnCultureChange);

#if DEBUG
            m_linkCreateDatabase.Visible = true;
            m_linkCreateDatabase.Enabled = true;            
#endif
        }

        #endregion

       /* #region OnCultureChange
        void OnCultureChange(CultureInfo cultureInfo)
        {
            CommonResources.ChangeCulture(cultureInfo);

            ApplyUIResources();
        }
        #endregion*/

        #region ApplyUIResources
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            CommonResources.ChangeCulture(cultureInfo);


            m_lbLanguage.Text = Resources.Language;
            m_mbEvents.Text = Resources.Events;
            m_mbMainMenu.Text = Resources.MainMenu;
            m_mbClose.Text = CommonResources.BtnExit;

            //m_lbCopyright.Text = Properties.Resources.Copyright;

        }
        #endregion

        #region OnLoad
        private void OnLoad(object sender, EventArgs e)
		{
            //Controller.Instance.Execute(CommandName.MainMenu);
            pictureBox1.Image = GUI.GetImage(ImageKeys.MobileTech);
#if WINCE
			WinAPI.HideXButton(this);
#endif
            m_mbMainMenu.Focus();
        }
        #endregion

        #region OnActivated
        private void OnActivated(object sender, EventArgs e)
		{
			//m_menuPane.Visible = Controller.Instance.TaskCount == 0;
#if WINCE
            WinAPI.ShowInputPanel(false);
#endif

            OnRefresh();

        }
        #endregion

        #region OnRefresh

        private void OnRefresh()
        {
            m_mbEvents.Enabled = Database.IsDatabaseExist();

            m_mbEvents.Refresh();
        }

        #endregion

        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            if ((File.Exists(Configuration.DBFullPath)) && (Route.IsContainsData()))
            {
                PasswordModel p_model = new PasswordModel(PasswordFunctionality.Exit);
                if (!p_model.PasswordPassed)
                    Controller.Instance.Execute(CommandName.Password, p_model, false);

                if (p_model.PasswordPassed)
                {
                    WinAPI.CloseWindow(this);
                }
            }
            else
            {
                WinAPI.CloseWindow(this);
            }

        }

        #endregion

        #region OnMainMenuClick
        private void OnMainMenuClick(object sender, EventArgs e)
        {
            Controller.Instance.Execute(CommandName.MainMenu);
        }
        #endregion

        #region OnEventsClick
        private void OnEventsClick(object sender, EventArgs e)
        {
            Controller.Instance.Execute(CommandName.Events);
        }
        #endregion

        #region OnLanguageSelectedIndexChanged
        private void OnLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            Host.Instance.Culture = (m_cbLanguage.SelectedItem as CultureInfoWrap).Culture;
        }
        #endregion

        #region OnLinkCreateDatabaseClick

        private void OnLinkCreateDatabaseClick(object sender, EventArgs e)
        {
            DatabaseManager.DatabaseManagerView dbManager = new MobileTech.Windows.UI.DatabaseManager.DatabaseManagerView();
            dbManager.ShowDialog();
        }

        #endregion

        #region LoadCultures
        void LoadCultures()
        {
            int selectIndex = -1;

            foreach (CultureInfo cultureInfo in Host.AvailableCultures)
            {
                int i = m_cbLanguage.Items.Add(new CultureInfoWrap(cultureInfo));

                if (cultureInfo.LCID == Host.Instance.Culture.LCID)
                    selectIndex = i;
            }

            m_cbLanguage.SelectedIndex = selectIndex;
        }
        #endregion

        #region Helper class

        private class CultureInfoWrap
        {
            public CultureInfoWrap(CultureInfo cultureInfo)
            {
                Culture = cultureInfo;
            }

            public CultureInfo Culture;

            public override string ToString()
            {
                return Culture.EnglishName;
            }
        }

        #endregion
    }
}